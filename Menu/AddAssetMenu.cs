using AssetTracking.Models;
using AssetTracking.Services;
using AssetTracking.Utils;
using Spectre.Console;

namespace AssetTracking.Menu;

public class AddAssetMenu(AssetDbContext context) : IMenu
{
    public string Title => "Add Asset";

    public void Show()
    {
        var type = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateSelectionPrompt<string>("Asset Type: ")
                .AddChoices("Mobile", "Computer")
        );
        var brand = AnsiConsole.Ask<string>("Brand: ");
        var model = AnsiConsole.Ask<string>("Model: ");
        var price = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateLimitPrompt("Purchase Price (USD): ", 1, 10000.0)
        );
        var office = SpectreConsoleHelper.AskOffice("Office: ", context.Offices);
        var serialNumber = AnsiConsole.Ask<string>("Serial Number: ");
        var purchaseDate = AnsiConsole.Prompt(SpectreConsoleHelper.CreateDatePrompt("Purchase Date: "));
        var warrantyPeriod = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateLimitPrompt("Warranty Period (in years): ", 1, 20)
        );

        Asset asset;
        if (type == "Mobile")
        {
            asset = new MobileAsset();
        }
        else
        {
            asset = new ComputerAsset();
        }

        asset.Brand = brand;
        asset.Model = model;
        asset.Price = price;
        asset.Office = office;
        asset.SerialNumber = serialNumber;
        asset.PurchaseDate = purchaseDate;
        asset.WarrantyExpirationDate = purchaseDate.AddYears(warrantyPeriod);

        context.Assets.Add(asset);
        context.SaveChanges();
        ConsoleHelper.WriteSuccess("Asset added");
        ConsoleHelper.AskEnterToContinue();
    }
}