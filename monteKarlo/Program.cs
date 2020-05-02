using System;
using System.Collections.Generic;


namespace monteKarlo
{
    using static Convert;


    internal class Program
    {
        //private static Point leftDownPoint_;
        //private static Point leftUPoint_;
        //private static Point rightUPoint_;
        //private static Point rightDowPoint_;

        private static Point leftPoint_;
        private static Point upPoint_;
        private static Point downPoint_;
        private static Point rightPoint_;

        private static double minY_;
        private static double minX_;
        private static double maxY_;
        private static double maxX_;

        private static double square_;

        private static readonly List <double> withSquares = new List <double>();


        private static void Main (string[] args)
        {
            inputDots();

            BorderFunctions.calculateCircleCenter (rightPoint_, upPoint_);
            BorderFunctions.calculateLinearCoeffsFirst (leftPoint_, upPoint_);
            BorderFunctions.calculateLinearCoeffsSecond (leftPoint_, downPoint_);

            var number = new Random();
            int insideCounter;
            double randomX;
            double randomY;
            for (var i = 0; i < 5; i++) {
                double n = Math.Pow (10, i + 3);

                insideCounter = 0;
                for (var j = 0; j < n; j++) {
                    randomX = minX_ + ToDouble (number.Next (0, 132767)) / 132767 * (maxX_ - minX_); //minX_ * number.Next (ToInt32 ( minX_ ), ToInt32(maxX_));
                    randomY = minY_ + ToDouble (number.Next (0, 132767)) / 132767 * (maxY_ - minY_); //number.Next (ToInt32 ( minY_ ), ToInt32(maxY_));
                    if (BorderFunctions.isInside (new Point (randomX, randomY)))
                        insideCounter++;
                }

                withSquares.Add (square_ * insideCounter / n);
            }

            var actuallySquare = BorderFunctions.calculateActualSquare (leftPoint_, upPoint_, downPoint_);

            foreach (var withSquare in withSquares)
                Console.WriteLine ($"{withSquare}, {(withSquare - actuallySquare) / actuallySquare}%");

            Console.WriteLine ($"xxxxx = {actuallySquare}");
            Console.ReadKey();
        }


        private static void inputDots()
        {
            var temp = Console.ReadLine().Split (' ');
            leftPoint_ = new Point (ToDouble (temp[0]), ToDouble (temp[1]));
            temp = Console.ReadLine().Split (' ');
            upPoint_ = new Point (ToDouble (temp[0]), ToDouble (temp[1]));
            temp = Console.ReadLine().Split (' ');
            rightPoint_ = new Point (ToDouble (temp[0]), ToDouble (temp[1]));
            temp = Console.ReadLine().Split (  ' ');
            downPoint_ = new Point (ToDouble (temp[0]), ToDouble (temp[1]));

            setMinsAndMaxs();

            calculateSquare();
        }


        private static void setMinsAndMaxs()
        {
            minX_ = leftPoint_.X;
            minY_ = downPoint_.Y;
            maxX_ = rightPoint_.X;
            maxY_ = upPoint_.Y;
        }


        private static void calculateSquare()
        {
            square_ = (maxX_ - minX_) * (maxY_ - minY_);
        }
    }
}