using AssetTracking.Models;
using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AssetTracking.Menu;

public class DeleteAssetMenu(AssetDbContext context) : IMenu
{
    public string Title => "Delete Asset";

    public void Show()
    {
        var asset = SpectreConsoleHelper.AskAsset(
            "Asset: ",
            context.Assets.Include(asset => asset.Office)
        );
        
        if (AnsiConsole.Confirm("Are you sure you want to delete this asset?"))
        {
            context.Assets.Remove(asset);
            context.SaveChanges();
            ConsoleHelper.WriteSuccess("Asset deleted");
        }
        else
        {
            ConsoleHelper.WriteInfo("Asset not deleted");
        }

        ConsoleHelper.AskEnterToContinue();
    }
}