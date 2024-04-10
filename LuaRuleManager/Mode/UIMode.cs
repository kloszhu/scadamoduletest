using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuaRuleManager.Mode
{
    public class UIMode
    {
        public string NodeName { get; set; }
        public TreeNode Node { get; set; }
        public double Value { get; set; }
        public void Random() {
            Random random = new Random();
            Value= random.NextDouble();
        }
    }
    public class EntityModel
    {
        public string Node { get; set; }

        public double RandomValue { get; set; }
        public double NewValue { get; set; }
    
    }

}
