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
    public partial class frSCBADMIN : Form
    {
        public frSCBADMIN()
        {
            InitializeComponent();
        }

        private void frSCBADMIN_Load(object sender, EventArgs e)
        {
            DataTable dt= DataProvider.getSCBSetup();           
            cbExpType.Text = dt.Rows[0]["EXPTYPE"].ToString();
        }

        private void cbExpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ExportType;
            switch (cbExpType.Text)
            {
                case "None":
                    {ExportType= "0"; break;}
                case "Posted":
                    {ExportType = "1";break;}
                case "No Posted":
                    {ExportType = "2";break;}
                default:
                    {ExportType = "0";break;}
            }
            DataProvider.UpdateSCBSetupExportType(ExportType);
        }
    }
}
