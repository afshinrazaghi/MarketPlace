using Microsoft.AspNetCore.Mvc;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Newtonsoft.Json;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost("uploadImage")]
        public IActionResult IActionResult(IFormFile upload, string CKEditorFuncName, string CKEditor, string langCode)
        {
            if (upload.Length < 0) return null;
            if (!upload.IsImage())
            {
                var noImageMessage = "لطفا یک تصویر انتخاب کنید";
                var noImage = JsonConvert.DeserializeObject("{'uploaded':0, 'error':{message: \" " + noImageMessage + " \"}");
                return Json(noImage);
            }

            var fileName = Path.GetFileNameWithoutExtension(upload.FileName) + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(upload.FileName);
            upload.AddImageToServer(fileName, PathExtension.UploadImageServer,null,null);
            return Json(new
            {
                uploaded = true,
                url = $"{PathExtension.UploadImage}{fileName}"
            });
        }
    }
}
