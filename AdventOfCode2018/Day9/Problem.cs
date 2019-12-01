using AdventOfCode2018.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day9
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var match = new Regex(@"(?<players>\d+) players; last marble is worth (?<points>\d+) points").Match(input[0]);
            var players = Enumerable.Range(1, int.Parse(match.Groups["players"].Value)).Select(p => new Player(p)).ToList();
            var lastMarblePoints = int.Parse(match.Groups["points"].Value);

            var result = this.PartCore(players, lastMarblePoints);
            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var match = new Regex(@"(?<players>\d+) players; last marble is worth (?<points>\d+) points").Match(input[0]);
            var players = Enumerable.Range(1, int.Parse(match.Groups["players"].Value)).Select(p => new Player(p)).ToList();
            var lastMarblePoints = int.Parse(match.Groups["points"].Value) * 100;

            var result = this.PartCore(players, lastMarblePoints);
            return result.ToString();
        }

        private long PartCore(List<Player> players, int lastMarblePoints)
        {
            var marbles = new Queue<int>(Enumerable.Range(0, lastMarblePoints + 1));
            var circle = new MarbleCircle();
            circle.PutTwoAfterCurrent(marbles.Dequeue());

            var playerEnumerator = players.RepeatForever().GetEnumerator();
            playerEnumerator.MoveNext();

            while (marbles.Count > 0)
            {
                var player = playerEnumerator.ProcessAndMove();
                player.PutMarble(marbles.Dequeue(), circle);
            }

            return players.OrderByDescending(p => p.Score).First().Score;
        }
    }
}
