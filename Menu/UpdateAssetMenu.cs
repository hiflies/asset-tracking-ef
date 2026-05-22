using AssetTracking.Models;
using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AssetTracking.Menu;

public class UpdateAssetMenu(AssetDbContext context) : IMenu
{
    public string Title => "Update Asset";

    public void Show()
    {
        var asset = SpectreConsoleHelper.AskAsset(
            "Asset: ",
            context.Assets.Include(asset => asset.Office)
        );

        var brand = AnsiConsole.Prompt(
            new TextPrompt<string>("Brand: ")
                .DefaultValue(asset.Brand)
                .ShowDefaultValue()
        );
        var model = AnsiConsole.Prompt(
            new TextPrompt<string>("Model: ")
                .DefaultValue(asset.Model)
                .ShowDefaultValue()
        );
        var price = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateLimitPrompt("Price: ", 1, 10000.0)
                .DefaultValue(asset.Price)
                .ShowDefaultValue()
        );

        var office = SpectreConsoleHelper.AskOffice(
            "Office: ",
            context.Offices,
            office => office.DefaultValue(asset.Office)
        );

        var serialNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Serial Number: ")
                .DefaultValue(asset.SerialNumber)
                .ShowDefaultValue()
        );

        var purchaseDate = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Purchased Date: ")
                .DefaultValue(asset.PurchaseDate)
                .ShowDefaultValue()
        );


        var warrantyPeriod = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateLimitPrompt("Warranty Period (in years): ", 1, 20)
                .DefaultValue((int)(asset.WarrantyExpirationDate - asset.PurchaseDate).TotalDays / 365)
                .ShowDefaultValue()
        );

        if (AnsiConsole.Confirm("Are you sure you want to update this asset?"))
        {
            asset.Brand = brand;
            asset.Model = model;
            asset.Price = price;
            asset.Office = office;
            asset.SerialNumber = serialNumber;
            asset.PurchaseDate = purchaseDate;
            asset.WarrantyExpirationDate = purchaseDate.AddYears(warrantyPeriod);

            context.SaveChanges();
            ConsoleHelper.WriteSuccess("Asset updated");
        }
        else
        {
            ConsoleHelper.WriteInfo("Asset not updated");
        }

        ConsoleHelper.AskEnterToContinue();
    }
}