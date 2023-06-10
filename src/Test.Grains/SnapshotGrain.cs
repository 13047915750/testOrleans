using Grain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace Test.Grains
{
    public class SnapshotGrain : Orleans.Grain<JournaledModel>, ISnapshotGrain
    {
        public SnapshotGrain()
        {
        }

        public async Task Shoot(JournaledModel journaledModel)
        {
            State = journaledModel;
        }

        public async Task<JournaledModel> Get()
        {
            return State;
        }

        public async Task Clear()
        {
            await ClearStateAsync();
        }
    }
}
