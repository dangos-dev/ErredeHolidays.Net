
# ErredeHolidays.Net 🇩🇴
[![Nuget](https://img.shields.io/nuget/dt/Errede.Holidays)](https://www.nuget.org/packages/Errede.Holidays)
[![Nuget](https://img.shields.io/nuget/v/Errede.Holidays)](https://www.nuget.org/packages/Errede.Holidays)

 Instala [el paquete](https://www.nuget.org/packages/Errede.Holidays) vía la consola de Package Manager:
```nuget
Install-Package Errede.Holidays -Version 1.0.1
```

O utilizando .NET CLI:
```bash
dotnet add package Errede.Holidays --version 1.0.1
```


## Como usar
Un ejemplo de uso se demuestra en [ErredeHolidays.Net.Example](https://github.com/dangos-dev/ErredeHolidays.Net/tree/master/ErredeHolidays.Net.Example), una aplicación de consola enriquecida con SpectreConsole.
```csharp 
// Obtener todos los feriados del año
IEnumerable<IHoliday> holidayList = Holiday.GetAll();
IEnumerable<IHoliday> nextYearHolidays = Holiday.GetAll(2025);

// Saber si un día específico es feriado
new DateTime(2020, 12, 25).IsHoliday();

// o también...

if ( payDay.IsHoliday() ){
  PayOneDayBefore();
}

// Obtener datos de un feriado específico
DateOnly feriadoConstitucion = Holiday.GetHoliday(Enums.HolidayIdentity.DiaDeLaRestauracion, 2021).EffectiveDate;

// Validar el hoy, ayer y mañana
Today.IsHoliday();
Yesterday.WasHoliday();
Tomorrow.WillBeHoliday();
```