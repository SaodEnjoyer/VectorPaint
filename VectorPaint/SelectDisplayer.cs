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
        private Picture shapes;


        public IEnumerable<Shape> GetShapes()
        {
            return shapes;
        }

        public PictureBox GetPictureBox()
        {
            return shapes.pictureBox;
        }

        public Picture GetPicture()
        {
            return shapes;
        }

        public void Init(ControlCollection control, Picture shapes)
        {
            foreach (var button in selectedButtons)
            {
                button.Init(control, this);
            }
            this.shapes = shapes;
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
                if (value > min_size)
                {
                    _w = value;
                }
                else
                {
                    _w = min_size;
                }

                UpdateShapes();
            }
        }

        public override float H
        {
            get { return _h; }
            set
            {
                if (value > min_size)
                {
                    _h = value;
                }
                else
                {
                    _h = min_size;
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

            float scaleW = W / lastW;
            float scaleH = H / lastH;

            if (!IsCanBeScaled(scaleW, scaleH))
            {
                return;
            }

            foreach (Shape shape in shapes)
            {
                if (shape.Selected)
                {
                    float newW = shape.W * scaleW;
                    float newH = shape.H * scaleH;
                    shape.Resize(newW, newH);

                    float relativeX = shape.X - lastX;
                    float relativeY = shape.Y - lastY;
                    float newX = X + relativeX * scaleW;
                    float newY = Y + relativeY * scaleH;
                    shape.Move(newX, newY);
                }
            }

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;
        }

        public bool IsCanBeScaled(float scaleW, float scaleH)
        {
            foreach (Shape shape in shapes)
            {
                if (shape.Selected)
                {
                    float newW = shape.W * scaleW;
                    float newH = shape.H * scaleH;

                    if (newW < min_size || newH < min_size)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void Resize(float ax, float ay)
        {
            float scaleW = ax / lastW;
            float scaleH = ay / lastH;

            if (!IsCanBeScaled(scaleW, scaleH))
            {
                return;
            }
            base.Resize(ax, ay);
        }

        public void Clear()
        {
            X = -1;
            Y = -1;
            W = 0;
            H = 0;
            Selected = false;
        }

        public void SetUp()
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

        public void VisibleSet(Graphics g)
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

        private void BeforeDraw()
        {

            List<Shape> selectedShapes = new List<Shape>();
            foreach (Shape shape in shapes)
            {
                if (shape.Selected)
                {
                    selectedShapes.Add(shape);
                }
            }           

            if (selectedShapes.Count() == 0)
            {
                Clear();
                return;
            }

            float minX = selectedShapes.First().X;
            float minY = selectedShapes.First().Y;
            float maxX = selectedShapes.First().X + selectedShapes.First().W;
            float maxY = selectedShapes.First().Y + selectedShapes.First().H;

            foreach (var shape in selectedShapes)
            {
                minX = Math.Min(minX, shape.X);
                minY = Math.Min(minY, shape.Y);
                maxX = Math.Max(maxX, shape.X + shape.W);
                maxY = Math.Max(maxY, shape.Y + shape.H);
            }

            X = minX;
            Y = minY;
            W = maxX - minX;
            H = maxY - minY;

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;
        }

        private int border = 3;
        public override void Draw(Graphics g)
        {
            if (X == -1 && Y == -1)
            {
                return;
            }
            BeforeDraw();
            g.DrawRectangle(Pens.Red, X - border, Y - border, W + 2 * border, H + 2 * border);
            foreach (var button in selectedButtons)
            {
                button.Move(button.GetPos());
                button.Draw(g);
            }
        }
    }
}
