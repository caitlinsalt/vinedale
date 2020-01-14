namespace Vinedale.Shell
{
    /// <summary>
    /// Interface for a shell UI control, for testing purposes.
    /// </summary>
    public interface IShellControl
    {
        /// <summary>
        /// Refresh the window contents.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Output text to the control.
        /// </summary>
        /// <param name="text">The text to be output.</param>
        void WriteText(string text);
    }
}
