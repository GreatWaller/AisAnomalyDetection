using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class SpeedLimitRule:Rule
    {
        public SpeedLimitRule(string name, int threshold)
        {
            Type = "SpeedLimitRule";
            Name = name;
            Condition = data => data.Speed > threshold;
            OnViolation = data => Console.WriteLine($"超速告警：船舶 {data.VesselId} 超过速度限制");
        }
    }
}
