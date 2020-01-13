using System;
using Vinedale.Turtle.Drawing;

namespace Vinedale.Turtle.Tests.Unit.TestHelpers
{
    internal static class RandomExtensions
    {
        private static PenStatus[] _validPenStatuses =
        {
            PenStatus.Down,
            PenStatus.Up,
        };

        private static TurtleStatus[] _validTurtleStatuses =
        {
            TurtleStatus.Shown,
            TurtleStatus.Hidden,
        };

        internal static PenStatus NextPenStatus(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }
            return _validPenStatuses[rnd.Next(_validPenStatuses.Length)];
        }

        internal static TurtleStatus NextTurtleStatus(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }
            return _validTurtleStatuses[rnd.Next(_validTurtleStatuses.Length)];
        }
    }
}
