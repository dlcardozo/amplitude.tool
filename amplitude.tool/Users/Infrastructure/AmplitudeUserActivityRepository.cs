using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Infrastructure
{
    public class AmplitudeUserActivityRepository : UserActivityRepository
    {
        HttpClient client;

        public AmplitudeUserActivityRepository(string amplitudeKey, string amplitudeSecretKey)
        {
            client = new HttpClient();
            SetupClientHeaders(amplitudeKey, amplitudeSecretKey);
        }

        public IObservable<UserActivity> Fetch(UserId userId) =>
            client.GetStringAsync($"https://amplitude.com/api/2/useractivity?user={userId.Value}")
                .ToObservable()
                .Do(Console.Write)
                .Select(result => new UserActivity());

        void SetupClientHeaders(string apiKey, string amplitudeSecretKey)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Amplitude Tool");
            
            var amplitudeAuth = Encoding.ASCII.GetBytes($"{apiKey}:{amplitudeSecretKey}");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(amplitudeAuth));
        }
    }
}