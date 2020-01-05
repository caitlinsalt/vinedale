using System;
using System.Collections.Generic;
using System.Linq;

namespace Logo.Tokens
{
    /// <summary>
    /// The parent class of all Logo input tokens.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// A textual representation of this token, such as the text originally entered by the user.
        /// </summary>
        public string Literal { get; set; }

        /// <summary>
        /// Whether this token has already had its value computed.
        /// </summary>
        public virtual bool Evaluated { get; set; }

        /// <summary>
        /// The value of this token.
        /// </summary>
        public LogoValue TokenValue { get; set; }

        /// <summary>
        /// Produces a copy of this token.
        /// </summary>
        /// <returns>A token identical to this one.</returns>
        public virtual Token Clone()
        {
            return new Token { Evaluated = Evaluated, Literal = Literal, TokenValue = TokenValue };
        }

        /// <summary>
        /// Convert an input string into a list of tokens.
        /// </summary>
        /// <param name="input">The input text to be tokenised.</param>
        /// <returns>A <c>TokeniserResult</c> containing the list of output tokens, and information on the state of the tokeniser on completion.</returns>
        public static TokeniserResult TokeniseString(string input)
        {
            TokeniserResult result = new TokeniserResult();
            result.TokenisedData = new List<Token>();

            int idx = 0;
            bool inWord = false;
            bool inComment = false;
            int startPos = 0;
            int listNest = 0;
            int exprNest = 0;
            const string singleSymbolWords = "+-*/";

            try
            {
                for (idx = 0; idx < input.Length; ++idx)
                {
                    if (!inWord && (listNest == 0) && (exprNest == 0) && !inComment)
                    {
                        if (Char.IsWhiteSpace(input[idx]))
                        {
                            continue;
                        }

                        if (input[idx] == '[')
                        {
                            listNest++;
                        }
                        else if (input[idx] == '(')
                        {
                            exprNest++;
                        }
                        else if (input[idx] == ';')
                        {
                            inComment = true;
                        }
                        else if (singleSymbolWords.Contains(input[idx]))
                        {
                            result.TokenisedData.Add(new LogoOperator(input.Substring(idx, 1)));
                        }
                        else
                        {
                            inWord = true;
                        }

                        startPos = idx;
                    }
                    else if (inWord)
                    {
                        if (char.IsWhiteSpace(input[idx]))
                        {
                            result.TokenisedData.Add(new Word { Literal = input.Substring(startPos, idx - startPos) });
                            inWord = false;
                        }
                        else if (input[idx] == '[')
                        {
                            result.TokenisedData.Add(new Word { Literal = input.Substring(startPos, idx - startPos) });
                            inWord = false;
                            listNest++;
                            startPos = idx;
                        }
                        else if (input[idx] == ';')
                        {
                            result.TokenisedData.Add(new Word { Literal = input.Substring(startPos, idx - startPos) });
                            inWord = false;
                            inComment = true;
                            startPos = idx;
                        }
                    }
                    else if (listNest > 0)
                    {
                        if (!inComment)
                        {
                            if (input[idx] == '[')
                            {
                                listNest++;
                            }
                            else if (input[idx] == ']')
                            {
                                listNest--;
                                if (listNest == 0)
                                {
                                    result.TokenisedData.Add(new LogoList(input.Substring(startPos, idx + 1 - startPos)));
                                }
                            }
                            else if (input[idx] == ';')
                            {
                                inComment = true;
                            }
                        }
                        else if (input.Substring(idx).StartsWith(Environment.NewLine))
                        {
                            idx += (Environment.NewLine.Length - 1);
                            inComment = false;
                        }
                    }
                    else if (exprNest > 0)
                    {
                        if (input[idx] == '(')
                        {
                            exprNest++;
                        }
                        else if (input[idx] == ')')
                        {
                            exprNest--;
                            if (exprNest == 0)
                            {
                                result.TokenisedData.Add(new LogoExpression(input.Substring(startPos, idx + 1 - startPos)));
                            }
                        }
                        else if (input[idx] == ';')
                        {
                            inComment = true;
                        }
                        else if (input.Substring(idx).StartsWith(Environment.NewLine))
                        {
                            idx += (Environment.NewLine.Length - 1);
                            inComment = false;
                        }
                    }
                    else if (inComment)
                    {
                        if (input.Substring(idx).StartsWith(Environment.NewLine))
                        {
                            result.TokenisedData.Add(new LogoComment { Literal = input.Substring(startPos, idx - startPos) });
                            idx += (Environment.NewLine.Length - 1);
                            inComment = false;
                        }
                    }
                }

                if (inWord)
                {
                    result.TokenisedData.Add(new Word { Literal = input.Substring(startPos) });
                }
                else if (listNest == 0 && exprNest == 0 && inComment)
                {
                    result.TokenisedData.Add(new LogoComment { Literal = input.Substring(startPos) });
                }

                if (listNest > 0 || exprNest > 0)
                {
                    result.ResultType = TokeniserResultType.SuccessIncomplete;
                    result.NonConsumedInput = input.Substring(startPos);
                }
                else
                {
                    result.ResultType = TokeniserResultType.SuccessComplete;
                    result.NonConsumedInput = "";
                }
            }
            catch (TokeniserException ex)
            {
                result.TokenisedData.Clear();
                result.ResultType = TokeniserResultType.Failure;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
