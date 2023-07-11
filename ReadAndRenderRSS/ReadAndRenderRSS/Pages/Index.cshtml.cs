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
        private readonly IHttpClientFactory _clientFactory;
        public List<Feed> NodesList { get; private set; }
        public IPagedList<Report> ItemList { get; private set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int NumberOfPages { get; set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            NodesList = new List<Feed>();
            _clientFactory = clientFactory;
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

            HttpClient httpClient = _clientFactory.CreateClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync("https://blue.feedland.org/opml?screenname=dave");

            HttpContent responseContent = httpResponse.Content;
            string responseData = await responseContent.ReadAsStringAsync();
            XmlDocument document = new XmlDocument();
            document.LoadXml(responseData);
            XmlNodeList outlineNodes = document.GetElementsByTagName("outline");

            for (int i = 0; i < outlineNodes.Count; i++)
            {
                Feed node = new Feed();

                node.Text = outlineNodes[i].Attributes["text"]?.Value ?? "";
                node.XmlUrl = outlineNodes[i].Attributes["xmlUrl"]?.Value ?? "";
                node.HtmlUrl = outlineNodes[i].Attributes["htmlUrl"]?.Value ?? "";

                NodesList.Add(node);
            }

            List<Report> items = new List<Report>();

            for (int i = 0; i < NodesList.Count; i++)
            {
                string xml = await httpClient.GetStringAsync(NodesList[i].XmlUrl);
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(xml);
                XmlNodeList ItemNodes = document1.GetElementsByTagName("item");

                for (int j = 0; j < ItemNodes.Count; j++)
                {
                    Report item = new Report();
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
            httpClient.Dispose();
            return Page();
        }
    }

    public class Feed
    {
        public string? Text { get; set; }
        public string? XmlUrl { get; set; }
        public string? HtmlUrl { get; set; }
    }

    public class Report
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