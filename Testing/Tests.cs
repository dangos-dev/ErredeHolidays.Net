using ErredeHolidays.Net;
using ErredeHolidays.Net.Enums;
using ErredeHolidays.Net.Extensions;

namespace Testing;

[TestFixture]
public class Tests {

  DateOnly testDate = new();

  [Test]
  public void NewYear_IsImmovable() {
    testDate = new( 2024, 1, 1 ); // Año nuevo, inamovible

    Holiday holiday = new(HolidayIdentity.NewYear, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate ), "El día de año nuevo debería ser inamovible" );
  }

  [Test]
  public void Holiday_MovesToPreviousMonday_WhenTuesdayOrWednesday() {
    testDate = new( 2022, 1, 26 ); // Natalicio de Juan Pablo Duarte, miércoles

    Holiday holiday = new(HolidayIdentity.NatalicioDeJuanPabloDuarte, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate.AddDays( -2 ) ), "El feriado debería moverse al siguiente lunes si cae martes/miércoles" );
  }

  [Test]
  public void Holiday_MovesToNextMonday_WhenThursdayOrFriday() {
    testDate = new( 2020, 11, 06 ); // Día de la constitución, viernes

    Holiday holiday = new(HolidayIdentity.DiaDeLaConstitucion, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate.AddDays( 3 ) ), "El feriado debería moverse al siguiente lunes si cae jueves/viernes" );
  }

  [Test]
  public void DiaDelTrabajo_MovesToMonday_WhenSunday() {
    testDate = new( 2022, 5, 1 ); // Domingo

    Holiday holiday = new(HolidayIdentity.DiaDelTrabajo, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate.AddDays( 1 ) ), "El día del trabajo debe moverse al próximo lunes si cae domingo" );

  }

  [Test]
  public void DiaDeLaRestauracion_StaysOnOriginalDate_InElectionYear() {
    testDate = new( 2020, 8, 16 ); // Miercoles, año constitucional

    Holiday holiday = new(HolidayIdentity.DiaDeLaRestauracion, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate ), "El día de la restauración no debe moverse si es año constitucional" );
  }

  [Test]
  public void DiaDeLaRestauracion_MoveOnMonday_NotElectionYear() {
    testDate = new( 2019, 8, 16 ); // Jueves, año no constitucional

    Holiday holiday = new(HolidayIdentity.DiaDeLaRestauracion, testDate);
    Assert.That( holiday.EffectiveDate, Is.EqualTo( testDate.AddDays( 3 ) ), "Si no es año constitucional, entonces el día de la restauración se comporta como los demás" );
  }

  [Test]
  public void CorpusChristi_HolidayDate_MultipleYears() {
    var corpusChristi2020 = Holiday.GetHoliday( HolidayIdentity.CorpusChristi, 2020 );
    var corpusChristi2021 = Holiday.GetHoliday( HolidayIdentity.CorpusChristi, 2021 );
    var corpusChristi2022 = Holiday.GetHoliday( HolidayIdentity.CorpusChristi, 2022 );
    var corpusChristi2023 = Holiday.GetHoliday( HolidayIdentity.CorpusChristi, 2023 );
    var corpusChristi2024 = Holiday.GetHoliday( HolidayIdentity.CorpusChristi, 2024 );

    Assert.Multiple( () => {
      Assert.That( corpusChristi2020.EffectiveDate, Is.EqualTo( new DateOnly( 2020, 6, 11 ) ), "Para el año 2020, el Corpus Christi fue el 11 de junio" );
      Assert.That( corpusChristi2021.EffectiveDate, Is.EqualTo( new DateOnly( 2021, 6, 3 ) ), "Para el año 2021, el Corpus Christi fue el 3 de junio" );
      Assert.That( corpusChristi2022.EffectiveDate, Is.EqualTo( new DateOnly( 2022, 6, 16 ) ), "Para el año 2022, el Corpus Christi fue el 16 de junio" );
      Assert.That( corpusChristi2023.EffectiveDate, Is.EqualTo( new DateOnly( 2023, 6, 8 ) ), "Para el año 2023, el Corpus Christi fue el 8 de junio" );
      Assert.That( corpusChristi2024.EffectiveDate, Is.EqualTo( new DateOnly( 2024, 5, 30 ) ), "Para el año 2024, el Corpus Christi fue el 30 de mayo" );
    } );
  }

  [Test]
  public void ViernesSanto_HolidayDate_MultipleYears() {
    var viernesSanto2020 = Holiday.GetHoliday(HolidayIdentity.ViernesSanto, 2020);
    var viernesSanto2021 = Holiday.GetHoliday(HolidayIdentity.ViernesSanto, 2021);
    var viernesSanto2022 = Holiday.GetHoliday(HolidayIdentity.ViernesSanto, 2022);
    var viernesSanto2023 = Holiday.GetHoliday(HolidayIdentity.ViernesSanto, 2023);
    var viernesSanto2024 = Holiday.GetHoliday(HolidayIdentity.ViernesSanto, 2024);

    Assert.Multiple( () => {
      Assert.That( viernesSanto2020.EffectiveDate, Is.EqualTo( new DateOnly( 2020, 4, 10 ) ), "Para el año 2020, el Viernes Santo fue el 10 de abril" );
      Assert.That( viernesSanto2021.EffectiveDate, Is.EqualTo( new DateOnly( 2021, 4, 2 ) ), "Para el año 2021, el Viernes Santo fue el 2 de abril" );
      Assert.That( viernesSanto2022.EffectiveDate, Is.EqualTo( new DateOnly( 2022, 4, 15 ) ), "Para el año 2022, el Viernes Santo fue el 15 de abril" );
      Assert.That( viernesSanto2023.EffectiveDate, Is.EqualTo( new DateOnly( 2023, 4, 7 ) ), "Para el año 2023, el Corpus Christi fue el 7 de abril" );
      Assert.That( viernesSanto2024.EffectiveDate, Is.EqualTo( new DateOnly( 2024, 3, 29 ) ), "Para el año 2024, el Corpus Christi fue el 29 de marzo" );
    } );
  }


  [Test]
  public void IsHoliday_ReturnsTrue_ForNewYearsDay() {
    testDate = new( 2024, 1, 1 ); // Año nuevo
    bool isHoliday = testDate.IsHoliday();

    Assert.That( isHoliday, Is.True, "El año nuevo debería ser considerado un día feriado" );
  }

  [Test]
  public void IsHoliday_ReturnsFalse_ForNonHoliday() {
    testDate = new( 2024, 1, 2 ); // No es feriado
    bool isHoliday = testDate.IsHoliday();

    Assert.That( isHoliday, Is.False, "La fecha indicada no debería ser un feriado" );
  }
}