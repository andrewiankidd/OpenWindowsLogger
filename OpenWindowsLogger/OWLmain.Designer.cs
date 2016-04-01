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
      this.btnEnableLogging.Location = new System.Drawing.Point(13, 40);
      this.btnEnableLogging.Name = "btnEnableLogging";
      this.btnEnableLogging.Size = new System.Drawing.Size(727, 23);
      this.btnEnableLogging.TabIndex = 2;
      this.btnEnableLogging.Text = "Logging Disabled";
      this.btnEnableLogging.UseVisualStyleBackColor = true;
      this.btnEnableLogging.Click += new System.EventHandler(this.btnEnableLogging_Click);
      // 
      // OWLmain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(752, 75);
      this.Controls.Add(this.btnEnableLogging);
      this.Controls.Add(this.txtLogPath);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "OWLmain";
      this.Text = "OWL";
      this.Load += new System.EventHandler(this.OWLmain_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.TextBox txtLogPath;
    private System.Windows.Forms.Button btnEnableLogging;
  }
}

