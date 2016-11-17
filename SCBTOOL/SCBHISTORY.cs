using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCBTOOL
{
    public partial class frSCBHISTORY : Form
    {
        public frSCBHISTORY()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtDate.Text = dateTimePicker1.Text;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cbChooseFilterType.Checked)
            {
                if (txtDate.Text.Length == 10)
                {
                    string date = txtDate.Text.Substring(3, 2) + "/" + txtDate.Text.Substring(0, 2) + "/" + txtDate.Text.Substring(6, 4); // MM/dd/YYYY
                    //MessageBox.Show(date);
                    dgvExportHistory.DataSource = DataProvider.getSCBPaymentHistoryByDate(date);
                    dgvExportHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                else
                    MessageBox.Show("Kiểm tra lại ngày: định dạng  dd/MM/yyyy - tối đa 10 ký tự");
            }
            else
            {
                dgvExportHistory.DataSource = DataProvider.getSCBPaymentHistoryByJnlBatNum(txtJnlBatchNumber.Text);
                dgvExportHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

        }

        private void frSCBHISTORY_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (cbChooseFilterType.Checked)
            {
                txtDate.Enabled = true;
                dateTimePicker1.Enabled = true;
                txtJnlBatchNumber.Enabled = false;
            }
            else
            {
                txtDate.Enabled = false;
                dateTimePicker1.Enabled = false;
                txtJnlBatchNumber.Enabled = true;
            }
        }

        private void cbChooseFilterType_CheckedChanged(object sender, EventArgs e)
        {
            if (cbChooseFilterType.Checked)
            {
                txtDate.Enabled = true;
                dateTimePicker1.Enabled = true;
                txtJnlBatchNumber.Enabled = false;
            }
            else
            {
                txtDate.Enabled = false;
                dateTimePicker1.Enabled = false;
                txtJnlBatchNumber.Enabled = true;
            }
        }
    }
}
