namespace TaskManager.Application.ViewModel;

public class CommentViewModel
{
    public string Id { get; set; }
    public string IdTask { get; set; }
    public string IdUser { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Content { get; set; }
    public DateTime DtCreated { get; set; }
}