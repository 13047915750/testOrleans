using Grain.Models;
using Orleans.EventSourcing;
using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.EventSourcing.LogStorage;
using Test.Interfaces;
using Orleans.Storage;
using Newtonsoft.Json;

namespace Test.Grains
{
    [StorageProvider]
    [LogConsistencyProvider]
    public class JournalTestGrain : JournaledGrain<JournaledModel, BaseEvent>, IJournalTestGrain
    {
        private bool flag = false;

        public override async Task OnActivateAsync()
        {

            Console.WriteLine("进入系统激活");
            await base.OnActivateAsync();

            var snapshotGrain = GrainFactory.GetGrain<ISnapshotGrain>(this.GetPrimaryKey());
            var snapshot = await snapshotGrain.Get();
            if (snapshot != null && snapshot.journaledEvents!=null && snapshot.journaledEvents.Count != 0)
            {
                RaiseEvent(new JournaledEvents { JournaledEventsModel = snapshot.journaledEvents });

                await snapshotGrain.Clear();

                Console.WriteLine("激活成功");
            }
            else
            {
                Console.WriteLine("无快照不需要激活");
            }
        }
        public async Task<bool> AddEvents(JournaledEvent journaledEvent)
        {
           

            if (!flag)
            {
                Console.WriteLine("进入处理阶段");
            }
            flag = true;
            RaiseEvent(journaledEvent);

            if (Version > 20)
            {
                Console.WriteLine("触发快照");
                var snapshotGrain = GrainFactory.GetGrain<ISnapshotGrain>(this.GetPrimaryKey());
                await snapshotGrain.Shoot(TentativeState);

                await ClearLogs();

                Console.WriteLine("快照结束");
                flag = false;

                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<JournaledModel> Get()
        {
            return TentativeState;
        }

        public async Task ClearLogs()
        {
            DeactivateOnIdle();

            //var grainStorage = this.GetGrainStorage(ServiceProvider);

            //await grainStorage.ClearStateAsync(
            //    GetType().FullName,
            //    GrainReference,
            //    grainState: new LogStateWithMetaDataAndETag<BaseEvent>());\
           await  this.Clear(true);

            Console.WriteLine($"{JsonConvert.SerializeObject(Version)}");
        }
    }
}
