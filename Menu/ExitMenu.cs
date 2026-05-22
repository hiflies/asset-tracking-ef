using AssetTracking.Utils;
using Spectre.Console;

namespace AssetTracking.Menu;

public class ExitMenu(Action onShow) : IMenu
{
    public string Title => "Exit";

    public void Show()
    {
        AnsiConsole.Write(new Markup("[green]Good bye[/] :waving_hand:"));
        onShow.Invoke();
    }
}