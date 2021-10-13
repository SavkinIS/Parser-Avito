using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Parser.Hub;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.ParserCore.Logic
{
    class SiteParser : IParser<List<Product>>
    {

        public SiteParser()
        {
            productHub = new ProductHub();
        }
        ProductHub productHub;

        public List<Product> products { get; set; }
        string itemDivConteiner = "iva-item-root-Nj_hb";//"xf-product js-product";
        string itemUrl = "iva-item-sliderLink-bJ9Pv";
        string itemTitleH3 = "title-root-j7cja";
        string itemPriceClassName = "price-price-BQkOZ";
        string itemDescription = "iva-item-root-Nj_hb";
        string itemAderess = "geo-address-QTv9k";
        string itemImage = "photo-slider-list-item-_fUPr";//"photo-slider-image-_Dc4I";//
        string itemImage2 = "photo-slider-image-_Dc4I";
        string vipBox = "items-vip-KXPvy";


        /// <summary>
        /// Парсим документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public  List<Product> Parse(IHtmlDocument document)
        {
            //Для хранения заголовков
            List<Product> list = new List<Product>();
            //Здесь мы получаем заголовки
            List<IElement> items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains(itemDivConteiner)).ToList();

            int i = 1;

            foreach (var item in items)
            {
                if (item.ParentElement.ParentElement.ClassName.Contains(vipBox)) continue;
                Console.WriteLine($"StartParse item {i}");

                Product product = new Product();


                product.Description = item.TextContent.Replace("\n", " ");

               
                product.Name = item.QuerySelectorAll("h3").Where(n => n.ClassName != null && n.ClassName.Contains(itemTitleH3)).FirstOrDefault().TextContent;

                string priceStr = item.QuerySelectorAll("span").Where(n => n.ClassName != null && n.ClassName.Contains(itemPriceClassName)).FirstOrDefault().TextContent.Replace("₽", "");
               
                product.Price = PriceToInt(priceStr);
                product.ItemAderess = item.QuerySelectorAll("span").Where(n => n.ClassName != null && n.ClassName.Contains(itemAderess)).FirstOrDefault().TextContent;

                var descr = item.Attributes.ToList();


                IElement aHref = item.QuerySelectorAll("a").Where(n => n.ClassName != null && n.ClassName.Contains(itemUrl)).FirstOrDefault();


                product.Url = @"http://avito.ru" + aHref.Attributes.ToList().Where(a => a.Value.StartsWith("/")).FirstOrDefault().Value;


                //product.ImgSource = ParseItemImagePageAsync(product.Url).Result;
                var li = item.QuerySelectorAll("li").ToList();
                IElement img;
                IAttr imgAttr;
                if (li.Count > 1)
                {
                    img = li.Where(a => a.ClassName.Contains(itemImage) || a.ClassName.Contains(itemImage2)).FirstOrDefault();
                    imgAttr= img.Attributes.Where(a => a.Value.Contains("http")).FirstOrDefault();
                    product.ImgSource = imgAttr.Value.Split("http")[1].Insert(0, "http");
                }
                else if (li.Count == 1)
                {
                    imgAttr = li.First().Attributes.Where(a => a.Value.Contains("http")).FirstOrDefault();
                    product.ImgSource = imgAttr.Value.Split("http")[1].Insert(0, "http");
                }
                
                list.Add(product);

                
                productHub.Send(product);
                i++;
                Console.WriteLine(product.Name);
                Console.WriteLine($"Stop Parse item {i}");
                Console.WriteLine();
            }
            products = list;
            return list;
        }

        /// <summary>
        /// Преобразование текстовой цены в числовое
        /// </summary>
        /// <param name="priceStr"></param>
        /// <returns></returns>
        int PriceToInt(string priceStr)
        {
            int price = 0;
            string endStr = "";
            foreach (var item in priceStr)
            {
                if(char.IsDigit(item))
                {
                    endStr += item;
                }
            }
            price = Convert.ToInt32(endStr);
            return price;

        }
    }
}
