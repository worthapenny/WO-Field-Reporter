using System.IO;

namespace WO.Field.Reporter.Support
{
    static class Util
    {
        static string _htmlDir = string.Empty;

        static public string EXE_DIR { get { return Directory.GetCurrentDirectory(); } }

        static public string HTML_DIR
        {
            get
            {
                if (!string.IsNullOrEmpty(_htmlDir))
                    return _htmlDir;

                string path = Path.Combine(EXE_DIR, "html");
                if (Directory.Exists(path))
                {
                    _htmlDir = path;
                    return _htmlDir;
                }


                while (!Directory.Exists(path))
                {
                    path = Path.Combine(Directory.GetParent(path).Parent.FullName, "html");
                }

                _htmlDir = path;
                return _htmlDir;

            }
        }
        static public string ASSETS_DIR { get { return Path.Combine(HTML_DIR, "assets"); } }

    }
}
