using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Ais
{
    internal class AisDataProcessor
    {
        // 定义 AIS 数据处理完成事件
        public event EventHandler<AisDataEventArgs> AisDataProcessed;

        public AisData ProcessAisData(string rawData)
        {
            // 假设 AIS 数据的格式为逗号分隔的字段
            string[] dataFields = rawData.Split(',');

            // 根据实际情况解析数据并创建 AisData 实例
            string vesselName = dataFields[0];
            string vesselId = dataFields[1];
            double latitude = double.Parse(dataFields[2]);
            double longitude = double.Parse(dataFields[3]);
            double speed = double.Parse(dataFields[4]);

            // 创建 AisData 实例
            var aisData = new AisData(vesselName, vesselId, latitude, longitude, speed);

            // 触发 AIS 数据处理完成事件
            OnAisDataProcessed(new AisDataEventArgs(aisData));

            // 返回 AisData 实例
            return aisData;
        }

        // 用于触发 AIS 数据处理完成事件的方法
        protected virtual void OnAisDataProcessed(AisDataEventArgs e)
        {
            AisDataProcessed?.Invoke(this, e);
        }
    }
}
