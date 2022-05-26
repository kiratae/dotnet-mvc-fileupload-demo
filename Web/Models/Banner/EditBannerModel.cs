using My.Demo.FileUpload.Model;
using System.ComponentModel.DataAnnotations;

namespace My.Demo.FileUpload.Web
{
    public class EditBannerModel : BaseEditModel
    {
        public EditBannerModel()
        {

        }

        public EditBannerModel(Banner banner) : this()
        {
            BannerId = banner.BannerId;
            Title = banner.Title;
            FileId = banner.FileId;
        }

        public int? BannerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int? FileId { get; set; }

        public Banner ToDataModel(Banner banner = null)
        {
            if (banner == null)
                banner = new();
            banner.BannerId = BannerId;
            banner.Title = Title;
            banner.FileId = FileId.Value;
            return banner;
        }
    }
}
