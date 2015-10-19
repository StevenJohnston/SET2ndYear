using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class PointVelocity
    {
        public PointF position;
        public PointF direction;
        public PointVelocity(PointF pos, PointF direc)
        {
            position = pos;
            direction = direc;
        }
    }
}
