using System;
using HtmlAgilityPack;
using UpSoft.UniSuperScrapper.DbAccess;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.Models
{
    internal class UniSuperWebParser
    {
        internal Balance ParseBalance(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='now']");
            if (node == null) return null;
            
            var date =
                DateTime.Parse(HtmlEntity.DeEntitize(node.SelectSingleNode(".//div[@class='date']").InnerText).Remove(0, 6));
            var amount =
                Decimal.Parse(node.SelectSingleNode(".//div[@class='amount']").InnerText.TrimStart('$', '\r', '\n'));

            return new Balance {TimeStamp = date, Amount = amount};
        }
    }
}
