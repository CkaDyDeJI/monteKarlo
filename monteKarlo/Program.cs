using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monteKarlo
{
    using static System.Convert;
    class Program
    {
        private static Point leftDownPoint_;
        private static Point leftUPoint_;
        private static Point rightUPoint_;
        private static Point rightDowPoint_;

        private static double minY_;
        private static double minX_;
        private static double maxY_;
        private static double maxX_;

        private static double square_;

        private static List<double> withSquares = new List <double>();

        static void Main (string[] args)
        {
            inputDots();

            BorderFunctions.calculateCircleCenter ( rightUPoint_, rightDowPoint_ );
            BorderFunctions.calculateLinearCoeffs ( leftDownPoint_, new Point ( rightDowPoint_.X - (rightUPoint_.Y - rightDowPoint_.Y), rightUPoint_.Y ) );

            Random number = new Random ();
            int insideCounter;
            double randomX;
            double randomY;
            for (int i = 0; i < 5; i++) {
                insideCounter = 0;
                for (int j = 0; j < Math.Pow (10, i + 3); j++) {
                    randomX = minX_ + ToDouble(number.Next (0, 32767)) / 32767 * (maxX_ - minX_);//minX_ * number.Next (ToInt32 ( minX_ ), ToInt32(maxX_));
                    randomY = minY_ + ToDouble(number.Next (0, 32767)) / 32767 * (maxY_ - minY_);//number.Next (ToInt32 ( minY_ ), ToInt32(maxY_));
                    if (BorderFunctions.isInside (new Point (randomX, randomY)) == true)
                        insideCounter++;
                }

                withSquares.Add (square_ * insideCounter / Math.Pow (10, i + 3));
            }

            double actuallySquare = BorderFunctions.calculateActualSquare ( leftDownPoint_ );

            foreach (var withSquare in withSquares) {
                Console.WriteLine ($"{withSquare}, {(withSquare - actuallySquare) / actuallySquare * 100}%");
            }

            
            Console.WriteLine( $"xxxxx = {actuallySquare}");
            Console.ReadKey();
        }


        private static void inputDots ()
        {
            string[] temp = Console.ReadLine().Split (new char[] {' '});
            leftDownPoint_ = new Point ( ToDouble ( temp[0]), ToDouble ( temp[1] ));
            temp = Console.ReadLine().Split (new char[] { ' ' });
            leftUPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1]) );
            temp = Console.ReadLine ().Split (new char[] { ' ' });
            rightUPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );
            temp = Console.ReadLine ().Split ( new char[] { ' ' } );
            rightDowPoint_ = new Point ( ToDouble ( temp[0] ), ToDouble ( temp[1] ) );

            calculateSquare ();

            setMinsAndMaxs();
        }


        private static void setMinsAndMaxs ()
        {
            minX_ = leftDownPoint_.X;
            minY_ = leftDownPoint_.Y;
            maxX_ = rightDowPoint_.X;
            maxY_ = leftUPoint_.Y;
        }


        private static void calculateSquare ()
        {
            square_ = (leftUPoint_.Y - leftDownPoint_.Y) * (rightDowPoint_.X - leftDownPoint_.X);
        }
    }
}
