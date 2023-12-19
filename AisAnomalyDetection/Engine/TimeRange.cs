using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class TimeRange
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // 构造函数用于初始化时间范围
        public TimeRange(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        // 判断给定时间是否在时间范围内
        public bool Contains(DateTime time)
        {
            return time >= StartTime && time <= EndTime;
        }
    }
}
