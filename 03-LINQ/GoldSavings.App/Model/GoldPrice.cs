using Newtonsoft.Json;

namespace GoldSavings.App.Model;

public class GoldPrice
{
    [JsonProperty("Data")]
    public DateTime Date { get; set; }

    [JsonProperty("Cena")]
    public double Price { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is GoldPrice other)
        {
            return this.Date == other.Date && this.Price == other.Price;
        }
        return false;
    }
}