using ChatApp.Api.Extensions.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlConnection(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureServiceManager();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
