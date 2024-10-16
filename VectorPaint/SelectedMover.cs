using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorPaint;
using static System.Windows.Forms.Control;

public class SelectedMover : ShapeButton
{
    public override void Init(ControlCollection control, SelectDisplayer selectDisplayer)
    {
        base.Init(control, selectDisplayer);
    }

    public override void Activate()
    {
        base.Activate();        
    }

    public override void DeActivate()
    {
        base.DeActivate();
    }

    public override float X
    {
        get
        {
            return selectDisplayer.X;
        }
        
    }

    public override float Y
    {
        get
        {
            return selectDisplayer.Y;
        }
        
    }
    public override float W
    {
        get
        {
            return selectDisplayer.W;
        }
        
    }

    public override float H
    {
        get
        {
            return selectDisplayer.H;
        }
        
    }

    public override void SelectAction(List<Shape> shapes, PictureBox pictureBox)
    {
        MouseDown += (sender, e) =>
        {
            
            XBefore = e.X;
            YBefore = e.Y;            
            Activate();
        };

        MouseMove += (sender, e) =>
        {
            
            if (IsActivate)
            {
                float deltaX = e.X - XBefore;
                float deltaY = e.Y - YBefore;

                foreach (var shape in shapes)
                {
                    if (shape.Selected)
                    {
                        shape.Move(shape.X + deltaX, shape.Y + deltaY);
                    }
                }
                
                XBefore = e.X;
                YBefore = e.Y;

                selectDisplayer.Selected = false;
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
        return new Point((int)X, (int)Y);
    }

    public override void Draw(Graphics g)
    {       
        
    }
}
