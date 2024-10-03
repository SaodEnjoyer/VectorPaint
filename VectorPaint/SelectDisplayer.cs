using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace VectorPaint
{
    public class SelectDisplayer : Shape
    {
        public ShapeButton[] selectedButtons = new ShapeButton[6]
        {
           
            new SelectedScaler(0),
            new SelectedScaler(1),
            new SelectedScaler(2),
            new SelectedScaler(3),
            new SelectedDeleter(),
            new SelectedMover()
        };

        public void Init(ControlCollection control)
        {
            foreach (var button in selectedButtons)
            {
                button.Init(control, this);
            }
        }

        public override float X
        {
            get;
            set;
        }
        public override float Y 
        {
            get;
            set;
        }
        public override float W
        {
            get;
            set;
        }
        public override float H
        {
            get;
            set;
        }

        public void Clear()
        {            
            X = -1;
            Y = -1;
            W = 0;
            H = 0;
            
        }
        public void SetUp(List<Shape> shapes)
        {
            if (shapes == null || !shapes.Any(s => s.Selected))
            {
                Clear();
                return;
            }

            float minX = -1;
            float minY = -1;
            float maxX = -1;
            float maxY = -1;

            foreach (var shape in shapes)
            {
                if (!shape.Selected)
                {
                    continue;
                }

                if (minX == -1 || minY == -1)
                {
                    minX = shape.X;
                    minY = shape.Y;
                    maxX = shape.X + shape.W;
                    maxY = shape.Y + shape.H;
                }
                else
                {
                    minX = Math.Min(minX, shape.X);
                    minY = Math.Min(minY, shape.Y);
                    maxX = Math.Max(maxX, shape.X + shape.W);
                    maxY = Math.Max(maxY, shape.Y + shape.H);
                }

            }

            X = minX;
            Y = minY;
            W = maxX - X;
            H = maxY - Y;

        }


        public void VisibleCheck(Graphics g)
        {
            if (X == -1 && Y == -1)
            {
                foreach (var button in selectedButtons)
                {
                    button.Visible = false;
                    button.Clear();
                }
            }
            else
            {
                foreach (var button in selectedButtons)
                {
                    button.Move(button.GetPos());
                    button.Visible = true;
                }
            }
        }
        private int border = 3;
        public override void Draw(Graphics g)
        {
            if (X == -1 && Y == -1)
            {
                return;
            }
            g.DrawRectangle(Pens.Red, X - border, Y - border, W + 2 * border, H + 2 * border);
            foreach (var button in selectedButtons)
            {
                button.Draw(g);
            }
        }
        

    }
}
