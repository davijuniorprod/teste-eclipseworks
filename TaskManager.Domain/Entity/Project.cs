using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Domain.Entity
{
    public class Project
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Project()
        {
        }

        public Project(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
