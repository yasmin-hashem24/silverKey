using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DealingWithCookies.Pages;
using System.Net.Http;
using Microsoft.AspNetCore.Html;
using System.Xml;
using X.PagedList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
var app = builder.Build();


var services = new ServiceCollection();
services.AddHttpClient();
services.AddSingleton<IndexModel>();
var serviceProvider = services.BuildServiceProvider();
var indexModel = serviceProvider.GetService<IndexModel>();
var NodesList = indexModel.NodesList;
var ItemList = indexModel.ItemList;
var PageSize = indexModel.PageSize;
var PageNumber = indexModel.PageNumber; 
var NumberOfPages = indexModel.NumberOfPages;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/togglefavorite", async (HttpContext context) =>
{
    var pubDate = context.Request.Form["pubDate"];
    var isStared = bool.Parse(context.Request.Form["isStared"]);
    var description = context.Request.Form["description"];
    var link = context.Request.Form["link"];

    var starredItems = context.Request.Cookies["starredCookie"] != null ?
        JsonSerializer.Deserialize<Dictionary<string, Report>>(context.Request.Cookies["starredCookie"]) :
        new Dictionary<string, Report>();

    var report = starredItems.ContainsKey(pubDate) ? starredItems[pubDate] : null;
    if (report == null)
    {
        report = new Report
        {
            PubDate = pubDate,
            Description = description,
            Link = link,
            Stared = isStared
        };
    }
    else
    {
        report.Stared = !report.Stared;
        if (!report.Stared)
        {
            starredItems.Remove(pubDate);
        }
    }
    starredItems[report.PubDate] = report;
    context.Response.Cookies.Append("starredCookie", JsonSerializer.Serialize(starredItems), new CookieOptions
    {
        Expires = DateTime.UtcNow.AddDays(365),
        SameSite = SameSiteMode.Strict,
        HttpOnly = true,
        Secure = true
    });
    await System.IO.File.WriteAllTextAsync("starredCookie.json", JsonSerializer.Serialize(starredItems.Values.ToList()));
    await context.Response.WriteAsJsonAsync(new { pubDate = report.PubDate, stared = report.Stared });
});


app.Run();
public class Feed
{
    public string? Text { get; set; }
    public string? XmlUrl { get; set; }
    public string? HtmlUrl { get; set; }
}

public class Report
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Guid { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string PubDate { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public bool Stared { get; set; } = false;
}