using AisAnomalyDetection.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Util
{
    internal class Utils
    {
        public static List<Rule> LoadSubRules(List<RuleInfo> subRuleInfos)
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
                    case "TimeRestrictedRule":
                        subRules.Add(new TimeRestrictedRule(subRuleInfo.Name, new TimeRange(subRuleInfo.RestrictedTime.StartTime, subRuleInfo.RestrictedTime.EndTime)));
                        break;
                    case "ShipInfoRule":
                        subRules.Add(new ShipInfoRule(subRuleInfo.Name));
                        break;
                    case "MinSpeedRule":
                        subRules.Add(new MinSpeedRule(subRuleInfo.Name, subRuleInfo.Threshold));
                        break;
                    case "DistanceLimitRule": // 新增处理 DistanceLimitRule
                        subRules.Add(new DistanceLimitRule(subRuleInfo.Name, subRuleInfo.DistanceLimit));
                        break;
                }
            }

            return subRules;
        }

        public static List<Rule> LoadRulesFromConfigFile(string configFile)
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
                        case "TimeRestrictedRule":
                            rules.Add(new TimeRestrictedRule(ruleInfo.Name, new TimeRange(ruleInfo.RestrictedTime.StartTime, ruleInfo.RestrictedTime.EndTime)));
                            break;
                        case "ShipInfoRule":
                            rules.Add(new ShipInfoRule(ruleInfo.Name));
                            break;
                        case "MinSpeedRule":
                            rules.Add(new MinSpeedRule(ruleInfo.Name, ruleInfo.Threshold));
                            break;
                        case "DistanceLimitRule":
                            rules.Add(new DistanceLimitRule(ruleInfo.Name, ruleInfo.DistanceLimit));
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
