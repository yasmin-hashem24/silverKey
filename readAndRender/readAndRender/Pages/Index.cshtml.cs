using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Xml;

using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace readAndRender.Pages


{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        public List<items> itemList = new List<items>();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

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
                item.description = itemNodes[i].SelectSingleNode("description")?.InnerText;
                item.link = itemNodes[i].SelectSingleNode("link")?.InnerText;
                item.guid = itemNodes[i].SelectSingleNode("guid")?.InnerText;
                item.pubDate = itemNodes[i].SelectSingleNode("pubDate")?.InnerText;

                string imgPattern = @"<img.*?src=""(.*?)"".*?>";
                Match imgMatch = Regex.Match(item.description, imgPattern);
                if (imgMatch.Success)
                {
                    string imageUrl = imgMatch.Groups[1].Value;

                    item.image = imageUrl;
                    item.description = Regex.Replace(item.description, imgPattern, "", RegexOptions.IgnoreCase);
                }
               

                itemList.Add(item);
            }


            return Page();
        }
    }

    public class items
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string image { get; set; }
        public string pubDate { get; set; }


    }
}