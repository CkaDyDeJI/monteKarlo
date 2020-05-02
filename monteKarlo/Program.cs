using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    using static System.Convert;
    class Program
    {
        //private static Point leftDownPoint_;
        //private static Point leftUPoint_;
        //private static Point rightUPoint_;
        //private static Point rightDowPoint_;

        private static Point leftPoint_;
        private static Point upPoint_;
        private static Point rightPoint_;

        private static double minY_;
        private static double minX_;
        private static double maxY_;
        private static double maxX_;

        private static double square_;

        private static List<double> withSquares = new List <double>();

        static void Main (string[] args)
        {
            inputDots();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            BorderFunctions.calculateCircleCenter ( rightPoint_, upPoint_ );
            BorderFunctions.calculateLinearCoeffs ( leftPoint_, upPoint_ );

            Random number = new Random ();
            int insideCounter;
            double randomX;
            double randomY;
            for (int i = 0; i < 5; i++) {
                insideCounter = 0;
                for (int j = 0; j < Math.Pow (10, i + 3); j++) {
                    randomX = minX_ + ToDouble(number.Next ( 0, 32767 )) / 32767 * (maxX_ - minX_);//minX_ * number.Next (ToInt32 ( minX_ ), ToInt32(maxX_));
                    randomY = minY_ + ToDouble(number.Next(0, 32767)) / 32767 * (maxY_ - minY_);//number.Next (ToInt32 ( minY_ ), ToInt32(maxY_));
                    if (BorderFunctions.isInside (new Point (randomX, randomY)) == true)
                        insideCounter++;
                }

                withSquares.Add (square_ * insideCounter / Math.Pow (10, i + 3));
            }

            watch.Stop();

            foreach (var withSquare in withSquares) {
                Console.WriteLine(withSquare);
            }

            double actuallySquare = BorderFunctions.calculateActualSquare (leftPoint_);
            Console.WriteLine( $"xxxxx = {actuallySquare}");
            Console.WriteLine($"{watch.Elapsed.Seconds}, {watch.Elapsed.Milliseconds}");
            Console.ReadKey();
        }


        private static void inputDots ()
        {
            string[] temp = Console.ReadLine().Split (new char[] {' '});
            leftPoint_ = new Point ( ToDouble ( temp[0]), ToDouble ( temp[1] ));
            temp = Console.ReadLine().Split (new char[] { ' ' });
            upPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1]) );
            temp = Console.ReadLine ().Split (new char[] { ' ' });
            rightPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );

            setMinsAndMaxs();

            calculateSquare ();
        }


        private static void setMinsAndMaxs ()
        {
            minX_ = leftPoint_.X;
            minY_ = leftPoint_.Y;
            maxX_ = rightPoint_.X;
            maxY_ = upPoint_.Y;
        }


        private static void calculateSquare ()
        {
            square_ = (maxX_ - minX_) * (maxY_ - minY_);
        }
    }
}
