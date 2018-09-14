using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class BoardData
    {
        public static int[] board = new int[9];
        public static void StoreMove(string value, int id)
        {
            board[Convert.ToInt32(value)] = id;

        }

        public static bool CheckForWinner(int i)
        {
            
                if ((board[0] == i && board[1] == i && board[2] == i) || (board[3] == i && board[4] == i && board[5] == i) || (board[6] == i && board[7] == i && board[8] == i))
                {
                    return true;
                }
                else if ((board[0] == i && board[3] == i && board[6] == i) || (board[1] == i && board[4] == i && board[7] == i) || (board[2] == i && board[5] == i && board[8] == i))
                {
                    return true;
                }
                else if ((board[0] == i && board[4] == i && board[8] == i) || (board[2] == i && board[4] == i && board[6] == i))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
           
        }
    }
}
