using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class UpdateReferralList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

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

            ProgramID = AppConstant.Program.UpdateReferral;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                cboSRRegistrationType.SelectedValue = AppConstant.RegistrationType.InPatient;
                
                //RestoreValueFromCookie();
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

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdRegisteredList.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue) && txtDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientSearch.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var refer = new ReferralQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var rg = new AppStandardReferenceItemQuery("rg");

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
                        qr.IsConsul,
                        qr.IsVoid,
                        qr.IsClosed,
                        qr.IsHoldTransactionEntry,
                        refer.ReferralName,
                        sal.ItemName.As("SalutationName"),
                        rg.ItemName.As("ReferralGroupName")
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.LeftJoin(refer).On(qr.ReferralID == refer.ReferralID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(rg).On(rg.StandardReferenceID == "ReferralGroup" & qr.SRReferralGroup == rg.ItemID);
                if (!this.IsUserCrossUnitAble)
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    qr.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == qr.ServiceUnitID);
                }

                qr.Where(
                    qr.SRRegistrationType == cboSRRegistrationType.SelectedValue,
                    qr.IsVoid == false,
                    qr.IsClosed == false
                    );
                
                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate == txtDate.SelectedDate);

                if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                    qr.Where(qr.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    var searchPatient = Helper.EscapeQuery(txtPatientSearch.Text);
                    //qr.Where(string.Format("<p.MedicalNo = '{0}' OR p.OldMedicalNo = '{0}' OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%' OR RTRIM(p.FirstName+' '+p.MiddleName)+' '+p.LastName LIKE '%{0}%'>", searchPatient));
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchPatient, "patient");
                }

                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationTime.Descending, qr.RegistrationNo.Ascending);

                var tbl = qr.LoadDataTable();

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
                grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            //SaveValueToCookie();

            grdRegisteredList.Rebind();
        }
    }
}
