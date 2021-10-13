using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore
{
    interface IParserSettings
    {
        string BaseUrl { get; set; } //url сайта
        string Postfix { get; set; } //в постфикс будет передаваться id страницы
        int StartPoint { get; set; } //c какой страницы парсим данные
        int EndPoint { get; set; } //по какую страницу парсим данные
    }
}
