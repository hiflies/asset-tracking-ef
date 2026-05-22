namespace AssetTracking.Models;

public class MobileAsset : Asset
{
    public override string Type
    {
        get => "Mobile";
        set;
    }
}