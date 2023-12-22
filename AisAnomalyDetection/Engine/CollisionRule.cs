using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class CollisionRule : Rule
    {
        private double distanceThreshold; // 碰撞距离阈值，可以根据实际需求调整
        public CollisionRule(string name, double distanceThreshold)
        {
            Type = "CollisionRule";
            Name = name;
            this.distanceThreshold = distanceThreshold;
            Condition = data => CheckCollision(data);
            OnViolation = data => Console.WriteLine($"碰撞告警：船舶 {data.VesselId} 与其他船只发生碰撞");
        }

        private bool CheckCollision(AisData data)
        {
            // 获取其他船只的信息
            var otherAisDataList = AisDataProcessor.GetOtherAisData(data);

            foreach (var otherAisData in otherAisDataList)
            {
                // 计算船只之间的距离
                double distance = CalculateDistance(data, otherAisData);

                // 如果距离小于阈值，触发碰撞告警
                if (distance < distanceThreshold)
                {
                    return true;
                }
            }

            return false;
        }

        private static double CalculateDistance(AisData data1, AisData data2)
        {
            // 获取船只1的当前位置
            double x1 = data1.Longitude;
            double y1 = data1.Latitude;

            // 获取船只2的当前位置
            double x2 = data2.Longitude;
            double y2 = data2.Latitude;

            // 计算船只1和船只2的距离
            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            // 计算船只1和船只2的速度向量差异
            double speedVectorDiffX = data2.SpeedVectorX - data1.SpeedVectorX;
            double speedVectorDiffY = data2.SpeedVectorY - data1.SpeedVectorY;

            // 计算速度向量差异的模
            double speedVectorDiffMagnitude = Math.Sqrt(Math.Pow(speedVectorDiffX, 2) + Math.Pow(speedVectorDiffY, 2));

            // 计算碰撞点在时间 t = 0 时的位置（假设两船当前位置为 t = 0）
            double collisionPointX = x2 - x1 - (speedVectorDiffX / speedVectorDiffMagnitude) * distance;
            double collisionPointY = y2 - y1 - (speedVectorDiffY / speedVectorDiffMagnitude) * distance;

            // 计算碰撞点到船只1速度向量的投影长度
            double projectionLength = (collisionPointX * data1.SpeedVectorX + collisionPointY * data1.SpeedVectorY) / speedVectorDiffMagnitude;

            // 如果投影长度小于0，说明碰撞点在船只1的反方向上，此时不考虑碰撞
            if (projectionLength < 0)
            {
                return double.MaxValue;
            }

            // 返回碰撞点到船只1当前位置的距离
            return Math.Sqrt(Math.Pow(collisionPointX, 2) + Math.Pow(collisionPointY, 2));
        }

    }
}
