using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore.Logic
{
    class SiteParserSet : IParserSettings
    {
        public SiteParserSet(int startPoint, int endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public string BaseUrl { get; set; }  //здесь прописываем url сайта.
        public string Postfix { get; set; } = "page{CurrentId}"; //вместо CurrentID будет подставляться номер страницы
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
