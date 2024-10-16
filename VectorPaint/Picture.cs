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
    public class Picture
    {
        public PictureBox pictureBox;
        public SelectDisplayer selectDisplayer = new SelectDisplayer();

        public static List<Shape> shapes = new List<Shape>();

        public static List<Shape> GetShapes() { return shapes; }
        public void Add(Shape shapeToCreate)
        {
            shapes.Add(shapeToCreate.Clone());
        }
        public static void Remove(Shape shapeToRemove)
        {
            if (shapes.Contains(shapeToRemove))
            {
                shapes.Remove(shapeToRemove);
            }
           
        }


        public void Select(Shape shape)
        {
            shape.Selected = true;
        }
        public void DeSelect(Shape shape)
        {
            shape.Selected = false;
        }
        public void ClearSelected() 
        { 
            foreach (Shape shape in GetShapes())
            {
                shape.Selected = false;
            }
        }
        public void RefreshPB(Graphics g)
        {
            g.Clear(Color.Black);
            ShowSD(g);
            foreach (Shape shape in GetShapes())
            {
                shape.Draw(g);
            }
            selectDisplayer.Draw(g);            
        }

        public void ShowSD(Graphics g)
        {
            selectDisplayer.SetUp(GetShapes());
            selectDisplayer.VisibleCheck(g);
        }


        public void DeSelectAll()
        {
            foreach (Shape shape in GetShapes())
            {
                DeSelect(shape);
            }
        }

        public void Init(ControlCollection control)
        {
            selectDisplayer.Init(control);
            foreach (var button in selectDisplayer.selectedButtons)
            {
                button.SelectAction(GetShapes(), pictureBox);
            }

        }

    }
}
