using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace silverKeyWebApp.Pages
{
    public class pictureModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string title { get; set; }

        [BindProperty(SupportsGet = true)]
        public string image { get; set; }

        public int ImageId { get; set; } 
        public void OnGet()
        {
        }
    }
}
