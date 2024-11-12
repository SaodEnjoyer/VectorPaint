using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorPaint.Strategies;

namespace VectorPaint
{
    public partial class Form1 : Form
    {
        public ShapeCreator shapeCreator;
        Picture picture = new Picture();
        StrategyController strategyController;

        public Form1()
        {
            InitializeComponent();

            picture.pictureBox = pictureBox1;

            SelectControllerSetup();

            ShapeCreatorSetup();

            FillShapeTS();

            picture.Init(Controls);

            Invalidate();

            pictureBox1.Refresh();

        }

        private void SelectControllerSetup()
        {
            strategyController = new StrategyController();

            strategyController.SetStrategy(new SelectStrategy(picture));
        }

        private void ShapeCreatorSetup()
        {
            shapeCreator = new ShapeCreator(picture);

            shapeCreator.AddCreator("Select", null);
            shapeCreator.AddCreator("Rect", new RectCreator());
            shapeCreator.AddCreator("Ellipse", new EllipseCreator());
        }

        void FillShapeTS()
        {
            foreach (var type in shapeCreator.GetKeys())
            {
                AddShapeTS(type);
            }
        }

        void AddShapeTS(string name, Shape shape = null)
        {

            if (shape != null)
            {
                if (shapeCreator.GetKeys().Any(shape => shape == name))
                {
                    MessageBox.Show("Введите уникальное название.", "Неправильное название", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                shapeCreator.AddCreator(name, shape.GetCreator());
            }           
            ToolStripButton toolStripButton = new ToolStripButton(name + "Button");
            toolStripButton.Text = name;
            toolStripButton.Click += shapeButton_Click;
            toolStrip2.Items.Add(toolStripButton);
        }

        private void shapeButton_Click(object sender, EventArgs e)
        {
            shapeCreator.SetCreator(((ToolStripButton)sender).Text);

            if (shapeCreator.GetCreator() == null)
            {
                strategyController.SetStrategy(new SelectStrategy(picture));
                return;
            }

            strategyController.SetStrategy(new CreateStrategy(picture, shapeCreator));
            picture.ClearSelected();
            pictureBox1.Invalidate();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            IStrategy originalStrategy = strategyController.GetStrategy();

            bool isShiftPressed = (ModifierKeys & Keys.Shift) == Keys.Shift;          

            if (isShiftPressed)
            {
                strategyController.SetStrategy(new GroupStrategy(picture));
            }

            strategyController.GetStrategy().MouseDown(me);

            if (isShiftPressed)
            {
                strategyController.SetStrategy(originalStrategy);
            }

            pictureBox1.Invalidate();
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
            strategyController.GetStrategy().HandlerDown(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            strategyController.GetStrategy().HandlerMove(sender, e);            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            strategyController.GetStrategy().HandlerUp(sender, e);            
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
