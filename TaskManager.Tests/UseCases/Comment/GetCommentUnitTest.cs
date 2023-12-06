using MongoDB.Bson;
using Moq;
using TaskManager.Application.UseCases.Comment.GetComments;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Comment;

public class GetCommentUnitTest
{
    [Fact]
    public async Task Handle_ValidIdTask_ReturnsSuccessResultWithComments()
    {
        // Arrange
        var mockCommentRepository = new Mock<ICommentRepository>();
        var comments = new List<Domain.Entity.Comment>
        {
            new Domain.Entity.Comment {  },
            new Domain.Entity.Comment {  }
        };
        mockCommentRepository.Setup(repo => repo.GetByTask(It.IsAny<string>())).ReturnsAsync(comments);

        var handler = new GetCommentsQueryHandler(mockCommentRepository.Object);
        var query = new GetCommentsQuery(ObjectId.GenerateNewId().ToString());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
        Assert.Equal(comments.Count, result.Payload.Count);
        mockCommentRepository.Verify(repo => repo.GetByTask(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidIdTask_ReturnsFailureResult()
    {
        // Arrange
        var mockCommentRepository = new Mock<ICommentRepository>();
        var handler = new GetCommentsQueryHandler(mockCommentRepository.Object);
        var query = new GetCommentsQuery("invalidObjectId");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Payload);
        Assert.Equal("Invalid IdTask", result.FailureReason);
        mockCommentRepository.Verify(repo => repo.GetByTask(It.IsAny<string>()), Times.Never);
    }
}