using System;
using System.Collections.Generic;
using System.Text;
using Board;

namespace Chess
{
    class ChessPosition
    {
        public char Collum { get; set; }
        public int Line { get; set; }

        public ChessPosition(char collum, int line)
        {
            Collum = collum;
            Line = line;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Collum - 'a');
        }
        public override string ToString()
        {
            return "" + Collum + Line;
        }
    }
}
