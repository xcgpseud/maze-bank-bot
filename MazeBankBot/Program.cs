using System;
using MazeBankBot.App;

namespace MazeBankBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot.Make().Start().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}