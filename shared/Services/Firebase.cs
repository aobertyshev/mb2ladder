using System;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using MBIILadder.Shared.Models;

namespace MBIILadder.Shared.Services
{
    public interface IFirebase
    {
        Task<bool> CreateMatchAsync(Match match);
        Task<Match> GetMatchAsync(Guid id);
    }

    public class Firebase : IFirebase
    {
        private readonly IFirebaseClient client;
        public Firebase()
        {
            client = new FireSharp.FirebaseClient(new FirebaseConfig
            {
                AuthSecret = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_BASE_PATH"),
                BasePath = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_AUTH_SECRET")
            });
        }

        public async Task<bool> CreateMatchAsync(Match match)
        {
            return (await client.SetTaskAsync($"Matches/{match.Id}", match)).ResultAs<Match>() == match;
        }

        public async Task<Match> GetMatchAsync(Guid id)
        {
            var match = (await client.GetTaskAsync($"Matches/{id}")).ResultAs<Match>();
            match.Date = match.Date.ToUniversalTime();
            return match;
        }
    }
}