using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Intcode.OpCodes
{
    internal static class OpCodeCache
    {
        public static readonly OpCodeAdjustRelativeBase OpCodeAdjustRelativeBase = new OpCodeAdjustRelativeBase();
        public static readonly OpCodeEquals OpCodeEquals = new OpCodeEquals();
        public static readonly OpCodeLessThan OpCodeLessThan = new OpCodeLessThan();
        public static readonly OpCodeJumpIfFalse OpCodeJumpIfFalse = new OpCodeJumpIfFalse();
        public static readonly OpCodeHalt OpCodeHalt = new OpCodeHalt();
        public static readonly OpCodeInput OpCodeInput = new OpCodeInput();
        public static readonly OpCodeMultiply OpCodeMultiply = new OpCodeMultiply();
        public static readonly OpCodeSum OpCodeSum = new OpCodeSum();
        public static readonly OpCodeOutput OpCodeOutput = new OpCodeOutput();
        public static readonly OpCodeJumpIfTrue OpCodeJumpIfTrue = new OpCodeJumpIfTrue();
    }
}
