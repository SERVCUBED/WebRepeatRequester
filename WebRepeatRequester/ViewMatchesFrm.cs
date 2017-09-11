using System;
using System.Linq;
using System.Windows.Forms;

namespace WebRepeatRequester
{
    public partial class ViewMatchesFrm : Form
    {
        MatchSettings _ms;
        WebResponse[] _responses;

        public ViewMatchesFrm()
        {
            InitializeComponent();
        }

        public void Show(MatchSettings ms, WebResponse[] responses)
        {
            _ms = ms;
            _responses = responses;

            foreach (MatchObject match in ms.MatchObjects)
            {
                listBox1.Items.Add(Enum.GetName(typeof(MatchObject.MatchType), match.Type) + (match.Parameters == ""? "" : ":" + match.Parameters));
            }

            Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = listBox1.SelectedItem?.ToString();
            if (selectedItem == string.Empty)
                return;

            textBox1.Text = String.Empty;
            var type = Enum.Parse(typeof(MatchObject.MatchType), 
                selectedItem.Contains(':') ? selectedItem.Substring(0, selectedItem.IndexOf(':')) : selectedItem);
            var parameters = selectedItem.Contains(':') ? selectedItem.Substring(selectedItem.IndexOf(':')) : "";

            for (int i = 0; i < _responses.Length; i++)
            {
                textBox1.Text += "item " + i + ": " + _responses[i].Matches.Count + " results\r\n";

                foreach (var matchResponse in _responses[i].Matches)
                {
                    foreach (var result in matchResponse.Result)
                        textBox1.Text += "    " + result + "\r\n";
                }

                textBox1.Text += "\r\n";
            }
        }
    }
}
