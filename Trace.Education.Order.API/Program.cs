
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Trace.Education.Order.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region OpenTelemetry Settings

            builder.Services.Configure<OpenTelemetrySettings>(builder.Configuration.GetSection("OpenTelemetrySettings"));

            var openTelemetryConstants = (builder.Configuration.GetSection("OpenTelemetrySettings").Get<OpenTelemetrySettings>())!;

            builder.Services.AddOpenTelemetry().WithTracing(configure => {
                //ilk olarak source ve servis eklenir
                configure.AddSource(openTelemetryConstants.ActivitySourceName)
                .ConfigureResource(resource => {
                    resource.AddService(serviceName:openTelemetryConstants.ServiceName,serviceVersion:openTelemetryConstants.ServiceVersion);
                });

                configure.AddAspNetCoreInstrumentation(opt => {
                    //benden içerisine context alan ve boolean dönen bir delege bekliyor
                    opt.Filter = (context) => {
                        if (!String.IsNullOrEmpty(context.Request.Path.Value)) {
                            return context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);
                        }
                            return false;
                    };
                });
                configure.AddConsoleExporter();
                configure.AddOtlpExporter();

            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("api/weatherforecast", (HttpContext httpContext) => {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.Run();
        }
    }
}
