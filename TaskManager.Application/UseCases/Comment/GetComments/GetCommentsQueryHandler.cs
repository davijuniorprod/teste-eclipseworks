using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;
using TaskManager.Core.Handlers.Interfaces;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application.UseCases.Comment.GetComments;

public class GetCommentsQueryHandler : IQueryHandler<GetCommentsQuery, List<CommentViewModel>>
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Result<List<CommentViewModel>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.IdTask, out _))
            return await Result.FailureAsync<List<CommentViewModel>>(null, "Invalid IdTask");

       var comments = await _commentRepository.GetByTask(request.IdTask);
       var mapped = comments.Select(x => x.ToViewModel()).ToList();
       
       return await Result.SuccessAsync(mapped);
    }
}