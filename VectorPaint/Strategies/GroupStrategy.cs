using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint.Strategies
{
    public class GroupStrategy : IStrategy
    {
        public GroupStrategy(Picture shapes)
        {
            _shapes = shapes;
        }

        private Picture _shapes;
        public void MouseDown(MouseEventArgs e)
        {
            bool deSelect = true;

            foreach (Shape shape in _shapes)
            {
                if (shape.Touch(e.X, e.Y))
                {
                    if (shape.Selected)
                    {
                        _shapes.DeSelect(shape);
                    }
                    else
                    {
                        _shapes.Select(shape);
                    }

                    _shapes.SetFrameActive(false);
                    deSelect = false;
                    break;
                }
            }

            if (deSelect)
            {
                _shapes.DeSelectAll();
                _shapes.ClearFrame();
            }
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
