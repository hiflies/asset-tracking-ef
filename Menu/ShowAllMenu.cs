using AssetTracking.Services;
using AssetTracking.Utils;
using Microsoft.EntityFrameworkCore;

namespace AssetTracking.Menu;

public class ShowAllMenu(AssetDbContext context) : ListAssetMenu
{
    public override string Title => "Show All Assets";

    public override void Show()
    {
        var assets = context.Assets
            .Include(asset => asset.Office)
            .Include(asset => asset.Office.Currency)
            .OrderBy(asset => asset.Type)
            .ThenByDescending(asset => asset.PurchaseDate);

        ShowAssets(assets);

        ConsoleHelper.AskEnterToContinue();
    }
}