using System;
using System.Windows.Forms;
using System.Net;
using Microsoft.VisualBasic;

namespace WebRepeatRequester
{
    public partial class HeadersWindow : Form
    {
        public HeadersWindow()
        {
            InitializeComponent();
        }

        private WebHeaderCollection _headers;

        public void EditCollection(WebHeaderCollection headers)
        {
            _headers = headers;
            foreach (var item in headers.Keys)
            {
                listBox1.Items.Add(item);
            }
            ShowDialog();

            headers = _headers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = Interaction.InputBox("Add header key/value pair (separated by ':')");
            if (String.IsNullOrWhiteSpace(input))
                return;

            try
            {
                _headers.Add(input);
                listBox1.Items.Add(input.Substring(0, input.IndexOf(':')));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem;
            if (item == null)
                return;

            _headers.Remove(item.ToString());
            listBox1.Items.Remove(item);
            listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var key = listBox1.SelectedItem;
            if (key == null)
                return;

            var input = Interaction.InputBox("Add value for " + key);
            if (String.IsNullOrWhiteSpace(input))
                return;

            _headers.Add(key.ToString(), input);
            listBox2.Items.Add(input);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var key = listBox1.SelectedItem;
            if (key == null)
                return;

            listBox2.Items.Clear();
            foreach (var item in _headers.GetValues(key.ToString()))
            {
                listBox2.Items.Add(item);
            }
        }
    }
}
