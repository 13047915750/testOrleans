using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace Test.Grains
{
    public class HelloArchiveGrain : Orleans.Grain, IHelloArchive
    {
        private readonly IPersistentState<GreetingArchive> _archive;

        public HelloArchiveGrain([PersistentState("archive", "PubSubStore")] IPersistentState<GreetingArchive> archive)
        {
            this._archive = archive;
        }

        public async Task<string> SayHello(string greeting)
        {
            this._archive.State.Greetings.Add(greeting);

            await this._archive.WriteStateAsync();

            return $"You said: '{greeting}', I say: Hello!";
        }

        public Task<IEnumerable<string>> GetGreetings() => Task.FromResult<IEnumerable<string>>(this._archive.State.Greetings);
    }

    public class GreetingArchive
    {
        public List<string> Greetings { get; } = new List<string>();
    }
}
