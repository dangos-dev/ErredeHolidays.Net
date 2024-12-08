using ErredeHolidays.Net;

namespace ErredeHolidays.Net.Extensions;
public static class HolidayExtension {

  public static bool IsHoliday( this DateOnly dateToCheck ) {
    return Holiday.GetAll( dateToCheck.Year ).Any( x => x.EffectiveDate.Equals( dateToCheck ) );
  }

}
