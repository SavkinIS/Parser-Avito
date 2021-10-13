using AngleSharp.Html.Dom;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore
{
    interface IParser<T> where T : class
    {
        List<Product> products { get; set; }
        List<Product> Parse(IHtmlDocument document);//парсит страницу
    }
}
