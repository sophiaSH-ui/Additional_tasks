using System;

namespace task15_lab4
{
    internal class Program
    {
        static bool CheckedBrackets(string input)
        {
            int balance = 0;

            foreach (char c in input)
            {
                if (c == '(')
                {
                    balance++;
                }
                else if (c == ')')
                {
                    balance--;
                    if (balance < 0)
                    {
                        return false;
                    }
                }
            }

            return balance == 0;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            string input = Console.ReadLine();

            if (CheckedBrackets(input))
            {
                Console.WriteLine("Розстановка дужок правильна");
            }
            else
            {
                Console.WriteLine("Розстановка дужок неправильна");
            }
        }
    }
}
