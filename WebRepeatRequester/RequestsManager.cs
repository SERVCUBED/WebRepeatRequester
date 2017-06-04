using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace WebRepeatRequester
{
    class RequestsManager
    {
        private readonly string _URL;
        private readonly string _postData;
        private readonly StringIterator _uAgent;
        private readonly string _referrer;
        private readonly int _delay;
        private readonly int _timeout;
        private readonly MatchSettings _matchSettings;
        public readonly WebHeaderCollection _headers;

        public delegate void StoppedEvent();
        public event StoppedEvent StoppedEventHandler;

        public bool Running { get; private set; }

        public readonly List<WebResponse> Responses = new List<WebResponse>();

        public RequestsManager(string url, string postData, string uAgent, string referrer, int delay, int timeout, WebHeaderCollection headers, MatchSettings ms, bool bypassSSLChecks)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException("Invalid URI. Please enter a well formatted URI");
            _URL = url;
            _postData = postData;
            _uAgent = new StringIterator(uAgent, '#');
            _referrer = referrer;
            _delay = delay;
            _timeout = timeout;
            _headers = headers;
            _matchSettings = ms;

            if (bypassSSLChecks)
                InitiateSSLTrust();
        }

        public void Start()
        {
            Running = true;
            Task.Run(() => DoRun());
        }

        public void Stop()
        {
            if (Running)
                StoppedEventHandler?.Invoke();
            Running = false;
        }

        private void DoRun()
        {
            while (Running)
            {
                SendWebRequest();
                System.Threading.Thread.Sleep(_delay);
            }
        }

        public void RunOnce()
        {
            Task.Run(() => SendWebRequest());
        }
        
        private HttpWebRequest SetupWebClient()
        {
            HttpWebRequest _wc;
            try
            {
                var r = WebRequest.Create(new Uri(_URL));
                _wc = r as HttpWebRequest;
            }
            catch (Exception)
            {
#if DEBUG
                if (Debugger.IsAttached)
                    throw;
#endif
                return null;
            }
            _wc.Timeout = _timeout;
            _wc.KeepAlive = false;
            _wc.Referer = _referrer;
            try
            {
                var accept = _headers.GetValues("Accept");
                if (accept != null)
                {
                    _wc.Accept = accept[0];
                    _headers.Remove("Accept");
                }

                var host = _headers.GetValues("Host");
                if (host != null)
                {
                    _wc.Host = host[0];
                    _headers.Remove("Host");
                }

                _wc.Headers = _headers;
            }
            catch (Exception ex)
            {
                if (System.Windows.Forms.MessageBox.Show("Error when setting headers:\r\n" + ex.Message + "\r\n\r\nContinue?", "Continue?", System.Windows.Forms.MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                {
                    Stop();
                    return null;
                }
            }

            return _wc;
        }
                
        /// <summary>
        /// Send a WebRequest (with a timeout of 1000ms). Returns an empty string on timeout.
        /// </summary>
        /// <param name="url">The requested resource</param>
        /// <param name="quick">True to set a timeout of 1000ms</param>
        /// <param name="postData">The data to include in the POST request</param>
        /// <param name="userAgent">The useragent object of the request.</param>
        /// <param name="referrer">The referrer object of the request.</param>
        /// <returns>The requested resource</returns>
        private void SendWebRequest()
        {
            var wrObject = new WebResponse();

            var _wc = SetupWebClient();

            // If still null, must have errored somewhere
            if (_wc == null)
                return;

            byte[] buf = new byte[8192];
            StringBuilder sb = new StringBuilder();
            try
            {
                _wc.UserAgent = _uAgent.Next();

                if (!String.IsNullOrEmpty(_postData))
                {
                    _wc.Method = "POST";
                    _wc.ContentType = "application/x-www-form-urlencoded";

                    ASCIIEncoding _encoding = new ASCIIEncoding();

                    byte[] data = _encoding.GetBytes(_postData);
                    _wc.ContentLength = data.Length;

                    var stream = _wc.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }

                wrObject.Date = DateTime.Now;
                var wr = _wc.GetResponse();
                wrObject.InitialURL = _URL;
                wrObject.URL = wr.ResponseUri.ToString();
                if (wr.SupportsHeaders)
                    wrObject.Headers = wr.Headers;
                wrObject.IsFromCache = wr.IsFromCache;
                wrObject.ContentType = wr.ContentType;
                wrObject.StatusCode = (int)((HttpWebResponse)wr).StatusCode;

                var encoding = getEncoding(wr.ContentType);
                Stream resStream = wr.GetResponseStream();

                List<byte> buffer2 = new List<byte>();

                int? count;
                do
                {
                    count = resStream?.Read(buf, 0, buf.Length);
                    if (count == null)
                        break;
                    if (count != 0)
                    {
                        buffer2.AddRange(buf.Take((int)count));
                        sb.Append(encoding.GetString(buf, 0, (int)count));
                    }
                } while (count > 0);

                wrObject.Content = buffer2.ToArray<byte>();

                wr.Close();
                resStream?.Close();
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    sb.Append(ex.Message);
                    var m = System.Text.RegularExpressions.Regex.Match(ex.Message, @"\(([0-9]+)\)");
                    if (m.Success)
                        int.TryParse(m.Value.Substring(1, m.Value.Length - 2), out wrObject.StatusCode);

                    if (wrObject.StatusCode == 200)
                        wrObject.StatusCode = 0;

                    wrObject.ContentType = "text/plain";
                }
#if DEBUG
                if (!(ex is WebException) && Debugger.IsAttached)
                    throw;
#endif
            }

            wrObject.ContentText = sb.ToString();

            // Handle matches
            if (_matchSettings.ShouldMatch)
            {
                var prevLength = Responses.Count > 0 ? Responses[Responses.Count - 1].Content.Length : wrObject.Content.Length;
                foreach (MatchObject mo in _matchSettings.MatchObjects)
                {
                    var matches = mo.Matches(wrObject, prevLength);

                    if (matches.Count == 0)
                        continue;

                    wrObject.Matches.Add(new MatchResponse() { Object = mo, Result = matches });

                    if (_matchSettings.StopOnMatch)
                    {
                        if (_matchSettings.StopOnMatchOperator == MatchSettings.Operator.AND)
                        {
                            if (wrObject.Matches.Count == _matchSettings.MatchObjects.Count)
                                Stop();
                        }
                        else
                            Stop();
                    }
                }
            }
            
            Responses.Add(wrObject);
        }

        private void InitiateSSLTrust()
        {
            //Change SSL checks so that all checks pass
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(
                    delegate
                    { return true; }
                );
        }

        private Encoding getEncoding(string contentType)
        {
            var _cType = contentType.ToUpper().Replace("-", "");
            if (_cType.Contains("UTF8"))
                return Encoding.UTF8;
            if (_cType.Contains("UTF16"))
                return Encoding.UTF8;
            if (_cType.Contains("UNICODE"))
                return Encoding.Unicode;
            return Encoding.ASCII;
        }
    }
}
