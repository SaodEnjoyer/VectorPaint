using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorPaint
{
    public abstract class Shape 
    {
        protected const float min_size = 25;
        public bool Selected
        {
            get; set;
        }
        protected float _x;
        protected float _y;
        protected float _w;
        protected float _h;
        public virtual float X
        {
            get { return _x; }
            set { 
                if (value < 0)
                {
                    _x = 0;                    
                }
                else
                {
                    _x = value;
                }                
            }
        }
        public virtual float Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                {
                    _y = 0;
                }
                else
                {
                    _y = value;
                }
            }
        }
        public virtual float W
        {
            get { return _w; }
            set
            {
                _w = value > min_size ? value : min_size;
            }
        }

        public virtual float H
        {
            get { return _h; }
            set
            {
                _h = value > min_size ? value : min_size;
            }
        }

        public virtual void Resize(float ax, float ay)
        {
            W = ax;
            H = ay;
        }
        public virtual void Resize(Point point)
        {
            W = point.X;
            H = point.Y;
        }
        public virtual void Move(float ax, float ay)
        {
            X = ax;
            Y = ay;
        }
        public virtual void Move(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public virtual bool Touch(float ax, float ay) 
        {
            return X<=ax && Y<=ay && X+W>=ax && Y+H>= ay;
        }
        public virtual void Draw(Graphics g)
        {

        }

        public virtual Shape Clone()
        {
            return null;
        }
        public virtual void Delete()
        {
            Picture.Remove(this);
        }

        public virtual Creator GetCreator()
        {
            return null;
        }


    }
}
