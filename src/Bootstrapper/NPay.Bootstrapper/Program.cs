using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NPay.Modules.Notifications.Api;
using NPay.Modules.Users.Api;
using NPay.Modules.Wallets.Api;
using NPay.Shared;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services
    .AddNotificationsModule()
    .AddUsersModule()
    .AddWalletsModule()
    .AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();
app.UseNotificationsModule();
app.UseUsersModule();
app.UseWalletsModule();
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