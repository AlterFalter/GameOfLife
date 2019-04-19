using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife2
    {
        const int AmountRows = 60;
        const int AmountColumns = 237;

        public bool[][] CurrentPitch { get; set; }
        public bool[][] FuturePitch { get; set; }

        public GameOfLife2()
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
        }

        public void Start()
        {
            InitializeRandomPitch();
            //InitializePitch();
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
                Console.Write(pitch + (generation++));

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

        public void InitializePitch()
        {
            CreateVerticalBlinker(20, 20);
            CreateExploder(30, 30);
            CreateExploder(60, 10);
            CreateGlider(5, 5);
            CreateGlider(5, 15);
            CreateGlider(5, 25);
            CreateInfiniteBlinker(60, 25);
            CreateCreator(150, 30);
        }

        public void InitializeRandomPitch()
        {
            Random random = new Random();
            for (int y = 0; y < AmountRows; y++)
            {
                for (int x = 0; x < AmountColumns; x++)
                {
                    SetLife(x, y, IntToBool(random.Next(1, 3) - 1));
                }
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

            return (!IsAlive(x, y) && amountLivingNeighbours == 3) ||
                (IsAlive(x, y) && (amountLivingNeighbours == 2 || amountLivingNeighbours == 3));
        }

        public bool IsNeighbourAlive(int x, int y, int xDifference, int yDifference)
        {
            int xEndPosition = x + xDifference;
            int yEndPosition = y + yDifference;
            bool isOutOfField = IsOutOfPitch(xEndPosition, yEndPosition);
            return (!isOutOfField && IsAlive(xEndPosition, yEndPosition));
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static bool IntToBool(int value)
        {
            return value != 0;
        }

        public bool IsAlive(int x, int y)
        {
            return CurrentPitch[y][x];
        }

        public bool IsFutureAlive(int x, int y)
        {
            return FuturePitch[y][x];
        }

        public void SetLife(int x, int y, bool value)
        {
            if (!IsOutOfPitch(x, y))
            {
                CurrentPitch[y][x] = value;
            }
        }

        public void SetFutureLife(int x, int y, bool value)
        {
            FuturePitch[y][x] = value;
        }

        public bool IsOutOfPitch(int x, int y)
        {
            return (x < 0 || x >= AmountColumns) || (y < 0 || y >= AmountRows);
        }

        public void CreateVerticalBlinker(int x, int y)
        {
            SetLife(x - 1, y, true);
            SetLife(x, y, true);
            SetLife(x + 1, y, true);
        }

        public void CreateGlider(int x, int y)
        {
            SetLife(x - 1, y - 2, true);
            SetLife(x, y - 1, true);
            SetLife(x - 2, y, true);
            SetLife(x - 1, y, true);
            SetLife(x, y, true);
        }

        public void CreateExploder(int x, int y)
        {
            SetLife(x, y - 1, true);
            SetLife(x - 1, y, true);
            SetLife(x, y, true);
            SetLife(x + 1, y, true);
            SetLife(x - 1, y + 1, true);
            SetLife(x + 1, y + 1, true);
            SetLife(x, y + 2, true);
        }

        public void CreateInfiniteBlinker(int x, int y)
        {
            SetLife(x, y - 4, true);
            SetLife(x, y - 3, true);
            SetLife(x, y - 2, true);
            SetLife(x, y + 4, true);
            SetLife(x, y + 3, true);
            SetLife(x, y + 2, true);
            SetLife(x - 4, y, true);
            SetLife(x - 3, y, true);
            SetLife(x - 2, y, true);
            SetLife(x + 4, y, true);
            SetLife(x + 3, y, true);
            SetLife(x + 2, y, true);
        }

        public void CreateCreator(int x, int y)
        {
            SetLife(x, y, true);
            SetLife(x + 1, y - 2, true);
            SetLife(x + 1, y - 3, true);
            SetLife(x + 1, y + 2, true);
            SetLife(x + 1, y + 3, true);
            SetLife(x - 1, y - 3, true);
            SetLife(x - 1, y + 3, true);
            SetLife(x - 2, y + 2, true);
            SetLife(x - 2, y - 2, true);
            SetLife(x - 2, y - 1, true);
            SetLife(x - 2, y + 1, true);
            SetLife(x - 3, y + 1, true);
            SetLife(x - 3, y - 1, true);
            SetLife(x - 4, y, true);
            SetLife(x - 5, y, true);

            SetLife(x - 10, y, true);
            SetLife(x - 11, y, true);
            SetLife(x - 10, y + 1, true);
            SetLife(x - 11, y + 1, true);

            SetLife(x + 11, y, true);
            SetLife(x + 12, y, true);
            SetLife(x + 11, y - 1, true);
            SetLife(x + 12, y - 2, true);
            SetLife(x + 13, y - 2, true);
            SetLife(x + 13, y - 1, true);

            SetLife(x + 15, y - 2, true);
            SetLife(x + 16, y - 2, true);
            SetLife(x + 16, y - 3, true);
            SetLife(x + 17, y - 3, true);
            SetLife(x + 17, y - 1, true);

            SetLife(x + 23, y - 1, true);
            SetLife(x + 23, y - 2, true);
            SetLife(x + 24, y - 1, true);
            SetLife(x + 24, y - 2, true);
        }

        public void CreateFigure(List<int[]> coordinates, int i)
        {

        }
    }
}
