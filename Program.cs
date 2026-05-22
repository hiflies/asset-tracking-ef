using AssetTracking.Menu;
using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = config.GetConnectionString("DefaultConnection");
if (connectionString == null)
{
    ConsoleHelper.WriteError("Please set connection string in the appsettings.json file");
    return;
}

var context = new AssetDbContext(connectionString);
var running = true;
while (running)
{
    Console.Clear();

    var selection = AnsiConsole.Prompt(
        SpectreConsoleHelper.CreateSelectionPrompt<IMenu>("What would you like to do?")
            .AddChoices(new AddAssetMenu(context))
            .AddChoices(new ShowAllMenu(context))
            .AddChoices(new UpdateAssetMenu(context))
            .AddChoices(new DeleteAssetMenu(context))
            .AddChoices(new SearchAssetMenu(context))
            .AddChoices(new ShowOfficeMenu(context))
            .AddChoices(new ExitMenu(() => running = false))
            .UseConverter(menu => menu.Title)
    );

    selection.Show();
}