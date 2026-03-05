using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaW3
{
        /// Класс для операций с текстовыми файлами 
    public static class FileOperations
    {
        //заполнение файла случайными целыми числами (по одному числу в строке)
        public static void FillFileWithRandomNumbersOnePerLine(string filename, int count)
        {
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(random.Next(-100, 101));
                }
            }
        }

        //вычисление разности между суммой первой и второй половины чисел в файле
        // Возвращает null, если количество элементов нечётное
        public static int? CalculateDifferenceFirstSecondHalf(string filename)
        {
            List<int> numbers = new List<int>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    int num;
                    if (int.TryParse(line, out num))
                    {
                        numbers.Add(num);
                    }
                }
            }

            if (numbers.Count % 2 != 0)
            {
                return null;
            }

            int mid = numbers.Count / 2;
            int sumFirst = 0;
            int sumSecond = 0;

            for (int i = 0; i < mid; i++)
            {
                sumFirst += numbers[i];
            }

            for (int i = mid; i < numbers.Count; i++)
            {
                sumSecond += numbers[i];
            }

            return sumFirst - sumSecond;
        }

        //заполнение файла случайными числами (несколько чисел в одной строке)
        public static void FillFileWithRandomNumbersMultiplePerLine(string filename, int totalNumbers, int perLine)
        {
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                int count = 0;

                while (count < totalNumbers)
                {
                    StringBuilder line = new StringBuilder();
                    int numbersInLine = Math.Min(perLine, totalNumbers - count);

                    for (int i = 0; i < numbersInLine; i++)
                    {
                        line.Append(random.Next(-100, 101));

                        if (i < numbersInLine - 1)
                        {
                            line.Append(" ");
                        }

                        count++;
                    }

                    writer.WriteLine(line.ToString());
                }
            }
        }

        //вычисление суммы всех целых чисел в файле
        public static int CalculateSumFromFile(string filename)
        {
            int sum = 0;

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string part in parts)
                    {
                        int num;
                        if (int.TryParse(part, out num))
                        {
                            sum += num;
                        }
                    }
                }
            }

            return sum;
        }

        //поиск самой короткой и самой длинной строк в файле и запись их в другой файл
        public static void CopyShortestAndLongestLines(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine($"Файл '{sourceFile}' не найден!");
                return;
            }

            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(sourceFile))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }

            if (lines.Count == 0)
            {
                Console.WriteLine("Файл пуст!");
                return;
            }

            string shortest = lines[0];
            string longest = lines[0];

            foreach (string line in lines)
            {
                if (line.Length < shortest.Length)
                {
                    shortest = line;
                }

                if (line.Length > longest.Length)
                {
                    longest = line;
                }
            }

            using (StreamWriter writer = new StreamWriter(destFile))
            {
                writer.WriteLine("САМАЯ КОРОТКАЯ СТРОКА:");
                writer.WriteLine(shortest);
                writer.WriteLine();
                writer.WriteLine("САМАЯ ДЛИННАЯ СТРОКА:");
                writer.WriteLine(longest);
            }

            Console.WriteLine($"Короткая: \"{shortest}\"");
            Console.WriteLine($"Длинная: \"{longest}\"");
            Console.WriteLine($"Результат в файле: {destFile}");
        }
    }

}
