using System.Diagnostics;

namespace Observability.Console {
    internal class ServiceOne {

        static HttpClient httpClient = new HttpClient();
        public async Task<int> MakeRequestToGoogle() {
            using var activity = ActivitySourceProvider.Source.StartActivity();


            var activityTagsCollection = new ActivityTagsCollection();
            activityTagsCollection.Add(key:"userId",value:646464);

            activity?.AddEvent(new(name:"Google a istek başladı",tags: activityTagsCollection));
            var result = await httpClient.GetAsync("https://www.google.com");
            var responseContent = await result.Content.ReadAsStringAsync();

            activityTagsCollection.Add(key: "google body length", value: responseContent.Length);
            activity?.AddEvent(new(name: "Google a istek tamamlandı", tags: activityTagsCollection));
            activity.AddTag("Request Type", "HTTPGET");

            return responseContent.Length;
        }
    }
}
