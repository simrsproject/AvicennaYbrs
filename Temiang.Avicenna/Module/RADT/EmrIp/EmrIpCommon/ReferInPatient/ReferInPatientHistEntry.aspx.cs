using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ReferInPatientHistEntry : BasePageDialogHistEntry
    {

        #region QueryString Properties
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        private bool IsParamedicInTeam
        {
            get
            {
                return Request.QueryString["pit"] == "1";
            }
        }
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Menu hardcode
            ToolBar.ApprovalEnabled = false;
            ToolBar.VoidEnabled = false;
            ToolBar.EditEnabled = false;

            ToolBar.AddEnabled = IsParamedicInTeam;

            Splitter.Orientation = Orientation.Horizontal;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "In Patient Refer of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", RegNo: " + RegistrationNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitComplete(EventArgs e)
        {
            if (!IsPostBack)
            {
                // Toolbar Print
                var btn = new RadToolBarButton("Refer Notes")
                {
                    Value = string.Format("rpt_{0}", AppConstant.Report.ReferNotes)
                };
                ToolBarMenuPrint.Buttons.Add(btn);                
                
                var btn2 = new RadToolBarButton("Refer Notes RM 12")
                {
                    Value = string.Format("rpt_{0}", AppConstant.Report.RM12_ReferNotes)
                };
                ToolBarMenuPrint.Buttons.Add(btn2);
            }
            base.OnInitComplete(e);

            // Hardcode Print button
            ToolBarMenuPrint.Visible = true;

        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdReferHist.Columns[0].Display = newVal == AppEnum.DataMode.Read;
            cboToParamedicID.Enabled = newVal == AppEnum.DataMode.New;
            txtReferDate.Enabled = newVal == AppEnum.DataMode.New;
        }
        protected override void OnMenuNewClick()
        {
            txtReferDate.SelectedDate = DateTime.Now.Date;
            txtReferTime.Text = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);

            txtReferNotes.Text = string.Empty;
            txtActionExamTreatment.Text = string.Empty;

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
            if (!args.IsCancel)
            {
                grdReferHist.Rebind();
            }
        }

        protected override void OnMenuCancelNewClick(ValidateArgs args)
        {
            hdnSequenceNo.Value = string.Empty;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            if (string.IsNullOrEmpty(hdnSequenceNo.Value))
            {
                Helper.RegisterStartupScript(this, "invalid", "alert('Please select refer record first');");
                args.IsCancel = true;
                return;
            }

            var jobParameter = printJobParameters.AddNew();
            jobParameter.Name = "RegistrationNo";
            jobParameter.ValueString = RegistrationNo;

            var jobParameter2 = printJobParameters.AddNew();
            jobParameter2.Name = "SequenceNo";
            jobParameter2.ValueNumeric = hdnSequenceNo.Value.ToInt();
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var refer = new ReferInPatient();
            if (refer.LoadByPrimaryKey(RegistrationNo, hdnSequenceNo.Value.ToInt()))
            {

                if (!string.IsNullOrEmpty(refer.Answer))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                        "alert('Consult has responded, can not void refer');", true);
                    args.IsCancel = true;
                    return;
                }

                if (txtReferDate.SelectedDate < DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                        "alert('Can not void refer previouse date');", true);
                    args.IsCancel = true;
                    return;
                }
                refer.MarkAsDeleted();
                refer.Save();

                grdReferHist.Rebind();
            }
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
            // Hanya dokter pada registrasi dan team yg bisa membuat refer
            //return AppSession.Parameter.IsSickLetterCanEntryByUserNonPhsycian 
            //    || ParamedicID == AppSession.UserLogin.ParamedicID 
            //    || ParamedicTeam.IsParamedicInTeam(AppSession.UserLogin.ParamedicID, RegistrationNo);

            return ParamedicID == AppSession.UserLogin.ParamedicID
                   || ParamedicTeam.IsParamedicInTeam(AppSession.UserLogin.ParamedicID, RegistrationNo,MergeRegistrations);

        }
        public override bool OnGetStatusMenuEdit()
        {
            return false;
        }

        public override bool OnGetStatusMenuDelete()
        {
            var refer = new ReferInPatient();
            if (refer.LoadByPrimaryKey(RegistrationNo, hdnSequenceNo.Value.ToInt()))
            {
                return (string.IsNullOrEmpty(refer.Answer));
            }

            return false;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            // Disable diset menggunakan ToolBar.ApprovalEnabled = false 
            // Jika fungsi ini return false malah akan memunculkan image Stamp
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            // Disable diset menggunakan ToolBar.VoidEnabled = false 
            // Jika fungsi ini return false malah akan memunculkan image Void
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            if (txtReferDate.SelectedDate.Value.Date < DateTime.Now.Date)
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Refer date is invalid.');", true);
                args.MessageText = "Refer date is invalid";
            }
            if (string.IsNullOrEmpty(cboToParamedicID.SelectedValue))
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "required", "alert('Physician is required.');", true);
                args.MessageText = args.MessageText + Environment.NewLine + "Physician is required";
                
            }

            if (!string.IsNullOrEmpty(args.MessageText))
            {
                args.IsCancel = true;
                return false;
            }


            using (var trans = new esTransactionScope())
            {
                // Save Refer
                var refer = new ReferInPatient();
                if (isNewRecord)
                {
                    var coll = new ReferInPatientCollection();
                    coll.Query.Where(coll.Query.RegistrationNo == RegistrationNo);
                    coll.Query.es.Top = 1;
                    coll.Query.OrderBy(coll.Query.SequenceNo.Descending);
                    coll.Query.Select(coll.Query.SequenceNo);
                    coll.LoadAll();

                    refer.RegistrationNo = RegistrationNo;
                    refer.FromParamedicID = ParamedicID;
                    refer.ToParamedicID = cboToParamedicID.SelectedValue;

                    if (coll.Count == 1)
                        refer.SequenceNo = coll[0].SequenceNo + 1;
                    else
                        refer.SequenceNo = 1;

                    // Add to ParamedicTeam
                    var teams = new ParamedicTeamCollection();
                    teams.Query.es.Top = 1;
                    teams.Query.Where(teams.Query.RegistrationNo == RegistrationNo,
                        teams.Query.ParamedicID == cboToParamedicID.SelectedValue);
                    teams.Query.OrderBy(teams.Query.StartDate.Descending);
                    teams.LoadAll();
                    if (teams.Count == 0 || (teams.Count == 1 &&
                                             (teams[0].StartDate > Convert.ToDateTime(txtReferDate.SelectedDate).Date 
                                              || (teams[0].EndDate !=null && teams[0].EndDate < Convert.ToDateTime(txtReferDate.SelectedDate).Date))))
                    {
                        var team = new ParamedicTeam();
                        team.RegistrationNo = RegistrationNo;
                        team.ParamedicID = cboToParamedicID.SelectedValue;
                        team.SRParamedicTeamStatus = "02"; //Dokter rawat bersama
                        team.StartDate = Convert.ToDateTime(txtReferDate.SelectedDate).Date;
                        team.Save();
                    }
                }
                else
                {
                    refer.LoadByPrimaryKey(RegistrationNo, hdnSequenceNo.Value.ToInt());
                }

                var dateSelected = Convert.ToDateTime(txtReferDate.SelectedDate);
                var times = txtReferTime.TextWithLiterals.Split(':');
                refer.ReferDateTime = new DateTime(dateSelected.Year, dateSelected.Month, dateSelected.Day,
                    times[0].ToInt(), times[1].ToInt(), 0);

                refer.Notes = txtReferNotes.Text;
                refer.ActionExamTreatment = txtActionExamTreatment.Text;
                refer.Save();

                SaveRegistrationInfoMedic(refer.SequenceNo.ToString(), refer.Notes, refer.ActionExamTreatment,
                    cboToParamedicID.Text, refer.ReferDateTime);

                trans.Complete();

                hdnSequenceNo.Value = refer.SequenceNo.ToString();
            }

            return true;
        }

        private void SaveRegistrationInfoMedic(string refNo, string notes, string action, string paramedicName, DateTime? referDate)
        {
            var ent = new RegistrationInfoMedic();
            var qr = new RegistrationInfoMedicQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.ReferenceNo == refNo, qr.SRMedicalNotesInputType == "REF");
            qr.es.Top = 1;

            ent.Load(qr);

            if (string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
            {
                var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = "REF";
                ent.ServiceUnitID = string.Empty;
                ent.ParamedicID = ParamedicID;

                ent.Info4 = string.Empty;
            }

            ent.Info1 = paramedicName;
            ent.Info2 = notes;
            ent.Info3 = action;
            ent.IsPRMRJ = false;
            ent.DateTimeInfo = referDate;
            ent.ReferenceNo = refNo;
            ent.Save();
        }

        #region Refer History

        protected void grdReferHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new ReferInPatientQuery("a");

            var parFrom = new ParamedicQuery("p1");
            query.InnerJoin(parFrom).On(query.FromParamedicID == parFrom.ParamedicID);

            var parTo = new ParamedicQuery("p2");
            query.InnerJoin(parTo).On(query.ToParamedicID == parTo.ParamedicID);

            query.Select
                (
                query.SelectAll(),
                parFrom.ParamedicName.As("FromParamedicName"),
                parTo.ParamedicName.As("ToParamedicName")
                );

            query.Where(query.RegistrationNo == RegistrationNo);

            var dtb = query.LoadDataTable();

            grdReferHist.DataSource = dtb;
        }

        protected void grdReferHist_ItemCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            hdnSequenceNo.Value = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            if (e.CommandName == "View")
            {
                PopulateEntryControl(hdnSequenceNo.Value.ToInt());
                RefreshMenuStatus();
            }
        }

        private void PopulateEntryControl(int seqNo)
        {
            var refer = new ReferInPatient();
            refer.LoadByPrimaryKey(RegistrationNo, seqNo);

            txtReferDate.SelectedDate = Convert.ToDateTime(refer.ReferDateTime).Date;
            var time = Convert.ToDateTime(refer.ReferDateTime);
            txtReferTime.Text = string.Format("{0:00}:{1:00}", time.Hour, time.Minute);
            ComboBox.PopulateWithOneParamedic(cboToParamedicID, refer.ToParamedicID);
            txtReferNotes.Text = refer.Notes;
            txtActionExamTreatment.Text = refer.ActionExamTreatment;
        }

        #endregion

    }
}
