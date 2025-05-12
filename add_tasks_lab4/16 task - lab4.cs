using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab4_Task16
{
    internal class Program
    {
        static void Main(string[] args)
        {
         Start:
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Введіть рядок зі словами (через пробіл та/або табуляцію): ");
            string line = Console.ReadLine();
            string[] words = line.Split();

            Console.WriteLine("Введіть шаблон (без пробілів): ");
            string template = Console.ReadLine();

            Console.WriteLine("Оберіть яким чином ви хочете перевірити відповідність слів до шаблону: \n 1 - через регулярні рядки \n 2 - через написаний вручну код ");
            byte choice = byte.Parse(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    RegexTemp(words, template);
                    break;
                case 2:
                    Manualy(words, template);
                    break;
                default:
                    Console.WriteLine("Такого вибору не існує, cпробуйте ще раз\r\nНатисніть Enter...");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
            }
                
        }

        private static void RegexTemp(string[] words, string template)
        {
            string regexTemp = "^"; // позначання початку (у нашрму випадку спочатку слова) 

            foreach (char el in template)
            {
                if (el == '?')
                    regexTemp += "."; // крапка в регулярних виразах означає будь-який елемент

                else if(el == '*')
                    regexTemp += ".*";// така комбінація значень у регулярному виразі буде означати - будь яка клю значень бцдь якої довжини
                                      
                else
                    regexTemp += Regex.Escape(el.ToString()); // Цей метод буде запобігати розпізнаванню інших знаків які щось значать у регулярному виразі + звичайні літери та цифри просто перетворюватиме на стрінг  
            }

            regexTemp += "$"; // долар для регулярного виразу означає закінчення кінцем слова (у даному випадку)
            Regex regex = new Regex(regexTemp);

            bool foundWord = false;
            string res = "";

            foreach(string word in words)
            {
                if(regex.IsMatch(word))
                {
                    res += word + " ";
                    foundWord = true;
                }
            }

            if(foundWord)
            { 
                Console.WriteLine($"""
                    Слова, що відповідають шаблону:
                    {res}
                    """); 
            }
            else
                Console.WriteLine("Не знайдено слів, які б відповідали шаблону.");


        }

        private static void Manualy(string[] words, string template)
        {
            bool foundWord = false;
            bool forFirstPrint = false;

            foreach (string word in words)
            {
                if (Check(word, template))
                {
                    if (!forFirstPrint)
                    {
                        Console.Write("Слова, що відповідають шаблону: ");
                        forFirstPrint = true;
                    }
                    Console.Write(word + " ");
                    foundWord = true;
                }
            }
            if (forFirstPrint)
            {
                Console.WriteLine();
            }
        }

        private static bool Check(string word, string template)
        {
            if (template.Length == word.Length && !template.Contains("*")) // Contаins - це мктод для str який перевіряє чи є данна строка (те що в дужках) в хагальній строкі (те що перед крапкою) 
            {
                return CheckIfLengthTheSameNoStar(word, template);
            }
            else
            {
                return CheckWithWildcards(word, template, 0, 0);
            }
        }

        private static bool CheckIfLengthTheSameNoStar(string word, string template)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char tempL = template[i];
                char c = word[i];

                if (tempL == '?')
                {
                    continue;
                }
                if (tempL != c)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckWithWildcards(string word, string template, int wordIdx, int templateIdx)
        {
            if (templateIdx == template.Length)
            {
                return wordIdx == word.Length;
            }

            if (wordIdx == word.Length)
            {
                if (template[templateIdx] == '*')
                {
                    return CheckWithWildcards(word, template, wordIdx, templateIdx + 1);
                }
                return false;
            }

            char pChar = template[templateIdx];
            char wChar = word[wordIdx];

            if (pChar == '?')
            {
                return CheckWithWildcards(word, template, wordIdx + 1, templateIdx + 1);
            }
            else if (pChar == '*')
            {
                return CheckWithWildcards(word, template, wordIdx, templateIdx + 1) || CheckWithWildcards(word, template, wordIdx + 1, templateIdx);
            }
            else if (pChar == wChar)
            {
                return CheckWithWildcards(word, template, wordIdx + 1, templateIdx + 1);
            }
            else
            {
                return false;
            }
        }
    }

}
