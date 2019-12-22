using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace AdventOfCode2019.Day13
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program);

            const int width = 50;
            const int height = 25;
            var tiles = new long[height, width];
            var blocksCount = 0;

            var v = Observable.FromEventPattern<long>(h => computer.Output += h, h => computer.Output -= h)
                              .Buffer(3)
                              .Subscribe(ev =>
                              {
                                  var x = ev[0].EventArgs;
                                  var y = ev[1].EventArgs;
                                  var z = ev[2].EventArgs;
                                  tiles[y, x] = z;
                                  if (z == 2)
                                  {
                                      blocksCount++;
                                  }
                              });

            computer.ExecuteProgram();
            v.Dispose();

            return blocksCount.ToString();
        }

        public string Part2(string[] input)
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program);
            computer[0] = 2;

            const int width = 50;
            const int height = 25;
            var tiles = new long[height, width];
            var ballX = -1L;
            var paddleX = -1L;
            var score = 0L;

            var v = Observable.FromEventPattern<long>(h => computer.Output += h, h => computer.Output -= h)
                              .Buffer(3)
                              .Subscribe(ev =>
                              {
                                  var x = ev[0].EventArgs;
                                  var y = ev[1].EventArgs;
                                  var z = ev[2].EventArgs;

                                  if (x == -1 && y == 0)
                                  {
                                      score = z;
                                      return;
                                  }

                                  tiles[y, x] = z;

                                  switch (z)
                                  {
                                      case 4:
                                          ballX = x;
                                          break;
                                      case 3:
                                          paddleX = x;
                                          break;
                                  }
                              });
            do
            {
                computer.ExecuteProgram();
                if (computer.State != IntCodeComputerState.WaitingForInput)
                {
                    continue;
                }

                var joystickInput = 0;
                if (ballX < paddleX)
                {
                    joystickInput = -1;
                }
                else if (ballX == paddleX)
                {
                    joystickInput = 0;
                }
                else
                {
                    joystickInput = 1;
                }

                computer.Input.Enqueue(joystickInput);
            } while (computer.State == IntCodeComputerState.WaitingForInput);

            v.Dispose();

            return score.ToString();
        }
    }
}