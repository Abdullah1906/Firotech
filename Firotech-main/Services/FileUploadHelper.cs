namespace Firotechbd.Services
{
    public class FileUploadHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<(bool Success, string FileName, string Message)> UploadFileAsync(List<IFormFile> files, string subDirectory)
        {
            if (files == null || !files.Any() )
            {
                return (false, null, "No files uploaded.");
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // Validate file type if necessary
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!validExtensions.Contains(fileExtension))
                    {
                        return (false, null, "Invalid file type.");
                    }

                    // Create a unique file name to avoid conflicts
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                    // Set the path where you want to save the uploaded file
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", subDirectory);
                    Directory.CreateDirectory(uploadsFolder); // Create directory if it doesn't exist
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Return a success response with the file name
                    return (true, uniqueFileName, null);
                }
            }

            return (false, null, "File upload failed.");
        }
    }
}
