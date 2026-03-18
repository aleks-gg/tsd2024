using GoldSavings.App.Model;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GoldSavings.App.IO
{
    public class XMLIO
    {
        public void SaveGoldPricesToXML(List<GoldPrice> prices, string filePath)
        {
            new XDocument(
                new XDeclaration("1.0", "utf-8", "true"),
                new XElement("GoldPrices",
                    prices.Select(p => new XElement("GoldPrice",
                        new XElement("Date", p.Date),
                        new XElement("Price", p.Price)
                    ))
                )
            ).Save(filePath);
        }

        public List<GoldPrice> LoadGoldPricesFromXML(string filePath) {
            return XDocument.Load(filePath)
                .Root
                .Elements("GoldPrice")
                .Select(x => new GoldPrice
                {
                    Date = DateTime.Parse(x.Element("Date").Value),
                    Price = double.Parse(x.Element("Price").Value)
                })
                .ToList();
        }
    }
}