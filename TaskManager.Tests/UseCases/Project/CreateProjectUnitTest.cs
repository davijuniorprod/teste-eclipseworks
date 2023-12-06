using System.Net;
using Moq;
using TaskManager.Application.Extensions;
using TaskManager.Application.UseCases.Project.CreateProject;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Project;

public class CreateProjectUnitTest
{
    [Fact]
    public async Task Handle_ShouldCreateProjectAndReturnSuccessResult()
    {
        // Arrange
        var mockProjectRepository = new Mock<IProjectRepository>();
        var commandHandler = new CreateProjectCommandHandler(mockProjectRepository.Object);
        var createProjectCommand = new CreateProjectCommand {
            Name = "Test Project",
            Description = "Test Description"
        };
        var expectedProject = new Domain.Entity.Project(createProjectCommand.Name, createProjectCommand.Description);
        var expectedViewModel = expectedProject.ToViewModel();

        mockProjectRepository.Setup(repo => repo.Create(It.IsAny<Domain.Entity.Project>()))
            .ReturnsAsync(expectedProject)
            .Verifiable();

        // Act
        var result = await commandHandler.Handle(createProjectCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(expectedViewModel.Description, result.Payload.Description);
        Assert.Equal(expectedViewModel.Name, result.Payload.Name);
        
        mockProjectRepository.Verify(repo => repo.Create(It.IsAny<Domain.Entity.Project>()), Times.Once);
    }
}