using System.Collections.Generic;

namespace ExamApp.DataProviders
{
    public class JsonConfig
    {
        public int DefaultServerPort { get; set; }
        public string DefaultServerDHCPFormat { get; set; }
        public int IPRangeForScanStart { get; set; }
        public int IPRangeForScanEnd { get; set; }
        public string HostName { get; set; }
        public string AddressFamily { get; set; }
        public string ProtocolType { get; set; }
        public string SocketType { get; set; }
        public int DataByteCount { get; set; }
        public string DefaultExamDocumentPath { get; set; }
        public Dictionary<string, string> ResponseCodes { get; set; }
        public Dictionary<string, string> RequestCodes { get; set; }
    }
}
