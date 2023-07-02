using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var title = "";
var imageFileName = "";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.MapGet("/", () =>
{
    return Results.Content(@"
    <html>
        <head>
            <title>Image uploader form</title>
            <style>
                body {
                    background-image: linear-gradient(to bottom, #000000, #1d1d1d);
                    background-size: cover;
                }

                .content {
                    display: block;
                    padding: 10px 20px;
                    background-color: #08081e;
                    color: white;
                    font-size: 20px;
                    border: none;
                    cursor: pointer;
                    margin: 5px;
                    border-radius: 5px;
                }

                h1 {
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #722828;
                    color: white;
                    text-align: center;
                    font-size: 30px;
                    border: none;
                    cursor: pointer;
                    margin: 5px;
                    border-radius: 5px;
                    border-top-left-radius: 0;
                    border-top-right-radius: 0;
                }
            </style>
        </head>
        <body>
            <h1>Image uploader form</h1>

            <form method='post' enctype='multipart/form-data'>
                <div class='content'>
                    <label for='titleOfImage'>Enter image title:</label>
                    <input type='text' id='titleOfImage' name='titleOfImage' required>

                    <label for='imageFile'>Choose an image file:</label>
                    <input type='file' id='imageFile' name='imageFile' accept='image/jpeg, image/png, image/gif' required>

                    <button type='submit' id='submitButton' disabled>Submit</button>
                </div>
            </form>

            <script>
                const imageTitle = document.getElementById('titleOfImage');
                const image = document.getElementById('imageFile');
                const submitButton = document.getElementById('submitButton');

                imageTitle.addEventListener('input', checkInputs);
                image.addEventListener('input', checkInputs);

                function checkInputs() {
                    if (imageTitle.value.trim() !== '' && image.value.trim() !== '') {
                        submitButton.disabled = false;
                    } else {
                        submitButton.disabled = true;
                    }
                }
            </script>
        </body>
    </html>  ", "text/html");

});
app.MapGet("/picture/{id}", async (HttpContext context) =>
{
   
   
    byte[] imageBytes = await File.ReadAllBytesAsync(imageFileName);
    string imageArray = Convert.ToBase64String(imageBytes);
    // Generate the HTML code for the picture page using the title and image URL
    var html = $@"
    <html>
        <head>
            <title>Display page</title>
            <style>
                body {{
                    background-image: linear-gradient(to bottom, #000000, #1d1d1d);
                    background-size: cover;
                }}

                .content {{
                    display: block;
                    padding: 10px 20px;
                    background-color: #08081e;
                    color: white;
                    font-size: 20px;
                    border: none;
                    cursor: pointer;
                    margin: 5px;
                    border-radius: 5px;
                }}
                         .image-container {{display: flex;
                flex-direction: column;
                align-items: center;
            }}

            .image-container img {{max - width: 100%;
                max-height: 80vh;
            }}
                h1 {{
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #722828;
                    color: white;
                    text-align: center;
                    font-size: 30px;
                    border: none;
                    cursor: pointer;
                    margin: 5px;
                    border-radius: 5px;
                    border-top-left-radius: 0;
                    border-top-right-radius: 0;
                }}
            </style>
        </head>
        <body>
            <h1>Display page</h1>
           <div class='content'>
            <div class='image-container'>
                <output id='titleOfImage'>{title}</output>
                <img src=""data:image/png;base64,{imageArray}"" alt='{title}' />
            </div>
        </div>
        </body>


    </html>";

    return Results.Content(html, "text/html");
});

app.MapPost("/", async (HttpContext context) =>
{
    var file = context.Request.Form.Files["imageFile"];

     title = context.Request.Form["titleOfImage"];
    if (string.IsNullOrEmpty(title))
    {
        return Results.BadRequest("Title is required.");
    }

    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("File is required.");
    }

    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
    Console.WriteLine($"Extension: {extension}");
    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif")
    {
        return Results.BadRequest("image type not allowed");
    }


    var baseDirectory = Directory.GetCurrentDirectory();
    if (baseDirectory == null)
    {
        throw new Exception("Current directory is null.");
    }

    var imagesDirectory = Path.Combine(baseDirectory, "images");
    if (!Directory.Exists(imagesDirectory))
    {
        Directory.CreateDirectory(imagesDirectory);
    }

    var fileName = Path.Combine(imagesDirectory, file.FileName);
    using (var stream = new FileStream(fileName, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var imageData = new
    {
        Title = context.Request.Form["titleOfImage"],
        ImageFileName = file.FileName,
        Imagee = file
    };
    string json = JsonConvert.SerializeObject(imageData, Formatting.Indented);
    string filePath = Path.Combine(baseDirectory, "file.json");
    System.IO.File.AppendAllText(filePath, json);
    var id = Guid.NewGuid().ToString();

    title = imageData.Title;
    imageFileName = fileName;

    return Results.Redirect($"/picture/{id}");
});

app.Run();

