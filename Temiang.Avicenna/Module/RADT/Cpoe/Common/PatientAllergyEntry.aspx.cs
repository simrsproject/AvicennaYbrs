using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.Configuration;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientAllergyEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Page.Validate();

            using (var trans = new esTransactionScope())
            {
                try
                {
                    var all = new PatientAllergyCollection();
                    all.Query.Where(all.Query.PatientID == PatientID);
                    all.LoadAll();
                    all.MarkAllAsDeleted();
                    all.Save();

                    all = new PatientAllergyCollection();

                    foreach (GridDataItem item in grdPatientAllergy.MasterTableView.Items)
                    {
                        string desc = ((RadTextBox)item.FindControl("txtAllergenDesc"))?.Text.Trim();
                        if (!string.IsNullOrEmpty(desc))
                        {
                            var allergy = all.AddNew();
                            allergy.AllergyGroup = item["StandardReferenceID"].Text;
                            allergy.Allergen = item["ItemID"].Text;
                            allergy.AllergenName = item["ItemName"].Text;
                            allergy.SRAnaphylaxis = item["ItemID"].Text;
                            allergy.Anaphylaxis = item["ItemID"].Text;
                            allergy.PatientID = PatientID;
                            allergy.DescAndReaction = desc;

                            var alDate = (RadDatePicker)item.FindControl("txtAllergenDate");
                            if (alDate != null && alDate.SelectedDate.HasValue)
                            {
                                allergy.AllergenDate = alDate.SelectedDate.Value;
                            }
                            else
                            {
                                allergy.AllergenDate = null;
                            }
                        }
                    }

                    foreach (GridDataItem item in grdPatientAllergySS.MasterTableView.Items)
                    {
                        string desc = ((RadTextBox)item.FindControl("txtAllergenDesc"))?.Text.Trim();
                        if (!string.IsNullOrEmpty(desc))
                        {
                            var allergy = all.AddNew();
                            allergy.AllergyGroup = item["StandardReferenceID"].Text;

                            var cboDrugAllergy = (RadComboBox)item.FindControl("cboDrugAllergy");
                            string selectedValue = cboDrugAllergy?.SelectedValue;
                            string fullText = cboDrugAllergy?.Text;
                            string zatAktif = "";

                            if (!string.IsNullOrEmpty(fullText))
                            {
                                int index = fullText.IndexOf("zat_aktif:");
                                if (index != -1)
                                {
                                    zatAktif = fullText.Substring(index + "zat_aktif:".Length).Trim();
                                }
                            }

                            allergy.Allergen = selectedValue;   // kfa_code
                            allergy.AllergenName = zatAktif;    // hanya zat_aktif
                            allergy.SRAnaphylaxis = item["ItemID"].Text;
                            allergy.Anaphylaxis = item["ItemID"].Text;
                            allergy.PatientID = PatientID;
                            allergy.DescAndReaction = desc;

                            allergy.SRAllergyCategory = item["ItemName"].Text;
                            allergy.SRAllergyVerificationStatus = ((RadComboBox)item.FindControl("cboAllergyVerif"))?.Text;
                            allergy.SRAllergyClinicalStatus = ((RadComboBox)item.FindControl("cboAllergyStatus"))?.Text;

                            var alDate = (RadDatePicker)item.FindControl("txtAllergenDate");
                            if (alDate != null && alDate.SelectedDate.HasValue)
                            {
                                allergy.AllergenDate = alDate.SelectedDate.Value;
                            }
                            else
                            {
                                allergy.AllergenDate = null;
                            }
                        }
                    }

                    all.Save();
                    trans.Complete();
                }
                catch (Exception ex)
                {
                    // Log the exception and handle it appropriately
                    throw;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            IsSingleRecordMode = true; //Save then close

            // Program Fiture berguna jika non SingleRecordMode
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // ------------------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Allergy List for : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                grdPatientAllergy.Rebind();
                grdPatientAllergySS.Rebind();

                //hfSatuSehatOrganizationID.Value = ConfigurationManager.AppSettings["SatuSehatOrganizationID"];
                hfSatuSehatOrganizationID.Value = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatOrganizationID);
            }

        }

        private DataTable AllergyTable(DataTable table)
        {
            var tbl = new DataTable();

            tbl.Columns.Add("Group", typeof(string));
            tbl.Columns.Add("StandardReferenceID", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("DescAndReaction", typeof(string));
            tbl.Columns.Add("AllergenDate", typeof(DateTime));
            tbl.Columns.Add("Allergen", typeof(string));
            tbl.Columns.Add("AllergenName", typeof(string));
            tbl.Columns.Add("SRAllergyClinicalStatus", typeof(string));
            tbl.Columns.Add("SRAllergyVerificationStatus", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                tbl.Rows.Add(
                    WordProcessing((string)row[0]),
                    row[0],
                    row[1],
                    row[2],
                    string.Empty,
                    DBNull.Value,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
                );
            }

            return tbl;
        }

        protected void grdPatientAllergy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //allergy data
            var allergyCollection = new PatientAllergyCollection();
            allergyCollection.Query.Where(allergyCollection.Query.PatientID == PatientID);
            allergyCollection.Query.OrderBy(allergyCollection.Query.AllergyGroup.Ascending);
            allergyCollection.LoadAll();

            var query = new AppStandardReferenceItemQuery("a");
            query.Select(query.StandardReferenceID, query.ItemID, query.ItemName);
            query.Where(query.ReferenceID == AppEnum.StandardReference.PatientHealthRecord, query.ItemID != "NoAllergen-001");

            DataTable tbl = AllergyTable(query.LoadDataTable());

            foreach (DataRow row in tbl.Rows)
            {
                foreach (var all in allergyCollection)
                {
                    if (row["StandardReferenceID"].ToString() == all.AllergyGroup && row["ItemID"].ToString() == all.Allergen)
                    {
                        row["DescAndReaction"] = all.DescAndReaction;
                        row["AllergenDate"] = all.AllergenDate.HasValue ? (object)all.AllergenDate.Value : DBNull.Value;
                        break;
                    }
                }
            }

            tbl.AcceptChanges();
            grdPatientAllergy.DataSource = tbl;
        }

        protected void grdPatientAllergySS_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var allergyCollection = new PatientAllergyCollection();
            allergyCollection.Query.Where(allergyCollection.Query.PatientID == PatientID);
            allergyCollection.Query.OrderBy(allergyCollection.Query.AllergyGroup.Ascending);
            allergyCollection.LoadAll();

            var query = new AppStandardReferenceItemQuery("a");
            query.Select(query.StandardReferenceID, query.ItemID, query.ItemName);
            query.Where(query.ReferenceID == AppEnum.StandardReference.AllergySatuSehat);

            DataTable tbl = AllergyTable(query.LoadDataTable());

            foreach (DataRow row in tbl.Rows)
            {
                foreach (var all in allergyCollection)
                {
                    if (row["StandardReferenceID"].ToString() == all.AllergyGroup && row["ItemID"].ToString() == all.Allergen)
                    {
                        row["AllergenName"] = all.AllergenName;
                        row["DescAndReaction"] = all.DescAndReaction;
                        row["SRAllergyClinicalStatus"] = all.SRAllergyClinicalStatus;
                        row["SRAllergyVerificationStatus"] = all.SRAllergyVerificationStatus;
                        row["AllergenDate"] = all.AllergenDate.HasValue ? (object)all.AllergenDate.Value : DBNull.Value;
                        break;
                    }
                }
            }

            tbl.AcceptChanges();
            grdPatientAllergySS.DataSource = tbl;
        }

        protected void grdPatientAllergySS_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem item)
            {
                var dataItem = (DataRowView)item.DataItem;

                var cboAllergyVerif = (RadComboBox)item.FindControl("cboAllergyVerif");
                var cboAllergyStatus = (RadComboBox)item.FindControl("cboAllergyStatus");

                FillRadComboBox(cboAllergyVerif, AppEnum.StandardReference.AllergyVerificationStatus);
                FillRadComboBox(cboAllergyStatus, AppEnum.StandardReference.AllergyClinicalStatus);

                if (cboAllergyVerif != null)
                {
                    cboAllergyVerif.SelectedValue = dataItem["SRAllergyVerificationStatus"].ToString();
                }

                if (cboAllergyStatus != null)
                {
                    cboAllergyStatus.SelectedValue = dataItem["SRAllergyClinicalStatus"].ToString();
                }
            }
        }

        private void FillRadComboBox(RadComboBox comboBox, AppEnum.StandardReference referenceType)
        {
            var referenceCollection = new AppStandardReferenceItemCollection();
            referenceCollection.Query.Where(referenceCollection.Query.StandardReferenceID == referenceType);
            referenceCollection.LoadAll();

            comboBox.Items.Clear();

            foreach (var referenceItem in referenceCollection)
            {
                comboBox.Items.Add(new RadComboBoxItem(referenceItem.ItemName, referenceItem.ItemID.ToString()));
            }

            comboBox.ClearSelection();
        }

        private string WordProcessing(string value)
        {
            string capital = string.Empty;
            int index = 0;
            foreach (char c in value)
            {
                if (Char.IsUpper(c) && index > 0)
                {
                    capital = c.ToString();
                    break;
                }

                index++;
            }

            if (!capital.Equals(string.Empty))
                return value.Insert(index, " ");
            else
                return value;
        }

    }
}
