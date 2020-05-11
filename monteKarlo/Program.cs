using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace monteKarlo
{
    using static Convert;


    struct forPoints
    {
        public static Point leftPoint_;
        public static Point upPoint_;
        public static Point rightPoint_;
    }


    class Program
    {
        private static void Main (string[] args)
        {
            inputDots();

            forOOP temp = new forOOP ();
            var firstStuff = temp.doStuff ();

            forNonOOP temp2 = new forNonOOP();
            var secondStuff = temp2.doStuff();

            if (firstStuff < secondStuff)
                Console.WriteLine ( "oop is faster" );
            else
                Console.WriteLine ( "nonoop is faster" );

            Console.ReadKey();
        }


        private static void inputDots ()
        {
            Console.Write ( "enter left point: " );
            var temp = Console.ReadLine ().Split ( ' ' );
            forPoints.leftPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );

            Console.Write ( "enter up point: " );
            temp = Console.ReadLine ().Split ( ' ' );
            forPoints.upPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );

            Console.Write ( "enter right point: " );
            temp = Console.ReadLine ().Split ( ' ' );
            forPoints.rightPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );
        }
    }


    class forOOP
    {
        private readonly List<double> withSquares = new List<double> ();
        private Figure mainFigure_;

        public TimeSpan doStuff()
        {
            mainFigure_ = new Figure ( forPoints.leftPoint_, forPoints.upPoint_, forPoints.rightPoint_ );

            Stopwatch watch = new Stopwatch ();
            watch.Start ();

            var number = new Random ();
            int insideCounter;
            double randomX;
            double randomY;
            for (var i = 0; i < 5; i++)
            {
                double n = Math.Pow ( 10, i + 3 );

                insideCounter = 0;
                for (var j = 0; j < n; j++)
                {
                    randomX = mainFigure_.minX_ + ToDouble ( number.Next ( 0, 132767 ) ) / 132767 * (mainFigure_.maxX_ - mainFigure_.minX_); //minX_ * number.Next (ToInt32 ( minX_ ), ToInt32(maxX_));
                    randomY = mainFigure_.minY_ + ToDouble ( number.Next ( 0, 132767 ) ) / 132767 * (mainFigure_.maxY_ - mainFigure_.minY_); //number.Next (ToInt32 ( minY_ ), ToInt32(maxY_));
                    if (BorderFunctions.isInside ( new Point ( randomX, randomY ) ))
                        insideCounter++;
                }

                withSquares.Add ( mainFigure_.square_ * insideCounter / n );
            }

            var actuallySquare = BorderFunctions.calculateActualSquare (mainFigure_.leftPoint_);
            watch.Stop ();

            foreach (var withSquare in withSquares)
                Console.WriteLine ( $"monte-carlo square = {withSquare}, infelicity = { Math.Abs ( withSquare - actuallySquare ) / actuallySquare}%" );


            Console.WriteLine ( $"correct square = {actuallySquare}" );
            Console.WriteLine ( $"time consumed = {watch.Elapsed}\n\r\n\r" );

            return watch.Elapsed;
        }
    }


    class forNonOOP
    {
        private readonly List<double> withSquares = new List<double> ();

        private Circle centerCircle_;

        private double k_;
        private double b_;

        private int functionsIsCalculated = 0;

        private readonly Point leftPoint_ = forPoints.leftPoint_;
        private readonly Point upPoint_ = forPoints.upPoint_;
        private readonly Point rightPoint_ = forPoints.rightPoint_;

        private double minY_;
        private double minX_;
        private double maxY_;
        private double maxX_;

        private double square_;

        public TimeSpan doStuff ()
        {
            setStuff ();

            Stopwatch watch = new Stopwatch ();
            watch.Start ();

            var number = new Random ();
            int insideCounter;
            double randomX;
            double randomY;
            for (var i = 0; i < 5; i++)
            {
                double n = Math.Pow ( 10, i + 3 );

                insideCounter = 0;
                for (var j = 0; j < n; j++)
                {
                    randomX = minX_ + ToDouble ( number.Next ( 0, 132767 ) ) / 132767 * (maxX_ - minX_); //minX_ * number.Next (ToInt32 ( minX_ ), ToInt32(maxX_));
                    randomY = minY_ + ToDouble ( number.Next ( 0, 132767 ) ) / 132767 * (maxY_ - minY_); //number.Next (ToInt32 ( minY_ ), ToInt32(maxY_));
                    if (isInside ( new Point ( randomX, randomY ) ))
                        insideCounter++;
                }

                withSquares.Add ( square_ * insideCounter / n );
            }

            var actuallySquare = calculateActualSquare ( leftPoint_);
            watch.Stop ();

            foreach (var withSquare in withSquares)
                Console.WriteLine ( $"monte-carlo square = {withSquare}, infelicity = { Math.Abs ( withSquare - actuallySquare ) / actuallySquare}%" );


            Console.WriteLine ( $"correct square = {actuallySquare}" );
            Console.WriteLine ( $"time consumed = {watch.Elapsed}\n\r\n\r" );

            return watch.Elapsed;
        }


        private void setStuff ()
        {
            setMinsAndMaxs ();

            calculateSquare ();

            calculateCircleCenter ( rightPoint_, upPoint_ );
            calculateLinearCoeffs ( leftPoint_, upPoint_ );
        }


        private void setMinsAndMaxs ()
        {
            minX_ = leftPoint_.X;
            minY_ = leftPoint_.Y;
            maxX_ = rightPoint_.X;
            maxY_ = upPoint_.Y;
        }


        private void calculateSquare ()
        {
            square_ = (maxX_ - minX_) * (maxY_ - minY_);
        }


        private bool isInside (Point newPoint)
        {
            if (functionsIsCalculated != 2)
            {
                Console.WriteLine ( "stuff is not set!" );

                return false;
            }

            if (newPoint.X < centerCircle_.X)
            {
                if (isLowerlinearFunction ( newPoint.X, newPoint.Y ) == true)
                    return true;
                else
                    return false;
            } else
                return isInsideCircle ( newPoint.X, newPoint.Y );
        }


        private void calculateLinearCoeffs (Point firstPoint, Point secondPoint)
        {
            k_ = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
            b_ = firstPoint.Y - k_ * firstPoint.X;

            functionsIsCalculated++;
        }


        private bool isLowerlinearFunction (double x, double y)
        {
            return (y < (k_ * x + b_)) ? true : false;
        }


        private void calculateCircleCenter (Point cPoint, Point dPoint)
        {
            centerCircle_ = new Circle ( new Point ( dPoint.X, cPoint.Y ), cPoint.X - dPoint.X );

            functionsIsCalculated++;
        }


        private bool isInsideCircle (double x, double y)
        {
            return ((Math.Sqrt ( (x - centerCircle_.X) * (x - centerCircle_.X) + y * y )) <= centerCircle_.Radius) ? true : false;
        }


        double calculateActualSquare (Point left)
        {
            return ((centerCircle_.X - left.X) * centerCircle_.Radius / 2) +
                   (Math.PI * centerCircle_.Radius * centerCircle_.Radius / 4);
        }
    }
}