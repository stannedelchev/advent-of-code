using AdventOfCode.Shared;

var problems = new IProblem[] {
    new AdventOfCode2021.Day01.Problem(),
    new AdventOfCode2021.Day02.Problem(),
    new AdventOfCode2021.Day03.Problem(),
    new AdventOfCode2021.Day04.Problem(),
};

var runner = new ProblemRunner(problems, 100, true, true);
runner.Run();
