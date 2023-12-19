using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class MinSpeedRule : Rule
    {
        public MinSpeedRule(string name, double minSpeed)
        {
            Type = "MinSpeedRule";
            Name = name;
            Condition = data => CheckMinSpeed(data, minSpeed);
            OnViolation = data => Console.WriteLine($"最小速度限制告警：船舶 {data.VesselId} 速度低于最小限制");
        }

        private bool CheckMinSpeed(AisData data, double minSpeed)
        {
            return data.Speed >= minSpeed;
        }
    }
}
