using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.AspNetCore.Html;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ReadAndRender.Pages
{
   
    public class IndexModel : PageModel
    {
        public List<Item> ItemList { get; private set; }
        public IndexModel()
        {
            ItemList = new List<Item>();
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = new HttpClient();
            string rss = await client.GetStringAsync("http://scripting.com/rss.xml");
            XmlDocument document = new XmlDocument();
            document.LoadXml(rss);
            XmlNodeList ItemNodes = document.GetElementsByTagName("item");
          
           for(int i=0;i<ItemNodes.Count;i++)
           {
                Item Item = new Item();

                Item.Title = ItemNodes[i].SelectSingleNode("title")?.InnerText;
                string descriptions = ItemNodes[i].SelectSingleNode("description")?.InnerText;
                HtmlString htmlDescription = new HtmlString(descriptions);
                Item.Description = htmlDescription;
                Item.Link = ItemNodes[i].SelectSingleNode("link")?.InnerText;
                Item.Guid = ItemNodes[i].SelectSingleNode("guid")?.InnerText;
                Item.PubDate = ItemNodes[i].SelectSingleNode("pubDate")?.InnerText;


                ItemList.Add(Item);
            }
            return Page();
        }
    }

    public class Item
    {
        public string Title { get; set; }
        public HtmlString? Description { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Image { get; set; }
        public string PubDate { get; set; }
    }
}