using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
namespace DealingWithCookies.Pages
{
    public class StarredFeedsModel : PageModel
    {
        public List<Report> StarredReports { get; set; }
        public void OnGet()
        {
            var starredItems = Request.Cookies["starredCookie"] != null ?
               JsonSerializer.Deserialize<Dictionary<string, Report>>(Request.Cookies["starredCookie"]) :
               new Dictionary<string, Report>();

            var reports = JsonSerializer.Deserialize<List<Report>>(System.IO.File.ReadAllText("starredCookie.json"));

            StarredReports = reports.Where(r => starredItems.ContainsKey(r.PubDate))
                         .OrderByDescending(r => r.PubDate)
                         .ToList();
        }
    }
}
