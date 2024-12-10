using System;
namespace Observability.Console {
    internal class ServiceHelper {
        internal async Task DoWork() {

            using var activity = ActivitySourceProvider.Source.StartActivity();
            System.Console.WriteLine("Do work çalıştı");
            var serviceOne = new ServiceOne();

            System.Console.WriteLine("google response length"+ await serviceOne.MakeRequestToGoogle());

        }
    }
}
