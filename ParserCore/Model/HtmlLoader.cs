using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore.Model
{
    class HtmlLoader
    {
        readonly HttpClient client;
        //readonly string url = "https://www.avito.ru/rossiya/noutbuki/apple-ASgCAQICAUCo5A0U9Nlm?user=1";


        public HtmlLoader(IParserSettings settings)
        {
            this.client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Это для индентификации на сайте.
        }

        public async Task<string> GetSourceByPage(string url) 
        {
            string currentUrl = url; 
            HttpResponseMessage responce = await client.GetAsync(currentUrl); //Получаем ответ с сайта.
            string source = default;

            if (responce != null)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
    }
}
