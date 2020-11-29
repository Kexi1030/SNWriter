using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.CommandAggregationStatusDistribution
{
    public class StatusDistribution
    {
        public Task Task;

        public string FWString { get; set; }

        public void FWStringGet(string stringBack)
        {
            FWString = stringBack;
            Task.Start();
        }
    }
}
