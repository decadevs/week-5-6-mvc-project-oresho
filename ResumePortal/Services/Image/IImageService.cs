using ResumePortal.Models.Dtos;

namespace ResumePortal.Services.Image
{
    public interface IImageService
    {
        GenericResponse<string> UploadImage(IFormFile photo);

        GenericResponse DeleteImage(string uniqueName);
    }
}
