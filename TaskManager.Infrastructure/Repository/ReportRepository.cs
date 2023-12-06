using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Domain.Entity;
using TaskManager.Domain.Enum;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Repository;

public class ReportRepository :  IReportRepository
{
    private readonly IMongoCollection<ProjectTask> _collection;
    
    public ReportRepository(IMongoDatabase mongoDatabase) => _collection = mongoDatabase.GetCollection<ProjectTask>(nameof(ProjectTask));
    
    public List<Tuple<string, int>> GetPerformanceReport(int year, int month)
    {
        const int firstDayOfMonth = 1;
        const int oneMonth = 1;
        const int lastSecondOfMonth = 1;
        
        var initialRange = new DateTime(year, month, firstDayOfMonth);
        var finalRange = initialRange.AddMonths(oneMonth).AddSeconds(-lastSecondOfMonth);
        var matchStage = new BsonDocument("$match", new BsonDocument {
            new BsonDocument(nameof(ProjectTask.Status), Status.Done),
            new BsonDocument(nameof(ProjectTask.FinishDate), new BsonDocument {
                new BsonDocument("$gte", initialRange),
                new BsonDocument("$lte", finalRange)
            }),
        });

        var groupStage = new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$IdUser" },
            { "Tasks", new BsonDocument("$sum", 1) },
        });


        var pipeline = PipelineDefinition<ProjectTask, BsonDocument>.Create(matchStage, groupStage);
        var results = _collection.Aggregate(pipeline).ToList();
        var report = results.Select(x => new Tuple<string, int>(x["_id"].AsString, x["Tasks"].AsInt32));

        return report.ToList();
    }
}