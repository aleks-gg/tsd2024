using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");
        
        GoldDataService dataService = new GoldDataService();
        // Task 1
        DateTime startDate = new DateTime(2025,01,01);
        DateTime endDate = new DateTime(2025,12,31);
        List<GoldPrice> goldPrices2025 = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();
        goldPrices2025 = goldPrices2025.OrderByDescending(x => x.Price).ToList();
        Console.WriteLine("Task 1");
        Console.WriteLine($"Top 3 Gold prices in 2025: {string.Join(", ", goldPrices2025.Take(3).Select(x => x.Price))}");
        Console.WriteLine($"Bottom 3 Gold prices in 2025: {string.Join(", ", goldPrices2025.TakeLast(3).Select(x => x.Price))}");
        Console.WriteLine("---------------------------------------------");
        
        // Task 2
        List<GoldPrice> jan2020Prices = dataService.GetGoldPrices(new DateTime(2020,01,01), new DateTime(2020,01,31)).GetAwaiter().GetResult();
        GoldPrice buyPrice = jan2020Prices.OrderBy(x => x.Price).FirstOrDefault();
        List<GoldPrice> sellPrices = dataService.GetGoldPrices(buyPrice.Date, DateTime.Now).GetAwaiter().GetResult();
        sellPrices = sellPrices.Where(x => x.Price >= (buyPrice.Price * 1.05)).OrderByDescending(x => x.Price).ToList();
        Console.WriteLine("\nTask 2\nTop 10 sell opportunities:");
        foreach (var sellPrice in sellPrices.Take(10))
        {
            Console.WriteLine($"Sell on {sellPrice.Date.ToShortDateString()} for {sellPrice.Price}");
        }
        Console.WriteLine("---------------------------------------------");

        // Task 3
        List<GoldPrice> prices2022_2019 = dataService.GetGoldPrices(new DateTime(2019,01,01), new DateTime(2022,12,31)).GetAwaiter().GetResult();
        prices2022_2019 = prices2022_2019.OrderByDescending(x => x.Price).ToList();
        List<GoldPrice> top3_in_second_10 = prices2022_2019.Skip(10).Take(3).ToList();
        Console.WriteLine("\nTask 3\nTop 3 prices in 2019-2022 excluding top 10:");
        foreach (var price in top3_in_second_10)        {
            Console.WriteLine($"{price.Date.ToShortDateString()}: {price.Price}");
        }
        Console.WriteLine("---------------------------------------------");

        // Task 4
        Console.WriteLine("\nTask 4\nAverage gold price in 2024:");
        List<GoldPrice> prices2020 = dataService.GetGoldPrices(new DateTime(2020,01,01), new DateTime(2020,12,31)).GetAwaiter().GetResult();
        var avgPrice2020 = prices2020.Average(x => x.Price);
        Console.WriteLine($"Average gold price in 2020: {avgPrice2020}");

        List<GoldPrice> prices2023 = dataService.GetGoldPrices(new DateTime(2023,01,01), new DateTime(2023,12,31)).GetAwaiter().GetResult();
        var avgPrice2023 = prices2023.Average(x => x.Price);
        Console.WriteLine($"Average gold price in 2023: {avgPrice2023}");

        List<GoldPrice> prices2024 = dataService.GetGoldPrices(new DateTime(2024,01,01), new DateTime(2024,12,31)).GetAwaiter().GetResult();
        var avgPrice2024 = prices2024.Average(x => x.Price);
        Console.WriteLine($"Average gold price in 2024: {avgPrice2024}");

        // Step 1: Get gold prices
        // DateTime startDate = new DateTime(2025,12,30);
        // DateTime endDate = DateTime.Now;
        // List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

        // if (goldPrices.Count == 0)
        // {
        //     Console.WriteLine("No data found. Exiting.");
        //     return;
        // }

        // Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // // Step 2: Perform analysis
        // GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        // var avgPrice = analysisService.GetAveragePrice();

        // // Step 3: Print results
        // GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");

        // Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}
