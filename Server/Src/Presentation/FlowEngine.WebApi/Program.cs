using FlowEngine.Application;
using FlowEngine.Application.Interfaces;
using FlowEngine.Infrastructure.Identity;
using FlowEngine.Infrastructure.Identity.Contexts;
using FlowEngine.Infrastructure.Identity.Models;
using FlowEngine.Infrastructure.Identity.Seeds;
using FlowEngine.Infrastructure.Persistence;
using FlowEngine.Infrastructure.Persistence.Contexts;
using FlowEngine.Infrastructure.Persistence.Seeds;
using FlowEngine.Infrastructure.Resources;
using FlowEngine.Infrastructure.Worker;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEngine.WebApi.Infrastructure.Middlewares;
using FlowEngine.WebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

bool useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration, useInMemoryDatabase);
builder.Services.AddIdentityInfrastructure(builder.Configuration, useInMemoryDatabase);
builder.Services.AddResourcesInfrastructure();
builder.Services.AddWorkerInfrastructure();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddMediator();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();
builder.Services.AddAnyCors();
builder.Services.AddAuthorization();
builder.Services.AddCustomLocalization(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddEndpoints();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    if (!useInMemoryDatabase)
    {
        await services.GetRequiredService<IdentityContext>().Database.MigrateAsync();
        await services.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    }

    //Seed Data
    await DefaultRoles.SeedAsync(services.GetRequiredService<RoleManager<ApplicationRole>>());
    await DefaultBasicUser.SeedAsync(services.GetRequiredService<UserManager<ApplicationUser>>());
    await DefaultData.SeedAsync(services.GetRequiredService<ApplicationDbContext>());

    await services.GetRequiredService<IFlowEngineServices>().LoadData(0);
}

app.UseCustomLocalization();
app.UseAnyCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomSwagger();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHealthChecks("/health");
app.MapEndpoints();
app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.Run();

public partial class Program
{
}
