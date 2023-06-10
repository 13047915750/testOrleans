using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public class JournaledModel
    {
        public List<JournaledEvent> journaledEvents;

        public JournaledModel()
        {
            journaledEvents = new List<JournaledEvent>();
        }

        public void Apply(JournaledEvent @event)
        {
            if (journaledEvents == null)
            {
                journaledEvents = new List<JournaledEvent>();

                Console.WriteLine("进入了JournaledEvent初始化");
            }
            journaledEvents.Add(@event);
        }

        public void Apply(JournaledEvents data)
        {
            journaledEvents = data.JournaledEventsModel;
        }
    }
}
