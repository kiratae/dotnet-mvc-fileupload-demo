using File = My.Demo.FileUpload.Model.File;

namespace My.Demo.FileUpload.Web
{
    public class FileModel : BaseModel
    {
        public FileModel()
        {

        }

        public FileModel(File file) : this()
        {
            FileId = file.FileId;
            FileGuid = file.FileGuid;
            FileName = file.FileName;
            MimeType = file.MimeType;
            FileSize = file.FileSize;
            Description = file.Description;
            CreateDate = file.CreateDate;
            CreateUserId = file.CreateUserId;
        }

        public static List<FileModel> CreateModels(List<File> files)
        {
            List<FileModel> list = new();
            foreach (File file in files)
                list.Add(new FileModel(file));
            return list;
        }

        public int? FileId { get; set; }
        public string FileGuid { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateText { get { return CreateDate.ToString("d"); } }
        public int? CreateUserId { get; set; }

        public File ToDataModel(File file = null)
        {
            if (file == null)
                file = new File();

            file.FileId = FileId;
            file.FileGuid = FileGuid;
            file.FileName = FileName;
            file.MimeType = MimeType;
            file.FileSize = FileSize;
            file.Description = Description;
            file.CreateUserId = CreateUserId;
            file.IsTemp = true;

            return file;
        }
    }
}
