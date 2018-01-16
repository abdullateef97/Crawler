    using System;
    using System.Collections.Generic;
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
//               startCrawler().Wait(TimeSpan.FromMinutes(20));
                startCrawler().Wait();
                Console.Write("oya");
            }
            
              
            private static async Task startCrawler()
            {
//                var url = "http://saucecode.xyz/"; 
                //9:56  11:21 34
                string url = "http://saucecode.xyz/teams";
                var httpClient = new HttpClient();
//                httpClient.Timeout = TimeSpan.FromMinutes(500);
                Console.WriteLine(1);
                try
                {
                    Console.WriteLine(2);
                    var html = await httpClient.GetStringAsync(new Uri(url));
                    
                    Console.WriteLine(3);
                    
//                    httpClient.Timeout = TimeSpan.FromMinutes(50);
                    var htmlDoc = new HtmlDocument();
                    Console.WriteLine(21);
                    htmlDoc.LoadHtml(html);
                    Console.WriteLine(31);
                   

//                    var divs =  htmlDoc.DocumentNode.Descendants("div").Where(node =>
//                        node.GetAttributeValue("class", "").Equals("col-md-6 col-sm-8")).ToString();
                 var divs =  htmlDoc.DocumentNode
                              .Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Equals("col-md-6 col-sm-8"));
                    
                    Console.WriteLine("bbbb");
                    List<string> strList = new List<string>();
                    var teams = divs?.Descendants("a").ToList();
                    foreach (var team in teams)
                    {
                        var name = team?.Descendants("p")?.FirstOrDefault()?.InnerText;
                        if (name !=  null && !name.Contains("[Disqualified]"))
                        {
                            strList.Add(name);
                            var splitName = name.Trim().Substring(13);
                            Console.WriteLine(splitName);  
                        }
                        
                    }
                    Console.WriteLine(strList.Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(4);
                    Console.WriteLine(ex);
                }
    
            }
        }
    }