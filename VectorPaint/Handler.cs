using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorPaint
{
    public class Handler : Rect
    {
        public bool Visible { get; internal set; }
        public Action<object, MouseEventArgs> MouseDown { get; internal set; }
        public Action<object, MouseEventArgs> MouseMove { get; internal set; }
        public Action<object, MouseEventArgs> MouseUp { get; internal set; }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(sender, e);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(sender, e);
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.DarkGray, X, Y, W, H);
            base.Draw(g);
        }
    }
}
