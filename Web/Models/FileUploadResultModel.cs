namespace My.Demo.FileUpload.Web
{
    public class FileUploadResultModel
    {
        public FileUploadResultModel()
        {
            Files = new List<FileModel>();
        }

        public FileUploadResultModel(List<FileModel> fileUploads) : this()
        {
            Files.AddRange(fileUploads);
        }

        public List<FileModel> Files { get; protected set; }
    }
}
