using Observability.Console;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

Console.WriteLine("Hello World");

using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(OpenTelemetryConstant.ActivitySourceName) 
    .ConfigureResource(x => {
        x.AddService(OpenTelemetryConstant.ServiceName, serviceVersion:OpenTelemetryConstant.ServiceVersion);
        x.AddAttributes(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
                    new KeyValuePair<string, object>("host.os", Environment.OSVersion.VersionString),
                    new KeyValuePair<string, object>("dotnet.version", Environment.Version.ToString()),
                     new KeyValuePair<string, object>("host.environment", "dev"),
                });
    }).AddConsoleExporter().AddOtlpExporter().Build();

ServiceHelper sh = new ServiceHelper();
await sh.DoWork();