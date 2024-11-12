using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public class ShapeCreator
    {
        private Creator currentCreator = null;

        private Picture picture;

        private Dictionary<string, Creator> Creators = new Dictionary<string, Creator>();

        public ShapeCreator(Picture picture)
        {
            this.picture = picture;
        }

        public void AddCreator(string name, Creator creator)
        {
            Creators[name] = creator;
        }

        public void SetCreator(string name)
        {
            if (!Creators.TryGetValue(name, out var creator) || creator == null)
            {
                currentCreator = null;
                return;
            }
            currentCreator = creator.Clone();            
        }

        public Creator GetCreator()
        {
            return currentCreator;
        }

        public IEnumerable<string> GetKeys()
        {
            return Creators.Keys;
        }

    }
}
