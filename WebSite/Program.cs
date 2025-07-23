using Fanex.NAP.Client;
using Fanex.NAP.Client.Attributes;
using Fanex.RemotingRef;
using Microsoft.AspNetCore.Mvc;
using ONEbook.UM.DependencyInjection;
using ONEbook.UM.Filters;
using Scalar.AspNetCore;
using WebSite;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddOpenApi();
services.AddSession();
services.AddRazorPages();
services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ServiceFilterAttribute(typeof(CheckUMFilterAttribute)));
    options.Filters.Add(new SessionAuthorizeAttribute());
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
});
services.AddRazorPages();
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
services.AddHealthChecks();

// Add Session
services.AddOneBookUmForWeb(configuration);
services.AddDistributedMemoryCache();
services.AddConfigRedisCache(configuration);
services.InitializeServices(configuration);
services.AddAuthentication(configuration);
services.AddNapClient(configuration);

RefDataTypeContainer
           .ConnectTo(new Uri(configuration.GetSection("Fanex.RemotingRef.Service.Url").Value))
           .FromApplication("Settlement");
services.Configure<IConfiguration>(configuration);
services.AddSingleton<IRemotingRefService, RemotingRefService>();



var app = builder.Build();

app.UseDeveloperExceptionPage();
app.MapHealthChecks("/healthz");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();