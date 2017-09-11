using System;
using System.Windows.Forms;

namespace WebRepeatRequester
{
    public partial class Form1 : Form
    {
        RequestsManager _manager;
        System.Net.WebHeaderCollection _headers = new System.Net.WebHeaderCollection();
        MatchSettings _matchSettings = new MatchSettings();

        public Form1()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (_manager != null)
            {
                if (_manager.Running)
                {
                    _manager.Stop();
                    startBtn.Text = "Start";
                    return;
                }
            }

            try
            {
                listBox1.Items.Clear();
                _manager = new RequestsManager(txtURI.Text, txtPostData.Text, txtUserAgent.Text, txtReferrer.Text, 
                    (int)delayNumericUpDown.Value, (int)timeoutNumericUpDown.Value, _headers, _matchSettings, chkSSL.Checked);
                _manager.StoppedEventHandler += RequestManagerStoppedEventHandler;
                _manager.IsRequestingEventHandler += ManagerOnIsRequestingEventHandler;
                _lastIndex = 0;
                startBtn.Text = "Stop";
                _manager.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    throw;
#endif
            }
        }

        private void ManagerOnIsRequestingEventHandler(bool isRunning)
        {
            if (requestingLbl.InvokeRequired)
            {
                requestingLbl.Invoke((MethodInvoker)(() => ManagerOnIsRequestingEventHandler(isRunning)));
                return;
            }
            requestingLbl.Visible = isRunning;
        }

        private void RequestManagerStoppedEventHandler()
        {
            if (startBtn.InvokeRequired)
            {
                startBtn.Invoke((MethodInvoker)RequestManagerStoppedEventHandler);
                return;
            }
            startBtn.Text = "Start";
        }

        private int _lastIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_manager == null)
                return;
            for (int i = _lastIndex; i < _manager.Responses.Count; i++)
            {
                var r = _manager.Responses[i];

                var matchesSubtext = " [ ";
                foreach (MatchResponse mo in r.Matches)
                    matchesSubtext += mo.Result.Count.ToString() + ' ';
                matchesSubtext += ']';
 
                // index, (num. matches), http status code, content length, url
                listBox1.Items.Add(i + ", " + (_matchSettings.ShouldMatch? r.Matches.Count + matchesSubtext + ", " : "") + 
                    r.StatusCode + ", " + r.ContentText.Length + ", " + r.URL);
            }
            _lastIndex = _manager.Responses.Count;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_manager == null)
                return;

            if (listBox1.SelectedIndex < 0)
                return;

            int index;
            int.TryParse(listBox1.SelectedItem.ToString().Split(',')[0], out index);

            if (index > _manager.Responses.Count)
                return;

            var f = new ObjectView();
            
            f.View(_manager.Responses[index]);
        }

        private void onceBtn_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                _manager = new RequestsManager(txtURI.Text, txtPostData.Text, txtUserAgent.Text, txtReferrer.Text, 
                    (int)delayNumericUpDown.Value, (int)timeoutNumericUpDown.Value, _headers, _matchSettings, chkSSL.Checked);
                _manager.IsRequestingEventHandler += ManagerOnIsRequestingEventHandler;
                _lastIndex = 0;
                _manager.RunOnce();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    throw;
#endif
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new HeadersWindow();
            f.EditCollection(_headers);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _matchSettings = new MatchSettingsFrm().Show(_matchSettings);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSSL.Checked && MessageBox.Show("Enabling this will disable checks for invalid SSL. This can leak personal information. " + 
                "ONLY ENABLE THIS IF YOU KNOW WHAT YOU ARE DOING!\r\n\r\nYOU WILL BE RESPONSIBLE FOR ANY LOSS AS A RESULT OF ENABLING THIS " + 
                "FEATURE.\r\n\r\nYOU MUST RESTART THIS PROGRAM TO DISABLE THIS!\r\n\r\nPress cancel to undo.", "WARNING: ATTEMPTING TO DISABLE SSL VERIFICATION", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                chkSSL.Checked = false;

            if (chkSSL.Checked)
                chkSSL.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_manager == null)
                return;

            new ViewMatchesFrm().Show(_matchSettings, _manager.Responses.ToArray());
        }
    }
}
