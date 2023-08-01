using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace UserForm.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<dynamic> ContactList { get; private set; } = new();
        [BindProperty]
        public Contact ContactInput { get; set; }
        [BindProperty]
        public String SearchTerm { get; set; }
        public List<Contact> ContactListe { get; private set; } = new();

        private readonly EdgeDBClient _edgeDbClient;
        public IndexModel(EdgeDBClient edgeDbClient)
        {
            _edgeDbClient = edgeDbClient;
         
        }
        public async Task<IActionResult> OnGet()
        {
            var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status }";

            query += $" FILTER {{ (LOWER(.first_name) LIKE '%{SearchTerm.ToLower()}%') OR (LOWER(.last_name) LIKE '%{SearchTerm.ToLower()}%') OR (LOWER(.email) LIKE '%{SearchTerm.ToLower()}%') }}";
        
            var contacts = await _edgeDbClient.QueryAsync(query);
            foreach (var contact in contacts)
            {
                if (contact != null)
                {

                        ContactList.Add(contact);
                   
                }

            }

             Console.WriteLine(JsonSerializer.Serialize(ContactList));
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


        public async Task<IActionResult> OnPostAsync()
        {
            

            var result = await _edgeDbClient.QueryAsync(
              "INSERT Contact { first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <str>$title, description := <str>$description, date_of_birth := <str>$date_of_birth, marriage_status := <bool>$marriage_status }",
              new Dictionary<string, object>
              {
                { "first_name", ContactInput.FirstName },
                { "last_name", ContactInput.LastName },
                { "email", ContactInput.Email },
                { "title", ContactInput.Title },
                { "description", ContactInput.Description },
                { "date_of_birth", ContactInput.DateOfBirth },
                { "marriage_status", ContactInput.MarriageStatus }
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
            public bool MarriageStatus { get; set; } = false;
        public Contact()
        {

        }
        public Contact(string firstName, string lastName, string email, string title, string description, string dateOfBirth, bool marriagestatus)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Title = title;
            Description = description;
            DateOfBirth = dateOfBirth;
            MarriageStatus = marriagestatus;
        }
    }
}