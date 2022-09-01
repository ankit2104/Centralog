using Core;

ServiceBuilder serviceBuilder = new ServiceBuilder(WebApplication.CreateBuilder(args));
var builder = serviceBuilder.GetWebApplicationBuilder();
Core.ApplicationBuilder applicationBuilder = new Core.ApplicationBuilder(builder.Build());
var app = applicationBuilder.GetWebApplication();
app.Run();

