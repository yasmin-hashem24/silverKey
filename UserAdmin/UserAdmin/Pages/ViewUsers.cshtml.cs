using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using Microsoft.AspNetCore.Identity;
namespace UserAdmin.Pages;

public class ViewUsersModel : PageModel
{
    [BindProperty]
    public List<Contact> ContactList { get; set; } = new();
    [BindProperty]
    public string SearchTerm { get; set; }

    private readonly EdgeDBClient _edgeDbClient;
    public ViewUsersModel(EdgeDBClient edgeDbClient)
    {
        _edgeDbClient = edgeDbClient;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        SearchTerm = Request.Query["SearchTerm"].ToString();
        var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status }";


        var result = await _edgeDbClient.QueryAsync<Contact>(query);


        foreach (var contact in result)
        {
            if (contact.first_name.Contains(SearchTerm) || contact.last_name.Contains(SearchTerm) || contact.email.Contains(SearchTerm))
            {
                ContactList.Add(contact);
            }

        }
        return Page();
    }

}
