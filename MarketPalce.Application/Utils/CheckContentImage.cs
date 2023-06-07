using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarketPlace.Application.Utils
{
    public static class CheckContentImage
    {
        private const int ImageMinimumBytes = 512;
        public static bool IsImage(this IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            List<string> mimeTyeps = new List<string> {
                "image/jpg",
                "image/jpeg",
                "image/pjpeg",
                "image/x-png",
                "image/png",
            };
            if (!mimeTyeps.Contains(postedFile.ContentType.ToLower()))
                return false;



            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            List<string> extensions = new List<string>
            {
                ".jpg",
                ".png",
                ".jpeg"
            };

            if (!extensions.Contains(Path.GetExtension(postedFile.FileName.ToLower())))
                return false;

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                    return false;

                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 

                if (postedFile.Length < ImageMinimumBytes)
                    return false;

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content= System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using(var bitmap = new System.Drawing.Bitmap(postedFile.OpenReadStream())) { }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.OpenReadStream().Position = 0;
            }
            return true;
        }
    }
}
