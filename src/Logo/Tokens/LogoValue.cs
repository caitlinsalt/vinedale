using System;

namespace Logo.Tokens
{
    /// <summary>
    /// A struct representing a loosely typed data value.
    /// </summary>
    public struct LogoValue : IEquatable<LogoValue>
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
        /// Equality testing method.
        /// </summary>
        /// <param name="obj">An object to compare with.</param>
        /// <returns>True if the other object equals this value, false if it does not.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is LogoValue v))
            {
                return false;
            }
            return Equals(v);
        }

        /// <summary>
        /// Equality testing method.
        /// </summary>
        /// <param name="v">Another <see cref="LogoValue" /> to compare against.</param>
        /// <returns>True if the other value equals this one, false if it does not.</returns>
        public bool Equals(LogoValue v)
        {
            return v.Value.Equals(Value);
        }

        /// <summary>
        /// Generate a hash code for this value.
        /// </summary>
        /// <returns>Returns the hash code of the underlying value, or a fixed hash code if the underlying value is null.</returns>
        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 4468;
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">A <see cref="LogoValue" />.</param>
        /// <param name="b">A second <see cref="LogoValue" />.</param>
        /// <returns>True if the two parameters are equal, false if not.</returns>
        public static bool operator ==(LogoValue a, LogoValue b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Inequality operator
        /// </summary>
        /// <param name="a">A <see cref="LogoValue" />.</param>
        /// <param name="b">A second <see cref="LogoValue" />.</param>
        /// <returns>True if the two parameters are not equal, false if they are.</returns>
        public static bool operator !=(LogoValue a, LogoValue b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Gets a default-valued <see cref="LogoValue" /> for a given <see cref="LogoValueType" />.
        /// </summary>
        /// <param name="type">The type of value to return a default for.</param>
        /// <returns>A <see cref="LogoValue" /> object of the given <see cref="LogoValueType" />.</returns>
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
