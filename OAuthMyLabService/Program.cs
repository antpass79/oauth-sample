using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OAuthMyLabService.Models;
using OAuthMyLabService.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services
    .AddOptions<JwtBearerOptions>()
    .Configure<IOptions<OAuthSettings>>((options, optionsService) =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            IssuerSigningKeys = optionsService.Value.Audiences?.Select(audience => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(audience.ClientSecret ?? String.Empty))),
            ValidAudiences = optionsService.Value.Audiences?.Select(audience => audience.ClientId),
            ValidIssuer = optionsService.Value.Issuer,
        };
    });

builder.Services.Configure<WebServerStartingParameters>(builder.Configuration.GetSection(nameof(WebServerStartingParameters)));
builder.Services.Configure<OAuthSettings>(builder.Configuration.GetSection(nameof(OAuthSettings)));

builder.Services.AddScoped<ILoginService, MockLoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

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
