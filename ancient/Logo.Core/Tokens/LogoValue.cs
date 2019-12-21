using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// A struct representing a loosely typed data value.
    /// </summary>
    public struct LogoValue
    {
        /// <summary>
        /// The kind of value that this struct contains.
        /// </summary>
        public ValueType Type { get; set; }

        /// <summary>
        /// The object which encapsulates the actual value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets a default-valued <c>LogoValue</c> for a given <c>ValueType</c>
        /// </summary>
        /// <param name="type">The type of value to return a default for.</param>
        /// <returns>A <c>LogoValue</c> object of the given <c>ValueType</c>.0</returns>
        public static LogoValue GetDefaultValue(ValueType type)
        {
            switch (type)
            {
                case ValueType.Bool:
                    return new LogoValue { Type = ValueType.Bool, Value = false };
                case ValueType.List:
                    return new LogoValue { Type = ValueType.List, Value = new LogoList("[]") };
                case ValueType.Number:
                    return new LogoValue { Type = ValueType.Number, Value = 0m };
                case ValueType.Parcel:
                    return new LogoValue { Type = ValueType.Parcel, Value = null };
                default:
                case ValueType.String:
                    return new LogoValue { Type = ValueType.String, Value = "" };
                case ValueType.Unknown:
                    return new LogoValue { Type = ValueType.Unknown, Value = null };
                case ValueType.Word:
                    return new LogoValue { Type = ValueType.Word, Value = new LogoWord() };
            }
        }
    }
}
