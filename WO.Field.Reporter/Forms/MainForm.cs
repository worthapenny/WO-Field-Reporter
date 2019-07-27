using Haestad.Framework.Windows.Forms.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WO.Field.Reporter.Support;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WO.Field.Reporter.Forms
{
    public partial class MainForm : Form, IProgressMessage
    {
        public MainForm()
        {
            InitializeComponent();
            Tabs = new List<Tab>();

            Load += (o, e) => InitializeForm();
            FormClosing += (o, e) => Model.Current.Close();
            buttonReport.Click += (o, e) => GenerateReport();
            buttonReportBrowser.Click += (o, e) => BrowseReport();
        }

        public string Message { set => richTextBox.AppendText(value + Environment.NewLine); }


        private void InitializeForm()
        {
#if DEBUG
            textBoxSqlitePath.Text = @"D:\Temp\WO.TestModels\GT.wtg.sqlite";
#endif
        }
        private void GenerateReport()
        {
            string sqliteFilePath = textBoxSqlitePath.Text;

            if (!File.Exists(sqliteFilePath))
            {
                MessageBox.Show($"Given path {sqliteFilePath} does not exist");
                return;
            }

            ReportFields(sqliteFilePath, checkBoxIncludeResults.Checked);


        }
        private void BrowseReport()
        {
            if (Tabs?.Count ==0)
                GenerateReport();

            FieldReporterHtml reporter = new FieldReporterHtml(Tabs);
            reporter.WriteToJson();

            // Open up the browser
            string htmlFilePath = Path.Combine(Util.HTML_DIR, "report.html");
            Process.Start(htmlFilePath);

        }
        private void ReportFields(string sqliteFilePath, bool showResultFields)
        {           

            // trusting the given path as is
            bool opened = Model.Current.Open(sqliteFilePath, this);
            
            if (opened)
            {
                FieldReporter reporter = new FieldReporter();
                List<Tab> tabsAPI = reporter.Generate(Model.Current, this, new ProgressIndicatorForm(true, this));
                Tabs.AddRange(tabsAPI);

                List<Tab> tabsUI = reporter.GenerateByUI(Model.Current, this, new ProgressIndicatorForm(true, this), showResultFields);
                Tabs.AddRange(tabsUI);
            }
            else
            {
                Message = "Failed to open up the model.";
            }
        }

        

        private List<Tab> Tabs { get; set; }

    }
}
