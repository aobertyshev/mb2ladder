using System;

namespace MBILadder.DiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("MBII_LADDER_DISCORD_BOT_API_KEY"));
        }
    }
}
