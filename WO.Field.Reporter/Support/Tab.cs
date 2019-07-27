using System.Collections.Generic;
using System.Text;

namespace WO.Field.Reporter.Support
{
    public class Tab
    {
        public Tab(string label)
        {
            Label = label;
            Chidren = new List<string>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Label);

            foreach (string line in Chidren)
                sb.Append("--").AppendLine(line);

            return sb.ToString();
        }

        public List<string> Chidren { get; private set; }
        public string Self { get; set; }
        public string Label { get; private set; }
    }
}
