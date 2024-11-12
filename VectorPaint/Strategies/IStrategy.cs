using System.Windows.Forms;

namespace VectorPaint
{
    public interface IStrategy
    {
        public void MouseDown(MouseEventArgs e);

        public void HandlerDown(object sender, MouseEventArgs e);

        public void HandlerMove(object sender, MouseEventArgs e);

        public void HandlerUp(object sender, MouseEventArgs e);
    }
}