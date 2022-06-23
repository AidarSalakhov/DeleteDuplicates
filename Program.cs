// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.Program
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

using Serilog;
using System;
using System.IO;
using System.Text;

namespace DeleteDuplicates
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      if (File.Exists(".\\Протокол.txt"))
        File.Delete(".\\Протокол.txt");
      Log.Logger = (ILogger) new LoggerConfiguration().WriteTo.File(".\\Протокол.txt", outputTemplate: "{Message}{NewLine}{Exception}").CreateLogger();
      bool flag = false;
      try
      {
        Processing.Process();
      }
      catch (Exception ex)
      {
        Log.Error(ex, "========= Ошибка при выполнении алгоритма!");
        flag = true;
      }
      Console.SetOut((TextWriter) new StreamWriter(Console.OpenStandardOutput())
      {
        AutoFlush = true
      });
      Console.OutputEncoding = Encoding.UTF8;
      if (flag)
        Console.WriteLine("Произошла ошибка. Смотри файл Протокол.txt");
      Console.WriteLine("Обработка завершена. Нажмите любую клавишу для выхода из программы.");
      Console.ReadKey();
    }
  }
}
