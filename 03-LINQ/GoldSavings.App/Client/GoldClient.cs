using Newtonsoft.Json;
using GoldSavings.App.Model;
using System.ComponentModel.DataAnnotations;

namespace GoldSavings.App.Client;

public class GoldClient
{
    private HttpClient _client;
    public GoldClient()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://api.nbp.pl/api/");
        _client.DefaultRequestHeaders.Accept.Clear();

    }
    public async Task<GoldPrice> GetCurrentGoldPrice()
    {
        try
        {
            HttpResponseMessage responseMsg = _client.GetAsync("cenyzlota/").GetAwaiter().GetResult();
            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                List<GoldPrice>? prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
                if (prices != null && prices.Count == 1)
                {
                    return prices[0];
                }
            }
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"API Request Error: {e.Message}");
            return null;
        }
   
        
        }

    public async Task<List<GoldPrice>> GetGoldPrices(DateTime startDate, DateTime endDate)
    {
        List<GoldPrice> allPrices = new List<GoldPrice>();
        for (int i = 0; i <= (endDate - startDate).Days; i+=90)
        {
            string dateFormat = "yyyy-MM-dd";
            string requestUri = $"cenyzlota/{startDate.AddDays(i).ToString(dateFormat)}/{startDate.AddDays(Math.Min(i + 90, (endDate - startDate).Days)).ToString(dateFormat)}";
            HttpResponseMessage responseMsg = _client.GetAsync(requestUri).GetAwaiter().GetResult();
            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                List<GoldPrice> prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
                if (prices != null)                {
                    allPrices.AddRange(prices);
                }
            }

        }
        return allPrices;
    }

}