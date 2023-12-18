using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class Rule
    {
        public string Name { get; set; }
        public Func<AisData, bool> Condition { get; set; }
        public Action<AisData> OnViolation { get; set; }
    }
}
