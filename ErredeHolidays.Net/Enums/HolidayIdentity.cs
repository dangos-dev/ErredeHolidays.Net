using System.ComponentModel;
using System.Reflection;

namespace ErredeHolidays.Net.Enums;

public enum HolidayIdentity {

  [Description("Año Nuevo")]
  NewYear,

  [Description("Santos Reyes")]
  SantosReyes,

  [Description("Nuestra Señora de la Altagracia")]
  NuestraSenoraDeLaAltagracia,

  [Description("Natalicio de Juan Pablo Duarte")]
  NatalicioDeJuanPabloDuarte,

  [Description("Día de la Independencia Nacional")]
  DiaDeLaIndependenciaNacional,

  [Description("Día del Trabajo")]
  DiaDelTrabajo,

  [Description("Viernes Santo")]
  ViernesSanto,

  [Description("Corpus Christi")]
  CorpusChristi,

  [Description("Día de la Restauración")]
  DiaDeLaRestauracion,

  [Description("Nuestra Señora de las Mercedes")]
  NuestraSenoraDeLasMercedes,

  [Description("Día de la Constitución")]
  DiaDeLaConstitucion,

  [Description("Día de Navidad")]
  DiaDeNavidad
}

public static class EnumExtensions {
  public static string GetDescription( this Enum value ) {
    Type type = value.GetType();
    string name = Enum.GetName(type, value);
    if ( name != null ) {
      FieldInfo field = type.GetField(name);
      if ( field != null ) {
        DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        if ( attr != null ) {
          return attr.Description;
        }
      }
    }
    return null;
  }
}
