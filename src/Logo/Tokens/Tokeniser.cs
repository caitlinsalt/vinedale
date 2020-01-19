using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Logo.Tokens
{
    /// <summary>
    /// Class responsible for tokenising string input.
    /// </summary>
    public static class Tokeniser
    {
        private enum TokenMode
        {
            WhiteSpace,
            LiteralNumber,
            LiteralString,
            Word,
            Variable,
            Comment
        }

        /// <summary>
        /// Convert an input string into a list of tokens.
        /// </summary>
        /// <param name="input">The input text to be tokenised.</param>
        /// <returns>A <c>TokeniserResult</c> containing the list of output tokens, and information on the state of the tokeniser on completion.</returns>
        public static TokeniserResult TokeniseString(string input)
        {
            TokenMode mode = TokenMode.WhiteSpace;

            int startPos = 0;
            int listNest = 0;
            int exprNest = 0;
            int idx;
            const string singleSymbolWords = "+-*/";
            const char literalStringMarker = '"';
            const char variableMarker = ':';
            const char commentMarker = ';';
            const char listStartMarker = '[';
            const char exprStartMarker = '(';
            const char listEndMarker = ']';
            const char exprEndMarker = ')';
            const char decimalPoint = '.';

            List<Token> tokensProduced = new List<Token>();

            try
            {
                for (idx = 0; idx < input.Length; ++idx)
                {
                    if (mode == TokenMode.WhiteSpace && listNest == 0 && exprNest == 0)
                    {
                        if (char.IsWhiteSpace(input[idx]))
                        {
                            continue;
                        }

                        if (char.IsDigit(input[idx]))
                        {
                            mode = TokenMode.LiteralNumber;
                        }
                        else if (input[idx] == listStartMarker)
                        {
                            listNest++;
                        }
                        else if (input[idx] == exprStartMarker)
                        {
                            exprNest++;
                        }
                        else if (input[idx] == commentMarker)
                        {
                            mode = TokenMode.Comment;
                        }
                        else if (singleSymbolWords.Contains(input[idx]))
                        {
                            tokensProduced.Add(new OperatorToken(input.Substring(idx, 1)));
                        }
                        else if (input[idx] == variableMarker)
                        {
                            mode = TokenMode.Variable;
                        }
                        else if (input[idx] == literalStringMarker)
                        {
                            mode = TokenMode.LiteralString;
                        }
                        else
                        {
                            mode = TokenMode.Word;
                        }
                        startPos = idx;
                    }
                    else if (mode == TokenMode.Word)
                    {
                        if (char.IsWhiteSpace(input[idx]))
                        {
                            tokensProduced.Add(new ProcedureToken(input.Substring(startPos, idx - startPos)));
                            mode = TokenMode.WhiteSpace;
                        }
                        else if (input[idx] == listStartMarker)
                        {
                            tokensProduced.Add(new ProcedureToken(input.Substring(startPos, idx - startPos)));
                            listNest++;
                            startPos = idx;
                            mode = TokenMode.WhiteSpace;
                        }
                        else if (input[idx] == commentMarker)
                        {
                            tokensProduced.Add(new ProcedureToken(input.Substring(startPos, idx - startPos)));
                            mode = TokenMode.Comment;
                            startPos = idx;
                        }
                    }
                    else if (listNest > 0)
                    {
                        if (mode != TokenMode.Comment)
                        {
                            if (input[idx] == listStartMarker)
                            {
                                listNest++;
                            }
                            else if (input[idx] == listEndMarker)
                            {
                                listNest--;
                                if (listNest == 0)
                                {
                                    tokensProduced.Add(new ListToken(input.Substring(startPos, idx + 1 - startPos)));
                                }
                            }
                            else if (input[idx] == ';')
                            {
                                mode = TokenMode.Comment;
                            }
                        }
                        else if (input.Substring(idx).StartsWith(Environment.NewLine, StringComparison.InvariantCulture))
                        {
                            idx += (Environment.NewLine.Length - 1);
                            mode = TokenMode.WhiteSpace;
                        }
                    }
                    else if (exprNest > 0)
                    {
                        if (mode != TokenMode.Comment)
                        {
                            if (input[idx] == exprStartMarker)
                            {
                                exprNest++;
                            }
                            else if (input[idx] == exprEndMarker)
                            {
                                exprNest--;
                                if (exprNest == 0)
                                {
                                    tokensProduced.Add(new ExpressionToken(input.Substring(startPos, idx + 1 - startPos)));
                                }
                            }
                            else if (input[idx] == commentMarker)
                            {
                                mode = TokenMode.Comment;
                                startPos = idx;
                            }
                        }
                        else if (input.Substring(idx).StartsWith(Environment.NewLine, StringComparison.InvariantCulture))
                        {
                            idx += (Environment.NewLine.Length - 1);
                            mode = TokenMode.WhiteSpace;
                        }
                    }
                    else if (mode == TokenMode.WhiteSpace)
                    {
                        if (input.Substring(idx).StartsWith(Environment.NewLine, StringComparison.InvariantCulture))
                        {
                            tokensProduced.Add(new CommentToken(input.Substring(startPos, idx - startPos)));
                            idx += (Environment.NewLine.Length - 1);
                            mode = TokenMode.WhiteSpace;
                        }
                    }
                    else if (mode == TokenMode.LiteralString)
                    {
                        if (char.IsWhiteSpace(input[idx]))
                        {
                            tokensProduced.Add(new ValueToken(input.Substring(startPos, idx - startPos), new LogoValue(LogoValueType.Text, input.Substring(startPos + 1, idx - (startPos + 1)))));
                            mode = TokenMode.WhiteSpace;
                        }
                    }
                    else if (mode == TokenMode.LiteralNumber)
                    {
                        if ((!char.IsDigit(input[idx])) && input[idx] != decimalPoint)
                        {
                            string tokenText = input.Substring(startPos, idx - startPos);
                            tokensProduced.Add(new ValueToken(tokenText, new LogoValue(LogoValueType.Number, decimal.Parse(tokenText, CultureInfo.InvariantCulture))));
                            if (input[idx] == listStartMarker)
                            {
                                ++listNest;
                                mode = TokenMode.WhiteSpace;
                            }
                            else if (input[idx] == exprStartMarker)
                            {
                                ++exprNest;
                                mode = TokenMode.WhiteSpace;
                            }
                            else if (input[idx] == commentMarker)
                            {
                                mode = TokenMode.Comment;
                                startPos = idx;
                            }
                            else if (singleSymbolWords.Contains(input[idx]))
                            {
                                tokensProduced.Add(new OperatorToken(input.Substring(idx, 1)));
                                mode = TokenMode.WhiteSpace;
                            }
                            else if (input[idx] == variableMarker)
                            {
                                mode = TokenMode.Variable;
                                startPos = idx;
                            }
                            else if (input[idx] == literalStringMarker)
                            {
                                mode = TokenMode.LiteralString;
                                startPos = idx;
                            }
                            else if (char.IsWhiteSpace(input[idx]))
                            {
                                mode = TokenMode.WhiteSpace;
                            }
                            else
                            {
                                mode = TokenMode.Word;
                                startPos = idx;
                            }
                        }
                    }
                    else if (mode == TokenMode.Variable)
                    {
                        if (char.IsWhiteSpace(input[idx]))
                        {
                            tokensProduced.Add(new VariableToken(input.Substring(startPos, idx - startPos), input.Substring(startPos + 1, idx - (startPos + 1))));
                            mode = TokenMode.WhiteSpace;
                        }
                        else if (input[idx] == listStartMarker)
                        {
                            tokensProduced.Add(new VariableToken(input.Substring(startPos, idx - startPos), input.Substring(startPos + 1, idx - (startPos + 1))));
                            listNest++;
                            startPos = idx;
                            mode = TokenMode.WhiteSpace;
                        }
                        else if (input[idx] == commentMarker)
                        {
                            tokensProduced.Add(new VariableToken(input.Substring(startPos, idx - startPos), input.Substring(startPos + 1, idx - (startPos + 1))));
                            mode = TokenMode.Comment;
                            startPos = idx;
                        }
                    }
                    else if (mode == TokenMode.Comment)
                    {
                        if (input.Substring(idx).StartsWith(Environment.NewLine, StringComparison.InvariantCulture))
                        {
                            tokensProduced.Add(new CommentToken(input.Substring(startPos, idx - startPos)));
                            idx += (Environment.NewLine.Length - 1);
                            mode = TokenMode.WhiteSpace;
                        }
                    }
                }

                if (mode == TokenMode.Word)
                {
                    tokensProduced.Add(new ProcedureToken(input.Substring(startPos)));
                }
                else if (mode == TokenMode.LiteralNumber)
                {
                    string tokenText = input.Substring(startPos, idx - startPos);
                    tokensProduced.Add(new ValueToken(tokenText, new LogoValue(LogoValueType.Number, decimal.Parse(tokenText, CultureInfo.InvariantCulture))));
                }
                else if (mode == TokenMode.LiteralString)
                {
                    tokensProduced.Add(new ValueToken(input.Substring(startPos, idx - startPos), new LogoValue(LogoValueType.Text, input.Substring(startPos + 1, idx - (startPos + 1)))));
                }
                else if (mode == TokenMode.Variable)
                {
                    tokensProduced.Add(new VariableToken(input.Substring(startPos, idx - startPos), input.Substring(startPos + 1, idx - (startPos + 1))));
                }
                else if (listNest == 0 && exprNest == 0 && mode == TokenMode.Comment)
                {
                    tokensProduced.Add(new CommentToken(input.Substring(startPos)));
                }

                if (listNest > 0 || exprNest > 0)
                {
                    return new TokeniserResult(TokeniserResultType.SuccessIncomplete, tokensProduced, input.Substring(startPos));
                }
                else
                {
                    return new TokeniserResult(TokeniserResultType.SuccessComplete, tokensProduced);
                }
            }
            catch (TokeniserException ex)
            {
                return new TokeniserResult(TokeniserResultType.Failure, Array.Empty<Token>(), null, ex.Message);
            }
        }
    }
}
