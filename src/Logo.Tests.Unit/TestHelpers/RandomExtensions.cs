using Logo.Tokens;
using System;

namespace Logo.Tests.Unit.TestHelpers
{
    public static class RandomExtensions
    {
        private static LogoValueType[] _validLogoValueTypes =
        {
            LogoValueType.Unknown,
            LogoValueType.Bool,
            LogoValueType.Text,
            LogoValueType.Number,
            LogoValueType.Word,
            LogoValueType.List,
            LogoValueType.Parcel,
        };

        public static LogoValueType NextLogoValueType(this Random rnd)
        {
            if (rnd == null)
            {
                throw new NullReferenceException();
            }

            return _validLogoValueTypes[rnd.Next(_validLogoValueTypes.Length)];
        }
    }
}
