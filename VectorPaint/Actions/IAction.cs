using System.Collections.Generic;

namespace VectorPaint
{
    public interface IAction
    {
        public void Do();
        public void UnDo();
    }
}