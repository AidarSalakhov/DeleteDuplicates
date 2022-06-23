// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.Extention
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DeleteDuplicates
{
  public static class Extention
  {
    public static List<T> CompressList<T>(this List<T> list, string propValue) where T : IEquatableProperty, new()
    {
      PropertyInfo[] properties = typeof (T).GetProperties();
      Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
      foreach (PropertyInfo propertyInfo in properties)
      {
        List<string> stringList = new List<string>();
        dictionary.Add(propertyInfo.Name, stringList);
        foreach (T obj in list)
        {
          string str = (string) propertyInfo.GetValue((object) obj);
          if (!stringList.Contains(str) && str != null)
            stringList.Add(str);
        }
      }
      List<T> objList = new List<T>();
      bool flag = true;
      while (flag)
      {
        T obj = new T();
        flag = false;
        PropertyInfo propertyInfo1 = (PropertyInfo) null;
        foreach (PropertyInfo propertyInfo2 in properties)
        {
          string str1 = obj.EPropertyName();
          if (propertyInfo2.Name == str1)
            propertyInfo1 = propertyInfo2;
          if (propertyInfo2.Name != str1 && dictionary[propertyInfo2.Name].Count > 0 && dictionary[propertyInfo2.Name][0] != null)
          {
            string str2 = dictionary[propertyInfo2.Name][0];
            if (propertyInfo2.Name == str1)
              str2 = propValue;
            propertyInfo2.SetValue((object) obj, (object) str2);
            dictionary[propertyInfo2.Name].Remove(str2);
            flag = true;
          }
        }
        if (flag)
        {
          propertyInfo1.SetValue((object) obj, (object) propValue);
          objList.Add(obj);
        }
      }
      return objList;
    }

    public static string ObjectToString(this object o)
    {
      string str = "";
      foreach (PropertyInfo property in o.GetType().GetProperties())
      {
        object obj = property.GetValue(o) ?? (object) "".ToString();
        str = str + ";" + obj?.ToString();
      }
      return str.Substring(1);
    }

    public static string Представление(this List<string> p) => string.Join("; ", (IEnumerable<string>) p);

    public static string Представление(this List<object> p)
    {
      string str1 = "";
      int num = 0;
      foreach (object obj in p)
      {
        object el = obj;
        string str2 = string.Join(" / ", ((IEnumerable<PropertyInfo>) el.GetType().GetProperties()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (info => string.Format("{0}={1}", (object) info.Name, info.GetValue(el)))));
        if (str2.Length > 0)
          str1 = str1 + string.Format("\t{0} ", (object) num) + str2 + Environment.NewLine;
        ++num;
      }
      if (str1.Length > 0)
        str1 = str1.Substring(0, str1.Length - Environment.NewLine.Length);
      return str1;
    }

    public static string ExtractDomainNameFromURL_Method1(string Url)
    {
      if (!Url.Contains("://"))
        Url = "http://" + Url;
      return new Uri(Url).Host;
    }

    public static string ExtractDomainNameFromURL_Method2(string Url)
    {
      if (Url.Contains("://"))
        Url = Url.Split(new string[1]{ "://" }, 2, StringSplitOptions.None)[1];
      string str = Url.Split('/')[0];
      return str.Substring(str.LastIndexOf('.', str.LastIndexOf('.') - 1) + 1);
    }

    public static string ExtractDomainNameFromURL_Method3(string Url) => Regex.Replace(Url, "^([a-zA-Z]+:\\/\\/)?([^\\/]+)\\/.*?$", "$2");

    public static string GetDomainNameOfUrlString(string urlString)
    {
      string host = new Uri(urlString).Host;
      return host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
    }
  }
}
