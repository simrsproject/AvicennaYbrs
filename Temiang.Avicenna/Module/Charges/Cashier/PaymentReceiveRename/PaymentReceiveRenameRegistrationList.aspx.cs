using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PaymentReceiveRenameRegistrationList : BasePage
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

            ProgramID = AppConstant.Program.PaymentReceiveRename; //???

            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    coll.Query.IsActive == true
                );
                coll.Query.OrderBy(coll.Query.DepartmentID.Ascending);
                coll.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                if (!IsPostBack) RestoreValueFromCookie();
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSSA")
                SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) &&
                  string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtPaymentNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                //var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;
                qr.es.Distinct = true;

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
                        grr.GuarantorName,
                        qr.IsConsul,
                        qr.ServiceUnitID,
                        sal.ItemName.As("SalutationName")
                        //,
                        //mrg.FromRegistrationNo
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                //qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtOrderDate1.SelectedDate, qr.RegistrationDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where
                    //    (qr.Or
                    //         (
                    //             string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                    //             string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                    //             string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                    //             string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //             string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //         )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                    
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where(qp.FullName.Like(searchPatient));

                    //qr.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }

                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    var py = new TransPaymentQuery("py");
                    qr.InnerJoin(py).On(qr.RegistrationNo == py.RegistrationNo);
                    qr.Where(py.PaymentNo == txtPaymentNo.Text,
                             py.Or(py.TransactionCode == TransactionCode.Payment,
                                   py.TransactionCode == TransactionCode.PaymentReturn), py.IsVoid == false);
                }

                qr.Where(qr.IsVoid == false);

                if (!AppSession.Parameter.IsSeparatePaymentForOpConsul)
                    qr.Where(qr.IsConsul == false);

                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationNo.Ascending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string[] regno = Helper.MergeBilling.GetMergeRegistration(e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

            var query = new TransPaymentQuery("a");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var usr = new AppUserQuery("e");

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select
                (
                    query.PaymentNo,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    unit.ServiceUnitName,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.IsApproved,
                    query.IsVoid,
                    usr.UserName,
                    query.Notes,
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>"
                );

            if (Request.QueryString["pc"] == "yes")
                query.Select(
                    "<'PaymentReceiveRenameDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=yes' as PaymentUrl>");
            else
                query.Select(
                    "<'PaymentReceiveRenameDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=no' as PaymentUrl>");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where(query.RegistrationNo.In(regno), query.IsVoid == false);

            if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                query.Where(query.PaymentNo == txtPaymentNo.Text);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var payment = new TransPayment();
                if (payment.LoadByPrimaryKey(param[1]) && payment.IsVoid == true)
                {
                    using (var trans = new esTransactionScope())
                    {
                        var coll = new TransPaymentCollection();
                        coll.Query.Where(coll.Query.PaymentReferenceNo == param[1]);
                        coll.LoadAll();
                        foreach (var dp in coll)
                        {
                            dp.PaymentReferenceNo = string.Empty;
                            dp.LastUpdateDateTime = DateTime.Now;
                            dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        coll.Save();
                        trans.Complete();
                    }
                }
                
                grdList.Rebind();
            }
        }
    }
}
