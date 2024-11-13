using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Actions
{
    public class MoveShapeAction : IAction
    {
        private Shape _shape;
        private Point _positionBefore;

        public MoveShapeAction(Shape shape)
        {
            _shape = shape;
            _positionBefore = new Point((int)_shape.X, (int)_shape.Y);
        }


        public void Do()
        {
            return;
        }

        public void UnDo()
        {
            _shape.Move(_positionBefore);
        }

    }
}
