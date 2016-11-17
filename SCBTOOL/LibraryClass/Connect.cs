using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace SCBTOOL
{
    class Connect
    {
        public string key = "system@admin";
        ////SqlConnection Con = new SqlConnection("Data Source=.;Initial Catalog=VTIREPORT;Persist Security Info=True;User ID=sa;pwd=123@321;Connect Timeout=30"); //Enter Your sql Server Password eg.pwd=xyz
        public  static string GetConnectString()
        {
            string s ="";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Config.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Configuration");
            foreach (XmlNode xn in xnlist)
            {
                //MessageBox.Show(s);
                s = "Server=" + xn["Server"].InnerText + "; Database=" + xn["Database"].InnerText + ";Integrated Security=True;";
                //MessageBox.Show(con);
            }

            return s;
        }
        public static string GetLocationString()
        {
            string s = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Location.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Location");
            foreach (XmlNode xn in xnlist)
            {
                //MessageBox.Show(s);
                s = xn["StockCard"].InnerText;
                //MessageBox.Show(con);
            }
            return s;
        }
        public static string GetLocationString2()
        {
            string s = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/AdminLocation.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Location");
            foreach (XmlNode xn in xnlist)
            {
                //MessageBox.Show(s);
                s = xn["AdminLocation"].InnerText;
                //MessageBox.Show(con);
            }
            return s;
        }
        public string GetConnectStringWithUsernameAndPassword()
        {
            string s = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Config.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Configuration");
            foreach (XmlNode xn in xnlist)
            {
                string pwd = this.Decrypt(xn["Password"].InnerText);
                
                //MessageBox.Show(s);
                //s = "Server=" + xn["Server"].InnerText + "; Database=" + xn["Database"].InnerText + ";Integrated Security=True;";
                s = "Data Source=" + xn["Server"].InnerText + ";Initial Catalog=" + xn["Database"].InnerText
                    + ";Persist Security Info=True;User ID=" + xn["Username"].InnerText + ";pwd=" 
                    + pwd + ";Connect Timeout=30";
                //MessageBox.Show(s);
            }

            return s;
        }
        //
        public string GetPassword()
        {
            string s = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Config/Config.xml");
            XmlNodeList xnlist = xdoc.SelectNodes("/Configuration");
            foreach (XmlNode xn in xnlist)
            {
                s = xn["Password"].InnerText;
            }
            return s;
        }
        public string Encript(string pwd)
        {
            byte[] keyArray;
            //convert password từ string về byte
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(pwd);
            //su dung key
            //Hash key 
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.key));
            hashmd5.Clear();
            // Su dung class dung encript & Decrypt
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public string Decrypt(string cipherString)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
