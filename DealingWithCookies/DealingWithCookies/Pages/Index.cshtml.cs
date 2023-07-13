using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.AspNetCore.Html;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using System.Text.Json;
namespace DealingWithCookies.Pages
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
                    item.Description = ItemNodes[j].SelectSingleNode("description")?.InnerText;
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

       
        public IActionResult OnPostToggleFavorite(string pubDate, bool isStared, string description, string link)
        {

            if (Request.Cookies["starredCookie"] != null)
            {
                string cookieValue = Request.Cookies["starredCookie"];
                Console.WriteLine(cookieValue);
            }
            else
            {
                Console.WriteLine("Cookie not found.");
            }

            var starredItems = Request.Cookies["starredCookie"] != null ?
                JsonSerializer.Deserialize<Dictionary<string, Report>>(Request.Cookies["starredCookie"]) :
                new Dictionary<string, Report>();

            
            var report = starredItems.ContainsKey(pubDate) ? starredItems[pubDate] : null;
            if (report == null)
            {
              
                report = new Report
                {
                    PubDate = pubDate,
                    Description = description,
                    Link = link,
                    Stared = isStared
                };
            }
            starredItems[report.PubDate] = report;
            Response.Cookies.Append("starredCookie", JsonSerializer.Serialize(starredItems), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(365),
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Secure = true 
            });
            System.IO.File.WriteAllText("starredCookie.json", JsonSerializer.Serialize(starredItems.Values.ToList()));
            return RedirectToPage();
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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Image { get; set; }
        public string PubDate { get; set; }
        public string Creator { get; set; }
        public bool Stared { get; set; }

        public Report()
        {
            
            Title = string.Empty;
            Description = string.Empty;
            Link = string.Empty;
            Guid = string.Empty;
            Image = string.Empty;
            PubDate = string.Empty;
            Creator = string.Empty;
            Stared = false;
        }
    }
}