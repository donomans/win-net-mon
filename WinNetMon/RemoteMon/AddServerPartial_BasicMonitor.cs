using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class AddServer
    {
        //private static Boolean TestMonitorRunBasic = false;
        //private Thread _basicMonitorPopulation;
        private BasicMonitorType _basicType = BasicMonitorType.None;

        //private void GetBasics()
        //{
        //}
        public BasicMonitorType BasicType { get { return _basicType; } }

        public BasicMonitor GetBasicMonitor()
        {
            BasicMonitor basicMonitor = new BasicMonitor
                                            {
                                                FriendlyName = FriendlyName,
                                                UpdateFrequency =
                                                    Convert.ToInt32(
                                                        basicMonitorTestDataUpdateFreqTextBox.Text),
                                                BasicMonitorType = BasicType
                                            };
            switch (BasicType)
            {
                case BasicMonitorType.Ping:
                    basicMonitor.Server = basicMonitorIpTextBox.Text;
                    basicMonitor.Data.UrlUncIp = basicMonitorIpTextBox.Text;
                    break;
                case BasicMonitorType.Http:
                    basicMonitor.Server = basicMonitorHttpPathTextBox.Text;
                    basicMonitor.Data.UrlUncIp = basicMonitorHttpPathTextBox.Text;
                    basicMonitor.Data.Port = (Int32)basicMonitorHttpPathPortTextBox.ValidateText();
                    break;
                case BasicMonitorType.Ftp:
                    basicMonitor.Server = basicMonitorFtpPathTextBox.Text;
                    basicMonitor.Data.UrlUncIp = basicMonitorFtpPathTextBox.Text;
                    basicMonitor.Data.Port = (Int32)basicMonitorFtpPathPortTextBox.ValidateText();
                    basicMonitor.Credential.Username = basicMonitorFtpUserTextBox.Text;
                    basicMonitor.Credential.Password = basicMonitorFtpPassTextBox.Text;
                    basicMonitor.Credential.UseSecure = basicMonitorFtpUseSslCheckBox.Checked;
                    break;
                case BasicMonitorType.None:
                default:
                    break;
            }
            return basicMonitor;
        }

        public void BasicRepopulate(BasicMonitor basicMonitor)
        {
            switch (basicMonitor.BasicMonitorType)
            {
                case BasicMonitorType.Ftp:
                    basicMonitorFtpPathTextBox.Text = basicMonitor.Data.UrlUncIp;
                    basicMonitorFtpPathPortTextBox.Text = basicMonitor.Data.Port.ToString();
                    basicMonitorFtpUseSslCheckBox.Checked = basicMonitor.Credential.UseSecure;
                    basicMonitorFtpUserTextBox.Text = basicMonitor.Credential.Username;
                    basicMonitorFtpPassTextBox.Text = basicMonitor.Credential.Password;
                    basicMonitorFtpGroupBox.BackColor = Color.LightGray;
                    basicMonitorFtpMonitorSelect.Checked = true;
                    break;
                case BasicMonitorType.Http:
                    basicMonitorHttpPathTextBox.Text = basicMonitor.Data.UrlUncIp;
                    basicMonitorHttpPathPortTextBox.Text = basicMonitor.Data.Port.ToString();
                    basicMonitorHttpGroupBox.BackColor = Color.LightGray;
                    basicMonitorHttpMonitorSelect.Checked = true;
                    break;
                case BasicMonitorType.Ping:
                    basicMonitorIpTextBox.Text = basicMonitor.Data.UrlUncIp;
                    basicMonitorPingGroupBox.BackColor = Color.LightGray;
                    basicMonitorPingMonitorSelect.Checked = true;
                    break;
            }
            _basicType = basicMonitor.BasicMonitorType;
            basicMonitorTestDataUpdateFreqTextBox.Text = basicMonitor.UpdateFrequency.ToString();
        }

        #region Button checks

        private void BasicMonitorHttpPathCheckBtnClick(object sender, EventArgs e)
        {
            HttpWebResponse response = null;
            try
            {
                Uri t;
                if (basicMonitorHttpPathTextBox.Text.ToLower().StartsWith(Uri.UriSchemeHttps) || basicMonitorHttpPathPortTextBox.Text.Trim() == "443")
                {
                    String baseurl = basicMonitorHttpPathTextBox.Text;

                    if (baseurl.Contains(Uri.SchemeDelimiter))
                    {
                        baseurl = baseurl.Replace(Uri.SchemeDelimiter, "");

                        if (baseurl.StartsWith(Uri.UriSchemeHttps))
                            baseurl = baseurl.Replace(Uri.UriSchemeHttps, "");
                        else if (baseurl.StartsWith(Uri.UriSchemeHttp))
                            baseurl = baseurl.Replace(Uri.UriSchemeHttp, "");
                    }
                    t =
                        new Uri(Uri.UriSchemeHttps + Uri.SchemeDelimiter +
                                (baseurl.EndsWith("/") ? baseurl.TrimEnd(new[] {'/'}) : baseurl) +
                                (basicMonitorHttpPathPortTextBox.Text.Trim() != ":443"
                                     ? ":" + basicMonitorHttpPathPortTextBox.Text.Trim()
                                     : ""));
                }
                else
                {
                    String baseurl = basicMonitorHttpPathTextBox.Text;
                    if (baseurl.Contains(Uri.SchemeDelimiter))
                    {
                        baseurl = baseurl.Replace(Uri.SchemeDelimiter, "");

                        if (baseurl.StartsWith(Uri.UriSchemeHttps))
                            baseurl = baseurl.Replace(Uri.UriSchemeHttps, "");
                        else if (baseurl.StartsWith(Uri.UriSchemeHttp))
                            baseurl = baseurl.Replace(Uri.UriSchemeHttp, "");
                    }
                    t =
                        new Uri(Uri.UriSchemeHttp + Uri.SchemeDelimiter +
                                (baseurl.EndsWith("/") ? baseurl.TrimEnd(new[] {'/'}) : baseurl) +
                                (basicMonitorHttpPathPortTextBox.Text.Trim() != "80"
                                     ? ":" + basicMonitorHttpPathPortTextBox.Text.Trim()
                                     : ""));
                }

                basicMonitorHttpPathTextBox.Text = t.ToString();

                HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(t);
                wr.AllowAutoRedirect = true;
                wr.UserAgent =
                    "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MDDC)";
                
                response = (HttpWebResponse) wr.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    basicMonitorHttpPathCheckResultText.ForeColor = Color.Green;
                    basicMonitorHttpPathCheckResultText.Text = "OK";
                }
                else
                {
                    basicMonitorHttpPathCheckResultText.ForeColor = Color.Black;
                    basicMonitorHttpPathCheckResultText.Text = response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                if(NetworkMonitor.VerboseLogging)
                    Logger.Instance.LogException(this.GetType(), ex);
                basicMonitorHttpPathCheckResultText.ForeColor = Color.Black;
                basicMonitorHttpPathCheckResultText.Text = "Failure";
            }
            finally
            {
                if(response != null)
                    response.Close();
            }
        }

        private void BasicMonitorIpCheckBtnClick(object sender, EventArgs e)
        {
            addServerValidIpIpTextBox.Text = addServerValidIpIpTextBox2.Text = basicMonitorIpTextBox.Text;
            SetIpAfterPing(true);
        }

        private void BasicMonitorFtpPathCheckBtnClick(object sender, EventArgs e)
        {
            FtpWebResponse response = null;
            try
            {
                String baseftpurl = basicMonitorFtpPathTextBox.Text;
                if (baseftpurl.Contains(Uri.SchemeDelimiter))
                {
                    baseftpurl = baseftpurl.Replace(Uri.SchemeDelimiter, "");

                    if (baseftpurl.StartsWith(Uri.UriSchemeFtp))
                        baseftpurl = baseftpurl.Substring(3);//(TrimStart(new []{'f','t','p'}).Replace(Uri.UriSchemeFtp, ""));

                    baseftpurl = (baseftpurl.EndsWith("/") ? baseftpurl.TrimEnd(new[] {'/'}) : baseftpurl);
                }
                
                Uri t = new Uri(Uri.UriSchemeFtp + Uri.SchemeDelimiter + 
                            //userpass +
                            baseftpurl +
                            ":" + basicMonitorFtpPathPortTextBox.Text.Trim());

                basicMonitorFtpPathTextBox.Text = t.ToString();

                FtpWebRequest wr = (FtpWebRequest)WebRequest.Create(t);
                wr.EnableSsl = basicMonitorFtpUseSslCheckBox.Checked;
                wr.Method = WebRequestMethods.Ftp.PrintWorkingDirectory; //NOTE: should be allowed by almost any ftp server you have permission to log in to
                wr.Credentials = new NetworkCredential(basicMonitorFtpUserTextBox.Text, basicMonitorFtpPassTextBox.Text);
                response = (FtpWebResponse)wr.GetResponse();
                if (response != null)
                {
                    if (response.StatusCode == FtpStatusCode.PathnameCreated)
                    {
                        basicMonitorFtpPathCheckResultText.ForeColor = Color.Green;
                        basicMonitorFtpPathCheckResultText.Text = "OK";
                    }
                    else
                    {
                        basicMonitorFtpPathCheckResultText.ForeColor = Color.Black;
                        basicMonitorFtpPathCheckResultText.Text = response.StatusDescription;
                    }
                }
                else
                {
                    basicMonitorHttpPathCheckResultText.ForeColor = Color.Black;
                    basicMonitorHttpPathCheckResultText.Text = "Failure";
                }
            }
            catch (Exception ex)
            {
                if (NetworkMonitor.VerboseLogging)
                    Logger.Instance.LogException(this.GetType(), ex);
                basicMonitorFtpPathCheckResultText.ForeColor = Color.Black;
                basicMonitorFtpPathCheckResultText.Text = "Failure";
            }
            finally
            {
                if(response != null)
                    response.Close();
            }
        }

        #endregion
        #region Single selection
        private void BasicMonitorGroupBoxEnter(object sender, EventArgs e)
        {
            GroupBox groupBox = (GroupBox) sender;
            groupBox.BackColor = Color.LightGray;
            //NOTE: uncheck boxes
            foreach (Control control in this.basicBaseTab.Controls)
            {
                if (control.GetType() == typeof(GroupBox))
                {
                    if (control.Name != groupBox.Name)
                    {
                        control.BackColor = Color.Transparent;
                        foreach (Control childcontrol in control.Controls)
                        {
                            if (childcontrol.Name.Contains("MonitorSelect") &&
                                childcontrol.GetType() == typeof (CheckBox))
                            {
                                ((CheckBox) childcontrol).CheckState = CheckState.Unchecked;
                                break;
                            }
                        }
                    }
                }
            }
            foreach (Control control in groupBox.Controls)
            {
                if (control.GetType() == typeof(CheckBox))
                {
                    if (control.Name.Contains("MonitorSelect"))
                    {
                        ((CheckBox) control).Checked = true;
                        _basicType = (BasicMonitorType) control.Tag;// Enum.Parse(typeof(BasicMonitorType), control.Tag.ToString());
                        break;
                    }
                }
            }
        }

        private void BasicMonitorMonitorSelectCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            if(checkBox.Parent.BackColor == Color.LightGray)
            {
                checkBox.CheckState = CheckState.Checked;
            }
        }
        #endregion

        private void BasicMonitorPortTextBoxTypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
                ((MaskedTextBox)sender).Text = ((MaskedTextBox)sender).Tag.ToString();
        }

    }
}