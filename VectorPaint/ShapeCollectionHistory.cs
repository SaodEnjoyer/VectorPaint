using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class ShapeCollectionHistory
    {
        Stack<ShapeCollectionMemento> mementos = new Stack<ShapeCollectionMemento>();

        public int Count()
        {
            return mementos.Count;
        }

        public ShapeCollectionMemento Pop()
        {
            if (mementos.Count == 0)
            {
                return null;
            }
            return mementos.Pop();
        }
        public void Push(ShapeCollectionMemento memento)
        {
            mementos.Push(memento);
        }
    }
}
