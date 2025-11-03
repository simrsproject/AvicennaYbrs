using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using System.Data;
using System.Drawing;
using Temiang.Dal.Interfaces;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClinicalPathwayDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPathway;

            UrlPageSearch = "ClinicalPathwaySearch.aspx";
            UrlPageList = "ClinicalPathwayList.aspx";

            if (!IsCallback)
            {
                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemAndZatActive, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemGroup, Page);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PathwayItems;
        }

        private PathwayItemCollection PathwayItems
        {
            get
            {
                string sessName = "collPathwayItemCollection";
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null) return ((PathwayItemCollection)(obj));
                }

                var coll = new PathwayItemCollection();

                var item = new PathwayItemQuery("a");
                var pathway = new PathwayQuery("b");

                item.Select(
                    item.SelectAllExcept(item.AssesmentGroupName, item.AssesmentName, item.AssesmentHeaderName),
                    "<dbo.[fn_CleanAndTrim](a.AssesmentGroupName, ' ', ' ', 1) AS AssesmentGroupName>",
                    "<dbo.[fn_CleanAndTrim](a.AssesmentName, ' ', ' ', 1) AS AssesmentName>",
                    "<dbo.[fn_CleanAndTrim](a.AssesmentHeaderName, ' ', ' ', 1) AS AssesmentHeaderName>",
                    "<'' AS refTo_1>",
                    "<'' AS refTo_2>",
                    "<'' AS refTo_3>",
                    "<'' AS refTo_4>",
                    "<'' AS refTo_5>",
                    "<'' AS refTo_6>",
                    "<'' AS refTo_7>"
                    //,
                    //"<a.AssesmentHeaderName + ' ' + a.AssesmentGroupName AS GridGroupName>"
                    );
                item.InnerJoin(pathway).On(item.PathwayID == pathway.PathwayID);
                item.Where(item.PathwayID == txtPathwayID.Text);

                coll.Load(item);

                if (coll.Any())
                {
                    foreach (var entity in coll)
                    {
                        foreach (var exec in PathwayItemExecutions.Where(p => p.PathwayItemSeqNo == entity.PathwayItemSeqNo).OrderBy(p => p.DayNo))
                        {
                            if (exec.DayNo == 1) entity.col_1 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 2) entity.col_2 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 3) entity.col_3 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 4) entity.col_4 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 5) entity.col_5 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 6) entity.col_6 = exec.SRPathwayExecutionType;
                            if (exec.DayNo == 7) entity.col_7 = exec.SRPathwayExecutionType;
                        }
                    }
                }
                else
                {
                    var pie = PathwayItemExecutions;
                }

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = "collPathwayItemCollection";
                Session[sessName] = value;
            }
        }

        private PathwayItemExecutionCollection PathwayItemExecutions
        {
            get
            {
                string sessName = "collPathwayItemExecutionCollection";
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null) return ((PathwayItemExecutionCollection)(obj));
                }

                var coll = new PathwayItemExecutionCollection();

                var query = new PathwayItemExecutionQuery("a");
                var item = new PathwayItemQuery("b");

                query.Select(
                    query,
                    //item.AssesmentGroupName.As("refToPathwayItem_AssesmentGroupName"),
                    //item.AssesmentName.As("refToPathwayItem_AssesmentName"),
                    //item.AssesmentHeaderName.As("refToPathwayItem_AssesmentHeaderName"),
                    "<dbo.[fn_CleanAndTrim](b.AssesmentGroupName, ' ', ' ', 1) AS refToPathwayItem_AssesmentGroupName>",
                    "<dbo.[fn_CleanAndTrim](b.AssesmentName, ' ', ' ', 1) AS refToPathwayItem_AssesmentName>",
                    "<dbo.[fn_CleanAndTrim](b.AssesmentHeaderName, ' ', ' ', 1) AS refToPathwayItem_AssesmentHeaderName>"
                    );
                query.InnerJoin(item).On(query.PathwayID == item.PathwayID && query.PathwayItemSeqNo == item.PathwayItemSeqNo);
                query.Where(item.PathwayID == txtPathwayID.Text);
                query.OrderBy(item.AssesmentHeaderName.Ascending, item.AssesmentGroupName.Ascending, item.AssesmentName.Ascending);
                coll.Load(query);

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = "collPathwayItemExecutionCollection";
                Session[sessName] = value;
            }
        }

        private void RefreshCommandItemPathwayItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdList.Columns[0].Visible = isVisible;
            grdList.Columns[grdList.Columns.Count - 1].Visible = isVisible;

            grdList.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdList.Rebind();
        }

        private void PopulatePathwayItemGrid()
        {
            //Display Data Detail
            PathwayItems = null; //Reset Record Detail
            PathwayItemExecutions = null;
            grdList.DataSource = PathwayItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var pathwayID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PathwayItemMetadata.ColumnNames.PathwayID].ToString();
            var pathwayItemSeqNo = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PathwayItemMetadata.ColumnNames.PathwayItemSeqNo].ToInt();

            var entity = FindPathwayItem(pathwayID, pathwayItemSeqNo);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var pathwayID = item.OwnerTableView.DataKeyValues[item.ItemIndex][PathwayItemMetadata.ColumnNames.PathwayID].ToString();
            var pathwayItemSeqNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][PathwayItemMetadata.ColumnNames.PathwayItemSeqNo].ToInt();
            var entity = FindPathwayItem(pathwayID, pathwayItemSeqNo);
            if (entity != null)
            {
                entity.MarkAsDeleted();

                foreach (var exec in PathwayItemExecutions.Where(p => p.PathwayID == pathwayID && p.PathwayItemSeqNo == pathwayItemSeqNo))
                {
                    exec.MarkAsDeleted();
                }
            }
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var lastPathwayItemSeqNo = 0;
            if (PathwayItems.Count > 0) lastPathwayItemSeqNo = PathwayItems[PathwayItems.Count - 1].PathwayItemSeqNo.ToInt();

            var entity = PathwayItems.AddNew();
            if (!string.IsNullOrEmpty(txtPathwayID.Text)) entity.PathwayID = txtPathwayID.Text;
            entity.PathwayItemSeqNo = lastPathwayItemSeqNo + 1;

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdList.Rebind();
        }

        private PathwayItem FindPathwayItem(string pathwayID, int? pathwayItemSeqNo)
        {
            var coll = PathwayItems;
            return coll.FirstOrDefault(rec => rec.PathwayID == pathwayID && rec.PathwayItemSeqNo == pathwayItemSeqNo);
        }

        private void SetEntityValue(PathwayItem entity, GridCommandEventArgs e)
        {
            var userControl = (ClinicalPathwayItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.AssesmentGroupName = Regex.Replace(userControl.AssesmentGroupName, @"\t|\n|\r", " ");
                entity.AssesmentName = Regex.Replace(userControl.AssesmentName, @"\t|\n|\r", " ");
                entity.CoverageValue1 = userControl.CoverageValue1;
                entity.CoverageValue2 = userControl.CoverageValue2;
                entity.CoverageValue3 = userControl.CoverageValue3;
                entity.Notes = userControl.Notes;
                entity.IsActive = userControl.IsActive;
                entity.col_1 = 1 <= txtALOS.Value ? userControl.Day1 : string.Empty;
                entity.col_2 = 2 <= txtALOS.Value ? userControl.Day2 : string.Empty;
                entity.col_3 = 3 <= txtALOS.Value ? userControl.Day3 : string.Empty;
                entity.col_4 = 4 <= txtALOS.Value ? userControl.Day4 : string.Empty;
                entity.col_5 = 5 <= txtALOS.Value ? userControl.Day5 : string.Empty;
                entity.col_6 = 6 <= txtALOS.Value ? userControl.Day6 : string.Empty;
                entity.col_7 = 7 <= txtALOS.Value ? userControl.Day7 : string.Empty;
                entity.AssesmentHeaderName = Regex.Replace(userControl.AssesmentHeaderName, @"\t|\n|\r", " ");

                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 1, entity.col_1, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 2, entity.col_2, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 3, entity.col_3, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 4, entity.col_4, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 5, entity.col_5, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 6, entity.col_6, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
                SetEntityValue(entity.PathwayID, entity.PathwayItemSeqNo, 7, entity.col_7, Regex.Replace(entity.AssesmentHeaderName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentGroupName, @"\t|\n|\r", " "), Regex.Replace(entity.AssesmentName, @"\t|\n|\r", " "));
            }
        }

        private void SetEntityValue(string pathwayID, int? pathwayItemSeqNo, int dayNo, string sRPathwayExecutionType, string assesmentHeaderName, string assesmentGroupName, string assesmentName)
        {
            var exec = PathwayItemExecutions.FindByPrimaryKey(pathwayID, pathwayItemSeqNo.Value, dayNo);
            if (exec == null)
            {
                var pie = PathwayItemExecutions.AddNew();
                pie.PathwayID = txtPathwayID.Text;
                pie.PathwayItemSeqNo = pathwayItemSeqNo;
                pie.DayNo = dayNo;
                pie.SRPathwayExecutionType = sRPathwayExecutionType;
                pie.IsActive = true;
                pie.AssesmentHeaderName = Regex.Replace(assesmentHeaderName, @"\t|\n|\r", " ");
                pie.AssesmentGroupName = Regex.Replace(assesmentGroupName, @"\t|\n|\r", " ");
                pie.AssesmentName = Regex.Replace(assesmentName, @"\t|\n|\r", " ");
            }
            else exec.SRPathwayExecutionType = sRPathwayExecutionType;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtPathwayID.ReadOnly = (newVal != AppEnum.DataMode.New);

            RefreshCommandItemPathwayItem(newVal);
            RefreshCommandItemPathwayDiagnoseItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Pathway();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey((String)parameters[0]);
                else entity.LoadByPrimaryKey(txtPathwayID.Text);
            }
            else entity.LoadByPrimaryKey(txtPathwayID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pathway = (Pathway)entity;

            txtPathwayID.Text = pathway.PathwayID;
            txtPathwayName.Text = pathway.PathwayName;
            txtStartingDate.SelectedDate = pathway.StartingDate;

            txtALOS.Value = pathway.ALOS;
            txtClass1.Value = Convert.ToDouble(pathway.CoverageValue1);
            txtClass2.Value = Convert.ToDouble(pathway.CoverageValue2);
            txtClass3.Value = Convert.ToDouble(pathway.CoverageValue3);
            txtNotes.Text = pathway.Notes;
            chkIsActive.Checked = pathway.IsActive ?? false;

            PopulatePathwayItemGrid();
            PopulatePathwayDiagnoseItemGrid();
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new PathwayQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PathwayID > txtPathwayID.Text);
                que.OrderBy(que.PathwayID.Ascending);
            }
            else
            {
                que.Where(que.PathwayID < txtPathwayID.Text);
                que.OrderBy(que.PathwayID.Descending);
            }
            var entity = new Pathway();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnMenuNewClick()
        {
            txtStartingDate.SelectedDate = DateTime.Now.Date;

            OnPopulateEntryControl(new Pathway());

            var diag = new DiagnoseQuery();
            diag.Select(diag.DiagnoseID, diag.DiagnoseName);
            diag.Where(diag.DiagnoseID == string.Empty);

            txtALOS.Value = 7;
            txtClass1.Value = 0;
            txtClass2.Value = 0;
            txtClass3.Value = 0;
            chkIsActive.Checked = true;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //Is it a GridDataItem
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                if (dataBoundItem["col_1"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_1"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_1"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_1"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_1"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_1"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_1"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_1"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_1"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_1"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_2"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_2"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_2"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_2"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_2"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_2"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_2"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_2"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    //dataBoundItem["col_2"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_2"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_3"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_3"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_3"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_3"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_3"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_3"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_3"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_3"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_3"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_3"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_4"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_4"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_4"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_4"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_4"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_4"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_4"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_4"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_4"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_4"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_5"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_5"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_5"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_5"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_5"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_5"].Text == "02")
                {

                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_5"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_5"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    //dataBoundItem["col_5"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_5"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_6"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_6"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_6"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_6"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_6"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_6"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_6"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_6"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_6"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_6"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }

                if (dataBoundItem["col_7"].Text == "01")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "01"))
                        dataBoundItem["col_7"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_7"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_7"].ForeColor = Color.Red; // chanmge particuler cell
                    //dataBoundItem["col_7"].BackColor = Color.Red;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
                else if (dataBoundItem["col_7"].Text == "02")
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey(prColor.StandardReferenceID = "ClinicalPathway", prColor.ItemID = "02"))
                        dataBoundItem["col_7"].ForeColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);
                    dataBoundItem["col_7"].BackColor = System.Drawing.ColorTranslator.FromHtml(prColor.ItemName);

                    //dataBoundItem["col_7"].ForeColor = Color.Yellow; // chanmge particuler cell
                    //dataBoundItem["col_7"].BackColor = Color.Yellow;
                    ////e.Item.BackColor = Color.Red; // for whole row
                    ////dataItem.CssClass = "MyMexicoRowClass";
                }
            }
        }

        private void SetEntityValue(Pathway p)
        {
            p.PathwayID = txtPathwayID.Text;
            p.PathwayName = txtPathwayName.Text;
            p.StartingDate = txtStartingDate.SelectedDate.Value.Date;
            p.CoverageValue1 = Convert.ToDecimal(txtClass1.Value);
            p.CoverageValue2 = Convert.ToDecimal(txtClass2.Value);
            p.CoverageValue3 = Convert.ToDecimal(txtClass3.Value);
            p.ALOS = Convert.ToInt32(txtALOS.Value);
            p.Notes = txtNotes.Text;
            p.IsActive = chkIsActive.Checked;
            p.LastUpdateByUserID = AppSession.UserLogin.UserID;
            p.LastUpdateDateTime = DateTime.Now;

            foreach (var e in PathwayDiagnoseItems)
            {
                e.PathwayID = p.PathwayID;
                e.LastUpdateByUserID = AppSession.UserLogin.UserID;
                e.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var e in PathwayItems)
            {
                e.PathwayID = p.PathwayID;
                e.LastUpdateByUserID = AppSession.UserLogin.UserID;
                e.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var e in PathwayItemExecutions)
            {
                e.PathwayID = p.PathwayID;
                e.LastUpdateByUserID = AppSession.UserLogin.UserID;
                e.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Pathway entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                PathwayItems.Save();

                //foreach (var e in PathwayItems)
                //{
                //    foreach (var n in PathwayItemExecutions.Where(p => p.AssesmentGroupName == e.AssesmentGroupName && p.AssesmentName == e.AssesmentName))
                //    {
                //        n.PathwayItemSeqNo = e.PathwayItemSeqNo;
                //    }
                //}

                PathwayItemExecutions.Save();
                PathwayDiagnoseItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new Pathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
            {
                entity.MarkAsDeleted();

                PathwayItems.MarkAllAsDeleted();
                PathwayItemExecutions.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    PathwayItemExecutions.Save();
                    PathwayItems.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new Pathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Pathway();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Pathway();
            if (entity.LoadByPrimaryKey(txtPathwayID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PathwayID='{0}'", txtPathwayID.Text.Trim());
            auditLogFilter.TableName = "Pathway";
        }

        protected void grdDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDiagnose.DataSource = PathwayDiagnoseItems;
        }

        private PathwayDiagnoseItemCollection PathwayDiagnoseItems
        {
            get
            {
                string sessName = "collPathwayDiagnoseItemCollection";
                if (IsPostBack)
                {
                    object obj = Session[sessName];
                    if (obj != null) return ((PathwayDiagnoseItemCollection)(obj));
                }

                var coll = new PathwayDiagnoseItemCollection();

                var item = new PathwayDiagnoseItemQuery("a");
                var diag = new DiagnoseQuery("b");

                item.Select(
                    item,
                    diag.DiagnoseName.As("refToDiagnose_DiagnoseName")
                    );
                item.InnerJoin(diag).On(item.DiagnoseID == diag.DiagnoseID);
                item.Where(item.PathwayID == txtPathwayID.Text);

                coll.Load(item);

                Session[sessName] = coll;
                return coll;
            }
            set
            {
                string sessName = "collPathwayDiagnoseItemCollection";
                Session[sessName] = value;
            }
        }

        private void RefreshCommandItemPathwayDiagnoseItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDiagnose.Columns[0].Visible = isVisible;
            grdDiagnose.Columns[grdDiagnose.Columns.Count - 1].Visible = isVisible;

            grdDiagnose.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdDiagnose.Rebind();
        }

        private void PopulatePathwayDiagnoseItemGrid()
        {
            //Display Data Detail
            PathwayDiagnoseItems = null; //Reset Record Detail
            grdDiagnose.DataSource = PathwayDiagnoseItems; //Requery
            grdDiagnose.MasterTableView.IsItemInserted = false;
            grdDiagnose.MasterTableView.ClearEditItems();
            grdDiagnose.DataBind();
        }

        protected void grdDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var diagnoseID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID]);

            var entity = FindPathwayDiagnoseItem(diagnoseID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var diagnoseID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID]);

            var entity = FindPathwayDiagnoseItem(diagnoseID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
            }
        }

        protected void grdDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PathwayDiagnoseItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDiagnose.Rebind();
        }

        private PathwayDiagnoseItem FindPathwayDiagnoseItem(string diagnoseID)
        {
            var coll = PathwayDiagnoseItems;
            return coll.FirstOrDefault(rec => rec.DiagnoseID.Equals(diagnoseID));
        }

        private void SetEntityValue(PathwayDiagnoseItem entity, GridCommandEventArgs e)
        {
            var userControl = (ClinicalPathwayDiagnoseItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PathwayID = txtPathwayID.Text;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
            }
        }

        protected void cboLoadFromData_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var diag = new PathwayQuery();
            diag.es.Top = 20;
            diag.Select(diag.PathwayID, diag.PathwayName);
            diag.Where(
                diag.Or
                    (
                        diag.PathwayID.Like(searchText),
                        diag.PathwayName.Like(searchText)
                    )
                );
            diag.OrderBy(diag.PathwayID.Ascending);

            cboLoadFromData.DataSource = diag.LoadDataTable();
            cboLoadFromData.DataBind();
        }

        protected void cboLoadFromData_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Format("({0}) {1}", ((DataRowView)e.Item.DataItem)["PathwayID"].ToString(), ((DataRowView)e.Item.DataItem)["PathwayName"].ToString());
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PathwayID"].ToString();
        }

        protected void imgLoad_Click(object sender, ImageClickEventArgs e)
        {
            HideInformationHeader();
            if (string.IsNullOrEmpty(txtPathwayID.Text))
            {
                ShowInformationHeader("Pathway ID required.");
                return;
            }

            if (string.IsNullOrEmpty(cboLoadFromData.SelectedValue)) return;

            var path = new Pathway();
            if (!path.LoadByPrimaryKey(cboLoadFromData.SelectedValue)) return;

            var diags = new PathwayDiagnoseItemCollection();
            diags.Query.Where(diags.Query.PathwayID == path.PathwayID);
            if (diags.Query.Load())
            {
                foreach (var diag in diags)
                {
                    diag.es.RowState = DataRowState.Added;
                    diag.PathwayID = txtPathwayID.Text;
                    PathwayDiagnoseItems.AttachEntity(diag);
                }
            }
            grdDiagnose.Rebind();

            var items = new PathwayItemCollection();
            items.Query.Where(items.Query.PathwayID == path.PathwayID);
            if (items.Query.Load())
            {
                foreach (var item in items)
                {
                    //item.es.RowState = DataRowState.Added;
                    //item.PathwayID = txtPathwayID.Text;
                    //PathwayItems.AttachEntity(item);

                    var item2 = PathwayItems.AddNew();
                    item2.PathwayID = txtPathwayID.Text;
                    item2.PathwayItemSeqNo = item.PathwayItemSeqNo;
                    item2.ItemID = item.ItemID;
                    item2.AssesmentGroupName = item.AssesmentGroupName;
                    item2.AssesmentName = item.AssesmentName;
                    item2.CoverageValue1 = item.CoverageValue1;
                    item2.CoverageValue2 = item.CoverageValue2;
                    item2.CoverageValue3 = item.CoverageValue3;
                    item2.Notes = item.Notes;
                    item2.IsActive = item.IsActive;
                    item2.LastUpdateDateTime = DateTime.Now;
                    item2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item2.AssesmentHeaderName = item.AssesmentHeaderName;

                    var execs = new PathwayItemExecutionCollection();
                    execs.Query.Where(execs.Query.PathwayID == path.PathwayID, execs.Query.PathwayItemSeqNo == item.PathwayItemSeqNo);
                    execs.Query.Load();
                    foreach (var exec in execs)
                    {
                        //exec.es.RowState = DataRowState.Added;
                        //exec.PathwayID = txtPathwayID.Text;
                        //PathwayItemExecutions.AttachEntity(exec);

                        var pie = PathwayItemExecutions.AddNew();
                        pie.PathwayID = txtPathwayID.Text;
                        pie.PathwayItemSeqNo = item2.PathwayItemSeqNo;
                        pie.DayNo = exec.DayNo;

                        if (pie.DayNo == 1) item2.col_1 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 2) item2.col_2 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 3) item2.col_3 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 4) item2.col_4 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 5) item2.col_5 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 6) item2.col_6 = exec.SRPathwayExecutionType;
                        if (pie.DayNo == 7) item2.col_7 = exec.SRPathwayExecutionType;

                        pie.SRPathwayExecutionType = exec.SRPathwayExecutionType;
                        pie.IsActive = exec.IsActive;
                        pie.LastUpdateDateTime = DateTime.Now;
                        pie.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        pie.AssesmentHeaderName = item.AssesmentHeaderName;
                        pie.AssesmentGroupName = item.AssesmentGroupName;
                        pie.AssesmentName = item.AssesmentName;
                    }
                }
            }
            grdList.Rebind();
        }

        protected void imgClear_Click(object sender, ImageClickEventArgs e)
        {
            PathwayDiagnoseItems.MarkAllAsDeleted();
            grdDiagnose.Rebind();

            PathwayItems.MarkAllAsDeleted();
            grdList.Rebind();
        }
    }
}