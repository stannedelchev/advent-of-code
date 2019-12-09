using AdventOfCode.Shared;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day08
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            const int width = 25;
            const int height = 6;
            var layers = CreateLayers(input, width, height);

            var minZeroesLayer = layers.MinBy(l => l.Digits[0]).First();
            var res = minZeroesLayer.Digits[1] * minZeroesLayer.Digits[2];

            return res.ToString();
        }


        public string Part2(string[] input)
        {
            const int width = 25;
            const int height = 6;

            var layers = CreateLayers(input, width, height);
            var flattenedLayer = new Layer(width, height);

            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    for (var layerIndex = 0; layerIndex < layers.Count; layerIndex++)
                    {
                        if (layers[layerIndex].Data[row, column] != 2)
                        {
                            flattenedLayer.Data[row, column] = layers[layerIndex].Data[row, column];
                            break;
                        }
                    }
                }
            }

            var result = $"{Environment.NewLine}{flattenedLayer}";
            return result;
        }

        private static List<Layer> CreateLayers(in string[] input, int width, int height)
        {
            var data = input[0].Select(c => int.Parse(c.ToString())).ToArray();
            var layersCount = data.Length / width / height;
            var layers = new List<Layer>(layersCount);

            for (var l = 0; l < layersCount; l++)
            {
                var layer = new Layer(width, height);
                layers.Add(layer);
                for (var row = 0; row < height; row++)
                {
                    for (var column = 0; column < width; column++)
                    {
                        var index = l * width * height + row * width + column;
                        var value = data[index];

                        layer.Data[row, column] = value;


                        var oldValue = layer.Digits[value];
                        layer.Digits[value] = oldValue + 1;
                    }
                }
            }

            return layers;
        }
    }
}
