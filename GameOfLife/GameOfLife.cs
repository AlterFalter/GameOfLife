using System;
using System.Linq;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        const int AmountRows = 40;
        const int AmountColumns = 80;

        public bool[][] CurrentPitch { get; set; }
        public bool[][] FuturePitch { get; set; }
        public LifeRules[] ActualLifeRules { get; set; }

        public GameOfLife()
        {
            CurrentPitch = new bool[AmountRows][];
            FuturePitch = new bool[AmountRows][];
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
            ActualLifeRules[2] = LifeRules.EmergesLifeOrLifesOn;
            ActualLifeRules[3] = LifeRules.EmergesLifeOrLifesOn;
            ActualLifeRules[4] = LifeRules.Die;
            ActualLifeRules[5] = LifeRules.Die;
            ActualLifeRules[6] = LifeRules.Die;
            ActualLifeRules[7] = LifeRules.Die;
            ActualLifeRules[8] = LifeRules.Die;
        }

        public void Start()
        {
            CreateVerticalBlinker(20, 20);
            long generation = 0;

            while (HasStillLifeOnPitch())
            {
                string pitch = "";
                for (int y = 0; y < AmountRows; y++)
                {
                    for (int x = 0; x < AmountColumns; x++)
                    {
                        pitch += BoolToInt(IsAlive(x, y)) == 1 ? "X" : " ";
                        bool cellResult = CheckCell(x, y);
                        SetFutureLife(x, y, cellResult);
                    }
                }

                Console.Clear();
                Console.Write(pitch);
                Console.Write(generation++);

                for (int y = 0; y < AmountRows; y++)
                {
                    for (int x = 0; x < AmountColumns; x++)
                    {
                        SetLife(x, y, IsFutureAlive(x, y));
                    }
                }
                
                Thread.Sleep(50);
            }
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
            
            return (ActualLifeRules[amountLivingNeighbours] == LifeRules.EmergesLifeOrLifesOn);
        }

        public bool IsNeighbourAlive(int x, int y, int xDifference, int yDifference)
        {
            int xEndPosition = x + xDifference;
            int yEndPosition = y + yDifference;
            bool isOutOfField = (xEndPosition < 0 || xEndPosition >= AmountColumns) || (yEndPosition < 0 || yEndPosition >= AmountRows);
            return (!isOutOfField && IsAlive(xEndPosition, yEndPosition));
        }

        public static int BoolToInt(bool value)
        {
            return value? 1 : 0;
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
