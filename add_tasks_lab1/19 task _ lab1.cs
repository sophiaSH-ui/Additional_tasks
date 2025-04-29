using System;

namespace task19_lab1
{
    internal class Program
    {
        public class add_task_1_19
        {
            public static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.InputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("Введіть три цілих числа через пробіл (наприклад, 2 3 5 або 1 5 1):");
                string inputLine = Console.ReadLine();

                string[] parts = inputLine.Split(' ');

                int[] nums = new int[3];

                nums[0] = int.Parse(parts[0]);
                nums[1] = int.Parse(parts[1]);
                nums[2] = int.Parse(parts[2]);

                Array.Sort(nums);

                Console.WriteLine("\nУсі різні перестановки:");

                bool permFlag;
                do
                {
                    PrintArray(nums);
                    permFlag = FindNextPermutation(nums);
                }
                while (permFlag);
            }

            private static void PrintArray(int[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i]);
                    if (i < arr.Length - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            private static bool FindNextPermutation(int[] nums)
            {
                int n = nums.Length;

                int k = -1;
                for (int i = n - 2; i >= 0; i--)
                {
                    if (nums[i] < nums[i + 1])
                    {
                        k = i;
                        break;
                    }
                }

                if (k == -1)
                {
                    return false;
                }

                int l = k + 1;
                for (int i = n - 1; i > k; i--)
                {
                    if (nums[k] < nums[i])
                    {
                        l = i;
                        break;
                    }
                }

                Swap(nums, k, l);

                Reverse(nums, k + 1, n - 1);

                return true;
            }

            private static void Swap(int[] arr, int i, int j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }

            private static void Reverse(int[] arr, int start, int end)
            {
                while (start < end)
                {
                    Swap(arr, start, end);
                    start++;
                    end--;
                }
            }
        }
    }
}
