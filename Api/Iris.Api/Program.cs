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
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

//Create Loggger and store in amazon S3
Log.Logger = new LoggerConfiguration()
    .WriteTo.AmazonS3(
        $"logBackend-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt",
        "logsirisexam",
        Amazon.RegionEndpoint.USEast1,
        builder.Configuration.GetSection("awsCredentials:awsAccessKeyId").Value,
        builder.Configuration.GetSection("awsCredentials:awsSecretAccessKey").Value,
        restrictedToMinimumLevel: LogEventLevel.Error,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        new CultureInfo("es-CO"),
        levelSwitch: null,
        rollingInterval: Serilog.Sinks.AmazonS3.RollingInterval.Day,
        encoding: Encoding.Unicode)
    .CreateLogger();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Iris_Api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n First Get yout JWT using Authenticate Endpoint. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
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

//Global Exception Handler
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("MyAllowSpecificOrigins");

app.Run();
