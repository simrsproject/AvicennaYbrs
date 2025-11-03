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

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{  /* perubahan untuk rsmmp -- diky */
    public partial class MedHist2Ctl : BaseAssessmentCtl
    {
        private const string PastMedHist = "PastMedHist";
        private const string FamilyMedHist = "FamilyMedHist";

        public string GrdFamilyMedicalHistClientID
        {
            get
            {
                return grdFamilyMedicalHist.ClientID;
            }
        }
        public string GrdPastMedicalHistClientID
        {
            get
            {
                return grdPastMedicalHist.ClientID;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.Anamnesis; }
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            //var surgicalHist = new PastSurgicalHistory();
            //surgicalHist.LoadByPrimaryKey(PatientID);
            //txtSurgicalHistory.Text = surgicalHist.SurgicalHistory;

            //var transfusionHist = new PastTransfusionHistory();
            //transfusionHist.LoadByPrimaryKey(PatientID);
            //txtYearOfTransfusion.Text = transfusionHist.Year;
            //txtAllergicReactionOfTransfusion.Text = transfusionHist.AllergicReaction;

            //txtJobHistNotes.Text = assessment.JobHistNotes;

            // Refresh
            grdFamilyMedicalHist.Rebind();
            grdPastMedicalHist.Rebind();
            grdPatientAllergy.Rebind();

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            SavePastMedicalHist();
            SaveFamilyMedicalHist();

            //var surgicalHist = new PastSurgicalHistory();
            //if (!surgicalHist.LoadByPrimaryKey(PatientID))
            //    surgicalHist.PatientID = PatientID;
            //surgicalHist.SurgicalHistory = txtSurgicalHistory.Text;
            //surgicalHist.Save();

            //var transfusionHist = new PastTransfusionHistory();
            //if (!transfusionHist.LoadByPrimaryKey(PatientID))
            //    transfusionHist.PatientID = PatientID;
            //transfusionHist.Year = txtYearOfTransfusion.Text;
            //transfusionHist.AllergicReaction = txtAllergicReactionOfTransfusion.Text;
            //transfusionHist.Save();

            SavePatientAllergy();

            //assessment.JobHistNotes = txtJobHistNotes.Text;
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            grdPastMedicalHist.Columns[0].Display = isEdited; // Selected
            grdPastMedicalHist.Columns[1].Display = !isEdited; // IsSelected
            grdPastMedicalHist.Columns[3].Display = !isEdited; // Notes
            grdPastMedicalHist.Columns[4].Display = isEdited; // Notes Edit

            grdFamilyMedicalHist.Columns[0].Display = isEdited;
            grdFamilyMedicalHist.Columns[1].Display = !isEdited; // IsSelected
            grdFamilyMedicalHist.Columns[3].Display = !isEdited;
            grdFamilyMedicalHist.Columns[4].Display = isEdited;

            // Refresh
            grdFamilyMedicalHist.Rebind();
            grdPastMedicalHist.Rebind();
            grdPatientAllergy.Rebind();
        }
        #endregion


        #region Past Medical History
        private void SavePastMedicalHist()
        {
            using (var trans = new esTransactionScope())
            {
                // PastMedicalHistory
                var medColl = new PastMedicalHistoryCollection();
                if (IsEdited)
                {
                    medColl.Query.Where(medColl.Query.PatientID == PatientID);
                    medColl.LoadAll();
                    medColl.MarkAllAsDeleted();
                    medColl.Save();
                }

                medColl = new PastMedicalHistoryCollection();

                foreach (GridDataItem item in grdPastMedicalHist.MasterTableView.Items)
                {
                    if (item.Selected)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var med = medColl.AddNew();
                        med.PatientID = PatientID;
                        med.SRMedicalDisease = item.GetDataKeyValue("ItemID").ToString();
                        med.Notes = txtNotes.Text;
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }


        protected void grdPastMedicalHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //((RadGrid)sender).DataSource = IsEdited ? PastMedicalHistDataTableEditMode() : PastMedicalHistDataTable();
            // Selalu ditampilkan semua
            ((RadGrid)sender).DataSource = PastMedicalHistDataTableEditMode();
        }

        private DataTable PastMedicalHistDataTableEditMode()
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new PastMedicalHistoryQuery("a");
            que.LeftJoin(qrFam)
                .On(que.ItemID == qrFam.SRMedicalDisease && qrFam.PatientID == PatientID);
            que.Where(que.StandardReferenceID == PastMedHist);

            if (!string.IsNullOrEmpty(this.PastMedicalHistRefId))
                que.Where(que.ReferenceID.Like(string.Format("{0}%", PastMedicalHistRefId)));
            else
                que.Where(que.Or(que.ReferenceID.IsNull(), que.ReferenceID == string.Empty));

            que.Select(que.ItemID, que.ItemName, qrFam.Notes, "<CONVERT(BIT,CASE WHEN a.SRMedicalDisease IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }
        private DataTable PastMedicalHistDataTable()
        {
            var que = new PastMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == PastMedHist);
            que.Where(que.PatientID == PatientID);
            que.Select(que.SRMedicalDisease.As("ItemID"), qrSri.ItemName, que.Notes, "<CONVERT(BIT,1) as IsSelected>");

            var dtbPast = que.LoadDataTable();
            return dtbPast;
        }
        protected void grdPastMedicalHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!IsEdited)
                return;
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked)
                {
                    dataItem.Selected = true;
                }
                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["Notes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;

            }
        }
        #endregion
        #region Family Medical History
        private void SaveFamilyMedicalHist()
        {
            using (var trans = new esTransactionScope())
            {
                // FamilyMedicalHistory
                var medColl = new FamilyMedicalHistoryCollection();
                if (IsEdited)
                {
                    medColl.Query.Where(medColl.Query.PatientID == PatientID);
                    medColl.LoadAll();
                    medColl.MarkAllAsDeleted();
                    medColl.Save();
                }

                medColl = new FamilyMedicalHistoryCollection();

                foreach (GridDataItem item in grdFamilyMedicalHist.MasterTableView.Items)
                {
                    if (item.Selected)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var med = medColl.AddNew();
                        med.PatientID = PatientID;
                        med.SRMedicalDisease = item.GetDataKeyValue("ItemID").ToString();
                        med.Notes = txtNotes.Text;
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }


        protected void grdFamilyMedicalHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //((RadGrid)sender).DataSource = IsEdited ? FamilyMedicalHistDataTableEditMode() : FamilyMedicalHistDataTable();
            // Selalu tampilkan semua record
            ((RadGrid)sender).DataSource = FamilyMedicalHistDataTableEditMode();
        }

        private DataTable FamilyMedicalHistDataTableEditMode()
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new FamilyMedicalHistoryQuery("a");
            que.LeftJoin(qrFam)
                .On(que.ItemID == qrFam.SRMedicalDisease && qrFam.PatientID == PatientID);
            que.Where(que.StandardReferenceID == FamilyMedicalHistory.MedicalDiseaseStandardReferenceID);

            // Tambah Filter Reference
            if (!string.IsNullOrEmpty(this.FamilyMedHistRefId))
                que.Where(que.ReferenceID.Like(string.Format("{0}%", FamilyMedHistRefId)));
            else
                que.Where(que.Or(que.ReferenceID.IsNull(), que.ReferenceID == string.Empty));

            que.Select(que.ItemID, que.ItemName, qrFam.Notes, "<CONVERT(BIT,CASE WHEN a.SRMedicalDisease IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }
        private DataTable FamilyMedicalHistDataTable()
        {
            var que = new FamilyMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == FamilyMedicalHistory.MedicalDiseaseStandardReferenceID);
            que.Where(que.PatientID == PatientID);
            que.Select(que.SRMedicalDisease.As("ItemID"), qrSri.ItemName, que.Notes, "<CONVERT(BIT,1) as IsSelected>");
            return que.LoadDataTable();
        }
        protected void grdFamilyMedicalHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!IsEdited)
                return;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked)
                {
                    dataItem.Selected = true;
                }
                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["Notes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;
            }
        }
        #endregion

        #region Patient Allergy
        private DataTable AllergyTable(DataTable table)
        {
            var tbl = new DataTable();

            tbl.Columns.Add("Group", typeof(string));
            tbl.Columns.Add("StandardReferenceID", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("DescAndReaction", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                tbl.Rows.Add(WordProcessing((string)row[0]), row[0], row[1], row[2], string.Empty);
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

            query.Select
                (
                    query.StandardReferenceID,
                    query.ItemID,
                    query.ItemName
                );
            query.Where(query.ReferenceID == AppEnum.StandardReference.PatientHealthRecord);

            DataTable tbl = AllergyTable(query.LoadDataTable());

            foreach (DataRow row in tbl.Rows)
            {
                foreach (BusinessObject.PatientAllergy all in allergyCollection)
                {
                    if (((string)row[1] == all.AllergyGroup) && ((string)row[2] == all.Allergen))
                    {
                        row[4] = all.DescAndReaction;
                        break;
                    }
                }
            }

            tbl.AcceptChanges();

            grdPatientAllergy.DataSource = tbl;
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

        private void SavePatientAllergy()
        {

            var all = new PatientAllergyCollection();
            all.Query.Where(all.Query.PatientID == PatientID);
            all.LoadAll();
            all.MarkAllAsDeleted();
            all.Save();

            all = new PatientAllergyCollection();

            foreach (GridDataItem item in grdPatientAllergy.MasterTableView.Items)
            {
                string desc = ((RadTextBox)item.FindControl("txtAllergenDesc")).Text.Trim();
                if (desc.Length > 0)
                {
                    BusinessObject.PatientAllergy allergy = all.AddNew();
                    allergy.AllergyGroup = item["StandardReferenceID"].Text;
                    allergy.Allergen = item["ItemID"].Text;
                    allergy.AllergenName = item["ItemName"].Text;
                    allergy.SRAnaphylaxis = item["StandardReferenceID"].Text;
                    allergy.Anaphylaxis = item["StandardReferenceID"].Text;
                    allergy.PatientID = PatientID;
                    allergy.DescAndReaction = desc;
                }
            }

            all.Save();
        }

        #endregion

    }
}