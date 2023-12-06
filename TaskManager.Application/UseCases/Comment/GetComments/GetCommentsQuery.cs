using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Comment.GetComments;

public class GetCommentsQuery : IQuery<List<CommentViewModel>>
{
    public string IdTask { get; set; }

    public GetCommentsQuery(string idTask)
    {
        IdTask = idTask;
    }
}