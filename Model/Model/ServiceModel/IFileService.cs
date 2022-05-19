using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Demo.FileUpload.Model
{
    public interface IFileService
    {
        List<File> GetList(FileFilter filter, ResultPaging paging);
        File GetData(int fileId);
        File SaveData(File file);
        bool DeletData(int fileId);
    }
}
