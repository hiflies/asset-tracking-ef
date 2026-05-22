namespace AssetTracking.Models;

public abstract class Asset
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public string SerialNumber { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime WarrantyExpirationDate { get; set; }
    public abstract string Type { get; set; }
}