using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SudokuForce
{
    public class Sudoku
    {
        public static Sudoku LoadFromFile(string fn)
        {
            Sudoku ret = new Sudoku();
            var ss = File.ReadAllLines(fn);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    ret[j / 3, i / 3][j % 3, i % 3] = ss[i][j];

            return ret;
        }

        private SudokuCell[,] _items;
        private int _generation = 0;
        public int Generation => _generation;

        public Sudoku()
        {
            _items = new SudokuCell[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _items[i, j] = new SudokuCell();
        }

        public SudokuCell this[int x, int y]
        {
            get => _items[x, y];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('-', 1 + 4 * 3));
            for (int y = 0; y < 3; y++) //y
            {
                for (int b = 0; b < 3; b++)
                {
                    sb.Append("|");
                    for (int x = 0; x < 3; x++)
                    {
                        if (x != 0) sb.Append("|");
                        for (int a = 0; a < 3; a++)
                        {
                            sb.Append(this[x, y][a, b]);
                        }
                    }
                    sb.AppendLine("|");
                }

                sb.AppendLine(new string('-', 1 + 4 * 3));
            }

            return sb.ToString();
        }

        public void SaveToFile(string fn, string separator = "\t")
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < 3; y++) //y
            {
                for (int b = 0; b < 3; b++)
                {
                    if (b != 0 || y != 0) sb.AppendLine();
                    for (int x = 0; x < 3; x++)
                    {
                        if (x != 0) sb.Append(separator);
                        for (int a = 0; a < 3; a++)
                        {
                            if (a != 0) sb.Append(separator);
                            sb.Append(this[x, y][a, b]);
                        }
                    }
                }
            }

            File.WriteAllText(fn, sb.ToString());
        }

        public Sudoku Clone()
        {
            Sudoku ret = new Sudoku();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    ret[j / 3, i / 3][j % 3, i % 3] = this[j / 3, i / 3][j % 3, i % 3];
            ret._generation = _generation + 1;
            return ret;
        }

        public List<Sudoku> FillOne()
        {
            List<Sudoku> ret = new List<Sudoku>();
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    if (this[x, y].IsFull) continue;
                    (int a, int b) = this[x, y].FirstClear;
                    var ans = this.AvalibleNums(x, y, a, b);
                    foreach (var bt in ans)
                    {
                        var c = this.Clone();
                        c[x, y].SetNum(a, b, bt);
                        ret.Add(c);
                    }
                    return ret;
                }
            return ret;
        }

        public List<byte> AvalibleNums(int x, int y, int a, int b)
        {
            var ans = this[x, y].AvalibleNums;
            for (int i = 0; i < 9; i++)
            {
                var c = this[x, i / 3].GetNum(a, i % 3);
                if (ans.Contains(c)) ans.Remove(c);
                c = this[i / 3, y].GetNum(i % 3, b);
                if (ans.Contains(c)) ans.Remove(c);
            }
            return ans;
        }

        public bool IsGood
        {
            get
            {
                foreach (var cl in _items)
                    if (!cl.IsGood) return false;
                List<char> cnt = new List<char>();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        var c = this[j / 3, i / 3][j % 3, i % 3];
                        if (cnt.Contains(c)) return false;
                    }
                    cnt.Clear();
                }
                for (int j = 0; j < 9; j++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        var c = this[j / 3, i / 3][j % 3, i % 3];
                        if (cnt.Contains(c)) return false;
                    }
                    cnt.Clear();
                }
                return true;
            }
        }
    }
}
