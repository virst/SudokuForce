using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuForce
{
    /// <summary>
    /// Ячейка на поле СУДОКУ
    /// </summary>
    public class SudokuCell
    {
        private byte[,] _bb;
        public SudokuCell()
        {
            _bb = new byte[3, 3];
        }

        /// <summary>
        /// Получить или задать значение в ячейке
        /// </summary>
        /// <param name="a">Позиция по X</param>
        /// <param name="b">Позиция по Y</param>
        /// <returns>Значение в ячейке</returns>
        public char this[int a, int b]
        {
            get => _bb[a, b] == 0 ? '*' : _bb[a, b].ToString()[0];
            set => byte.TryParse(value.ToString(), out _bb[a, b]);
        }

        /// <summary>
        /// Задать значение в ячейке
        /// </summary>
        /// <param name="a">Позиция по X</param>
        /// <param name="b">Позиция по Y</param>
        /// <param name="bt">Число</param>
        public void SetNum(int a, int b, byte bt) => _bb[a, b] = bt;
        /// <summary>
        /// Получить значение в ячейке
        /// </summary> значение в ячейке
        /// <param name="a">Позиция по X</param>
        /// <param name="b">Позиция по Y</param>
        /// <returns>Число</returns>
        public byte GetNum(int a, int b) => _bb[a, b] ;

        /// <summary>
        /// Получить позицию первого "пустого" поля
        /// </summary>
        public (int a,int b) FirstClear
        {
            get
            {
                for (int a = 0; a < 3; a++)
                    for (int b = 0; b < 3; b++)
                        if (_bb[a, b] == 0) return (a, b);
                return (-1, -1);
            }
        }

        /// <summary>
        /// Получить все неиспользованные в ячейке числа
        /// </summary>
        public List<byte> AvalibleNums
        {
            get
            {
                List<byte> nums = new List<byte>();
                for (byte i = 0; i < 9;) nums.Add(++i);
                foreach (var b in _bb)
                {
                    if (nums.Contains(b)) 
                    nums.Remove(b);
                }
                return nums;
            }
        }

        /// <summary>
        /// Ячейка заполнена корректно
        /// </summary>
        public bool IsGood
        {
            get
            {
                List<byte> control = new List<byte>();
                for (byte i = 0; i < 9;) control.Add(++i);
                foreach(var b in _bb)
                {
                    if (!control.Contains(b)) return false;
                    control.Remove(b);
                }
                return true;
            }
        }

        /// <summary>
        /// Ячейка полностью заполнена
        /// </summary>
        public bool IsFull
        {
            get
            {
                foreach (var b in _bb) if (b == 0) return false;
                return true;
            }
        }
    }
}
