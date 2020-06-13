using System;
using System.Threading.Tasks;
using MBIILadder.Shared.Services;
using MBIILadder.Shared.Models;

namespace MBILadder.ServerManager
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            var firebase = new Firebase();
            var match = new Match
            {
                Date = DateTime.UtcNow,
                Id = Guid.NewGuid()
            };
            await firebase.CreateMatchAsync(match);
            var match2 = await firebase.GetMatchAsync(match.Id);
            Console.WriteLine(match.Date);
            Console.WriteLine(match2.Date.ToUniversalTime());
            Console.WriteLine(match.Date == match2.Date);
        }
    }
}
