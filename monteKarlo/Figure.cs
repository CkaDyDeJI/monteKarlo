using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    class Figure
    {

        public Point leftPoint_ { get; set; }
        public Point upPoint_ { get; set; }
        public Point downPoint_ { get; set; }
        public Point rightPoint_ { get; set; }

        public double minY_ { get; set; }
        public double minX_ { get; set; }
        public double maxY_ { get; set; }
        public double maxX_ { get; set; }

        public double square_ { get; set; }


        public Figure(Point leftPoint, Point upPoint, Point rightPoint, Point downPoint)
        {
            leftPoint_ = leftPoint;
            upPoint_ = upPoint;
            rightPoint_ = rightPoint;
            downPoint_ = downPoint;

            setMinsAndMaxs ();

            calculateSquare ();

            BorderFunctions.calculateCircleCenter ( rightPoint_, upPoint_ );
            BorderFunctions.calculateLinearCoeffsFirst ( leftPoint_, upPoint_ );
            BorderFunctions.calculateLinearCoeffsSecond ( leftPoint_, downPoint_ );
        }


        private void setMinsAndMaxs ()
        {
            minX_ = leftPoint_.X;
            minY_ = downPoint_.Y;
            maxX_ = rightPoint_.X;
            maxY_ = upPoint_.Y;
        }


        private void calculateSquare ()
        {
            square_ = (maxX_ - minX_) * (maxY_ - minY_);
        }
    }
}
