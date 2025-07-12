using Mailjet.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using pkbackend.interfaces;
using pkbackend.services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddSingleton<MailjetClient>(cl => new MailjetClient(
        config["Mailjet:ApiKeyPublic"],
        config["Mailjet:ApiKeyPrivate"]
    ));
builder.Services.AddScoped<IMailService, MailJetService>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Listen on port 8080 on all interfaces
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok("Hello");
});

app.Run();