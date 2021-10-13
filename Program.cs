using Parser.Model;
using Parser.ParserCore.Logic;
using Parser.ParserCore.Model;
using System;
using System.Collections.Generic;

namespace Parser
{
    class Program
    {
        static string ProductParse { get; set; }
        static string parseUrl = "https://www.avito.ru/rossiya/noutbuki/apple-ASgCAQICAUCo5A0U9Nlm?user=1";
        static void Main(string[] args)
        {
            Start();
        }


        static public void Start()
        {
            DateTime str = DateTime.Now;
            ParserWorker<List<Product>> parser = new ParserWorker<List<Product>>(new SiteParser(), ProductParse);
            parser.Settings = new SiteParserSet(1, 2);
            parser.Settings.BaseUrl = parseUrl;
            parser.Start();
           
            Console.WriteLine(DateTime.Now - str);
            Console.ReadKey();
        }
    }
}
