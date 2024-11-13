using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Actions
{
    public class ShapeActionList : IAction, IEnumerable<IAction>
    {
        private List<IAction> _actions = new List<IAction>();
        public void Do()
        {
            foreach (var action in this)
            {
                action.Do();
            }
        }

        public IEnumerator<IAction> GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        public void UnDo()
        {
            foreach (var action in this)
            {
                action.UnDo();
            }
        }

        internal void Add(IAction deleteShapeAction)
        {
            _actions.Add(deleteShapeAction);
        }

        internal void Clear()
        {
            _actions.Clear();
        }

        internal IAction Clone()
        {
            ShapeActionList actions = new ShapeActionList();
            foreach (var action in _actions)
            {
                actions.Add(action);
            }

            return actions;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
