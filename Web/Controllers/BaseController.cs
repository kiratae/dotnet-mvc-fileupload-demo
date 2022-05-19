using Microsoft.AspNetCore.Mvc;

namespace My.Demo.FileUpload.Web
{
    public class BaseController : Controller
    {
        protected IActionResult Error(bool isPartial = false)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new AjaxResultModel(AjaxResultModel.StatusCodeError, new List<string>() { "Server Error" }));
            if (isPartial)
                return PartialView("Error");
            return View("Error");
        }
    }
}
