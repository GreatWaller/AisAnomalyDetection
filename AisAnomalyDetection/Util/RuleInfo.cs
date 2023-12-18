using AisAnomalyDetection.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Util
{
    internal class RuleInfo
    {
        public string Name { get; set; }
        public int Threshold { get; set; }
        public List<Coordinate> AreaCoordinates { get; set; }
    }
}
