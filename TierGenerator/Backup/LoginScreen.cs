using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TierGenerator.Common;
using TierGenerator.DatabaseSchema;
using TierGenerator.Properties;

namespace TierGenerator
{
    public partial class LoginScreen : Form
    {

        Database _database = null;
        string _connectionString = string.Empty;

        #region Constructor

        public LoginScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties


        /// <summary>
        /// get the databse schema
        /// </summary>
        public Database Database
        {
            get { return _database; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Method to validate the forms
        /// </summary>
        /// <returns>true for valid Data</returns>
        private bool IsValidate()
        {
            errorProvider1.Clear();
            bool result = true;

            // Sql Server Name
            if (txtSqlServer.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtSqlServer, "Please Provide SQL Server.");
                result = false;
            }

            // CataLog
            if (txtCatalog.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtCatalog, "Please Provide Catalog Name.");
                result = false;
            }

            return result;

        }

        /// <summary>
        /// Test the connection
        /// </summary>
        /// <returns></returns>
        private bool TestConnection()
        {
            string connectionString = GetConnectionString();
            try
            {
                return SqlDatabaseSchema.TestConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect.\n" + ex.Message);
            }
            return false;

        }

        /// <summary>
        /// Load schema in the database object
        /// </summary>
        private void LoadSchema()
        {

            _connectionString = GetConnectionString();
            IDatabaseSchema dbSchema = new SqlDatabaseSchema();


            _database = dbSchema.GetDataBaseSchema(txtSqlServer.Text, txtCatalog.Text, _connectionString);


            // Set in global Valiable
            TierGeneratorSettings.Instance.Database = _database;
            TierGeneratorSettings.Instance.ConnectionString = _connectionString;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Method to Get The Connection string
        /// </summary>
        /// <returns>Connection String</returns>
        private string GetConnectionString()
        {
            string connectionString = string.Empty;
            if (rbtnWindowsAuthentication.Checked)
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog.Text + ";" +
                         "Integrated Security=SSPI";
            }
            else
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog.Text + ";" +
                         "User Id = " + txtLogin.Text + ";Password = " + txtPassword.Text;

            }

            return connectionString;

        }

        /// <summary>
        /// Method to load default values
        /// </summary>
        private void LoadDefaultValues()
        {
            txtCatalog.Text = Settings.Default.Database_Catalog;
            txtSqlServer.Text = Settings.Default.DataBase_SqlServer;
            rbtnSqlServerAuthentication.Checked = Settings.Default.Database_SqlAuthentication;

            if (rbtnSqlServerAuthentication.Checked)
            {
                txtPassword.Text = Settings.Default.Database_Password;
                txtLogin.Text = Settings.Default.Database_UserName;
            }
            else
            {
                txtPassword.Text = "";
                txtLogin.Text = "";
            }
        }

        /// <summary>
        /// method to save setting
        /// </summary>
        private void SaveSetting()
        {
            Settings.Default.Database_Catalog = txtCatalog.Text;
            Settings.Default.DataBase_SqlServer = txtSqlServer.Text;
            Settings.Default.Database_SqlAuthentication = rbtnSqlServerAuthentication.Checked;
            Settings.Default.Database_Password = txtPassword.Text;
            Settings.Default.Database_UserName = txtLogin.Text;

            Settings.Default.Save();

        }

        #endregion

        #region Events

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
        }

        private void rbtnSqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            gbSQLServerCredential.Enabled = rbtnSqlServerAuthentication.Checked;
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {

            if (!IsValidate()) return;

            if (TestConnection())
            {
                try
                { // Load schema
                    LoadSchema();

                    // Save setting for future
                    SaveSetting();


                    // Close Form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to load schema.\n" + ex.Message, "Error to load schema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        #endregion



    }
}