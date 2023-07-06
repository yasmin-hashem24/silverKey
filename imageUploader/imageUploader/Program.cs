var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Global variables to be accessable within any mapping.
var title = "";
var imageFileName = "";

app.MapGet("/", () =>
{
    return Results.Content(@"
   <html>
  <head>
    <title>Image Uploader</title>
    <style>

      body{
        font-family: Arial, Helvetica, sans-serif;
      }
      form{
        display: flex;
        justify-content: center;
     }
     label{
        font-size: 24px;
        font-weight: 700;
     }
      .title{
        text-align: center;
        padding-top: 5%;
        padding-bottom: 2%;
      }

      .textbox{
        width: 20%;
        height: 30px;
      }

      .upload-image-frame {
        width: 40%;
        height: 40%;
        border: 5px dashed black;
        margin: 0 auto;
        display: flex;
        justify-content: center;
        align-items: center;
      }

      .chooseButton{
        display: flex;
        justify-content: center;
        align-items: center;
        width:15%;
        height: 70px;
        background-color: #919297;
        font-size: 22px;
        font-weight: 700;
        border-radius: 10px;
        position: absolute;

      }

      .chooseButton:hover{
        cursor: pointer;
        background-color: #ADAEB3; ;
    }

    .uploadButton{
        display: flex;
        justify-content: center;
        width:30%;
        height: 40px;
        font-size: 20px;
        padding-top: 7px;
        font-weight: 700;
        margin: 2%;
        border-radius: 10px;
        background-color: #04aa6d;
        cursor: pointer;
        border: 0 none;
    }
    .uploadButton:hover{
        background-color: #00ba6d;
    }
    </style>
  </head>

  <body>
    
    <h1 class=""title""> Image Uploader</h1>
    <form method=""post"" enctype=""multipart/form-data"">
        <label for=""imageTitle"">Enter Title: </label>
        <input type=""text"" name=""imageTitle"" class=""textbox"">
        <input type=""file"" name=""image"" id=""fileToUpload"" hidden>
        <input type=""submit"" value=""Upload Image"" name=""submit"" id=""submitButton"" hidden>
    </form>
    <div class=""upload-image-frame"">
        <img src="""" id=""image"" style=""width: 99%; height: 99%; object-fit: contain;"">
        <div class=""chooseButton"" id=""choosePic""> Choose Image</div>
    </div>
    <div style=""display: flex; justify-content: space-evenly; width:40%; margin: 0 auto ;"">
    <button class=""uploadButton"" id=""changeButton"">Change Pic</button>
    <button class=""uploadButton"" id=""fakeSubmitButton"">Upload</button>
    </div>



  </body>

  <script>
    const realSubmitButton = document.getElementById(""submitButton"");
    const realChoosePic = document.getElementById(""fileToUpload"");
    const fakeChoosePic = document.getElementById(""choosePic"");
    const fakeSubmitButton = document.getElementById(""fakeSubmitButton"");
    const img = document.getElementById(""image"");

    fakeChoosePic.addEventListener(""click"", function(){
      realChoosePic.click();
      fakeChoosePic.remove();
    });

    fakeSubmitButton.addEventListener(""click"", function(){
      realSubmitButton.click();
    });

    changeButton.addEventListener(""click"", function(){
        realChoosePic.click();
    });

    realChoosePic.addEventListener(""change"", function(){
        const file = this.files[0];
        if(file){
            const reader = new FileReader();
            reader.addEventListener(""load"", function(){
                img.setAttribute(""src"", this.result);
            });
            reader.readAsDataURL(file);
        }
    });
  </script>
</html>
", "text/html");
});

app.MapGet("/picture/{id}", async (HttpContext context) =>
{
    byte[] imageBytes = await File.ReadAllBytesAsync(imageFileName);
    Console.Write(imageFileName);
    string imageArray = Convert.ToBase64String(imageBytes);

    return Results.Content(@"<html>
  <head>
    <title>Image Uploader</title>
    <style>
      body {{
        font-family: Arial, Helvetica, sans-serif;
      }}
      .title {{
        text-align: center;
        padding-top: 5%;
        padding-bottom: 2%;
      }}
      .upload-image-frame {{
        width: 50%;
        height: 50%;
        border: 5px dashed black;
        margin: 0 auto;
        display: flex;
        justify-content: center;
        align-items: center;
      }}

      .img{{
        height: 100%;
      }}
    </style>
  </head>

  <body>
    <h1 class=""title"">Title Here</h1>
   <div class='image-container'>
                <output id='titleOfImage'>{title}</output>
                <img src=""data:image/png;base64,{imageArray}"" alt='{title}' />
            </div>
  </body>
</html>", "text/html");

});

app.MapPost("/", async (HttpContext context) =>
{
    var image = context.Request.Form.Files["image"];

    //Making server-side validations

    // 1. Check image is not null
    if (image == null || image.Length == 0)
        return Results.BadRequest("Kindly choose an image.");

    // 2. Check existance of a title
    var imageTitle = context.Request.Form["imageTitle"];
    if (string.IsNullOrEmpty(imageTitle))
        return Results.BadRequest("Kindly add a title for your image.");

    // 3. Check valid extenstions
    var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif")
        return Results.BadRequest("Please enter a vaild file extension.");

    //Creating a directory to save uploaded images

    // 1. Check that the current directory exists 
    var currDir = Directory.GetCurrentDirectory();
    if (currDir == null)
        throw new Exception("No directory exists.");

    // 2. Check whether images directory is created or not
    var imagesDirectory = Path.Combine(currDir, "images");
    if (!Directory.Exists(imagesDirectory))
        Directory.CreateDirectory(imagesDirectory);

    // 3. Save image file 
    var imageName = Path.Combine(imagesDirectory, image.FileName);
    using (var stream = new FileStream(imageName, FileMode.Create))
        await image.CopyToAsync(stream);

    // Creating a unqiue ID for an image
    var uniqueID = Guid.NewGuid().ToString();

    title = context.Request.Form["imageTitle"];
    imageFileName = imageName;

    return Results.Redirect($"/picture/{uniqueID}");
});




app.Run();

