namespace My.Demo.FileUpload.Web
{
    public class BaseEditModel : BaseModel
    {
        public string ReturnUrl { get; set; }
        public bool IsView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
