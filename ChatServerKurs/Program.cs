using ChatServerKurs.Controllers.ChatHub;
using ChatServerKurs.Entites;
using ChatServerKurs.Functions.Message;
using ChatServerKurs.Functions.User;
using ChatServerKurs.Functions.UserFriend;
using ChatServerKurs.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppContext>(options =>
{
    options.UseSqlite("Data Source = text.db");
});

builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<ChatHub>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<JwtMiddleware>();
//app.MapControllers();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/ChatHub");
});

app.Run();