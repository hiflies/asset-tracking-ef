using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AssetTracking.Menu;

public class ShowOfficeMenu(AssetDbContext context) : IMenu
{
    public string Title => "Show Office";

    public void Show()
    {
        CurrencyConverter.FetchRates();
        
        var offices = context.Offices
            .Include(office => office.Currency)
            .Include(office => office.Assets);

        var table = new Table()
            .AddColumns(
                "Id",
                "Name",
                "Country",
                "Currency",
                "Asset Count",
                "Total Value (USD)",
                "Total Value (Local)"
            )
            .RoundedBorder()
            .BorderColor(Color.Blue);

        var i = 0;
        foreach (var office in offices)
        {
            var price = office.Assets.Sum(asset => asset.Price);
            var count = office.Assets.Count();
            table.AddRow(
                SpectreConsoleHelper.ColorRow(
                    i % 2 == 0 ? "green" : "blue",
                    office.Id.ToString(),
                    office.Name,
                    office.Country,
                    office.Currency.Name,
                    count.ToString(),
                    $"$ {price:N2}",
                    office.Currency.Symbol + " " + CurrencyConverter
                        .Convert(price, "USD", office.Currency.Name)
                        .ToString("N2")
                )
            );

            i++;
        }
        
        AnsiConsole.Write(table);

        ConsoleHelper.AskEnterToContinue();
    }
}