using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.IO;

namespace silverKeyWebApp.Pages
{
    public class imageInfo
    {
        public string imageTitle { get; set; }
        public IFormFile image { get; set; }
    }

    public class imageUploaderModel : PageModel
    {
        public string imageTitle { get; set; }
        public IFormFile image { get; set; }
        public bool dataGiven = false;
        imageInfo myImage = new imageInfo();
        public int ImageId { get; set; }

        private readonly IWebHostEnvironment _env;

        public imageUploaderModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            dataGiven = true;
            imageTitle = Request.Form["titleOfImage"];

            IFormFile imageFile = Request.Form.Files.GetFile("imageFile");

            myImage.imageTitle = imageTitle;
            myImage.image = imageFile;
        

            string fileName = Path.GetFileName(imageFile.FileName);
            string imagePath = Path.Combine(_env.WebRootPath, "images", fileName);

            // Set the ImageId property
            int imageIde = Guid.NewGuid().GetHashCode();

            // Set the ImageId property
           // ImageId = imageIde;
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

           // myImage.imagePath = imagePath;

            string json = JsonConvert.SerializeObject(myImage, Formatting.Indented);
            string filePath = Path.Combine(_env.ContentRootPath, "file.json");
            System.IO.File.AppendAllText(filePath, json);

            string imageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/{fileName}";
            return RedirectToPage("/picture", new { title = imageTitle, image = imageUrl, imageId = imageIde });
        
        }

     


    }
}