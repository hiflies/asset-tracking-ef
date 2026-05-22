using AssetTracking.Models;
using AssetTracking.Utils;
using Spectre.Console;

namespace AssetTracking.Menu;

public abstract class ListAssetMenu: IMenu
{
    public abstract string Title { get; }
    public abstract void Show();

    protected void ShowAssets(IEnumerable<Asset> assets)
    {
        CurrencyConverter.FetchRates();

        var table = new Table()
            .AddColumns(
                "Id",
                "Asset",
                "Brand",
                "Model",
                "Office",
                "Price (USD)",
                "Price (Local)",
                "Purchase Date",
                "Warranty Exp."
            )
            .RoundedBorder()
            .BorderColor(Color.Blue);

        foreach (var asset in assets)
        {
            var expirationDate = asset.PurchaseDate.AddYears(3);
            var today = DateTime.Now;
            var remainingTime = expirationDate - today;
            var remainingMonths = remainingTime.TotalDays / 30;
            var color = remainingMonths switch
            {
                < 0 => "white",
                < 3 => "red",
                < 6 => "yellow",
                _ => "green"
            };

            table.AddRow(
                SpectreConsoleHelper.ColorRow(
                    color,
                    asset.Id.ToString(),
                    asset.Type,
                    asset.Brand,
                    asset.Model,
                    asset.Office.Name,
                    "$ " + asset.Price.ToString("N2"),
                    asset.Office.Currency.Symbol + " " + CurrencyConverter
                        .Convert(asset.Price, "USD", asset.Office.Currency.Name)
                        .ToString("N2"),
                    asset.PurchaseDate.ToString("yyyy-MM-d"),
                    asset.WarrantyExpirationDate.ToString("yyyy-MM-d")
                )
            );
        }

        AnsiConsole.Write(table);
    }
}