using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class Creator
    {
        protected Shape _shapeToCreate;

        public bool CanCreateShape()
        {
            return _shapeToCreate == null ? false : true;
        }

        public void ClearShape()
        {
            _shapeToCreate = null;
        }
        public void SetShape(Shape shape)
        {
            _shapeToCreate = shape;
        }
        public virtual Shape CreateShape(float ax, float ay)
        {
            if (_shapeToCreate == null)
            {
                return null;
            }

            _shapeToCreate.Move(ax, ay);
            return _shapeToCreate;
        }

        public Creator Clone()
        {
            var clonedCreator = new Creator();

            clonedCreator.SetShape(_shapeToCreate != null ? _shapeToCreate.Clone() : null);

            return clonedCreator;
        }

    }
}
