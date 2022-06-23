// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.URLICO
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

namespace DeleteDuplicates
{
  public class URLICO : EqualsBase<URLICO>, IEquatableProperty
  {
    public override string EProperty() => this.ЮрЛицо;

    public override string EPropertyName() => "ЮрЛицо";

    public string ЮрЛицо { get; set; }

    public string Руководитель { get; set; }

    public string Должностьруководителя { get; set; }
  }
}
