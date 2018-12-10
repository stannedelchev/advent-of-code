using System.Collections.Generic;

namespace AdventOfCode2018.Day7
{
    internal class Vertex
    {
        public Vertex(string letter)
        {
            this.Letter = letter;
        }

        public string Letter { get; }

        public IList<Vertex> Outputs { get; } = new List<Vertex>();

        public IList<Vertex> Inputs { get; } = new List<Vertex>();

        public void AddDestination(Vertex vertex)
        {
            this.Outputs.Add(vertex);
            vertex.Inputs.Add(this);
        }

        public void RemoveDestination(Vertex vertex)
        {
            this.Outputs.Remove(vertex);
            vertex.Inputs.Remove(this);
        }
    }
}
