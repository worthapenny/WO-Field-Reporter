using Haestad.Domain;
using Haestad.Support.Support;
using System.Text;

namespace WO.Field.Reporter.Support
{
    public class FieldTypeReporter
    {
        #region Constructor
        public FieldTypeReporter(string name, string label, string category, int id, string dataType, string parentFieldName)
        {
            Name = name;
            Label = label;
            Category = category;
            ID = id;
            FieldDataType = dataType;
            ParentFieldName = parentFieldName;

            Fields = new string[] {"Category", "Name", "Label", "ParentFieldName", "FieldType", "FieldDataType",
                "NumericFormatterName", "StorageUnit", "ID", "Collection", "Enumerated", "ReadOnly" };
        }
        public FieldTypeReporter(IFieldType fieldType)
            : this(fieldType.Name, fieldType.Label, fieldType.Category, fieldType.Id, fieldType.FieldDataType().ToString(), "")
        {
            NumericFormatterName = fieldType.NumericFormatterName;
            Collection = fieldType.IsCollectionMemberField;
            Enumerated = fieldType.IsEnumeratedMemberField;
            ReadOnly = fieldType.IsReadOnly;
            ParentFieldName = fieldType.ParentCollectionFieldType == null ? "" : fieldType.ParentCollectionFieldType.ToString();
        }
        public FieldTypeReporter(IField field)
            : this(field.Name, field.Label, field.Category, field.Id, field.FieldDataType.ToString(), "")
        {
        }
        #endregion

        #region Public Methods
        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{")
                .Append($"\"Category\": \"{Category}\", ")
                .Append($"\"Name\": \"{Name}\", ")
                .Append($"\"Label\": \"{Label}\", ")
                .Append($"\"ParentFieldName\": \"{ParentFieldName}\", ")
                .Append($"\"FieldType\": \"{FieldType}\", ")
                .Append($"\"FieldDataType\": \"{FieldDataType}\", ")
                .Append($"\"NumericFormatterName\": \"{NumericFormatterName}\", ")
                .Append($"\"StorageUnit\": \"{StorageUnit}\", ")
                .Append($"\"ID\": {ID}, ")
                .Append($"\"Collection\": \"{Collection}\", ")
                .Append($"\"Enumerated\": \"{Enumerated}\", ")
                .Append($"\"ReadOnly\": \"{ReadOnly}\"")
                .Append("}");

            return sb.ToString().Replace("'", ""); // a single quite will upset the json string.
        }
        public string ToString(string seperator = ", ")
        {
            string _ = seperator;
            string line = $"{Category}{_}{Name}{_}{Label}{_}{ParentFieldName}{_}";
            line += $"{FieldType}{_}{FieldDataType}{_}{NumericFormatterName}{_}{StorageUnit}{_}";
            line += $"{ID}{_}{Collection}{_}{Enumerated}{_}{ReadOnly}";


            return line;
        }
        #endregion


        #region Public Properties
        public string Category { get; private set; }
        public string Name { get; private set; }
        public string Label { get; private set; }
        public string ParentFieldName { get; private set; }
        public string FieldType { get; private set; }
        public string FieldDataType { get; private set; }
        public string NumericFormatterName { get; private set; }
        public string StorageUnit { get; private set; }
        public int ID { get; private set; }
        public bool Collection { get; private set; }
        public bool Enumerated { get; private set; }
        public bool ReadOnly { get; private set; }

        public string[] Fields { get; }
        #endregion
    }
}
