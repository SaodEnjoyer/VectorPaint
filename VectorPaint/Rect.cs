using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class Rect : Shape
    {
        public Rect() { 
            X = 0; Y = 0;
            W = 40; H = 30;
        }
        public Rect(float aX, float aY, float aW, float aH)
        {
            X = aX; Y = aY;
            W = aW; H = aH;
        }
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            g.DrawRectangle(Pens.White, X, Y, W, H);
        }
        public override Shape Clone()
        {
            return new Rect(X, Y, W, H);
        }
        public override Creator GetCreator()
        {
            RectCreator RectCreator = new RectCreator();
            RectCreator.SetShape(this.Clone());
            return RectCreator;
        }
    }
}
