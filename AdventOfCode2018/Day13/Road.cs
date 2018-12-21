using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day13
{

    internal enum Road
    {
        Empty = 0,
        StraightUpDown = 1,
        StraightLeftRight = 2,
        Crossroad = 3,
        TurnBottomLeftTopRight = 4,
        TurnTopLeftBottomRight = 5
    }
}
