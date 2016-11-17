using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace SCBTOOL
{
    class DataProvider
    {
        //Check user Exception
        public  Boolean     CheckUserException(string connectString,string user)
        {
            string cmdTxt = "SELECT * FROM [VTIREPORT].[dbo].[UserWHException] WHERE [UserName] = '" + user + "'";
            if (this.GetDataByCommandTextAndConnectString(cmdTxt, connectString).Rows.Count > 0)
                return true;
            else
                return false;
        }
        //
        public Boolean CheckUserExistUserWHSetup(string connectString, string user)
        {
            string cmdTxt = "SELECT * FROM [VTIREPORT].[dbo].[UserWH]  WHERE [User] = '" + user + "'";
            if (this.GetDataByCommandTextAndConnectString(cmdTxt, connectString).Rows.Count > 0)
                return true;
            else
                return false;
        }
        //Check user warehouse setup
        public Boolean CheckUserWH(string connectString, string user,string wh)
        {
            string cmdTxt = "SELECT * FROM [VTIREPORT].[dbo].[UserWH] WHERE [User] = '" + user + "' AND [Warehouse] = '" + wh + "'";
            if (this.GetDataByCommandTextAndConnectString(cmdTxt, connectString).Rows.Count > 0)
                return true;
            else
                return false;
        }
        //insert by cmd and connect string
        public       int    InsertByCmdTextAndConnecString(string commandText, string connectString)
        {
            int i=0;
            SqlConnection cnn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(commandText, cnn);
            try
            {
                cnn.Open();
                i = cmd.ExecuteNonQuery();
                cnn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return i;
        }
        //Get data by command St Procedure and connect
        public DataTable    GetDataBySPAndConnectString(string StProcedure, string connectString)
        {
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(StProcedure,cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return tb;
        }
        //Get data by command text and connect
        public DataTable    GetDataByCommandTextAndConnectString(string cmdText, string connectString)
        {
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(cmdText, cnn);
            cmd.CommandType = CommandType.Text;
            try
            {
                cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return tb;
        }
        //LOAD DATA METHODS
        public DataTable getPackByPackID(string StProcedure, string connectStr, string PackingId, string whFilter)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(StProcedure, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PackingId", SqlDbType.NVarChar, 50).Value = PackingId;
            cmd.Parameters.Add("@Wh", SqlDbType.NVarChar, 1000).Value = whFilter;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb); 
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public DataTable getPackBySO(string StProcedure, string connectStr, string saleID,string whFilter)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(StProcedure, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SaleId", SqlDbType.NVarChar,1000).Value = saleID;
            cmd.Parameters.Add("@Wh", SqlDbType.NVarChar,1000).Value = whFilter;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public DataTable getPackBySalesId(string StProcedure, string StrConnect, string saleID, string whFilter)
        {
            SqlConnection cnn = new SqlConnection(StrConnect);
            SqlCommand cmd = new SqlCommand(StProcedure, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SaleId", SqlDbType.NVarChar, 1000).Value = saleID;
            cmd.Parameters.Add("@Wh", SqlDbType.NVarChar, 1000).Value = whFilter;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public DataTable getPackByPackIdANDSO(string StProcedure, string connectStr, string SaleID, string PackId,string whFilter)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(StProcedure, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SaleId", SqlDbType.NVarChar,100).Value = SaleID;
            cmd.Parameters.Add("@PackId", SqlDbType.NVarChar,100).Value = PackId;
            cmd.Parameters.Add("@Wh",SqlDbType.NVarChar,1000).Value = whFilter;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public int       getNumber(string StProcedure, string connectStr, string PackingId)
        {

            SqlConnection cnn = new SqlConnection(connectStr);
            DataTable tb = new DataTable();
            SqlCommand cmd = new SqlCommand(StProcedure, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PackingId", SqlDbType.NVarChar, 20).Value = PackingId;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (tb.Rows.Count > 0)
                return int.Parse(tb.Rows[0][0].ToString());
            else
                return 0;
        }
        public int InsertNumberByTxt(string cmdTxt, string connectStr, string PackingId)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(cmdTxt, cnn);
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
        }
        public int InsertNumberByStore(string SP, int Id, string connectStr, string SalesId, string PackId, string Warehouse, string Sphieu, string Ctrinh, string DVTC, string DC,string NgayXX)
        {
            //dp.InsertNumberByStore("InsertNumber_VTIREPORT", id, strConnect, SalesId, PackingId, wh, txtNumber.Text, txtCTrinh.Text, txtDonVi.Text, txtDiaChi.Text);
            int rs;
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@SalesId",SqlDbType.NVarChar,20).Value = SalesId;
            cmd.Parameters.Add("@PackingId", SqlDbType.NVarChar, 20).Value = PackId;
            cmd.Parameters.Add("@Warehouse", SqlDbType.NVarChar, 10).Value = Warehouse;
            cmd.Parameters.Add("@SP",SqlDbType.NVarChar,50).Value = Sphieu;
            cmd.Parameters.Add("@CT", SqlDbType.NVarChar, 250).Value = Ctrinh;
            cmd.Parameters.Add("@DVTC", SqlDbType.NVarChar, 250).Value = DVTC;
            cmd.Parameters.Add("@DC", SqlDbType.NVarChar, 250).Value = DC;
            cmd.Parameters.Add("@UserName",SqlDbType.NVarChar,20).Value=Environment.UserName;
            cmd.Parameters.Add("@DateTime",SqlDbType.DateTime).Value = DateTime.Now.ToString();
            //cmd.Parameters.Add("@NgayXX", SqlDbType.Date).Value = DateTime.Now;
            cmd.Parameters.Add("@NgayXX", SqlDbType.Date).Value = NgayXX;
            cnn.Open();
            rs =  cmd.ExecuteNonQuery();
            cnn.Close();

            return rs;
        }
        public int InsertNumberLinesByStore(string SP, int NumId, string connectStr,string SalesId,string ItemId,string ItemName,double Qty
                                                       ,string UOM,string Batch,string Date)
        {

            int rs;
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NumId",SqlDbType.Int).Value = NumId;
            cmd.Parameters.Add("@SalesID",SqlDbType.NVarChar,20).Value =SalesId;
            cmd.Parameters.Add("@ItemId", SqlDbType.NVarChar,20).Value = ItemId;
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar,150).Value = ItemName;
            cmd.Parameters.Add("@Qty",SqlDbType.Float).Value =  Qty;
            cmd.Parameters.Add("@UOM", SqlDbType.NVarChar, 20).Value = UOM;
            cmd.Parameters.Add("@Batch", SqlDbType.NVarChar, 20).Value = Batch;
            //cmd.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(Date);
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(Date,"dd/MM/yyyy",null);
            cnn.Open();
            rs = cmd.ExecuteNonQuery();
            cnn.Close();
            return rs;
        }
         public int InsertNumberLinesByStore(string SP,string connectStr, int NumId,string SalesId,string PackingId,string wh,string ItemId
                        , string ItemName, string OrderQty, double Qty, string Unit, string EditQty, string Batch,string Date, string StrBatch,string InvAcc,string CustName)
        {
            int rs;
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NumId",SqlDbType.Int).Value = NumId;
            cmd.Parameters.Add("@SalesID", SqlDbType.NVarChar, 20).Value = SalesId;
            cmd.Parameters.Add("@PackingId", SqlDbType.NVarChar, 20).Value = PackingId;
            cmd.Parameters.Add("@Warehouse", SqlDbType.NVarChar, 10).Value = wh;
            cmd.Parameters.Add("@ItemId", SqlDbType.NVarChar, 20).Value = ItemId;
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 150).Value = ItemName;
            cmd.Parameters.Add("@OrderQty", SqlDbType.NVarChar, 20).Value = OrderQty;
            cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty;
            cmd.Parameters.Add("@UOM", SqlDbType.NVarChar, 20).Value = Unit;
            cmd.Parameters.Add("@EditQty", SqlDbType.NVarChar, 150).Value = EditQty;
            cmd.Parameters.Add("@Batch", SqlDbType.NVarChar,150).Value = Batch;
            //cmd.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(Date);
            //cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(Date,"dd/MM/yyyy",null);
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
            cmd.Parameters.Add("@StrBatch", SqlDbType.NVarChar, 150).Value = StrBatch;
            cmd.Parameters.Add("@InvoiceAccount", SqlDbType.NVarChar, 20).Value = InvAcc; 
            cmd.Parameters.Add("@CustName", SqlDbType.NVarChar, 250).Value = CustName;
            cnn.Open();
            rs = cmd.ExecuteNonQuery();
            cnn.Close();
            return rs;
        }
        public int ExecuteByCmdtxt(string cmdtxt,string conectStr)
        {
            int rs;
            SqlConnection cnn = new SqlConnection(conectStr);
            SqlCommand cmd = new SqlCommand(cmdtxt, cnn);
            cmd.CommandType = CommandType.Text;
            cnn.Open();
            rs = cmd.ExecuteNonQuery();
            cnn.Close();

            return rs;
        }
        public Boolean isUserInvoiceMandatory(string user, string wh, string connectStr)
        {
            Boolean rs = false;
            string cmdtxt = "SELECT * FROM VTIREPORT.dbo.UserWH WHERE  [user]  = '" + user + "' AND [Warehouse] = '" + wh + "' AND [InvoiceManda] = '1'";
            if (this.GetDataByCommandTextAndConnectString(cmdtxt, connectStr).Rows.Count > 0)
                rs = true;      
            return rs;
        }
        public Boolean isSOInvoiced(string SO,string connectStr)
        {
            Boolean rs = false;
            string cmdTxt = "SELECT * FROM VinhTuongAX2012.dbo.[SalesTable] WHERE [SALESID] ='" + SO + "' AND [SALESSTATUS] = '3'";
            if (this.GetDataByCommandTextAndConnectString(cmdTxt, connectStr).Rows.Count > 0)
                rs = true;
            return rs; 
        }
        public Boolean isUserWHInvoiceMandatoryValid(string user,string wh,string SO,string connectStr)
        {
            Boolean rs = true;
            if (this.isUserInvoiceMandatory(user, wh, connectStr) && !this.isSOInvoiced(SO, connectStr))
                rs = false;
            return rs;
        }
        public string[] getServerInfo()
        {
            string[] s = new string[2];
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Config.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Configuration");
            foreach (XmlNode xn in xnlist)
            {
                //MessageBox.Show(s);
                //s = "Server=" + xn["Server"].InnerText + "; Database=" + xn["Database"].InnerText + ";Integrated Security=True;";
                s[0] = xn["Server"].InnerText;
                s[1] = xn["Database"].InnerText;
                //MessageBox.Show(con);
            }
            return s;
        }
        public void updateServerInfo(string sv,string db)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Config.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Configuration");
            foreach (XmlNode xn in xnlist)
            {
                //MessageBox.Show(s);
                //s = "Server=" + xn["Server"].InnerText + "; Database=" + xn["Database"].InnerText + ";Integrated Security=True;";
                xn["Server"].InnerText = sv;
                xn["Database"].InnerText = db;
                //MessageBox.Show(con);
                xdoc.Save("Config/Config.xml");
            }
        }
        public int getId(string connectStr)
        {
            string cmd = "SELECT MAX(id) FROM VTIREPORT.dbo.Number";
            DataTable tb = GetDataByCommandTextAndConnectString(cmd, connectStr);
            if (tb.Rows.Count > 0)
            {
                if (tb.Rows[0][0].ToString() == "")
                    return 0;
                else
                    return int.Parse(tb.Rows[0][0].ToString());
            }
            return 0;
        }
        public DataTable getCNXXHistory(string SP, string connectStr, string user, string SO, string packId, string number, string from, string to)
        {

            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            if( from.Length > 0 )
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact(from, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", null);
            if (to.Length > 0)
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact(to, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2050", "dd/MM/yyyy", null);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 20).Value = user;
            cmd.Parameters.Add("@SalesId", SqlDbType.NVarChar, 20).Value = SO;
            cmd.Parameters.Add("@packId", SqlDbType.NVarChar, 100).Value = packId;
            cmd.Parameters.Add("@Number2", SqlDbType.NVarChar, 50).Value = number;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public DataTable getCNXXHistoryByWareHouse(string SP, string connectStr, string user, string SO, string packId, string number, string from, string to,string warehouse)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            if (from.Length > 0)
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact(from, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", null);
            if (to.Length > 0)
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact(to, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2050", "dd/MM/yyyy", null);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 20).Value = user;
            cmd.Parameters.Add("@SalesId", SqlDbType.NVarChar, 20).Value = SO;
            cmd.Parameters.Add("@packId", SqlDbType.NVarChar, 100).Value = packId;
            cmd.Parameters.Add("@Number2", SqlDbType.NVarChar, 50).Value = number;
            cmd.Parameters.Add("@Warehouse", SqlDbType.NVarChar, 100).Value = warehouse;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Lỗi kết nối tới Server !");
                MessageBox.Show(ex.Message.ToString());
            }
            return tb;
        }
        public List<string> getWHUserPermission(string user, string strConnect)
        {
            List<string> l = new List<string>();
            string cmd = "SELECT [Warehouse] FROM [VTIREPORT].[dbo].[UserWH] WHERE [User] = '" + user + "'" ;
            DataTable tb;
            tb = GetDataByCommandTextAndConnectString(cmd, strConnect);
            if (tb.Rows.Count > 0)
            {
                foreach (DataRow dr in tb.Rows)
                {
                    l.Add(dr[0].ToString());
                }
            }
            return l;
        }
        public List<string> getListAllWareHouse(string strConnect)
        {
            List<string> l = new List<string>();
            string cmd = "SELECT [Warehouse] FROM [VTIREPORT].[dbo].[WH]";
            DataTable tb;
            tb = GetDataByCommandTextAndConnectString(cmd, strConnect);
            if (tb.Rows.Count > 0)
            {
                foreach(DataRow dr in tb.Rows)
                {
                    l.Add(dr[0].ToString());
                }
            }
            return l;
        }
        public DataTable getStockCard(  string connectStr, string SP, string itemId, string wh
                                        ,string from, string to,string conf, string siz, string col, string ser)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandTimeout = 0;
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@item", SqlDbType.NVarChar, 20).Value = itemId;
            cmd.Parameters.Add("@wh", SqlDbType.NVarChar, 20).Value = wh;
            cmd.Parameters.Add("@config", SqlDbType.NVarChar, 10).Value = conf;
            cmd.Parameters.Add("@size", SqlDbType.NVarChar, 10).Value = siz;
            cmd.Parameters.Add("@color", SqlDbType.NVarChar, 10).Value = col;
            cmd.Parameters.Add("@serial", SqlDbType.NVarChar, 20).Value = ser;
            if (from.Length > 0)
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact(from, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", null);
            if (to.Length > 0)
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact(to, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2050", "dd/MM/yyyy", null);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            {MessageBox.Show(ex.Message.ToString());}return tb;
        }
        public DataTable getBeginQty(string connectStr, string SP, string itemId, string wh, string from, string to, string conf, string siz, string col, string ser)
        {
            SqlConnection cnn = new SqlConnection(connectStr);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandTimeout = 0;
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@item", SqlDbType.NVarChar, 20).Value = itemId;
            cmd.Parameters.Add("@wh", SqlDbType.NVarChar, 20).Value = wh;
            cmd.Parameters.Add("@config", SqlDbType.NVarChar, 10).Value = conf;
            cmd.Parameters.Add("@size", SqlDbType.NVarChar, 10).Value = siz;
            cmd.Parameters.Add("@color", SqlDbType.NVarChar, 10).Value = col;
            cmd.Parameters.Add("@serial", SqlDbType.NVarChar, 20).Value = ser;
            if (from.Length > 0)
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact(from, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2013", "dd/MM/yyyy", null);
            if (to.Length > 0)
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact(to, "dd/MM/yyyy", null);
            else
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact("01/01/2050", "dd/MM/yyyy", null);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }

        public DataTable getNhapXuatTonDimension(string connectString,string SP,string wh,string item,string from,string to)
        {
            SqlConnection cnn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandTimeout = 0;
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = DateTime.ParseExact(from, "dd/MM/yyyy", null);
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = DateTime.ParseExact(to, "dd/MM/yyyy", null);
            cmd.Parameters.Add("@wh", SqlDbType.NVarChar, 20).Value = wh;
            cmd.Parameters.Add("@item", SqlDbType.NVarChar, 20).Value = item;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public DataTable getInventory(string connectString, string SP, string wh,string date)
        {
            SqlConnection cnn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(SP, cnn);
            cmd.CommandTimeout = 0;
            DataTable tb = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@wh", SqlDbType.NVarChar, 20).Value = wh;
            cmd.Parameters.Add("@dt", SqlDbType.Date).Value = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }

        public static DataTable getPayment(string Jbn)
        {
            DataTable tb = new DataTable();

            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_UNCExport_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@Jbn", SqlDbType.NVarChar, 255).Value = Jbn;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public static DataTable getJournalList()
        {
            DataTable tb = new DataTable();

            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_JournalList_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }

        public static DataTable getJournalListByFilter(string Des)
        {
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_JournalListFilter_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@Des", SqlDbType.NVarChar, 255).Value = Des;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public static bool checkUser(string UserId)
        {
            bool exist = false;
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_GetUser_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@UserId", SqlDbType.NVarChar, 20).Value = UserId;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } 
            //
            if (tb.Rows.Count > 0)
                exist = true;
            return exist;

        }
        public static int InsertSCBExportHistory(string No,string DebitAccount, string PaymentType, string TransactionDate, string CustomerREF
            , string PayeeName, string PayeeAddress, string PayeeAccount, string PayeeBankCode, string PayeeBank, string Currency
            , string Amount, string PaymentDetails, string PayeeEmail, string BankCharge,string JnlBatNum,string RecId)
        {
            int rs;
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_InsertSCBExportHistory_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@No", SqlDbType.NVarChar, 20).Value = No;
            cmd.Parameters.Add("@DebitAccount", SqlDbType.NVarChar, 50).Value = DebitAccount;
            cmd.Parameters.Add("@PaymentType", SqlDbType.NVarChar, 20).Value = PaymentType;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.NVarChar, 20).Value = TransactionDate;
            cmd.Parameters.Add("@CustomerREF", SqlDbType.NVarChar, 50).Value = CustomerREF;
            cmd.Parameters.Add("@PayeeName", SqlDbType.NVarChar, 150).Value = PayeeName;
            cmd.Parameters.Add("@PayeeAddress", SqlDbType.NVarChar, 150).Value = PayeeAddress;
            cmd.Parameters.Add("@PayeeAccount", SqlDbType.NVarChar, 50).Value = PayeeAccount;
            cmd.Parameters.Add("@PayeeBankCode", SqlDbType.NVarChar, 50).Value = PayeeBankCode;
            cmd.Parameters.Add("@PayeeBank", SqlDbType.NVarChar, 150).Value = PayeeBank;
            cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 10).Value = Currency;
            cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = Amount;
            cmd.Parameters.Add("@PaymentDetails", SqlDbType.NVarChar,255).Value = PaymentDetails;
            cmd.Parameters.Add("@PayeeEmail", SqlDbType.NVarChar, 255).Value = PayeeEmail;
            cmd.Parameters.Add("@BankCharge", SqlDbType.NVarChar, 20).Value = BankCharge;
            cmd.Parameters.Add("@JnlBatNum", SqlDbType.NVarChar, 20).Value = JnlBatNum;
            cmd.Parameters.Add("@CreateBy", SqlDbType.NVarChar, 20).Value = Environment.UserName;
            cmd.Parameters.Add("@RecId", SqlDbType.NVarChar,20).Value = RecId; 
            cnn.Open();
            rs = cmd.ExecuteNonQuery();
            cnn.Close();

            return rs;
        }
        public static DataTable getSCBPaymentHistoryByJnlBatNum(string JnlBatNum)
        {
            DataTable tb = new DataTable();

            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_getExportHistoryByJnlBatNumVTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@JnlBatNum", SqlDbType.NVarChar, 255).Value = JnlBatNum;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public static DataTable getSCBPaymentHistoryByDate(string Date)
        {
            DataTable tb = new DataTable();

            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_getExportHistoryByDate_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@Date", SqlDbType.NVarChar,10).Value = Date;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public static bool checkPaymentJournalTransaction(string JournalRecId)
        {
            bool exist = false;
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_GetExportHistoryByJournalRecId_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@JournalRecId", SqlDbType.NVarChar, 20).Value = JournalRecId;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
            //
            if (tb.Rows.Count > 0)
                exist = true;
            return exist;

        }
        public static bool checkSCBAdmin(string UserId)
        {
            bool exist = false;
            DataTable tb = new DataTable();
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_GetUserAdmin_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@UserId", SqlDbType.NVarChar, 20).Value = UserId;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
            //
            if (tb.Rows.Count > 0)
                exist = true;
            return exist;

        }
        public static DataTable getSCBSetup()
        {
            DataTable tb = new DataTable();

            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_getSetup_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tb);
                cnn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); } return tb;
        }
        public static int UpdateSCBSetupExportType(string ExportType)
        {
            int rs;
            SqlConnection cnn = new SqlConnection(Connect.GetConnectString());
            SqlCommand cmd = new SqlCommand("SCB_UpdateSetup_VTIREPORT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExportType", SqlDbType.Int).Value = ExportType;
            cnn.Open();
            rs = cmd.ExecuteNonQuery();
            cnn.Close();

            return rs;
        }
    }
}
