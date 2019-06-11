using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        public int AmountRows { get; set; }
        public int AmountColumns { get; set; }
        public bool Endless { get; set; }
        public bool[][] CurrentPitch { get; set; }
        public bool[][] FuturePitch { get; set; }
        public LifeRules[] ActualLifeRules { get; set; }
        public List<bool[][]> AllGameStates { get; set; }

        public GameOfLife(int amountColumns = 80, int amountRows = 40, bool endless = false)
        {
            AmountColumns = amountColumns;
            AmountRows = amountRows;
            Endless = endless;
            if (Endless)
            {
                AmountColumns += 2;
                AmountRows += 2;
            }
            CurrentPitch = new bool[AmountRows][];
            FuturePitch = new bool[AmountRows][];
            AllGameStates = new List<bool[][]>();

            for (int y = 0; y < AmountRows; y++)
            {
                CurrentPitch[y] = new bool[AmountColumns];
                FuturePitch[y] = new bool[AmountColumns];

                for (int x = 0; x < AmountColumns; x++)
                {
                    SetLife(x, y, false);
                    SetFutureLife(x, y, false);
                }
            }

            ActualLifeRules = new LifeRules[9];
            ActualLifeRules[0] = LifeRules.Die;
            ActualLifeRules[1] = LifeRules.Die;
            ActualLifeRules[2] = LifeRules.LifesOn;
            ActualLifeRules[3] = LifeRules.EmergesLifeOrLifesOn;
            ActualLifeRules[4] = LifeRules.Die;
            ActualLifeRules[5] = LifeRules.Die;
            ActualLifeRules[6] = LifeRules.Die;
            ActualLifeRules[7] = LifeRules.Die;
            ActualLifeRules[8] = LifeRules.Die;
        }

        public int Start()
        {
            int generation = 0;

            while (HasStillLifeOnPitch())
            {
                string pitch = "";
                for (int y = 0; y < AmountRows; y++)
                {
                    for (int x = 0; x < AmountColumns; x++)
                    {
                        pitch += IsAlive(x, y) ? "X" : " ";
                        bool cellResult = CheckCell(x, y);
                        SetFutureLife(x, y, cellResult);
                    }
                    pitch += '\n';
                }

                Console.Clear();
                Console.Write(pitch);
                Console.Write(generation++);
                if (!AllGameStates.Any(s => s.SequenceEqual(CurrentPitch)))
                {
                    AllGameStates.Add(CurrentPitch);
                }
                else
                {
                    return generation;
                }
                Thread.Sleep(25);

                FuturePitch.CopyTo(CurrentPitch, 0);
            }

            return generation;
        }

        public bool HasStillLifeOnPitch()
        {
            return CurrentPitch.Any(y => y.Any(x => x));
        }

        public bool CheckCell(int x, int y)
        {
            int amountLivingNeighbours =
                BoolToInt(IsNeighbourAlive(x, y, -1, -1)) +
                BoolToInt(IsNeighbourAlive(x, y, 0, -1)) +
                BoolToInt(IsNeighbourAlive(x, y, 1, -1)) +
                BoolToInt(IsNeighbourAlive(x, y, -1, 0)) +
                BoolToInt(IsNeighbourAlive(x, y, 1, 0)) +
                BoolToInt(IsNeighbourAlive(x, y, -1, 1)) +
                BoolToInt(IsNeighbourAlive(x, y, 0, 1)) +
                BoolToInt(IsNeighbourAlive(x, y, 1, 1));

            bool oldValue = IsAlive(x, y);

            if (ActualLifeRules[amountLivingNeighbours] == LifeRules.LifesOn)
            {
                return oldValue;
            }
            else if (ActualLifeRules[amountLivingNeighbours] == LifeRules.EmergesLifeOrLifesOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNeighbourAlive(int x, int y, int xDifference, int yDifference)
        {
            int xEndPosition = x + xDifference;
            int yEndPosition = y + yDifference;

            if (Endless &&
                (xEndPosition == 0 ||
                 xEndPosition == AmountColumns - 1 ||
                 yEndPosition == 0 ||
                 yEndPosition == AmountRows - 1))
            {
                return false;
            }

            //////////////////////
            bool isOutOfField = (xEndPosition < 0 || xEndPosition >= AmountColumns) || (yEndPosition < 0 || yEndPosition >= AmountRows);
            return (!isOutOfField && IsAlive(xEndPosition, yEndPosition));
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public bool IsAlive(int x, int y)
        {
            return CurrentPitch[y][x];
        }

        public bool IsFutureAlive(int x, int y)
        {
            return FuturePitch[y][x];
        }

        public void CreateVerticalBlinker(int x, int y)
        {
            SetLife(x - 1, y, true);
            SetLife(x, y, true);
            SetLife(x + 1, y, true);
        }

        public void SetLife(int x, int y, bool value)
        {
            CurrentPitch[y][x] = value;
        }

        public void SetFutureLife(int x, int y, bool value)
        {
            FuturePitch[y][x] = value;
        }
    }
}
