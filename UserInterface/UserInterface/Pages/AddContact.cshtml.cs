using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using Microsoft.AspNetCore.Identity;
namespace UserInterface.Pages;

public class AddContactModel : PageModel
{
    [BindProperty]
    public List<Contact> ContactList { get; private set; } = new();
    [BindProperty]
    public string SearchTerm { get; set; }

    [BindProperty]
    public string FirstName { get; set; }
    [BindProperty]
    public string LastName { get; set; }
    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Title { get; set; }
    [BindProperty]
    public string Description { get; set; }
    [BindProperty]
    public string DateOfBirth { get; set; }

    [BindProperty]
    public string UserName { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string Role { get; set; }
    [BindProperty]
    public bool MarriageStatus { get; set; } = false;

    private readonly EdgeDBClient _edgeDbClient;
    public AddContactModel(EdgeDBClient edgeDbClient)
    {
        ContactList = new List<Contact>();
        _edgeDbClient = edgeDbClient;

    }
    public IActionResult OnPostLogout()
    {
       
        return RedirectToPage("/Index");
    }
    public IActionResult OnPostEdit(string username)
    {
        return RedirectToPage("/EditContact", new { username });
    }

    public async Task<IActionResult> OnGetAsync()
    {
        SearchTerm = Request.Query["SearchTerm"].ToString();
        var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status }";
      

        var result = await _edgeDbClient.QueryAsync(query);


        foreach (var obj in result)
        {
            if (obj is Contact contact)
            {
                if (contact.FirstName.Contains(SearchTerm) || contact.LastName.Contains(SearchTerm) || contact.Email.Contains(SearchTerm))
                {
                    ContactList.Add(new Contact
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        Title = contact.Title,
                        Description = contact.Description,
                        DateOfBirth = contact.DateOfBirth,
                        MarriageStatus = contact.MarriageStatus,
                        Role = contact.Role,
                        UserName = contact.UserName
                    });
                }
            }
        }
        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        var passwordHasher = new PasswordHasher<string>();
        string hashedPassword = passwordHasher.HashPassword(null, Password);
        var contact = new Contact
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Title = Title,
            Description = Description,
            DateOfBirth = DateOfBirth,
            MarriageStatus = MarriageStatus,
            UserName = UserName,
            Role = Role,
            Password = hashedPassword
        };

            var result = await _edgeDbClient.QueryAsync<Contact>(
        "INSERT Contact { FirstName := <str>$first_name, LastName := <str>$last_name, UserName := <str>$user_name, Email := <str>$email, Title := <str>$title, Password := <str>$password, Role := <str>$role, Description := <str>$description, DateOfBirth := <str>$date_of_birth, MarriageStatus := <bool>$marriage_status }",
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
            { "password", contact.Password}
        });

        return Page();
    }
}
public class Contact
{
    public string FirstName { get; set; } = " ";
    public string LastName { get; set; } = " ";
    public string Email { get; set; } = " ";
    public string Title { get; set; } = " ";
    public string Description { get; set; } = " ";
    public string DateOfBirth { get; set; } = " ";
    public string Role { get; set; } = " ";
    public string UserName { get; set; } = " ";
    public string Password { get; set; } = " ";
    public bool MarriageStatus { get; set; } = false;
}