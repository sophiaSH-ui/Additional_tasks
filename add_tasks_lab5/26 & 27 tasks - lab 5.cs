using Microsoft.VisualBasic;
using System;
using System.Runtime.CompilerServices;

namespace tasks26and27_lab5 
{
    internal class Program
    {
        static void Text()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
        }
        struct Student
        { 
            public string name;
            public string lastname;
            public string thirdname;
            public char sex;
            public string birthday;
            public string[] points;
            public int salary;

        }

        private static int InformationTransfer(string[] input, Student[] st, int index)
        {
            for (int i = 0; i < input.Length;)
            {
                Student s = new Student();
                s.lastname = input[i++];
                s.name = input[i++];
                s.thirdname = input[i++];
                s.sex = input[i++][0];
                s.birthday = input[i++];

                s.points = new string[3];

                for (int j = 0; j < 3; j++)
                {
                    string line = input[i++];
                    s.points[j] = line;
                }

                s.salary = int.Parse(input[i++]);
                st[index++] = s;
            }

            return index;
        }

        static void Main(string[] args)
        {
            Text();
            string[] input = File.ReadAllLines("code.txt");
            Student[] st = new Student[input.Length / 9];
            int index = 0;
            index = InformationTransfer(input, st, index);


            Console.WriteLine("""
                Оберіть програму, яку ви хочете виконати:

                1 - задача 26 (Вивести прізвища, імена, по батькові студенток жіночої статі,
                які мають середній бал строго більший, чим середній бал серед студентів чоловічої статі.)

                2 - задача 27 (Знайти і, якщо є, вивести, погрупувавши, дані про студентів,
                народжених в один день одного року, а також (окремо, теж погрупувавши і теж якщо такі є) 
                студентів (можливо, різних років народження), що мають день народження в один день.)
                """);

            byte choice = byte.Parse(Console.ReadLine());
            Console.WriteLine("Натисніть Enter...");
            Console.ReadKey();
            Console.Clear();

            switch (choice)
            {
                case 1:
                    Task26(st, index);
                    break;
                case 2:
                    Task27(st);
                    break;
                default:
                    Console.WriteLine("Ну, не хочеш по нормальному, ладно. ");
                    break;

            }

           

        }

        

        private static string[] BirthDay(Student[] st)
        {
            string[] dateBirth = new string[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                dateBirth[i] = st[i].birthday;
            }
            return dateBirth;
        }

        private static void Task26(Student[] st, int index)
        {
            int[] gender = WHO(st);
            string[] fullname = FullNames(st);
            int[,] allPoints = PointsArr(st);
            double[] averageEach = Average(allPoints);
            double guysAv = AllGuysAverage(gender, averageEach);
            
            if(guysAv == 0)
            {
                Console.WriteLine("Не має жодного знайденого хлопця.");
                return;
            }

            GirlsBest(st, gender, averageEach, guysAv, fullname);
        }

        private static void GirlsBest(Student[] st, int[] gender, double[] averageEach, double guysAv, string[] fullname)
        {
            bool flag = false;

            Console.WriteLine($"--- Дівчата, які мають локальний середній бал вище ніж загальний чоловічий середній бал ({guysAv}) ---");
            Console.WriteLine();
            for (int i = 0; i < st.Length; i++)
            {
                if (gender[i] == 0 && averageEach[i] > guysAv)
                {
                    flag = true;
                    Console.WriteLine($"""
                        ~ {fullname[i]} має бал: {averageEach[i]}
                        """);

                }
            }
            if (!flag)
            {
                Console.WriteLine("Жодної такої дівчини не знайдено.");
            }
        }

        private static string[] FullNames(Student[] st)
        {
            string[] fullname = new string[st.Length];

            for (int i = 0; i < st.Length; i++)
            {
                fullname[i] = st[i].lastname +" "+ st[i].name +" "+ st[i].thirdname;
            }
            return fullname;
        }

        private static double AllGuysAverage(int[] gender, double[] averageEach)
        {
            double sum_men = 0; int cnt_men = 0;

            for (int i = 0; i < gender.Length; i++)
            {
                if (gender[i] == 1)
                {
                    sum_men += averageEach[i];
                    cnt_men++;
                }
            }
            if(cnt_men ==0)
            {
                return 0;
            }
            return sum_men / cnt_men;
        }

        private static int[] WHO(Student[] st)
        {
            int[] who = new int[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                if (st[i].sex == 'W' || st[i].sex == 'Ж')
                    who[i] = 0;

                else if (st[i].sex == 'М' || st[i].sex == 'Ч' || st[i].sex == 'M')
                    who[i] = 1;
            }
            return who;
        }

        private static int[,] PointsArr(Student[] st)
        {
            int[,] points = new int[st.Length, 3];

            for (int i = 0; i < st.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (st[i].points[j] == "-")
                        points[i, j] = 2;
                    else
                        points[i, j] = int.Parse(st[i].points[j]);
                }
            }
            return points;
        }

        private static double[] Average(int[,] points)
        {
            double[] average = new double[points.GetLength(0)];

            for (int i = 0; i < points.GetLength(0); i++)
            {
                average[i] = Math.Round((points[i, 0] + points[i, 1] + points[i, 2]) / 3.0, 1);
            }
            return average;
        }

        private static void Task27(Student[] st)
        {
            string[] date = BirthDay(st);
            string[] fullname = FullNames(st);

            Console.WriteLine("--- Студенти, народжені в один день одного року ---");
            PrintGroupsByFullDate(date, fullname);

            Console.WriteLine("\n--- Студенти, що мають день народження в один день (можливі як різні, так і однакові роки) ---");
            PrintGroupsByDayMonth(date, fullname);
        }

        private static string GetDayMonth(string fullBirthday)
        {
            string[] parts = fullBirthday.Split('.', 3);
            if (parts.Length >= 2)
                return $"{parts[0]}.{parts[1]}";

            return string.Empty;
        }

        private static void PrintGroupsByFullDate(string[] date, string[] fullname)
        {
            bool[] processed = new bool[date.Length];
            bool foundAnyGroup = false;

            for (int i = 0; i < date.Length; i++)
            {
                if (processed[i]) continue;

                int matchCount = 0;
                for (int j = i + 1; j < date.Length; j++)
                {
                    if (!processed[j] && date[i] == date[j])
                    {
                        matchCount++;
                    }
                }

                if (matchCount > 0)
                {
                    foundAnyGroup = true;
                    Console.WriteLine($"\nНароджені {date[i]}:");
                    Console.WriteLine($"- {fullname[i]}");
                    processed[i] = true;

                    for (int j = i + 1; j < date.Length; j++)
                    {
                        if (!processed[j] && date[i] == date[j])
                        {
                            Console.WriteLine($"- {fullname[j]}");
                            processed[j] = true;
                        }
                    }
                }
            }

            if (!foundAnyGroup)
            {
                Console.WriteLine("Не знайдено груп студентів, народжених в один день одного року.");
            }
        }

        private static void PrintGroupsByDayMonth(string[] date, string[] fullname)
        {
            bool[] processedDayMonth = new bool[date.Length];
            bool foundAnyGroup = false;

            for (int i = 0; i < date.Length; i++)
            {
                string dayMonthI = GetDayMonth(date[i]);
                if (processedDayMonth[i]) continue;

                int matchCount = 0;
                for (int j = i + 1; j < date.Length; j++)
                {
                    if (!processedDayMonth[j])
                    {
                        string dayMonthJ = GetDayMonth(date[j]);
                        if (dayMonthI == dayMonthJ)
                        {
                            matchCount++;
                        }
                    }
                }

                if (matchCount > 0)
                {
                    foundAnyGroup = true;
                    Console.WriteLine($"\nДень народження {dayMonthI}:");
                    Console.WriteLine($"- {fullname[i]} (дата: {date[i]})");
                    processedDayMonth[i] = true;

                    for (int j = i + 1; j < date.Length; j++)
                    {
                        if (!processedDayMonth[j])
                        {
                            string dayMonthJ = GetDayMonth(date[j]);
                            if (dayMonthI == dayMonthJ)
                            {
                                Console.WriteLine($"- {fullname[j]} (дата: {date[j]})");
                                processedDayMonth[j] = true;
                            }
                        }
                    }
                }
            }

            if (!foundAnyGroup)
            {
                Console.WriteLine("Не знайдено груп студентів з однаковим днем народження (різні роки).");
            }
        }


    }
}
