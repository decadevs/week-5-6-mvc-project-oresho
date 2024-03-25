namespace ResumePortal.Models.Dtos
{
    public class GenericResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
    
    public class GenericResponse<T> : GenericResponse
    {
        public T Data { get; set; }

    }
}
