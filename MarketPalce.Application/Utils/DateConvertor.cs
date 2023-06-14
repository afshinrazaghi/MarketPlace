﻿using System;
using System.Globalization;

namespace MarketPlace.Application.Utils
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(value) + "/" +
                   calendar.GetMonth(value).ToString("00") + "/" +
                   calendar.GetDayOfMonth(value).ToString("00");
        }
    }
}
