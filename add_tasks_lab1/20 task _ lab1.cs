using System;
using System.Linq;

namespace task20_lab1
{
    class Program
    {
        static int[] ReadAndSort()
        {
            int[] sides = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Array.Sort(sides);
            return sides;
        }

        static bool CanFit(int[] box1Sorted, int[] box2Sorted)
        {
            return box1Sorted[0] < box2Sorted[0] &&
                   box1Sorted[1] < box2Sorted[1] &&
                   box1Sorted[2] < box2Sorted[2];
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            int[] box1 = ReadAndSort();
            int[] box2 = ReadAndSort();

            bool firstFitsInSecond = CanFit(box1, box2);
            bool secondFitsInFirst = CanFit(box2, box1);

            if (firstFitsInSecond)
            {
                Console.WriteLine("перша коробка поміщається у другу");
            }
            else if (secondFitsInFirst)
            {
                Console.WriteLine("друга коробка поміщається у першу");
            }
            else
            {
                Console.WriteLine("жодна з коробок не поміщається в іншу");
            }
        }
    }
}