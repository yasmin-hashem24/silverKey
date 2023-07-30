using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
namespace UserInterface.Pages
{
    public class EditContactModel : PageModel
    {

        private readonly EdgeDBClient _edgeDbClient;
        public EditContactModel(EdgeDBClient edgeDbClient)
        {

            _edgeDbClient = edgeDbClient;

        }
        public async Task<IActionResult> OnGetAsync(string username)
        {
            // retrieve the contact with the specified username from the database
            var contact = await _edgeDbClient.QueryAsync<Contact>(
                "SELECT Contact FILTER Contact.UserName = <str>$username",
                new Dictionary<string, object>
                {
            { "username", username }
                });

            if (contact == null)
            {
                return NotFound();
            }

            // populate the input fields with the existing values
            // ...

            return Page();
        }
    }
}
