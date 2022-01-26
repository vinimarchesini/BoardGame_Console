﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame_Console
{
    class Screen
    {
        public static void PrintBoard(Board.Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Collums; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    Console.Write(board.Piece(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}