using Microsoft.AspNetCore.Mvc;

namespace My.Demo.FileUpload.Web
{
    public class BannerController : BaseController
    {
        private readonly ILogger<BannerController> _logger;
        public const string Name = "Banner";
        public const string ActionList = "List";

        public BannerController(ILogger<BannerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Redirect(Url.Action(ActionList, Name));
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
