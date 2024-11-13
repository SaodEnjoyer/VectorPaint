using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Actions
{
    internal class DeleteShapeAction : IAction
    {
        private Shape _shape;
        private Picture _shapes;

        public DeleteShapeAction(Shape shape, Picture shapes)
        {
            _shape = shape;
            _shapes = shapes;
        }

        public void Do()
        {
            _shape.Delete();
        }

        public void UnDo()
        {
            _shapes.Add(_shape);            
        }
    }
}
