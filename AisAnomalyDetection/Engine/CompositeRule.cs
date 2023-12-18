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

        public CompositeRule(string name, List<Rule> rules, Func<AisData, bool> combineMethod)
        {
            Name = name;
            subRules = rules;
            Condition = combineMethod;
        }

        // 全部满足
        private bool CheckSubRulesAll(AisData data)
        {
            // 判断所有子规则是否都满足
            return subRules.All(rule => rule.Condition(data));
        }

        // 至少一个满足
        private bool CheckSubRulesAny(AisData data)
        {
            // 判断是否至少有一个子规则满足
            return subRules.Any(rule => rule.Condition(data));
        }
    }

}
