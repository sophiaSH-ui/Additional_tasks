namespace task22_lab1

{
    internal class Program
    {
        static double Area(double a, double b, double c)
        {
            double p = (a + b + c) / 2.0;
            double SqArea = p * (p - a) * (p - b) * (p - c);
            return Math.Sqrt(SqArea);
        }

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

            Array.Sort(sides);

            double maxArea = 0.0;
            double sideA = -1, sideB = -1, sideC = -1;
            bool triangleFound = false;

            for (int i = sides.Length - 1; i >= 2; i--)
            {
                double c = sides[i];
                double b = sides[i - 1];
                double a = sides[i - 2];

                if (a + b > c)
                {
                    maxArea = Area(a, b, c);
                    sideA = a;
                    sideB = b;
                    sideC = c;
                    triangleFound = true;
                    break; 
                }
            }


            if (triangleFound)
            {
                Console.WriteLine($"{sideA} {sideB} {sideC}");
                Console.WriteLine($"{maxArea}");
            }
            else
            {
                Console.WriteLine("жодного трикутника не існує");
            }
        }
    }
}
