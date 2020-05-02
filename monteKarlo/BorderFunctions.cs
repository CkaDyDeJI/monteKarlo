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
            k_ = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
            b_ = firstPoint.Y - k_ * firstPoint.X;

            functionsIsCalculated++;
        }


        private static bool isLowerlinearFunction(double x, double y)
        {
            return (y < (k_ * x + b_)) ? true : false;
        }


        public static void calculateCircleCenter(Point cPoint, Point dPoint)
        {
            centerCircle_ = new Circle (new Point ( cPoint.X - dPoint.X, cPoint.Y), cPoint.X - dPoint.X );

            functionsIsCalculated++;
        }


        private static bool isInsideCircle(double x, double y)
        {
            return ((Math.Sqrt ( (x - centerCircle_.X) * (x - centerCircle_.X) + y * y )) <= centerCircle_.Radius) ? true : false;
        }


        public static double calculateActualSquare (Point leftDown)
        {
            return ((centerCircle_.X - leftDown.X) * centerCircle_.Radius / 2) +
                   (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4);
        }
    }
}
