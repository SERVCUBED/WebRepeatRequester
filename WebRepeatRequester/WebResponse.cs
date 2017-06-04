using System;
using System.Collections.Generic;
using System.Net;

namespace WebRepeatRequester
{
    public class WebResponse
    {
        public string URL;
        public string InitialURL;
        public int StatusCode;
        public DateTime Date;
        public string ContentText;
        public byte[] Content;
        public WebHeaderCollection Headers;
        public bool IsFromCache;
        public string ContentType = "none";
        public List<MatchResponse> Matches = new List<MatchResponse>();
    }
}
