using Grain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Interfaces
{
    public interface ISnapshotGrain : Orleans.IGrainWithGuidKey
    {
        Task Shoot(JournaledModel journaledModel);

        Task<JournaledModel> Get();

        Task Clear();
    }
}
