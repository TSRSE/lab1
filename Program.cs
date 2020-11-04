using System;
using System.IO;
using System.Linq;

namespace ConsoleApp13
{
    class ReadingFromFile
    {
        public int[] SingleArrayFromFile()
        {
            string[] FileInput = File.ReadAllText(@"MyArrays\single.txt").Split(' ');
            int[] CurrentArray = new int[FileInput.Length];
            for (int i = 0; i < FileInput.Length; i++)
            {
                CurrentArray[i] = int.Parse(FileInput[i]);
            }
            return CurrentArray;
        }
        public int[,] DoubleArrayFromFile(int a)
        {
            int maxElements = 0;
            string dpath = "";
            switch (a)
            {
                case 1:
                    dpath = @"MyArrays\double1.txt";
                    break;
                case 2:
                    dpath = @"MyArrays\double2.txt";
                    break;
                default:
                    break;
            }
            string FileInput = File.ReadAllText(dpath);

            string[] Lines = FileInput.Replace("\n", "w").Split('w');
            //Счетчик
            for (int i = 0; i < Lines.Length; i++)
            {
                string[] elements = Lines[i].Split(' ');
                if (elements.Length > maxElements)
                {
                    maxElements = elements.Length;
                }
            }

            int[,] CurrentArray = new int[Lines.Length, maxElements];
            //Запись
            for (int i = 0; i < CurrentArray.GetLength(0); i++)
            {
                string[] elements = Lines[i].Split(' ');
                for (int j = 0; j < CurrentArray.GetLength(1); j++)
                {
                    try
                    {
                        CurrentArray[i, j] = int.Parse(elements[j]);
                    }
                    catch
                    {
                        CurrentArray[i, j] = 0;
                    }
                }
            }

            return CurrentArray;
        }
        public int[][] StepArrayFromFile()
        {
            string FileInput = File.ReadAllText(@"MyArrays\step.txt");
            string[] Lines = FileInput.Replace("\r\n", "w").Split('w');
            int[][] CurrentArray = new int[Lines.Length][];
            for (int i = 0; i < Lines.Length; i++)
            {
                string[] tmp = Lines[i].Split(' ');
                CurrentArray[i] = new int[tmp.Length];
                for (int j = 0; j < tmp.Length; j++)
                {
                    CurrentArray[i][j] = int.Parse(tmp[j]);
                }
            }

            return CurrentArray;
        }
    }
    class Program
    {
        public static void CheckKeyForMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\nОшибка!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Чтобы вернуться в ГЛАВНОЕ МЕНЮ нажмите ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("ESC");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n\nЧтобы повторить ввод, нажмите любую другую клавишу...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ResetColor();
            if ((int)Console.ReadKey().Key == 27)
                Menu();
        }
        static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Одномерный массив");
                Console.WriteLine("2) Одномерный массив (SystemArray)");
                Console.WriteLine("3) Двумерный массив");
                Console.WriteLine("4) \"Ступенчатый\" массив");
                ConsoleKey Choice = Console.ReadKey().Key;

                switch (Choice)
                {
                    case ConsoleKey.D1:
                        SingleArray();
                        break;
                    case ConsoleKey.D2:
                        SingleArray_SystemArray();
                        break;
                    case ConsoleKey.D3:
                        DoubleArray();
                        break;
                    case ConsoleKey.D4:
                        StepArray();
                        break;
                }
            }
        }
        static bool AskForInput(string ArrayName)
        {
            bool FromFile;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ввести данные для {0} из файла? [Y/n]", ArrayName);
                int key = (int)Console.ReadKey().Key;
                switch (key)
                {
                    case 89:
                        FromFile = true;
                        return FromFile;
                    case 78:
                        FromFile = false;
                        return FromFile;
                    case 27:
                        Menu();
                        break;
                    default:
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Лабораторная работа 1 | Сортировка массивов | Удалых Максим БПИ 20-9";
            if (!Directory.Exists(@"MyArrays")) { 
                Directory.CreateDirectory(@"MyArrays");
                File.WriteAllText(@"MyArrays\single.txt","-2 4 2 0");
                File.WriteAllText(@"MyArrays\double1.txt", "-2 4\n2 0");
                File.WriteAllText(@"MyArrays\double2.txt", "5 14 32\n0 11 13");
                File.WriteAllText(@"MyArrays\step.txt","2\n4 5\n0\n11 32 321 -0 14 -28");
            }
            Menu();
        }
        static void SingleArray()
        {
            int inMin = 0,inMax = 0;
            int Min, Max;

            Console.Clear();
            int[] SArray = new int[0];
            if (AskForInput("одномерного массива")) { 
                ReadingFromFile RFF = new ReadingFromFile();
                try { 
                    SArray = RFF.SingleArrayFromFile();
                }
                catch { Console.WriteLine("Не удалось считать из файла"); Menu(); }
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Введите массив, каждый элемент через пробел");
                    string[] CurrentArray = Console.ReadLine().Split(' ');
                    SArray = new int[CurrentArray.Length];
                    try
                    {
                        for (int i = 0; i < SArray.Length; i++)
                        {
                            SArray[i] = int.Parse(CurrentArray[i]);
                        }
                        break;
                    }
                    catch{ CheckKeyForMenu(); }
                }
            }
            Min = SArray[0];
            Max = SArray[0];
            //MinMax
            for (int i = 0; i < SArray.Length; i++)
            {
                if (Max < SArray[i])
                {
                    Max = SArray[i];
                    inMax = i;
                }
                if (Min >= SArray[i])
                {
                    Min = SArray[i];
                    inMin = i;
                }
            }
            //Output
            Console.Clear();
            Console.WriteLine("Массив");
            foreach (int element in SArray)
            {
                Console.Write("{0}\t", element);
            }

            Console.WriteLine("\n\nСамое большое число\nНомер:{2} \tИндекс:{0}\tЗначение:{1}", inMax, Max, inMax + 1);
            Console.WriteLine("\nСамое маленькое число\nНомер:{2} \tИндекс:{0}\tЗначение:{1}", inMin, Min, inMin + 1);

            for (int i = 0; i < SArray.Length; i++)
            {
                for (int j = i; j < SArray.Length; j++)
                {
                    if (SArray[i] > SArray[j])
                    {
                        int temp = SArray[i];
                        SArray[i] = SArray[j];
                        SArray[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n\nПо возрастанию");
            foreach (int element in SArray)
            {
                Console.Write("{0}\t", element);
            }

            for (int i = 0; i < SArray.Length; i++)
            {
                for (int j = i; j < SArray.Length; j++)
                {
                    if (SArray[i] < SArray[j])
                    {
                        int temp = SArray[i];
                        SArray[i] = SArray[j];
                        SArray[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n\nПо убыванию");
            foreach (int element in SArray)
            {
                Console.Write("{0}\t", element);
            }

            Console.WriteLine("\n\nЧетные");
            int n2 = 0;
            for (int i = 0; i < SArray.Length; i++)
            {
                if (SArray[i] % 2 == 0)
                    n2++;
            }
            if (n2 == 0)
                Console.WriteLine("В массиве не было четных чисел.");
            else
            {
                for (int i = 0; i < SArray.Length; i++)
                {
                    for (int j = i; j < SArray.Length; j++)
                    {
                        if (SArray[i] > SArray[j])
                        {
                            int temp = SArray[i];
                            SArray[i] = SArray[j];
                            SArray[j] = temp;
                        }
                    }
                }
                int[] SArray_even = new int[n2];
                int f = 0;
                for (int k = 0; k < SArray.Length; k++)
                {
                    if (SArray[k] % 2 == 0)
                    {
                        SArray_even[f] = SArray[k];
                        f++;
                    }
                }
                for (int i = 0; i < n2; i++)
                {
                    Console.Write("{0}\t", SArray_even[i]);
                }

            }
            #region e
            Console.WriteLine("\nНажминте любую клавишу для возвращения в меню");
            Console.ReadKey();
            Menu();
            #endregion
        }
        static void SingleArray_SystemArray()
        {
            Console.Clear();
            int[] SArray = new int[0];
            if (AskForInput("одномерного массива"))
            {
                ReadingFromFile RFF = new ReadingFromFile();
                try
                {
                    SArray = RFF.SingleArrayFromFile();
                }
                catch { Console.WriteLine("Не удалось считать из файла"); Menu(); }
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Введите массив, каждый элемент через пробел");
                    string[] CurrentArray = Console.ReadLine().Split(' ');
                    SArray = new int[CurrentArray.Length];
                    try
                    {
                        for (int i = 0; i < SArray.Length; i++)
                        {
                            SArray[i] = int.Parse(CurrentArray[i]);
                        }
                        break;
                    }
                    catch { CheckKeyForMenu(); }
                }
            }
            //Вывод
            Console.Clear();
            Console.WriteLine("Вывожу массив");
            foreach (var item in SArray)
            {
                Console.Write("{0}\t", item);
            }

            Console.WriteLine("\n\nМаксимальное значение: \nНомер:{2} \tИндекс:{0}\tЗначение:{1}", Array.IndexOf(SArray, SArray.Max()), SArray.Max(), Array.IndexOf(SArray, SArray.Max()) + 1);
            Console.WriteLine("\n\nМинимальное значение: \nНомер:{2} \tИндекс:{0}\tЗначение:{1}", Array.IndexOf(SArray, SArray.Min()), SArray.Min(), Array.IndexOf(SArray, SArray.Min()) + 1);

            Array.Sort(SArray);
            Console.WriteLine("\n\nСортировка по возрастанию");
            foreach (var item in SArray)
            {
                Console.Write("{0}\t", item);
            }

            Array.Reverse(SArray);
            Console.WriteLine("\n\nСортировка по убыванию");
            foreach (var item in SArray)
            {
                Console.Write("{0}\t", item);
            }

            Console.WriteLine("\n\nВывод четных чисел массива");
            Array.Sort(SArray);
            int counter = 0;//Сколько четных элементов
            for (int i = 0; i < SArray.Length; i++)
            {
                if (SArray[i] % 2 == 0)
                {
                    counter++;
                }
            }
            if (counter == 0)
                Console.WriteLine("В массиве нет четных чисел.");
            else//запись четных элементов
            {
                int[] a_sortByCodeArray_even = Array.FindAll(SArray, x => x % 2 == 0);
               
                // Вывод
                foreach (var item in a_sortByCodeArray_even)
                {
                    Console.Write("{0}\t", item);
                }

            }

            #region e
            Console.WriteLine("\nНажминте любую клавишу для возвращения в меню");
            Console.ReadKey();
            Menu();
            #endregion
        }
        static void DoubleArray()
        {
            
            Console.Clear();
            int[,] _Case(int Arr)
            {
                int[,] arr = new int[0,0];
                string which = "";
                switch (Arr)
                {
                    case 1:
                        which = "первого";
                        break;
                        case 2:
                        which = "второго";
                        break;
                    default:
                        break;
                }
                if (AskForInput(which + " двумерного массива"))
                {
                    ReadingFromFile RFF = new ReadingFromFile();
                    try
                    {
                        arr = RFF.DoubleArrayFromFile(Arr);
                    }
                    catch { Console.WriteLine("Не удалось считать из файла"); Menu(); }
                    return arr;
                }
                else
                {
                    string[] LineOfArray;
                    bool Answer = false;
                    int lines = 0, maxElements = 0;

                    while (!Answer)
                    {
                        Console.Clear();
                        Console.Write("Введите кол-во строк: ");
                        Answer = int.TryParse(Console.ReadLine(), out lines);
                        if (!Answer)
                        {
                            Console.WriteLine();
                            CheckKeyForMenu();
                        }
                    }
                    Answer = false;
                    while (!Answer)
                    {
                        Console.Clear();
                        Console.Write("\nВведите кол-во элементов: ");
                        Answer = int.TryParse(Console.ReadLine(), out maxElements);
                    }

                    LineOfArray = new string[lines];
                    arr = new int[lines, maxElements];

                    Console.Clear();
                    Console.WriteLine("\n\nМассив {0}x{1}", lines, maxElements);
                    Console.WriteLine("\nВводите строку массива\n");
                    for (int i = 0; i < lines; i++)
                    {
                        while (true)
                        {
                            Console.Write("{0} | ", i + 1);
                            string ThisLine = Console.ReadLine();
                            LineOfArray[i] = ThisLine;
                            if (ThisLine.Split(' ').Length != maxElements || !int.TryParse(ThisLine[0].ToString(), out int nothing))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Возникла ошибка, перепишите строку заново");
                                Console.ResetColor();
                                CheckKeyForMenu();
                            }
                            else break;
                        }
                    }
                    return arr;
                }
            }
            int[,] DArrayF = _Case(1);
            int[,] DArrayS = _Case(2);
            int max = 0, min = 0;
            int pillar_maxInd = 0, pillar_minInd = 0, l_maxInd = 0, l_minInd = 0;

            //Output
            Console.WriteLine("Вывожу массив 1");
            for (int i = 0; i < DArrayF.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < DArrayF.GetLength(1); j++)
                {
                    Console.Write("{0}\t", DArrayF[i, j]);
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nВывожу массив 2");
            for (int i = 0; i < DArrayS.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < DArrayS.GetLength(1); j++)
                {
                    Console.Write("{0}\t", DArrayS[i, j]);
                }
            }

            for (int i = 0; i < DArrayF.GetLength(0); i++)
            {
                for (int j = 0; j < DArrayF.GetLength(1); j++)
                {
                    if (max < DArrayF[i, j])
                    {
                        max = DArrayF[i, j];
                        l_maxInd = i;
                        pillar_maxInd = j;
                    }
                    if (min > DArrayF[i, j])
                    {
                        min = DArrayF[i, j];
                        l_minInd = i;
                        pillar_minInd = j;
                    }
                }
            }

            Console.WriteLine("\n\nМаксимальное значение в первом массиве: \nНомер: {3};{4} \tИндекс: {0};{1}\tЗначение: {2}", l_maxInd, pillar_maxInd, max, l_maxInd + 1, pillar_maxInd + 1);
            Console.WriteLine("\nМинимальное значение в первом массиве:  \nНомер: {3};{4} \tИндекс: {0};{1}\tЗначение: {2}", l_minInd, pillar_minInd, min, l_minInd + 1, pillar_minInd + 1);
            int summ_a1 = 0,
            summ_a2 = 0;
            int mult_a = 0;

            foreach (int el in DArrayF)
            {
                summ_a1 += el;
                mult_a *= el;
            }
            foreach (int el in DArrayS)
            {
                summ_a2 += el;
                mult_a *= el;
            }

            Console.WriteLine("\nПроизведение двух массивов: {0}\nРазность двух массивов: {1}\nСумма двух массивов: {2}", summ_a1 * summ_a2, summ_a1 - summ_a2, summ_a1 + summ_a2);

            Console.WriteLine("\n\nНажмите любую клавишу для возвращения в главное меню");
            Console.ReadKey();
            Menu();
        }
        static void StepArray()
        {
            int min = int.MaxValue, max = int.MinValue;
            Console.Clear();
            int[][] SArray = new int[0][];
            if (AskForInput("Ступенчатого"))
            {
                ReadingFromFile RFF = new ReadingFromFile();
                try { 
                    SArray = RFF.StepArrayFromFile();
                }
                catch { Console.WriteLine("Не удалось считать из файла"); Menu(); }
            }
            else
            {
                int Lines = 0;
                bool Answer = false;
                while (!Answer)
                {
                    Console.Clear();
                    Console.Write("Введите кол-во строк: ");
                    Answer = int.TryParse(Console.ReadLine(), out Lines);
                    if (!Answer)
                    {
                        Console.WriteLine();
                        CheckKeyForMenu();
                    }
                    break;
                }

                int[][] CurrentArray = new int[Lines][];
                for (int i = 0; i < Lines; i++)
                {
                    string[] tmp = {""};
                    bool Approwed = false;
                    
                    while (!Approwed)
                    {
                        bool Good = false;
                        Console.Clear();
                        Console.Write("\nВведите {0} строку через пробел: ", i + 1);
                        tmp = Console.ReadLine().Split(' ');
                        foreach (string El in tmp)
                        {
                            try { 
                                int.Parse(El);
                                Good = true;
                            }
                            catch
                            {
                                CheckKeyForMenu();
                                Good = false;
                                break;
                            }
                        }
                        if (Good)
                        {
                            Approwed = true;
                        }
                    }

                    CurrentArray[i] = new int[tmp.Length];
                    for (int j = 0; j < tmp.Length; j++)
                    {
                        CurrentArray[i][j] = int.Parse(tmp[j]);
                    }
                }
                SArray = CurrentArray;
            }
            // Я бы вас убил за такие циклы, но вы победили, потому что я уже убил самого себя за такие приколы.
            bool AskEditor()
            {
                while (true) {
                    Console.Clear();
                    Console.WriteLine("Хотите изменить элементы массива?[Y/n]");
                    int key = (int)Console.ReadKey().Key;
                    switch (key)
                    {
                        case 89://Y
                            return true;
                        case 78://N
                            return false;
                        case 27://ESC
                            Menu();
                            break;
                    }
                }
            }
            int[][] GetEdited(int[][] CuArray)
            {
                if (!AskEditor()) 
                    return CuArray;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nКакой массив вы хотите изменить?");
                    for (int i = 0; i < CuArray.Length; i++)
                    {
                        Console.Write("\n" + (i + 1) + ") ");
                        for (int j = 0; j < CuArray[i].Length; j++)
                            Console.Write("{0} ", CuArray[i][j]);

                        Console.Write("\n");
                    }
                    
                    bool Choice = int.TryParse(Console.ReadLine(),out int line);

                    if (Choice && line>0 && line <= CuArray.Length)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("\nКакой элемент вы хотите изменить?");
                            for (int i = 0; i < CuArray[line-1].Length; i++)
                                Console.Write("{0}\t",CuArray[line-1][i]);

                            Choice = int.TryParse(Console.ReadLine(), out int elementNum);
                            if (Choice && elementNum > 0 && elementNum <= CuArray[line-1].Length)
                            {
                                while (true)
                                {
                                    Console.WriteLine("Заменить {0} на ", CuArray[line-1][elementNum-1]);
                                    Choice = int.TryParse(Console.ReadLine(), out int newNum);
                                    if (Choice)
                                    {
                                        CuArray[line-1][elementNum-1] = newNum;
                                        return GetEdited(CuArray);
                                    }
                                    else
                                        CheckKeyForMenu();
                                }
                            }
                            else
                                CheckKeyForMenu();
                        }
                    }
                    else
                        CheckKeyForMenu();  
                }
            }
            
            SArray = GetEdited(SArray);
            Console.Clear();
            Console.WriteLine("Вывожу массив...");
            for (int i = 0; i < SArray.Length; i++)
            {
                Console.Write("\nМассив {0}\n", i + 1);
                for (int j = 0; j < SArray[i].Length; j++)
                {
                    Console.Write("{0}\t", SArray[i][j]);
                    if (min > SArray[i][j]) min = SArray[i][j];
                    if (max < SArray[i][j]) max = SArray[i][j];
                }
            }

            Console.WriteLine("\n\nМаксимальное значение: {0}\nМинимальное значение: {1}", max, min);
            Console.WriteLine("\nНажминте любую клавишу для возвращения в меню");
            Console.ReadKey();
            Menu();
        }
    }
}