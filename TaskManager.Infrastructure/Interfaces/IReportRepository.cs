namespace TaskManager.Infrastructure.Interfaces;

public interface IReportRepository
{
    List<Tuple<string, int>> GetPerformanceReport(int year, int month);
}