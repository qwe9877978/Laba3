using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabaW3
{
    internal class Program
    {
        //выполнение задания 1 - заполнение матриц различными способами
        private static void RunTask1()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 1: Заполнение матриц ===\n");

            Console.WriteLine("Матрица 1 (ввод с клавиатуры):");
            Console.Write("Строк: ");
            int n1 = int.Parse(Console.ReadLine());
            Console.Write("Столбцов: ");
            int m1 = int.Parse(Console.ReadLine());

            Matrix matrix1 = new Matrix(n1, m1, fillFromKeyboard: true);
            Console.WriteLine("\nРезультат:");
            Console.WriteLine(matrix1);

            Console.WriteLine("Матрица 2 (случайное заполнение):");
            Console.Write("Размер n: ");
            int n2 = int.Parse(Console.ReadLine());

            Matrix matrix2 = new Matrix(n2, randomFill: true);
            Console.WriteLine($"\nРезультат ({n2}x{n2}):");
            Console.WriteLine(matrix2);

            Console.WriteLine("Матрица 3 (нижняя треугольная):");
            Console.Write("Размер n: ");
            int n3 = int.Parse(Console.ReadLine());

            Matrix matrix3 = new Matrix(n3, 'L');
            Console.WriteLine($"\nРезультат ({n3}x{n3}):");
            Console.WriteLine(matrix3);

            Console.ReadKey();
        }

        //выполнение задания 2 - поиск максимального неповторяющегося элемента
        private static void RunTask2()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 2: Максимальный неповторяющийся элемент ===\n");

            Matrix testMatrix = new Matrix(3, 3, 0, 10);

            Console.WriteLine("Матрица:");
            Console.WriteLine(testMatrix);

            int? result = testMatrix.FindMaxNonRepeating();

            if (result.HasValue)
            {
                Console.WriteLine($"Ответ: {result.Value}");
            }
            else
            {
                Console.WriteLine("Нет неповторяющихся элементов");
            }

            Console.ReadKey();
        }

        //выполнение задания 3 - вычисление матричного выражения 2*A - 3*B*C
        private static void RunTask3()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 3: Вычисление 2*A - 3*B*C ===\n");

            Matrix A = new Matrix(2, 2, 1, 10);
            Matrix B = new Matrix(2, 2, 1, 10);
            Matrix C = new Matrix(2, 2, 1, 10);

            Console.WriteLine("Матрица A:");
            Console.WriteLine(A);
            Console.WriteLine("Матрица B:");
            Console.WriteLine(B);
            Console.WriteLine("Матрица C:");
            Console.WriteLine(C);

            Matrix result = 2 * A - 3 * B * C;
            Console.WriteLine("Результат (2*A - 3*B*C):");
            Console.WriteLine(result);

            Console.ReadKey();
        }

        //выполнение задания 6 - работа с файлом (числа по одному в строке)
        private static void RunTask6()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 6 ===\n");

            string filename = "task6.txt";

            Console.Write("Количество чисел (чётное): ");
            int count = int.Parse(Console.ReadLine());

            if (count % 2 != 0)
            {
                count++;
            }

            FileOperations.FillFileWithRandomNumbersOnePerLine(filename, count);
            Console.WriteLine($"Создан файл {filename}");

            int? diff = FileOperations.CalculateDifferenceFirstSecondHalf(filename);

            if (diff.HasValue)
            {
                Console.WriteLine($"Разность сумм: {diff.Value}");
            }

            Console.ReadKey();
        }

        //выполнение задания 7 - работа с файлом (несколько чисел в строке)
        private static void RunTask7()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 7 ===\n");

            string filename = "task7.txt";

            Console.Write("Всего чисел: ");
            int total = int.Parse(Console.ReadLine());

            Console.Write("Чисел в строке: ");
            int perLine = int.Parse(Console.ReadLine());

            FileOperations.FillFileWithRandomNumbersMultiplePerLine(filename, total, perLine);
            Console.WriteLine($"Создан файл {filename}");

            int sum = FileOperations.CalculateSumFromFile(filename);
            Console.WriteLine($"Сумма: {sum}");


            Console.ReadKey();
        }

        //выполнение задания 8 - поиск самой короткой и длинной строк в файле
        private static void RunTask8()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 8 ===\n");

            string sourceFile = "task8_source.txt";
            string resultFile = "task8_result.txt";


            if (File.Exists(sourceFile))
            {
                FileOperations.CopyShortestAndLongestLines(sourceFile, resultFile);
            }
            else
            {
                Console.WriteLine($"Файл {sourceFile} не найден!");
            }

            Console.ReadKey();
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА");
                Console.WriteLine("====================\n");
                Console.WriteLine("1. Задание 1 (заполнение матриц)");
                Console.WriteLine("2. Задание 2 (неповторяющийся элемент)");
                Console.WriteLine("3. Задание 3 (матричное выражение)");
                Console.WriteLine("4. Задание 6 (разность сумм)");
                Console.WriteLine("5. Задание 7 (сумма элементов)");
                Console.WriteLine("6. Задание 8 (короткая/длинная строки)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "4":
                        RunTask6();
                        break;
                    case "5":
                        RunTask7();
                        break;
                    case "6":
                        RunTask8();
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}