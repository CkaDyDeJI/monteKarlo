using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    class Figure
    {
        public double minY_ { get; set; }
        public double minX_ { get; set; }
        public double maxY_ { get; set; }
        public double maxX_ { get; set; }

        public double square_ { get; set; } //площадь изначального прямоугольника


        public Figure(Point leftPoint, Point upPoint, Point rightPoint)
        {
            setMinsAndMaxs (leftPoint, upPoint, rightPoint );

            calculateSquare ();

            BorderFunctions.calculateCircleCenter ( leftPoint, upPoint );    //вычисление линейной фунции по двум точкам
            BorderFunctions.calculateLinearCoeffs ( rightPoint, upPoint ); //вычисление функции окружности по двум точкам
        }


        private void setMinsAndMaxs (Point leftPoint, Point upPoint, Point rightPoint)
        {
            minX_ = leftPoint.X;
            minY_ = leftPoint.Y;
            maxX_ = rightPoint.X;
            maxY_ = upPoint.Y;
        }


        private void calculateSquare ()
        {
            square_ = (maxX_ - minX_) * (maxY_ - minY_);
        }
    }
}
