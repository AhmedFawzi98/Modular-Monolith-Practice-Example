using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NPay.Modules.Notifications.Api;
using NPay.Modules.Users.Api;
using NPay.Modules.Users.Core;
using NPay.Modules.Wallets.CompositionRoot;
using NPay.Modules.Wallets.Infrastructure;
using NPay.Shared;
using NPay.Shared.Messaging;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddNotificationsModule(builder.Configuration)
    .AddUsersModule(builder.Configuration)
    .AddWalletsModule(builder.Configuration)
    .AddSharedFramework(builder.Configuration)
    .AddMessaging(builder.Configuration, busconfig =>
    {
        busconfig
            .ConfigureMasstrasnitForWalletModule()
            .ConfigureMasstrasnitForNotificationsModule()
            .ConfigureMasstrasnitForUsersModule();
    });

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.EnableAnnotations();
    swagger.CustomSchemaIds(x => x.FullName);
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NPay API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSharedFramework();
app.UseNotificationsModule();
app.UseUsersModule();
app.UseWalletsModule();

app.UseSwagger();
app.UseReDoc(reDoc =>
{
    reDoc.RoutePrefix = "docs";
    reDoc.SpecUrl($"/swagger/v1/swagger.json");
    reDoc.DocumentTitle = "NPay API";
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("NPay API"));
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();