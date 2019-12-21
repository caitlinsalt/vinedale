using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinedale.Shell
{
    /// <summary>
    /// A <c>Stream</c> implementation which, when written to, displays the written output in a <c>ShellControl</c> component.
    /// </summary>
    public class ShellOutputStream : Stream
    {
        private readonly ShellControl _shell;

        /// <summary>
        /// The sole constructor for this class.
        /// </summary>
        /// <param name="control">The <c>ShellControl</c> component in which written output should be displayed.</param>
        public ShellOutputStream(ShellControl control)
        {
            _shell = control;
        }

        /// <summary>
        /// Whether this <c>Stream</c> is readable (it is not).
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Whether this <c>Stream</c> is seekable (it is not).
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Whether this <c>Stream</c> is writable (it is).
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// A no-op implementation of <c>Stream.Flush()</c>.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Returns the length of the <c>Stream</c>, which is always zero.
        /// </summary>
        public override long Length
        {
            get
            {
                return 0L;
            }
        }

        /// <summary>
        /// Returns the current position in the <c>Stream</c>, which is always zero.
        /// </summary>
        public override long Position
        {
            get
            {
                return 0L;
            }
            set
            {
            }
        }

        /// <summary>
        /// Read data from the <c>Stream</c>.  As this stream is not readable, this method always throws <c>NotImplementedException</c>.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        /// <param name="buffer">Buffer to be written to the stream.</param>
        /// <param name="offset">Buffer offset to start writing from.</param>
        /// <param name="count">Bytes to be written.</param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the position of the <c>Stream</c>.  As this stream is not seekable, this method always throws <c>NotImplementedException.</c>
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        /// <param name="offset">The position to seek to relative to the <c>origin</c>.</param>
        /// <param name="origin">The origin from which the <c>offset</c> parameter is measured.</param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the length of the <c>Stream</c>.  This is a no-op implementation.
        /// </summary>
        /// <param name="value">Not used.</param>
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// Write data to the <c>ShellControl</c> component.
        /// </summary>
        /// <param name="buffer">The data to be written.</param>
        /// <param name="offset">The position in the <c>buffer</c> from which to start writing.</param>
        /// <param name="count">The number of bytes to write from the <c>buffer</c>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_shell == null)
            {
                return;
            }
            byte[] temp = new byte[count];
            Array.Copy(buffer, offset, temp, 0, count);
            _shell.WriteText(Encoding.Default.GetString(temp));
            _shell.Refresh();
        }
    }
}
