using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatskiPDFMVC
{
    public static class Constants
    {
#if DEBUG
        public static readonly string ApiUrl = "http://localhost:52597/";
#else
        public static readonly string ApiUrl = "http://titan:8018/";
#endif

    }
}
