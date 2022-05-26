using My.Demo.FileUpload.Model;

namespace My.Demo.FileUpload.Web
{
    public class ListBannerModel
    {
        public ListBannerModel()
        {
            Banners = new List<BannerModel>();
        }

        public string Keyword { get; set; }
        public List<BannerModel> Banners { get; protected set; }

        public BannerFilter ToBannerFilter()
        {
            return new BannerFilter()
            {
                Keyword = $"%{Keyword}%"
            };
        }
    }
}
