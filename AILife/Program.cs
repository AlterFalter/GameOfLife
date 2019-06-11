using ConwaysGameOfLife;
using System;
using YKArtificialIntelligence.SearchAlgorithms.GeneticAlgorithms.ValueTypes;

namespace AILife
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            GeneticAlgorithm<bool> gen = new GeneticAlgorithm<bool>(200, 7*7, random, RandomBool, 8,
                ParentSelector<bool>.SelectParentByRank, CalculateGenerations);

            for (int i = 0; i < 50; i++)
            {
                gen.NewGeneration();
                Console.WriteLine($"\nGeneration: {i+1} - {gen.BestFitness}");
                Console.ReadKey();
            }

            GameOfLife game = PrepareGame(gen.BestGenes, 35, 15);
            game.Start();
        }

        static double CalculateGenerations(bool[] field)
        {
            GameOfLife game = PrepareGame(field, 35, 15, 40, 80);
            bool[][] pitch = game.CurrentPitch;

            return game.Start();
        }

        static GameOfLife PrepareGame(bool[] field, int xPosition, int yPosition, int worldSizeX = 40, int worldSizeY = 80)
        {
            GameOfLife game = new GameOfLife(endless:true);

            int size = (int)Math.Sqrt((double)field.Length);

            for (int currentX = 0; currentX < size; currentX++)
            {
                for (int currentY = 0; currentY < size; currentY++)
                {
                    int newX = xPosition + currentX;
                    int newY = yPosition + currentY;
                    game.SetLife(newX, newY, field[size * currentY + currentX]);
                }
            }

            return game;
        }

        static bool RandomBool(Random random)
        {
            return random.Next(2) == 1;
        }
    }
}
