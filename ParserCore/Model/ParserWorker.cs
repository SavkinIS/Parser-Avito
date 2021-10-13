using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Parser.Hub;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore.Model
{
    class ParserWorker<T>
       where T : class
    {

        IParser<T> parser;
        IHtmlDocument document;
        IParserSettings parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        bool isActive; //активность парсера


        
        public List<Product> result { get; set; }

        string prodName;

        public ParserWorker(IParser<T> parser, string prodName)
        {
            this.parser = parser;
            this.prodName = prodName;
        }

        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value; //Новые настройки парсера
                loader = new HtmlLoader(value); //сюда помещаются настройки для загрузчика кода страницы
            }
        }

        public bool IsActive //проверяем активность парсера.
        {
            get { return isActive; }
        }

        public void Start() //Запускаем парсер
        {
            document = Task.Run(() => Worker()).Result;
            
            Console.WriteLine("StartParse");
            result = parser.Parse(document);

            isActive = false;

        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }

        public async Task<IHtmlDocument> Worker()
        {   string source = await loader.GetSourceByPage(Settings.BaseUrl); //Получаем код страницы
            HtmlParser domParser = new HtmlParser();
            return await domParser.ParseDocumentAsync(source);
        }
    }
}
