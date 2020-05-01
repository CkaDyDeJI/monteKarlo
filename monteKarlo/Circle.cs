using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    class Circle : Point
    {
        public double Radius { get; set; }


        public Circle (Point newCenter, double newRadius) : base(newCenter.X, newCenter.Y)
        {
            Radius = newRadius;
        }
    }
}
