// Decompiled with JetBrains decompiler
// Type: DeleteDuplicates.EqualsBase`1
// Assembly: DeleteDuplicates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D94614E3-449E-4FD4-9514-360959F04117
// Assembly location: C:\Users\ConversionPRO1\Desktop\Инструкция\Ранняя версия\Release\DeleteDuplicates.exe

using System;

namespace DeleteDuplicates
{
  public abstract class EqualsBase<T> : IEquatable<T> where T : class, IEquatableProperty
  {
    public abstract string EProperty();

    public abstract string EPropertyName();

    public bool Equals(T other)
    {
      if ((object) other == null)
        return false;
      if (this == (object) other)
        return true;
      return !(this.GetType() != other.GetType()) && this.EProperty()?.ToUpper() == other.EProperty()?.ToUpper();
    }

    public override int GetHashCode() => (this.EProperty() == null ? (object) (string) null : (object) this.EProperty().ToUpper()).GetHashCode();

    public static bool operator ==(EqualsBase<T> lhs, EqualsBase<T> rhs)
    {
      if ((object) lhs != null)
        return lhs.Equals((object) rhs);
      return (object) rhs == null;
    }

    public static bool operator !=(EqualsBase<T> lhs, EqualsBase<T> rhs) => !(lhs == rhs);

    public override bool Equals(object other) => this.Equals(other as T);
  }
}
