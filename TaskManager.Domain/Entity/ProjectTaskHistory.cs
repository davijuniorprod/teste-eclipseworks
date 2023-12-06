using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Domain.Entity
{
    public class ProjectTaskHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public ProjectTask After { get; set; }
        public ProjectTask Before { get; set; }
        
        public List<Comment> Comments { get; set; }
        public DateTime ChangeDate { get; set; }

        public ProjectTaskHistory()
        {
            
        }

        public ProjectTaskHistory(string idTask, string idUser, string name, string role, ProjectTask after, ProjectTask before, List<Comment> comments)
        {
            Id = ObjectId.GenerateNewId();
            IdTask = idTask;
            IdUser = idUser;
            Name = name;
            Role = role;
            After = after;
            Before = before;
            Comments = comments;
            ChangeDate = DateTime.Now;
        }
    }
}
