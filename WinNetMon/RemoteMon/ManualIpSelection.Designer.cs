namespace RemoteMon
{
    partial class ManualIpSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ManualIpSelection_StartRange_Lbl = new System.Windows.Forms.Label();
            this.ManualIpSelection_EndRange_Lbl = new System.Windows.Forms.Label();
            this.ManualIpSelection_EndRange_Txt = new System.Windows.Forms.TextBox();
            this.ManualIpSelection_StartRange_Txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(12, 138);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(107, 138);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // ManualIpSelection_StartRange_Lbl
            // 
            this.ManualIpSelection_StartRange_Lbl.AutoSize = true;
            this.ManualIpSelection_StartRange_Lbl.Location = new System.Drawing.Point(35, 13);
            this.ManualIpSelection_StartRange_Lbl.Name = "ManualIpSelection_StartRange_Lbl";
            this.ManualIpSelection_StartRange_Lbl.Size = new System.Drawing.Size(75, 13);
            this.ManualIpSelection_StartRange_Lbl.TabIndex = 1;
            this.ManualIpSelection_StartRange_Lbl.Text = "Start IP range:";
            // 
            // ManualIpSelection_EndRange_Lbl
            // 
            this.ManualIpSelection_EndRange_Lbl.AutoSize = true;
            this.ManualIpSelection_EndRange_Lbl.Location = new System.Drawing.Point(35, 68);
            this.ManualIpSelection_EndRange_Lbl.Name = "ManualIpSelection_EndRange_Lbl";
            this.ManualIpSelection_EndRange_Lbl.Size = new System.Drawing.Size(72, 13);
            this.ManualIpSelection_EndRange_Lbl.TabIndex = 1;
            this.ManualIpSelection_EndRange_Lbl.Text = "End IP range:";
            // 
            // ManualIpSelection_EndRange_MaskTxt
            // 
            this.ManualIpSelection_EndRange_Txt.Location = new System.Drawing.Point(38, 84);
            this.ManualIpSelection_EndRange_Txt.Name = "ManualIpSelection_EndRange_MaskTxt";
            this.ManualIpSelection_EndRange_Txt.Size = new System.Drawing.Size(116, 20);
            this.ManualIpSelection_EndRange_Txt.TabIndex = 1;
            // 
            // ManualIpSelection_StartRange_MaskTxt
            // 
            this.ManualIpSelection_StartRange_Txt.Location = new System.Drawing.Point(38, 29);
            this.ManualIpSelection_StartRange_Txt.Name = "ManualIpSelection_StartRange_MaskTxt";
            this.ManualIpSelection_StartRange_Txt.Size = new System.Drawing.Size(116, 20);
            this.ManualIpSelection_StartRange_Txt.TabIndex = 0;
            // 
            // ManualIpSelection
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(195, 173);
            this.Controls.Add(this.ManualIpSelection_StartRange_Txt);
            this.Controls.Add(this.ManualIpSelection_EndRange_Txt);
            this.Controls.Add(this.ManualIpSelection_EndRange_Lbl);
            this.Controls.Add(this.ManualIpSelection_StartRange_Lbl);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManualIpSelection";
            this.Text = "Manual Ip Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label ManualIpSelection_StartRange_Lbl;
        private System.Windows.Forms.Label ManualIpSelection_EndRange_Lbl;
        public System.Windows.Forms.TextBox ManualIpSelection_EndRange_Txt;
        public System.Windows.Forms.TextBox ManualIpSelection_StartRange_Txt;
    }
}