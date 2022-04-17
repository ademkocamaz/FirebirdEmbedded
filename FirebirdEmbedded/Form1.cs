using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
namespace FirebirdEmbedded
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        string connectionString = new FbConnectionStringBuilder
        {
            Database = @"data\mydb.fdb",
            ServerType = FbServerType.Embedded,
            UserID = "SYSDBA",
            Password = "masterkey",
            //ClientLibrary = "fbclient.dll"
            ClientLibrary = @"lib\fbclient.dll"
        }.ToString();

        private void button1_Click(object sender, EventArgs e)
        {


            //MessageBox.Show(connectionString);
            //FbConnection con = new FbConnection(connectionString);
            //con.Open();
            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                var da = new FbDataAdapter("SELECT * FROM CUSTOMER", connection);
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                var dbInfo = new FbDatabaseInfo(connection);

                MessageBox.Show("Server Version: " + dbInfo.GetServerVersion());
                File.WriteAllText(@"lib\version.txt", dbInfo.GetServerVersion());
                MessageBox.Show("ODS Version: " +
                    dbInfo.GetOdsVersion().ToString() +
                    "." +
                    dbInfo.GetOdsMinorVersion().ToString());

            }
        }
    }
}
