using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint
{
    public partial class Form1 : Form
    {
        public static Point Border;
        public Shape shapeToCreate = null;
        Picture picture = new Picture();
        Dictionary<string, Shape> ShapeTypes = new Dictionary<string, Shape>()
        {
            {"Rect" , new Rect()},
            {"Ellipse" , new Ellipse()}
        };
        public Form1()
        {
            InitializeComponent();

            
            picture.pictureBox = pictureBox1;
            foreach (var type in ShapeTypes)
            {
                ToolStripButton toolStripButton = new ToolStripButton(type.Key + "Button");
                toolStripButton.Text = type.Key;
                toolStripButton.Click += shapeButton_Click;
                toolStrip2.Items.Add(toolStripButton);
            }

            picture.Init(Controls);
            Invalidate();
            pictureBox1.Refresh();
            Border = pictureBox1.Location;
        }


        private void shapeButton_Click(object sender, EventArgs e)
        {
            ShapeTypes.TryGetValue(((ToolStripButton)sender).Text, out shapeToCreate) ;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (shapeToCreate != null)
            {
                shapeToCreate.X = me.X;
                shapeToCreate.Y = me.Y;
                picture.Add(shapeToCreate);
            }
            else
            {
                bool deSelect = true;                

                foreach (Shape shape in Picture.GetShapes())
                {
                    if (shape.Touch(me.X, me.Y))
                    {
                                               
                        if (!((ModifierKeys & Keys.Shift) == Keys.Shift))
                        {
                                
                            picture.ClearSelected();
                            picture.Select(shape);
                        }
                        else
                        {
                                
                            if (shape.Selected)
                            {
                                picture.DeSelect(shape);
                            }                                    
                            else
                            {
                                picture.Select(shape);
                            }

                            picture.selectDisplayer.Selected = false;
                        }
                        deSelect = false;
                        break;
                        
                        
                    }
                }
                if (deSelect)
                {
                    picture.DeSelectAll();
                    picture.selectDisplayer.Clear();
                }
            }
            pictureBox1.Invalidate();
        }


        private void selectButton_Click(object sender, EventArgs e)
        {
            shapeToCreate = null;
            
        }




        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            picture.RefreshPB(e.Graphics);
            
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            foreach (var handler in picture.selectDisplayer.selectedButtons)
            {
                if (handler.Touch(me.X, me.Y))
                {
                    handler.MouseDown?.Invoke(sender, me);
                    
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            foreach (var button in picture.selectDisplayer.selectedButtons)
            {
                if (button.Visible)
                {
                    button.MouseMove?.Invoke(sender, e);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
            foreach (var button in picture.selectDisplayer.selectedButtons)
            {
                button.MouseUp?.Invoke(sender, e);
            }
        }

    }
}
