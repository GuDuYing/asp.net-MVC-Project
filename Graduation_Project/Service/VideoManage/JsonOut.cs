using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VideoManage
{
    class JsonOut
    {
        public string code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public JsonIn data;
    }
}
