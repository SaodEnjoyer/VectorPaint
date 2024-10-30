using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class EllipseCreator : Creator
    {
        public EllipseCreator()
        {
            _shapeToCreate = new Ellipse();
        }
    }
}
