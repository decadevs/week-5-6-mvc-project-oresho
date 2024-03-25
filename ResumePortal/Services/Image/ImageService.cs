
using ResumePortal.Models.Dtos;

namespace ResumePortal.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public GenericResponse DeleteImage(string uniqueName)
        {
            GenericResponse response = new GenericResponse();
            string basePath = _hostEnvironment.WebRootPath + "/Images";
            string fullPath = Path.Combine(basePath, uniqueName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                response.Message = "Successfully deleted file";
                response.Status = 200;
                return response;
            }
            else
            {
                response.Message = "No file with this name";
                response.Status = 400;
                return response;
            }
        }

        public GenericResponse<string> UploadImage(IFormFile photo)
        {
            GenericResponse<string> response = new GenericResponse<string>();
            string[] allowedTypes = { "image/jpeg", "image/png", "image/jpg" };
            if (photo == null || photo.Length <= 0)
            {
                response.Status = 400;
                response.Message = "Image cannot be null or empty";
                return response;
            }
            if (photo.Length > 100000)
            {
                response.Status = 400;
                response.Message = "Maximum Image size is 2Mb";
                return response;
            }
            if (!allowedTypes.Contains(photo.ContentType))
            {
                response.Status = 400;
                response.Message = "Acceptable types are .png, .jpg and .jpeg";
                return response;
            }

            string basePath = _hostEnvironment.WebRootPath + "/images";
            string uniqueName = Guid.NewGuid().ToString() + "-" + photo.FileName;
            string fullPath = Path.Combine(basePath, uniqueName);
            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fs);
            }

            response.Status = 200;
            response.Message = "Successfully uploaded file";
            response.Data = $"~/Images/{uniqueName}";
            return response;
        }
    }
}
