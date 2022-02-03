using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}
