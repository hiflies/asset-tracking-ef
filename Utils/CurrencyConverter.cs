using System.Globalization;
using System.Xml;

namespace AssetTracking.Utils;

public class CurrencyConverter // Class that handles fetching the exchange rates and converting currencies
{
    // private static List<CurrencyObj> currencyList = new List<CurrencyObj>();
    private static Dictionary<string, double> currencyList = new();

    public static void FetchRates()
    {
        if (currencyList.Count > 0)
        {
            return;
        }
        
        string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document

        XmlTextReader reader = new XmlTextReader(url);
        while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name ==
                        "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                    {
                        string currencyCode = reader.Value;

                        reader.MoveToNextAttribute();
                        double rate = double.Parse(reader.Value, CultureInfo.InvariantCulture);
                        currencyList.Add(currencyCode, rate);
                    }
                }
            }
        }
    }

    public static double
        Convert(double input, string fromCurrency,
            string toCurrency) // Method that uses the fetched rates to convert between the given rates via Euro
    {
        double value = 0;

        if (fromCurrency == "EUR")
        {
            value = input * currencyList[toCurrency];
        }
        else if (toCurrency == "EUR")
        {
            value = input / currencyList[fromCurrency];
        }
        else
        {
            value = input / currencyList[fromCurrency];
            value *= currencyList[toCurrency];
        }

        return value;
    }
}