using System;
using System.IO;
using MemoryLifecycle.Resources;

namespace MemoryLifecycle.Demos;

public static class DisposeDemos
{
    public static void RunCorrectScenario()
    {
        Console.WriteLine("\n--- Демо 1: Корректный сценарий (using блок) ---");
        const string path = "correct_log.txt";
        if (File.Exists(path)) File.Delete(path);

        using (var log = new LogFile(path))
        {
            log.Write("Это правильная запись.");
            log.Write("Ресурс будет закрыт автоматически.");
        }

        Console.WriteLine("Конец блока using.");
    }

    public static void RunIncorrectScenario()
    {
        Console.WriteLine("\n--- Демо 2: Некорректный сценарий (без using) ---");
        const string path = "incorrect_log.txt";

        if (File.Exists(path)) File.Delete(path);

        CreateLogFileWithoutDispose(path);
        Console.WriteLine("Метод завершен, ссылка потеряна. Файл все еще открыт.");
        Console.WriteLine("Вызываем GC.Collect()...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Сборка мусора завершена.");
    }

    private static void CreateLogFileWithoutDispose(string path)
    {
        var log = new LogFile(path);
        log.Write("Эта запись сделана в 'забытом' файле.");
        // Забыли вызвать log.Dispose();
    }

    public static void RunUsingDeclarationScenario()
    {
        Console.WriteLine("\n--- Демо 3: Using-декларация (без скобок) ---");
        const string path = "declaration_log.txt";

        if (File.Exists(path)) File.Delete(path);

        using var log = new LogFile(path);
        log.Write("Запись через using-декларацию.");
        Console.WriteLine("Строка внутри метода.");
    }
}