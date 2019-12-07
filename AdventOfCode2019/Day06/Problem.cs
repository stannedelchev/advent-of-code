using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using AdventOfCode.Shared;

namespace AdventOfCode2019.Day06
{
    [DebuggerDisplay("{Name} -> {Parent}")]
    public class OrbitingObject
    {
        public OrbitingObject Parent { get; set; }
        public List<OrbitingObject> Children { get; set; } = new List<OrbitingObject>(256);
        public string Name { get; set; }
    }

    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var relationships = input.Select(l =>
            {
                var split = l.Split(")");
                return (split[0], split[1]);
            }).ToArray();
            var objects = CreateOrbitingObjects(relationships);

            var directAndIndirect = 0;
            foreach (var (_, orbitingObject) in objects)
            {
                var tmp = orbitingObject;
                while (tmp.Parent != null)
                {
                    directAndIndirect++;
                    tmp = tmp.Parent;
                }
            }

            return directAndIndirect.ToString();
        }

        public string Part2(string[] input)
        {
            var relationships = input.Select(l =>
            {
                var split = l.Split(")");
                return (split[0], split[1]);
            }).ToArray();
            var objects = CreateOrbitingObjects(relationships);

            var pathFromYouToCenterOfMass = PathToCenterOfMass(objects["YOU"]);
            var pathFromSantaToCenterOfMass = PathToCenterOfMass(objects["SAN"]);
            var commonPath = pathFromYouToCenterOfMass.Intersect(pathFromSantaToCenterOfMass).ToArray();

            var result = pathFromYouToCenterOfMass.Concat(pathFromSantaToCenterOfMass).Except(commonPath).ToArray();

            return result.Length.ToString();
        }

        private static List<OrbitingObject> PathToCenterOfMass(OrbitingObject from)
        {
            var tmp = from.Parent;
            var result = new List<OrbitingObject>(256);
            while (tmp.Parent != null)
            {
                result.Add(tmp);
                tmp = tmp.Parent;
            }

            return result;
        }

        private static Dictionary<string, OrbitingObject> CreateOrbitingObjects((string, string)[] relationships)
        {
            var objects = new Dictionary<string, OrbitingObject>();
            foreach (var relationship in relationships)
            {
                var (parentName, childName) = relationship;

                if (!objects.TryGetValue(parentName, out var parent))
                {
                    parent = new OrbitingObject()
                    {
                        Name = parentName
                    };
                    objects.Add(parentName, parent);
                }

                if (!objects.TryGetValue(childName, out var child))
                {
                    child = new OrbitingObject()
                    {
                        Name = childName
                    };
                    objects.Add(childName, child);
                }

                child.Parent = parent;
                parent.Children.Add(child);
            }

            return objects;
        }
    }
}
