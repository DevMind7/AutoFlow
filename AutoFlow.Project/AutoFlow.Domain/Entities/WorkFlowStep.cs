using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Entities
{
    public class WorkFlowStep
    {
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = [];
    }
}
