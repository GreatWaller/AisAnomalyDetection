using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Ais
{
    internal class AisDataProcessor
    {
        private static Dictionary<string, Queue<AisData>> vesselQueues = new Dictionary<string, Queue<AisData>>();
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
            double course = double.Parse(dataFields[5]);

            // 新增的时间字段
            DateTime timestamp = DateTime.Parse(dataFields[6]);

            // 创建 AisData 实例，并传递时间戳
            var aisData = new AisData(vesselName, vesselId, latitude, longitude, speed, course, timestamp);

            // 维护船舶队列
            AddAisDataToQueue(aisData);

            // 触发 AIS 数据处理完成事件
            OnAisDataProcessed(new AisDataEventArgs(aisData));

            // 返回 AisData 实例
            return aisData;
        }

        private void AddAisDataToQueue(AisData aisData)
        {
            // 如果队列中已有该船舶的数据，则将新数据加入队列
            if (vesselQueues.ContainsKey(aisData.VesselId))
            {
                vesselQueues[aisData.VesselId].Enqueue(aisData);
            }
            else
            {
                // 如果队列中没有该船舶的数据，则创建队列并加入新数据
                var newQueue = new Queue<AisData>();
                newQueue.Enqueue(aisData);
                vesselQueues.Add(aisData.VesselId, newQueue);
            }

            // 维护队列的长度，确保不超过设定的最大长度
            const int maxQueueLength = 100; // 您可以根据需求调整最大队列长度
            if (vesselQueues[aisData.VesselId].Count > maxQueueLength)
            {
                vesselQueues[aisData.VesselId].Dequeue();
            }
        }

        public static Queue<AisData> GetVesselQueue(string vesselId)
        {
            if (vesselQueues.ContainsKey(vesselId))
            {
                return vesselQueues[vesselId];
            }
            else
            {
                // 如果队列不存在，根据需要进行处理，如创建新队列
                var newQueue = new Queue<AisData>();
                vesselQueues.Add(vesselId, newQueue);
                return newQueue;
            }
        }

        // 获取除当前船只以外的所有其他船只信息
        public static IEnumerable<AisData> GetOtherAisData(AisData currentAisData)
        {
            //foreach (var queue in vesselQueues.Values)
            //{
            //    foreach (var aisData in queue)
            //    {
            //        // 排除当前船只
            //        if (aisData.VesselId != currentAisData.VesselId)
            //        {
            //            yield return aisData;
            //        }
            //    }
            //}
            foreach (var (vesselId, queue) in vesselQueues)
            {
                if (vesselId != currentAisData.VesselId && queue.Count > 0)
                {
                    yield return queue.Peek();
                }
            }
        }

        // 用于触发 AIS 数据处理完成事件的方法
        protected virtual void OnAisDataProcessed(AisDataEventArgs e)
        {
            AisDataProcessed?.Invoke(this, e);
        }
    }
}
