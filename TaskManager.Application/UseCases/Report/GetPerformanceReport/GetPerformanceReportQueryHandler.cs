using System.Net;
using MongoDB.Bson;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Report.GetPerformanceReport;

public class GetPerformanceReportQueryHandler :  IQueryHandler<GetPerformanceReportQuery, List<PerformanceReportViewModel>>
{
    private readonly IReportRepository _reportRepository;
    private readonly IUserRepository _userRepository;

    public GetPerformanceReportQueryHandler(IReportRepository reportRepository, IUserRepository userRepository)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<List<PerformanceReportViewModel>>> Handle(GetPerformanceReportQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.IdManager, out _))
            return await Result.FailureAsync<List<PerformanceReportViewModel>>(null, "Invalid IdManager", HttpStatusCode.NotFound);
        
        var user = await _userRepository.Get(request.IdManager);
        
        if(user is not { Role: Role.Manager })
            return await Result.FailureAsync<List<PerformanceReportViewModel>>(null, "Unauthorized", HttpStatusCode.NotFound);

        var reportData = _reportRepository.GetPerformanceReport(request.Year, request.Month);
        var users = await _userRepository.GetUsers();
        var report = reportData.Select(reportRow => {
            var user = users.First(z => z.Id == ObjectId.Parse(reportRow.Item1));
            return new PerformanceReportViewModel(reportRow.Item1, user.Name, user.Role.ToString(), reportRow.Item2);
        }).ToList();
        
        return await Result.SuccessAsync(report);
    }
}