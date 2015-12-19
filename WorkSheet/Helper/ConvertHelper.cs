using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkSheet.Helper
{
    public static class ConvertHelper
    {
        public static int? ParseNullableInt(dynamic value)
        {
            
            int intValue;
            if (int.TryParse(value, out intValue))
                return intValue;
            return null;
        }
    }
}