using AisAnomalyDetection.Ais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisAnomalyDetection.Engine
{
    internal class Rule
    {
        public string Type { get; set; } // 规则类型
        public string Name { get; set; } // 规则名称
        public Func<AisData, bool> Condition { get; set; } // 规则条件
        public Action<AisData> OnViolation { get; set; } // 违规处理

        // 构造函数用于初始化实例
        public Rule()
        {
            Type = "Rule";
        }
    }
}
