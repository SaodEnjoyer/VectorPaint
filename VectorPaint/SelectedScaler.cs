using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VectorPaint
{
    public class SelectedScaler : ShapeButton
    {
        private int _id;

        public SelectedScaler() { }

        public SelectedScaler(int id)
        {
            _id = id;
        }

        public override void Init(Control.ControlCollection control, SelectDisplayer selectDisplayer)
        {
            W = 6;
            H = 6;
            base.Init(control, selectDisplayer);
        }

        public override float X { get; set; }
        public override float Y { get; set; }
        public override float W { get; set; }
        public override float H { get; set; }

        public override void SelectAction()
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

                    switch (_id)
                    {
                        case 0: // Верхний левый угол
                            selectDisplayer.Move(selectDisplayer.X + deltaX, selectDisplayer.Y + deltaY);
                            selectDisplayer.Resize(selectDisplayer.W - deltaX, selectDisplayer.H - deltaY);
                            break;

                        case 1: // Верхний правый угол
                            selectDisplayer.Move(selectDisplayer.X, selectDisplayer.Y + deltaY);
                            selectDisplayer.Resize(selectDisplayer.W + deltaX, selectDisplayer.H - deltaY);
                            break;

                        case 2: // Нижний правый угол
                            selectDisplayer.Move(selectDisplayer.X, selectDisplayer.Y);
                            selectDisplayer.Resize(selectDisplayer.W + deltaX, selectDisplayer.H + deltaY);
                            break;

                        case 3: // Нижний левый угол
                            selectDisplayer.Move(selectDisplayer.X + deltaX, selectDisplayer.Y);
                            selectDisplayer.Resize(selectDisplayer.W - deltaX, selectDisplayer.H + deltaY);
                            break;
                    }
                    XBefore = mouseEventArgs.X;
                    YBefore = mouseEventArgs.Y;
                    selectDisplayer.GetPictureBox().Invalidate();
                }
            };

            MouseUp += (sender, e) =>
            {
                DeActivate();
                selectDisplayer.GetPictureBox().Invalidate();
            };

        }

        public override Point GetPos()
        {
            Point point = new Point();

            switch (_id)
            {
                case 0:
                    point = new Point((int)(selectDisplayer.X - 2 * W), (int)(selectDisplayer.Y - 2 * H));
                    break;
                case 1:
                    point = new Point((int)(selectDisplayer.X + selectDisplayer.W + W), (int)(selectDisplayer.Y - 2 * H));
                    break;
                case 2:
                    point = new Point((int)(selectDisplayer.X + selectDisplayer.W + W), (int)(selectDisplayer.Y + selectDisplayer.H + H));
                    break;
                case 3:
                    point = new Point((int)(selectDisplayer.X - 2 * W), (int)(selectDisplayer.Y + selectDisplayer.H + H));
                    break;
            }

            return SetBorder(point);
        }
    }
}
