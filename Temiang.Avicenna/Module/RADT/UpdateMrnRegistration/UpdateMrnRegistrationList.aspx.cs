using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class UpdateMrnRegistrationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.UpdateMrnRegistration;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;

                var unit = new ServiceUnitCollection();
                unit.Query.Where(unit.Query.IsActive == true);
                unit.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit u in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }
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
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && txtDate.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientSearch.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
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
                        qr.IsConsul,
                        qr.IsVoid,
                        qr.IsClosed,
                        qr.IsHoldTransactionEntry,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                qr.Where(
                    qr.IsClosed == false,
                    qr.IsVoid == false,
                    qr.Or(qr.SRRegistrationType != AppConstant.RegistrationType.InPatient, qr.DischargeDate.IsNull())
                    );

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate == txtDate.SelectedDate);

                if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                    qr.Where(qr.RegistrationNo == Helper.EscapeQuery(txtRegistrationNo.Text));

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    var searchPatient = Helper.EscapeQuery(txtPatientSearch.Text);
                    qr.Where(string.Format("<p.MedicalNo = '{0}' OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%' OR RTRIM(p.FirstName+' '+p.MiddleName)+' '+p.LastName LIKE '%{0}%'>", searchPatient));
                }

                qr.OrderBy(qr.RegistrationNo.Ascending);

                var tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
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
            //Load record
            var hist = new RegistrationMRNHistoryQuery("a");
            var from = new PatientQuery("b");
            var to = new PatientQuery("c");
            var usr = new AppUserQuery("d");
            var sal = new AppStandardReferenceItemQuery("e");
            var sal2 = new AppStandardReferenceItemQuery("f");

            hist.InnerJoin(from).On(from.PatientID == hist.FromPatientID);
            hist.InnerJoin(to).On(to.PatientID == hist.ToPatientID);
            hist.InnerJoin(usr).On(usr.UserID == hist.UpdateByUserID);
            hist.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation && sal.ItemID == from.SRSalutation);
            hist.LeftJoin(sal2).On(sal2.StandardReferenceID == AppEnum.StandardReference.Salutation && sal2.ItemID == to.SRSalutation);

            hist.Select(hist, usr.UserName.As("UpdateByUserName"), 
                from.MedicalNo.As("FromMedicalNo"), from.PatientName.As("FromPatientName"), sal.ItemName.As("FromSalutationName"), from.Sex.As("FromSex"),
                to.MedicalNo.As("ToMedicalNo"), to.PatientName.As("ToPatientName"), sal2.ItemName.As("ToSalutationName"), to.Sex.As("ToSex"));
            hist.Where(hist.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());
            hist.OrderBy(hist.UpdateDateTime.Ascending);

            //Apply
            e.DetailTableView.DataSource = hist.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.Rebind();
        }
    }
}
