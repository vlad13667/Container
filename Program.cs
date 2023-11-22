using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int count;
        int n = 0;// количество предметов
        int M =0;// вместимость одного контейнера
        int[] m;
        bool flag = true;
        do
        {
            Console.WriteLine("Нажмите 1 для ручного ввода и 2 для рандомных значений");
            count = Convert.ToInt32(Console.ReadLine());
        } while (count != 1 && count != 2);
        if (count == 1)
            {

                Console.WriteLine("Введите количество предметов");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите вместимость одного контейнера ");
                M = Convert.ToInt32(Console.ReadLine());
                m = new int[n]; // массы предметов

                for (int i = 0; i < m.Length; i++)
                {
                    Console.WriteLine("Введите массу  предмета " + (i + 1));
                    m[i] = Convert.ToInt32(Console.ReadLine());
                }


            }else if(count == 2)


            {
                 n = generateRandInt(100, 200); 
                 M = generateRandInt(100, 200); 
                 m = new int[n]; // массы предметов

                for (int i = 0; i < m.Length; i++)
                {
                    m[i] = generateRandInt(1, M);
                }
               
            }
        else
        {
            m = new int[n]; // массы предметов
        }
       



        Console.WriteLine("количество предметов " + n);
        Console.WriteLine("вместимость одного контейнера " + M);
        BF(n, M, m);
        FFS(n, M, m);
            // enumeration(n, M, m);
        
    }
    
    public static int generateRandInt(int minValue, int maxValue)
    {
        Random r = new Random();
        return r.Next(minValue, maxValue);
    }

    static void BF(int n, int M, int[] m)
    {

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        List<List<int>> containers = new List<List<int>>(); // список контейнеров
        containers.Add(new List<int>()); // добавляем первый контейнер

        for (int i = 0; i < n; i++)
        {
            bool itemAdded = false; // флаг, указывающий, был ли предмет добавлен в контейнер

            // перебираем все контейнеры
            for (int j = 0; j < containers.Count; j++)
            {
                // проверяем, можно ли добавить предмет в текущий контейнер
                if (containers[j].Sum() + m[i] <= M)
                {
                    containers[j].Add(m[i]); // добавляем номер предмета в контейнер
                    itemAdded = true;
                    break;
                }
            }

            // если предмет не был добавлен в существующий контейнер, создаем новый контейнер
            if (!itemAdded)
            {
                containers.Add(new List<int> { m[i] });
            }
        }

        // выводим результаты
        Console.WriteLine("Количество использованных контейнеров: " + containers.Count);
        for (int i = 0; i < containers.Count; i++)
        {


            Console.WriteLine("Контейнер " + (i + 1) + ": " + string.Join(", ", containers[i].Select(x => x)));
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }
    static void FFS(int n, int M, int[] m)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        List<int> weights = new List<int>();

        for (int i = 0; i < n; i++)
        {

            int weight = m[i];
            weights.Add(weight);
        }

        // Сортировка предметов по убыванию массы
        weights.Sort();
        weights.Reverse();

        List<List<int>> containers = new List<List<int>>();
        containers.Add(new List<int>());

        foreach (int weight in weights)
        {
            bool placed = false;

            // Поиск контейнера, в котором предмет может быть размещен
            foreach (List<int> container in containers)
            {
                if (container.Sum() + weight <= M)
                {
                    container.Add(weight);
                    placed = true;
                    break;
                }
            }

            // Если ни один контейнер не подходит, создаем новый контейнер
            if (!placed)
            {
                List<int> newContainer = new List<int>();
                newContainer.Add(weight);
                containers.Add(newContainer);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Количество использованных контейнеров: " + containers.Count);

        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine("Предметы в контейнере " + (i + 1) + ": " + string.Join(", ", containers[i]));
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    
}





