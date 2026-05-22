namespace AssetTracking.Menu;

public interface IMenu
{
    public string Title { get; }

    public void Show();
}