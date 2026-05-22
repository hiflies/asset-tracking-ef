namespace AssetTracking.Models;

public class ComputerAsset : Asset
{
    public override string Type
    {
        get => "Computer";
        set;
    }
}