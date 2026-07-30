using ChatBotApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Load chatbot with your dataset file
builder.Services.AddSingleton(new ChatBotService("data.txt"));

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();