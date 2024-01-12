using ChatServerKurs;
using ChatServerKurs.Controllers.ChatHub;
using ChatServerKurs.Entites;
using ChatServerKurs.Functions.Message;
using ChatServerKurs.Functions.User;
using ChatServerKurs.Functions.UserFriend;
using ChatServerKurs.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

while (true)
{
    Console.WriteLine("Select encryption type:");
    Console.WriteLine("1. Caesar ");
    Console.WriteLine("2. Abash");
    Console.WriteLine("3. Verman");
    Console.WriteLine("4. With out crypt");


    // Считываем выбор пользователя
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CryptChoiceHandler.ChosenCrypt = Crypts.Caesar;
            break;
        case "2":
            CryptChoiceHandler.ChosenCrypt = Crypts.PolybiusSquare;

            break;
        case "3":
            CryptChoiceHandler.ChosenCrypt = Crypts.Verman;

           break;
        case "4":
            CryptChoiceHandler.ChosenCrypt = Crypts.Non;


            break;
        default:
            Console.WriteLine("Select an existing menu item.");
            continue;
    }
    break;
}

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