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

        internal static PenStatus NextPenStatus(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }
            return _validPenStatuses[rnd.Next(_validPenStatuses.Length)];
        }
    }
}
