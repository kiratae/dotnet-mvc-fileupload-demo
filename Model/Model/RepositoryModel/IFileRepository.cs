
namespace My.Demo.FileUpload.Model
{
    public interface IFileRepository
    {
        List<File> GetList(FileFilter filter, ResultPaging paging);
        File GetData(int fileId);
        File SaveData(File file);
        bool DeletData(int fileId);
    }
}
