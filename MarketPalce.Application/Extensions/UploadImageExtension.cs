using LazZiya.ImageResize;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Extensions
{
    public static class UploadImageExtension
    {
        public static void AddImageToServer(this IFormFile image, string fileName, string originalPath, int? width, int? height, string? thumbPath = null, string? deleteFileName = null)
        {
            if (image is not null && image.IsImage())
            {
                if (!Directory.Exists(originalPath))
                    Directory.CreateDirectory(originalPath);
                string originPath = originalPath + fileName;

                if (!string.IsNullOrEmpty(deleteFileName))
                    (deleteFileName).DeleteImage(originalPath, thumbPath);


                using (var stream = new FileStream(originPath, FileMode.Create))
                {
                    if (!Directory.Exists(originPath))
                    {
                        image.CopyTo(stream);
                    }
                }

                if (!string.IsNullOrWhiteSpace(thumbPath))
                {
                    if (!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);

                    ImageOptimizer resizer = new ImageOptimizer();

                    if (width is not null && height is not null)
                    {
                        resizer.ImageResizer(originalPath + fileName, thumbPath + fileName, width, height);
                    }
                }
            }
        }

        public static void DeleteImage(this string imageName, string originPath, string? thumbPath)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                if (File.Exists(originPath + imageName))
                    File.Delete(originPath + imageName);

                if (!string.IsNullOrEmpty(thumbPath))
                    if (File.Exists(thumbPath + imageName))
                        File.Delete(thumbPath + imageName);
            }
        }
    }
}
