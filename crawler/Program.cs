using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            startCrawler();
        }

        private static async Task startCrawler()
        {
            var url = "https://www.xpau.se";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var contentDiv = htmlDoc.DocumentNode
                .Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Equals("content"));

            Console.WriteLine(contentDiv);

        }
    }
}