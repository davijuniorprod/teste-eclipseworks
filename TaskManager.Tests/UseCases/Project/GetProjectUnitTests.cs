using System.Net;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.Extensions;
using TaskManager.Application.UseCases.Project.GetProject;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Project;

public class GetProjectUnitTests
{
    [Fact]
    public async Task Handle_InvalidObjectId_ReturnsFailureResult()
    {
        // Arrange
        var projectRepositoryMock = new Mock<IProjectRepository>();
        var handler = new GetProjectQueryHandler(projectRepositoryMock.Object);
        var query = new GetProjectQuery("invalid_object_id");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Invalid IdProject", result.FailureReason);
    }

    [Fact]
    public async Task Handle_ValidObjectId_ReturnsSuccessResult()
    {
        // Arrange
        var project = new Domain.Entity.Project
        {
            
        };
        var projectViewModel = project.ToViewModel();

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync(project);
        var handler = new GetProjectQueryHandler(projectRepositoryMock.Object);
        var query = new GetProjectQuery(ObjectId.GenerateNewId().ToString());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(projectViewModel.Description, result.Payload.Description);
        Assert.Equal(projectViewModel.Description, result.Payload.Name);
    }
}