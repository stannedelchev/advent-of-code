namespace AdventOfCode2019.Intcode
{
    internal enum IntCodeComputerState
    {
        NotSet,
        InitialState,
        Initialized,
        Running,
        Halted,
        WaitingForInput,
        Outputting
    }
}