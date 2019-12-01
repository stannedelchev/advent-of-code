using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day7
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var vertices = CreateVertices(input);

            var verticesWithNoInputs = vertices.Where(v => !v.Value.Inputs.Any()).OrderBy(v => v.Key).Select(kvp => kvp.Value).ToList();
            var traversedVertices = new List<Vertex>();

            while (verticesWithNoInputs.Any())
            {
                var n = verticesWithNoInputs.First();
                verticesWithNoInputs.RemoveAt(0);

                traversedVertices.Add(n);

                foreach (var m in n.Outputs.ToList())
                {
                    n.RemoveDestination(m);
                }

                verticesWithNoInputs = vertices.Where(v => !v.Value.Inputs.Any())
                            .OrderBy(v => v.Key)
                            .Select(kvp => kvp.Value)
                            .Except(traversedVertices)
                            .ToList();
            }

            return string.Join("", traversedVertices.Select(v => v.Letter));
        }

        public string Part2(string[] input)
        {
            return "";
        }

        private static Dictionary<string, Vertex> CreateVertices(string[] input)
        {
            var vertices = new Dictionary<string, Vertex>();

            foreach (var line in input)
            {
                var match = new Regex("Step (?<in>.) must be finished before step (?<out>.) can begin.").Match(line);
                var inputVertexLetter = match.Groups["in"].Value;
                var outputVertexLetter = match.Groups["out"].Value;

                vertices.TryAdd(inputVertexLetter, new Vertex(inputVertexLetter));
                vertices.TryAdd(outputVertexLetter, new Vertex(outputVertexLetter));

                vertices[inputVertexLetter].AddDestination(vertices[outputVertexLetter]);
            }

            return vertices;
        }
    }
}
