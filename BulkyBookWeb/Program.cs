
using BulkyBookWeb.Extensions;
using BulkyBookWeb.Metrics;
using OpenTelemetry;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<OtelMetrics>();

var meters = new OtelMetrics();
var meterProvider = Sdk.CreateMeterProviderBuilder()
    .AddMeter(meters.MetricName)
    .AddAspNetCoreInstrumentation()
    .AddRuntimeInstrumentation()
    .AddView(
        instrumentName: "orders-number-of-books",
        new ExplicitBucketHistogramConfiguration { Boundaries = new double[] { 1, 2, 5 } })
    .AddPrometheusHttpListener(
        options => options.UriPrefixes = new string[] { "http://localhost:9184/" })
    .Build();
//builder.Services.AddSingleton(meterProvider);
builder.Services.AddControllersWithViews();
builder.Services.ConfigureServices(builder.Configuration);

//builder.Services.ConfigureAddServicesExtension();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
