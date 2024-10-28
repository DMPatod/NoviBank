using System.Xml.Linq;

namespace ECB.ApiClient.Models;

public class XmlDeserializer
{
    public static IList<Exchange> Parse(string xml)
    {
        var xdoc = XDocument.Parse(xml);
        XNamespace eurofxref = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";
        var result = new List<Exchange>();

        var d = xdoc.Descendants();
        var time = xdoc.Descendants(eurofxref + "Cube")
            .FirstOrDefault(e => e.Attribute("time") != null)?
            .Attribute("time")?
            .Value;

        foreach (var item in xdoc.Descendants(eurofxref + "Cube").Where(e => e.Attribute("currency") != null))
        {
            result.Add(new Exchange(item.Attribute("currency")?.Value, item.Attribute("rate")?.Value, time));
        }

        return result;
    }
}