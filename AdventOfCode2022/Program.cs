using AdventOfCode.Shared;

var problems = new IProblem[] {
    new AdventOfCode2022.Day01.Problem(),
    new AdventOfCode2022.Day02.Problem(),
    new AdventOfCode2022.Day03.Problem(),
};

#if DEBUG
const int runs = 1000;
#else
const int runs = 10000;
#endif

var runner = new ProblemRunner(problems, runs, true, true);
runner.Run();