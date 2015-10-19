using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class Shape
    {
        protected Color c;
        private int shapeTrail = 10;

        public int ShapeTrail
        {
            get
            {
                return shapeTrail;
            }

            set
            {
                shapeTrail = value;
            }
        }

        public virtual void draw(Graphics e)
        {
        }
        public virtual void update()
        {
        }
    }
}
