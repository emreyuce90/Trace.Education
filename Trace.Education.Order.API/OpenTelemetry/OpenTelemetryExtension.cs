using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Trace.Education.Order.API.OpenTelemetry {
    public static class OpenTelemetryExtension {

        public static IServiceCollection AddOpenTelemetryExt(this IServiceCollection services,IConfiguration configuration) {

            services.AddOptions<OpenTelemetrySettings>().BindConfiguration(nameof(OpenTelemetrySettings)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<OpenTelemetrySettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<OpenTelemetrySettings>>().Value);

            //bu sınıfı şimdi okuyalım

            using var serviceProvider = services.BuildServiceProvider();
                var openTelemetrySettings = serviceProvider.GetRequiredService<OpenTelemetrySettings>();
            services.AddOpenTelemetry().WithTracing(configure => {
                configure.AddSource(openTelemetrySettings.ActivitySourceName)
                .ConfigureResource(resource => {
                    resource.AddService(serviceName:openTelemetrySettings.ServiceName,serviceVersion:openTelemetrySettings.ServiceVersion);
                });
                configure.AddAspNetCoreInstrumentation(opt => {
                    opt.RecordException = true;
                    opt.Filter = (context) => {
                        if (!String.IsNullOrEmpty(context.Request.Path.Value)) {
                            return context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);
                        }
                        return false;
                    };
                });
                configure.AddOtlpExporter();
                configure.AddEntityFrameworkCoreInstrumentation(efCoreOptions => {
                    efCoreOptions.SetDbStatementForText = true;
                    efCoreOptions.SetDbStatementForStoredProcedure = true;
                    efCoreOptions.EnrichWithIDbCommand = (action, dbCommand) => {
                        //
                    };
                });
            });

            ActivitySourceProvider.Source = new System.Diagnostics.ActivitySource(openTelemetrySettings.ActivitySourceName);
            return services;
        }

    }
}
