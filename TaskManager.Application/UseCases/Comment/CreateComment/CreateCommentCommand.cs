using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Comment.CreateComment;

public class CreateCommentCommand :  ICommand
{
    public string IdTask { get; set; }
    public string IdUser { get; set; }
    public string Comment { get; set; }
}