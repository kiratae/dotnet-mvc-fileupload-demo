using My.Demo.FileUpload.Model;

namespace My.Demo.FileUpload.Web
{
    public class BannerModel : BaseModel
    {
        public BannerModel(Banner banner)
        {
            BannerId = banner.BannerId;
            Title = banner.Title;
            FileId = banner.FileId;
        }

        public int? BannerId { get; set; }
        public string Title { get; set; }
        public int FileId { get; set; }

        public static List<BannerModel> CreateModels(List<Banner> banners)
        {
            List<BannerModel> list = new();
            foreach (Banner banner in banners)
                list.Add(new(banner));
            return list;
        }
    }
}
