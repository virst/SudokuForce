using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SudokuForce
{
    public class SudokuCell
    {
        private byte[,] _bb;
        public SudokuCell()
        {
            _bb = new byte[3, 3];
        }

        public char this[int a, int b]
        {
            get => _bb[a, b] == 0 ? '*' : _bb[a, b].ToString()[0];
            set => byte.TryParse(value.ToString(), out _bb[a, b]);
        }

        public void SetNum(int a, int b, byte bt) => _bb[a, b] = bt;
        public byte GetNum(int a, int b) => _bb[a, b] ;

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
