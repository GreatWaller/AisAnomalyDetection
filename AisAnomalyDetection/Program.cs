using AisAnomalyDetection.Ais;
using AisAnomalyDetection.Engine;
using AisAnomalyDetection.Util;
using Newtonsoft.Json;

namespace AisAnomalyDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //// 从配置文件中加载规则
            //var rules = Utils.LoadRulesFromConfigFile("rules.json");

            //// 初始化规则引擎
            //var ruleEngine = new RuleEngine();

            //// 添加规则到规则引擎
            //foreach (var rule in rules)
            //{
            //    ruleEngine.AddRule(rule);
            //}

            //// 初始化 AIS 数据处理器
            //var aisDataProcessor = new AisDataProcessor();

            //// 初始化异常检测器并将 AIS 数据处理器传递给它`
            //var exceptionDetector = new ExceptionDetector(ruleEngine, aisDataProcessor);

            RuleManager ruleManager = new RuleManager("rules.json");

            // 模拟 AIS 数据，船舶在禁入区域内
            string rawAisDataInside = "Ship123,1,34.7,-123.2,15.0,1.5,2023-01-01T12:00:00";
            ruleManager.ProcessAisData(rawAisDataInside);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶在禁入区域外
            string rawAisDataOutside = "Ship456,2,35.5,-122.8,25.0,2.8,2023-01-01T20:00:00";
            ruleManager.ProcessAisData(rawAisDataOutside);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶在禁入区域内，速度超过 15，触发 CompositeRuleAllMatch
            string rawAisDataCompositeAllMatch = "Ship123,3,34.3,-121.7,18.0,1.2,2023-01-01T15:00:00";
            ruleManager.ProcessAisData(rawAisDataCompositeAllMatch);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶在禁入区域外，速度超过 25，触发 CompositeRuleAnyMatch
            string rawAisDataCompositeAnyMatch = "Ship456,4,34.2,-121.0,30.0,0.8,2023-01-01T14:00:00";
            ruleManager.ProcessAisData(rawAisDataCompositeAnyMatch);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶在禁入时间段内，触发 TimeRestrictedRule
            string rawAisDataInRestrictedTime = "Ship789,5,34.2,-121.8,20.0,1.4,2023-01-01T10:00:00";
            ruleManager.ProcessAisData(rawAisDataInRestrictedTime);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶不满足基本信息规则，触发 ShipInfoRule
            string rawAisDataShipInfo = "Ship101,6,34.0,-122.0,22.0,0.9,2023-01-01T12:00:00";
            ruleManager.ProcessAisData(rawAisDataShipInfo);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶速度低于最小限制，触发 MinSpeedRule
            string rawAisDataMinSpeed = "Ship202,7,34.1,-122.5,8.0,1.7,2023-01-01T14:00:00";
            ruleManager.ProcessAisData(rawAisDataMinSpeed);
            Console.WriteLine("=====================================================");

            // 模拟 AIS 数据，船舶在队列中行驶距离超过设定的限制
            string rawAisData1 = "Ship123,8,34.0,-122.0,15.0,1.2,2023-01-01T14:00:00";
            ruleManager.ProcessAisData(rawAisData1);

            string rawAisData2 = "Ship123,8,34.1,-121.9,15.0,1.5,2023-01-01T14:01:00";
            ruleManager.ProcessAisData(rawAisData2);

            // 等待一段时间，以便船舶行驶超过设定的限制
            //System.Threading.Thread.Sleep(1000);

            string rawAisData3 = "Ship123,8,34.2,-131.8,15.0,1.8,2023-01-01T14:02:00";
            ruleManager.ProcessAisData(rawAisData3);
            Console.WriteLine("=====================================================");


        }

        
    }
}
