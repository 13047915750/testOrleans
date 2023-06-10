using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public class JournaledEvents : BaseEvent
    {
        public List<JournaledEvent> JournaledEventsModel { get; set; }
    }
}
