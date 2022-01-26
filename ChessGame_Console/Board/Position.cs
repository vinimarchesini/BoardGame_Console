using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class Position
    {
        public int Line { get; set; }
        public int Collum { get; set; }

        public Position(int line, int collum)
        {
            Line = line;
            Collum = collum;
        }

        public override string ToString()
        {
            return $"{Line}, {Collum}";
        }
    }
}
