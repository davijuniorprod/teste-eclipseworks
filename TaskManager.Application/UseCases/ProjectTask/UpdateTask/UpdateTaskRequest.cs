namespace TaskManager.Application.UseCases.ProjectTask.UpdateTask;

public class UpdateTaskRequest
{
    public string IdUser { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Status { get; set; }
}