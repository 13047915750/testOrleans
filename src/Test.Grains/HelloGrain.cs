using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace Test.Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
        }

        Task<string> IHello.SayHello(string greeting)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(500);
            DelayDeactivation(timeSpan);

            logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello!");
        }

        public override Task OnDeactivateAsync() => base.OnDeactivateAsync();

        public override Task OnActivateAsync() => base.OnActivateAsync();

        public async Task<string> NoThing()
        {
            DeactivateOnIdle();

            return string.Empty;
        }
    }
}
