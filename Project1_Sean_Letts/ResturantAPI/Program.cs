global using Serilog;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Resturant;
using Resturant.Resturant;
using ResturantAPI.Repository;
using System.Reflection;
using System.Text;
using User;
//Users/Owner/Desktop/Revature/Sean-Letts/Project1_Sean_Letts/User/UserDatabase
string connectionStringFilePath = "../User/UserDatabase/SQLinfo.txt";
string connectionString = File.ReadAllText(connectionStringFilePath);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("../UserInterface/Logs/APILogs.txt").MinimumLevel.Debug().MinimumLevel.Information()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//access settings file in json
ConfigurationManager config = builder.Configuration;

// Add services to the container.

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    var key = Encoding.UTF8.GetBytes(config["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Key"],
        ValidAudience = config["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddMemoryCache();
builder.Services.AddControllers(options => options.RespectBrowserAcceptHeader = true)
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sean's Resturant App",
        Description = "An ASP.NET Core Web API for displaying resturant info",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<UserLogic>(userLogic => new UserLogic(connectionString));
builder.Services.AddScoped<ResturantLogic>(resLogic => new ResturantLogic(connectionString));
builder.Services.AddScoped<ReviewLogic>(revLogic => new ReviewLogic(connectionString));
builder.Services.AddScoped<IJWTManagerRepo, JWTManagerRepo>();

var app = builder.Build();
app.Logger.LogInformation("App Started");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();