using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WebRepeatRequester
{
    public partial class MatchSettingsFrm : Form
    {
        public MatchSettingsFrm()
        {
            InitializeComponent();

            // Add match settings operators to combo
            stopReqOnMatchSettingsCombo.Items.AddRange(Enum.GetNames(typeof(MatchSettings.Operator)));
        }

        public MatchSettings Show(MatchSettings ms)
        {
            // Setup with old values
            chkShouldMatch.Checked = ms.ShouldMatch;
            chkStopReqOnMatch.Checked = ms.StopOnMatch;
            stopReqOnMatchSettingsCombo.SelectedItem = Enum.GetName(typeof(MatchSettings.Operator), ms.StopOnMatchOperator);
            foreach (var mo in ms.MatchObjects)
            {
                lstMatchObjects.Items.Add(Enum.GetName(typeof(MatchObject.MatchType), mo.Type) + ':' + mo.Parameters);
            }

            ShowDialog();

            // Get new values
            var newms = new MatchSettings();
            newms.ShouldMatch = chkShouldMatch.Checked;
            newms.StopOnMatch = chkStopReqOnMatch.Checked;
            newms.StopOnMatchOperator = (MatchSettings.Operator)Enum.Parse(typeof(MatchSettings.Operator), stopReqOnMatchSettingsCombo.SelectedItem.ToString());
            foreach (var moItem in lstMatchObjects.Items)
            {
                var s = moItem.ToString();
                newms.MatchObjects.Add(new MatchObject()
                {
                    Type = (MatchObject.MatchType)Enum.Parse(typeof(MatchObject.MatchType), s.Substring(0, 6)),
                    Parameters = s.Length > 6 ? s.Substring(7, s.Length - 7) : String.Empty
                });
            }

            return newms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = Interaction.InputBox("Add match object ([SRegex/BArray/LenDev]:params)");
            if (String.IsNullOrWhiteSpace(input))
                return;

            try
            {
                lstMatchObjects.Items.Add(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = lstMatchObjects.SelectedItem;
            if (item == null)
                return;
            
            lstMatchObjects.Items.Remove(item);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var item = lstMatchObjects.SelectedItem;
            if (item == null)
                return;

            var input = Interaction.InputBox("Edit match object ([SRegex/BArray/LenDev]:params)", "Edit match object", item.ToString());
            if (String.IsNullOrWhiteSpace(input))
                return;

            lstMatchObjects.Items[lstMatchObjects.SelectedIndex] = input;
        }
    }
}
