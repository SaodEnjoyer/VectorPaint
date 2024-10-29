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

            FillShapeTS();

            picture.Init(Controls);

            Invalidate();

            pictureBox1.Refresh();

            Border = pictureBox1.Location;
        }

        void FillShapeTS()
        {
            foreach (var type in ShapeTypes)
            {
                AddShapeTS(type.Key);
            }
        }

        void AddShapeTS(string name, Shape shape = null)
        {
            if (shape != null)
            {
                ShapeTypes.Add(name, shape);
            }           
            ToolStripButton toolStripButton = new ToolStripButton(name + "Button");
            toolStripButton.Text = name;
            toolStripButton.Click += shapeButton_Click;
            toolStrip2.Items.Add(toolStripButton);
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
                shapeToCreate.Move(me.X, me.Y);                
                picture.Add(shapeToCreate);
            }
            else
            {
                bool deSelect = true;                

                foreach (Shape shape in picture)
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

                            picture.SetFrameActive(false);
                        }
                        deSelect = false;
                        break;        
                    }
                }
                if (deSelect)
                {
                    picture.DeSelectAll();
                    picture.ClearFrame();
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
            foreach (var handler in picture.GetHandlerButtons())
            {
                if (handler.Touch(me.X, me.Y))
                {
                    handler.OnMouseDown(sender, me);                    
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            foreach (var button in picture.GetHandlerButtons())
            {
                if (button.Visible)
                {
                    button.OnMouseMove(sender, e);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var button in picture.GetHandlerButtons())
            {
                button.OnMouseUp(sender, e);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!picture.Any(shape => shape.Selected))
            {
                MessageBox.Show("Выберите хотя бы одну фигуру.", "Нет выбранных фигур", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Group group = new Group();
            List<Shape> ShapesToDelete = new List<Shape>();

            foreach (var shape in picture)
            {
                if (shape.Selected)
                {
                    group.AddShape(shape.Clone());
                    ShapesToDelete.Add(shape);
                }
            }            

            while (ShapesToDelete.Count > 0)
            {
                ShapesToDelete[0].Delete();
                ShapesToDelete.RemoveAt(0);
            }

            picture.Add(group);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!picture.Any(shape => shape.Selected))
            {
                MessageBox.Show("Выберите хотя бы одну фигуру.", "Нет выбранных фигур", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Group group = new Group();

            foreach (var shape in picture)
            {
                if (shape.Selected)
                {
                    group.AddShape(shape.Clone());
                }
            }

            using (var groupNameForm = new GroupNameForm())
            {
                if (groupNameForm.ShowDialog(this) == DialogResult.OK)
                {
                    AddShapeTS(groupNameForm.GroupName, group);                    
                }
            }
        }
    }
}
