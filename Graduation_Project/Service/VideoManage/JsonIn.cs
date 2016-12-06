using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VideoManage
{
    class JsonIn
    {
        public string video_id { get; set; }
        public string video_unique { get; set; }
        public string upload_url { get; set; }
        public string progress_url { get; set; }
        public string token { get; set; }
        public string uploadtype { get; set; }
        public string isdrm { get; set; }
    }
}
