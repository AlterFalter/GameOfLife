using System;

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
