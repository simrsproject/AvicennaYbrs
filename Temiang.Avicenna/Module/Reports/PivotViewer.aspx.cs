using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using PivotGridField = DevExpress.Web.ASPxPivotGrid.PivotGridField;
using System.IO;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Util;

using DevExpress.Export;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPrinting;


namespace Temiang.Avicenna.Module.Reports
{
    public partial class PivotViewer : BasePage
    {
        private DataTable GetDataSource()
        {
            DataTable dtb = (new ReportDataSource()).GetDataTable(AppSession.PrintJobReportID,
                                                                  AppSession.PrintJobParameters);
            return dtb;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pivotGrid.OptionsView.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;
            if (!IsPostBack)
            {
                AppProgram appProgram = new AppProgram();
                appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);

                this.Title = appProgram.ProgramName;
                InitializedPivotField();

                var appCp = new AppUserCustomPivot();
                if (appCp.LoadByPrimaryKey(AppSession.PrintJobReportID, AppSession.PrintCustomPivotID.ToInt()))
                {

                    pivotGrid.OptionsView.ShowColumnGrandTotals = appCp.IsShowColumnGrandTotals ?? false;
                    pivotGrid.OptionsView.ShowColumnTotals = appCp.IsShowColumnTotals ?? false;
                    pivotGrid.OptionsView.ShowRowGrandTotals = appCp.IsShowRowGrandTotals ?? false;
                    pivotGrid.OptionsView.ShowRowTotals = appCp.IsShowRowTotals ?? false;
                    pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = appCp.IsShowGrandTotalsForSingleValues ?? false;
                    pivotGrid.OptionsView.ShowTotalsForSingleValues = appCp.IsShowTotalsForSingleValues ?? false;
                }

                PopulateComboBox(appCp.SummaryType);
                SetOptionsViewCheckBoxes();
                SetDataFieldsProperties();
            }

            try
            {
                pivotGrid.DataSource = GetDataSource();
                pivotGrid.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InitializedPivotField()
        {
            AppReportPivotCollection collection = new AppReportPivotCollection();
            AppReportPivotQuery query = new AppReportPivotQuery();
            query.Where(query.ProgramID == AppSession.PrintJobReportID, query.CustomPivotID == AppSession.PrintCustomPivotID);
            query.OrderBy(query.PivotArea.Ascending, query.IndexNo.Ascending);
            collection.Load(query);

            pivotGrid.OptionsPager.RowsPerPage = 25;

            RadComboBox cboFieldName = (RadComboBox)Helper.FindControlRecursive(Page, "cboFieldName");
            var isGroupExist = false;
            foreach (AppReportPivot item in collection)
            {
                pivotGrid.Fields.AddField(GetPivotGridField(item));

                //List FieldName
                if (item.SummaryType > 0)
                    cboFieldName.Items.Add(new RadComboBoxItem(item.FieldCaption, item.FieldName));

                if (item.GroupIndex != null)
                {
                    isGroupExist = true;
                }
            }

            //if (isGroupExist)
            //{
            //    // Creates a PivotGridWebGroup instance.
            //    query = new AppReportPivotQuery();
            //    query.Where(query.ProgramID == AppSession.PrintJobReportID, query.CustomPivotID == AppSession.PrintCustomPivotID, query.GroupIndex.IsNotNull());
            //    query.OrderBy(query.GroupIndex.Ascending, query.IndexNo.Ascending);
            //    collection.Load(query);

            //    PivotGridWebGroup pivotGridWebGroup = null;
            //    int groupIndex = -1;
            //    foreach (AppReportPivot item in collection)
            //    {
            //        if (groupIndex != item.GroupIndex)
            //        {
            //            if (pivotGridWebGroup != null)
            //            {
            //                // Adds the created group to the collection of the ASPxPivotGrid groups.
            //                pivotGrid.Groups.Add(pivotGridWebGroup);
            //            }

            //            groupIndex = item.GroupIndex ?? 0;
            //            pivotGridWebGroup = new PivotGridWebGroup();
            //        }

            //        // Adds fields to the created group.
            //        pivotGridWebGroup.Fields.Add();
            //    }
            //}

        }

        private PivotGridField GetPivotGridField(esAppReportPivot item)
        {
            PivotGridField field = new PivotGridField();
            field.Caption = item.FieldCaption;
            field.FieldName = item.FieldName;
            field.GroupInterval = (PivotGroupInterval)item.GroupInterval;

            //0 RowArea
            //1 ColumnArea,
            //2 FilterArea,
            //3 DataArea
            field.Area = (PivotArea)item.PivotArea;

            //0 Count,
            //1 Sum,
            //2 Min,
            //3 Max,
            //4 Average,
            //5 StdDev,
            //6 StdDevp,
            //7 Var,
            //8 Varp,
            //9 Custom,
            //10 CountDistinct,
            //11 Median,
            //12 Mode
            field.SummaryType = (PivotSummaryType)item.SummaryType;
            field.AreaIndex = (int)item.IndexNo;

            if (!item.FormatString.Trim().Equals(string.Empty))
            {
                field.CellFormat.FormatType = (FormatType)item.FormatType;
                field.CellFormat.FormatString = item.FormatString;

                field.GrandTotalCellFormat.FormatType = (FormatType)item.FormatType;
                field.GrandTotalCellFormat.FormatString = item.FormatString;
            }

            if (field.SummaryType > 0)
                field.AllowedAreas = PivotGridAllowedAreas.DataArea | PivotGridAllowedAreas.FilterArea;
            else
                field.AllowedAreas = PivotGridAllowedAreas.ColumnArea | PivotGridAllowedAreas.FilterArea |
                                     PivotGridAllowedAreas.RowArea;

            //Year Filter
            if (field.GroupInterval == PivotGroupInterval.DateYear)
            {
                field.FilterValues.FilterType = PivotFilterType.Included;
                field.FilterValues.Add(DateTime.Now.Year);
            }
            return field;
        }

        private void SetOptionsViewCheckBoxes()
        {
            CheckBox chkShowColumnGrandTotals = (CheckBox)Helper.FindControlRecursive(Page, "chkShowColumnGrandTotals");
            CheckBox chkShowColumnTotals = (CheckBox)Helper.FindControlRecursive(Page, "chkShowColumnTotals");
            CheckBox chkShowRowGrandTotals = (CheckBox)Helper.FindControlRecursive(Page, "chkShowRowGrandTotals");
            CheckBox chkShowRowTotals = (CheckBox)Helper.FindControlRecursive(Page, "chkShowRowTotals");
            CheckBox chkShowGrandTotalsForSingleValues = (CheckBox)Helper.FindControlRecursive(Page, "chkShowGrandTotalsForSingleValues");
            CheckBox chkShowTotalsForSingleValues = (CheckBox)Helper.FindControlRecursive(Page, "chkShowTotalsForSingleValues");

            chkShowColumnGrandTotals.Checked = pivotGrid.OptionsView.ShowColumnGrandTotals;
            chkShowColumnTotals.Checked = pivotGrid.OptionsView.ShowColumnTotals;
            chkShowRowGrandTotals.Checked = pivotGrid.OptionsView.ShowRowGrandTotals;
            chkShowRowTotals.Checked = pivotGrid.OptionsView.ShowRowTotals;
            chkShowGrandTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowGrandTotalsForSingleValues;
            chkShowTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowTotalsForSingleValues;
        }


        private void PopulateComboBox(string summaryType)
        {
            RadComboBox cboSummaryType = (RadComboBox)Helper.FindControlRecursive(radMenu, "cboSummaryType");
            //SummaryType
            foreach (PivotSummaryType type in Enum.GetValues(typeof(PivotSummaryType)))
                if (!type.ToString().ToLower().Equals("custom"))
                    cboSummaryType.Items.Add(new RadComboBoxItem(type.ToString()));

            if (!string.IsNullOrWhiteSpace(summaryType))
                ComboBox.SelectedValue(cboSummaryType, summaryType);

            //ExportType
            RadComboBox cboExportType = (RadComboBox)Helper.FindControlRecursive(radMenu, "cboExportType");
            cboExportType.Items.Add(new RadComboBoxItem("Excel", "excel"));
            cboExportType.Items.Add(new RadComboBoxItem("Pdf", "pdf"));
            cboExportType.Items.Add(new RadComboBoxItem("Rtf", "rtf"));
            cboExportType.Items.Add(new RadComboBoxItem("Text", "text"));
            cboExportType.Items.Add(new RadComboBoxItem("Html", "html"));
            cboExportType.Items.Add(new RadComboBoxItem("Excel (Data Aware)", "eda"));
        }

        private void SetOptionsViewProperties()
        {
            CheckBox chkShowColumnGrandTotals = (CheckBox)Helper.FindControlRecursive(radMenu, "chkShowColumnGrandTotals");
            CheckBox chkShowColumnTotals = (CheckBox)Helper.FindControlRecursive(radMenu, "chkShowColumnTotals");
            CheckBox chkShowRowGrandTotals = (CheckBox)Helper.FindControlRecursive(radMenu, "chkShowRowGrandTotals");
            CheckBox chkShowRowTotals = (CheckBox)Helper.FindControlRecursive(Page, "chkShowRowTotals");
            CheckBox chkShowGrandTotalsForSingleValues = (CheckBox)Helper.FindControlRecursive(radMenu, "chkShowGrandTotalsForSingleValues");
            CheckBox chkShowTotalsForSingleValues = (CheckBox)Helper.FindControlRecursive(radMenu, "chkShowTotalsForSingleValues");


            pivotGrid.OptionsView.ShowColumnGrandTotals = chkShowColumnGrandTotals.Checked;
            pivotGrid.OptionsView.ShowColumnTotals = chkShowColumnTotals.Checked;
            pivotGrid.OptionsView.ShowRowGrandTotals = chkShowRowGrandTotals.Checked;
            pivotGrid.OptionsView.ShowRowTotals = chkShowRowTotals.Checked;
            pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = chkShowGrandTotalsForSingleValues.Checked;
            pivotGrid.OptionsView.ShowTotalsForSingleValues = chkShowTotalsForSingleValues.Checked;
        }

        private void SetDataFieldsProperties()
        {
            RadComboBox cboSummaryType = (RadComboBox)Helper.FindControlRecursive(Page, "cboSummaryType");

            foreach (PivotGridField field in pivotGrid.Fields)
            {
                if (field.Area == PivotArea.DataArea)
                {
                    field.HeaderStyle.Font.Bold = false;
                    field.HeaderStyle.HoverStyle.Font.Bold = false;
                    RadComboBoxItem item = cboSummaryType.Items[(int)field.SummaryType];
                    field.Caption =
                        string.Format("{0} ({1})", field.Caption.Split('(')[0], item.Text);
                }
            }
            if (SelectedDataField != null)
            {
                SelectedDataField.HeaderStyle.Font.Bold = true;
                SelectedDataField.HeaderStyle.HoverStyle.Font.Bold = true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        //private void ExportPivot(bool saveAs)
        //{
        //    RadComboBox cboExportType = (RadComboBox)Helper.FindControlRecursive(Page, "cboExportType");
        //    string format = cboExportType.Text;
        //    if (format == string.Empty) return;
        //    format = format.ToLower();
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        CheckBox chkPrintHeadersOnEveryPage = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintHeadersOnEveryPage");
        //        CheckBox chkPrintFilterHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintFilterHeaders");
        //        CheckBox chkPrintColumnHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintColumnHeaders");
        //        CheckBox chkPrintRowHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintRowHeaders");
        //        CheckBox chkPrintDataHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintDataHeaders");

        //        pivotGridExporter.OptionsPrint.PrintHeadersOnEveryPage = chkPrintHeadersOnEveryPage.Checked;
        //        pivotGridExporter.OptionsPrint.PrintFilterHeaders = chkPrintFilterHeaders.Checked
        //                                                                ? DefaultBoolean.True
        //                                                                : DefaultBoolean.False;
        //        pivotGridExporter.OptionsPrint.PrintColumnHeaders = chkPrintColumnHeaders.Checked
        //                                                                ? DefaultBoolean.True
        //                                                                : DefaultBoolean.False;
        //        pivotGridExporter.OptionsPrint.PrintRowHeaders = chkPrintRowHeaders.Checked
        //                                                             ? DefaultBoolean.True
        //                                                             : DefaultBoolean.False;
        //        pivotGridExporter.OptionsPrint.PrintDataHeaders = chkPrintDataHeaders.Checked
        //                                                              ? DefaultBoolean.True
        //                                                              : DefaultBoolean.False;

        //        string contentType = "", fileName = "";
        //        switch (format)
        //        {
        //            case "pdf":
        //                contentType = "application/pdf";
        //                fileName = "DataAnalisys.pdf";
        //                pivotGridExporter.ExportToPdf(stream);
        //                break;
        //            case "xls":
        //                contentType = "application/ms-excel";
        //                fileName = "DataAnalisys.xls";
        //                pivotGridExporter.ExportToXls(stream);
        //                break;
        //            case "mht":
        //                contentType = "multipart/related";
        //                fileName = "DataAnalisys.mht";
        //                pivotGridExporter.ExportToMht(stream, "utf-8", "Pivot", true);
        //                break;
        //            case "rtf":
        //                contentType = "text/enriched";
        //                fileName = "DataAnalisys.rtf";
        //                pivotGridExporter.ExportToRtf(stream);
        //                break;
        //            case "text":
        //                contentType = "text/plain";
        //                fileName = "DataAnalisys.txt";
        //                pivotGridExporter.ExportToText(stream);
        //                break;
        //            case "html":
        //                contentType = "text/html";
        //                fileName = "DataAnalisys.htm";
        //                pivotGridExporter.ExportToHtml(stream, "utf-8", "Pivot", true);
        //                break;
        //        }

        //        cboExportType.SelectedIndex = 0;

        //        byte[] buffer = stream.GetBuffer();

        //        string disposition = saveAs ? "attachment" : "inline";
        //        Response.Clear();
        //        Response.Buffer = false;
        //        Response.AppendHeader("Content-Type", contentType);
        //        Response.AppendHeader("Content-Transfer-Encoding", "binary");
        //        Response.AppendHeader("Content-Disposition", disposition + "; filename=" + fileName);
        //        Response.BinaryWrite(buffer);
        //        Response.End();
        //    }
        //}

        private void ExportPivot()
        {
            CheckBox chkPrintHeadersOnEveryPage = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintHeadersOnEveryPage");
            CheckBox chkPrintFilterHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintFilterHeaders");
            CheckBox chkPrintColumnHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintColumnHeaders");
            CheckBox chkPrintRowHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintRowHeaders");
            CheckBox chkPrintDataHeaders = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintDataHeaders");
            CheckBox checkCustomFormattedValuesAsText = (CheckBox)Helper.FindControlRecursive(Page, "checkCustomFormattedValuesAsText");

            foreach (PivotGridField field in pivotGrid.Fields)
            {
                if (field.ValueFormat != null && !string.IsNullOrEmpty(field.ValueFormat.FormatString))
                    field.UseNativeFormat = checkCustomFormattedValuesAsText.Checked ? DefaultBoolean.False : DefaultBoolean.True;
            }

            pivotGridExporter.OptionsPrint.PrintHeadersOnEveryPage = chkPrintHeadersOnEveryPage.Checked;
            pivotGridExporter.OptionsPrint.PrintFilterHeaders = chkPrintFilterHeaders.Checked
                                                                    ? DefaultBoolean.True
                                                                    : DefaultBoolean.False;
            pivotGridExporter.OptionsPrint.PrintColumnHeaders = chkPrintColumnHeaders.Checked
                                                                    ? DefaultBoolean.True
                                                                    : DefaultBoolean.False;
            pivotGridExporter.OptionsPrint.PrintRowHeaders = chkPrintRowHeaders.Checked
                                                                 ? DefaultBoolean.True
                                                                 : DefaultBoolean.False;
            pivotGridExporter.OptionsPrint.PrintDataHeaders = chkPrintDataHeaders.Checked
                                                                  ? DefaultBoolean.True
                                                                  : DefaultBoolean.False;


            CheckBox chkPrintColumnAreaOnEveryPage = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintColumnAreaOnEveryPage");
            CheckBox chkPrintRowAreaOnEveryPage = (CheckBox)Helper.FindControlRecursive(Page, "chkPrintRowAreaOnEveryPage");
            CheckBox chkMergeColumnFieldValues = (CheckBox)Helper.FindControlRecursive(Page, "chkMergeColumnFieldValues");
            CheckBox chkMergeRowFieldValues = (CheckBox)Helper.FindControlRecursive(Page, "chkMergeRowFieldValues");

            pivotGridExporter.OptionsPrint.PrintColumnAreaOnEveryPage = chkPrintColumnAreaOnEveryPage.Checked;
            pivotGridExporter.OptionsPrint.PrintRowAreaOnEveryPage = chkPrintRowAreaOnEveryPage.Checked;
            pivotGridExporter.OptionsPrint.MergeColumnFieldValues = chkMergeColumnFieldValues.Checked;
            pivotGridExporter.OptionsPrint.MergeRowFieldValues = chkMergeRowFieldValues.Checked;


            XlsxExportOptionsEx options;
            RadComboBox cboExportType = (RadComboBox)Helper.FindControlRecursive(Page, "cboExportType");

            AppProgram appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(AppSession.PrintJobReportID);
            string fileName = appProgram.ProgramName.Trim().Replace(" ", "");
            switch (cboExportType.SelectedValue)
            {
                case "pdf":
                    pivotGridExporter.ExportPdfToResponse(fileName, true);
                    break;
                case "excel":
                    options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                    pivotGridExporter.ExportXlsxToResponse(fileName, options, true);
                    break;
                case "mht":
                    pivotGridExporter.ExportMhtToResponse(fileName, "utf-8", appProgram.ProgramName.Trim(), true);
                    break;
                case "rtf":
                    pivotGridExporter.ExportRtfToResponse(fileName, true);
                    break;
                case "text":
                    fileName = "DataAnalisys.txt";
                    pivotGridExporter.ExportTextToResponse(fileName, true);
                    break;
                case "html":
                    pivotGridExporter.ExportHtmlToResponse(fileName, "utf-8", appProgram.ProgramName.Trim(), true, true);
                    break;
                case "eda":
                    CheckBox chkDaAllowGrouping = (CheckBox)Helper.FindControlRecursive(Page, "chkDaAllowGrouping");
                    CheckBox chkDaExportCellValuesAsText = (CheckBox)Helper.FindControlRecursive(Page, "chkDaExportCellValuesAsText");
                    CheckBox chkDaAllowFixedColumns = (CheckBox)Helper.FindControlRecursive(Page, "chkDaAllowFixedColumns");
                    CheckBox chkDaExportRawData = (CheckBox)Helper.FindControlRecursive(Page, "chkDaExportRawData");

                    options = new XlsxExportOptionsEx()
                    {
                        ExportType = DevExpress.Export.ExportType.DataAware,
                        AllowGrouping = chkDaAllowGrouping.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                        TextExportMode = chkDaExportCellValuesAsText.Checked ? TextExportMode.Text : TextExportMode.Value,
                        AllowFixedColumns = chkDaAllowFixedColumns.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                        AllowFixedColumnHeaderPanel = chkDaAllowFixedColumns.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                        RawDataMode = chkDaExportRawData.Checked
                    };
                    pivotGridExporter.ExportXlsxToResponse(fileName, options, true);
                    break;
            }
        }
        protected void chkShowRowGrandTotals_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void chkShowColumnTotals_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void chkShowColumnGrandTotals_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void chkShowRowTotals_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void chkShowGrandTotalsForSingleValues_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void chkShowTotalsForSingleValues_CheckedChanged(object sender, EventArgs e)
        {
            SetOptionsViewProperties();
        }

        protected void cboSummaryType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox cboSummaryType = (RadComboBox)o;

            if (SelectedDataField != null)
                SelectedDataField.SummaryType =
                    (PivotSummaryType)Convert.ToInt32(cboSummaryType.SelectedIndex);
            SetDataFieldsProperties();
        }
        private PivotGridField SelectedDataField
        {
            get
            {
                RadComboBox cboFieldName = (RadComboBox)Helper.FindControlRecursive(Page, "cboFieldName");

                if (cboFieldName.Text != null)
                    return pivotGrid.Fields[cboFieldName.SelectedValue];

                return null;
            }
        }

        #region save pivot
        protected void btnSavePivot_Click(object sender, EventArgs e)
        {
            SaveCurrentPivot();

        }
        private void SaveCurrentPivot()
        {
            //User Custom Pivot Header
            AppUserCustomPivot cutomPivot = new AppUserCustomPivot();
            cutomPivot.UserID = AppSession.UserLogin.UserID;
            cutomPivot.ProgramID = AppSession.PrintJobReportID;
            RadTextBox txtCustomPivotName = (RadTextBox)Helper.FindControlRecursive(Page, "txtCustomPivotName");
            cutomPivot.CustomPivotName = txtCustomPivotName.Text;

            //Detil Field
            AppReportPivotCollection collection = new AppReportPivotCollection();
            foreach (PivotGridField field in pivotGrid.Fields)
            {
                AppReportPivot item = collection.AddNew();
                PrepareAppReportPivot(item, field);
            }

            AppUserCustomPivotQuery cutomPivotQuery = new AppUserCustomPivotQuery();
            cutomPivotQuery.Select(cutomPivotQuery.CustomPivotID.Max().As("CustomPivotID"));
            cutomPivotQuery.Where(cutomPivotQuery.ProgramID == AppSession.PrintJobReportID);

            //Update
            using (esTransactionScope trans = new esTransactionScope())
            {
                DataTable dtb = cutomPivotQuery.LoadDataTable();
                int newCustomPivotID = Convert.ToInt32(dtb.Rows[0][0] == DBNull.Value ? 0 : dtb.Rows[0][0]) + 1;
                cutomPivot.CustomPivotID = newCustomPivotID;
                foreach (AppReportPivot item in collection)
                {
                    item.CustomPivotID = newCustomPivotID;
                }

                cutomPivot.Save();
                collection.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        private void PrepareAppReportPivot(AppReportPivot item, PivotGridField field)
        {
            item.UserID = AppSession.UserLogin.UserID;
            item.ProgramID = AppSession.PrintJobReportID;
            item.FieldCaption = field.Caption;
            item.FieldName = field.FieldName;
            item.GroupInterval = Convert.ToInt16(field.GroupInterval);
            item.PivotArea = Convert.ToInt16(field.Area);
            item.IndexNo = field.AreaIndex;
            item.SummaryType = Convert.ToInt16(field.SummaryType);
            if (!field.CellFormat.FormatString.Equals(string.Empty))
            {
                item.FormatType = Convert.ToInt16(field.CellFormat.FormatType);
                item.FormatString = field.CellFormat.FormatString;

                item.FormatType = Convert.ToInt16(field.GrandTotalCellFormat.FormatType);
                item.FormatString = field.GrandTotalCellFormat.FormatString;
            }

        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportPivot();
        }
    }
}