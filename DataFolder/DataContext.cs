using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.DataFolder 
{ 
    class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }


        public DataContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.GetFullPath(@"..\..\..\") + "/ConnectionString.json";
            string connectionstring = File.ReadAllText(path);
            Root root = JsonConvert.DeserializeObject<Root>(connectionstring);
            ///Изменить строку подключения
            optionsBuilder.UseSqlServer(
                root.ConnectionString
                );
        }
    }
}
