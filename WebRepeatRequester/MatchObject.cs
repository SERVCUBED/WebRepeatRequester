using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebRepeatRequester
{
    public class MatchObject
    {
        public enum MatchType
        {
            SRegex,
            BArray,
            LenDev,
            URLReD
        }

        public MatchType Type;

        public string Parameters;

        public List<string> Matches(WebResponse wr, int prevLength)
        {
            if (Type == MatchType.BArray)
                return ByteArraySearch(wr.Content);
            else if (Type == MatchType.SRegex)
                return RegexSearch(wr.ContentText);
            else if (Type == MatchType.LenDev)
                return LengthDeviation(wr.Content, prevLength);
            else if (Type == MatchType.URLReD)
                return RedirectTest(wr);

            return new List<string>();
        }

        private List<string> RedirectTest(WebResponse wr)
        {
            var result = new List<string>();

            if (wr.InitialURL != wr.URL)
                result.Add("Redirected");

            return result;
        }

        private List<string> ByteArraySearch(byte[] bytes)
        {
            var result = new List<string>();

            // Get byte[] to search for
            var param = Parameters.Split(',');
            var subject = new byte[param.Length];
            for (int i = 0; i < param.Length; i++)
            {
                var _int = Convert.ToInt16(param[i]);
                if (_int > 255 || _int < 0)
                    subject[i] = 0;
                else
                    subject[i] = Convert.ToByte(_int);
            }

            var complete = false;
            for (int i = 0; i < bytes.Length; i++)
            {
                // Not enough bytes left to fill subject. Break
                if (subject.Length > bytes.Length - i)
                    break;

                // Compare subject against byte
                for (int j = 0; j < subject.Length; j++)
                {
                    if (bytes[i + j] != subject[j])
                        break;
                    if (j == subject.Length - 1)
                        complete = true;
                }

                if (complete)
                {
                    result.Add(i.ToString());
                    complete = false;
                }
            }

            return result;
        }

        private List<string> RegexSearch(string text)
        {
            var result = new List<string>();

            var mc = Regex.Matches(text, Parameters);

            foreach (Match match in mc)
            {
                if (match.Success)
                    result.Add(match.Value);
            }

            return result;
        }


        private List<string> LengthDeviation(byte[] bytes, int prevLength)
        {
            var result = new List<string>();

            var deviationParam = Convert.ToInt32(Parameters);

            if (deviationParam == 0)
                return result;

            var deviation = Math.Abs(bytes.Length - prevLength);

            if (deviation >= deviationParam)
                result.Add("Length deviation of: " + deviation);

            return result;
        }
    }
}
