using System;
using System.Net.Mail;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SettingsEmailTestClick(object sender, EventArgs e)
        {
            //NOTE: Test Email settings here - send a test email
            MailMessage message = new MailMessage(settingsAlertEmailFromTextBox.Text, settingsAlertEmailToTextBox.Text,
                                                  "Test email from RemoteMon:  It worked.", "This is only a test.");
            Object port = settingsMailServerPortMaskedTextBox.ValidateText();
            
            SmtpClient client = new SmtpClient(settingsMailServerAddressTextBox.Text)
                                    {
                                        EnableSsl = settingsMailServerSslCheckBox.Checked,
                                        UseDefaultCredentials = false,
                                        DeliveryMethod = SmtpDeliveryMethod.Network
                                    };

            if (settingsAlertEmailAccountTextBox.Text == "" && settingsAlertEmailPasswordTextBox.Text == "")
                client.UseDefaultCredentials = true;
            else
                client.Credentials = new System.Net.NetworkCredential(settingsAlertEmailAccountTextBox.Text, settingsAlertEmailPasswordTextBox.Text);
            
            if (port != null)
            {
                client.Port = Convert.ToInt32(port);
            }

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send test mail message.", "Failure", MessageBoxButtons.OK);
                Logger.Instance.LogException(this.GetType(), ex);
                message.Dispose();
                return;
            }
            //}
            //else
            //{
            //    MessageBox.Show("Port is invalid.  Please enter a proper port value.", "Unable to validate Port value.", MessageBoxButtons.OK);
            //}
            message.Dispose();
            MessageBox.Show("Message successfully sent", "Success", MessageBoxButtons.OK);
        }

        private void SettingsSmsTestClick(object sender, EventArgs e)
        {
            //NOTE: Test Sms settings here - send a test SMS
        }

        private void SettingsOkButtonClick(object sender, EventArgs e)
        {
            if (settingsMailServerPortMaskedTextBox.Text != "")
            {
                Object port = settingsMailServerPortMaskedTextBox.ValidateText();
                if (port == null)
                {
                    //DialogResult = DialogResult.None;
                    //MessageBox.Show("Port is invalid.  Please enter a proper port value.", "Unable to validate Port value.", MessageBoxButtons.OK);
                    settingsMailServerPortMaskedTextBox.Text = "-1";
                }
            }
        }
    }
}
