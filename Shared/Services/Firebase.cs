using System;
using System.Threading.Tasks;
// using FireSharp.Config;
// using FireSharp.Interfaces;
using Shared.Models;
using System.Collections.Generic;

namespace Shared.Services
{
    public interface IFirebase
    {
        // Task<Match> CreateMatchAsync(Match match);
        // Task<Match> UpdateMatchAsync(Match match);
        // Task<Match> GetMatchAsync(Guid id);
        // Task DeleteMatchAsync(Guid id);
        // Task<User> CreateUserAsync(User user);
        // Task<Player> CreatePlayerAsync(Player player);
        // Task<IDictionary<string, User>> GetUsersAsync();
        // Task<Player> GetPlayerAsync(Guid id);
        // Task<IDictionary<string, Player>> GetPlayersAsync();
        // Task<Queue> CreateQueueAsync(Queue queue);
        // Task JoinQueueAsync(Guid queueId, Guid playerId);
        // Task<Queue> GetQueueAsync(Guid id);
    }

    public class Firebase : IFirebase
    {
        // private readonly IFirebaseClient client;
        // public Firebase()
        // {
        //     client = new FireSharp.FirebaseClient(new FirebaseConfig
        //     {
        //         AuthSecret = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_AUTH_SECRET"),
        //         BasePath = Environment.GetEnvironmentVariable("MBII_LADDER_FIREBASE_BASE_PATH")
        //     });
        // }
        //
        // public async Task<User> CreateUserAsync(User user)
        // {
        //     return (await client.SetTaskAsync($"Users/{user.Id}", user)).ResultAs<User>();
        // }
        // public async Task<IDictionary<string, User>> GetUsersAsync()
        // {
        //     return (await client.GetTaskAsync("Users/")).ResultAs<Dictionary<string, User>>();
        // }
        //
        //
        //
        // public async Task<Player> CreatePlayerAsync(Player player)
        // {
        //     return (await client.SetTaskAsync($"Players/{player.Id}", player)).ResultAs<Player>();
        // }
        // public async Task<IDictionary<string, Player>> GetPlayersAsync()
        // {
        //     return (await client.GetTaskAsync("Players/")).ResultAs<Dictionary<string, Player>>();
        // }
        // public async Task<Player> GetPlayerAsync(Guid id)
        // {
        //     return (await client.GetTaskAsync($"Players/{id}")).ResultAs<Player>();
        // }
        //
        //
        //
        // public async Task<Match> CreateMatchAsync(Match match)
        // {
        //     return (await client.SetTaskAsync($"Matches/{match.Id}", match)).ResultAs<Match>();
        // }
        // public async Task<Match> GetMatchAsync(Guid id)
        // {
        //     var match = (await client.GetTaskAsync($"Matches/{id}")).ResultAs<Match>();
        //     match.Date = match.Date.ToUniversalTime();
        //     return match;
        // }
        // public async Task<Match> UpdateMatchAsync(Match match)
        // {
        //     return (await client.UpdateTaskAsync($"Matches/{match.Id}", match)).ResultAs<Match>();
        // }
        // public async Task DeleteMatchAsync(Guid id)
        // {
        //     await client.DeleteTaskAsync($"Matches/{id}");
        // }
        //
        //
        //
        // public async Task<MBIILadder.Shared.Models.Queue> CreateQueueAsync(MBIILadder.Shared.Models.Queue queue)
        // {
        //     return (await client.SetTaskAsync($"Queues/{queue.Id}", queue)).ResultAs<MBIILadder.Shared.Models.Queue>();
        // }
        // public async Task JoinQueueAsync(Guid queueId, Guid playerId)
        // {
        //     await client.SetTaskAsync($"Queues/{queueId}/PlayerIds/{playerId}", true);
        // }
        // public async Task<MBIILadder.Shared.Models.Queue> GetQueueAsync(Guid id)
        // {
        //     return (await client.GetTaskAsync($"Queues/{id}")).ResultAs<MBIILadder.Shared.Models.Queue>();
        // }
    }
}