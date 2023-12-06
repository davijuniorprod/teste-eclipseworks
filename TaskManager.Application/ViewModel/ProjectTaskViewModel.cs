using TaskManager.Domain.Enum;

namespace TaskManager.Application.ViewModel;

public class ProjectTaskViewModel
{
    public string Id { get; set; }
    public string IdProject { get; set; }
    public string IdUser { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}