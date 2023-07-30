using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserInterface.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }

        private readonly EdgeDBClient _edgeDbClient;
        public IndexModel(EdgeDBClient edgeDbClient)
        {
            _edgeDbClient = edgeDbClient;
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var query = $@"SELECT Contact {{ UserName, Role }} FILTER .UserName = <str>$UserName;";

            var result = await _edgeDbClient.QueryAsync<Contact>(query, new Dictionary<string, object?>
            {
                ["UserName"] = UserName
            });

           
            if (result.Count == 0 || result == null)
            {
                ModelState.AddModelError("", "Invalid login");
                Console.WriteLine("No matching user found.");
                return Page();
            }
            var user = result.SingleOrDefault();

            if (user != null)
            {
               
                Console.WriteLine("fe user ah");
                return user.Role == "Admin" ? Redirect("/AddContact") : Redirect("/ViewUsers");
            }

            ModelState.AddModelError("", "Invalid login");
            Console.WriteLine("mafeesh");
            return Page();
        }
    }
}