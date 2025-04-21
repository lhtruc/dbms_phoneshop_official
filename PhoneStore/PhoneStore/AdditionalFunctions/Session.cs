using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.AdditionalFunctions
{
    public static class Session
    {
        public static string CurrentUser { get; set; }
        public static string CurrentRole { get; set; }

        public static void Clear()
        {
            CurrentUser = null;
            CurrentRole = null;
        }
    }
}
