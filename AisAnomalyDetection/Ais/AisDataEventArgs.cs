using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Ais
{
    // 定义包含 AisData 的事件参数类 
    internal class AisDataEventArgs : EventArgs
    {
        public AisData AisData { get; }

        public AisDataEventArgs(AisData aisData)
        {
            AisData = aisData;
        }
    }
}
