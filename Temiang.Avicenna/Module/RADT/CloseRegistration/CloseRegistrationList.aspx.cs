using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class CloseRegistrationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected string RegistrationType
        {
            get { return (string)ViewState["_regType"]; }
            set { ViewState["_regType"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            RegistrationType = Page.Request.QueryString["rt"];
            switch (RegistrationType)
            {
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientCloseRegistration;
                    grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = false;
                    break;
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.InPatientCloseRegistration;
                    grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = true;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientCloseRegistration;
                    grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = false;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientCloseRegistration;
                    grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = false;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningCloseRegistration;
                    grdRegisteredList.Columns[grdRegisteredList.Columns.Count - 1].Visible = false;
                    break;
            }

            if (!IsPostBack)
            {
                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    pnlFilterDate.Visible = false;
                    grdRegisteredList.Columns[3].Visible = true;
                }

                if (RegistrationType != AppConstant.RegistrationType.InPatient)
                    pnlFilterDischargeDate.Visible = false;

                if (pnlFilterDate.Visible)
                    txtDate.SelectedDate = (new DateTime()).NowAtSqlServer().AddDays(-1);
                if (pnlFilterDischargeDate.Visible)
                    txtDischargeDate.SelectedDate = (new DateTime()).NowAtSqlServer().AddDays(-1);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && txtDischargeDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var guar = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        "<CAST(1 AS BIT) AS 'IsAllowClose'>",
                        qr.IsConsul,
                        guar.GuarantorName,
                        sal.ItemName.As("SalutationName"),
                        qr.DischargeDate
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                switch (RegistrationType)
                {
                    case AppConstant.RegistrationType.InPatient:
                        qr.Where(qr.DischargeDate.IsNotNull());
                        if (!txtDischargeDate.IsEmpty)
                            qr.Where(qr.DischargeDate == txtDischargeDate.SelectedDate);
                        break;
                    case AppConstant.RegistrationType.ClusterPatient:
                    case AppConstant.RegistrationType.EmergencyPatient:
                    case AppConstant.RegistrationType.OutPatient:
                        if (!txtDate.IsEmpty)
                            qr.Where(qr.RegistrationDate == txtDate.SelectedDate);
                        break;
                }

                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qr.Where(
                            qr.Or(
                                qp.MedicalNo == searchMedNo,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }
                if (txtRegistrationNo.Text != string.Empty)
                    qr.Where(qr.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                         (
                             string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                         );
                }

                qr.Where
                    (
                        qr.SRRegistrationType == RegistrationType,
                        qr.IsClosed == false
                    );

                qr.OrderBy(qr.RegistrationNo.Ascending);

                DataTable tbl = qr.LoadDataTable();

                foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.ClusterPatient &&
                                                                              (bool)row["IsConsul"]))
                {
                    row.Delete();
                }

                tbl.AcceptChanges();
                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdRegisteredList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var historyQ = new RegistrationCloseOpenHistoryQuery("a");

            historyQ.Select(
                historyQ.RegistrationCloseOpenId,
                historyQ.RegistrationNo,
                @"<CASE WHEN a.StatusId = 'C' THEN 'CLOSE / OPEN Registration' WHEN a.StatusId = 'H' THEN 'LOCK / UNLOCK Transaction' ELSE '' END AS 'ActionName'>",
                @"<CASE WHEN a.StatusId = 'C' THEN (CASE WHEN a.IsTrue = 1 THEN 'CLOSE' ELSE 'OPEN' END) WHEN a.StatusId = 'H' THEN (CASE WHEN a.IsTrue = 1 THEN 'LOCK' ELSE 'UNLOCK' END) ELSE '-' END AS 'Status'>",
                historyQ.IsTrue,
                historyQ.Notes,
                historyQ.Reason,
                historyQ.LastUpdateDateTime,
                historyQ.LastUpdateByUserID
            );
            historyQ.Where(historyQ.RegistrationNo == dataItem.GetDataKeyValue("RegistrationNo").ToString());
            historyQ.OrderBy(historyQ.LastUpdateDateTime.Descending);

            DataTable dtb = historyQ.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            SelectedState(((CheckBox)sender).Checked);
        }

        private void SelectedState(bool selected)
        {
            foreach (CheckBox chkBox in grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = selected;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (eventArgument == "process")
            {
                foreach (GridDataItem dataItem in grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                {
                    Helper.RegistrationOpenClose.SetClosed(dataItem["RegistrationNo"].Text, true, "Close Registration");
                }

                grdRegisteredList.Rebind();
            }
        }
    }
}