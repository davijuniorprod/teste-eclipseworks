using System.Net;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.UseCases.Comment.CreateComment;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Comment;

public class CreateCommentUnitTest
{
    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnCreatedStatusCode()
    {
        // Arrange
        var mockTaskRepository = new Mock<ITaskRepository>();
        var mockUserRepository = new Mock<IUserRepository>();
        var mockCommentRepository = new Mock<ICommentRepository>();
        var handler = new CreateCommentCommandHandler(mockTaskRepository.Object, mockUserRepository.Object, mockCommentRepository.Object);

        var command = new CreateCommentCommand
        {
            IdTask = ObjectId.GenerateNewId().ToString(),
            IdUser = ObjectId.GenerateNewId().ToString(),
            Comment = "Test Comment"
        };

        mockUserRepository.Setup(repo => repo.Get(command.IdUser)).ReturnsAsync(new Domain.Entity.User());
        mockTaskRepository.Setup(repo => repo.Get(command.IdTask)).ReturnsAsync(new Domain.Entity.ProjectTask());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        mockCommentRepository.Verify(repo => repo.Insert(It.IsAny<Domain.Entity.Comment>()), Times.Once);
    }
}