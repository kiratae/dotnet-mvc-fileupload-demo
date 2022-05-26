
namespace My.Demo.FileUpload.Model
{
    public interface IBannerRepository
    {
        List<Banner> GetList(BannerFilter filter, ResultPaging paging);
        Banner GetData(int bannerId);
        Banner SaveData(Banner banner);
        bool DeletData(int bannerId);
    }
}
