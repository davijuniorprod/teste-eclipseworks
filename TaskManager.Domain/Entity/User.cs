using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Enum;

namespace TaskManager.Domain.Entity
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        
        public User() { }
        
        public User(string name, Role role)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Role = role;
        }
    }
}
