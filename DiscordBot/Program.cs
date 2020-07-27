using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Shared.Contexts;

namespace DiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private LadderDbContext _db;

        private static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            _db = new LadderDbContext();
            var config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(config);

            _client.Log += Log;
            var token = Environment.GetEnvironmentVariable("MBII_LADDER_DISCORD_BOT_API_KEY");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };

            // while (true)
            // {
            //     var matches = _db.Matches.ToList();
            // }

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
            // await after.Author.SendMessageAsync("test");
        }

    }
}
