using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            CreateTime = DateTimeOffset.Now;
        }

        public DateTimeOffset CreateTime { get; set; }
    }
}
