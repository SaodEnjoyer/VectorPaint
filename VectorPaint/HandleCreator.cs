using System.Drawing;

namespace VectorPaint
{
    public class HandleCreator
    {
        public static Handler CreateHandle(Size size)
        {
            Handler newHandle = new Handler()
            {
                W = size.Width,
                H = size.Height
            };
            return newHandle;
        }
    }
}
