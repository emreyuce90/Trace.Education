using System.ComponentModel.DataAnnotations;

namespace Trace.Education.Order.API.OpenTelemetry;
public class OpenTelemetrySettings
{
    [Required] public string ServiceName { get; set; } = default!;
    [Required] public string ServiceVersion { get; set; } = default!;
    [Required] public string ActivitySourceName { get; set; } = default!;

}


