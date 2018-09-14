using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class IRepository
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public int Id { get; set; }
        public bool CurrentStatus { get; set; }
    }
}
