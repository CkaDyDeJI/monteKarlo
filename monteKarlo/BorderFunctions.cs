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

        //private static double k2_;
        //private static double b2_;
        //private static double r_;

        private static int functionsIsCalculated = 0;


        public static bool isInside(Point newPoint)
        {
            if (functionsIsCalculated != 2) {
                Console.WriteLine("stuff is not set!");

                return false;
            }

            if (newPoint.X < centerCircle_.X) {
                if (isLowerlinearFunction (newPoint.X, newPoint.Y) == true)
                    return true;
                else
                    return false;
            }
            else
                return isInsideCircle (newPoint.X, newPoint.Y);
        }


        public static void calculateLinearCoeffs (Point firstPoint, Point secondPoint)
        {
            k_ = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
            b_ = firstPoint.Y - k_ * firstPoint.X;

            functionsIsCalculated++;
        }


        //public static void calculateLinearCoeffsSecond (Point firstPoint, Point secondPoint)
        //{
        //    k2_ = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
        //    b2_ = firstPoint.Y - k2_ * firstPoint.X;

        //    functionsIsCalculated++;
        //}


        private static bool isLowerlinearFunction(double x, double y)
        {
            return (y < (k_ * x + b_)) ? true : false;
        }


        //private static bool isUpperlinearFunction (double x, double y)
        //{
        //    return (y > (k2_ * x + b2_)) ? true : false;
        //}


        public static void calculateCircleCenter(Point cPoint, Point dPoint)
        {
            centerCircle_ = new Circle (new Point (dPoint.X, cPoint.Y), cPoint.X - dPoint.X );

            functionsIsCalculated++;
        }


        private static bool isInsideCircle(double x, double y)
        {
            return ((Math.Sqrt ((x - centerCircle_.X) * (x - centerCircle_.X) + y * y)) <= centerCircle_.Radius) ? true : false;
        }


        public static double calculateActualSquare (Point left)
        {
            //return (centerCircle_.X - left.X) * (up.Y - down.Y) - ((up.Y - left.Y) * (up.X - left.X) * 0.5) -
            //    ((down.X - left.X) * (left.Y - down.Y) * 0.5) + (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4);

            return ((centerCircle_.X - left.X) * centerCircle_.Radius / 2) +
                   (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4);
        }
    }
}
