using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SCBTOOL
{
    class Process
    {
        DataProvider dp = new DataProvider();
        Connect cnn = new Connect();
        public  string      key = "system@admin";
        public string[]     processQty(string Qty)
        {
            string[] st;
            st = Qty.Split(new char[] { ',' });

            return st;
        }
        public string[]     processBatch(string Batch)
        {
            string[] st;
            st = Batch.Split(new char[] { ',' });

            return st;
        }
        public Boolean      checkQty(string[] qty, double total)
        {
            double sum = 0;
            for (int i = 0; i < qty.Length; i++)
            {
                sum += double.Parse(qty[i]);
            }
            if (sum == total)
                return true;
            else
                return false;
        }
        public List<string> getListByDataTable(DataTable tb)
        {
            List<string> l = new List<string>();
            foreach (DataRow dr in tb.Rows)
            {
                l.Add(dr[0].ToString());
            }
            return l;
        }
        public Boolean      checkUser(string user,string wh)
        {
            Boolean rt = false;
            string connecString = Connect.GetConnectString();
            if (dp.CheckUserException(connecString,user))
            {rt = true;}
            else { if(dp.CheckUserWH(connecString,user,wh)) rt = true; else rt = false;}                            
            return rt;
        }
        public Boolean      checkUserAllowCNXX(string user)
        {
            Boolean rt = false;
            string connecString = Connect.GetConnectString();
            if (dp.CheckUserException(connecString, user))
            { rt = true; }
            else { if (dp.CheckUserExistUserWHSetup(connecString, user)) rt = true; else rt = false; }
            return rt;
        }
        public Boolean      checkSplit(DataGridView dgv)
        {
            Boolean rs = true;
            double total = 0;
            double subtotal;
            string[] qtyArr;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if(dr.Cells["QTY"].Value.ToString().Length > 0)
                    total = double.Parse(dr.Cells["QTY"].Value.ToString());
                if (dr.Cells["EDITQTY"].Value.ToString() != "")
                {
                    qtyArr = processQty(dr.Cells["EDITQTY"].Value.ToString());
                    if (qtyArr.Length > 0)
                    {
                        subtotal = 0;
                        for (int i = 0; i < qtyArr.Length; i++)
                        {
                            subtotal += double.Parse(qtyArr[i]);
                        }
                        if (subtotal > total)
                            rs = false;
                    }
                }
            }
            return rs;
        }
        public List<string> getPackingByDatagridview(DataGridView dgv)
        {
            List<string> l= new List<string>();
            Boolean duplicate ;
            string packingId;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                duplicate = false;
                packingId = dr.Cells["PACKINGSLIPID"].Value.ToString();
                // check exist list packing
                if(l.Count  > 0 )
                {
                    for (int i = 0; i < l.Count; i++)
                    {if (l[i] == packingId){duplicate = true;}}
                }
                //add packingid to list if not exist
                if(!duplicate) l.Add(packingId);
            }
            return l;
        }
        public string       getPackingByList(List<string> l)
        {
            string rs="";
            //MessageBox.Show(l.Count.ToString());
            foreach (string i in l)
            {if (rs.Length > 0){rs += "," + i;}else{rs += i;}}
            return rs;
        }
        public string       WareHouseStringFilter(List<string> l)
        {
            string s = "";
            if (l.Count > 0){foreach (string wh in l){if (s.Length > 0){s += "," + wh;}else{s += wh;}}}
            return s;
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }  

    }
}
