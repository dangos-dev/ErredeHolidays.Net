namespace ErredeHolidays.Net.Extensions;
public class Tomorrow {
  public static bool WillBeHoliday() => DateOnly.FromDateTime( DateTime.Today.AddDays( 1 ) ).IsHoliday();
}
