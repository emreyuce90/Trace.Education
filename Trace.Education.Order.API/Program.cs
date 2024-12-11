
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Trace.Education.Order.API.OpenTelemetry;
using Trace.Education.Order.API.Repositories.Order;
using Trace.Education.Shared;

namespace Trace.Education.Order.API;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenTelemetryExt(builder.Configuration);
        builder.Services.AddOrdersExt();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<RequestAndResponseTelemetryMiddleware>();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
