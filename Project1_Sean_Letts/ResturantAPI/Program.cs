using Resturant;
using Resturant.Resturant;
using User;

string connectionStringFilePath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project1_Sean_Letts/User/UserDatabase/SQLinfo.txt";
string connectionString = File.ReadAllText(connectionStringFilePath);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers(options => options.RespectBrowserAcceptHeader = true)
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserLogic>(userLogic => new UserLogic(connectionString));
builder.Services.AddScoped<ResturantLogic>(resLogic => new ResturantLogic(connectionString));
builder.Services.AddScoped<ReviewLogic>(revLogic => new ReviewLogic(connectionString));

var app = builder.Build();
app.Logger.LogInformation("App Started");
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
