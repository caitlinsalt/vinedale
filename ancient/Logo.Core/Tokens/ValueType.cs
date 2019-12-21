using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Core.Tokens
{
    /// <summary>
    /// The possible data types of the value of a variable or result in Logo.
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// The data type of an undefined variable.
        /// </summary>
        Unknown,

        /// <summary>
        /// Boolean.
        /// </summary>
        Bool,

        /// <summary>
        /// String.
        /// </summary>
        String,

        /// <summary>
        /// Numeric data.  Internally stored as the .NET <c>decimal</c> type.
        /// </summary>
        Number,

        /// <summary>
        /// Word.  Represents a Logo procedure, command, variable or constant.
        /// </summary>
        Word,

        /// <summary>
        /// A list of tokens or values.
        /// </summary>
        List,

        /// <summary>
        /// An external object.  Used to pass around, for example, references to implementation objects such as turtles or windows.
        /// </summary>
        Parcel
    }
}
