using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Shared.Contexts;
using Shared.Models;

namespace ServerManager
{
    class Program
    {
        private LadderDbContext _db;
        private string _ip;
        static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            var timer = new Timer(10000);
            timer.Elapsed += CheckForMatchesWithEnoughPlayersAndPasswordServerAsync;
            timer.Start();
            
            
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
            {
                client.BaseAddress = new Uri("https://icanhazip.com/");
                var response = await client.GetAsync(string.Empty);
                response.EnsureSuccessStatusCode();
                _ip = await response.Content.ReadAsStringAsync();
                _ip = _ip.Remove(_ip.Length - 1);
            }

            await Task.Delay(-1);
        }

        async Task<string> SendCommandAsync(string command, bool waitForResponse = false)
        {
            var bufferPrefix = new[] {(byte) 0xFF, (byte) 0xFF, (byte) 0xFF, (byte) 0xFF,};
            var bufferSuffix = Encoding.ASCII.GetBytes(command);
            var buffer = bufferPrefix.Concat(bufferSuffix).ToArray();
            using var ws = new UdpClient();
            ws.Connect("127.0.0.1", 29070);
            await ws.SendAsync(buffer, buffer.Length);
            return waitForResponse ? Encoding.ASCII.GetString((await ws.ReceiveAsync()).Buffer) : string.Empty;
        }
        
        async void CheckForMatchesWithEnoughPlayersAndPasswordServerAsync(object state, ElapsedEventArgs args)
        {
            _db = new LadderDbContext();
            var match = _db.Matches.FirstOrDefault(match => match.MatchState == MatchState.GotEnoughPlayers);
            if (match == null) return;
            var pw = "pug";
            await SendCommandAsync($"rcon myrcon g_password {pw}");
            match.MatchState = MatchState.Passworded;
            match.ServerIp = $"{_ip}:29070";
            match.Password = pw;
            _db.Update(match);
            await _db.SaveChangesAsync();
        }
    }
}
