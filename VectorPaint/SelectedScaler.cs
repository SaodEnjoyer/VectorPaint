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
    public class SelectedScaler : ShapeButton
    {
        private int _id;
        public SelectedScaler()
        {

        }

        public SelectedScaler(int id)
        {
            _id = id;
        }

        
        public override void Init(ControlCollection control, SelectDisplayer selectDisplayer)
        {
            W = 6;
            H = 6;
            base.Init(control, selectDisplayer);          

        }

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

        public override void SelectAction(List<Shape> shapes, PictureBox pictureBox)
        {
            
            MouseDown += (sender, e) =>
            {                
                var mouseEventArgs = e;
                XBefore = mouseEventArgs.X;
                YBefore = mouseEventArgs.Y;
                Activate();
            };

            MouseMove += (sender, e) =>
            {
                if (IsActivate)
                {
                    var mouseEventArgs = e;
                    float deltaX = mouseEventArgs.X - XBefore;
                    float deltaY = mouseEventArgs.Y - YBefore;
                    DeActivate();
                    foreach (Shape shape in shapes)
                    {
                        if (shape.Selected)
                        {
                            switch (_id)
                            {
                                case 0: 
                                    shape.Resize(shape.W - deltaX, shape.H - deltaY);
                                    shape.Move(shape.X + deltaX, shape.Y + deltaY);
                                    break;
                                case 1:
                                    shape.Resize(shape.W + deltaX, shape.H - deltaY);
                                    shape.Move(shape.X, shape.Y + deltaY);
                                    break;
                                case 2:
                                    shape.Resize(shape.W + deltaX, shape.H + deltaY);
                                    break;
                                case 3: 
                                    shape.Resize(shape.W - deltaX, shape.H + deltaY);
                                    shape.Move(shape.X + deltaX, shape.Y);
                                    break;
                            }
                        }
                    }
                    Activate();
                    XBefore = mouseEventArgs.X;
                    YBefore = mouseEventArgs.Y;
                    pictureBox.Invalidate();
                }
            };


            MouseUp += (sender, e) =>
            {
                DeActivate();
                pictureBox.Invalidate();
            };
            
        }
        public override Point GetPos()
        {
            Point point = new Point();
            
            switch (_id)
            {
                case 0:
                    {
                        point = new Point((int)(selectDisplayer.X - 2*W), (int)(selectDisplayer.Y - 2*H));
                        break;
                    }
                case 1:
                    {
                        point = new Point((int)(selectDisplayer.X + selectDisplayer.W + W), (int)(selectDisplayer.Y - 2*H));
                        break;
                    }
                case 2:
                    {
                        point = new Point((int)(selectDisplayer.X + selectDisplayer.W + W), (int)(selectDisplayer.Y + selectDisplayer.H + H));
                        break;
                    }
                case 3:
                    {
                        point = new Point((int)(selectDisplayer.X - 2*W), (int)(selectDisplayer.Y + selectDisplayer.H + H));
                        break;
                    }
            }
            
            return SetBorder(point);
        }
    }
}
