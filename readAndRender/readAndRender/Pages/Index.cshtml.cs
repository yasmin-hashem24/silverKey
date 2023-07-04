using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.AspNetCore.Html;
using System.Xml;

using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace readAndRender.Pages


{
    public class IndexModel : PageModel
    {

        public List<items> itemList = new List<items>();
        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = new HttpClient();
            string rss = await client.GetStringAsync("http://scripting.com/rss.xml");

            XmlDocument document = new XmlDocument();
            document.LoadXml(rss);
            XmlNodeList itemNodes = document.GetElementsByTagName("item");
          
           
            
            
           for(int i=0;i<itemNodes.Count;i++)
           {
                items item = new items();

                item.title = itemNodes[i].SelectSingleNode("title")?.InnerText;
                string descriptions = itemNodes[i].SelectSingleNode("description")?.InnerText;
                HtmlString htmlDescription = new HtmlString(descriptions);
                item.description = htmlDescription;
                item.link = itemNodes[i].SelectSingleNode("link")?.InnerText;
                item.guid = itemNodes[i].SelectSingleNode("guid")?.InnerText;
                item.pubDate = itemNodes[i].SelectSingleNode("pubDate")?.InnerText;

               
                itemList.Add(item);
            }


            return Page();
        }
    }

    public class items
    {
        public string title { get; set; }
        public HtmlString? description { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string image { get; set; }
        public string pubDate { get; set; }


    }
}