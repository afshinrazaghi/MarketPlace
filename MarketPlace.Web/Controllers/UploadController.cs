using Microsoft.AspNetCore.Mvc;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Newtonsoft.Json;

namespace MarketPlace.Web.Controllers
{
    public class UploadController : SiteBaseController
    {
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload, string? CKEditorFuncName, string? CKEditor, string? langCode)
        {
            var noImageMessage = "لطفا یک تصویر انتخاب کنید";

            if (upload.Length < 0) return Json(new { uploaded = false, error = new { message = noImageMessage } });
            if (!upload.IsImage())
            {
                return Json(new { uploaded = false, error = new { message = noImageMessage } });
            }

            var fileName = Path.GetFileNameWithoutExtension(upload.FileName) + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(upload.FileName);
            upload.AddImageToServer(fileName, PathExtension.UploadImageServer, null, null);
            return Json(new
            {
                uploaded = true,
                url = $"{PathExtension.UploadImage}{fileName}"
            });
        }
    }
}
