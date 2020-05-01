using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    static class BorderFunctions
    {
        private static Circle centerCircle_;
        private static double k_;
        private static double b_;
        //private static double r_;

        private static int functionsIsCalculated = 0;


        public static bool isInside(Point newPoint)
        {
            if (functionsIsCalculated != 2) {
                Console.WriteLine("stuff is not set!");

                return false;
            }

            if (newPoint.X < centerCircle_.X)
                return isLowerlinearFunction (newPoint.X, newPoint.Y);
            else
                return isInsideCircle (newPoint.X, newPoint.Y);
        }


        public static void calculateLinearCoeffs(Point firstPoint, Point secondPoint)
        {
            b_ = (secondPoint.Y * firstPoint.X - secondPoint.X * firstPoint.Y) / (-secondPoint.X + 1);
            if ((firstPoint.X != 0 && firstPoint.Y != 0) || (firstPoint.X != 0))
                k_ = (firstPoint.Y - b_) / firstPoint.X;
            else {
                if (firstPoint.Y != 0)
                    k_ = (firstPoint.Y - b_);
                else
                    k_ = 1;
            }

            functionsIsCalculated++;
        }


        private static bool isLowerlinearFunction(double x, double y)
        {
            return (y < (k_ * x + b_)) ? true : false;
        }


        public static void calculateCircleCenter(Point cPoint, Point dPoint)
        {
            centerCircle_ = new Circle (new Point ( dPoint.X - (cPoint.Y - dPoint.Y), dPoint.Y), cPoint.Y - dPoint.Y );

            functionsIsCalculated++;
        }


        private static bool isInsideCircle(double x, double y)
        {
            return ((Math.Sqrt (x * x + y * y) - centerCircle_.X) < centerCircle_.Radius) ? true : false;
        }


        public static double calculateActualSquare (Point leftDown)
        {
            return ((centerCircle_.X - leftDown.X) * centerCircle_.Radius / 2) +
                   (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4);
        }
    }
}
