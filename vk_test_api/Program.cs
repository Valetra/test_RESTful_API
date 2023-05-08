using Microsoft.EntityFrameworkCore;
using vk_test_api.Database;
using vk_test_api.Data.Models.Base;
using System.Text.Json.Serialization;
using vk_test_api.Core.Services.Implimentations;
using vk_test_api.Core.Services.Interfaces;
using vk_test_api.Data.Models;
using vk_test_api.Data.Repositories.Implimentations;
using vk_test_api.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var configuration = builder.Configuration;

builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IBaseRepository<UserGroup>, BaseRepository<UserGroup>>();
builder.Services.AddScoped<IBaseRepository<UserState>, BaseRepository<UserState>>();
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer> ();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

//Init db
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
databaseInitializer.SeedAsync().Wait();


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