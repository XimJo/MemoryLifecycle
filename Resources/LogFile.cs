using System;
using System.IO;

namespace MemoryLifecycle.Resources;

public class LogFile : IDisposable
{
    private StreamWriter? _writer;
    private readonly string _path;
    private bool _disposed = false;

    public LogFile(string path)
    {
        _path = path;
        _writer = new StreamWriter(path, append: true);
        Console.WriteLine($"[LogFile] Конструктор: файл '{path}' открыт.");
    }

    public void Write(string message)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _writer?.WriteLine(message);
        _writer?.Flush();
        Console.WriteLine($"[LogFile] Записано в файл: {message}");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _writer?.Dispose();
            _writer = null;
        }
        _disposed = true;
        Console.WriteLine($"[LogFile] Dispose: файл '{_path}' закрыт.");
    }

    ~LogFile()
    {
        if (!_disposed)
        {
            Console.WriteLine($"[LogFile] Финализатор!: файл '{_path}' не был закрыт явно. Освобождаем через GC.");
            Dispose(false);
        }
    }
}