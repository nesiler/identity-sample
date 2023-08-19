using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using ISP.Application.Exceptions;
using ISP.Application.Validators.Identity;
using ISP.Domain.Entities.Identity;
using ISP.Infrastructure.Auth.Jwt;
using ISP.Infrastructure.Factory;
using ISP.Infrastructure.Filters;
using ISP.Infrastructure.Mappers;
using ISP.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
Env.TraversePath().Load();

// Add services to the container.
//add autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });

var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//add asp.net identity
builder.Services.AddIdentity<User, Role>()
    .AddRoles<Role>()
    .AddUserValidator<UserValidator>()
    .AddRoleValidator<RoleValidator>()
    .AddPasswordValidator<PasswordValidator>()
    .AddEntityFrameworkStores<ISPContext>()
    .AddDefaultTokenProviders();

var connectionString = Env.GetString("CONNECTION__STRING") ??
                       throw new NotFoundException("DotNetEnv.Env.GetString(\"CONNECTION__STRING\")");
builder.Services.AddDbContext<ISPContext>(options =>
    options.UseNpgsql(connectionString
    ));

builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

builder.Services
    .AddAuthentication(authentication =>
    {
        authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//add fluent validation

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache(c => c.TrackStatistics = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ISP.API v1");
    options.DocumentTitle = "ISP API Documentation";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();