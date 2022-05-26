using Microsoft.AspNetCore.Mvc;
using My.Demo.FileUpload.Model;

namespace My.Demo.FileUpload.Web
{
    public class BannerController : BaseController
    {
        private readonly ILogger<BannerController> _logger;
        private readonly IBannerService _service;
        public const string Name = "Banner";
        public const string ActionList = "List";
        public const string ActionEdit = "Edit";

        public BannerController(ILogger<BannerController> logger, IBannerService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return Redirect(Url.Action(ActionList, Name));
        }

        public IActionResult List(ListBannerModel model, int pageNo = 1)
        {
            const string func = "List";
            try
            {
                BannerFilter filter = model.ToBannerFilter();
                List<Banner> banners = _service.GetList(filter, null);
                model.Banners.AddRange(BannerModel.CreateModels(banners));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                return Error();
            }
            
        }

        public IActionResult Edit(int? id)
        {
            const string func = "Edit";
            try
            {
                EditBannerModel model;
                if (id.HasValue)
                {
                    model = new(_service.GetData(id.Value));
                }
                else
                {
                    model = new();
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught with id {id}.", func, id);
                return Error();
            }
        }

        [HttpPost]
        public IActionResult Edit(EditBannerModel model)
        {
            const string func = "Edit";
            try
            {
                if (ModelState.IsValid)
                {
                    Banner banner = model.BannerId.HasValue ? _service.GetData(model.BannerId.Value) : null;
                    banner = model.ToDataModel(banner);
                    _service.SaveData(banner);
                    return Json(new AjaxResultModel(AjaxResultModel.StatusCodeSuccess));
                }

                return Json(new AjaxResultModel(AjaxResultModel.StatusCodeError, ModelState));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{func}: Exception caught.", func);
                return Error();
            }
        }
    }
}
