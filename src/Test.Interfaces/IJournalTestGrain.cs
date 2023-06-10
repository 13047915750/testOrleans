using Grain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Interfaces
{
    public interface IJournalTestGrain : Orleans.IGrainWithGuidKey
    {
        Task<bool> AddEvents(JournaledEvent journaledEvent);

        Task<JournaledModel> Get();
    }
}
