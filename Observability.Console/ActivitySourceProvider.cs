using System.Diagnostics;

namespace Observability.Console; 
public static class ActivitySourceProvider {
    public static ActivitySource Source = new ActivitySource(OpenTelemetryConstant.ActivitySourceName);
}
