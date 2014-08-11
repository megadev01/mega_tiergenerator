using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TierGenerator.Common;
using TierGenerator.Properties;

namespace TierGenerator
{
    public partial class MainScreen : Form
    {

        #region Data members



        #endregion

        #region Constructor

        public MainScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to Populate tables grid
        /// </summary>
        private void PopulateTablesGrid()
        {

            gvTables.DataSource = TierGeneratorSettings.Instance.Database.Tables;
        }

        /// <summary>
        /// Generate the code
        /// </summary>
        private void GeneraterCode()
        {
            if (!ValidateData()) return;

            // Set the Tier Generator Setting Class
            SetSettingObject();

            // Generate Business Layer
            CodeGeneration.BusinessLayerGenerator blgenerator = new TierGenerator.CodeGeneration.BusinessLayerGenerator();
            blgenerator.Generate();

            // Generate Other CommonFiles
            CodeGeneration.CommonFileGenerator cfGenerator = new TierGenerator.CodeGeneration.CommonFileGenerator();

            cfGenerator.GenerateAppDotConfig(); // App.config
            cfGenerator.GenerateSqlStoreProcedures(); // store procedures 
            
            MessageBox.Show("Successfully generated.", "Code generation", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// Validate the Data
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            string errorMsg = string.Empty;

            // Get the tables count....
            if (GetSelectedTableCount() == 0)
            {
                errorMsg += "Please select the tables.\n";
            }

            // Check Project NAme
            if (txtProjectName.Text.Trim().Length == 0)
            {
                errorMsg += "Please provide Project Name.\n";
            }

            // Check 
            if (txtOutputDir.Text.Trim().Length == 0)
            {
                errorMsg += "Please provide Output directory.\n";
            }

            if (errorMsg != string.Empty)
            {
                MessageBox.Show(errorMsg, "Data required for code generation.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        /// <summary>
        /// Populate the setting object
        /// </summary>
        private void SetSettingObject()
        {
            TierGeneratorSettings setting = TierGeneratorSettings.Instance;

            setting.CodeGenerationPath = txtOutputDir.Text.Trim();
            setting.ClassPrefix = txtClassPrefix.Text.Trim();
            setting.ProjectNameSpace = txtProjectName.Text.Trim();
            setting.StoreProcedurePrefix = txtSpPrefix.Text.Trim();

            setting.GenerateBusinessLayer = chkBL.Checked;
            //setting.GenerateWebProject = chkWebProj.Checked;
            //setting.GenerateWindowProject = chkWinProj.Checked;
            //setting.GenerateHelp = chkHtmlHelp.Checked;
        }

        /// <summary>
        /// get the count of the selected Tables
        /// </summary>
        /// <returns>number of selected table</returns>
        private int GetSelectedTableCount()
        {
            int count = 0;
            Database database = TierGeneratorSettings.Instance.Database;

            if (database != null && database.Tables.Count > 0)
            {
                foreach (DatabaseTable table in database.Tables)
                {
                    if (table.IsSelected) count++;
                }

            }
            return count;
        }


        /// <summary>
        /// Method to load default values
        /// </summary>
        private void LoadDefaultValues()
        {
            txtProjectName.Text = Settings.Default.Setting_ProjectName;
            txtClassPrefix.Text = Settings.Default.Setting_ClassPrefix;
            txtSpPrefix.Text = Settings.Default.Setting_SpPrefix;
            txtOutputDir.Text = Settings.Default.Setting_OutputDirectory; 
 
        }

        /// <summary>
        /// method to save setting
        /// </summary>
        private void SaveSetting()
        {
            Settings.Default.Setting_ProjectName = txtProjectName.Text;
            Settings.Default.Setting_ClassPrefix = txtClassPrefix.Text;
            Settings.Default.Setting_SpPrefix = txtSpPrefix.Text;
            Settings.Default.Setting_OutputDirectory = txtOutputDir.Text; 

            Settings.Default.Save();

        }
        
        #endregion

        #region Events

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            DialogResult result = loginScreen.ShowDialog();

            if (result == DialogResult.OK)
            {
                PopulateTablesGrid();
                LoadDefaultValues();
            }
            else
            {
                Application.Exit();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow row in gvTables.Rows)
            {
                row.Cells["IsSelected"].Value = chkSelectAll.Checked;
                gvTables.UpdateCellValue(0, i);
                i++;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                GeneraterCode();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to generate code.\n"+ex.Message, "Error to generate code.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            SaveSetting();
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}