using Microsoft.EntityFrameworkCore;
using Backtracer.Persistence;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.AddUserSecrets(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(o => o.LowercaseUrls = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "docs";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
