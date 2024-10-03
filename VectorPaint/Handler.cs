using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint
{
    public class Handler : Rect
    {
        public bool Visible { get; internal set; }
        public Action<object, MouseEventArgs> MouseDown { get; internal set; }
        public Action<object, MouseEventArgs> MouseMove { get; internal set; }
        public Action<object, MouseEventArgs> MouseUp { get; internal set; }


        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.DarkGray, X, Y, W, H);
            base.Draw(g);
        }

    }
}
