
using Account.API.Extensions;
using Account.API.Middlewares;
using Azure.Identity;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Shared.Data.Contexts;
using Shared.ExternalServices.DTOs;

var builder = WebApplication.CreateBuilder(args);

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
IConfiguration config = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSingleton(config);

ServiceExtension.RegisterServices(builder.Services, config, myAllowSpecificOrigins);

builder.Services.AddEndpointsApiExplorer();

//Configuring application to use App Config
var appConnectionString = builder.Configuration.GetConnectionString("AppConfig");
var useAppConfiguration = !string.IsNullOrWhiteSpace(appConnectionString);

if (useAppConfiguration)
{
    builder.Host.ConfigureAppConfiguration(cfg => {
        cfg.AddAzureAppConfiguration(options =>
        {
            options.Connect(appConnectionString)
                .ConfigureRefresh(refresh =>
                {
                    refresh.Register("Test").SetCacheExpiration(
                        TimeSpan.FromSeconds(20));
                });

            if (Convert.ToBoolean(builder.Configuration["UsingAzureKeyVault"]))
                options.ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
        });
    });

    builder.Services.AddAzureAppConfiguration();
}



void ConfigureKissLog(IOptionsBuilder options)
{
    KissLogConfiguration.Listeners
        .Add(new RequestLogsApiListener(new Application(config["KissLog.OrganizationId"], config["KissLog.ApplicationId"]))
        {
            ApiUrl = config["KissLog.ApiUrl"]
        });
}

var app = builder.Build();

app.Use((ctx, next) =>
{
    var headers = ctx.Response.Headers;

    headers.Add("X-Frame-Options", "DENY");
    headers.Add("X-XSS-Protection", "1; mode=block");
    headers.Add("X-Content-Type-Options", "nosniff");
    headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
    headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
    headers.Add("Pragma", "no-cache");

    headers.Remove("X-Powered-By");
    headers.Remove("x-aspnet-version");

    // Some headers won't remove
    headers.Remove("Server");

    return next();
});

// Configure the HTTP request pipeline.
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccountMicroservice v1");
    c.RoutePrefix = string.Empty;
});


if (useAppConfiguration)
    app.UseAzureAppConfiguration();

app.UseMiddleware<ExceptionMiddleware>();
app.UseKissLogMiddleware(options => ConfigureKissLog(options));
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
