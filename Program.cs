using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagementApi;  // Ensure this is present for TaskDbContext
using TaskManagementApi.Services;  // Ensure this is present for TaskService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "A simple API for managing tasks"
    });
});

// Add In-Memory Database
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb")); // Use in-memory database

// Register TaskService
builder.Services.AddScoped<TaskService>();  // Add this line to register the TaskService

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
