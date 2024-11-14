using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VectorPaint
{
    public class Group : Shape, IEnumerable<Shape>
    {
        private List<Shape> _shapes = new List<Shape>();
        private float lastX;
        private float lastY;
        private float lastW;
        private float lastH;

        public Group() { }

        public Group(List<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                AddShape(shape);
            }
            UpdateBounds();
        }

        public void AddShape(Shape shape)
        {
            shape.Move(shape.X - X, shape.Y - Y);
            _shapes.Add(shape);
            UpdateBounds();
        }

        public void RemoveShape(Shape shape)
        {
            _shapes.Remove(shape);
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            if (_shapes.Count == 0)
            {
                Clear();
                return;
            }

            float minX = _shapes[0].X;
            float minY = _shapes[0].Y;
            float maxX = _shapes[0].X + _shapes[0].W;
            float maxY = _shapes[0].Y + _shapes[0].H;

            foreach (var shape in _shapes)
            {
                minX = Math.Min(minX, shape.X);
                minY = Math.Min(minY, shape.Y);
                maxX = Math.Max(maxX, shape.X + shape.W);
                maxY = Math.Max(maxY, shape.Y + shape.H);
            }

            X += minX;
            Y += minY;
            W = maxX - minX;
            H = maxY - minY;

            foreach (var shape in _shapes)
            {
                shape.Move(shape.X - minX, shape.Y - minY);
            }

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;
        }

        public override void Draw(Graphics g)
        {
            if (X == -1 && Y == -1) return;

            g.TranslateTransform(X, Y);
            foreach (var shape in _shapes)
            {
                shape.Draw(g);
            }
            g.TranslateTransform(-X, -Y);
        }

        public override void Move(float ax, float ay)
        {
            float deltaX = ax - lastX;
            float deltaY = ay - lastY;

            X += deltaX;
            Y += deltaY;
            lastX = X;
            lastY = Y;
        }

        public override void Resize(float ax, float ay)
        {
            float scaleW = ax / lastW;
            float scaleH = ay / lastH;

            if (!IsCanBeScaled(scaleW, scaleH))
            {
                return;
            }

            foreach (var shape in _shapes)
            {
                float newW = shape.W * scaleW;
                float newH = shape.H * scaleH;
                shape.Resize(newW, newH);

                float relativeX = shape.X;
                float relativeY = shape.Y;
                float newX = relativeX * scaleW;
                float newY = relativeY * scaleH;
                shape.Move(newX, newY);
            }

            W = ax;
            H = ay;
            lastW = W;
            lastH = H;
            UpdateBounds();
        }

        private bool IsCanBeScaled(float scaleW, float scaleH)
        {
            foreach (var shape in _shapes)
            {
                float newW = shape.W * scaleW;
                float newH = shape.H * scaleH;

                if (newW < min_size || newH < min_size)
                {
                    return false;
                }
            }
            return true;
        }

        public void Clear()
        {
            X = -1;
            Y = -1;
            W = 0;
            H = 0;
            _shapes.Clear();
        }

        public override bool Touch(float ax, float ay)
        {
            float localX = ax - X;
            float localY = ay - Y;
            return _shapes.Any(shape => shape.Touch(localX, localY));
        }

        public override Shape Clone()
        {
            Group newGroup = new Group();
            foreach (var shape in _shapes)
            {
                newGroup.AddShape(shape.Clone());
            }
            return newGroup;
        }

        public void SetUp()
        {
            if (_shapes == null || _shapes.Count == 0)
            {
                Clear();
                return;
            }

            UpdateBounds();

            lastX = X;
            lastY = Y;
            lastW = W;
            lastH = H;
        }

        public override Creator GetCreator()
        {
            GroupCreator groupCreator = new GroupCreator();
            groupCreator.SetShape(this.Clone());
            return groupCreator;
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return _shapes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
