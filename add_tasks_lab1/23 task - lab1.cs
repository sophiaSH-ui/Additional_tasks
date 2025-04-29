using System;

namespace task23_lab1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            double[] sides = Console.ReadLine().Split().Select(double.Parse).ToArray();

            if (sides.Length < 3)
            {
                Console.WriteLine("жодного трикутника не існує");
                return;
            }

            int n = sides.Length;
            int cnt = 0;

            //Array.Sort(sides);
            Sort(sides);

            for (int i = n - 1; i >= 2; i--)
            {
                double c = sides[i];
                int left = 0;
                int right = i - 1;

                while (left < right)
                {
                    double a = sides[left];
                    double b = sides[right];

                    if (a + b > c)
                    {
                        cnt += (right - left);
                        // Оскільки масив відсортований: sides[left] ≤ sides[right] ≤ sides[i] (тобто a ≤ b ≤ c),
                        // якщо sides[left] + sides[right] > sides[i] (тобто a + b > c),
                        // то автоматично всі пари (sides[left], sides[left+1]...sides[right-1]) з sides[right]
                        // також утворюють невироджені трикутники з sides[i],
                        // бо їх сума буде ще більшою: (left+1) + right > i, (left+2) + right > i і т.д.
                        // Тому одразу додаємо всі ці варіанти: count += (right - left)

                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
            }

            Console.WriteLine(cnt);
        }

        static void Sort(double[] arr)
        {
            int n = arr.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }

    }
}