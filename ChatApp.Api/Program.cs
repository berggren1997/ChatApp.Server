using ChatApp.Api.Extensions.Middleware;
using ChatApp.Api.Extensions.Service;
using ChatApp.Api.SignalR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlConnection(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureUserAccessorService();
builder.Services.ConfiugreSwaggerAuthentication();
builder.Services.ConfigureConversationAuthorizationHandler();
builder.Services.AddSignalR();

builder.Services.ConfigureCustomAuthPolicy();

var app = builder.Build();


app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");

app.UseAuthentication();

app.UseAuthorization();

//app.UseRouting();

app.MapHub<ChatHub>("/chathub");

app.MapControllers();

app.Run();
