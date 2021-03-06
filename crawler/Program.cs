﻿    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.Serialization;
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
                string url = "http://saucecode.xyz/teams";
                var client = new HttpClient();
                try
                {
                    var html = await client.GetStringAsync(new Uri(url));
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);
                    var mainDiv = htmlDoc.DocumentNode
                        .Descendants("div").SingleOrDefault(node =>
                            node.GetAttributeValue("class", "").Equals("col-md-6 col-sm-8"));
                    var paginationDiv = mainDiv?.Descendants("ul")
                        .SingleOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("pagination"));
                    // get all links
                    var paginationLinks = paginationDiv?.Descendants("li").ToList();
                    List <string> validCandidates = new List<string>();
                    StreamWriter sw = new StreamWriter("contestants.txt");
                    if (paginationLinks != null)
                    {
                        foreach (var paginationLink in paginationLinks)
                        {
                            var link = paginationLink?.Descendants("a").SingleOrDefault();
                            var urlValue = link?.ChildAttributes("href")?.SingleOrDefault()?.Value;
                            await getTeams(urlValue, validCandidates, sw);
                        } 
                    }
                    else
                    {
                        Console.WriteLine("it hapned");
                    }
                    
                    Console.WriteLine(validCandidates.Count);


                }
                catch (Exception ex)
                {
                    Console.WriteLine("got inside exception");
                    Console.WriteLine(ex);
                    
                }

            }

            private static async Task getTeams(string url, List<string> list, StreamWriter sw)
            {
//                string url = "http://saucecode.xyz/teams";
                var httpClient = new HttpClient();

                try
                {
                    var html = await httpClient.GetStringAsync(new Uri(url));
                    
                  
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);
              
                   
                 var divs =  htmlDoc.DocumentNode
                              .Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").
                         Equals("col-md-6 col-sm-8"));
                    
                  
//                    List<string> strList = new List<string>();
                    var teams = divs?.Descendants("a").ToList();
                    foreach (var team in teams)
                    {
                        var name = team?.Descendants("p")?.FirstOrDefault()?.InnerText;
                        if (name !=  null && !name.Contains("[Disqualified]"))
                        {
                           list.Add(name);
                            
                            var splitName = name.Trim().Substring(13);
                            
                            Console.WriteLine(splitName); 
                            sw.Write(splitName);
                        }
                        
                    }
                    Console.WriteLine(list.Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(4);
                    Console.WriteLine(ex);
                }
    
            }
        }
    }