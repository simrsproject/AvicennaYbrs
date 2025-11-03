using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Drawing;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ClinicalPathwayDetail : BasePageDialog
    {
        private string _color01 = string.Empty;
        protected string Color01
        {
            get
            {
                if (_color01 == string.Empty)
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey("ClinicalPathway", "01"))
                        _color01 = prColor.ItemName;
                    else
                        _color01 = "red";
                }
                return _color01;
            }
        }

        private string _color02 = string.Empty;
        protected string Color02
        {
            get
            {
                if (_color02 == string.Empty)
                {
                    var prColor = new AppStandardReferenceItem();
                    if (prColor.LoadByPrimaryKey("ClinicalPathway", "02"))
                        _color02 = prColor.ItemName;
                    else
                        _color02 = "yellow";
                }
                return _color02;
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected void txtPathwayID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPathwayID.Text)) txtPathwayName.Text = string.Empty;
            else
            {
                var item = new Pathway();
                item.LoadByPrimaryKey(txtPathwayID.Text);
                txtPathwayName.Text = item.PathwayName;

                var rpiec = new RegistrationPathwayItemExecutionCollection();
                var rpic = new RegistrationPathwayItemCollection();
                var rp = new RegistrationPathway();

                // clear dl yg sdh ada
                using (var trans = new esTransactionScope())
                {
                    rpiec.Query.Where(rpiec.Query.RegistrationNo == RegistrationNo);
                    if (rpiec.Query.Load())
                    {
                        rpiec.MarkAllAsDeleted();
                        rpiec.Save();
                    }

                    rpic.Query.Where(rpiec.Query.RegistrationNo == RegistrationNo);
                    if (rpic.Query.Load())
                    {
                        rpic.MarkAllAsDeleted();
                        rpic.Save();
                    }

                    rp.Query.Where(rp.Query.RegistrationNo == RegistrationNo);
                    if (rp.Query.Load())
                    {
                        rp.MarkAsDeleted();
                        rp.Save();
                    }

                    trans.Complete();
                }

                ViewState["vw_PathwayItemExecution"] = null;
                PopulateGrid();

                rp = new RegistrationPathway();
                if (!rp.LoadByPrimaryKey(RegistrationNo, txtPathwayID.Text))
                {
                    rp = new RegistrationPathway();
                    rp.RegistrationNo = RegistrationNo;
                }
                rp.PathwayID = txtPathwayID.Text;

                var pic = new PathwayItemCollection();
                pic.Query.Where(pic.Query.PathwayID == rp.PathwayID);
                pic.Query.Load();

                rpic = new RegistrationPathwayItemCollection();
                rpiec = new RegistrationPathwayItemExecutionCollection();

                foreach (var entity in pic)
                {
                    var rpi = rpic.AddNew();
                    rpi.RegistrationNo = rp.RegistrationNo;
                    rpi.PathwayID = rp.PathwayID;
                    rpi.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                    rpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    rpi.LastUpdateDateTime = DateTime.Now;

                    var piec = new PathwayItemExecutionCollection();
                    piec.Query.Where(piec.Query.PathwayID == rp.PathwayID, piec.Query.PathwayItemSeqNo == entity.PathwayItemSeqNo);
                    piec.Query.Load();

                    foreach (var entity2 in piec)
                    {
                        var rpie = rpiec.AddNew();
                        rpie.RegistrationNo = rp.RegistrationNo;
                        rpie.PathwayID = rp.PathwayID;
                        rpie.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                        rpie.DayNo = entity2.DayNo;
                        rpie.IsApprove = false;
                        rpie.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        rpie.LastUpdateDateTime = DateTime.Now;
                    }
                }
                using (var trans = new esTransactionScope())
                {
                    rp.Save();
                    rpic.Save();
                    rpiec.Save();

                    trans.Complete();
                }

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            //if (!IsCallback)
            //{
            //    PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Pathway, Page);
            //    PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Pathway, txtPathwayID);
            //    txtPathwayID.ReadOnly = false;
            //}

            ButtonOk.Text = "Close";
            ButtonCancel.Visible = false;

            if (IsPostBack) return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            txtRegistrationNo.Text = reg.RegistrationNo;
            txtRegistrationDate.SelectedDate = reg.RegistrationDate;
            txtRegistrationTime.Text = reg.RegistrationTime;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            txtMedicalNo.Text = pat.MedicalNo;
            txtPatientName.Text = pat.PatientName;
            txtGender.Text = pat.Sex;

            txtAgeInYear.Value = reg.AgeInYear;
            txtAgeInMonth.Value = reg.AgeInMonth;
            txtAgeInDay.Value = reg.AgeInDay;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            txtGuarantorID.Text = grr.GuarantorID;
            lblGuarantorName.Text = grr.GuarantorName;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            txtServiceUnitID.Text = unit.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);

            txtRoomID.Text = room.RoomID;
            lblRoomName.Text = room.RoomName;

            var cls = new Class();
            cls.LoadByPrimaryKey(reg.ChargeClassID);

            txtClassID.Text = cls.ClassID;
            lblClassName.Text = cls.ClassName;

            txtBedID.Text = reg.BedID;

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(reg.ParamedicID);

            txtParamedicID.Text = medic.ParamedicID;
            lblParamedicName.Text = medic.ParamedicName;

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : DateTime.Now.Date;
                var y = reg.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays + 1;
            }
            else txtLengthOfStay.Value = 1;

            var eps = new EpisodeDiagnoseQuery("a");

            eps.es.Top = 1;
            eps.Select(eps.DiagnoseID, eps.DiagnosisText);
            eps.Where(
                eps.RegistrationNo == reg.RegistrationNo,
                eps.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain,
                eps.IsVoid == false
                );

            var tbl = eps.LoadDataTable();
            if (tbl.Rows.Count > 0)
            {
                txtDiagnoseID.Text = tbl.Rows[0][0].ToString();
                lblDiagnoseName.Text = tbl.Rows[0][1].ToString();
            }

            var rp = new RegistrationPathway();
            rp.Query.es.Top = 1;
            rp.Query.Where(rp.Query.RegistrationNo == RegistrationNo, rp.Query.PathwayID != string.Empty);
            if (rp.Query.Load())
            {
                if (string.IsNullOrEmpty(rp.PathwayStatus))
                {
                    PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Pathway, Page);
                    PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Pathway, txtPathwayID);
                }
                txtPathwayID.Text = rp.PathwayID;
                ComboBox.SelectedValue(cboPathwayStatus, rp.PathwayStatus);
                txtNotes.Text = rp.Notes;
            }
            else
            {
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Pathway, Page);
                PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Pathway, txtPathwayID);
            }
            txtPathwayID.ReadOnly = false;
            //else
            //{
            //    // Default PathwayID
            //    var diag = new PathwayDiagnoseItem();
            //    diag.Query.Where(diag.Query.DiagnoseID == txtDiagnoseID.Text);
            //    diag.Query.es.Top = 1;
            //    diag.Query.Load();

            //    txtPathwayID.Text = diag.PathwayID ?? string.Empty;

            //    rp = new RegistrationPathway { RegistrationNo = RegistrationNo, PathwayID = txtPathwayID.Text };
            //    rp.Save();
            //}

            if (!string.IsNullOrEmpty(txtPathwayID.Text))
            {
                var pw = new Pathway();
                pw.LoadByPrimaryKey(txtPathwayID.Text);
                txtPathwayName.Text = pw.PathwayName;
                //txtNotes.Text = pw.Notes;
            }
            else
            {
                txtPathwayName.Text = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var arg = Request.Params.Get("__EVENTARGUMENT");
            if (!string.IsNullOrEmpty(arg) && arg.Contains("unRealized_"))
            {
                var vals = arg.Split('_');
                var path = new ClinicalPathway();
                if (path.LoadByPrimaryKey(txtRegistrationNo.Text, txtPathwayID.Text, vals[1].ToInt(), vals[2].ToInt()))
                {
                    path.MarkAsDeleted();
                    path.Save();
                }
            }
            else if (!string.IsNullOrEmpty(arg) && arg.Contains("realized_"))
            {
                var vals = arg.Split('_');

                var cp = new BusinessObject.ClinicalPathway();
                if (!cp.LoadByPrimaryKey(txtRegistrationNo.Text, txtPathwayID.Text, vals[1].ToInt(), vals[2].ToInt()))
                {
                    cp.RegistrationNo = txtRegistrationNo.Text;
                    cp.PathwayID = txtPathwayID.Text;
                    cp.PathwayItemSeqNo = vals[1].ToInt();
                    cp.DayNo = vals[2].ToInt();
                }

                cp.IsRealized = true;
                cp.RealizedDateTime = DateTime.Now;
                cp.ReferenceNo = string.Empty;
                cp.ItemID = string.Empty;
                cp.Save();
            }

            PopulateGrid();

            // Pathway ID change entry
            //var cp = new ClinicalPathway();
            //cp.Query.Where(cp.Query.RegistrationNo == RegistrationNo);
            //cp.Query.es.Top = 1;
            //txtPathwayID.ShowButton = !cp.Query.Load();
            //txtPathwayID.ReadOnly = !txtPathwayID.ShowButton;
        }

        private void PopulateGrid()
        {
            var items = PathwayItems(txtPathwayID.Text);
            var realizedColl = ClinicalPathways(txtPathwayID.Text);
            if (realizedColl.Count > 0)
            {
                var pathwayItemExecutions = ViewState["vw_PathwayItemExecution"] as PathwayItemExecutionCollection;

                foreach (var item in items)
                {
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 1);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 2);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 3);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 4);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 5);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 6);
                    PopulatePathwayItemDisplay(item, pathwayItemExecutions, realizedColl, 7);
                }
            }

            grdList.DataSource = null;
            grdList.DataSource = items;
            grdList.DataBind();
        }

        private static void PopulatePathwayItemDisplay(PathwayItem item,
            PathwayItemExecutionCollection pathwayItemExecutions, ClinicalPathwayCollection realizedColl, int dayNo)
        {
            var day = pathwayItemExecutions.SingleOrDefault(p => p.PathwayItemSeqNo == item.PathwayItemSeqNo && p.DayNo == dayNo);
            var realized = realizedColl.SingleOrDefault(p => day != null && (p.PathwayItemSeqNo == day.PathwayItemSeqNo && p.DayNo == day.DayNo));

            item.SetColumn(string.Format("refTo_chk{0}", dayNo), realized != null && (realized.IsRealized ?? false));
            if (realized != null)
            {
                item.SetColumn(string.Format("refTo_RefNo{0}", dayNo), realized.ReferenceNo);

                if (!string.IsNullOrEmpty(realized.InterventionItemID))
                {
                    var im = new Item();
                    im.LoadByPrimaryKey(realized.InterventionItemID);
                    item.SetColumn(string.Format("refTo_InterventionItemName{0}", dayNo), im.ItemName);
                }
            }
        }

        private PathwayItemCollection PathwayItems(string pathwayID)
        {
            var coll = new PathwayItemCollection();

            var item = new PathwayItemQuery("a");
            var pathway = new PathwayQuery("b");

            item.Select(
                item,
                "<'' AS refTo_1>",
                "<'' AS refTo_2>",
                "<'' AS refTo_3>",
                "<'' AS refTo_4>",
                "<'' AS refTo_5>",
                "<'' AS refTo_6>",
                "<'' AS refTo_7>",
                "<CAST(0 AS BIT) AS refTo_chk1>",
                "<CAST(0 AS BIT) AS refTo_chk2>",
                "<CAST(0 AS BIT) AS refTo_chk3>",
                "<CAST(0 AS BIT) AS refTo_chk4>",
                "<CAST(0 AS BIT) AS refTo_chk5>",
                "<CAST(0 AS BIT) AS refTo_chk6>",
                "<CAST(0 AS BIT) AS refTo_chk7>",
                "<'' AS refTo_RefNo1>",
                "<'' AS refTo_RefNo2>",
                "<'' AS refTo_RefNo3>",
                "<'' AS refTo_RefNo4>",
                "<'' AS refTo_RefNo5>",
                "<'' AS refTo_RefNo6>",
                "<'' AS refTo_RefNo7>",
                "<'' AS refTo_InterventionItemName1>",
                "<'' AS refTo_InterventionItemName2>",
                "<'' AS refTo_InterventionItemName3>",
                "<'' AS refTo_InterventionItemName4>",
                "<'' AS refTo_InterventionItemName5>",
                "<'' AS refTo_InterventionItemName6>",
                "<'' AS refTo_InterventionItemName7>"
                );
            item.InnerJoin(pathway).On(item.PathwayID == pathway.PathwayID);
            item.Where(item.PathwayID == pathwayID);

            coll.Load(item);

            foreach (var entity in coll)
            {
                foreach (var exec in PathwayItemExecutions(pathwayID).Where(p => p.PathwayItemSeqNo == entity.PathwayItemSeqNo).OrderBy(p => p.DayNo))
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

            return coll;
        }

        private PathwayItemExecutionCollection PathwayItemExecutions(string pathwayID)
        {
            if (ViewState["vw_PathwayItemExecution"] != null) return ViewState["vw_PathwayItemExecution"] as PathwayItemExecutionCollection;

            var coll = new PathwayItemExecutionCollection();

            var query = new PathwayItemExecutionQuery("a");
            var item = new PathwayItemQuery("b");

            query.Select(
                query,
                item.AssesmentHeaderName.As("refToPathwayItem_AssesmentHeaderName"),
                item.AssesmentGroupName.As("refToPathwayItem_AssesmentGroupName"),
                item.AssesmentName.As("refToPathwayItem_AssesmentName")
                ); ;
            query.InnerJoin(item).On(query.PathwayID == item.PathwayID && query.PathwayItemSeqNo == item.PathwayItemSeqNo);
            query.Where(item.PathwayID == pathwayID);

            coll.Load(query);

            ViewState["vw_PathwayItemExecution"] = coll;

            return coll;
        }

        private ClinicalPathwayCollection ClinicalPathways(string pathwayID)
        {
            var coll = new ClinicalPathwayCollection();
            coll.Query.Where(coll.Query.RegistrationNo == txtRegistrationNo.Text, coll.Query.PathwayID == pathwayID);
            coll.Query.Load();

            return coll;
        }

        protected void cboPathwayStatus_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPathwayID.Text))
            {
                var rp = new RegistrationPathway();
                if (rp.LoadByPrimaryKey(RegistrationNo, txtPathwayID.Text))
                {
                    if (string.IsNullOrEmpty(txtPathwayID.Text))
                        rp.str.PathwayStatus = string.Empty;
                    else
                        rp.PathwayStatus = cboPathwayStatus.SelectedValue;

                    rp.Save();
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            ShowInformationHeader(string.Empty);

            if (!string.IsNullOrWhiteSpace(txtPathwayID.Text))
            {
                if (string.IsNullOrEmpty(cboPathwayStatus.SelectedValue))
                {
                    var rpiec = new RegistrationPathwayItemExecutionCollection();
                    var rpic = new RegistrationPathwayItemCollection();
                    var rp = new RegistrationPathway();

                    // clear dl yg sdh ada
                    using (var trans = new esTransactionScope())
                    {
                        rpiec.Query.Where(rpiec.Query.RegistrationNo == RegistrationNo);
                        if (rpiec.Query.Load())
                        {
                            rpiec.MarkAllAsDeleted();
                            rpiec.Save();
                        }

                        rpic.Query.Where(rpiec.Query.RegistrationNo == RegistrationNo);
                        if (rpic.Query.Load())
                        {
                            rpic.MarkAllAsDeleted();
                            rpic.Save();
                        }

                        rp.Query.Where(rp.Query.RegistrationNo == RegistrationNo);
                        if (rp.Query.Load())
                        {
                            rp.MarkAsDeleted();
                            rp.Save();
                        }

                        trans.Complete();
                    }
                }
                else
                {
                    if (cboPathwayStatus.SelectedValue == "F" && string.IsNullOrWhiteSpace(txtNotes.Text)) // fail
                    {
                        ShowInformationHeader("Notes is required.");
                        return false;
                    }

                    var rp = new RegistrationPathway();
                    if (rp.LoadByPrimaryKey(RegistrationNo, txtPathwayID.Text))
                    {
                        rp.Notes = txtNotes.Text;
                        rp.Save();
                    }
                }
            }
            return true;
        }
    }
}
