namespace SCBTOOL
{
    partial class frSCBADMIN
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
            this.cbExpType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbExpType
            // 
            this.cbExpType.FormattingEnabled = true;
            this.cbExpType.Items.AddRange(new object[] {
            "None",
            "Posted",
            "No Posted"});
            this.cbExpType.Location = new System.Drawing.Point(104, 24);
            this.cbExpType.Name = "cbExpType";
            this.cbExpType.Size = new System.Drawing.Size(153, 21);
            this.cbExpType.TabIndex = 0;
            this.cbExpType.Text = "None";
            this.cbExpType.SelectedIndexChanged += new System.EventHandler(this.cbExpType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Export Posted";
            // 
            // frSCBADMIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 259);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbExpType);
            this.Name = "frSCBADMIN";
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.frSCBADMIN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExpType;
        private System.Windows.Forms.Label label1;
    }
}