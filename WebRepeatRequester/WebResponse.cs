using System.Collections.Generic;
using System.Net;

namespace WebRepeatRequester
{
    public class WebResponse
    {
        public string URL;
        public string InitialURL;
        public int StatusCode;
        public DateTimePackage Timings = new DateTimePackage();
        public string ContentText;
        public byte[] Content;
        public WebHeaderCollection Headers;
        public bool IsFromCache;
        public string ContentType = "none";
        public List<MatchResponse> Matches = new List<MatchResponse>();
    }
}
