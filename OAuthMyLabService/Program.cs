using Microsoft.AspNetCore.Authentication.JwtBearer;
using OAuthMyLabService.Models;
using OAuthMyLabService.Services;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.Configure<OAuthSettings>(builder.Configuration.GetSection(nameof(OAuthSettings)));

builder.Services.AddScoped<ILoginService, MockLoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IClientSecretEncoder, ClientSecretBase64Encoder>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration

builder.WebHost
    .ConfigureAppConfiguration((context, options) =>
    {
        Console.WriteLine($"Environment Name {context.HostingEnvironment.EnvironmentName}");

        options
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureKestrel((context, serverOptions) =>
    {
        //serverOptions.ConfigureEndpoints();

        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.Compare(environmentName, "Development", true) == 0)
        {
            serverOptions.Configure(context.Configuration.GetSection("Kestrel"))
            .Endpoint("HTTPS", listenOptions =>
            {
                listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls12;
            });
        }
    });

var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
