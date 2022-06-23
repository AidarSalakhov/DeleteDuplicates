// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.Processing
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

using ExcelDataReader;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DeleteDuplicates
{
  public static class Processing
  {
    public static void Process()
    {
      Log.Information("Этап 1");
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      List<Model> modelList1 = new List<Model>();
      if (!Directory.Exists("Исходный"))
      {
        int foregroundColor = (int) Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Не найдена папка \"Исходный\"");
        Console.ForegroundColor = (ConsoleColor) foregroundColor;
      }
      else
      {
        IEnumerable<string> source1 = Directory.EnumerateFiles("Исходный", "*.xlsx", SearchOption.TopDirectoryOnly);
        if (source1.Count<string>() != 1)
        {
          int foregroundColor = (int) Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("В папке \"Исходный\" должен быть один файл с расширением .xlsx");
          Console.ForegroundColor = (ConsoleColor) foregroundColor;
        }
        else
        {
          using (FileStream fileStream = File.Open(source1.First<string>(), FileMode.Open, FileAccess.Read))
          {
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader((Stream) fileStream))
            {
              int num = 0;
              while (reader.Read())
              {
                if (num == 0)
                {
                  for (int index = 0; index < reader.FieldCount; ++index)
                  {
                    object obj = reader.GetValue(index);
                    string str = new Regex("[ |\\d|\\-|\\.]").Replace(obj == null ? (string) null : obj.ToString(), "").Replace("№", "Номер");
                    dictionary.Add(index, str);
                  }
                }
                else
                {
                  Model model = new Model();
                  model.Id = num;
                  for (int index = 0; index < reader.FieldCount; ++index)
                  {
                    string source2 = (reader.GetValue(index)?.ToString() ?? "").Replace("\n", "");
                    if (!source2.Contains<char>('¶') && !source2.Contains("\r"))
                      source2.Contains("Федин");
                    model.SetProperty(dictionary[index], source2);
                  }
                  modelList1.Add(model);
                }
                ++num;
              }
            }
          }
          Log.Information("Этап 2");
          List<Model> resultData = new List<Model>();
          List<Model> modelList2 = new List<Model>();
          Model model1 = modelList1[0];
          while (modelList1.Count > 0)
          {
            bool flag = false;
            for (int index = 1; index < modelList1.Count; ++index)
            {
              Model other = modelList1[index];
              if (other.Id != model1.Id && model1.Слияние(other) != null)
              {
                modelList2.Add(other);
                flag = true;
              }
            }
            if (flag)
            {
              foreach (Model model2 in modelList2)
                modelList1.Remove(model2);
              modelList2.Clear();
            }
            else
            {
              List<CONTLICO> contlicoList = new List<CONTLICO>();
              foreach (IGrouping<string, CONTLICO> source3 in model1.Контактноелицо.GroupBy<CONTLICO, string>((Func<CONTLICO, string>) (p => p.EProperty())))
              {
                string key = source3.Key;
                List<CONTLICO> collection = source3.ToList<CONTLICO>().CompressList<CONTLICO>(key);
                contlicoList.AddRange((IEnumerable<CONTLICO>) collection);
              }
              model1.Контактноелицо = contlicoList;
              List<URLICO> urlicoList = new List<URLICO>();
              foreach (IGrouping<string, URLICO> source4 in model1.ЮрЛицо.GroupBy<URLICO, string>((Func<URLICO, string>) (p => p.EProperty())))
              {
                string key = source4.Key;
                List<URLICO> collection = source4.ToList<URLICO>().CompressList<URLICO>(key);
                urlicoList.AddRange((IEnumerable<URLICO>) collection);
              }
              model1.ЮрЛицо = urlicoList;
              List<LPR> lprList = new List<LPR>();
              foreach (IGrouping<string, LPR> source5 in model1.ЛПР.GroupBy<LPR, string>((Func<LPR, string>) (p => p.EProperty())))
              {
                string key = source5.Key;
                List<LPR> collection = source5.ToList<LPR>().CompressList<LPR>(key);
                lprList.AddRange((IEnumerable<LPR>) collection);
              }
              model1.ЛПР = lprList;
              resultData.Add(model1);
              modelList1.Remove(model1);
              if (modelList1.Count > 0)
                model1 = modelList1[0];
            }
          }
          Log.Information("Этап 3");
          Log.Information(string.Format("          Консолидировано франшиз {0}", (object) resultData.Count));
          for (int index = 0; index < resultData.Count; ++index)
          {
            Model model3 = resultData[index];
            Log.Information(string.Format("\n\r====== ФРАНШИЗА №{0} {1}", (object) (index + 1), (object) model3.Франшиза.Представление()));
            foreach (PropertyInfo property in model3.GetType().GetProperties())
            {
              if (!(property.Name == "Номер") && !(property.Name == "Id") && !(property.Name == "Франшиза"))
              {
                object obj = property.GetValue((object) model3);
                Log.Information("*" + property.Name);
                switch (obj)
                {
                  case List<string> p:
                    string str1 = p.Представление() ?? "";
                    if (!string.IsNullOrEmpty(str1))
                    {
                      Log.Information("\t" + str1);
                      continue;
                    }
                    continue;
                  case IEnumerable<IEquatableProperty> collection:
                    string str2 = new List<object>((IEnumerable<object>) collection).Представление() ?? "";
                    if (!string.IsNullOrEmpty(str2))
                    {
                      Log.Information(str2 ?? "");
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
          Log.Information("Этап 4");
          Processing.ToExcel(resultData);
        }
      }
    }

    public static void ToExcel(List<Model> resultData)
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      for (int index = 0; index < resultData.Count; ++index)
      {
        foreach (PropertyInfo property in typeof (Model).GetProperties())
        {
          string name = property.Name;
          switch (property.GetValue((object) resultData[index]))
          {
            case List<string> stringList:
              if (dictionary.ContainsKey(name))
              {
                int val1 = dictionary[name];
                dictionary[name] = Math.Max(val1, stringList.Count);
                break;
              }
              dictionary.Add(property.Name, stringList.Count);
              break;
            case IEnumerable<IEquatableProperty> source:
              if (dictionary.ContainsKey(name))
              {
                int val1 = dictionary[name];
                dictionary[name] = Math.Max(val1, source.Count<IEquatableProperty>());
                break;
              }
              dictionary.Add(property.Name, source.Count<IEquatableProperty>());
              break;
          }
        }
      }
      string str1 = "";
      foreach (string key in dictionary.Keys)
      {
        for (int index = 1; index <= dictionary[key]; ++index)
        {
          string str2 = "";
          if (dictionary[key] > 1)
            str2 = index.ToString();
          str1 = str1 + ";" + key + str2;
          if (!(key == "ЮрЛицо"))
          {
            if (!(key == "Контактноелицо"))
            {
              if (key == "ЛПР")
              {
                str1 = str1 + ";ДолжностьЛПР" + str2;
                str1 = str1 + ";EmailЛПР" + str2;
                str1 = str1 + ";ТелЛПР" + str2;
                str1 = str1 + ";СоцсетиЛПР" + str2;
              }
            }
            else
            {
              str1 = str1 + ";ТелефонКЛ" + str2;
              str1 = str1 + ";ДолжностьКЛ" + str2;
            }
          }
          else
          {
            str1 = str1 + ";Руководитель" + str2;
            str1 = str1 + ";Должностьруководителя" + str2;
          }
        }
      }
      string str3 = str1.Substring(1);
      if (!Directory.Exists("Результат"))
        Directory.CreateDirectory("Результат");
      using (StreamWriter streamWriter = new StreamWriter("Результат\\Результат.csv", false, Encoding.UTF8))
      {
        streamWriter.WriteLine(str3);
        foreach (Model model in resultData)
        {
          string str4 = "";
          foreach (PropertyInfo property in model.GetType().GetProperties())
          {
            string name = property.Name;
            object obj = property.GetValue((object) model);
            switch (obj)
            {
              case List<string> stringList:
                foreach (string str5 in stringList)
                  str4 = str4 + ";" + str5;
                str4 += new string(';', dictionary[name] - stringList.Count);
                break;
              case IEnumerable<IEquatableProperty> source:
                foreach (IEquatableProperty o in source)
                  str4 = str4 + ";" + o.ObjectToString();
                str4 += new string(';', (dictionary[name] - source.Count<IEquatableProperty>()) * ((IEnumerable<PropertyInfo>) obj.GetType().GetGenericArguments()[0].GetProperties()).Count<PropertyInfo>());
                break;
            }
          }
          string str6 = str4.Substring(1);
          streamWriter.WriteLine(str6);
        }
      }
    }
  }
}
