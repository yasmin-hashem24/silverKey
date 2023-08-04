using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using Microsoft.AspNetCore.Identity;

namespace UserAdmin.Pages;

public class AddContactModel : PageModel
{
    [BindProperty]
    public List<Contact> ContactList { get;  set; } = new();
    [BindProperty]
    public InputContact contact { get;  set; }
    [BindProperty]
    public string SearchTerm { get; set; }

    private readonly EdgeDBClient _edgeDbClient;
    public AddContactModel(EdgeDBClient edgeDbClient)
    {
        _edgeDbClient = edgeDbClient;
    }
    public IActionResult OnPostLogout()
    {

        return RedirectToPage("/Index");
    }
    public IActionResult OnPostEdit(string username)
    {
        Console.WriteLine(username);
        return RedirectToPage("/EditContact", new { username });
    }


    public async Task<IActionResult> OnGetAsync()
    {
        SearchTerm = Request.Query["SearchTerm"].ToString();
        var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status,user_name }";


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


    public async Task<IActionResult> OnPostAsync()
    {
        var passwordHasher = new PasswordHasher<string>();
        string hashedPassword = passwordHasher.HashPassword(null, contact.Password);
       

        var result = await _edgeDbClient.QueryAsync<Contact>(
    "INSERT Contact { first_name := <str>$first_name, last_name := <str>$last_name, user_name := <str>$user_name, email := <str>$email, title := <str>$title, password := <str>$password, role := <str>$role, description := <str>$description, date_of_birth := <str>$date_of_birth, marriage_status := <bool>$marriage_status }",
    new Dictionary<string, object>
    {
            { "first_name", contact.FirstName },
            { "last_name", contact.LastName },
            { "email", contact.Email },
            { "title", contact.Title },
            { "description", contact.Description },
            { "date_of_birth", contact.DateOfBirth },
            { "marriage_status", contact.MarriageStatus },
            { "role", contact.Role },
            { "user_name", contact.UserName},
            { "password", hashedPassword}
    });

        return Page();
    }
}

public class InputContact
{
    public string FirstName { get; set; } = " ";
    public string LastName { get; set; } = " ";
    public string Email { get; set; } = " ";
    public string Title { get; set; } = " ";
    public string Description { get; set; } = " ";
    public string DateOfBirth { get; set; } = " ";
    public string UserName { get; set; } = " ";
    public string Password { get; set; } = " ";
    public string Role { get; set; } = " ";
    public bool MarriageStatus { get; set; } = false;
}