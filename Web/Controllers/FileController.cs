using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My.Demo.FileUpload.Model;

namespace My.Demo.FileUpload.Web
{
    public class FileController : BaseController
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService _service;
        public static readonly string Name = "File";
        public static readonly string ActionDetail = "Detail";
        public static readonly string ActionUpload = "Upload";
        public const string StorageLocation = "C:\\my\\storage\\demo";

        public FileController(ILogger<FileController> logger, IFileService service)
        {
            _logger = logger;
            _service = service;
        }

        [AllowAnonymous]
        public IActionResult Detail(int? id)
        {
            const string func = "Detail";
            try
            {
                if (!id.HasValue)
                    return NotFound();
                Model.File file = _service.GetData(id.Value);
                if (file == null)
                    return NotFound();

                string fileName = Path.Combine(StorageLocation, file.FileGuid);
                if (!System.IO.File.Exists(fileName))
                    return NotFound("File not found.");
                return PhysicalFile(fileName, file.MimeType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught with id {1}.", func, id);
                return Error();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            const string func = "Upload";
            try
            {
                List<FileModel> fileUploads = new();
                if (files.Count == 0)
                    files = HttpContext.Request.Form.Files.ToList();

                foreach (IFormFile postedFile in files)
                {
                    string fileGuid = Guid.NewGuid().ToString();
                    string fileName = Path.Combine(StorageLocation, fileGuid);
                    using (FileStream output = System.IO.File.Create(fileName))
                        await postedFile.CopyToAsync(output);
                    FileModel fileModel = new()
                    {
                        FileGuid = fileGuid,
                        FileName = postedFile.FileName,
                        MimeType = postedFile.ContentType,
                        FileSize = (int)postedFile.Length
                    };

                    Model.File file = fileModel.ToDataModel();
                    file = _service.SaveData(file);
                    fileUploads.Add(new FileModel(file));
                }

                FileUploadResultModel model = new(fileUploads);
                return Json(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught.", func);
                return Error();
            }
        }
    }
}
