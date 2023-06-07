﻿using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Utils
{
    public class ImageOptimizer
    {
        public void ImageResizer(string inputImagePath, string outputImagePaht, int? width, int? height)
        {
            var customWidth = width ?? 100;
            var customHeight = height ?? 100;

            using (var image = SixLabors.ImageSharp.Image.Load(inputImagePath))
            {
                image.Mutate(x => x.Resize(customWidth, customHeight));
                image.Save(outputImagePaht, new JpegEncoder
                {
                    Quality = 100
                });
            }
        }
    }
}
