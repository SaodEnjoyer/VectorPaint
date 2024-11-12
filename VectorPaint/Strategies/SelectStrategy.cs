using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint.Strategies
{
    public class SelectStrategy : IStrategy
    {
        public SelectStrategy(Picture shapes)
        {
            _shapes = shapes;
        }

        private Picture _shapes;
        public void MouseDown(MouseEventArgs e)
        {
            if (_shapes.GetHandlerButtons().Any(handler => handler.Touch(e.X, e.Y)))
            {
                return;
            }

            bool deSelect = true;

            foreach (Shape shape in _shapes)
            {
                if (shape.Touch(e.X, e.Y))
                {
                    _shapes.DeSelectAll();
                    _shapes.ClearSelected();
                    _shapes.Select(shape);

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
            MouseEventArgs me = (MouseEventArgs)e;
            foreach (var handler in _shapes.GetHandlerButtons())
            {
                if (handler.Touch(me.X, me.Y))
                {
                    handler.OnMouseDown(sender, me);
                }
            }
        }

        public void HandlerMove(object sender, MouseEventArgs e)
        {
            foreach (var button in _shapes.GetHandlerButtons())
            {
                if (button.Visible)
                {
                    button.OnMouseMove(sender, e);
                }
            }
        }

        public void HandlerUp(object sender, MouseEventArgs e)
        {
            foreach (var button in _shapes.GetHandlerButtons())
            {
                button.OnMouseUp(sender, e);
            }
        }
    }
}
