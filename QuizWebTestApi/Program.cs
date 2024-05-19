using FluentValidation.AspNetCore;
using log4net.Config;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quiz.Contracts.Helpers;
using Wattana.Infrastructure;
using Quiz.Core;
using Wattana.Web.Filters;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
// Add services to the container.
builder.Services.AddDateTime();
builder.Services.AddFileService();
builder.Services.AddIdentityService();
builder.Services.AddAES256Service();
builder.Services.AddRandomService();
builder.Services.AddDatabaseManager();
builder.Services.AddPersistence();
builder.Services.AddJwtService();
builder.Services.AddEnumService();
builder.Services.AddLoggingService();
builder.Services.AddJwtTokenAuthentication(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(o => o.AddPolicy("Default", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Content-Disposition");
}));

// Add services to the container.
builder.Services.AddCore();
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz APIs", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{ ""}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


XmlConfigurator.Configure(new FileInfo("log4net.config"));
builder.Services.AddSingleton(LogManager.GetLogger(typeof(Program)));
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();


builder.Services.AddControllersWithViews(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("SAP").AddHttpMessageHandler<LoggingHandler>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
