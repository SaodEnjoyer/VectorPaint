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
    public class ShapeButton : Handler
    {

        public SelectDisplayer selectDisplayer;

        public Point Offset;        

        public virtual void Init(ControlCollection control, SelectDisplayer selectDisplayer)
        {
            this.selectDisplayer = selectDisplayer;
            this.Visible = false;
        }
        public float XBefore
        {
            get;
            set;
        }
        public float YBefore
        {
            get;
            set;
        }



        public bool IsActivate
        {
            get;
            set;
        }


        public virtual void Activate()
        {
            Clear();
            IsActivate = true;
            //Cursor.Hide();
        }
        public virtual void DeActivate()
        {            
            Clear();
            //Cursor.Show();
        }

        public virtual void Clear()
        {
            IsActivate = false;
        }

        public virtual void SelectAction(List<Shape> shapes, PictureBox pictureBox)
        {

        }
        public virtual Point GetPos()
        {
            return SetBorder(new Point((int)(selectDisplayer.X - W) / 2 + Offset.X, (int)(selectDisplayer.Y - H) / 2 + Offset.Y) );
        }
        public Point SetBorder(Point point)
        {
            return point;

        }
    }
}
