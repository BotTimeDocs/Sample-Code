using System.Collections.Generic;

namespace RPA.Abstractions
{
    // 文件上传通道
    public class Channel
    {
        // 上传地址
        public string Uri { get; set; }

        // 上传请求的Headers
        public Dictionary<string, string> Headers { get; set; }
    }
}
