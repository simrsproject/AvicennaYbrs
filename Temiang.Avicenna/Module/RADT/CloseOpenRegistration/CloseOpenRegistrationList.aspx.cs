using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class CloseOpenRegistrationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected string RegistrationType
        {
            get { return (string)ViewState["_regType"]; }
            set { ViewState["_regType"] = value; }
        }

        private bool IsMr
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["rt"]) ? false : Request.QueryString["rt"] == "MR";
            }
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

            var regType = Page.Request.QueryString["rt"];
            if (string.IsNullOrEmpty(regType))
                regType = AppConstant.RegistrationType.ClusterPatient;

            switch (regType)
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.InPatientCloseOpenRegistration;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientCloseOpenRegistration;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientCloseOpenRegistration;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientCloseOpenRegistration;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningCloseOpenRegistration;
                    break;
                default:
                    ProgramID = AppConstant.Program.CloseOpenMedicalRecordStatus;
                    break;
            }

            RegistrationType = regType;
            trRegistrationType.Visible = regType == "MR";

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                if (trRegistrationType.Visible)
                    cboSRRegistrationType.SelectedValue = AppConstant.RegistrationType.InPatient;

                grdRegisteredList.Columns.FindByUniqueName("BedID").Visible = (regType == AppConstant.RegistrationType.InPatient);

                grdRegisteredList.Columns.FindByUniqueName("pCloseOpenReg").Visible = !trRegistrationType.Visible;
                grdRegisteredList.Columns.FindByUniqueName("pCloseOpenMr").Visible = trRegistrationType.Visible;
                grdRegisteredList.Columns.FindByUniqueName("pCloseOpenTrans").Visible = !trRegistrationType.Visible;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(RegistrationType == AppConstant.RegistrationType.InPatient || trRegistrationType.Visible))
                    txtDate.SelectedDate = (new DateTime()).NowAtSqlServer().AddDays(-1);
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

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid)) return;

            if (eventArgument.Contains("closeopen_"))
            {
                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(eventArgument.Split('|')[1]);
                    bool isClosed = reg.IsClosed ?? false;
                    if (reg.DischargeDate == null && !isClosed && reg.IsFromDispensary == false)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Patient have not discharge yet. Registration can't be closed.";
                        grdRegisteredList.Rebind();
                        return;
                    }
                }

                Helper.RegistrationOpenClose.SetClosed(eventArgument.Split('|')[1], "Close / Open Registration");
                pnlInfo.Visible = false;
                grdRegisteredList.Rebind();
            }
            else if (eventArgument.Contains("trans_"))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(eventArgument.Split('|')[1]);
                reg.IsHoldTransactionEntry = !reg.IsHoldTransactionEntry;
                reg.Save();

                var hist = new RegistrationCloseOpenHistory();
                hist.AddNew();
                hist.RegistrationNo = reg.RegistrationNo;
                hist.StatusId = "H";
                hist.IsTrue = reg.IsHoldTransactionEntry;
                hist.Notes = "Close / Open Registration";
                hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hist.Save();

                pnlInfo.Visible = false;
                grdRegisteredList.Rebind();
            }
            else if (eventArgument.Contains("closeopenentrymr_"))
            {
                var regNo = eventArgument.Split('|')[1];
                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);

                reg.IsOpenEntryMR = !(reg.IsOpenEntryMR ?? false);
                reg.Save();

                pnlInfo.Visible = false;
                grdRegisteredList.Rebind();
            }
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue) && txtDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientSearch.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gr = new GuarantorQuery("gr");

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
                        qr.IsVoid,
                        qr.IsClosed,
                        qr.IsHoldTransactionEntry,
                        sal.ItemName.As("SalutationName"),
                        qr.IsOpenEntryMR,
                        qr.DischargeDateTime,
                        qr.SRDischargeMethod,
                        gr.GuarantorName,
                        @"<CASE WHEN r.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(gr).On(gr.GuarantorID == qr.GuarantorID);

                if (!trRegistrationType.Visible)
                {
                    qr.Select(@"<CAST(0 AS BIT) AS 'IsAllowMr'>");
                    qr.Where(qr.SRRegistrationType == RegistrationType);
                }
                else
                {
                    qr.Select(@"<CASE WHEN (r.SRRegistrationType = 'IPR' AND (DATEDIFF(HOUR, CAST(CONVERT(VARCHAR(11), r.DischargeDate, 113) + ' ' + r.DischargeTime AS DATETIME), GETDATE()) > 24)) THEN CAST(1 AS BIT) 
                            WHEN (r.SRRegistrationType <> 'IPR' AND (DATEDIFF(HOUR, CAST(CONVERT(VARCHAR(11), r.RegistrationDate, 113) + ' ' + r.RegistrationTime AS DATETIME), GETDATE()) > 24)) THEN CAST(1 AS BIT) 
                            ELSE CAST(0 AS BIT) END AS 'IsAllowMr'>");
                    qr.Where(qr.SRRegistrationType == cboSRRegistrationType.SelectedValue);
                }

                qr.Where(qr.IsVoid == false);

                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate == txtDate.SelectedDate);

                if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                    qr.Where(qr.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));

                if (!txtMedicalNo.Text.Trim().Equals(string.Empty))
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

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    var searchPatient = "%" + Helper.EscapeQuery(txtPatientSearch.Text) + "%";
                    qr.Where(string.Format("<RTRIM(p.FirstName+' '+p.MiddleName)+' '+p.LastName LIKE '{0}'>", searchPatient));
                }

                qr.OrderBy(qr.RegistrationNo.Descending);

                var tbl = qr.LoadDataTable();

                foreach (var row in tbl.Rows.Cast<DataRow>().Where(row => row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.ClusterPatient &&
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
            DataTable dtb;
            if (IsMr)
                dtb = null;
            else
            {
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

                dtb = historyQ.LoadDataTable();
            }

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            pnlInfo.Visible = false;
            grdRegisteredList.Rebind();
        }
    }
}