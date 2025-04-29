using System;

namespace task21_lab1
{
    internal class Program
    {
        static int[] ReadAndSortInput()
        {
            string line = Console.ReadLine();

            int[] numbers = line.Split().Select(int.Parse).Where(n => n > 0).ToArray(); // .Where(n => n > 0) вбудований метод з бібліотеки linq що допомогає відсіяти елементи (у нашому випадку ті які менше нуля)

            Array.Sort(numbers);
            return numbers;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Введіть ширини прогалин через пробіл:");
            int[] gaps = ReadAndSortInput();
            Console.WriteLine("Введіть ширини мостів через пробіл:");
            int[] bridges = ReadAndSortInput();

            int coverGapsCnt = 0;
            int bridgeIndex = 0;

            for (int i = 0; i < gaps.Length; i++)
            {
                int currentGap = gaps[i];

                while (bridgeIndex < bridges.Length && bridges[bridgeIndex] <= currentGap)
                {
                    bridgeIndex++;
                }

                if (bridgeIndex < bridges.Length)
                {
                    coverGapsCnt++;
                    bridgeIndex++;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Кількість прогалин, які можна перекрити: ");
            Console.WriteLine(coverGapsCnt);
        }
    }
}
