using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class PatientChildBirthHistCtl : BasePhrCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            // Refresh
            grdPatientChildBirthHist.DataSource = null;
            grdPatientChildBirthHist.Rebind();

        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            SavePatientChildBirthHist();
        }

        private GridColumn FindGridColumnByUniqueName(string uniqName)
        {
            return grdPatientChildBirthHist.Columns.FindByUniqueName(uniqName);
        }

        private void GridColumnDisplay(string fieldName, bool isEdited)
        {
            FindGridColumnByUniqueName(fieldName).Display = !isEdited;
            FindGridColumnByUniqueName(fieldName + "Edit").Display = isEdited;
        }

        protected override void OnSetReadOnly(bool isReadOnly, Patient pat, Registration reg)
        {
            var isEdited = !isReadOnly;
            GridColumnDisplay("ChildBirth", isEdited);
            GridColumnDisplay("Sex", isEdited);
            GridColumnDisplay("Helper", isEdited);
            GridColumnDisplay("Location", isEdited);
            GridColumnDisplay("HM", isEdited);
            GridColumnDisplay("BBL", isEdited);
            GridColumnDisplay("Complication", isEdited);
            GridColumnDisplay("Notes", isEdited);
            GridColumnDisplay("PregnanDuration", isEdited);
            GridColumnDisplay("BirthMethod", isEdited);

            // Refresh
            grdPatientChildBirthHist.Rebind();
        }

        #endregion


        #region Past Medical History
        private void SavePatientChildBirthHist()
        {
            using (var trans = new esTransactionScope())
            {
                // Delete all
                var coll = new PatientChildBirthHistoryCollection();
                coll.Query.Where(coll.Query.PatientID == PatientID);
                coll.LoadAll();
                coll.MarkAllAsDeleted();
                coll.Save();

                // Add
                coll = new PatientChildBirthHistoryCollection();
                var i = 1;
                foreach (GridDataItem item in grdPatientChildBirthHist.MasterTableView.Items)
                {
                    var txtChildBirth = ((RadTextBox)item.FindControl("txtChildBirth"));
                    if (!string.IsNullOrWhiteSpace(txtChildBirth.Text))
                    {
                        var partus = coll.AddNew();
                        partus.PatientID = PatientID;
                        partus.SequenceNo = i;

                        partus.ChildBirth = txtChildBirth.Text;

                        partus.Sex =  ((RadComboBox) item.FindControl("cboSex")).SelectedValue;

                        var txtHelper = ((RadTextBox) item.FindControl("txtHelper"));
                        partus.Helper = txtHelper.Text;

                        var txtLocation = ((RadTextBox) item.FindControl("txtLocation"));
                        partus.Location = txtLocation.Text;

                        partus.HM =  ((RadComboBox) item.FindControl("cboHM")).SelectedValue;

                        var txtBBL = ((RadTextBox) item.FindControl("txtBBL"));
                        partus.BBL = txtBBL.Text;

                        var txtComplication = ((RadTextBox) item.FindControl("txtComplication"));
                        partus.Complication = txtComplication.Text;

                        var txtNotes = ((RadTextBox) item.FindControl("txtNotes"));
                        partus.Notes = txtNotes.Text;

                        partus.SRBirthMethod = ((RadComboBox) item.FindControl("cboSRBirthMethod")).SelectedValue;

                        //partus.PregnanDurationMonth= ((RadNumericTextBox) item.FindControl("txtPregnanDurationMonth")).Value.ToInt();
                        partus.PregnanDurationWeek= ((RadNumericTextBox) item.FindControl("txtPregnanDurationWeek")).Value.ToInt();
                        partus.PregnanDurationDay= ((RadNumericTextBox)item.FindControl("txtPregnanDurationDay")).Value.ToInt();
                    }

                    i++;
                }

                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdPatientChildBirthHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = PatientChildBirthHistDataTable();
        }

        private DataTable PatientChildBirthHistDataTable()
        {
            var que = new PatientChildBirthHistoryQuery("a");
            var stdi = new AppStandardReferenceItemQuery("i");
            que.LeftJoin(stdi).On(que.SRBirthMethod == stdi.ItemID & stdi.StandardReferenceID == "BirthMethod");
            que.Where(que.PatientID == PatientID);
            que.Select(que, stdi.ItemName.As("BirthMethod"));
            var dtbPast = que.LoadDataTable();

            //// Buat 4 record
            //if (dtbPast.Rows.Count < 4)
            if (DataModeCurrent != AppEnum.DataMode.Read)
            {
                if (dtbPast.Rows.Count < 11) // RSMM
                {
                    //for (int i = dtbPast.Rows.Count; i < 4; i++)
                    for (int i = dtbPast.Rows.Count; i < 11; i++)
                    {
                        var newRow = dtbPast.NewRow();
                        newRow["SequenceNo"] = i + 1;
                        dtbPast.Rows.Add(newRow);
                    }
                }
            }

            return dtbPast;
        }
        protected void grdPatientChildBirthHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (IsReadMode)
                return;
            if (e.Item is GridDataItem)
            {
                var ditem = (GridDataItem) e.Item;
                PopulateTextBoxInDataItem(ditem, "ChildBirth");
                PopulateComboBoxInDataItem(ditem, "Sex");
                PopulateTextBoxInDataItem(ditem, "Helper");
                PopulateTextBoxInDataItem(ditem, "Location");
                PopulateComboBoxInDataItem(ditem, "HM");
                PopulateTextBoxInDataItem(ditem, "BBL");
                PopulateTextBoxInDataItem(ditem, "Complication");
                PopulateTextBoxInDataItem(ditem, "Notes");
                //PopulateNumericTextBoxInDataItem(ditem, "PregnanDurationMonth");
                PopulateNumericTextBoxInDataItem(ditem, "PregnanDurationWeek");
                PopulateNumericTextBoxInDataItem(ditem, "PregnanDurationDay");


                var cbo = ((RadComboBox)ditem.FindControl("cboSRBirthMethod"));
                StandardReference.InitializeIncludeSpace(cbo,AppEnum.StandardReference.BirthMethod);

                var value = ditem["SRBirthMethod"].Text;
                if (value == "&nbsp;")
                    value = string.Empty;
                ComboBox.SelectedValue(cbo,value);
            }
        }

        private static void PopulateTextBoxInDataItem(GridDataItem dataItem, string fieldName)
        {
            var txt = ((RadTextBox)dataItem.FindControl("txt" + fieldName));
            var value = dataItem[fieldName].Text;
            if (value == "&nbsp;")
                value = string.Empty;
            txt.Text = value;
        }
        private static void PopulateNumericTextBoxInDataItem(GridDataItem dataItem, string fieldName)
        {
            var txt = ((RadNumericTextBox)dataItem.FindControl("txt" + fieldName));
            var value = dataItem[fieldName].Text;
            if (value == "&nbsp;" || value=="0")
                txt.Text = string.Empty;
            else
                txt.Value = value.ToDouble();
        }

        private static void PopulateComboBoxInDataItem(GridDataItem dataItem, string fieldName)
        {
            var cbo = ((RadComboBox)dataItem.FindControl("cbo" + fieldName));
            var value = dataItem[fieldName].Text;
            if (value == "&nbsp;")
                value = string.Empty;
            ComboBox.SelectedValue(cbo,value);
        }
        #endregion

    }
}