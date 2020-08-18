using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Shared.Contexts;
using Shared.Models;
using Shared;

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
            var config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(config);

            _client.Log += Log;
            var token = Environment.GetEnvironmentVariable("MBII_LADDER_DISCORD_BOT_API_KEY");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            var timer = new Timer(10000);
            timer.Elapsed += CheckForPasswordedMatchesAndSendNotificationsAsync;
            timer.Start();
            
            _client.MessageUpdated += MessageUpdated;
            _client.ReactionAdded += ReactionAdded;
            _client.ReactionRemoved += ReactionRemoved;
            _client.MessageReceived += MessageReceived;
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };
            
            await Task.Delay(-1);
        }

        public async void CheckForPasswordedMatchesAndSendNotificationsAsync(object state, ElapsedEventArgs args)
        {
            _db = new LadderDbContext();
            var match = _db.Matches.FirstOrDefault(match => match.MatchState == MatchState.Passworded);
            if (match == null) return;
            //very bad here, need to refactor
            foreach (var player in match.Players)
            {
                var playerfromDb = _db.Users.SingleOrDefault(user => user.Nick == player);
                await _client.GetUser(playerfromDb.DiscordId).SendMessageAsync(
                    "Match is starting." +
                    Environment.NewLine +
                    $"Team 1: {string.Join(", ",match.Players.Take(Constants.REQUIRED_AMOUNT_OF_PLAYERS / 2))}" +
                    Environment.NewLine +
                    $"Team 2: {string.Join(", ",match.Players.TakeLast(Constants.REQUIRED_AMOUNT_OF_PLAYERS / 2))}" +
                    Environment.NewLine +
                    "Open the game and paste this into the console:" +
                    Environment.NewLine +
                    $"`name {player}; password {match.Password}; connect {match.ServerIp};`");
            }
            match.MatchState = MatchState.SentNotifications;
            _db.Update(match);
            await _db.SaveChangesAsync();
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
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, 
            SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
            if (reaction.Emote.Name != Constants.PLUS_REACTION_EMOTE)
            {
                await message.Value.RemoveReactionAsync(reaction.Emote, reaction.User.Value, RequestOptions.Default);
                return;
            }
            await message.Value.RemoveReactionAsync(new Emoji(Constants.PLUS_REACTION_EMOTE), _client.CurrentUser);
            
            _db = new LadderDbContext();
            if (!_db.Users.Any(user => user.Nick == reaction.User.Value.Username))
            {
                await _db.Users.AddAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Nick = reaction.User.Value.Username,
                    DiscordId = reaction.User.Value.Id
                });
            }

            var matchId = Guid.Parse(message.Value.Content.Split(Environment.NewLine)[0].Split("||")[1]);
            var match = _db.Matches.SingleOrDefault(match => match.Id == matchId);
            match.Players = match.Players.Append(reaction.User.Value.Username).ToArray();
            if (match.Players.Length == Constants.REQUIRED_AMOUNT_OF_PLAYERS)
            {
                match.MatchState = MatchState.GotEnoughPlayers;
            }

            await _db.SaveChangesAsync();
        }

        private async Task ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, 
            SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
            if (!message.Value.Reactions.Keys.Any())
            {
                await message.Value.AddReactionAsync(new Emoji(Constants.PLUS_REACTION_EMOTE));
            }
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id != Constants.AUTHORIZED_USER_ID) return;
            _db = new LadderDbContext();
            var matchId = Guid.NewGuid();
            await _db.Matches.AddAsync(
                new Match
                {
                    Id = matchId,
                    Players = new string[0],
                    MatchState = MatchState.Created
                });
            await _db.SaveChangesAsync();
            
            var channel = _client.GetChannel(Constants.AUTHORIZED_CHANNEL) as IMessageChannel;
            var announcement = await channel.SendMessageAsync(
                $"||{matchId}||" +
                Environment.NewLine +
                "@everyone" +
                Environment.NewLine +
                "New match!");
            await announcement.AddReactionAsync(new Emoji(Constants.PLUS_REACTION_EMOTE));
        }
    }
}
