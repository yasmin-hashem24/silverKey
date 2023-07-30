using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
namespace UserInterface.Pages
{
   
    public class ViewUsersModel : PageModel
    {
        [BindProperty]
        public List<Contact> ContactList { get; private set; } = new List<Contact>();
        private readonly EdgeDBClient _edgeDbClient;
        public ViewUsersModel(EdgeDBClient edgeDbClient)
        {
            ContactList = new List<Contact>();
            _edgeDbClient = edgeDbClient;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            

            var query = "SELECT Contact { FirstName, LastName, Email, Title, Description, DateOfBirth, MarriageStatus,Role,UserName }";


            var result = await _edgeDbClient.QueryAsync<Contact>(query);


            foreach (var obj in result)
            {
                if (obj is Contact contact)
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
            return Page();
        }
    }
}
