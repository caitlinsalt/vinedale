using System;
using System.IO;
using System.Text;

namespace Vinedale.Shell
{
    public class ShellOutputStream : Stream
    {
        private readonly IShellControl _shellControl;

        public ShellOutputStream(IShellControl shellControl)
        {
            _shellControl = shellControl;
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override void Flush()
        {
        }

        public override long Length => 0L;

        public override long Position { get { return 0L; } set { } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
        }

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
