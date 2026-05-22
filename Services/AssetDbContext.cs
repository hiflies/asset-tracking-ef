using AssetTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTracking.Services;

public class AssetDbContext(string connectionString) : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<MobileAsset> MobileAssets{ get; set; }
    public DbSet<ComputerAsset> ComputerAssets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
        
        // optionsBuilder.UseSqlite("Data Source=assettracking.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>()
            .HasDiscriminator(x => x.Type)
            .HasValue<MobileAsset>("Mobile")
            .HasValue<ComputerAsset>("Computer");
        
        var usd = new Currency{Id = 1, Name = "USD", Symbol = "$"};
        var eur = new Currency{Id = 2, Name = "EUR",  Symbol = "€"};
        var sek = new Currency{Id = 3, Name = "SEK", Symbol = "kr"};
        var chf = new Currency{Id = 4, Name = "CHF", Symbol = "Fr"};
        var tl = new Currency{Id = 5, Name = "TRY", Symbol = "₺"};
        
        modelBuilder.Entity<Currency>().HasData(usd, eur, sek, chf, tl);

        var usa = new Office { Id = 1, Name = "Lexicon USA", Country = "USA", CurrencyId = usd.Id };
        var germany = new Office { Id = 2, Name = "Lexicon Germany", Country = "Sweden", CurrencyId = eur.Id };
        var sweden = new Office { Id = 3, Name = "Lexicon Sweden", Country = "Sweden", CurrencyId = sek.Id };
        var switzerland = new Office { Id = 4, Name = "Lexicon Switzerland", Country = "Switzerland", CurrencyId = chf.Id };
        var turkiye = new Office { Id = 5, Name = "Lexicon Türkiye", Country = "Türkiye", CurrencyId = tl.Id };

        modelBuilder.Entity<Office>().HasData(usa, germany, sweden, switzerland,  turkiye);
        
        var now = new DateTime(2026, 5, 20);

        modelBuilder.Entity<MobileAsset>().HasData(
            new MobileAsset {Id = 1, Brand = "Motorola", Model = "X3", Price = 200.0, OfficeId = usa.Id, SerialNumber = "SR-MBL-1", PurchaseDate = now.AddMonths(-36 + 4), WarrantyExpirationDate = now.AddMonths(4)}, 
            new MobileAsset {Id = 2, Brand = "Motorola", Model = "X3", Price = 400.0, OfficeId = usa.Id, SerialNumber = "SR-MBL-2", PurchaseDate = now.AddMonths(-36 + 5), WarrantyExpirationDate = now.AddMonths(5)}, 
            new MobileAsset {Id = 3, Brand = "Motorola", Model = "X2", Price = 400.0, OfficeId = usa.Id, SerialNumber = "SR-MBL-3", PurchaseDate = now.AddMonths(-36 + 10), WarrantyExpirationDate = now.AddMonths(10)}, 
            new MobileAsset {Id = 4, Brand = "Samsung", Model = "Galaxy 10", Price = 4500.0, OfficeId = sweden.Id, SerialNumber = "SR-MBL-4", PurchaseDate = now.AddMonths(-36 + 6), WarrantyExpirationDate = now.AddMonths(6)}, 
            new MobileAsset {Id = 5, Brand = "Samsung", Model = "Galaxy 10", Price = 4500.0, OfficeId = sweden.Id, SerialNumber = "SR-MBL-5", PurchaseDate = now.AddMonths(-36 + 7), WarrantyExpirationDate = now.AddMonths(7)}, 
            new MobileAsset {Id = 6, Brand = "Sony", Model = "XPeria 7", Price = 3000.0, OfficeId = sweden.Id, SerialNumber = "SR-MBL-6", PurchaseDate = now.AddMonths(-36 + 4), WarrantyExpirationDate = now.AddMonths(4)}, 
            new MobileAsset {Id = 7, Brand = "Sony", Model = "XPeria 7", Price = 3000.0, OfficeId = sweden.Id, SerialNumber = "SR-MBL-7", PurchaseDate = now.AddMonths(-36 + 5), WarrantyExpirationDate = now.AddMonths(5)}, 
            new MobileAsset {Id = 8, Brand = "Siemens", Model = "Brick", Price = 220.0, OfficeId = germany.Id, SerialNumber = "SR-MBL-8", PurchaseDate = now.AddMonths(-36 + 12), WarrantyExpirationDate = now.AddMonths(12)}, 
            new MobileAsset {Id = 21, Brand = "Motorola", Model = "X4", Price = 400.0, OfficeId = switzerland.Id, SerialNumber = "SR-MBL-21", PurchaseDate = now.AddMonths(-36 + 5), WarrantyExpirationDate = now.AddMonths(5)}
        );
        
        modelBuilder.Entity<ComputerAsset>().HasData(
            new ComputerAsset {Id = 9, Brand = "Dell", Model = "Desktop 900", Price = 100.0, OfficeId = usa.Id, SerialNumber = "SR-CMP-9", PurchaseDate = now.AddMonths(-36 - 2), WarrantyExpirationDate = now.AddMonths(-2)}, 
            new ComputerAsset {Id = 10, Brand = "Dell", Model = "Desktop 900", Price = 100.0, OfficeId = usa.Id, SerialNumber = "SR-CMP-10", PurchaseDate = now.AddMonths(-36 - 1), WarrantyExpirationDate = now.AddMonths(-1)}, 
            new ComputerAsset {Id = 11, Brand = "Lenovo", Model = "X100", Price = 300.0, OfficeId = usa.Id, SerialNumber = "SR-CMP-11", PurchaseDate = now.AddMonths(-36 + 1), WarrantyExpirationDate = now.AddMonths(1)}, 
            new ComputerAsset {Id = 12, Brand = "Lenovo", Model = "X200", Price = 300.0, OfficeId = usa.Id, SerialNumber = "SR-CMP-12", PurchaseDate = now.AddMonths(-36 + 4), WarrantyExpirationDate = now.AddMonths(4)}, 
            new ComputerAsset {Id = 13, Brand = "Lenovo", Model = "X300", Price = 500.0, OfficeId = usa.Id, SerialNumber = "SR-CMP-13", PurchaseDate = now.AddMonths(-36 + 9), WarrantyExpirationDate = now.AddMonths(9)}, 
            new ComputerAsset {Id = 14, Brand = "Dell", Model = "Optiplex 100", Price = 1500.0, OfficeId = sweden.Id, SerialNumber = "SR-CMP-14", PurchaseDate = now.AddMonths(-36 + 7), WarrantyExpirationDate = now.AddMonths(7)}, 
            new ComputerAsset {Id = 15, Brand = "Dell", Model = "Optiplex 200", Price = 1400.0, OfficeId = sweden.Id, SerialNumber = "SR-CMP-15", PurchaseDate = now.AddMonths(-36 + 8), WarrantyExpirationDate = now.AddMonths(8)}, 
            new ComputerAsset {Id = 16, Brand = "Dell", Model = "Optiplex 300", Price = 1300.0, OfficeId = sweden.Id, SerialNumber = "SR-CMP-16", PurchaseDate = now.AddMonths(-36 + 9), WarrantyExpirationDate = now.AddMonths(9)}, 
            new ComputerAsset {Id = 17, Brand = "Asus", Model = "ROG 600", Price = 1600.0, OfficeId = germany.Id, SerialNumber = "SR-CMP-17", PurchaseDate = now.AddMonths(-36 + 14), WarrantyExpirationDate = now.AddMonths(14)}, 
            new ComputerAsset {Id = 18, Brand = "Asus", Model = "ROG 500", Price = 1200.0, OfficeId = germany.Id, SerialNumber = "SR-CMP-18", PurchaseDate = now.AddMonths(-36 + 4), WarrantyExpirationDate = now.AddMonths(4)}, 
            new ComputerAsset {Id = 19, Brand = "Asus", Model = "ROG 500", Price = 1200.0, OfficeId = germany.Id, SerialNumber = "SR-CMP-19", PurchaseDate = now.AddMonths(-36 + 3), WarrantyExpirationDate = now.AddMonths(3)}, 
            new ComputerAsset {Id = 20, Brand = "Asus", Model = "ROG 500", Price = 1300.0, OfficeId = germany.Id, SerialNumber = "SR-CMP-20", PurchaseDate = now.AddMonths(-36 + 2), WarrantyExpirationDate = now.AddMonths(2)}
        );
    }
}