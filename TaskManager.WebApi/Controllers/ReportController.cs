using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Report.GetPerformanceReport;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;

namespace TaskManager.WebApi.Controllers;

public class ReportController :  ApiControllerBase
{
    public ReportController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    [HttpGet("performance-report")]
    [ProducesResponseType(typeof(Result<List<PerformanceReportViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> Get(string idManager, int? year, int? month) 
        => ExecuteQueryAsync<GetPerformanceReportQuery, List<PerformanceReportViewModel>>(new GetPerformanceReportQuery(idManager, year ?? DateTime.Now.Year, month ?? DateTime.Now.Month));
}