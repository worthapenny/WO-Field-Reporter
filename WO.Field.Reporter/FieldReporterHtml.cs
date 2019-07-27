using System.Collections.Generic;
using System.IO;
using System.Text;
using WO.Field.Reporter.Support;

namespace WO.Field.Reporter
{
    public class FieldReporterHtml
    {
        #region Constructor
        public FieldReporterHtml(List<Tab> tabs)
        {
            Tabs = tabs;
        }
        #endregion

        #region Public Methods
        public string JsonFile { get { return Path.Combine(Util.ASSETS_DIR, "Data.json"); } }
        public string WriteToJson()
        {
            //string header = "var leftNavItems='[";
            //foreach (Tab tab in Tabs)
            //    header += "\"" + tab.Label + "\",";

            //header = header.Substring(0, header.Length - 1);
            //header += "]'";


            StringBuilder sb = new StringBuilder();
            sb.Append("var dataString = '{");

            foreach (Tab tab in Tabs)
            {
                sb.Append("\"" + tab.Label + "\":[");
                sb.Append(string.Join(",", tab.Chidren));
                sb.Append("],");
            }

            sb.Length--; // remove the last comma
            sb.Append("}';");

            // write to file
            File.WriteAllText(JsonFile, sb.ToString());

            return JsonFile;
        }
        #endregion


        #region Private properties
        private List<Tab> Tabs { get; set; }
        #endregion
    }
}
