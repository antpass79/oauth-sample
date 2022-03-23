using Microsoft.AspNetCore.Authentication.JwtBearer;
using OAuthMyLabService.Models;
using OAuthMyLabService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.Configure<WebServerStartingParameters>(builder.Configuration.GetSection(nameof(WebServerStartingParameters)));
builder.Services.Configure<OAuthSettings>(builder.Configuration.GetSection(nameof(OAuthSettings)));

builder.Services.AddScoped<ILoginService, MockLoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IClientSecretEncoder, ClientSecretBase64Encoder>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
