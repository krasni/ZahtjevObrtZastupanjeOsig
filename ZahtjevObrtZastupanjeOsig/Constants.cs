#define RELEASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZahtjevObrtZastupanjeOsig
{
    public static class Constants
    {
#if (DEBUG)
        public static readonly string ApiUrl = "http://localhost:52597/";
#elif (STAGE)
        public static readonly string ApiUrl = "http://localhost:2019/";
#elif (RELEASE)
        public static readonly string ApiUrl = "http://titan:8018/";
#endif
    }
}
