using ErredeHolidays.Net.Enums;

namespace ErredeHolidays.Net.Interfaces;

public interface IHoliday {

  HolidayIdentity Title { get; }
  string Description { get; }
  DateOnly OriginalDate { get; }
  DateOnly EffectiveDate { get; }
  bool IsImmovable { get; }

}
