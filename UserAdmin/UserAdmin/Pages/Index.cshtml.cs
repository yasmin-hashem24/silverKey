using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace UserAdmin.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Login login { get; set; }

    public string role;

    private readonly EdgeDBClient _edgeDbClient;
    public IndexModel(EdgeDBClient edgeDbClient)
    {
        _edgeDbClient = edgeDbClient;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string username = login.UserName;
        string password = login.Password;

        var query = "SELECT Contact {user_name, password,role ,first_name,last_name,description,title,marriage_status} " +
                        "FILTER Contact.user_name = <str>$username AND Contact.password = <str>$password LIMIT 1;";


        var contact = await _edgeDbClient.QuerySingleAsync<Contact>(query, new Dictionary<string, object>
        {
            { "username", username },
            { "password", password }
        });


        if (contact == null)
        {
            ModelState.AddModelError("", "Invalid login");
            Console.WriteLine("No matching user found.");
            return Page();
        }

        if (contact != null)
        {

            return contact.role == "Admin" ? Redirect("/AddContact") : Redirect("/ViewUsers");
        }

        ModelState.AddModelError("", "Invalid login");
        Console.WriteLine("mafeesh");
        return Page();
    }
}

public class Contact
{
    public string first_name { get; set; } = " ";
    public string last_name { get; set; } = " ";
    public string email { get; set; } = " ";
    public string title { get; set; } = " ";
    public string description { get; set; } = " ";
    public string date_of_birth { get; set; } = " ";
    public string role { get; set; } = " ";
    public string user_name { get; set; } = " ";
    public string password { get; set; } = " ";
    public bool marriage_status { get; set; } = false;

}

public class Login
{
    public string UserName { get; set; } = " ";
    public string Password { get; set; } = " ";
    
}