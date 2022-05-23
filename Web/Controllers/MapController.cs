using Microsoft.AspNetCore.Mvc;

namespace My.Demo.FileUpload.Web
{
    public class MapController : BaseController
    {
        private readonly ILogger<MapController> _logger;
        public static readonly string Name = "Map";
        public static readonly string ActionIndex = "Index";

        public MapController(ILogger<MapController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
