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
    public class SelectedDeleter : ShapeButton
    {
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
        public override void Init(ControlCollection control, SelectDisplayer selectDisplayer)
        {
            
            W = 10;
            H = 10;
            Offset = new Point(-5, -15);
            base.Init(control, selectDisplayer);

        }

        public override void SelectAction()
        {
            MouseDown += (sender, e) =>
            {
                List<Shape> listToDelete = new List<Shape>();
                
                foreach (var shape in selectDisplayer.GetShapes())
                {
                    if (shape.Selected)
                    {
                        listToDelete.Add(shape);
                    }
                }

                foreach (var shape in listToDelete)
                {
                    shape.Delete();
                }
                selectDisplayer.GetPictureBox().Invalidate();
            };
            
        }

        public override Point GetPos()
        {
            return SetBorder(new Point(((int)(selectDisplayer.X + selectDisplayer.W/2 ))  + Offset.X , (int)(selectDisplayer.Y) + Offset.Y));
        }

        public override void Draw(Graphics g)
        {
            
            g.FillRectangle(Brushes.DarkRed, X, Y, W, H);
            g.DrawRectangle(Pens.Red, X, Y, W, H);
        }
    }
}
