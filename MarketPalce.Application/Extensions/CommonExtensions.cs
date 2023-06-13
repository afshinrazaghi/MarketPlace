using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Extensions
{
    public static class CommonExtensions
    {
        public static string GetEnuName(this Enum myEnum)
        {
            var myEnumMemberInfo = myEnum.GetType().GetMember(myEnum.ToString()).FirstOrDefault();
            if (myEnumMemberInfo != null)
            {
                return myEnumMemberInfo.GetCustomAttribute<DisplayAttribute>()!.Name!;
            }

            return "";
        }
    }
}
