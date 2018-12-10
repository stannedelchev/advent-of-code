using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day8
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var data = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var enumerator = data.GetEnumerator();
            enumerator.MoveNext();
            var root = this.ParseNode(enumerator);

            var result = root.CalculateMetadata();
            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var data = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var enumerator = data.GetEnumerator();
            enumerator.MoveNext();
            var root = this.ParseNode(enumerator);

            var result = root.CalculateValue();
            return result.ToString();
        }

        private TreeNode ParseNode(IEnumerator<int> enumerator)
        {
            var node = new TreeNode();
            var childrenCount = enumerator.ProcessAndMove();
            var metadataCount = enumerator.ProcessAndMove();

            for (var i = 0; i < childrenCount; i++)
            {
                var child = this.ParseNode(enumerator);
                node.Children.Add(child);
            }

            for (var i = 0; i < metadataCount; i++)
            {
                var metadata = enumerator.ProcessAndMove();
                node.Metadata.Add(metadata);
            }

            return node;
        }
    }

    internal static class IEnumeratorExtensions
    {
        public static T ProcessAndMove<T>(this IEnumerator<T> enumerator)
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            return result;
        }
    }


    internal class TreeNode
    {
        public IList<TreeNode> Children { get; } = new List<TreeNode>();

        public IList<int> Metadata { get; } = new List<int>();

        public int CalculateMetadata()
        {
            return this.Children.Sum(c => c.CalculateMetadata()) + this.Metadata.Sum();
        }

        public int CalculateValue()
        {
            if (!this.Children.Any())
            {
                return this.Metadata.Sum();
            }

            var sum = 0;
            foreach (var metadata in this.Metadata)
            {
                if (metadata > this.Children.Count)
                {
                    sum += 0;
                }
                else
                {
                    sum += this.Children[metadata - 1].CalculateValue();
                }
            }

            return sum;
        }
    }
}
