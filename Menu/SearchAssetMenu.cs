using AssetTracking.Models;
using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AssetTracking.Menu;

public class SearchAssetMenu(AssetDbContext context) : ListAssetMenu
{
    public override string Title => "Search Asset";

    public override void Show()
    {
        var searchBy = AnsiConsole.Prompt(
            SpectreConsoleHelper.CreateSelectionPrompt<string>("Search by: ")
                .AddChoices("Brand", "Model", "Office", "Purchase Year")
        );

        IQueryable<Asset> list = context.Assets.Include(asset => asset.Office)
            .Include(asset => asset.Office.Currency);
        list = searchBy switch
        {
            "Brand" => FilterByBrand(list),
            "Model" => FilterByModel(list),
            "Office" => FilterByOffice(list),
            "Purchase Year" => FilterByPurchaseYear(list),
            _ => list
        };

        ShowAssets(list);

        ConsoleHelper.AskEnterToContinue();
    }

    private IQueryable<Asset> FilterByBrand(IQueryable<Asset> list)
    {
        var brand = AnsiConsole.Ask<string>("Brand: ");

        return list.Where(asset => asset.Brand.Contains(brand));
    }

    private IQueryable<Asset> FilterByModel(IQueryable<Asset> list)
    {
        var model = AnsiConsole.Ask<string>("Model: ");

        return list.Where(asset => asset.Model.Contains(model));
    }

    private IQueryable<Asset> FilterByOffice(IQueryable<Asset> list)
    {
        var office = SpectreConsoleHelper.AskOffice("Office: ", context.Offices);

        return list.Where(asset => asset.Office == office);
    }

    private IQueryable<Asset> FilterByPurchaseYear(IQueryable<Asset> list)
    {
        var year = AnsiConsole.Ask<int>("Year: ");

        return list.Where(asset => asset.PurchaseDate.Year == year);
    }
}