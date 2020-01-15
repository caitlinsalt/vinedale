using Logo.Tokens;
using System;
using Tests.Utility.Extensions;

namespace Logo.Tests.Unit.TestHelpers
{
    public static class RandomExtensions
    {
        private static readonly LogoValueType[] _validLogoValueTypes =
        {
            LogoValueType.Unknown,
            LogoValueType.Bool,
            LogoValueType.Text,
            LogoValueType.Number,
            LogoValueType.Word,
            LogoValueType.List,
            LogoValueType.Parcel,
        };

        private static readonly LogoValueType[] _limitedLogoValueTypes =
        {
            LogoValueType.Bool,
            LogoValueType.Text,
            LogoValueType.Number
        };

        public static LogoValueType NextLogoValueType(this Random rnd)
        {
            if (rnd == null)
            {
                throw new NullReferenceException();
            }

            return _validLogoValueTypes[rnd.Next(_validLogoValueTypes.Length)];
        }

        public static LogoValue NextLogoValue(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            LogoValueType selectedType = _limitedLogoValueTypes[rnd.Next(_limitedLogoValueTypes.Length)];
            object val;
            switch (selectedType)
            {
                case LogoValueType.Bool:
                default:
                    val = rnd.NextBool();
                    break;
                case LogoValueType.Text:
                    val = rnd.NextString(rnd.Next(512));
                    break;
                case LogoValueType.Number:
                    int chooseValue = rnd.Next(3);
                    val = chooseValue switch
                    {
                        1 => rnd.NextDouble(),
                        2 => (decimal)rnd.NextDouble(),
                        _ => rnd.Next(),
                    };
                    break;
            }

            return new LogoValue(selectedType, val);
        }
    }
}
