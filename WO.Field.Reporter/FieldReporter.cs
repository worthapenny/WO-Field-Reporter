using Haestad.Domain;
using Haestad.Support.Support;
using Haestad.Support.User;
using System;
using System.Collections.Generic;
using WO.Field.Reporter.Forms;
using WO.Field.Reporter.Support;

namespace WO.Field.Reporter
{
    public class FieldReporter
    {
        #region Constructor
        public FieldReporter()
        {
        }
        #endregion

        #region Public Methods
        public List<Tab> Generate(Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            List<Tab> tabs = new List<Tab>();
            try
            {

                foreach (ModelingElementType modelingElementType in Enum.GetValues(typeof(ModelingElementType)))
                {
                    progress.Message = $"\n\n------------------------------------\nModeling element type: {modelingElementType.ToString().ToUpper()}...\n\n";

                    Tab tab = new Tab(modelingElementType.ToString());
                    tabs.Add(tab);

                    foreach (IFieldType fieldType in model.DomainDataSet.DomainDataSetType().ModelingElementType(modelingElementType).FieldTypes())
                        ReportModelingElementFieldTypes(progress, tab, fieldType);


                    switch (modelingElementType)
                    {
                        case ModelingElementType.All:
                        case ModelingElementType.EmbeddedStickyObject:
                        case ModelingElementType.EngineeringLibrary:
                        case ModelingElementType.Profile:
                        case ModelingElementType.PrototypeDomainElement:
                            break;

                        case ModelingElementType.Alternative:
                            ReportAlternativeFields(tab, model, progress, hmProgress);
                            break;


                        case ModelingElementType.CalculationOptions:
                            ReportCalculationOptionsFields(tab, model, progress, hmProgress);
                            break;


                        case ModelingElementType.DomainElement:
                            ReportDomainElementFields(tab, model, progress, hmProgress);
                            break;


                        case ModelingElementType.Scenario:
                            ReportScenarioFields(tab, model, progress, hmProgress);
                            break;


                        case ModelingElementType.SelectionSet:
                            ReportSelectionSets(tab, model, progress, hmProgress);
                            break;


                        case ModelingElementType.SupportElement:
                            ReportSupportingElementFields(tab, model, progress, hmProgress);
                            break;


                        default:
                            break;
                    }
                }
            }
            finally
            {
                hmProgress.Done();
            }

            return tabs;

        }
        public List<Tab> GenerateByUI(Model model, IProgressMessage progress, IProgressIndicator hmProgress, bool showResultFields)
        {
            List<Tab> tabs = new List<Tab>();
            try
            {
                DomainElementTypeCollection items = model.DomainDataSet.DomainDataSetType().DomainElementTypes(false);

                foreach (IDomainElementType domainElementType in items)
                {
                    string name = domainElementType.ToString();
                    progress.Message = $"\n\n------------------------------------\nDomain element type: {name.ToUpper()}...\n\n";


                    Tab tab = new Tab("Element-" + model.DomainDataSet.DomainDataSetType().DomainElementType(domainElementType.Id).Label);
                    tabs.Add(tab);


                    FieldCollection fields = model.DomainDataSet.DomainElementManager(domainElementType.Id).SupportedFields();

                    if (showResultFields)
                        fields.AddRange(model.DomainDataSet.DomainElementManager(domainElementType.Id).SupportedResultFields());


                    FieldTypeReporter reporter;

                    string message = $"Working with the {name}...";
                    hmProgress.AddTask(message).Steps = fields.Count;
                    hmProgress.IncrementTask();
                    hmProgress.BeginTask(fields.Count);

                    foreach (IField field in fields)
                    {
                        hmProgress.IncrementStep();
                        reporter = new FieldTypeReporter(field);
                        tab.Chidren.Add(reporter.ToJson());
                        progress.Message = reporter.ToString();
                    }
                    hmProgress.EndTask();

                }


                foreach (IdahoSupportElementTypes supportElementType in Enum.GetValues(typeof(IdahoSupportElementTypes)))
                {
                    string name = supportElementType.ToString();
                    progress.Message = $"\n\n------------------------------------\nSupport element type: {name.ToUpper()}...\n\n";


                    Tab tab = new Tab("Support-" + model.DomainDataSet.DomainDataSetType().SupportElementType((int)supportElementType).Label);
                    tabs.Add(tab);


                    FieldCollection fields = model.DomainDataSet.SupportElementManager((int)supportElementType).SupportedFields();

                    FieldTypeReporter reporter;

                    string message = $"Working with the {name}...";
                    hmProgress.AddTask(message).Steps = fields.Count;
                    hmProgress.IncrementTask();
                    hmProgress.BeginTask(fields.Count);

                    foreach (IField field in fields)
                    {
                        hmProgress.IncrementStep();
                        reporter = new FieldTypeReporter(field);
                        tab.Chidren.Add(reporter.ToJson());
                        progress.Message = reporter.ToString();
                    }
                    hmProgress.EndTask();
                }

                /*foreach (IdahoAlternativeTypes alternativeType in Enum.GetValues(typeof(IdahoAlternativeTypes)))
                {
                    
                    try
                    {
                        Tab tab = new Tab("Alternative-" + model.DomainDataSet.DomainDataSetType().AlternativeType((int)alternativeType).Label);
                        FieldCollection fields = model.DomainDataSet.AlternativeManager((int)alternativeType).SupportedFields();

                        string name = alternativeType.ToString();
                        progress.Message = $"\n\n------------------------------------\nAlternative type: {name.ToUpper()}...\n\n";

                        tabs.Add(tab);
                        FieldTypeReporter reporter;

                        string message = $"Working with the {name}...";
                        hmProgress.AddTask(message).Steps = fields.Count;
                        hmProgress.IncrementTask();
                        hmProgress.BeginTask(fields.Count);

                        foreach (IField field in fields)
                        {
                            hmProgress.IncrementStep();
                            reporter = new FieldTypeReporter(field);
                            tab.Chidren.Add(reporter.ToJson());
                            progress.Message = reporter.ToString();
                        }
                        hmProgress.EndTask();
                    }
                    catch{}
                   
                }*/


            }
            finally
            {
                hmProgress.Done();
            }

            return tabs;
        }
        #endregion

        #region Private methods
        private void ReportModelingElementFieldTypes(IProgressMessage progress, Tab tab, IFieldType fieldType)
        {
            FieldTypeReporter reporter = new FieldTypeReporter(fieldType);
            progress.Message = reporter.ToString();
            tab.Chidren.Add(reporter.ToJson());

            if (fieldType.FieldDataType().ToString() == "Collection")
                ReportCollecionFieldTypes(progress, tab, fieldType);
        }
        private void ReportAlternativeFields(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            AlternativeTypeCollection alternativeTypes = model.DomainDataSet.DomainDataSetType().AlternativeTypes();
            FieldTypeReporter reporter;

            string message = "Working with the alternatives...";
            hmProgress.AddTask(message).Steps = alternativeTypes.Count;
            hmProgress.IncrementTask();
            hmProgress.BeginTask(alternativeTypes.Count);

            foreach (IAlternativeType alternativeType in alternativeTypes)
            {
                hmProgress.IncrementStep();
                reporter = new FieldTypeReporter(alternativeType.Name, alternativeType.Label, "alternativeType", alternativeType.Id, "", "");
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                foreach (IFieldType fieldType in alternativeType.FieldTypes())
                    ReportCollecionFieldTypes(progress, tab, fieldType);
            }
            hmProgress.EndTask();
        }
        private void ReportCalculationOptionsFields(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            NumericalEngineTypeCollection types = model.DomainDataSet.DomainDataSetType().NumericalEngineTypes();
            FieldTypeReporter reporter;

            string message = "Working with the calc options...";
            hmProgress.AddTask(message).Steps = types.Count;
            hmProgress.IncrementTask();
            hmProgress.BeginTask(types.Count);

            foreach (INumericalEngineType itemType in types)
            {
                hmProgress.IncrementStep();
                reporter = new FieldTypeReporter(itemType.Name, itemType.Label, "numericEngineType", itemType.Id, "", "");
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                foreach (IFieldType fieldType in itemType.FieldTypes())
                    ReportCollecionFieldTypes(progress, tab, fieldType);
            }
            hmProgress.EndTask();
        }
        private void ReportDomainElementFields(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            DomainElementTypeCollection types = model.DomainDataSet.DomainDataSetType().DomainElementTypes();
            FieldTypeReporter reporter;

            string message = "Working with the domain elements...";
            hmProgress.AddTask(message).Steps = types.Count;
            hmProgress.IncrementTask();
            hmProgress.BeginTask(types.Count);

            foreach (IDomainElementType itemType in types)
            {
                hmProgress.IncrementStep();
                reporter = new FieldTypeReporter(itemType.Name, itemType.Label, "domainElementType", itemType.Id, "", itemType.ParentID.ToString());
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                foreach (IFieldType fieldType in itemType.FieldTypes())
                    ReportCollecionFieldTypes(progress, tab, fieldType);
            }
            hmProgress.EndTask();
        }
        private void ReportSupportingElementFields(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            SupportElementTypeCollection types = model.DomainDataSet.DomainDataSetType().SupportElementTypes();
            FieldTypeReporter reporter;

            string message = "Working with the support elements...";
            hmProgress.AddTask(message).Steps = types.Count;
            hmProgress.IncrementTask();
            hmProgress.BeginTask(types.Count);

            foreach (ISupportElementType itemType in types)
            {
                hmProgress.IncrementStep();
                reporter = new FieldTypeReporter(itemType.Name, itemType.Label, "supportElementType", itemType.Id, "", "");
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                foreach (IFieldType fieldType in itemType.FieldTypes())
                    ReportCollecionFieldTypes(progress, tab, fieldType);
            }
            hmProgress.EndTask();
        }

        private void ReportScenarioFields(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            IScenario scenario = (IScenario)model.DomainDataSet.ScenarioManager.Elements()[0];

            ReportSupportedFields(
                "Working with the scenario...",
                scenario.SupportedFields(),
                tab,
                hmProgress,
                progress,
                scenario.Id
                );


            //FieldCollection fields = scenario.SupportedFields();
            //FieldTypeReporter reporter;

            //string message = "Working with the scenario...";
            //hmProgress.AddTask(message).Steps = fields.Count;
            //hmProgress.IncrementTask();
            //hmProgress.BeginTask(fields.Count);

            //foreach (IField field in fields)
            //{
            //    hmProgress.IncrementStep();
            //    reporter = new FieldTypeReporter(field);
            //    tab.Chidren.Add(reporter.ToJson());
            //    progress.Message = reporter.ToString();
            //    ReportCollectionFieldsType(tab, progress, field, scenario.Id);
            //}

            hmProgress.EndTask();
        }
        private void ReportSelectionSets(Tab tab, Model model, IProgressMessage progress, IProgressIndicator hmProgress)
        {
            ISelectionSetManager selecitonSetManager = model.DomainDataSet.SelectionSetManager;
            // Create an empty SS if none are there            
            if (selecitonSetManager.ElementIDs().Count == 0)
            {
                int id = selecitonSetManager.Add();
                ISelectionSet selectionSet = (ISelectionSet)selecitonSetManager.Element(id);
                selectionSet.Label = "Blank SS created by WO.Domain.Reporter";
            }


            ReportSupportedFields(
                "Working with the selection set...",
                selecitonSetManager.SupportedFields(),
                tab,
                hmProgress,
                progress,
                selecitonSetManager.ElementIDs()[0]
                );


            //FieldCollection fields = selecitonSetManager.SupportedFields();
            //FieldTypeReporter reporter;

            //string message = "Working with the selection set...";
            //hmProgress.AddTask(message).Steps = fields.Count;
            //hmProgress.IncrementTask();
            //hmProgress.BeginTask(fields.Count);          


            //foreach (IField field in fields)
            //{
            //    hmProgress.IncrementStep();
            //    reporter = new FieldTypeReporter(field);
            //    tab.Chidren.Add(reporter.ToJson());
            //    progress.Message = reporter.ToString();
            //    ReportCollectionFieldsType(tab, progress, field, selecitonSetManager.ElementIDs()[0]);
            //}

            hmProgress.EndTask();
        }



        private void ReportSupportedFields(string message, FieldCollection fields,
            Tab tab, IProgressIndicator hmProgress, IProgressMessage progress, int elementID)
        {
            //string message = "Working with the scenario...";
            FieldTypeReporter reporter;

            hmProgress.AddTask(message).Steps = fields.Count;
            hmProgress.IncrementTask();
            hmProgress.BeginTask(fields.Count);

            foreach (IField field in fields)
            {
                hmProgress.IncrementStep();
                reporter = new FieldTypeReporter(field);
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                ReportCollectionFieldsType(tab, progress, field, elementID);
            }
        }
        private void ReportCollecionFieldTypes(IProgressMessage progress, Tab tab, IFieldType fieldType)
        {
            FieldTypeReporter reporter;
            foreach (IFieldType fieldTypeChild in fieldType.CollectionFieldTypes())
            {
                reporter = new FieldTypeReporter(fieldTypeChild);
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
                if (fieldTypeChild.FieldDataType().ToString() == "Collection")
                    ReportCollecionFieldTypes(progress, tab, fieldTypeChild);
            }
        }
        private void ReportCollectionFieldsType(Tab tab, IProgressMessage progress, IField field, int modelingElementID)
        {

            if (field.FieldDataType.ToString() != "Collection")
                return;

            if (modelingElementID == -1)
                return;

            FieldTypeReporter reporter;

            ICollectionFieldListManager clfm = (ICollectionFieldListManager)field.GetValue(modelingElementID);
            foreach (IField fieldChild in clfm.SupportedFields())
            {
                reporter = new FieldTypeReporter(fieldChild);
                tab.Chidren.Add(reporter.ToJson());
                progress.Message = reporter.ToString();
            }
        }

        #endregion


    }
}
