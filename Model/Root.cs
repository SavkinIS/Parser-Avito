using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Model
{
    class Root
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Ссылка на сервер 
        /// </summary>
        public string Url { get; set; }
    }
}
