// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.CONTLICO
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

namespace DeleteDuplicates
{
  public class CONTLICO : EqualsBase<CONTLICO>, IEquatableProperty
  {
    public override string EProperty() => this.Контактноелицо;

    public override string EPropertyName() => "Контактноелицо";

    public string Контактноелицо { get; set; }

    public string ТелефонКЛ { get; set; }

    public string ДолжностьКЛ { get; set; }
  }
}
