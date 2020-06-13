using System;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using MBIILadder.Shared.Models;

namespace MBIILadder.Shared.Services
{
    public interface IFirebase
    {
        Task<Match> CreateMatchAsync(Match match);
        Task<Match> UpdateMatchAsync(Match match);
        Task<Match> GetMatchAsync(Guid id);
        Task DeleteMatchAsync(Guid id);
    }

    public class Firebase : IFirebase
    {
        private readonly IFirebaseClient client;
        public Firebase()
        {
            client = new FireSharp.FirebaseClient(new FirebaseConfig
            {
                AuthSecret = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_AUTH_SECRET"),
                BasePath = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_BASE_PATH")
            });
        }

        public async Task<Match> CreateMatchAsync(Match match)
        {
            return (await client.SetTaskAsync($"Matches/{match.Id}", match)).ResultAs<Match>();
        }

        public async Task<Match> UpdateMatchAsync(Match match)
        {
            return (await client.UpdateTaskAsync($"Matches/{match.Id}", match)).ResultAs<Match>();
        }

        public async Task<Match> GetMatchAsync(Guid id)
        {
            var match = (await client.GetTaskAsync($"Matches/{id}")).ResultAs<Match>();
            match.Date = match.Date.ToUniversalTime();
            return match;
        }

        public async Task DeleteMatchAsync(Guid id)
        {
            await client.DeleteTaskAsync($"Matches/{id}");
        }
    }
}