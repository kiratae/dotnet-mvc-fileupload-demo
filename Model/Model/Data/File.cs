
namespace My.Demo.FileUpload.Model
{
    public class File
    {
        public int? FileId { get; set; }
        public string FileGuid { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public bool IsTemp { get; set; }
    }
}
