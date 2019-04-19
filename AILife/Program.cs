﻿using ConwaysGameOfLife;
using System;
using YKArtificialIntelligence.SearchAlgorithms.GeneticAlgorithms.ValueTypes;

namespace AILife
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            GameOfLife game = new GameOfLife();

            GeneticAlgorithm<bool> gen = new GeneticAlgorithm<bool>(200, 3*3, random, RandomBool, 8,
                ParentSelector<bool>.SelectParentByRank, CalculateGenerations);

            for (int i = 0; i < 50; i++)
            {
                gen.NewGeneration();
            }

            var a = gen.BestGenes;
        }

        static double CalculateGenerations(bool[] field)
        {
            GameOfLife game = PrepareGame(field, 35, 15, 40, 80);
            bool[][] pitch = game.CurrentPitch;

            return game.Start();
        }

        static GameOfLife PrepareGame(bool[] field, int xPosition, int yPosition, int worldSizeX = 40, int worldSizeY = 80)
        {
            GameOfLife game = new GameOfLife();

            int size = (int)Math.Sqrt((double)field.Length);

            for (int currentX = 0; currentX < size; currentX++)
            {
                for (int currentY = 0; currentY < size; currentY++)
                {
                    int newX = xPosition + currentX;
                    int newY = yPosition + currentY;
                    game.SetLife(newX, newY, field[size * currentY]);
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
