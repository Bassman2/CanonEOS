namespace CanonAPI;

public class EdsStream : Stream
{
    private nint stream;

    public EdsStream(nint stream)
    {
        this.stream = stream;
    }

    public EdsStream(string filepath, FileMode fileMode, FileAccess access)
    {
        EdsFileAccess edsAccess = access switch
        {
            FileAccess.Read => EdsFileAccess.Read,
            FileAccess.Write => EdsFileAccess.Write,
            FileAccess.ReadWrite => EdsFileAccess.ReadWrite,
            _ => throw new NotSupportedException()
        };

        EdsFileCreateDisposition edsCreateDisposition = fileMode switch
        {
            FileMode.CreateNew => EdsFileCreateDisposition.CreateNew,
            FileMode.Create => EdsFileCreateDisposition.CreateAlways,
            FileMode.Open => EdsFileCreateDisposition.OpenExisting,
            FileMode.OpenOrCreate => EdsFileCreateDisposition.OpenAlways,
            FileMode.Truncate => EdsFileCreateDisposition.TruncateExisting,
            _ => throw new NotSupportedException()
        };

        Eds.EdsCreateFileStreamEx(filepath, edsCreateDisposition, edsAccess, out this.stream);
    }

    public EdsStream(string filepath, EdsFileCreateDisposition createDisposition, EdsFileAccess access)
    {
        Eds.EdsCreateFileStreamEx(filepath, createDisposition, access, out this.stream);
    }

    public EdsStream(long length)
    {
        Eds.EdsCreateMemoryStream(length, out this.stream);
    }

    public EdsStream(byte[] buffer)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));

        Eds.EdsCreateMemoryStreamFromPointer(buffer, buffer.LongLength, out this.stream);
        
    }

    public EdsStream(IntPtr buffer, long length)
    {
        if (buffer == IntPtr.Zero) throw new ArgumentNullException(nameof(buffer));

        Eds.EdsCreateMemoryStreamFromPointer(buffer, length, out this.stream);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (this.stream != nint.Zero)
        {
            Eds.EdsRelease(this.stream);
            this.stream = nint.Zero;
        }
    }

    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite => true;

    public override long Length
    {
        get
        {
            Eds.EdsGetLength(this.stream, out long length);
            return length;
        }
    }

    public override long Position 
    { 
        get
        {
            Eds.EdsGetPosition(this.stream, out long position);
            return position;
        }
        set => Seek(value, SeekOrigin.Begin); 
    }

    public override void Flush()
    {
        // nothing to do
    }

    public unsafe override int Read(byte[] buffer, int offset, int count)
    {
        if (buffer.LongLength < offset + count)
        {
            throw new ArgumentOutOfRangeException();
        }

        fixed (byte* bufferPtr = buffer)
        {
            byte* offsetBufferPtr = bufferPtr + offset;
            Eds.EdsRead(this.stream, count, (IntPtr)offsetBufferPtr, out long read);
            return (int)read;                
        };
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        EdsSeekOrigin edsOrigin = origin switch
        {
            SeekOrigin.Begin => EdsSeekOrigin.Begin,
            SeekOrigin.Current => EdsSeekOrigin.Current,
            SeekOrigin.End => EdsSeekOrigin.End,
            _ => throw new NotSupportedException()
        };

        Eds.EdsSeek(this.stream, offset, edsOrigin);
        return Position;
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public unsafe override void Write(byte[] buffer, int offset, int count)
    {
        if (buffer.LongLength < offset + count)
        {
            throw new ArgumentOutOfRangeException();
        }
        fixed (byte* bufferPtr = buffer)
        {
            byte* offsetBufferPtr = bufferPtr + offset;
            Eds.EdsWrite(this.stream, count, (IntPtr)offsetBufferPtr, out long _);
            
        }
    }
}
