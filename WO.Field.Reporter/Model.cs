using Haestad.Domain;
using Haestad.Domain.ModelingObjects.Water;
using System;
using System.Text;
using System.Windows.Forms;
using WO.Field.Reporter.Forms;

namespace WO.Field.Reporter
{
    public class Model
    {
        #region Static Field
        private static Model _model;
        #endregion

        #region Static Properties
        public static Model Current
        {
            get
            {
                _model = _model ?? new Model();
                return _model;
            }
        }
        #endregion

        #region Public Methods
        public bool Open(string sqliteFilePath, IProgressMessage progress)
        {
            bool successful = true;
            Progress = progress;


            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Close if any
                Close();


                DataSource = new IdahoDataSource();
                DataSource.SetConnectionProperty(ConnectionProperty.FileName, sqliteFilePath);
                DataSource.SetConnectionProperty(ConnectionProperty.ConnectionType, ConnectionType.Sqlite);
                DataSource.SetConnectionProperty(ConnectionProperty.EnableSchemaUpdate, false);

                DataSource.Open();
                DomainDataSet = DataSource.DomainDataSetManager.DomainDataSet(1);

                progress.Message = $"Hydraulic model database got opened. Path: {sqliteFilePath}";

            }
            catch (System.Runtime.InteropServices.SEHException sehEx)
            {
                successful = false;
                string message =
                    "Exception occurred while opening up the model.\n May be you are trying to open a model from a directory where _ADMIN_ right is required to edit files?. \n\n";

                WriteException(sehEx, message);
            }
            catch (BadImageFormatException badImageEx)
            {
                successful = false;
                string message =
                    "Exception occurred while opening up the model. It appears 32/64 bit version conflict occurred. \n\n";

                WriteException(badImageEx, message);
            }
            catch (Exception ex)
            {
                successful = false;
                WriteException(ex, "");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


            return successful;
        }

        public void Close()
        {
            if (DataSource == null || !DataSource.IsOpen())
                return;

            DataSource.Close();
            DataSource.Dispose();
            DataSource = null;

            Progress.Message = "Model closed!";
        }
        #endregion

        #region Private Methods        

        private void WriteException(Exception ex, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------")
                .AppendLine(message)
                .AppendLine("----------------------------------------")
                .AppendLine("Exception Occured:")
                .AppendLine("Exception Message: " + ex.Message)
                .AppendLine("Exception Inner Message: " + ex.InnerException.ToString())
                .AppendLine("Exception Stack Trace: " + ex.StackTrace)
                .AppendLine("----------------------------------------");

            Progress.Message = sb.ToString();
        }
        #endregion

        #region Public Properties
        public IDomainDataSet DomainDataSet { get; private set; }
        public IdahoDataSource DataSource { get; protected set; }


        #endregion

        #region Private Properties
        private IProgressMessage Progress { get; set; }
        #endregion
    }
}
