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
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Education Entry Control
    /// 
    /// Created by: Handono (Timika 13 Nov 2019)
    /// Note:
    /// Data di save ke table PatientEducation
    /// </summary>
    public partial class EducationIntegratedCtl : BaseAssessmentCtl
    {

        private const string EducationType = "ASMNT";
        private const string ReferenceType = "ASMNT";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new PatientEducation();
            if (ent.LoadByPrimaryKey(RegistrationNo, assessment.PatientEducationSeqNo ?? 0))
            {
                if (!IsEdited)
                {
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationEvaluation,
        AppEnum.StandardReference.PatientEducationEvaluation.ToString(), ent.SRPatientEducationEvaluation);
                    ComboBox.PopulateWithOneStandardReference(cboSREducationMethod,
                        AppEnum.StandardReference.PatientEducationMethod.ToString(), ent.SRPatientEducationMethod);
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationRecipient,
                        AppEnum.StandardReference.PatientEducationRecipient.ToString(), ent.SRPatientEducationRecipient);
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationGoal,
                        AppEnum.StandardReference.PatientEducationGoal.ToString(), ent.SRPatientEducationGoal);
                }
                else
                {
                    ComboBox.SelectedValue(cboSRPatientEducationEvaluation,ent.SRPatientEducationEvaluation);
                    ComboBox.SelectedValue(cboSREducationMethod, ent.SRPatientEducationMethod);
                    ComboBox.SelectedValue(cboSRPatientEducationRecipient, ent.SRPatientEducationRecipient);
                    ComboBox.SelectedValue(cboSRPatientEducationGoal, ent.SRPatientEducationGoal);
                }

                txtMethodOther.Text = ent.MethodOther;
                txtRecipientName.Text = ent.RecipientName;
                txtDuration.Value = ent.Duration;
                txtPatientEducationEvaluationOth.Text = ent.PatientEducationEvaluationOth;
                txtPatientEducationGoalOth.Text = ent.PatientEducationGoalOth;
                //txtVerificator.Text = ent.Verificator;
                txtDuration.Value = ent.Duration;
            }

            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, assessment.PatientEducationSeqNo ?? 0, ReferenceType, false);
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new PatientEducation();
            if (!ent.LoadByPrimaryKey(RegistrationNo, assessment.PatientEducationSeqNo ?? 0))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.SeqNo = NewSeqNo();
            }
            ent.SRUserType = AppSession.UserLogin.SRUserType;
            ent.EducationType = EducationType;
            ent.ReferenceType = ReferenceType;
            ent.EducationByUserID = AppSession.UserLogin.UserID;
            ent.EducationDateTime = assessment.AssessmentDateTime;
            ent.SRPatientEducationEvaluation = cboSRPatientEducationEvaluation.SelectedValue;
            ent.PatientEducationEvaluationOth = txtPatientEducationEvaluationOth.Text;
            ent.SRPatientEducationMethod = cboSREducationMethod.SelectedValue;
            ent.MethodOther = txtMethodOther.Text;
            ent.str.SRPatientEducationProblem = string.Empty;
            ent.SRPatientEducationRecipient = cboSRPatientEducationRecipient.SelectedValue;
            ent.RecipientName = txtRecipientName.Text;
            ent.SRPatientEducationGoal = cboSRPatientEducationGoal.SelectedValue;
            ent.PatientEducationGoalOth = txtPatientEducationGoalOth.Text;
            //ent.Verificator = txtVerificator.Text;
            ent.Duration = txtDuration.Value.ToInt();
            ent.ReferenceNo = RegistrationInfoMedicID;

            ent.Save();

            SavePatientEducation(ent.SeqNo ?? 0);

            // Update Ref
            assessment.PatientEducationSeqNo = ent.SeqNo;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            if (isEdited)
            {
                // Selanjutkan akan dijalankan OnPopulate jadi jangan ditimpa
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationRecipient, AppEnum.StandardReference.PatientEducationRecipient);
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationEvaluation, AppEnum.StandardReference.PatientEducationEvaluation);
                StandardReference.InitializeIncludeSpace(cboSREducationMethod, AppEnum.StandardReference.PatientEducationMethod);
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationGoal, AppEnum.StandardReference.PatientEducationGoal);
            }

            grdPatientEducation.Columns[0].Display = isEdited; // Selected
            grdPatientEducation.Columns[1].Display = !isEdited; // IsSelected
            grdPatientEducation.Columns[3].Display = !isEdited; // Notes
            grdPatientEducation.Columns[4].Display = isEdited; // Notes Edit


            // Refresh
            grdPatientEducation.Rebind();
        }

        public override void OnMenuNewClick()
        {
            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, 0, ReferenceType, false);
            ComboBox.SelectedValue(cboSRPatientEducationRecipient, "001"); // Patient
        }

        #endregion



        #region Education
        private int NewSeqNo()
        {
            var qr = new PatientEducationQuery("a");
            var fb = new PatientEducation();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SeqNo.ToInt() + 1;
            }
            return 1;
        }

        protected void grdPatientEducation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!IsEdited)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;

                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["EducationNotes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;
            }
        }

        private DataTable PatientEducationDataTable(string registrationNo, int seqNo, string referenceID, bool isJustSelected)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new PatientEducationLineQuery("a");

            if (isJustSelected)
                que.InnerJoin(qrFam)
                    .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);
            else
                que.LeftJoin(qrFam)
                    .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);

            que.Where(que.StandardReferenceID == "PatientEducation", que.IsActive==1);

            que.Where(que.ReferenceID.Like(string.Format("{0}%", referenceID)));
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrFam.EducationNotes, "<CONVERT(BIT,CASE WHEN a.SRPatientEducation IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        private void SavePatientEducation(int seqNo)
        {
            using (var trans = new esTransactionScope())
            {
                // PatientEducationLine
                var medColl = new PatientEducationLineCollection();
                if (IsEdited)
                {
                    medColl.Query.Where(medColl.Query.RegistrationNo == RegistrationNo, medColl.Query.SeqNo == seqNo);
                    medColl.LoadAll();
                    medColl.MarkAllAsDeleted();
                    medColl.Save();
                }

                medColl = new PatientEducationLineCollection();

                foreach (GridDataItem item in grdPatientEducation.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));

                    if (chkIsSelected.Checked)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var med = medColl.AddNew();
                        med.RegistrationNo = RegistrationNo;
                        med.SeqNo = seqNo;
                        med.SRPatientEducation = item.GetDataKeyValue("ItemID").ToString();
                        med.EducationNotes = txtNotes.Text;
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        #endregion

    }
}