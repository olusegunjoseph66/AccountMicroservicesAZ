namespace Account.Application.ViewModels.Responses
{
    public class FileResponse
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
