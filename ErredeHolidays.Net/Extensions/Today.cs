namespace ErredeHolidays.Net.Extensions;
public class Today {
  public static bool IsHoliday() => DateOnly.FromDateTime(DateTime.Today.Date).IsHoliday();
}
