using System.Diagnostics;
using Trace.Education.Order.API.OpenTelemetry;

namespace Trace.Education.Order.API.Feature.Orders {
    public class OrderService : IOrderService {

        public Task CreateAsync(OrderCreateDto orderCreateDto) {
            Activity.Current?.SetTag("ASP.NET CORE instrumentation tag1", "tag1 value");
            //Bu benim child activitym
            //Custom activitemizi using ile kullanıyoruz
            using var activity = ActivitySourceProvider.Source.StartActivity();
            //Bu trace e event ekliyoruz zaman damgası ile beraber
            activity?.AddEvent(new ActivityEvent(name: "Sipariş süreci başladı"));
            //veritabanı kaydı yapıldı
            Thread.Sleep(1000);
            //event
            activity?.AddEvent(new ActivityEvent(name: "Sipariş süreci bitti"));
            //Jeger deki logs kısmına ilgili datayı set ettik
            activity?.SetTag("order user id", orderCreateDto.UserId);

            return Task.CompletedTask;
        }
    }
}
