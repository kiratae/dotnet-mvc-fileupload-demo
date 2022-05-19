using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Demo.FileUpload.Model
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly IFileRepository _fileRepository;

        public FileService(ILogger<FileService> logger, IFileRepository fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        public List<File> GetList(FileFilter filter, ResultPaging paging)
        {
            const string func = "GetList";
            try
            {
                return _fileRepository.GetList(filter, paging);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught.", func);
                throw;
            }
        }

        public File GetData(int fileId)
        {
            const string func = "GetData";
            try
            {
                return _fileRepository.GetData(fileId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught with file id {1}.", func, fileId);
                throw;
            }
        }

        public File SaveData(File file)
        {
            const string func = "SaveData";
            try
            {
                return _fileRepository.SaveData(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught.", func);
                throw;
            }
        }

        public bool DeletData(int fileId)
        {
            const string func = "SaveData";
            try
            {
                return _fileRepository.DeletData(fileId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{0}: Exception caught with file id {1}.", func, fileId);
                throw;
            }
        }
    }
}
