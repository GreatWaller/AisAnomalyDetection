using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class RestrictedAreaRule : Rule
    {
        public RestrictedAreaRule(List<Coordinate> areaCoordinates)
        {
            Name = "RestrictedAreaRule";
            Condition = data => IsInRestrictedArea(data, areaCoordinates);
            OnViolation = data => Console.WriteLine($"禁入区域告警：船舶 {data.VesselId} 进入禁入区域");
        }

        private bool IsInRestrictedArea(AisData data, List<Coordinate> areaCoordinates)
        {
            // 实现检测船舶是否在指定区域的逻辑
            // 返回 true 表示在区域内，否则返回 false
            return PointInPolygon(new Coordinate(data.Latitude, data.Longitude), areaCoordinates);
        }

        // 判断点是否在多边形内的算法
        // 参考：https://wrf.ecse.rpi.edu/Research/Short_Notes/pnpoly.html
        private bool PointInPolygon(Coordinate point, List<Coordinate> polygon)
        {
            int n = polygon.Count;
            bool inside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                if (((polygon[i].Latitude <= point.Latitude && point.Latitude < polygon[j].Latitude) ||
                     (polygon[j].Latitude <= point.Latitude && point.Latitude < polygon[i].Latitude)) &&
                    (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) * (point.Latitude - polygon[i].Latitude) / (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
