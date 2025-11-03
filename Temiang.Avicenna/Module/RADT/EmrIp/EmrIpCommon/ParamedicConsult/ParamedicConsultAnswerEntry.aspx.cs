using System;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ParamedicConsultAnswerEntry : BasePageDialogEntry
    {

        #region QueryString Properties
        protected string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        protected string ConsultReferNo
        {
            get { return Request.QueryString["crno"]; }
        }

        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            IsSingleRecordMode = true;
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            IsCustomReportList = true;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.DeleteVisible = false;
            ToolBar.PrintVisible = false;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Consultation Patient : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", RegNo: " + RegistrationNo + ")";
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var consult = new ParamedicConsultRefer();
            consult.LoadByPrimaryKey(ConsultReferNo);

            PopulateEntryControl(consult);
        }

        private void PopulateEntryControl(ParamedicConsultRefer consult)
        {
            ApplyConsultReferType(consult.ConsultReferType);
            ApplyServiceUnitID(consult.ToServiceUnitID);

            optConsultReferType.SelectedValue = consult.ConsultReferType;
            txtConsultDate.SelectedDate = consult.ConsultDateTime;
            txtRegistrationNo.Text = consult.RegistrationNo;
            var reg = new Registration();
            reg.LoadByPrimaryKey(consult.RegistrationNo);
            txtFromServiceUnitName.Text = ServiceUnit.GetServiceUnitName(reg.ServiceUnitID);
            txtFromParamedicName.Text = Paramedic.GetParamedicName(consult.ParamedicID);

            if (consult.ConsultReferType == "R")
                txtToServiceUnitName.Text = ServiceUnit.GetServiceUnitName(consult.ToServiceUnitID);

            txtToParamedicName.Text = Paramedic.GetParamedicName(consult.ToParamedicID);
            txtParamedicConsultType.Text = StandardReference.GetItemName(AppEnum.StandardReference.ParamedicConsultType,
                consult.SRParamedicConsultType);
            txtNotes.Text = consult.Notes;

            txtChiefComplaint.Text = consult.ChiefComplaint;
            txtPastMedicalHistory.Text = consult.PastMedicalHistory;
            txtHpi.Text = consult.Hpi;
            txtActionExamTreatment.Text = consult.ActionExamTreatment;

            txtConsultAnswerDate.SelectedDate = consult.AnswerDateTime;
            txtConsultAnswerTime.SelectedDate = consult.AnswerDateTime;


            if (answerPhy.Visible)
            {
                StandardReference.InitializeWithOneRow(cboSRConsultAnswerTypePhysiotherapy, AppEnum.StandardReference.ConsultAnswerType,
                    consult.SRConsultAnswerType);
                txtActiveMotion.Text = consult.ActiveMotion;
                txtPassiveMotion.Text = consult.PassiveMotion;

                txtAnswerPhysiotherapy.Text = consult.Answer;
                txtAnswerDiagnose.Text = consult.AnswerDiagnose;
                txtAnswerPlan.Text = consult.AnswerPlan;
                txtAnswerAction.Text = consult.AnswerAction;
            }
            else
            {
                StandardReference.InitializeWithOneRow(cboSRConsultAnswerType, AppEnum.StandardReference.ConsultAnswerType,
                    consult.SRConsultAnswerType);

                txtAnswer.Text = consult.Answer;
            }

            PopulateSignValue(consult);
        }


        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //SIGN
            var isVisible = newVal != AppEnum.DataMode.Read;
            btnPhysicianAnswerSign.Enabled = isVisible;
        }
        protected override void OnMenuNewClick()
        {

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
        }

        protected override void OnMenuCancelNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            // Answer hanya bisa diedit oleh dokter yg dituju (Handono 231016)
            var consult = new ParamedicConsultRefer();
            if (consult.LoadByPrimaryKey(ConsultReferNo))
            {
                if (!consult.ToParamedicID.Equals(AppSession.UserLogin.ParamedicID))
                {
                    args.IsCancel = true;
                    args.MessageText = "The consult answer can only be edited by the consult doctor";
                    return;
                }
            }
            else
            {
                args.IsCancel = true;
                args.MessageText = "Consult not found";
                return;
            }
        }

        protected override void OnMenuEditClick()
        {
            if (answerPhy.Visible)
                StandardReference.InitializeIncludeSpace(cboSRConsultAnswerTypePhysiotherapy, AppEnum.StandardReference.ConsultAnswerType);
            else
                StandardReference.InitializeIncludeSpace(cboSRConsultAnswerType, AppEnum.StandardReference.ConsultAnswerType);

            var pcr = new ParamedicConsultRefer();
            if (pcr.LoadByPrimaryKey(ConsultReferNo))
            {
                if (pcr.AnswerDateTime != null)
                {
                    txtConsultAnswerDate.SelectedDate = pcr.AnswerDateTime;
                    txtConsultAnswerTime.SelectedDate = pcr.AnswerDateTime;
                }
                else
                {
                    // Default Value
                    var date = (new DateTime()).NowAtSqlServer();
                    txtConsultAnswerDate.SelectedDate = date;
                    txtConsultAnswerTime.SelectedDate = date;
                }
            }

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
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

        public override bool OnGetStatusMenuAdd()
        {
            return false;
        }
        public override bool OnGetStatusMenuEdit()
        {
            // Answer hanya bisa diedit oleh dokter yg dituju (Handono 230503)
            var consult = new ParamedicConsultRefer();
            if (!consult.LoadByPrimaryKey(ConsultReferNo)) return false;

            return consult.ToParamedicID.Equals(AppSession.UserLogin.ParamedicID);
        }

        public override bool OnGetStatusMenuDelete()
        {
            return false;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return null;
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

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.enableadd='{0}';", ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID));
        }

        #endregion

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var tran = new esTransactionScope())
            {
                // Save Answer
                var consult = new ParamedicConsultRefer();
                if (consult.LoadByPrimaryKey(ConsultReferNo))
                {
                    consult.AnswerDateTime = DateTime.Parse(txtConsultAnswerDate.SelectedDate.Value.ToShortDateString() + " " + txtConsultAnswerTime.SelectedTime);
                    if (answerPhy.Visible)
                    {
                        consult.SRConsultAnswerType = cboSRConsultAnswerTypePhysiotherapy.SelectedValue;
                        consult.Answer = txtAnswerPhysiotherapy.Text;
                        consult.AnswerDiagnose = txtAnswerDiagnose.Text;
                        consult.AnswerPlan = txtAnswerPlan.Text;
                        consult.AnswerAction = txtAnswerAction.Text;
                    }
                    else
                    {
                        consult.SRConsultAnswerType = cboSRConsultAnswerType.SelectedValue;
                        consult.Answer = txtAnswer.Text;
                    }

                    SetSignValue(consult);

                    consult.Save();

                    tran.Complete();
                }
            }

            return true;
        }

        private void ApplyServiceUnitID(string serviceUnitID)
        {
            pnlBasicFunction.Visible = false; // Hanya untuk Fisioterapi
            answerGen.Visible = true;
            answerPhy.Visible = false;

            if (!string.IsNullOrEmpty(serviceUnitID))
            {
                var unit = new ServiceUnit();
                if (unit.LoadByPrimaryKey(serviceUnitID))
                {
                    pnlBasicFunction.Visible = AppParameter
                        .GetParameterValue(AppParameter.ParameterItem.PhysiotherapyServiceUnitIDs).Contains(serviceUnitID);

                    answerGen.Visible = !pnlBasicFunction.Visible;
                    answerPhy.Visible = pnlBasicFunction.Visible;

                    if (pnlBasicFunction.Visible)
                        txtAnswer.Height = new System.Web.UI.WebControls.Unit(300, System.Web.UI.WebControls.UnitType.Pixel);
                    return;
                }
            }

        }

        private void ApplyConsultReferType(string consultReferType)
        {
            var isRefer = consultReferType == "R";
            trReferServiceUnit.Visible = isRefer;

        }

        #region sign

        private void SetSignValue(ParamedicConsultRefer consult)
        {
            // PhysicianAnswerSign
            var imgHelper = new ImageHelper();
            if (!string.IsNullOrWhiteSpace(hdnPhysicianAnswerSignImage.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPhysicianAnswerSignImage.Value), new Size(332, 185));
                consult.PhysicianAnswerSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
            else
                consult.PhysicianAnswerSign = null;
        }

        private void PopulateSignValue(ParamedicConsultRefer consult)
        {
            //SIGN
            var imgHelper = new ImageHelper();
            if (consult.PhysicianAnswerSign != null)
            {
                var val = (byte[])consult.PhysicianAnswerSign;
                fmImage.DataValue = val;
                try
                {
                    var mstream = new MemoryStream(val);
                    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                    hdnPhysicianAnswerSignImage.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                }
                catch
                {
                    fmImage.DataValue = null;
                    hdnPhysicianAnswerSignImage.Value = String.Empty;
                }
            }
            else
            {
                fmImage.DataValue = null;
                hdnPhysicianAnswerSignImage.Value = String.Empty;
            }
        }
        #endregion
    }
}
