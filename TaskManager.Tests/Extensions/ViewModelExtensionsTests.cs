using MongoDB.Bson;
using TaskManager.Application.Extensions;
using TaskManager.Domain.Entity;
using TaskManager.Domain.Enum;

namespace TaskManager.Tests.Extensions;

public class ViewModelExtensionsTests
{
    [Fact]
    public void CanConvertProjectToViewModel()
    {
        // Arrange
        var name = "Project For UnitTest";
        var description = "Description For UnitTest";
        var project = new Project(name, description);

        // Act
        var viewModel = project.ToViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(project.Id.ToString(), viewModel.Id);
        Assert.Equal(project.Name, viewModel.Name);
        Assert.Equal(project.Description, viewModel.Description);
    }
    
    [Fact]
    public void CanConvertTaskToViewModel()
    {
        // Arrange
        var idProject = ObjectId.GenerateNewId().ToString();
        var idUser = ObjectId.GenerateNewId().ToString();
        var title = "Title For UnitTest";
        var description = "Description For UnitTest";
        var dueDate = DateTime.Now;
        var priority = Priority.Medium;
        var task = new ProjectTask(idProject, idUser, title, description, dueDate, priority);

        // Act
        var viewModel = task.ToViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(task.Id.ToString(), viewModel.Id);
        Assert.Equal(task.IdProject, viewModel.IdProject);
        Assert.Equal(task.IdUser, viewModel.IdUser);
        Assert.Equal(task.Title, viewModel.Title);
        Assert.Equal(task.DueDate, viewModel.DueDate);
        Assert.Equal(task.FinishDate, viewModel.FinishDate);
        Assert.Equal(task.Status, viewModel.Status);
        Assert.Equal(task.Priority, viewModel.Priority);
    }
    
    [Fact]
    public void CanConvertCommentToViewModel()
    {
        // Arrange
        var idTask = ObjectId.GenerateNewId().ToString();
        var idUser = ObjectId.GenerateNewId().ToString();
        var name = "Name Of The User";
        var role = "Role Of The User";
        var content = "Role Of The User";
        var comment = new Comment(idTask, idUser, name, role, content);

        // Act
        var viewModel = comment.ToViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(comment.Id.ToString(), viewModel.Id);
        Assert.Equal(comment.IdTask, viewModel.IdTask);
        Assert.Equal(comment.IdUser, viewModel.IdUser);
        Assert.Equal(comment.Name, viewModel.Name);
        Assert.Equal(comment.Role, viewModel.Role);
        Assert.Equal(comment.Content, viewModel.Content);
    }
}