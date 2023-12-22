using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Ais
{
    internal class AisData
    {
        public string VesselName { get; set; }
        public string VesselId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; }
        // 其他船舶相关属性可以根据需求添加
        public double Course { get; set; } // 航向，以弧度表示
        public double SpeedVectorX { get; set; } // 速度向量在水平方向上的分量
        public double SpeedVectorY { get; set; } // 速度向量在垂直方向上的分量
        public DateTime Timestamp { get; set; }

        // 构造函数用于初始化实例
        public AisData(string vesselName, string vesselId, double latitude, double longitude, double speed, double course, DateTime timestamp)
        {
            VesselName = vesselName;
            VesselId = vesselId;
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            Course = course;
            Timestamp = timestamp;

            // 计算速度向量分量
            SpeedVectorX = speed * Math.Cos(course);
            SpeedVectorY = speed * Math.Sin(course);
        }
    }
}
