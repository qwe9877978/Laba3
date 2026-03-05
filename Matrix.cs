using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaW3
{

    /// Класс для работы с матрицами

    public class Matrix
    {
        private int[,] _data;

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        //  создание матрицы с вводом элементов с клавиатуры
        // Заполнение происходит по строкам от последних элементов к первым
        public Matrix(int n, int m, bool fillFromKeyboard = false)
        {
            Rows = n;
            Columns = m;
            _data = new int[n, m];

            if (fillFromKeyboard)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = m - 1; j >= 0; j--)
                    {
                        Console.Write($"Элемент [{i},{j}]: ");

                        int value;
                        while (!int.TryParse(Console.ReadLine(), out value))
                        {
                            Console.Write("Ошибка! Введите число: ");
                        }

                        _data[i, j] = value;
                    }
                }
            }
        }

        //  создание квадратной матрицы со случайным заполнением
        // Элементы ниже главной диагонали: [-17; 36]
        // Элементы на и выше главной диагонали: [100; 10000]
        public Matrix(int n, bool randomFill = false)
        {
            Rows = n;
            Columns = n;
            _data = new int[n, n];

            if (randomFill)
            {
                Random random = new Random();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j < i)
                        {
                            _data[i, j] = random.Next(-17, 37);
                        }
                        else
                        {
                            _data[i, j] = random.Next(100, 10001);
                        }
                    }
                }
            }
        }

        //  создание нижней треугольной матрицы по образцу
        // Элементы заполняются последовательно по столбцам
        public Matrix(int n, char pattern)
        {
            Rows = n;
            Columns = n;
            _data = new int[n, n];

            if (pattern == 'L')
            {
                int value = 1;

                for (int col = 0; col < n; col++)
                {
                    for (int row = col; row < n; row++)
                    {
                        _data[row, col] = value++;
                    }
                }
            }
        }

        //  создание матрицы из существующего двумерного массива
        public Matrix(int[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            _data = new int[Rows, Columns];
            Array.Copy(array, _data, array.Length);
        }

        //  создание матрицы со случайными числами в заданном диапазоне
        public Matrix(int rows, int cols, int minValue, int maxValue)
        {
            Rows = rows;
            Columns = cols;
            _data = new int[rows, cols];

            Random random = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _data[i, j] = random.Next(minValue, maxValue + 1);
                }
            }
        }

        //поиск максимального элемента, который встречается в матрице только один раз
        // Возвращает null, если все элементы повторяются
        public int? FindMaxNonRepeating()
        {
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    int val = _data[i, j];

                    if (frequency.ContainsKey(val))
                    {
                        frequency[val]++;
                    }
                    else
                    {
                        frequency[val] = 1;
                    }
                }
            }

            int? maxNonRepeating = null;

            foreach (KeyValuePair<int, int> kvp in frequency)
            {
                if (kvp.Value == 1)
                {
                    if (maxNonRepeating == null)
                    {
                        maxNonRepeating = kvp.Key;
                    }
                    else if (kvp.Key > maxNonRepeating)
                    {
                        maxNonRepeating = kvp.Key;
                    }
                }
            }

            return maxNonRepeating;
        }

        // Перегрузка оператора сложения матриц
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Матрицы должны иметь одинаковые размеры");
            }

            int[,] result = new int[a.Rows, a.Columns];

            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    result[i, j] = a._data[i, j] + b._data[i, j];
                }
            }

            return new Matrix(result);
        }

        // Перегрузка оператора вычитания матриц
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Матрицы должны иметь одинаковые размеры");
            }

            int[,] result = new int[a.Rows, a.Columns];

            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    result[i, j] = a._data[i, j] - b._data[i, j];
                }
            }

            return new Matrix(result);
        }

        // Перегрузка оператора умножения матриц
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
            {
                throw new ArgumentException("Неверные размеры матриц");
            }

            int[,] result = new int[a.Rows, b.Columns];

            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < b.Columns; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < a.Columns; k++)
                    {
                        result[i, j] += a._data[i, k] * b._data[k, j];
                    }
                }
            }

            return new Matrix(result);
        }

        // Перегрузка оператора умножения матрицы на скаляр
        public static Matrix operator *(int scalar, Matrix m)
        {
            int[,] result = new int[m.Rows, m.Columns];

            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    result[i, j] = scalar * m._data[i, j];
                }
            }

            return new Matrix(result);
        }

        // Перегрузка оператора умножения скаляра на матрицу
        public static Matrix operator *(Matrix m, int scalar)
        {
            return scalar * m;
        }

        // Перегрузка метода ToString: форматированный вывод матрицы в виде таблицы
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.Append(_data[i, j].ToString().PadLeft(6));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
