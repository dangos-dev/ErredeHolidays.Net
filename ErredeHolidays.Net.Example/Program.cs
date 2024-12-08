using ErredeHolidays.Net.Example;
using Spectre.Console;
using System.Globalization;
using System.Text;

/* Jabes Rivas. Dangos.dev 🍡

 * Secciones (usa ctrl + f para navegar):
 *  §1 Obtener todos los feriados de un año
 *  §2 Teniendo una fecha, determinar si corresponde a un feriado
 *  §3 Confirmar si hoy es feriado
 *  §4 Confirmar si ayer fue feriado
 *  §5 Confirmar si mañana será feriado
*/

Console.Title = "ErredeHolidays.Net Example🍡";

CultureInfo.CurrentCulture = new CultureInfo( "es-DO" );
Console.OutputEncoding = Encoding.UTF8;

Emoji.Remap( "dominican-republic", "🇩🇴" );

AnsiConsole.Clear();
DangoConsole.ShowMainMenu();