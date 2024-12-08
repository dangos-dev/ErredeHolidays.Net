namespace ErredeHolidays.Net;

using ErredeHolidays.Net.Enums;
using ErredeHolidays.Net.Interfaces;

public class Holiday : IHoliday {

  public Holiday( HolidayIdentity title, DateOnly date ) {
    Title = title;
    
    OriginalDate = date;

    if ( HolidayIsImmovable( title, date ) ) {
      EffectiveDate = date;
      IsImmovable = true;
    } else {
      EffectiveDate = MoveToNearestMonday( title, date ); // Se traslada al lunes más cercano si aplica
      IsImmovable = false;
    }
  }

  public HolidayIdentity Title { get; private set; }
  public string Description => Title.GetDescription();
  public DateOnly OriginalDate { get; private set; }
  public DateOnly EffectiveDate { get; private set; }
  public bool IsImmovable { get; private set; }

  /// <summary>
  ///   Devuelve la lista completa de feriados de la República Dominicana.
  /// </summary>
  /// <param name="year">Año para ubicar los días feriados</param>
  /// <returns>Lista de todos los feriados</returns>
  public static IEnumerable<IHoliday> GetAll( int? year = null ) {

    int holidayYear = year ?? DateTime.Today.Year;

    DateOnly easterDate     = GetEasterDate( holidayYear );

    return [
      new Holiday(title: HolidayIdentity.NewYear,                       date: new DateOnly(holidayYear, 1, 1)),
      new Holiday(title: HolidayIdentity.SantosReyes,                   date: new DateOnly(holidayYear, 1, 6)),
      new Holiday(title: HolidayIdentity.NuestraSenoraDeLaAltagracia,   date: new DateOnly(holidayYear, 1, 21)),
      new Holiday(title: HolidayIdentity.NatalicioDeJuanPabloDuarte,    date: new DateOnly(holidayYear, 1, 26)),
      new Holiday(title: HolidayIdentity.DiaDeLaIndependenciaNacional,  date: new DateOnly(holidayYear, 2, 27)),
      new Holiday(title: HolidayIdentity.DiaDelTrabajo,                 date: new DateOnly(holidayYear, 5, 1)),
      new Holiday(title: HolidayIdentity.ViernesSanto,                  date: easterDate.AddDays( -2 )),
      new Holiday(title: HolidayIdentity.CorpusChristi,                 date: easterDate.AddDays(60)),
      new Holiday(title: HolidayIdentity.DiaDeLaRestauracion,           date: new DateOnly(holidayYear, 8, 16)),
      new Holiday(title: HolidayIdentity.NuestraSenoraDeLasMercedes,    date: new DateOnly(holidayYear, 9, 24)),
      new Holiday(title: HolidayIdentity.DiaDeLaConstitucion,           date: new DateOnly(holidayYear, 11, 6)),
      new Holiday(title: HolidayIdentity.DiaDeNavidad,                  date: new DateOnly(holidayYear, 12, 25))
    ];
  }

  public static IHoliday GetHoliday( HolidayIdentity identity, int? yearParameter = null ) {
    return GetAll( yearParameter ).FirstOrDefault( x => x.Title == identity );
  }

  private static DateOnly GetEasterDate( int year ) {
    // Algoritmo de Gauss simplificado para calcular la fecha de Pascua

    int a = year % 19;            // Ubicación del año en el ciclo lunar
    int b = year % 4;             // Determina si el año es bisiesto
    int c = year % 7;             // Información sobre las semanas
    int d = year / 100;           // Para conocer si el año es centenario
    int e = (13 + 8 * d ) / 25;   // Ajuste de las 2 horas del calculo de fase lunar

    int m = d / 4;
    int n = (15 - e + d - m) % 30; // Conversión del año juliano al gregoriano

    int p = (4 + d - m) % 7;
    int q = (19 * a + n) % 30;     // Determina el numero de dias desde el 22 de marzo hasta la primera luna llena
    int r = ( 2 * b + 4 * c + 6 * q + p ) % 7; // Determina el dia de la semana de la primera luna llena

    DateOnly easterDate = new();
    if ( q + r > 9 ) {  // La fecha será en abril
      easterDate = new( year, 4, q + r - 9 );
    } else {            // La fecha será en marzo
      easterDate = new( year, 3, 22 + q + r );
    }

    // Excepción 1: Si la fecha de Pascua es el 26 de abril, se traslada a la semana anterior
    if ( easterDate == new DateOnly( easterDate.Year, 4, 26 ) ) {
      easterDate = easterDate.AddDays( -7 );
    }
    // Excepción 2: Si la fecha de Pascua es el 25 de abril y se cumplen las demas condiciones, se traslada a la semana anterior
    if ( easterDate == new DateOnly( easterDate.Year, 4, 25 ) && r == 6 && a > 10 ) {
      easterDate = easterDate.AddDays( -7 );
    }

    return easterDate;
  }

  /// <summary>
  ///   Obtiene el feriado más cercano a la fecha dada teniendo en cuenta la ley 139-97:
  ///   "Los días feriados que caen entre martes y viernes se trasladarán al lunes más cercano"
  ///   No aplica para los feriados que ya caen en sábado, domingo o lunes.
  /// </summary>
  /// <param name="date"></param>
  /// <returns>Verdadera fecha efectiva</returns>
  private static DateOnly MoveToNearestMonday( HolidayIdentity holiday, DateOnly date ) {

    // Si es el día del trabajo y es domingo, se traslada al lunes
    if ( holiday == HolidayIdentity.DiaDelTrabajo && date.DayOfWeek == DayOfWeek.Sunday ) {
      return date.AddDays( 1 );
    }

    // Si ya es lunes, sábado o domingo no se traslada
    if ( date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ) {
      return date;
    }


    if ( date.DayOfWeek == DayOfWeek.Tuesday || date.DayOfWeek == DayOfWeek.Wednesday ) {
      int currentDayNumber  = (int)date.DayOfWeek;
      int daysAfterMonday   = currentDayNumber == 0 ? 6 : currentDayNumber - 1;
      return date.AddDays( -daysAfterMonday );

    } else if ( date.DayOfWeek == DayOfWeek.Thursday || date.DayOfWeek == DayOfWeek.Friday ) {
      DateOnly nextWeek      = date.AddDays( 7 ); // Avanza una semana

      int nextWeekDayNumber  = (int)nextWeek.DayOfWeek;
      int daysUntilMonday    = nextWeekDayNumber == 0 ? 6 : nextWeekDayNumber - 1;

      return nextWeek.AddDays( -daysUntilMonday );
    } else {
      return date;
    }
  }


  /// <summary>
  ///  Determina si un feriado es inamovible o no según la ley 139-97 Art. 2
  /// </summary>
  /// <param name="holiday"></param>
  /// <returns>Si es inamovible o no</returns>
  private static bool HolidayIsImmovable( HolidayIdentity holiday, DateOnly date ) {

    bool EsInamovibleFijo =
           holiday == HolidayIdentity.NewYear
        || holiday == HolidayIdentity.NuestraSenoraDeLaAltagracia
        || holiday == HolidayIdentity.DiaDeLaIndependenciaNacional
        || holiday == HolidayIdentity.NuestraSenoraDeLasMercedes
        || holiday == HolidayIdentity.ViernesSanto
        || holiday == HolidayIdentity.CorpusChristi
        || holiday == HolidayIdentity.DiaDeNavidad ;

    bool EsDiaRestauracionEnPeriodoConstitucional =
           holiday == HolidayIdentity.DiaDeLaRestauracion
        && ThereIsChangeOfGovernment( date.Year ) ;

    return EsInamovibleFijo || EsDiaRestauracionEnPeriodoConstitucional;

  }

  private static bool ThereIsChangeOfGovernment( int year ) {

    // Verificamos si el año dado coincide con un año de elección
    return year % 4 == 0;
  }

}