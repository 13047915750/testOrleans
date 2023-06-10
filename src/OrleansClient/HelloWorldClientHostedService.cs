using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace OrleansClient
{
    public class HelloWorldClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public HelloWorldClientHostedService(IClusterClient client)
        {
            this._client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling grains from the initialized client
            //var friend = this._client.GetGrain<IHello>(0);
            //var response = await friend.SayHello("Good morning, my friend!");
            //Console.WriteLine($"{response}");

            //// example of calling IHelloArchive grqain that implements persistence
            //var g = this._client.GetGrain<IHelloArchive>(0);
            //response = await g.SayHello("Good day!");
            //Console.WriteLine($"{response}");

            //response = await g.SayHello("Good evening!");
            //Console.WriteLine($"{response}");

            //var greetings = await g.GetGreetings();
            //Console.WriteLine($"\nArchived greetings: {Utils.EnumerableToString(greetings)}");

            //var key = Guid.NewGuid();

            //Console.WriteLine(key.ToString());
            var key = Guid.Parse("b7f409a1-9893-4081-a99d-4a3e5f154b7a");
            var i = 0;
            while (true)
            {
                i++;
                var grain = this._client.GetGrain<IJournalTestGrain>(key);
                var flag = await grain.AddEvents(new Grain.Models.JournaledEvent());
                if (flag)
                {
                    Console.WriteLine($"{i}");
                    Console.WriteLine("开始等待");
                    await Task.Delay(5 *1000);
                    Console.WriteLine("等待5秒后继续请求");

                    await grain.Get();

                    Console.WriteLine("获取成功");
                }
            } 
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
