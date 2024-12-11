
using Microsoft.EntityFrameworkCore;
using Trace.Education.Order.API.Feature.Order;
using Trace.Education.Order.API.OpenTelemetry;
using Trace.Education.Order.API.Repositories;
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
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString:builder.Configuration.GetConnectionString("SqlServer")));

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
