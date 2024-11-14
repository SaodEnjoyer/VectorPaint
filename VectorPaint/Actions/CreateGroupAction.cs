using System.Collections.Generic;

namespace VectorPaint.Actions
{
    internal class CreateGroupAction : IAction
    {
        private Picture _picture;
        private Group _group;
        private List<Shape> _shapes;

        private Dictionary<Shape, (float X, float Y)> _originalPositions;

        public CreateGroupAction(Group group, List<Shape> shapes, Picture picture)
        {
            _group = group;
            _shapes = shapes;
            _picture = picture;

            _originalPositions = new Dictionary<Shape, (float X, float Y)>();
            foreach (var shape in _shapes)
            {
                _originalPositions[shape] = (shape.X, shape.Y);
            }
        }

        public void Do()
        {
            foreach (var shape in _shapes)
            {
                _group.AddShape(shape);
                shape.Delete();
            }
            _picture.Add(_group);
        }

        public void UnDo()
        {
            foreach (var shape in _shapes)
            {
                var (originalX, originalY) = _originalPositions[shape];
                shape.Move(originalX, originalY);
                _picture.Add(shape);
            }
            _group.Delete();
        }
    }
}
