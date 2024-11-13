using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Actions
{
    public class CreateShapeAction : IAction
    {
        private Shape _shape;
        private Picture _shapes;

        public CreateShapeAction(Shape shape, Picture shapes)
        {
            _shape = shape;
            _shapes = shapes;
        }

        public void Do()
        {
            _shapes.Add(_shape);
        }

        public void UnDo()
        {
            _shape.Delete();   
        }
    }
}
