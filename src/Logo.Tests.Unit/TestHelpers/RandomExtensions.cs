using Logo.Tokens;
using System;
using System.Collections.Generic;
using Tests.Utility.Extensions;
using Utility = Tests.Utility;

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
            LogoValueType.Number,
            LogoValueType.Word,
            LogoValueType.List,
        };

        private static readonly TokeniserResultType[] _validTokeniserResultTypes =
        {
            TokeniserResultType.Failure,
            TokeniserResultType.SuccessComplete,
            TokeniserResultType.SuccessIncomplete,
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
            return NextLogoValue(rnd, selectedType);
        }

        public static LogoValue NextLogoValue(this Random rnd, LogoValueType valueType)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }
            if (valueType == LogoValueType.Unknown || valueType == LogoValueType.Parcel)
            {
                return new LogoValue(valueType, null);
            }

            object val;
            switch (valueType)
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
                case LogoValueType.List:
                    val = rnd.NextListToken();
                    break;
                case LogoValueType.Word:
                    val = rnd.NextToken();
                    break;
            }

            return new LogoValue(valueType, val);
        }

        public static TokeniserResultType NextTokeniserResultType(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return _validTokeniserResultTypes[rnd.Next(_validTokeniserResultTypes.Length)];
        }

        public static Token NextToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return rnd.Next(7) switch
            {
                0 => NextCommentToken(rnd),
                1 => NextLiteralToken(rnd),
                2 => NextOperatorToken(rnd),
                3 => NextProcedureToken(rnd),
                4 => NextVariableToken(rnd),
                5 => NextExpressionToken(rnd),
                _ => NextListToken(rnd)
            };
        }

        public static CommentToken NextCommentToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new CommentToken(rnd.NextString(rnd.Next(256)));
        }

        public static ValueToken NextLiteralToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new ValueToken(rnd.NextString(rnd.Next(1, 24)), rnd.NextLogoValue());
        }

        public static ValueToken NextNumericToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new ValueToken(rnd.NextString(rnd.Next(1, 24)), rnd.NextLogoValue(LogoValueType.Number));
        }

        public static OperatorToken NextOperatorToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new OperatorToken(rnd.NextString("+-*/", 1));
        }

        public static ProcedureToken NextProcedureToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new ProcedureToken(rnd.NextString(Utility.Extensions.RandomExtensions.AlphabeticalCharacters, rnd.Next(1, 40)));
        }

        public static VariableToken NextVariableToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            string varName = rnd.NextString(Utility.Extensions.RandomExtensions.AlphabeticalCharacters, rnd.Next(1, 24));
            return new VariableToken(":" + varName, varName);
        }

        public static ExpressionToken NextExpressionToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            return new ExpressionToken(new Token[] { NextNumericToken(rnd), NextOperatorToken(rnd), NextNumericToken(rnd) });
        }

        public static ListToken NextListToken(this Random rnd)
        {
            if (rnd is null)
            {
                throw new NullReferenceException();
            }

            int listLength = rnd.Next(1, 6);
            List<Token> contents = new List<Token>(listLength);
            for (int i = 0; i < listLength; ++i)
            {
                contents.Add(NextToken(rnd));
            }
            return new ListToken(contents);
        }
    }
}
