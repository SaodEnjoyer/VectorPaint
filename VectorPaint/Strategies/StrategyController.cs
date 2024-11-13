using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class StrategyController
    {


        private IStrategy _strategy;

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public IStrategy GetStrategy()
        {
            return _strategy;
        }
    }
}
