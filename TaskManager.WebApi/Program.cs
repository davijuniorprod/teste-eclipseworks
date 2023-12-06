using System.Text.Json.Serialization;
using MongoDB.Driver;
using TaskManager.Application;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Environment Variables
var mongoDbServer = Environment.GetEnvironmentVariable("MONGODB_SERVER");
var mongoDbDatabase = Environment.GetEnvironmentVariable("MONGODB_DATABASE");

// Repositories
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IProjectTaskHistoryRepository, ProjectTaskHistoryRepository>();

// Libraries
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly));
builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoDatabase>(_ => {
    var client = new MongoClient(mongoDbServer);
    return client.GetDatabase(mongoDbDatabase);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
