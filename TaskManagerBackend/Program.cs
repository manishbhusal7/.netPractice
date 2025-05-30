using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Manish Task Managing Api",
        Version = "v1",
        Description = "API for managing tasks"
    });
});

// Configure SQLite database (replaces in-memory DB)
builder.Services.AddDbContext<TaskDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("LocalDb")));

// Configure CORS to allow React's port (3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React's default port
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API v1");
    });
    
    // Apply database migrations automatically in development
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseCors("ReactPolicy"); // Use the CORS policy here
app.MapControllers();
app.Run();


