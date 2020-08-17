using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Shared.Contexts;
using Shared.Models;

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
            _client.ReactionAdded += ReactionAdded;
            _client.ReactionRemoved += ReactionRemoved;
            _client.MessageReceived += MessageReceived;
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

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, 
            ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
            // await after.Author.SendMessageAsync("test");
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, 
            SocketReaction reaction)
        {
            var now = DateTime.UtcNow;
            if (reaction.User.Value.IsBot) return;
            if (reaction.Emote.Name != "➕")
            {
                await message.Value.RemoveReactionAsync(reaction.Emote, reaction.User.Value, RequestOptions.Default);
                return;
            }
            await message.Value.RemoveReactionAsync(new Emoji("➕"), _client.CurrentUser);
            if (!_db.Users.Any(user => user.Nick == reaction.User.Value.Username))
            {
                await _db.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Nick = reaction.User.Value.Username,
                    RegisterDate = now
                });
            }

            var matchId = Guid.Parse(message.Value.Content.Split(Environment.NewLine)[0].Split("||")[1]);
            var match = _db.Matches.SingleOrDefault(match => match.Id == matchId);
            if (match == null)
            {
                match = new Match
                {
                    Date = now,
                    Id = matchId,
                    Maps = new[] {"mb2_dotf", "mb2_lunarbase"},
                    DateCreated = now,
                    DateUpdated = now,
                    Score = "0:0",
                    Players = new []{reaction.User.Value.Username}
                };
                await _db.Matches.AddAsync(match);
            }
            else
            {
                match.Players.Append(reaction.User.Value.Username);
            }

            await _db.SaveChangesAsync();
            // await reaction.User.Value.SendMessageAsync("Ok, you're accepted for the match");
        }

        private async Task ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, 
            SocketReaction reaction)
        {
            if (!message.Value.Reactions.Keys.Any())
            {
                await message.Value.AddReactionAsync(new Emoji("➕"));
            }
            Console.WriteLine(reaction.Emote.Name);
            // await reaction.User.Value.SendMessageAsync("Ok, you're removed from the match");
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Username != "Helix") return;
            var matchId = Guid.NewGuid();
            var date = DateTime.UtcNow.AddDays(3);
            var channel = _client.GetChannel(720655447870406670) as IMessageChannel;
            var announcement = await channel.SendMessageAsync(
                $"||{matchId}||{Environment.NewLine}"+
                "@everyone" +
                $"{Environment.NewLine}" +
                "New match!" +
                $"{Environment.NewLine}" +
                "Maps: `mb2_dotf`, `mb2_lunarbase`" +
                $"{Environment.NewLine}" +
                $"Date: `{date}`");
            await announcement.AddReactionAsync(new Emoji("➕"));
        }
    }
}
