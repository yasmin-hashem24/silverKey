using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
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


            var query = $@"SELECT Contact {{ user_name, role }} FILTER .user_name = <str>$UserName AND .password = <str>$Password LIMIT 1;";
            var contact = await _edgeDbClient.QuerySingleAsync<Contact>(query, new Dictionary<string, object?>
            {
                ["user_name"] = UserName,
                ["password"] = Password
            });


            if (contact == null)
            {
                ModelState.AddModelError("", "Invalid login");
                Console.WriteLine("No matching user found.");
                return Page();
            }

            var json = JsonSerializer.Serialize(contact, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);

            if (contact != null)
            {
               
                return contact.Role == "Admin" ? Redirect("/AddContact") : Redirect("/ViewUsers");
            }
           
            ModelState.AddModelError("", "Invalid login");
            Console.WriteLine("mafeesh");
            return Page();
        }
    }
}