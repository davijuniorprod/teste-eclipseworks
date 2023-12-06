using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Report.GetPerformanceReport;

public class GetPerformanceReportQuery : IQuery<List<PerformanceReportViewModel>>
{
    public string IdManager { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }

    public GetPerformanceReportQuery(string idManager, int year, int month)
    {
        IdManager = idManager;
        Year = year;
        Month = month;
    }
}