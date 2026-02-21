using Asp.Versioning.ApiExplorer;
using AutoMapper;
using ERP.Api.Configuration;
using ERP.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SalesForce.Api.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddDbContext<SalesForceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.CacheConfiguration(builder.Configuration);

builder.Services.LoggerConfiguration();

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.HealthCheckConfiguration(builder.Configuration);

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure Pipeline
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment, builder.Configuration);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.Run();
