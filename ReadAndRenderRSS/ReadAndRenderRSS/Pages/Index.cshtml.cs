using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.AspNetCore.Html;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ReadAndRenderRSS.Pages
{
    public class IndexModel : PageModel
    {
        public List<Node> NodesList { get; private set; }
        public IPagedList<Item> ItemList { get; private set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int NumberOfPages { get; set; }

        public IndexModel()
        {
            NodesList = new List<Node>();
        }

        public async Task<IActionResult> OnGetAsync(int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue)
            {
                PageNumber = pageNumber.Value;
            }

            if (pageSize.HasValue)
            {
                PageSize = pageSize.Value;
            }

            HttpClient client = new HttpClient();
            string rss = await client.GetStringAsync("https://blue.feedland.org/opml?screenname=dave");
            XmlDocument document = new XmlDocument();
            document.LoadXml(rss);
            XmlNodeList outlineNodes = document.GetElementsByTagName("outline");

            for (int i = 0; i < outlineNodes.Count; i++)
            {
                Node node = new Node();

                node.Text = outlineNodes[i].Attributes["text"]?.Value ?? "";
                node.XmlUrl = outlineNodes[i].Attributes["xmlUrl"]?.Value ?? "";
                node.HtmlUrl = outlineNodes[i].Attributes["htmlUrl"]?.Value ?? "";

                NodesList.Add(node);
            }

            List<Item> items = new List<Item>();

            for (int i = 0; i < NodesList.Count; i++)
            {
                string xml = await client.GetStringAsync(NodesList[i].XmlUrl);
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(xml);
                XmlNodeList ItemNodes = document1.GetElementsByTagName("item");

                for (int j = 0; j < ItemNodes.Count; j++)
                {
                    Item item = new Item();
                    item.Title = ItemNodes[j].SelectSingleNode("title")?.InnerText;
                    string descriptions = ItemNodes[j].SelectSingleNode("description")?.InnerText;
                    HtmlString htmlDescription = new HtmlString(descriptions);
                    item.Description = htmlDescription;
                    item.Link = ItemNodes[j].SelectSingleNode("link")?.InnerText;
                    item.Guid = ItemNodes[j].SelectSingleNode("guid")?.InnerText;
                    item.PubDate = ItemNodes[j].SelectSingleNode("pubDate")?.InnerText;
                    item.Creator = ItemNodes[j].SelectSingleNode("creator")?.InnerText;

                    items.Add(item);
                }
            }

            NumberOfPages = (int)Math.Ceiling(items.Count / (double)PageSize);
            ItemList = items.ToPagedList(PageNumber, PageSize);

            return Page();
        }
    }

    public class Node
    {
        public string? Text { get; set; }
        public string? XmlUrl { get; set; }
        public string? HtmlUrl { get; set; }
    }

    public class Item
    {
        public string? Title { get; set; }
        public HtmlString? Description { get; set; }
        public string? Link { get; set; }
        public string? Guid { get; set; }
        public string? Image { get; set; }
        public string? PubDate { get; set; }
        public string? Creator { get; set; }
    }
}