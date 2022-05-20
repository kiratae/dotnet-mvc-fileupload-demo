namespace My.Demo.FileUpload.Web
{
    public class BannerModel : BaseModel
    {
        public int? BannerId { get; set; }
        public string Title { get; set; }
        public int FileId { get; set; }
    }
}
