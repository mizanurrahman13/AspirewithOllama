using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.AddKeyedOllamaSharpChatClient("deepseek");
builder.AddKeyedOllamaSharpChatClient("llama");

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapPost("/deepseek", async ([FromKeyedServices("deepseek")] IChatClient client, string prompt) =>
{
    var response = await client.CompleteAsync(prompt);
    return response.Message;
});

app.MapPost("/llama", async ([FromKeyedServices("llama")] IChatClient client, string prompt) =>
{
    var response = await client.CompleteAsync(prompt);
    return response.Message;
});

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
