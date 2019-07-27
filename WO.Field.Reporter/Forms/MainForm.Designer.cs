namespace WO.Field.Reporter.Forms
{
    partial class MainForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.checkBoxIncludeResults = new System.Windows.Forms.CheckBox();
            this.buttonReportBrowser = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.textBoxSqlitePath = new System.Windows.Forms.TextBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.checkBoxIncludeResults);
            this.splitContainer.Panel1.Controls.Add(this.buttonReportBrowser);
            this.splitContainer.Panel1.Controls.Add(this.buttonReport);
            this.splitContainer.Panel1.Controls.Add(this.textBoxSqlitePath);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.richTextBox);
            this.splitContainer.Size = new System.Drawing.Size(800, 450);
            this.splitContainer.SplitterDistance = 78;
            this.splitContainer.TabIndex = 0;
            // 
            // checkBoxIncludeResults
            // 
            this.checkBoxIncludeResults.AutoSize = true;
            this.checkBoxIncludeResults.Location = new System.Drawing.Point(15, 48);
            this.checkBoxIncludeResults.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxIncludeResults.Name = "checkBoxIncludeResults";
            this.checkBoxIncludeResults.Size = new System.Drawing.Size(167, 21);
            this.checkBoxIncludeResults.TabIndex = 1;
            this.checkBoxIncludeResults.Text = "Include Results Fields";
            this.checkBoxIncludeResults.UseVisualStyleBackColor = true;
            // 
            // buttonReportBrowser
            // 
            this.buttonReportBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReportBrowser.Location = new System.Drawing.Point(708, 7);
            this.buttonReportBrowser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonReportBrowser.Name = "buttonReportBrowser";
            this.buttonReportBrowser.Size = new System.Drawing.Size(80, 46);
            this.buttonReportBrowser.TabIndex = 4;
            this.buttonReportBrowser.Text = "Report In Browser";
            this.buttonReportBrowser.UseVisualStyleBackColor = true;
            // 
            // buttonReport
            // 
            this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReport.Location = new System.Drawing.Point(625, 7);
            this.buttonReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(80, 46);
            this.buttonReport.TabIndex = 3;
            this.buttonReport.Text = "Report Here";
            this.buttonReport.UseVisualStyleBackColor = true;
            // 
            // textBoxSqlitePath
            // 
            this.textBoxSqlitePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSqlitePath.Location = new System.Drawing.Point(15, 18);
            this.textBoxSqlitePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSqlitePath.Name = "textBoxSqlitePath";
            this.textBoxSqlitePath.Size = new System.Drawing.Size(604, 22);
            this.textBoxSqlitePath.TabIndex = 0;
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(800, 368);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.TextBox textBoxSqlitePath;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button buttonReportBrowser;
        private System.Windows.Forms.CheckBox checkBoxIncludeResults;
    }
}