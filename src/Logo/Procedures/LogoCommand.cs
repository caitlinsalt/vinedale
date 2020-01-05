namespace Logo.Procedures
{
    /// <summary>
    /// A <c>LogoCommand</c> is a subclass of <c>LogoProcedure</c> which represents a procedure implemented in .NET code.
    /// </summary>
    public class LogoCommand : LogoProcedure
    {
        /// <summary>
        /// The reference to the delegate which is called to execute the command.
        /// </summary>
        public CommandImplementation Implementation { get; set; }
    }
}
