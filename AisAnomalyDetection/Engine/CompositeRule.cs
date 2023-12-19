using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    // 新的规则类型，表示多个规则的组合
    internal class CompositeRule : Rule
    {
        private List<Rule> subRules;
        private Func<IEnumerable<bool>, bool> checkMethod;

        public CompositeRule(string name, List<Rule> rules, Func<IEnumerable<bool>, bool> checkMethod)
        {
            Type = "CompositeRule";
            Name = name;
            subRules = rules;
            this.checkMethod = checkMethod;
            Condition = data => CheckSubRules(data);
            OnViolation = data => Console.WriteLine($"组合规则告警：船舶 {data.VesselId} 违反了组合规则 {Name}");
        }

        private bool CheckSubRules(AisData data)
        {
            // 检查所有子规则是否满足
            var results = subRules.Select(rule => rule.Condition(data));
            return checkMethod(results);
        }
    }

}
