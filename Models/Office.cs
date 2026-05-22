namespace AssetTracking.Models;

public class Office
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    
    public IEnumerable<Asset> Assets { get; set; }
}