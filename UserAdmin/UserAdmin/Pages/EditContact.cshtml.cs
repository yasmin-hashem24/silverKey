using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using System.Text.Json;

namespace UserAdmin.Pages;

public class EditContactModel : PageModel
{
    private readonly EdgeDBClient _edgeDbClient;
  
    public Contact ContactTemp { get; set; }

    [BindProperty]
    public Contact ContactInput { get; set; }

    public EditContactModel(EdgeDBClient edgeDbClient)
    {
        _edgeDbClient = edgeDbClient;
    }


    public async Task<IActionResult> OnGetAsync(string username)
    {
       
        var query = "SELECT Contact {user_name, password,role ,first_name,last_name,description,title,marriage_status} " +
                       "FILTER Contact.user_name = <str>$username LIMIT 1;";

        var parameters = new Dictionary<string, object>
            {
                { "username", username }
            };
        
        var contact = await _edgeDbClient.QuerySingleAsync<Contact>(query, parameters);

     
        ContactTemp = contact;



        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {

      
        string username = Request.Form["UserName"];
        ContactInput.first_name= Request.Form["FirstName"];
        ContactInput.last_name = Request.Form["LastName"];
        ContactInput.email = Request.Form["Email"];
        Console.WriteLine(ContactInput.first_name);

        var query = @"
                    UPDATE Contact
                    FILTER Contact.user_name = <str>$username
                    SET {
                        first_name := <str>$first_name,
                        last_name := <str>$last_name
                    }";
                            await _edgeDbClient.ExecuteAsync(query, new Dictionary<string, object?>
                    {
                        { "username", username },
                        { "first_name", ContactInput.first_name },
                        { "last_name", ContactInput.last_name }
                    });

        return RedirectToPage("/AddContact");
    }
}
