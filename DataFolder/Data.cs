using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.DataFolder
{
    /// <summary>
    /// Работа с БД
    /// </summary>
    class Data
    {
        DataContext context = new DataContext();


        /// <summary>
        /// Добавляем записть в БД
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            if (!context.Products.Contains(product) && product != null)
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

    }
}
