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
        public string Type { get; set; }
        public string Name { get; set; }
        public int Threshold { get; set; }
        public List<Coordinate> AreaCoordinates { get; set; }
        public List<RuleInfo> SubRules { get; set; }
        public double DistanceLimit { get; set; }
        public int DistanceThreshold { get; set; }
        public TimeRange RestrictedTime { get; set; } // 新添加的属性，用于 TimeRestrictedRule

        // 新添加的属性，用于 ShipInfoRule
        public bool AISStatus { get; set; }
        public string ShipType { get; set; }
        public string ShipSize { get; set; }
    }
}
