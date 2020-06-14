using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MBIILadder.Shared.Services;

namespace MBILadder.DiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private IFirebase _firebase;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _firebase = new Firebase();

            _client.Log += Log;
            var token = Environment.GetEnvironmentVariable("MBII_LADDER_DISCORD_BOT_API_KEY");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
