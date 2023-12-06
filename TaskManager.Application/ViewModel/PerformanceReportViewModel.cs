namespace TaskManager.Application.ViewModel;

public class PerformanceReportViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int Quantity { get; set; }

    public PerformanceReportViewModel()
    {
        
    }
    public PerformanceReportViewModel(string id, string name, string role, int quantity)
    {
        Id = id;
        Name = name;
        Role = role;
        Quantity = quantity;
    }
}