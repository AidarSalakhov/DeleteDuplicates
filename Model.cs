// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.Model
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeleteDuplicates
{
  public class Model : IEquatable<Model>
  {
    public void SetProperty(string name, string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      if (name == "Номер")
      {
        this.Номер = value;
      }
      else
      {
        switch (name)
        {
          case "EmailЛПР":
          case "ДолжностьЛПР":
          case "ЛПР":
          case "СоцсетиЛПР":
          case "ТелЛПР":
            this.SetObjectProp<LPR>(this.GetType().GetProperty("ЛПР").GetValue((object) this) as List<LPR>, name, value);
            break;
          case "ДолжностьКЛ":
          case "Контактноелицо":
          case "ТелефонКЛ":
            this.SetObjectProp<CONTLICO>(this.GetType().GetProperty("Контактноелицо").GetValue((object) this) as List<CONTLICO>, name, value);
            break;
          case "Должностьруководителя":
          case "Руководитель":
          case "ЮрЛицо":
            this.SetObjectProp<URLICO>(this.GetType().GetProperty("ЮрЛицо").GetValue((object) this) as List<URLICO>, name, value);
            break;
          default:
            List<string> stringList = this.GetType().GetProperty(name).GetValue((object) this) as List<string>;
            if (stringList.Contains(value))
              break;
            stringList.Add(value);
            break;
        }
      }
    }

    public Model Слияние(Model other)
    {
      if (!this.Equals(other))
        return (Model) null;
      foreach (PropertyInfo property in this.GetType().GetProperties())
      {
        if (!(property.Name == "Номер") && !(property.Name == "Id"))
        {
          object obj = property.GetValue((object) other);
          if (obj.GetType() == typeof (List<string>))
          {
            foreach (string str in obj as List<string>)
              this.SetProperty(property.Name, str);
          }
          else
          {
            switch (obj)
            {
              case List<URLICO> urlicoList:
                using (List<URLICO>.Enumerator enumerator = urlicoList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                    this.ЮрЛицо.Add(enumerator.Current);
                  continue;
                }
              case List<CONTLICO> contlicoList:
                using (List<CONTLICO>.Enumerator enumerator = contlicoList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                    this.Контактноелицо.Add(enumerator.Current);
                  continue;
                }
              case List<LPR> lprList:
                using (List<LPR>.Enumerator enumerator = lprList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                    this.ЛПР.Add(enumerator.Current);
                  continue;
                }
              default:
                continue;
            }
          }
        }
      }
      return this;
    }

    private void SetObjectProp<T>(List<T> list, string name, string value) where T : class, IEquatableProperty, new()
    {
      T obj;
      if (list.Count == 0 || list[0].EPropertyName() == name)
      {
        obj = new T();
        list.Add(obj);
      }
      else
        obj = list[list.Count - 1];
      obj.GetType().GetProperty(name).SetValue((object) obj, (object) value);
    }

    public bool Equals(Model other)
    {
      IEnumerable<string> source1 = this.Телефон.Intersect<string>((IEnumerable<string>) other.Телефон).Select<string, string>((Func<string, string>) (tel => tel));
      IEnumerable<string> source2 = this.Сайт.Intersect<string>((IEnumerable<string>) other.Сайт).Select<string, string>((Func<string, string>) (site => site));
      return source1.Count<string>() > 0 || source2.Count<string>() > 0;
    }

    public int Id { get; set; }

    public string Номер { get; set; }

    public List<string> Франшиза { get; set; } = new List<string>();

    public List<string> Франшизвсети { get; set; } = new List<string>();

    public List<string> Своихточек { get; set; } = new List<string>();

    public List<string> Срококупаемости { get; set; } = new List<string>();

    public List<string> Статус { get; set; } = new List<string>();

    public List<string> Обзвон { get; set; } = new List<string>();

    public List<string> Город { get; set; } = new List<string>();

    public List<string> Адрес { get; set; } = new List<string>();

    public List<URLICO> ЮрЛицо { get; set; } = new List<URLICO>();

    public List<string> ИНН { get; set; } = new List<string>();

    public List<string> Описание { get; set; } = new List<string>();

    public List<string> Специализация { get; set; } = new List<string>();

    public List<string> Источниксбора { get; set; } = new List<string>();

    public List<string> Инвестиции { get; set; } = new List<string>();

    public List<string> Сайт { get; set; } = new List<string>();

    public List<string> Телефон { get; set; } = new List<string>();

    public List<string> Email { get; set; } = new List<string>();

    public List<CONTLICO> Контактноелицо { get; set; } = new List<CONTLICO>();

    public List<LPR> ЛПР { get; set; } = new List<LPR>();

    public List<string> Vkontakte { get; set; } = new List<string>();

    public List<string> Instagram { get; set; } = new List<string>();

    public List<string> Facebook { get; set; } = new List<string>();

    public List<string> YouTube { get; set; } = new List<string>();

    public List<string> Однокласники { get; set; } = new List<string>();

    public List<string> Telegram { get; set; } = new List<string>();

    public List<string> WhatsApp { get; set; } = new List<string>();

    public List<string> Vkontakteадминистратор { get; set; } = new List<string>();

    public List<string> ДопИфнормация { get; set; } = new List<string>();
  }
}
