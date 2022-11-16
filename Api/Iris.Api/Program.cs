using ApiTemplate.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiTemplate.Domain.DomainServices.Authentication;
using ApiTemplate.Api.Middlewares;
using FluentValidation;
using ApiTemplate.Api.ViewModels.Validations;
using ApiTemplate.Domain.DTOs.Authentication;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infraestructure.Repositories;
using Iris.Api.ViewModels.Validations;
using Iris.Domain.DTOs.Tasks;
using Iris.Domain.DomainServices.Tasks;

//Create Loggger and store in amazon S3
Log.Logger = new LoggerConfiguration()
    .WriteTo.AmazonS3(
        $"logBackend-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt",
        "logsirisexam",
        Amazon.RegionEndpoint.USEast1,
        "AKIAT6BAPMNIYKXHIP5W",
        "YPKAZg+7JUvpGyab1v54P9ZW4uN1hJFwLNtX1VR5",
        restrictedToMinimumLevel: LogEventLevel.Error,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        new CultureInfo("es-CO"),
        levelSwitch: null,
        rollingInterval: Serilog.Sinks.AmazonS3.RollingInterval.Day,
        encoding: Encoding.Unicode)
    .CreateLogger();    

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IValidator<UserDTO>, UserDtoValidator>();
builder.Services.AddScoped<IValidator<TaskDto>, TaskDtoValidator>();
builder.Services.AddScoped<IValidator<TaskRequest>, TaskRequestValidator>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Host.UseSerilog();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
