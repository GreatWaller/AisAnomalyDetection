using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AisAnomalyDetection.Engine
{
    internal class ShipInfoRule : Rule
    {
        public ShipInfoRule(string name)
        {
            Type = "ShipInfoRule";
            Name = name;
            Condition = data => CheckBasicInfo(data);
            OnViolation = data => Console.WriteLine($"船舶基本信息告警：船舶 {data.VesselId} 基本信息不符合要求");
        }

        private bool CheckBasicInfo(AisData data)
        {
            // 实现判断船舶基本信息的逻辑
            // 返回 true 表示基本信息满足要求，否则返回 false
            return true;  // 临时返回 true，实际中需根据具体逻辑实现
        }
    }
}
