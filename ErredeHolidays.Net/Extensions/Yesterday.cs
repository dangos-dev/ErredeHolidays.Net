namespace ErredeHolidays.Net.Extensions;
public class Yesterday {
  public static bool WasHoliday() => DateOnly.FromDateTime( DateTime.Today.AddDays( -1 ) ).IsHoliday();
}
