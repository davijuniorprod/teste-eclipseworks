using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Enum;

namespace TaskManager.Domain.Entity
{
    public class ProjectTask
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdProject { get; set; }
        public string IdUser { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public ProjectTask()
        {
        }

        public ProjectTask(string idProject, string idUser, string title, string description, DateTime? dueDate, Priority priority)
        {
            Id = ObjectId.GenerateNewId();
            IdProject = idProject;
            IdUser = idUser;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = Status.Pending;
            Priority = priority;
        }
        
        public ProjectTask Clone() => (ProjectTask) MemberwiseClone ();
    }
}
