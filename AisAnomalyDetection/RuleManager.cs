using AisAnomalyDetection.Ais;
using AisAnomalyDetection.Engine;
using AisAnomalyDetection.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection
{
    public class RuleManager
    {
        private readonly List<Rule> rules;
        private readonly RuleEngine ruleEngine;
        private readonly AisDataProcessor aisDataProcessor;
        private readonly ExceptionDetector exceptionDetector;

        //public RuleManager() { }

        public RuleManager(string ruleFileName) {
            // 从配置文件中加载规则
            rules = Utils.LoadRulesFromConfigFile(ruleFileName);

            // 初始化规则引擎
            ruleEngine = new RuleEngine();

            // 添加规则到规则引擎
            foreach (var rule in rules)
            {
                ruleEngine.AddRule(rule);
            }

            // 初始化 AIS 数据处理器
            aisDataProcessor = new AisDataProcessor();

            // 初始化异常检测器并将 AIS 数据处理器传递给它`
            exceptionDetector = new ExceptionDetector(ruleEngine, aisDataProcessor);
        }

        public void ProcessAisData(string rawData)
        {
            var data = aisDataProcessor.ProcessAisData(rawData);
        }
    }
}
