using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using OfficeOpenXml;
using System.Diagnostics;


namespace SCBTOOL
{
    public partial class SCBEXPORT : Form
    {
        public SCBEXPORT()
        {
            InitializeComponent();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if((dgvPayment.RowCount > 0) )
            {
                if (DataProvider.checkUser(Environment.UserName))
                {
                    if(CheckPreExport())
                        this.ExportExcelFile();
                    //Check
                }
                else
                    MessageBox.Show("User [" + Environment.UserName + "] không có quyền thực hiện chức năng này!");
                    
            }
            else
            {
                MessageBox.Show("không có dữ liệu để Export");
            }
        }
        private bool CheckPreExport()
        {
            bool rs = true;
            string Jnl = "",RecID = "",Amount,Vendor,Bank,Description;
            foreach (DataGridViewRow dr in dgvPayment.Rows)
            {
                Jnl = dr.Cells["JOURNALNUM"].Value.ToString();
                RecID = dr.Cells["RECID"].Value.ToString();
                if(DataProvider.checkPaymentJournalTransaction(RecID.ToString()))
                {
                    rs = false;
                    MessageBox.Show("Không thể export dữ liệu do thanh toán đã được export: \n" 
                                        + "Journal Batch Number :" + Jnl + "\n"
                                        + "Date:" + dr.Cells["Date"].Value.ToString() + "\n"
                                        + "Vendor:" + dr.Cells["Customer REF."].Value.ToString() + "\n"
                                        + "Bank:" + dr.Cells["Payee Bank"].Value.ToString() + "\n"
                                        + "Amount:" + dr.Cells["Amount"].Value.ToString() + "\n"
                                        + "Description:" + dr.Cells["Payment Details"].Value.ToString() + "\n"
                                        + "RECID:" + dr.Cells["RECID"].Value.ToString() );
                    return false;
                    break;
                }
            }
            return rs;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            //if (DataProvider.checkUser(Environment.UserName))
            {
                dgvPayment.DataSource = DataProvider.getPayment(txtJnlBNum.Text);
                dgvPayment.Columns["Amount"].DefaultCellStyle.Format = "#,##0.0000";
                this.ReadOnlyColummPaymentTable();
                this.AllowEditColummPayment();
            }
            //else
            //{
            //    MessageBox.Show("User [" + Environment.UserName + "] không có quyền thực hiện chức năng này!");
            //}
        }
        private void btnJnList_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                dgvJnList.DataSource = null;
                //
                btnJnList.Enabled = false;
                dgvJnList.DataSource = DataProvider.getJournalList();
                btnJnList.Enabled = true;
                btnJnList.Text = "Chọn";
                groupBox1.Visible = true;
                dgvJnList.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                dgvJnList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                if (dgvJnList.Columns.Contains("checkBoxColumn") && dgvJnList.Columns["checkBoxColumn"].Visible)
                {

                }
                else
                {
                    this.AddCheckboxColumn();
                }

                dgvJnList.Visible = true;
                dgvJnList.Columns["checkBoxColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvJnList.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                groupBox1.Visible = true;

                //Enable Check
                dgvJnList.EnableHeadersVisualStyles = false;
                dgvJnList.Columns["checkBoxColumn"].ReadOnly = false;
                
            }
            else
            {
                btnJnList.Text = "Journal list avaiable";
                groupBox1.Visible = false;
                string input = "";
                DataGridViewCheckBoxCell cbc;
                foreach (DataGridViewRow dr in dgvJnList.Rows)
                {
                    cbc = dr.Cells[0] as DataGridViewCheckBoxCell;
                    bool bchecked = (null != cbc && null != cbc.Value && true == (bool)cbc.Value);
                    if (true == bchecked)
                    {
                        if (input == "")
                        {
                            input += dr.Cells["JOURNALNUM"].Value.ToString();
                        }
                        else
                        {
                            input += "," + dr.Cells["JOURNALNUM"].Value.ToString();
                        }
                    }
                }
                if (input != "")
                    txtJnlBNum.Text = input;
            }
        }
        private void AddCheckboxColumn()
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Choose";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "checkBoxColumn";
            dgvJnList.Columns.Insert(0, checkBoxColumn);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvJnList.Enabled = false;
            dgvJnList.DataSource = DataProvider.getJournalListByFilter(txtDes.Text);
            dgvJnList.Enabled = true;
        }
        private void AllowEditColummPayment()
        {         
            dgvPayment.EnableHeadersVisualStyles = false;
            dgvPayment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvPayment.Columns["Bank Charge"].HeaderCell.Style.ForeColor = Color.Red;            
            dgvPayment.Columns["Bank Charge"].ReadOnly = false;

            dgvPayment.Columns["Transaction Date"].HeaderCell.Style.ForeColor = Color.Red;
            dgvPayment.Columns["Transaction Date"].ReadOnly = false;
        }
        private void ReadOnlyColummPaymentTable()
        {
            foreach (DataGridViewColumn col in dgvPayment.Columns)
                col.ReadOnly = true;
        }
        private void AllowEditColummCheckBoxJournalListTable()
        {
            dgvJnList.EnableHeadersVisualStyles = false;
            dgvJnList.Columns["checkBoxColumn"].HeaderCell.Style.ForeColor = Color.Red;
            dgvJnList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJnList.Columns["checkBoxColumn"].ReadOnly = false;
        }
        private void ReadOnlyColummCheckBoxJournalListTable()
        {
            foreach (DataGridViewColumn col in dgvJnList.Columns)
                col.ReadOnly = true;
        }
        private void ExportExcelFile()
        {
            string starUpPath = Connect.GetLocationString();
            string starUpPath2 = Connect.GetLocationString2(); //admin location file storage
            string FileName = @"PAYMENT-" + Environment.UserName + DateTime.Now.ToString("-ddMMyyyy-hhmmss") + ".xlsx";
            string No, DebitAccount, PaymentType, TransactionDate, CustomerREF, PayeeName, PayeeAddress, PayeeAccount, PayeeBankCode, PayeeBank
                    , Currency, Amount, PaymentDetails, PayeeEmail, BankCharge,JnlBatNum,RecId;
            string filePatch = starUpPath + FileName;
            string filePatch2 =  starUpPath2 +FileName;
            FileInfo newFile = new FileInfo(filePatch);           
            FileInfo newFile2 = new FileInfo(filePatch2);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(starUpPath + FileName);
            }
            if (newFile2.Exists)
            {
                newFile2.Delete();
                newFile2 = new FileInfo(starUpPath + FileName);
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                int rowIndex = 1;
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("PAYMENT");
         // Header
                ws.Cells[1, 1].Value = "No";
                ws.Cells[1, 2].Value = "Debit Account";
                ws.Cells[1, 3].Value = "Payment Type";
                ws.Cells[1, 4].Value = "Transaction Date";
                ws.Cells[1, 5].Value = "Customer REF.";
                ws.Cells[1, 6].Value = "Payee Name";
                ws.Cells[1, 7].Value = "Payee Address";
                ws.Cells[1, 8].Value = "Payee Account";
                ws.Cells[1, 9].Value = "Payee Bank Code";
                ws.Cells[1, 10].Value = "Payee Bank";
                ws.Cells[1, 11].Value = "Currency";
                ws.Cells[1, 12].Value = "Amount";
                ws.Cells[1, 13].Value = "Payment Details";
                ws.Cells[1, 14].Value = "Payee Email";
                ws.Cells[1, 15].Value = "Bank Charge";

         //Line
                foreach (DataGridViewRow dr in dgvPayment.Rows)
                {
                    rowIndex += 1;
                //------------SET VALUE---------------------------------------------------------------------------------------------------------
                        No = (rowIndex - 1).ToString();     //1
                        DebitAccount = dr.Cells["Debit Account"].Value.ToString();      //2
                        DebitAccount = DebitAccount.Replace(".", "");
                        DebitAccount = DebitAccount.Replace("-", "");
                        DebitAccount = DebitAccount.Replace(" ", "");

                        PaymentType = dr.Cells["Payment Type"].Value.ToString();        //3
                                                                                        //4
                        TransactionDate = CustomerREF = dr.Cells["Transaction Date"].Value.ToString();//DateTime.Now.ToString("dd/MM/yyyy");  
                        if (dr.Cells["Customer REF."].Value.ToString().Length <= 16)    //5
                            CustomerREF = dr.Cells["Customer REF."].Value.ToString();
                          else
                            CustomerREF = dr.Cells["Customer REF."].Value.ToString().Substring(0, 16);
                        if (dr.Cells["Payee Name"].Value.ToString().Length <= 70)       //6
                            PayeeName = Process.convertToUnSign3(dr.Cells["Payee Name"].Value.ToString());
                        else
                            PayeeName = Process.convertToUnSign3(dr.Cells["Payee Name"].Value.ToString()).Substring(0, 70);
                        if (dr.Cells["Payee Address"].Value.ToString().Length <= 70)    //7
                            PayeeAddress = dr.Cells["Payee Address"].Value.ToString();
                        else
                            PayeeAddress = dr.Cells["Payee Address"].Value.ToString().Substring(0, 70);
                        PayeeBankCode = dr.Cells["Payee Bank Code"].Value.ToString();   //9
                                                                                        //10
                        if (Process.convertToUnSign3(dr.Cells["Payee Bank"].Value.ToString()).Length <= 140)      
                            PayeeBank = Process.convertToUnSign3(dr.Cells["Payee Bank"].Value.ToString());
                        else
                            PayeeBank = Process.convertToUnSign3(dr.Cells["Payee Bank"].Value.ToString().Substring(0, 140));
                        
                        Currency = dr.Cells["Currency"].Value.ToString();               //11
                        Amount= dr.Cells["Amount"].Value.ToString();                    //12                        

                        if (dr.Cells["Payment Details"].Value.ToString().Length <= 140) //13
                           PaymentDetails = Process.convertToUnSign3(dr.Cells["Payment Details"].Value.ToString());
                        else
                            PaymentDetails = Process.convertToUnSign3(dr.Cells["Payment Details"].Value.ToString()).Substring(0, 140);
                       if (dr.Cells["Payee Email"].Value.ToString().Length <= 255)      //14
                            PayeeEmail = dr.Cells["Payee Email"].Value.ToString();
                        else
                            PayeeEmail = dr.Cells["Payee Email"].Value.ToString().Substring(0, 255);

                       BankCharge = dr.Cells["Bank Charge"].Value.ToString();           //15
                //--------------------------------------------------------------------------------------------------------------------------------------
                        //  1.  No.
                        ws.Cells[rowIndex, 1].Value = No;
                        //  2.  Debit Account
                        ws.Cells[rowIndex, 2].Value = DebitAccount;
                        //  3.  Payment Type
                        ws.Cells[rowIndex, 3].Value = PaymentType;
                        //  4.  Transaction Date
                        ws.Cells[rowIndex, 4].Value = TransactionDate;
                        //  5.  Customer REF. Mã giao dịch (max 16)
                        ws.Cells[rowIndex, 5].Value = CustomerREF;
                        //  6.  Payee Name (max 70)
                        ws.Cells[rowIndex, 6].Value = PayeeName;                      
                        //  7.  Payee Address (max 70)
                        ws.Cells[rowIndex, 7].Value = PayeeAddress;
                        //  8.  Payee Account (max 35)
                        PayeeAccount = dr.Cells["Payee Account"].Value.ToString();
                        PayeeAccount = PayeeAccount.Replace(".", "");
                        PayeeAccount = PayeeAccount.Replace("-", "");
                        PayeeAccount = PayeeAccount.Replace(" ", "");
                        if (PayeeAccount.Length <= 35)
                            ws.Cells[rowIndex, 8].Value = PayeeAccount;
                        else
                            ws.Cells[rowIndex, 8].Value = PayeeAccount.Substring(0, 35);
                        //  9.  Payee Bank Code
                        ws.Cells[rowIndex, 9].Value = PayeeBankCode;
                        //  10. Payee Bank (max 140)
                        ws.Cells[rowIndex, 10].Value = PayeeBank;
                        //  11. Currency
                        ws.Cells[rowIndex, 11].Value = Currency;

                        //  12. Amount
                        ws.Cells[rowIndex, 12].Value = Amount;
                        ws.Cells[rowIndex, 12].Style.Numberformat.Format = "#,##0.0000";
                        ws.Cells[rowIndex, 12].Style.Font.Bold = true;
                        
                        //  13. Payment Details (max 140)
                         ws.Cells[rowIndex, 13].Value = PaymentDetails;
                        //  14. Payee Email (max 255)
                        ws.Cells[rowIndex, 14].Value = PayeeEmail;
                        //  15. Bank Charge
                        ws.Cells[rowIndex, 15].Value = BankCharge;
                    //INSERT
                        RecId = dr.Cells["RecId"].Value.ToString();
                        JnlBatNum = dr.Cells["JOURNALNUM"].Value.ToString();
                    DataProvider.InsertSCBExportHistory(No, DebitAccount, PaymentType, TransactionDate, CustomerREF, PayeeName
                        , PayeeAddress, PayeeAccount, PayeeBankCode, PayeeBank, Currency, Amount, PaymentDetails, PayeeEmail, BankCharge, JnlBatNum, RecId);
                }
                package.Save();
                //
                //package.SaveAs();
                // Openning the created excel file using MS Excel Application
                ProcessStartInfo pi = new ProcessStartInfo(filePatch);
                System.Diagnostics.Process.Start(pi);
                //system.Process.Start(pi);
            }
        }
        private void SCBEXPORT_Load(object sender, EventArgs e)
        {
            mnAdmin.Visible = false;
            mnAdmin.Enabled = false;
            if (!DataProvider.checkUser(Environment.UserName))
            {
                // MessageBox.Show("User [" + Environment.UserName + "] không có quyền thực hiện chức năng này!");
                //Application.Exit();
            }
            else
            {
                //MessageBox.Show(DataProvider.checkSCBAdmin(Environment.UserName).ToString());
                if (DataProvider.checkSCBAdmin(Environment.UserName))
                {
                    mnAdmin.Visible = true;
                    mnAdmin.Enabled = true;
                }
                else
                {
                    mnAdmin.Visible = false;
                    mnAdmin.Enabled = false;
                }
            }
        }
        private void btnHistory_Click(object sender, EventArgs e)
        {
            frSCBHISTORY sch = new frSCBHISTORY();
            sch.ShowDialog();
        }
        private void mnAdmin_Click(object sender, EventArgs e)
        {
            frSCBADMIN frAdm = new frSCBADMIN();
            frAdm.ShowDialog();
        }

    }
}
