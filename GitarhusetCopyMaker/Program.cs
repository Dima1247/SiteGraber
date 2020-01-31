using CsQuery;
using GitarhusetCopyMaker.DataLawyer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GitarhusetCopyMaker
{
    class Program
    {
        private static readonly GitarhusetDbContext context = new GitarhusetDbContext();
        private static string categoriesPath = "https://www.gitarhuset.no/api/Menu/GetHtmlMenu?nodeId=2015652&screensize=sm&screensizePixels=768&width=1920&height=1080&showMobileMenuCollapsed=false&_=1560006916847";
        private static readonly string sitePath = "https://www.gitarhuset.no";

        static void Main(string[] args)
        {
            //    ParseCategories(CQ.CreateFromUrl(categoriesPath)["divCategories"]);

            //foreach (var category in context.Categories.Where(c => !string.IsNullOrEmpty(c.Url) && !c.Checked).ToList())
            //{
            //    ParseProductsWithUrl(CQ.CreateFromUrl(sitePath + category.Url)["div"], category.Id);

            //    for (int i = 2; CheckPages(CQ.CreateFromUrl(sitePath + category.Url + "?pageID=" + (i - 1))["div"]) && i < 4; i++)
            //        ParseProductsWithUrl(CQ.CreateFromUrl(sitePath + category.Url + "?pageID=" + i)["div"], category.Id);

            //    category.Checked = true;
            //    context.SaveChanges();
            //    Console.WriteLine(category.Name);
            //}

            //foreach (var product in context.Products.Where(p => !string.IsNullOrEmpty(p.Url) && !p.Checked).ToList())
            //{
            //    ParseProducts(CQ.CreateFromUrl(sitePath + product.Url)["div"], product);
            //    Console.WriteLine(product.Id + " - " + product.Name);
            //}
                 LoadImages();
           
      //      ParseHtmlSigns();
        }

        static void ParseHtmlSigns()
        {
            foreach (var cat in context.Categories)
            {
                cat.Name = System.Web.HttpUtility.HtmlDecode(cat.Name);
                context.SaveChanges();
            }
        }

        static void ParseProducts(CQ divs, Product product)
        {
            int imgCount = 1;
            bool imgLoaded = true;

            foreach (var div in divs.ToList())
            {
                if (div.HasClass("product-number-inner"))
                    product.Code = div.LastElementChild.InnerText;

                if (div.HasClass("current-price-container"))
                    product.Price = int.Parse(div.FirstElementChild.InnerText.Split(',')[0].Replace(" ", ""));

                if (div.HasClass("product-description"))
                    product.Description = div.InnerHTML;

                if (div.HasClass("prod-image-slider"))
                {
                    foreach (var pict in div.ChildElements)
                    {
                        using (WebClient client = new WebClient())
                        {
                            if (!Directory.Exists(Environment.CurrentDirectory + "\\Images"))
                                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Images");

                            try
                            {
                                if (!pict.HasAttribute("data-rsVideo"))
                                {
                                    if (!imgLoaded)
                                    {
                                        client.DownloadFile(new Uri(sitePath + pict.GetAttribute("data-rsTmb")), Environment.CurrentDirectory + "\\Images\\" + product.Id + "_SmallImage" + imgCount + ".jpeg");
                                        client.DownloadFile(new Uri(sitePath + pict.GetAttribute("href")), Environment.CurrentDirectory + "\\Images\\" + product.Id + "_MiddleImage" + imgCount + ".jpeg");
                                        client.DownloadFile(new Uri(sitePath + pict.GetAttribute("data-rsBigImg")), Environment.CurrentDirectory + "\\Images\\" + product.Id + "_LargeImage" + imgCount + ".jpeg");
                                    }

                                    context.Pictures.Add(new Picture() { Url = sitePath + pict.GetAttribute("data-rsTmb"), Name = product.Id + "_SmallImage" + imgCount, Type = 1, Position = imgCount, ProductId = product.Id });
                                    context.Pictures.Add(new Picture() { Url = sitePath + pict.GetAttribute("href"), Name = product.Id + "_MiddleImage" + imgCount, Type = 2, Position = imgCount, ProductId = product.Id });
                                    context.Pictures.Add(new Picture() { Url = sitePath + pict.GetAttribute("data-rsBigImg"), Name = product.Id + "_LargeImage" + imgCount, Type = 3, Position = imgCount, ProductId = product.Id });

                                    imgLoaded = true;
                                }
                                else
                                {
                                    context.Pictures.Add(new Picture() { Url = pict.GetAttribute("data-rsVideo"), Name = product.Id + "_Video" + imgCount, Type = 4, Position = imgCount, ProductId = product.Id });
                                }
                            }
                            catch { }

                            imgCount++;
                        }
                    }
                }
            }

            product.Checked = true;
            context.SaveChanges();
        }

        static void LoadImages()
        {
            foreach (var pict in context.Pictures)
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        if (!Directory.Exists(Environment.CurrentDirectory + "\\Images"))
                            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Images");

                        if (!File.Exists(Environment.CurrentDirectory + "\\Images\\" + pict.Name + ".jpeg") && pict.Type != 4)
                            client.DownloadFile(new Uri(pict.Url), Environment.CurrentDirectory + "\\Images\\" + pict.Name + ".jpeg");
                    }
                    catch { }
                }
            }
        }

        static bool CheckPages(CQ divs)
        {
            foreach (var div in divs.Where(d => d.HasClass("FieldPaging")).ToList())
            {
                foreach (var element in div.ChildElements)
                    if (element.FirstChild.NodeValue == ">" && element.InnerHTML == "&gt;")
                        return true;
            }

            return false;
        }

        static List<string> urls = new List<string>();
        static void ParseProductsWithUrl(CQ divs, int categoryId)
        {
            foreach (var div in divs.Where(d => d.HasClass("AddHeaderContainer")).ToList())
            {
                var product = div.FirstElementChild;

                string productUrl = product.GetAttribute("href");
                string productName = product.FirstElementChild.InnerText;
                string productManufacturer = null;

                if (product.ChildElements.Count() > 1)
                    productManufacturer = product.ChildElements.ToList()[1].InnerText;

                if (!urls.Any(i => i.Equals(productUrl)))
                {
                    urls.Add(productUrl);
                    context.Products.Add(new Product() { Name = productName, Manufacturer = productManufacturer, Url = productUrl, CategoryId = categoryId });
                    context.SaveChanges();
                }
            }
        }

        static void ParseCategories(CQ divs)
        {
            int mainCategoryId = -1;
            int secondCategoryId = -1;

            foreach (var div in divs.ToList())
            {
                if (div.HasClass("col-xs-12"))
                {
                    var name = div.FirstElementChild.InnerHTML.Split(' ')[0];
                    if (name.EndsWith("\n"))
                        name = name.Remove(name.Length - 3);

                    context.Categories.Add(new Category() { Name = name });
                    context.SaveChanges();

                    mainCategoryId = context.Categories.Last().Id;
                }

                if (div.HasClass("menu-items-container"))
                {
                    foreach (var category in div.ChildElements)
                    {
                        if (category == div.FirstElementChild)
                        {
                            context.Categories.Add(new Category() { Name = category.InnerHTML, ParentCategoryId = mainCategoryId });
                            context.SaveChanges();

                            secondCategoryId = context.Categories.Last().Id;
                        }
                        else
                        {
                            context.Categories.Add(new Category() { Name = category.InnerHTML, ParentCategoryId = secondCategoryId, Url = category.GetAttribute("href") });
                            context.SaveChanges();
                        }
                    }
                }
            }

            context.SaveChanges();
        }
    }
}