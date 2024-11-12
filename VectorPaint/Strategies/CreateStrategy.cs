using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint.Strategies
{
    public class CreateStrategy : IStrategy
    {
        public CreateStrategy(Picture shapes, ShapeCreator creator)
        {
            _shapes = shapes;
            _creator = creator;
        }

        private Picture _shapes;
        private ShapeCreator _creator;

        public void MouseDown(MouseEventArgs e)
        {
            _shapes.Add(_creator.GetCreator().CreateShape(e.X, e.Y));
        }

        public void HandlerDown(object sender, MouseEventArgs e)
        {
            return;
        }

        public void HandlerMove(object sender, MouseEventArgs e)
        {
            return;
        }

        public void HandlerUp(object sender, MouseEventArgs e)
        {
            return;
        }
    }
}
