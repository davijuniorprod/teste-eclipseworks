using TaskManager.Application.ViewModel;
using TaskManager.Domain.Entity;

namespace TaskManager.Application.Extensions;

public static class ViewModelExtensions
{
    public static ProjectViewModel ToViewModel(this Domain.Entity.Project project) =>
        new() {
            Id = project.Id.ToString()!,
            Name = project.Name,
            Description = project.Description,
        };
    
    public static ProjectTaskViewModel ToViewModel(this Domain.Entity.ProjectTask project)
    {
        return new ProjectTaskViewModel {
            Id = project.Id.ToString()!,
            IdProject = project.IdProject,
            IdUser = project.IdUser,
            Description = project.Description,
            Status = project.Status,
            Title = project.Title,
            DueDate = project.DueDate,
            FinishDate = project.FinishDate,
            Priority = project.Priority,
        };
    }
    
    public static CommentViewModel ToViewModel(this Domain.Entity.Comment comment) =>
        new() {
            Id = comment.Id.ToString()!,
            IdTask = comment.IdTask,
            IdUser = comment.IdUser,
            Name = comment.Name,
            Role = comment.Role,
            Content = comment.Content,
            DtCreated = comment.DtCreated,
        };
   
}