using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using System.Text.Json;
namespace UserInterface.Pages
{
   
    public class ViewUsersModel : PageModel
    {
        [BindProperty]
        public List<dynamic> ContactList { get; private set; } = new();
        [BindProperty]
        public List<Contact> ContactListe { get; private set; } = new();
        private readonly EdgeDBClient _edgeDbClient;
        public ViewUsersModel(EdgeDBClient edgeDbClient)
        {
          
            _edgeDbClient = edgeDbClient;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            

            var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status,role,user_name }";


            var result = await _edgeDbClient.QueryAsync(query);
           
            foreach (var contact in result)
            {
                if (contact != null)
                {

                    ContactList.Add(contact);

                }

            }

            ContactListe = ContactList.ConvertAll(c => new Contact
            {
                FirstName = c.first_name ?? "",
                LastName = c.last_name ?? "",
                Email = c.email ?? "",
                Title = c.title ?? "",
                Description = c.description ?? "",
                DateOfBirth = c.date_of_birth ?? "",
                MarriageStatus = c.marriage_status ?? false
            });
            return Page();
        }
    }
}
