using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    internal class Ellipse : Shape
    {
        public Ellipse()
        {
            X = 0; Y = 0;
            W = 40; H = 30;
        }
        public Ellipse(float aX, float aY, float aW, float aH)
        {
            X = aX; Y = aY;
            W = aW; H = aH;
        }
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            g.DrawEllipse(Pens.White, X, Y, W, H);
        }
        public override Shape Clone()
        {
            return new Ellipse(X, Y, W, H);
        }
        public override Creator GetCreator()
        {
            EllipseCreator ellipseCreator = new EllipseCreator();
            ellipseCreator.SetShape(this.Clone());
            return ellipseCreator;
        }
    }
}
