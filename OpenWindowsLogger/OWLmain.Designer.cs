namespace OpenWindowsLogger
{
    partial class OWLmain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OWLmain));
      this.txtLogPath = new System.Windows.Forms.TextBox();
      this.btnEnableLogging = new System.Windows.Forms.Button();
      this.chkFilterExisting = new System.Windows.Forms.CheckBox();
      this.chkExtFilterList = new System.Windows.Forms.CheckBox();
      this.chkAutoOpenLog = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // txtLogPath
      // 
      this.txtLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLogPath.Enabled = false;
      this.txtLogPath.Location = new System.Drawing.Point(13, 13);
      this.txtLogPath.Name = "txtLogPath";
      this.txtLogPath.Size = new System.Drawing.Size(727, 20);
      this.txtLogPath.TabIndex = 0;
      // 
      // btnEnableLogging
      // 
      this.btnEnableLogging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEnableLogging.ForeColor = System.Drawing.Color.Red;
      this.btnEnableLogging.Location = new System.Drawing.Point(12, 53);
      this.btnEnableLogging.Name = "btnEnableLogging";
      this.btnEnableLogging.Size = new System.Drawing.Size(727, 23);
      this.btnEnableLogging.TabIndex = 2;
      this.btnEnableLogging.Text = "Logging Disabled";
      this.btnEnableLogging.UseVisualStyleBackColor = true;
      this.btnEnableLogging.Click += new System.EventHandler(this.btnEnableLogging_Click);
      // 
      // chkFilterExisting
      // 
      this.chkFilterExisting.AutoSize = true;
      this.chkFilterExisting.Checked = true;
      this.chkFilterExisting.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkFilterExisting.Location = new System.Drawing.Point(13, 36);
      this.chkFilterExisting.Name = "chkFilterExisting";
      this.chkFilterExisting.Size = new System.Drawing.Size(134, 17);
      this.chkFilterExisting.TabIndex = 3;
      this.chkFilterExisting.Text = "Filter Existing Windows";
      this.chkFilterExisting.UseVisualStyleBackColor = true;
      // 
      // chkExtFilterList
      // 
      this.chkExtFilterList.AutoSize = true;
      this.chkExtFilterList.Checked = true;
      this.chkExtFilterList.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkExtFilterList.Location = new System.Drawing.Point(153, 36);
      this.chkExtFilterList.Name = "chkExtFilterList";
      this.chkExtFilterList.Size = new System.Drawing.Size(108, 17);
      this.chkExtFilterList.TabIndex = 4;
      this.chkExtFilterList.Text = "External Filter List";
      this.chkExtFilterList.UseVisualStyleBackColor = true;
      // 
      // chkAutoOpenLog
      // 
      this.chkAutoOpenLog.AutoSize = true;
      this.chkAutoOpenLog.Checked = true;
      this.chkAutoOpenLog.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAutoOpenLog.Location = new System.Drawing.Point(268, 36);
      this.chkAutoOpenLog.Name = "chkAutoOpenLog";
      this.chkAutoOpenLog.Size = new System.Drawing.Size(92, 17);
      this.chkAutoOpenLog.TabIndex = 5;
      this.chkAutoOpenLog.Text = "AutoOpenLog";
      this.chkAutoOpenLog.UseVisualStyleBackColor = true;
      // 
      // OWLmain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(752, 88);
      this.Controls.Add(this.chkAutoOpenLog);
      this.Controls.Add(this.chkExtFilterList);
      this.Controls.Add(this.chkFilterExisting);
      this.Controls.Add(this.btnEnableLogging);
      this.Controls.Add(this.txtLogPath);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "OWLmain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "OWL";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OWLmain_FormClosed);
      this.Load += new System.EventHandler(this.OWLmain_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.TextBox txtLogPath;
    private System.Windows.Forms.Button btnEnableLogging;
    private System.Windows.Forms.CheckBox chkFilterExisting;
    private System.Windows.Forms.CheckBox chkExtFilterList;
    private System.Windows.Forms.CheckBox chkAutoOpenLog;
  }
}

