using System;
using System.Collections.Generic;

namespace SudokuForce
{
    class Program
    {
        static void Main(string[] args)
        {
            AnalyticDict a = new AnalyticDict();
            DateTime dt = DateTime.Now;
            int n = 0;
            var sd = Sudoku.LoadFromFile("demo.txt");
            Console.WriteLine("Судоку:");
            Console.WriteLine(sd);
            Queue<Sudoku> qq = new Queue<Sudoku>();
            qq.Enqueue(sd);
            try
            {
                while (qq.Count > 0)
                {
                    n++;
                    var tmp = qq.Dequeue();
                    a[tmp.Generation]++;
                    if (tmp.IsGood)
                    {
                        Console.WriteLine($"Решение ({n} вариантов проверено):");
                        Console.WriteLine(tmp);
                        tmp.SaveToFile("rez.txt");
                        return;
                    }
                    foreach (Sudoku t in tmp.FillOne())
                        qq.Enqueue(t);
                }
                Console.WriteLine("Нет решения.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err - " + ex);
            }
            finally
            {
                Console.WriteLine((DateTime.Now - dt).TotalSeconds + " sec.");
                Console.WriteLine("Аналитика:");
                Console.WriteLine(a);
            }
        }
    }
}
