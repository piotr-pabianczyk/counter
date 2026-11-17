using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Models
{
    internal class CounterList
    {
            public List<CounterItem> Counters { get; set; } = new List<CounterItem>();
    }
}
