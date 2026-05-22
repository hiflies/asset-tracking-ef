using AssetTracking.Models;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace AssetTracking.Utils;

public static class SpectreConsoleHelper
{
    public static IRenderable[] ColorRow(string color, params string[] rows)
    {
        return rows.Select(IRenderable (row) => new Markup($"[{color}]{row}[/]")).ToArray();
    }

    public static Office AskOffice(
        string title,
        IEnumerable<Office> offices,
        Func<SelectionPrompt<Office>, SelectionPrompt<Office>>? onPrompt = null
    )
    {
        var prompt = CreateSelectionPrompt<Office>(title)
            .AddChoices(offices)
            .UseConverter(office => office.Name);

        if (onPrompt != null)
        {
            prompt = onPrompt.Invoke(prompt);
        }

        var result = AnsiConsole.Prompt(prompt);
        Console.WriteLine($"{title} {result.Name}");

        return result;
    }

    public static Asset AskAsset(string title, IEnumerable<Asset> assets)
    {
        var result = AnsiConsole.Prompt(
            CreateSelectionPrompt<Asset>(title)
                .AddChoices(assets)
                .UseConverter(asset => $"{asset.Id} - {asset.Brand} - {asset.Model} - {asset.Office.Name}")
                .EnableSearch()
                .PageSize(10)
        );

        ConsoleHelper.WriteInfo(
            $"Selected Asset: {result.Id} - {result.Brand} - {result.Model} - {result.Office.Name}");

        return result;
    }

    public static TextPrompt<DateTime> CreateDatePrompt(string title)
    {
        return new TextPrompt<DateTime>(title)
            .Validate(date =>
            {
                if (date < DateTime.Today.AddYears(-20))
                {
                    return ValidationResult.Error("[red]Date must be in 20 years.[/]");
                }

                if (date > DateTime.Now)
                {
                    return ValidationResult.Error("[red]Date cannot be in the future.[/]");
                }

                return ValidationResult.Success();
            });
    }

    public static TextPrompt<T> CreateLimitPrompt<T>(string title, T lowest, T highest) where T : IComparable<T>
    {
        return new TextPrompt<T>(title)
            .Validate(value =>
            {
                if (value.CompareTo(lowest) < 0)
                {
                    return ValidationResult.Error($"[red]Value must be greater than {lowest}[/]");
                }

                if (value.CompareTo(highest) > 0)
                {
                    return ValidationResult.Error($"[red]Value must be less than or equal to {highest}[/]");
                }

                return ValidationResult.Success();
            });
    }

    public static SelectionPrompt<T> CreateSelectionPrompt<T>(string title) where T : notnull
    {
        return new SelectionPrompt<T>()
            .Title(title)
            .WrapAround()
            .HighlightStyle(new Style(Color.Green, Color.Black, Decoration.Bold));
    }
}