using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day4
{
    class Task : BaseTask<int>
    {
        public override int ExpectedPart1Test { get; set; } = 4512;
        public override int ExpectedPart2Test { get; set; } = 1924;

        private class BingoItem
        {
            public int Value { get; set; }
            public bool IsDrawn { get; set; }
        }
        private class BingoBoard
        {
            public BingoItem[,] Board { get; set; } = new BingoItem[,] {
                { new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), },
                { new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), },
                { new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), },
                { new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), },
                { new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), new BingoItem(), },
            };

            public bool Finished { get; set; }

            public void CheckNumber(int number)
            {
                foreach (var item in Board)
                {
                    item.IsDrawn = item.IsDrawn || item.Value == number;
                }

            }

            public void SetLine(int line, string values)
            {
                values.Split(' ').Where(p => !string.IsNullOrEmpty(p)).Select(int.Parse).Select((value, index) => new { value, index }).ToList().ForEach(p =>
                {
                    Board[line, p.index].Value = p.value;
                });
            }

            public bool IsWin()
            {
                Finished = GetColumn(Board, 0).Count(p => p.IsDrawn) == 5
                    || GetColumn(Board, 1).Count(p => p.IsDrawn) == 5
                    || GetColumn(Board, 2).Count(p => p.IsDrawn) == 5
                    || GetColumn(Board, 3).Count(p => p.IsDrawn) == 5
                    || GetColumn(Board, 4).Count(p => p.IsDrawn) == 5
                    || GetRow(Board, 0).Count(p => p.IsDrawn) == 5
                    || GetRow(Board, 1).Count(p => p.IsDrawn) == 5
                    || GetRow(Board, 2).Count(p => p.IsDrawn) == 5
                    || GetRow(Board, 3).Count(p => p.IsDrawn) == 5
                    || GetRow(Board, 4).Count(p => p.IsDrawn) == 5;
                return Finished;
            }

            public void Print()
            {
                Console.WriteLine(string.Join(',', GetRow(Board, 0).Select(p => p.Value)));
                Console.WriteLine(string.Join(',', GetRow(Board, 1).Select(p => p.Value)));
                Console.WriteLine(string.Join(',', GetRow(Board, 2).Select(p => p.Value)));
                Console.WriteLine(string.Join(',', GetRow(Board, 3).Select(p => p.Value)));
                Console.WriteLine(string.Join(',', GetRow(Board, 4).Select(p => p.Value)));
            }

            private T[] GetColumn<T>(T[,] matrix, int columnNumber)
            {
                return Enumerable.Range(0, matrix.GetLength(0))
                        .Select(x => matrix[x, columnNumber])
                        .ToArray();
            }

            private T[] GetRow<T>(T[,] matrix, int rowNumber)
            {
                return Enumerable.Range(0, matrix.GetLength(1))
                        .Select(x => matrix[rowNumber, x])
                        .ToArray();
            }
        }
        public override int SolvePart1(IEnumerable<string> input)
        {
            var randomDrawns = input.ElementAt(0).Split(',').Select(int.Parse).ToList();

            var boards = new List<BingoBoard>();
            for (int i = 2; i + 5 <= input.Count(); i+=6)
            {
                var board = new BingoBoard();
                board.SetLine(0, input.ElementAt(i + 0));
                board.SetLine(1, input.ElementAt(i + 1));
                board.SetLine(2, input.ElementAt(i + 2));
                board.SetLine(3, input.ElementAt(i + 3));
                board.SetLine(4, input.ElementAt(i + 4));
                //board.Print();
                boards.Add(board);
            }
            foreach (var drawn in randomDrawns)
            {
                foreach (var board in boards)
                {
                    board.CheckNumber(drawn);
                    if (board.IsWin())
                    {
                        return (from BingoItem item in board.Board
                                 where !item.IsDrawn
                                 select item.Value).Sum() * drawn;
                    }
                }
            }

            throw new Exception("Bad input");
        }

        public override int SolvePart2(IEnumerable<string> input)
        {
            var randomDrawns = input.ElementAt(0).Split(',').Select(int.Parse).ToList();

            var boards = new List<BingoBoard>();
            for (int i = 2; i + 5 <= input.Count(); i += 6)
            {
                var board = new BingoBoard();
                board.SetLine(0, input.ElementAt(i + 0));
                board.SetLine(1, input.ElementAt(i + 1));
                board.SetLine(2, input.ElementAt(i + 2));
                board.SetLine(3, input.ElementAt(i + 3));
                board.SetLine(4, input.ElementAt(i + 4));
                //board.Print();
                boards.Add(board);
            }
            var lastBoardToWin = 0;
            foreach (var drawn in randomDrawns)
            {
                foreach (var board in boards)
                {
                    board.CheckNumber(drawn);
                    if (!board.Finished && board.IsWin())
                    {
                        lastBoardToWin = (from BingoItem item in board.Board
                                where !item.IsDrawn
                                select item.Value).Sum() * drawn;
                    }
                }
            }
            return lastBoardToWin;
        }
    }
}
