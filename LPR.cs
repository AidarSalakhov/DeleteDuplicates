// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.LPR
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

namespace DeleteDuplicates
{
  public class LPR : EqualsBase<LPR>, IEquatableProperty
  {
    public override string EProperty() => this.ЛПР;

    public override string EPropertyName() => "ЛПР";

    public string ЛПР { get; set; }

    public string ДолжностьЛПР { get; set; }

    public string EmailЛПР { get; set; }

    public string ТелЛПР { get; set; }

    public string СоцсетиЛПР { get; set; }
  }
}
