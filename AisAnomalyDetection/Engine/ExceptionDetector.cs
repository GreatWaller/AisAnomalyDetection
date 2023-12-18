using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class ExceptionDetector
    {
        private RuleEngine ruleEngine;

        public ExceptionDetector(RuleEngine ruleEngine, AisDataProcessor aisDataProcessor)
        {
            this.ruleEngine = ruleEngine;

            // 订阅 AIS 数据处理完成事件
            aisDataProcessor.AisDataProcessed += OnAisDataProcessed;
        }

        // 处理 AIS 数据处理完成事件的方法
        private void OnAisDataProcessed(object sender, AisDataEventArgs e)
        {
            // 在这里进行规则检测
            ruleEngine.CheckRules(e.AisData);
        }
    }
}
