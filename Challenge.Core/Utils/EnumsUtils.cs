using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Utils
{
    public static class EnumsUtils
    {
        public static string GetName(object enumValue)
        {
            return Enum.GetName(enumValue.GetType(), enumValue);    
        }
    }
}
