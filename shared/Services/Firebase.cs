using System;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using MBIILadder.Shared.Models;

namespace MBIILadder.Shared.Services
{
    public interface IFirebase
    {
        Task InsertDataAsync();
    }

    public class Firebase : IFirebase
    {
        private readonly IFirebaseClient client = new FireSharp.FirebaseClient(new FirebaseConfig
        {
            AuthSecret = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_BASE_PATH"),
            BasePath = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_AUTH_SECRET")
        });
        public Firebase()
        {
        }

        public async Task InsertDataAsync()
        {
            var match = new Match
            {
                Date = DateTime.UtcNow,
                Id = Guid.NewGuid()
            };
            var response = await client.SetTaskAsync($"Matches/{match.Id}", match);
            var result = response.ResultAs<Match>();
            Console.WriteLine("Data inserted");
        }
    }
}