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

            // 从配置文件中加载规则
            var rules = LoadRulesFromConfigFile("rules.json");

            // 初始化规则引擎
            var ruleEngine = new RuleEngine();

            // 添加规则到规则引擎
            foreach (var rule in rules)
            {
                ruleEngine.AddRule(rule);
            }

            // 初始化 AIS 数据处理器
            var aisDataProcessor = new AisDataProcessor();

            // 初始化异常检测器并将 AIS 数据处理器传递给它
            var exceptionDetector = new ExceptionDetector(ruleEngine, aisDataProcessor);

            // 模拟 AIS 数据，船舶在禁入区域内
            string rawAisDataInside = "Ship123,1,34.7,-123.2,15.0";
            var aisDataInside = aisDataProcessor.ProcessAisData(rawAisDataInside);

            // 模拟 AIS 数据，船舶在禁入区域外
            string rawAisDataOutside = "Ship456,2,35.5,-122.8,25.0";
            var aisDataOutside = aisDataProcessor.ProcessAisData(rawAisDataOutside);

            // 模拟 AIS 数据，船舶在禁入区域内，速度超过 15，触发 CompositeRuleAllMatch
            string rawAisDataCompositeAllMatch = "Ship123,3,34.3,-121.7,18.0";
            var aisDataCompositeAllMatch = aisDataProcessor.ProcessAisData(rawAisDataCompositeAllMatch);

            // 模拟 AIS 数据，船舶在禁入区域外，速度超过 25，触发 CompositeRuleAnyMatch
            string rawAisDataCompositeAnyMatch = "Ship456,4,34.2,-121.0,30.0";
            var aisDataCompositeAnyMatch = aisDataProcessor.ProcessAisData(rawAisDataCompositeAnyMatch);
        }

        private static List<Rule> LoadSubRules(List<RuleInfo> subRuleInfos)
        {
            List<Rule> subRules = new List<Rule>();

            foreach (var subRuleInfo in subRuleInfos)
            {
                switch (subRuleInfo.Type)
                {
                    case "SpeedLimitRule":
                        subRules.Add(new SpeedLimitRule(subRuleInfo.Name, subRuleInfo.Threshold));
                        break;
                    case "RestrictedAreaRule":
                        subRules.Add(new RestrictedAreaRule(subRuleInfo.Name, subRuleInfo.AreaCoordinates.Select(coord => new Coordinate(coord.Latitude, coord.Longitude)).ToList()));
                        break;
                        // 添加其他规则类型的处理
                }
            }

            return subRules;
        }

        private static List<Rule> LoadRulesFromConfigFile(string configFile)
        {
            List<Rule> rules = new List<Rule>();

            try
            {
                // 获取当前工作目录
                string currentDirectory = Directory.GetCurrentDirectory();

                // 拼接配置文件的完整路径
                string configFilePath = Path.Combine(currentDirectory, "Config", configFile);

                string jsonContent = File.ReadAllText(configFilePath);
                var config = JsonConvert.DeserializeObject<RuleConfig>(jsonContent);

                foreach (var ruleInfo in config.Rules)
                {
                    switch (ruleInfo.Type)
                    {
                        case "SpeedLimitRule":
                            rules.Add(new SpeedLimitRule(ruleInfo.Name, ruleInfo.Threshold));
                            break;
                        case "RestrictedAreaRule":
                            rules.Add(new RestrictedAreaRule(ruleInfo.Name, ruleInfo.AreaCoordinates.Select(coord => new Coordinate(coord.Latitude, coord.Longitude)).ToList()));
                            break;
                        case "CompositeRuleAllMatch":
                            rules.Add(new CompositeRule(ruleInfo.Name, LoadSubRules(ruleInfo.SubRules), results => results.All(result => result)));
                            break;
                        case "CompositeRuleAnyMatch":
                            rules.Add(new CompositeRule(ruleInfo.Name, LoadSubRules(ruleInfo.SubRules), results => results.Any(result => result)));
                            break;
                            // 可根据需要添加其他规则类型的处理
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载规则配置文件出错: {ex.Message}");
            }

            return rules;
        }
    }
}
