using System;
using System.IO;
using System.Text;

namespace Vinedale.Shell
{
    /// <summary>
    /// A <see cref="Stream" /> implementation which writes its output to a <see cref="ShellControl" /> for the user to read.
    /// </summary>
    public class ShellOutputStream : Stream
    {
        private readonly IShellControl _shellControl;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shellControl">The control that this stream should write its output to.</param>
        public ShellOutputStream(IShellControl shellControl)
        {
            _shellControl = shellControl;
        }

        /// <summary>
        /// Whether or not this stream is readable.  It's not.
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// Whether or not this stream is seekable.  It's not.
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Whether or not this stream can be written to.  It can!
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Flush the stream.  Currently a no-op implementation.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// The length of this stream.  Always zero.
        /// </summary>
        public override long Length => 0L;

        /// <summary>
        /// The current position in the stream.  Should be the same as <see cref="Length" /> given the stream is write-only and not seekable.
        /// </summary>
        public override long Position { get { return 0L; } set { } }

        /// <summary>
        /// Read data from the stream.  Throws <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="buffer">The buffer to read into.</param>
        /// <param name="offset">The location in the buffer to start reading into.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>This method never returns successfully.</returns>
        /// <exception cref="InvalidOperationException">Always thrown, as this <see cref="Stream" /> is not readable.</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Move to a different position in the stream.  Throws <see cref="InvalidOperationException" />.
        /// </summary>
        /// <param name="offset">The offset to seek to.</param>
        /// <param name="origin">The location from which to count the offset.</param>
        /// <returns>This method never returns successfully.</returns>
        /// <exception cref="InvalidOperationException">Always thrown, as this <see cref="Stream" /> is not seekable.</exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Set the length of the stream.
        /// </summary>
        /// <param name="value">The length to set the stream to.</param>
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// Write data to the stream.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The index in the buffer to start writing from.</param>
        /// <param name="count">The number of bytes to write.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_shellControl == null)
            {
                return;
            }

            byte[] temp = new byte[count];
            Array.Copy(buffer, offset, temp, 0, count);
            _shellControl.WriteText(Encoding.Default.GetString(temp));
            _shellControl.Refresh();
        }
    }
}
