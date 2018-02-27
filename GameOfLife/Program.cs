using GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameOfLife2 game = new GameOfLife2();
            game.Start();
            Console.ReadLine();
        }
    }
}
