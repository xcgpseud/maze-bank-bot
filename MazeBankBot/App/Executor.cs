using System;
using System.Threading.Tasks;

namespace MazeBankBot.App
{
    public class Executor
    {
        public static async Task Execute(Func<Task> fn)
        {
            try
            {
                await fn();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}