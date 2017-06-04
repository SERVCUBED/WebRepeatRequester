using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WebRepeatRequester
{
    public partial class ObjectView : Form
    {
        WebResponse _wr;

        public ObjectView()
        {
            InitializeComponent();
        }

        public void View(WebResponse wr)
        {
            _wr = wr;

            var headersText = "";
            for (int i = 0; i < wr.Headers?.Count; i++)
            {
                headersText += "\r\n    " + wr.Headers.Keys[i];

                var values = wr.Headers.GetValues(i);
                
                foreach (var item in values)
                {
                    headersText += "\r\n        " + item;
                }
            }

            var dateText = String.Format("\r\n    Initiated\r\n        {0}\r\n    Request Started\r\n        {1}\r\n    Finished\r\n        {2}\r\n    Request Duration\r\n        {3} ms",
                FormatDate(wr.Timings.Initiated), FormatDate(wr.Timings.RequestStarted), FormatDate(wr.Timings.Finished), wr.Timings.ReqDuration);

            var matchesText = "";
            if (wr.Matches.Count > 0)
            {
                matchesText += "\r\n\r\nMatches: ";
                foreach (MatchResponse match in wr.Matches)
                {
                    matchesText += "\r\n    " + match.Object.Type + ":" + match.Object.Parameters;
                    foreach (var res in match.Result)
                    {
                        matchesText += "\r\n        " + res;
                    }
                }
            }

            textBox1.Text = String.Format("Final URL: {0}\r\n\r\nInitial URL: {1}\r\n\r\nContent Type: {2}\r\n\r\nContent Length: {3}\r\n\r\nLoaded From Cache: {4}\r\n\r\nTimings: {5}\r\n\r\nHTTP Status Code: {6}\r\n\r\nHeaders: {7}{8}", 
                wr.URL, wr.InitialURL, wr.ContentType, wr.Content?.Length, wr.IsFromCache.ToString(), dateText, wr.StatusCode, headersText, matchesText);

            textBox2.Text = wr.ContentText;

            Show();
        }

        private string FormatDate(DateTime dt) => dt.ToShortDateString() + " " + dt.TimeOfDay;

        private void button2_Click(object sender, EventArgs e)
        {
            var t = Path.GetTempFileName();
            
            if (Path.HasExtension(_wr.InitialURL))
                t += Path.GetExtension(_wr.InitialURL);

            if (t.Contains('?'))
                t = t.Substring(0, t.IndexOf('?'));

            WriteToPath(t);

            if (MessageBox.Show("Are you sure you want to open the file?" + Environment.NewLine + t, "Are you sure?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            ShowOpenWithDialog(t);
            //new System.Diagnostics.Process()
            //{
            //    StartInfo = new System.Diagnostics.ProcessStartInfo(t)
            //}.Start();
        }

        private void WriteToPath(string path)
        {
            var content = _wr.Content;

            var sw = new FileStream(path, FileMode.Create, System.IO.FileAccess.Write);
            sw.Write(content, 0, content.Length);
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Uri(_wr.URL);
            var seg1 = u.Segments.Last();
            saveFileDialog1.FileName = seg1.Substring(1);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                WriteToPath(saveFileDialog1.FileName);
        }

        public static void ShowOpenWithDialog(string path)
        {
            var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
            args += ",OpenAs_RunDLL " + path;
            Process.Start("rundll32.exe", args);
        }
    }
}
