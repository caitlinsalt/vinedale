using System;

namespace Logo.Tokens
{
    /// <summary>
    /// A struct representing a loosely typed data value.
    /// </summary>
    public struct LogoValue
    {
        /// <summary>
        /// The kind of value that this struct contains.
        /// </summary>
        public LogoValueType Type { get; set; }

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
                case LogoValueType.Bool:
                    return new LogoValue { Type = LogoValueType.Bool, Value = false };
                case LogoValueType.List:
                    return new LogoValue { Type = LogoValueType.List, Value = new LogoList("[]") };
                case LogoValueType.Number:
                    return new LogoValue { Type = LogoValueType.Number, Value = 0m };
                case LogoValueType.Parcel:
                    return new LogoValue { Type = LogoValueType.Parcel, Value = null };
                default:
                case LogoValueType.Text:
                    return new LogoValue { Type = LogoValueType.Text, Value = "" };
                case LogoValueType.Unknown:
                    return new LogoValue { Type = LogoValueType.Unknown, Value = null };
                case LogoValueType.Word:
                    return new LogoValue { Type = LogoValueType.Word, Value = new Word() };
            }
        }
    }
}
