using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public partial class NoConfigurationFound : Form
    {
        private Boolean _file = true; // File = true ifs its a file, otherwise its going to try to get it from the service
        private String _fileName = "";
        private String _ipOrHostName = "";
        private Int32 _port = 5502;

        public NoConfigurationFound()
        {
            InitializeComponent();
        }

        public Boolean File { get { return _file; } }
        public String FileName { get { return _fileName; } }
        public String IpOrHostName { get { return _ipOrHostName; } }
        public Int32 Port { get { return _port; } }

        private void LoadFromPathBtnClick(object sender, EventArgs e)
        {
            _file = true;

            this.Height = 190;
            this.okBtn.Location = new Point(87, 117);
            this.cancelBtn.Location = new Point(213, 117);

            ipLbl.Visible = false;
            ipTxt.Visible = false;
            portLbl.Visible = false;
            portMaskedTxt.Visible = false;
            okBtn.Visible = true;
            cancelBtn.Visible = true;

            DialogResult dialogResult = loadConfigurationFromPathOfd.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                _fileName = loadConfigurationFromPathOfd.FileName;
            }
        }

        private void LoadFromServiceBtnClick(object sender, EventArgs e)
        {
            _file = false;

            this.Height = 217;
            this.okBtn.Location = new Point(87, 157);
            this.cancelBtn.Location = new Point(213, 157);

            ipLbl.Visible = true;
            ipTxt.Visible = true;
            //portLbl.Visible = true;
            //portMaskedTxt.Visible = true;
            okBtn.Visible = true;
            cancelBtn.Visible = true;
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            if (!_file)
            {
                Object port = portMaskedTxt.ValidateText();
                if (port != null)
                    _port = Convert.ToInt32(port);
                else
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
                _ipOrHostName = ipTxt.Text;
            }
        }

        private void StartFreshBtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
