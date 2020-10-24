using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuForce
{
    class AnalyticDict : Dictionary<int,int>
    {
        public new int this[int n]
        {
            set => base[n] = value;
            get
            {
                if (!ContainsKey(n)) base[n] = 0;
                return base[n];
            }
        }

        public override string ToString()
        {
            var Keys = base.Keys.OrderBy(t => t);
            StringBuilder sb = new StringBuilder();
            foreach (var k in Keys)
                sb.AppendLine($"{k} - {this[k]}");
            return sb.ToString();
        }
    }
}
