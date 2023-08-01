using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using System.Text.Json;
namespace UserInterface.Pages;
public class EditContactModel : PageModel
{
    private readonly EdgeDBClient _edgeDbClient;
    [BindProperty]
    public Contact ContactTemp { get; set; } = new();

    public EditContactModel(EdgeDBClient edgeDbClient)
    {
        _edgeDbClient = edgeDbClient;
    }

  
    public async Task<IActionResult> OnGetAsync(string username)
    {
        string query = "SELECT Contact {FirstName, LastName, Email, Title, Description, DateOfBirth, MarriageStatus,Role,UserName } " +
                                        "FILTER Contact.UserName = <str>$username;";

        var parameters = new Dictionary<string, object>
            {
                { "username", username }
            };
        // Execute the EdgeDB query and retrieve the result
        var contact = await _edgeDbClient.QueryAsync<Contact>(query, parameters);

        // Retrieve the first contact in the collection (assuming there's only one)
        ContactTemp = contact.FirstOrDefault();
       
        

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        string UserName = Request.Form["UserName"];
        string query1 = "SELECT Contact {FirstName, LastName, Email, Title, Description, DateOfBirth, MarriageStatus,Role,UserName } " +
                                        "FILTER Contact.UserName = <str>$UserName;";

        var parameters = new Dictionary<string, object>
            {
                { "UserName", UserName }
            };
        // Execute the EdgeDB query and retrieve the result
        var contact = await _edgeDbClient.QueryAsync<Contact>(query1, parameters);
       
        // Retrieve the first contact in the collection (assuming there's only one)
        ContactTemp = contact.FirstOrDefault();
        
        var query = @"
        UPDATE Contact
        FILTER Contact.UserName = <str>$UserName
        SET {
            FirstName := <str>$FirstName,
            LastName := <str>$LastName
        }";

        await _edgeDbClient.ExecuteAsync(query, new Dictionary<string, object?>
    {
        { "UserName", ContactTemp.UserName },
        { "FirstName", ContactTemp.FirstName },
        { "LastName", ContactTemp.LastName }
    });

        return RedirectToPage("/AddContact");
    }
}
