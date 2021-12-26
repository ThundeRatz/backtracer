using Microsoft.EntityFrameworkCore;
using Backtracer.Persistence;
using Microsoft.AspNetCore.Mvc;
using Backtracer.Api.Extensions;
using Microsoft.AspNetCore.SpaServices.Extensions;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.InjectDependencies();

builder.Services.AddMvcCore();
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "ClientApp/build";
});

builder.Services.ConfigureSwagger();
builder.Services.AddRouting(o => o.LowercaseUrls = true);

var app = builder.Build();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.ConfigureSwagger();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.UseSpa(configuration => {
    configuration.Options.SourcePath = "ClientApp";

    if (env.IsDevelopment()) {
        configuration.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

app.Run();
