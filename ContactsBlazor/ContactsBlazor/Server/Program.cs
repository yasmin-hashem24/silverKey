using Microsoft.AspNetCore.ResponseCompression;
using EdgeDB;
using ContactsBlazor.Client;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEdgeDB(EdgeDBConnection.FromInstanceName("ContactsBlazor"), config =>
{
    config.SchemaNamingStrategy = INamingStrategy.SnakeCaseNamingStrategy;
});
var app = builder.Build();

app.MapPost("/login", async (HttpContext context, EdgeDBClient _edgeDbClient, Login login) =>
{
 
    string username = login.UserName;
    string password = login.Password;
    var query= "SELECT Contact {user_name, password, role, first_name, last_name, description, title, marriage_status} " +
                "FILTER Contact.user_name = <str>$username AND Contact.password = <str>$password LIMIT 1;";
    var contact = await _edgeDbClient.QuerySingleAsync(query, new Dictionary<string, object>
    {
        { "username", username },
        { "password", password }
    });

    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(JsonSerializer.Serialize(contact));
});

app.MapPost("/AddConactPost", async (HttpContext context, EdgeDBClient _edgeDbClient, InputContact contact) =>
{

    Console.WriteLine("WE INSIDE ADD CONTACT");
    Console.WriteLine(contact.FirstName);
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
    return Results.Ok();
});



app.MapPost("/SearchContactPost", async (HttpContext context, EdgeDBClient _edgeDbClient, string SearchTerm) =>
{
    List<Contact> ContactList = new();

    var query = "SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status,user_name }";


    var result = await _edgeDbClient.QueryAsync<Contact>(query);


    foreach (var contact in result)
    {
        if (contact.first_name.Contains(SearchTerm) || contact.last_name.Contains(SearchTerm) || contact.email.Contains(SearchTerm))
        {
            ContactList.Add(contact);
        }

    }
    return Results.Ok(ContactList);

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
