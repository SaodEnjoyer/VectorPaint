using System;
using System.Collections.Generic;

namespace VectorPaint
{
    public class ShapeCollectionMemento
    {
        private IAction _action;

        public ShapeCollectionMemento(IAction action)
        {
            _action = action;
        }

        public void Do()
        {
            _action.Do();
        }

        public void UnDo()
        {
            _action.UnDo();
        }
    }
}