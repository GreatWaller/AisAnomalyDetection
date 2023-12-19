using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class TimeRestrictedRule : Rule
    {
        // 添加用于存储禁入时间段的属性
        public TimeRange RestrictedTime { get; set; }

        public TimeRestrictedRule(string name, TimeRange restrictedTime)
        {
            Type = "TimeRestrictedRule";
            Name = name;
            RestrictedTime = restrictedTime;
            Condition = data => IsInRestrictedTime(data);
            OnViolation = data => Console.WriteLine($"禁入时间告警：船舶 {data.VesselId} 在禁入时间段内");
        }

        private bool IsInRestrictedTime(AisData data)
        {
            // 获取 AIS 数据中的时间
            DateTime aisDataTime = data.Timestamp;

            // 判断 AIS 数据中的时间是否在禁入时间段内
            return RestrictedTime.Contains(aisDataTime);
        }
    }
}
