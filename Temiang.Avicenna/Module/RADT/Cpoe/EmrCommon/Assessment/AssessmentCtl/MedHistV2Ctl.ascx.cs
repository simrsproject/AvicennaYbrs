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
{
    public partial class MedHistV2Ctl : BaseAssessmentCtl
    {

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

            txtPastmedical.Text = assessment.PastmedicalHistory;
            txtFamilyMedical.Text = assessment.FamilyMedicalHistory;

            var surgicalHist = new PastSurgicalHistory();
            surgicalHist.LoadByPrimaryKey(PatientID);
            txtSurgicalHistory.Text = surgicalHist.SurgicalHistory;

            var transfusionHist = new PastTransfusionHistory();
            transfusionHist.LoadByPrimaryKey(PatientID);
            txtYearOfTransfusion.Text = transfusionHist.Year;
            txtAllergicReactionOfTransfusion.Text = transfusionHist.AllergicReaction;

            txtJobHistNotes.Text = assessment.JobHistNotes;

            // Refresh
            grdPatientAllergy.Rebind();

        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            assessment.PastmedicalHistory = txtPastmedical.Text;
            assessment.FamilyMedicalHistory = txtFamilyMedical.Text;

            var surgicalHist = new PastSurgicalHistory();
            if (!surgicalHist.LoadByPrimaryKey(PatientID))
                surgicalHist.PatientID = PatientID;
            surgicalHist.SurgicalHistory = txtSurgicalHistory.Text;
            surgicalHist.Save();

            var transfusionHist = new PastTransfusionHistory();
            if (!transfusionHist.LoadByPrimaryKey(PatientID))
                transfusionHist.PatientID = PatientID;
            transfusionHist.Year = txtYearOfTransfusion.Text;
            transfusionHist.AllergicReaction = txtAllergicReactionOfTransfusion.Text;
            transfusionHist.Save();

            SavePatientAllergy();

            assessment.JobHistNotes = txtJobHistNotes.Text;
        }

        protected override void OnDataModeChanged(bool isEdited)
        {

            // Refresh
            grdPatientAllergy.Rebind();
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

        private string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        protected void lbtnPrevJobHistNotes_OnClick(object sender, EventArgs e)
        {
            var qra = new PatientAssessmentQuery("a");
            qra.Where(qra.RegistrationNo == FromRegistrationNo);
            qra.OrderBy(qra.RegistrationInfoMedicID.Ascending);
            qra.es.Top = 1;
            var assesment = new PatientAssessment();
            if (assesment.Load(qra))
            {
                txtJobHistNotes.Text = assesment.JobHistNotes;
            }
        }
    }
}