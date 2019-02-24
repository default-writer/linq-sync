using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace linq_sync
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var waitHandle = new AutoResetEvent(true);
            var result = Enumerable
                .Range(0, 3)
                .Select(async (int param) => {
                    waitHandle.WaitOne();
                    await TestMethod(param);
                    waitHandle.Set();
                }).ToArray();
            Console.ReadKey();
        }

        private static async Task<int> TestMethod(int param)
        {
            Console.WriteLine($"{param} before");
            await Task.Delay(50);
            Console.WriteLine($"{param} after");
            return param;
        }
    }

    public static class StopwatchExtensions
    {
        public static void Watch(this Stopwatch stopwatch, string message = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) =>
        Console.WriteLine(
            $"{stopwatch.Elapsed} " +
            $"{message} " +
            $"{memberName} " +
            $"{sourceFilePath}:{sourceLineNumber}");
    }
}
