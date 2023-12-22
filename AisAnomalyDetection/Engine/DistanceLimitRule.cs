using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class DistanceLimitRule : Rule
    {
        private readonly double distanceLimit;

        public DistanceLimitRule(string name, double distanceLimit)
        {
            Type = "DistanceLimitRule";
            Name = name;
            this.distanceLimit = distanceLimit;
            Condition = data => CheckDistanceLimit(data);
            OnViolation = data => Console.WriteLine($"距离限制告警：船舶 {data.VesselId} 行驶距离超过限制");
        }

        private bool CheckDistanceLimit(AisData data)
        {
            // 获取船舶队列
            var vesselQueue = AisDataProcessor.GetVesselQueue(data.VesselId);

            // 计算队列中船舶行驶的总距离
            double totalDistance = CalculateTotalDistance(vesselQueue);

            // 检查是否超过距离限制
            return totalDistance > distanceLimit;
        }

        private double CalculateTotalDistance(Queue<AisData> vesselQueue)
        {
            double totalDistance = 0;

            if (vesselQueue.Count > 1)
            {
                var previousData = vesselQueue.Peek();

                foreach (var currentData in vesselQueue)
                {
                    totalDistance += CalculateDistance(previousData, currentData);
                    previousData = currentData;
                }
            }

            return totalDistance;
        }

        private double CalculateDistanceLine(AisData data1, AisData data2)
        {
            // 使用适当的方法计算两点之间的距离
            // 这里简化为直线距离，您可以根据实际情况选择更精确的方法
            return Math.Sqrt(Math.Pow(data1.Latitude - data2.Latitude, 2) + Math.Pow(data1.Longitude - data2.Longitude, 2));
        }
        /// <summary>
        /// 这里使用了 Haversine 公式，CalculateDistance 方法现在返回两个坐标之间的实际距离，单位为千米。
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        private static double CalculateDistance(AisData data1, AisData data2)
        {
            const double earthRadius = 6371; // 地球半径，单位千米

            // 将经纬度转换为弧度
            double lat1 = ToRadians(data1.Latitude);
            double lon1 = ToRadians(data1.Longitude);
            double lat2 = ToRadians(data2.Latitude);
            double lon2 = ToRadians(data2.Longitude);

            // Haversine 公式计算距离
            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;
            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return distance;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
