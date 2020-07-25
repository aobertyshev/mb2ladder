using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Contexts;
using Shared.Services;
using Shared.Models;

namespace ServerManager
{
    class Program
    {
        static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
        }
    }
}
