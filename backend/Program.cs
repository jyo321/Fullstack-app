using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------
builder.Services.AddControllers();

// Load generator (if you have it)
builder.Services.AddHostedService<LoadGenerator>();

// -------------------- OPENTELEMETRY --------------------
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService("dotnet-backend"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://otel-collector:4317");
            });
    });

// -------------------- BUILD APP --------------------
var app = builder.Build();

// -------------------- ROUTES --------------------
app.MapControllers();

// -------------------- RUN --------------------
app.Run("http://0.0.0.0:5000");