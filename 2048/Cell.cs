using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Cell
    {
        public int value;
        public bool isDoubled;

        public Cell(int value, bool isDoubled)
        {
            this.value = value;
            this.isDoubled = isDoubled;
        }
    }
}
