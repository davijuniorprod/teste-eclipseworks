using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Domain.Entity
{
    public class Comment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public DateTime DtCreated { get; set; }

        public Comment()
        {
            
        }

        public Comment(string idTask, string idUser, string name, string role, string content)
        {
            Id = ObjectId.GenerateNewId();
            IdTask = idTask;
            IdUser = idUser;
            Name = name;
            Role = role;
            Content = content;
            DtCreated = DateTime.Now;
        }
    }
}
