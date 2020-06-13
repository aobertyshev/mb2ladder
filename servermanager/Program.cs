using System;
using System.Threading.Tasks;
using MBIILadder.Shared.Services;

namespace MBILadder.ServerManager
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            var firebase = new Firebase();
            await firebase.InsertDataAsync();
        }
    }
}
