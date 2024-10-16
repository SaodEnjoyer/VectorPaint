using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            get { return _x; }
            set
            {
                _x = value;
                UpdateShapes();
            }
        }

        public override float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                UpdateShapes();
            }
        }

        public override float W
        {
            get { return _w; }
            set
            {
                if (value > 25)
                {
                    _w = value;
                }
                else
                {
                    _w = 25;
                }
                UpdateShapes();
            }
        }

        public override float H
        {
            get { return _h; }
            set
            {
                if (value > 25)
                {
                    _h = value;
                }
                else
                {
                    _h = 25;
                }
                UpdateShapes();
            }
        }

        float lastX;
        float lastY;
        float lastW;
        float lastH;

        private void UpdateShapes()
        {
            if (!Selected)
            {
                return;
            }

            List<Shape> shapes = Picture.GetShapes();

            float scaleW = W / lastW;
            float scaleH = H / lastH;

            foreach (Shape shape in shapes)
            {
                if (shape.Selected)
                {
                    float relativeX = shape.X - lastX;
                    float relativeY = shape.Y - lastY;

                    float newX = X + relativeX * scaleW;
                    float newY = Y + relativeY * scaleH;
                    float newW = shape.W * scaleW;
                    float newH = shape.H * scaleH;

                    shape.Move(newX, newY);
                    shape.Resize(newW, newH);
                }
            }

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;
        }


        public void Clear()
        {
            X = -1;
            Y = -1;
            W = 0;
            H = 0;
            Selected = false;
        }

        public void SetUp(List<Shape> shapes)
        {
            if (shapes == null || !shapes.Any(s => s.Selected))
            {
                Clear();
                return;
            }

            if (Selected)
            {
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

            Move(minX, minY);
            Resize(maxX - X, maxY - Y);

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;

            Selected = true;
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
