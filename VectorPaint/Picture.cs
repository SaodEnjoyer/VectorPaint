using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace VectorPaint
{
    public class Picture : IEnumerable<Shape>
    {
        public PictureBox pictureBox;
        public SelectDisplayer selectDisplayer = new SelectDisplayer();

        private static List<Shape> _shapes = new List<Shape>();

        public void Add(Shape shapeToCreate)
        {
            _shapes.Add(shapeToCreate.Clone());
        }
        public ShapeButton[] GetHandlerButtons()
        {
            return selectDisplayer.selectedButtons;
        }
        public static void Remove(Shape shapeToRemove)
        {
            if (_shapes.Contains(shapeToRemove))
            {
                _shapes.Remove(shapeToRemove);
            }
        }

        public bool IsFrameActive()
        {
            return selectDisplayer.Selected;
        }

        public void SetFrameActive(bool b)
        {
            selectDisplayer.Selected = b;
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
            foreach (Shape shape in this)
            {
                shape.Selected = false;
            }
        }

        public void RefreshPB(Graphics g)
        {
            g.Clear(Color.Black);
            ShowSD(g);           
            foreach (Shape shape in this)
            {
                shape.Draw(g);
            }
            selectDisplayer.Draw(g);
        }

        public void ShowSD(Graphics g)
        {
            selectDisplayer.SetUp();
            selectDisplayer.VisibleSet(g);
        }

        public void DeSelectAll()
        {
            foreach (Shape shape in this)
            {
                DeSelect(shape);
            }
        }

        public void ClearFrame()
        {
            selectDisplayer.Clear();
        }

        public void Init(ControlCollection control)
        {
            selectDisplayer.Init(control, this);
            foreach (var button in selectDisplayer.selectedButtons)
            {
                button.SelectAction();
            }
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return _shapes.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
