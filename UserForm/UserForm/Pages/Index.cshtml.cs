using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserForm.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Contact> ContactList { get; private set; }
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
        public bool MarriageStatus { get; set; } = false;

        private readonly EdgeDBClient _edgeDbClient;
        public IndexModel(EdgeDBClient edgeDbClient)
        {
            ContactList = new List<Contact>();
            _edgeDbClient = edgeDbClient;
           
        }
        public async Task<IActionResult> OnGetAsync()
        {
            SearchTerm = Request.Query["SearchTerm"].ToString();

            var query = "SELECT Contact { FirstName, LastName, Email, Title, Description, DateOfBirth, MarriageStatus }";


            var result = await _edgeDbClient.QueryAsync<Contact>(query);


            foreach (var obj in result)
            {
                if (obj is Contact contact)
                {
                    if (contact.FirstName.Contains(SearchTerm)|| contact.LastName.Contains(SearchTerm)|| contact.Email.Contains(SearchTerm)) {
                        ContactList.Add(new Contact
                        {
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            Email = contact.Email,
                            Title = contact.Title,
                            Description = contact.Description,
                            DateOfBirth = contact.DateOfBirth,
                            MarriageStatus = contact.MarriageStatus
                        });
                    }
                }
            }
            return Page();
        }
    

        public async Task<IActionResult> OnPostAsync()
        {
            var contact = new Contact
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Title = Title,
                Description = Description,
                DateOfBirth = DateOfBirth,
                MarriageStatus = MarriageStatus
            };

            var result = await _edgeDbClient.QueryAsync<Contact>(
                "INSERT Contact { FirstName := <str>$FirstName, LastName := <str>$LastName, Email := <str>$Email, Title := <str>$Title, Description := <str>$Description, DateOfBirth := <str>$DateOfBirth, MarriageStatus := <bool>$MarriageStatus }",
                new Dictionary<string, object>
                {
                    { "FirstName", contact.FirstName },
                    { "LastName", contact.LastName },
                    { "Email", contact.Email },
                    { "Title", contact.Title },
                    { "Description", contact.Description },
                    { "DateOfBirth", contact.DateOfBirth },
                    { "MarriageStatus", contact.MarriageStatus }
                });

            return Page();
        }
    }
        public class Contact
        {
            public string FirstName { get; set; } = " ";
            public string LastName { get; set; } = " ";
            public string Email { get; set; } = " ";
            public string Title { get; set; }
            public string Description { get; set; }
            public string DateOfBirth { get; set; }
            public bool MarriageStatus { get; set; } = false;
        }
}