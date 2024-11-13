using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Actions
{
    public class ResizeShapeAction : IAction
    {
        private Shape _shape;
        private Point _sizeBefore;

        public ResizeShapeAction(Shape shape)
        {
            _shape = shape;
            _sizeBefore = new Point((int)_shape.W, (int)_shape.H);
        }
        public void Do()
        {
            return;
        }

        public void UnDo()
        {
            _shape.Resize(_sizeBefore);
        }
    }
}
