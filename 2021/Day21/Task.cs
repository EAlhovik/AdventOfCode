using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day21
{
    class Task : BaseTask<long>
    {
        public override long ExpectedPart1Test { get; set; } = 739785;

        public override long ExpectedPart2Test { get; set; } = 444356092776315;

        public override long SolvePart1(IEnumerable<string> input)
        {
            var stsartingPositions = input.Select(p => p.Split(':')[1].Trim())
                .Select(int.Parse)
                .ToList();

            var player1Score = 0;
            var player2Score = 0;
            var player1Position = stsartingPositions[0];
            var player2Position = stsartingPositions[1];

            var rolls = 0;
            var dieValue = 0;
            int? loosingScore = null;
            while (loosingScore == null)
            {
                rolls += 3;
                player1Position = (player1Position + ++dieValue + ++dieValue + ++dieValue) % 10;
                player1Position = player1Position == 0 ? 10 : player1Position;
                player1Score += player1Position;
                if (player1Score >= 1000)
                {
                    loosingScore = player2Score;
                    break;
                }

                rolls += 3;
                player2Position = (player2Position + ++dieValue + ++dieValue + ++dieValue) % 10;
                player2Position = player2Position == 0 ? 10 : player2Position;
                player2Score += player2Position;
                if (player2Score >= 1000)
                {
                    loosingScore = player1Score;
                    break;
                }

            }
            return rolls * loosingScore.Value;
        }

        public override long SolvePart2(IEnumerable<string> input)
        {
            var stsartingPositions = input.Select(p => p.Split(':')[1].Trim())
                .Select(int.Parse)
                .ToList();

            var player1 = new Player { Position = stsartingPositions.First(), Wins = new Wins() };
            var player2 = new Player { Position = stsartingPositions.Last(), Wins = new Wins() };

            ComputeWins(new[] { player1, player2 }, true, 1);
            return Math.Max(player1.Wins.Value, player2.Wins.Value);
        }

        private Dictionary<int, int> possibleDieThrows = new Dictionary<int, int>
        {
            {3, 1},
            {4, 3},
            {5, 6},
            {6, 7},
            {7, 6},
            {8, 3},
            {9, 1}
        };

        private void ComputeWins(Player[] players, bool first, long numberOfUniverses)
        {
            foreach (var dieOption in possibleDieThrows)
            {
                var diesValue = dieOption.Key;
                long occurences = numberOfUniverses * dieOption.Value;
                var player = players[first ? 0 : 1];
                player.Position = (player.Position + diesValue) % 10;
                player.Position = player.Position == 0 ? 10 : player.Position;
                player.Score = player.Score + player.Position;
                if(player.Score >= 21)
                {
                    player.Wins.Value += occurences;
                }
                else
                {
                    Player[] nextPlayers = first
                        ? new[] { player, players[1] }
                        : new[] { players[0], player };
                    ComputeWins(nextPlayers, !first, occurences);
                }
            }
        }

        class Wins
        {
            public long Value { get; set; }
        }

        struct Player
        {
            public Wins Wins { get; set; }
            public int Position { get; set; }
            public int Score { get; set; }
        }
    }
}
