namespace SCBTOOL
{
    partial class frSCBHISTORY
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
            this.dgvExportHistory = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtJnlBatchNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbChooseFilterType = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExportHistory
            // 
            this.dgvExportHistory.AllowUserToAddRows = false;
            this.dgvExportHistory.AllowUserToDeleteRows = false;
            this.dgvExportHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExportHistory.Location = new System.Drawing.Point(12, 90);
            this.dgvExportHistory.Name = "dgvExportHistory";
            this.dgvExportHistory.ReadOnly = true;
            this.dgvExportHistory.Size = new System.Drawing.Size(1232, 459);
            this.dgvExportHistory.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(438, 27);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 29);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtJnlBatchNumber
            // 
            this.txtJnlBatchNumber.Location = new System.Drawing.Point(146, 32);
            this.txtJnlBatchNumber.Name = "txtJnlBatchNumber";
            this.txtJnlBatchNumber.Size = new System.Drawing.Size(277, 20);
            this.txtJnlBatchNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Journal Batch Number";
            // 
            // txtDate
            // 
            this.txtDate.AcceptsReturn = true;
            this.txtDate.Location = new System.Drawing.Point(146, 58);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(141, 20);
            this.txtDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Export Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(304, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // cbChooseFilterType
            // 
            this.cbChooseFilterType.AutoSize = true;
            this.cbChooseFilterType.Location = new System.Drawing.Point(146, 9);
            this.cbChooseFilterType.Name = "cbChooseFilterType";
            this.cbChooseFilterType.Size = new System.Drawing.Size(121, 17);
            this.cbChooseFilterType.TabIndex = 7;
            this.cbChooseFilterType.Text = "Filter by Export Date";
            this.cbChooseFilterType.UseVisualStyleBackColor = true;
            this.cbChooseFilterType.CheckedChanged += new System.EventHandler(this.cbChooseFilterType_CheckedChanged);
            // 
            // frSCBHISTORY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 580);
            this.Controls.Add(this.cbChooseFilterType);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJnlBatchNumber);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dgvExportHistory);
            this.Name = "frSCBHISTORY";
            this.Text = "SCBHISTORY";
            this.Load += new System.EventHandler(this.frSCBHISTORY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExportHistory;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtJnlBatchNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox cbChooseFilterType;
    }
}