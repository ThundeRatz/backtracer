using Microsoft.EntityFrameworkCore;
using Backtracer.Persistence;
using Backtracer.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Backtracer.Api.Attributes;
using Backtracer.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.InjectDependencies();

builder.Services.AddMvcCore();
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.ConfigureSwagger();
builder.Services.AddRouting(o => o.LowercaseUrls = true);

var app = builder.Build();
app.UseStaticFiles();

app.ConfigureSwagger();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
