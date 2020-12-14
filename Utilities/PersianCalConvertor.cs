using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyCms
{
    public static class PersianCalConvertor
    {
        public static string ToShamsi(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dt) + "/" + pc.GetMonth(dt).ToString("00") + "/" + pc.GetDayOfMonth(dt).ToString("00");
        }
    }
}