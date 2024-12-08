using ErredeHolidays.Net.Extensions;
using ErredeHolidays.Net.Interfaces;
using Spectre.Console;

namespace ErredeHolidays.Net.Example; 
public class DangoConsole {

  public static void ShowMainMenu() {

    AnsiConsole.Clear();
    AnsiConsole.MarkupLine( "Dangos.dev :dango:" );
    AnsiConsole.WriteLine();

    AnsiConsole.Write( new Rule( "[default on blue][black] ErredeHolidays.Net Example [/][/]:dango:" ).Centered() );

    string seleccionMenuPrincipal = GetSeleccionMenuPrincipal();

    switch ( seleccionMenuPrincipal ) {
      case "Lista de dias feriados":
        AnsiConsole.Clear();
        ListaFeriados();
        break;

      case "Verificar si un día es feriado":
        AnsiConsole.Clear();
        DateOnly checkDay = AnsiConsole.Prompt(new TextPrompt<DateOnly>("Fecha a validar [[MM/dd/yyyy]]"));
        VerificarDia( checkDay );
        break;

      case "¿Hoy es feriado?":
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(
            Today.IsHoliday() // §3 Confirmar si hoy es feriado
                ? $"[green]Si, hoy es feriado.[/]"
                : $"[red]No, hoy no es feriado.[/]"
        );
        
        Retornar();
        break;

      case "¿Ayer fue feriado?":
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(
            Yesterday.WasHoliday()  // §4 Confirmar si ayer fue feriado
                ? $"[green]Si, ayer fue feriado.[/]"
                : $"[red]No, ayer no fue feriado.[/]"
        );

        Retornar();
        break;

      case "¿Mañana será feriado?":
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(
            Tomorrow.WillBeHoliday() // §5 Confirmar si mañana será feriado
                ? $"[green]Si, mañana será feriado.[/]"
                : $"[red]No, mañana no será feriado.[/]"
        );

        DateOnly feriadoConstitucion = Holiday.GetHoliday(Enums.HolidayIdentity.DiaDeLaRestauracion, 2021).EffectiveDate;

        Retornar();
        break;

      case "Salir":
        Environment.Exit( 0 );
        break;
    }

    ShowMainMenu();

  }

  private static string GetSeleccionMenuPrincipal() {
    return AnsiConsole.Prompt(
      new SelectionPrompt<string>()
          .MoreChoicesText( "[grey](Utiliza las teclas de dirección para moverte entre las opciones)[/]" )
          .AddChoices( OpcionesMenuPrincipal ) );
  }

  private static readonly string[] OpcionesMenuPrincipal = [
    "Lista de dias feriados",
    "Verificar si un día es feriado",
    "¿Hoy es feriado?",
    "¿Ayer fue feriado?",
    "¿Mañana será feriado?",
    "Salir"
  ];

  private static void ListaFeriados() {
    AnsiConsole.Clear();

    int year = AnsiConsole.Prompt(new TextPrompt<int>("¿De qué año?")
            .DefaultValue(DateTime.Now.Year ));

    // §1 Obtener todos los feriados de un año
    IEnumerable<IHoliday> holidayList = Holiday.GetAll(year);

    string[] columnNames = [
            "Nombre",
            "Fecha efectiva",
            "Fecha original",
            "¿Se cambia?"
          ];

    Table holidayTable = new() { Border = TableBorder.HeavyEdge };

    foreach ( string header in columnNames ) {
      string? headerText = $"[yellow]{header}[/]";
      holidayTable.AddColumn( headerText );
    }

    foreach ( var holiday in holidayList ) {
      string originalDate = holiday.EffectiveDate == holiday.OriginalDate
              ? holiday.OriginalDate.ToString("dddd, dd 'de' MMMM")
              : $"[dim]{holiday.OriginalDate:dddd, dd 'de' MMMM}[/]";

      holidayTable.AddRow(
          holiday.Description.ToString(),
          holiday.EffectiveDate.ToString( "dddd, dd 'de' MMMM" ),
          originalDate,
          holiday.IsImmovable ? "No" : "Si"
      );
    }

    holidayTable.Collapse();
    AnsiConsole.Write( holidayTable );

    Retornar();
  }

  private static void VerificarDia( DateOnly checkDay ) {
    AnsiConsole.MarkupLine(
        checkDay.IsHoliday()
            ? $"[green]El día [default on green][black]{checkDay:dddd, dd 'de' MMMM}[/][/] es feriado.[/]"
            : $"[red]El día [default on red][black]{checkDay:dddd, dd 'de' MMMM}[/][/] no es feriado.[/]"
    );

    Retornar();
  }

  private static void Retornar() {
    AnsiConsole.Write(
        new Rule( "[yellow]Presione [rapidblink][[ENTER ⏎]][/] para regresar al Menu Principal[/]" )
            .RuleStyle( Style.Parse( "silver" ) )
            .Centered()
    );
    AnsiConsole.WriteLine();
    Console.ReadLine();
  }

};



