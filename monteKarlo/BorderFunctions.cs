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

        private static int functionsIsCalculated = 0;


        public static int isInside(Point newPoint) //проверка того, внутри ли находится точка
        {
            if (functionsIsCalculated != 2) {   //если линейная фунция и фунция окружности не заданы, возврат фолса

                return -1;
            }

            if (newPoint.X < centerCircle_.X) { //проверка находится ли внутри треугольника, который слева от верхней точки
                if (isLowerlinearFunction (newPoint.X, newPoint.Y) == true)
                    return 1;
                else
                    return 0;
            }
            else
                return Convert.ToInt32(isInsideCircle (newPoint.X, newPoint.Y));    //находится ли внутри четверти окружности
        }


        public static void calculateLinearCoeffs (Point firstPoint, Point secondPoint)
        {
            k_ = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);   //коэффициенты линейной функции, чтобы проверять, находится ли внутри треугольника
            b_ = firstPoint.Y - k_ * firstPoint.X;

            functionsIsCalculated++;
        }


        private static bool isLowerlinearFunction(double x, double y)
        {
            return (y < (k_ * x + b_)) ? true : false;  //проверка находится ли внутри треугольника, который слева от верхней точки
        }


        public static void calculateCircleCenter(Point cPoint, Point dPoint)
        {
            centerCircle_ = new Circle (new Point (dPoint.X, cPoint.Y), cPoint.X - dPoint.X );  //инициализация параметров круга

            functionsIsCalculated++;
        }


        private static bool isInsideCircle(double x, double y)
        {
            return ((Math.Sqrt ((x - centerCircle_.X) * (x - centerCircle_.X) + y * y)) <= centerCircle_.Radius) ? true : false;    //внутри ли круга
        }


        public static double calculateActualSquare (Point left)
        {

            return ((centerCircle_.X - left.X) * centerCircle_.Radius / 2) +
                   (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4); //нахождение настоящей площади
        }
    }
}
