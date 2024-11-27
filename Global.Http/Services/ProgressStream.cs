using System.IO;

namespace Global.Http.Services;

public class ProgressStream : Stream
{

    private readonly Stream _baseStream;
    private readonly long _totalBytes;
    private readonly Action<long, long> _progressCallback;
    private long _bytesSent;

    public ProgressStream(Stream baseStream, long totalBytes, Action<long, long> progressCallback)
    {
        _baseStream = baseStream;
        _totalBytes = totalBytes;
        _progressCallback = progressCallback;
    }

    public override bool CanRead => _baseStream.CanRead;
    public override bool CanSeek => _baseStream.CanSeek;
    public override bool CanWrite => _baseStream.CanWrite;
    public override long Length => _baseStream.Length;

    public override long Position
    {
        get => _baseStream.Position;
        set => _baseStream.Position = value;
    }

    public override void Flush() => _baseStream.Flush();

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _baseStream.Read(buffer, offset, count);
        _bytesSent += bytesRead;
        _progressCallback?.Invoke(_bytesSent, _totalBytes);
        return bytesRead;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _baseStream.Write(buffer, offset, count);
        _bytesSent += count;
        _progressCallback?.Invoke(_bytesSent, _totalBytes);
    }

    public override long Seek(long offset, SeekOrigin origin) => _baseStream.Seek(offset, origin);

    public override void SetLength(long value) => _baseStream.SetLength(value);
}