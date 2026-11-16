using System;
using MemoryLifecycle.Demos;

namespace MemoryLifecycle;

public static class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация жизненного цикла памяти ===");
        DisposeDemos.RunCorrectScenario();
        DisposeDemos.RunIncorrectScenario();
        DisposeDemos.RunUsingDeclarationScenario();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}