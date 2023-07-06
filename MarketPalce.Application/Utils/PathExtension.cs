using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Utils
{
    public static class PathExtension
    {

        #region default images
        public static string DefaultAvatar = "/img/defaults/avatar.png";
        #endregion

        #region user Avatar
        public static string UserAvatarOrigin = "/Content/Images/UserAvatar/origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/origin/");


        public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/thumb/");
        #endregion

        #region Upload
        public static string UploadImage = "/img/upload/";
        public static string UploadImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/upload/");
        #endregion

        #region product
        #region user Avatar
        public static string ProductImageOrigin = "/Content/Images/Product/origin/";
        public static string ProductImageOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/origin/");


        public static string ProductImageThumb = "/Content/Images/Product/Thumb/";
        public static string ProductImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/thumb/");
        #endregion
        #endregion

        #region slider
        public static string SliderOrigin = "/img/slider/";
        #endregion

        #region site banner
        public static string BannerOrigin = "/img/bg/";
        #endregion
    }
}
