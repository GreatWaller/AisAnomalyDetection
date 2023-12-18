using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class RuleEngine
    {
        private List<Rule> rules = new List<Rule>();

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public void CheckRules(AisData aisData)
        {
            foreach (var rule in rules)
            {
                if (rule.Condition(aisData))
                {
                    rule.OnViolation(aisData);
                }
            }
        }
    }
}
