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

        // 构造函数用于初始化实例
        public AisData(string vesselName, string vesselId, double latitude, double longitude, double speed)
        {
            VesselName = vesselName;
            VesselId = vesselId;
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
        }
    }
}
