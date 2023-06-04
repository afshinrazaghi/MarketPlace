﻿using MarketPlace.Application.Utils;
using MarketPlace.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.EntitiesExtensions
{
	public static class BannerExtension
	{
		public static string GetBannerMainImageAddress(this SiteBanner banner)
		{
			return PathExtension.BannerOrigin + banner.ImageName;
		}
	}
}
