using Moq;
using TaskManager.Application.UseCases.Project.ListProjects;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Tests.UseCases.Project;

public class ListProjectsUnitTest
{
    [Fact]
    public async Task Handle_ReturnsProjectViewModelList()
    {
        // Arrange
        var mockProjectRepository = new Mock<IProjectRepository>();
        var projects = new List<Domain.Entity.Project >
        {
            new Domain.Entity.Project {  },
            new Domain.Entity.Project  {  }
        };
        mockProjectRepository.Setup(repo => repo.List()).ReturnsAsync(projects);
        var handler = new ListProjectsQueryHandler(mockProjectRepository.Object);
        var query = new ListProjectsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(projects.Count, result.Payload.Count);
        mockProjectRepository.Verify(repo => repo.List(), Times.Once);
    }
}