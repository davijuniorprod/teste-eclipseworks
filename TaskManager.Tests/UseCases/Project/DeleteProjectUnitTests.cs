using System.Net;
using MongoDB.Bson;
using Moq;
using TaskManager.Application.UseCases.Project.DeleteProject;
using TaskManager.Domain.Entity;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Project;

public class DeleteProjectUnitTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly DeleteProjectCommandHandler _handler;

    public DeleteProjectUnitTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new DeleteProjectCommandHandler(_mockProjectRepository.Object, _mockTaskRepository.Object);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var command = new DeleteProjectCommand(ObjectId.GenerateNewId().ToString());
        _mockProjectRepository.Setup(repo => repo.Get(command.Id)).ReturnsAsync(default(Domain.Entity.Project));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ProjectHasUnfinishedTasks_ReturnsConflictResult()
    {
        // Arrange
        var command = new DeleteProjectCommand(ObjectId.GenerateNewId().ToString());
        var project = new Domain.Entity.Project();
        var tasks = new[] { new ProjectTask { Status = Status.Working } };

        _mockProjectRepository.Setup(repo => repo.Get(command.Id)).ReturnsAsync(project);
        _mockTaskRepository.Setup(repo => repo.GetByProjectId(command.Id)).ReturnsAsync(tasks.ToList);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ProjectCanBeDeleted_ReturnsSuccessResult()
    {
        // Arrange
        var command = new DeleteProjectCommand(ObjectId.GenerateNewId().ToString());
        var project = new Domain.Entity.Project();
        var tasks = new[] { new ProjectTask { Status = Status.Done } };

        _mockProjectRepository.Setup(repo => repo.Get(command.Id)).ReturnsAsync(project);
        _mockTaskRepository.Setup(repo => repo.GetByProjectId(command.Id)).ReturnsAsync(tasks.ToList);
        _mockProjectRepository.Setup(repo => repo.Delete(command.Id)).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        
        _mockProjectRepository.Verify(repo => repo.Delete(command.Id), Times.Once);
    }
}