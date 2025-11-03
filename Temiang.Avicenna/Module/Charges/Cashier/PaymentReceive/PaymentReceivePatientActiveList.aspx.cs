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
    public partial class PaymentReceivePatientActiveList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceivePatientActive;
            
            if (!IsPostBack)
            {
                txtRegistrationDateActive.SelectedDate = DateTime.Now;
                txtRegistrationDate.SelectedDate = DateTime.Now;
                var coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
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
                cboServiceUnitIdActive.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                    cboServiceUnitIdActive.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Registrations;
        }

        protected void grdListActive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdListActive.DataSource = RegistrationActives;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void btnFilterActive_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdListActive.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");

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
                        grr.GuarantorName,
                        qr.IsConsul
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);

                qr.Where(qr.RegistrationDate == txtRegistrationDate.SelectedDate);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = "%" + txtRegistrationNo.Text + "%";
                    qr.Where
                        (qr.Or
                            (
                                string.Format("<r.RegistrationNo LIKE '{0}' OR >", searchReg),
                                string.Format("<p.MedicalNo LIKE '{0}' OR >", searchReg),
                                string.Format("<p.OldMedicalNo LIKE '{0}'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.IsConsul == false,
                    qr.IsClosed == false,
                    qr.IsVoid == false
                    );

                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        private DataTable RegistrationActives
        {
            get
            {
                var qr = new VwRegistrationActiveQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var grr = new GuarantorQuery("c");

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
                        grr.GuarantorName
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);

                if (cboServiceUnitIdActive.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitIdActive.SelectedValue);
                if (txtRegistrationNoActive.Text != string.Empty)
                {
                    string searchReg = "%" + txtRegistrationNoActive.Text + "%";
                    qr.Where
                        (qr.Or
                            (
                                string.Format("<r.RegistrationNo LIKE '{0}' OR >", searchReg),
                                string.Format("<p.MedicalNo LIKE '{0}' OR >", searchReg),
                                string.Format("<p.OldMedicalNo LIKE '{0}'>", searchReg)
                             )
                        );
                }

                if (txtPatientNameActive.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientNameActive.Text + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.OrderBy(qr.RegistrationNo.Descending);

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
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>"
                );

            if (Request.QueryString["pc"] == "yes")
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=yes' as PaymentUrl>");
            else
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=no' as PaymentUrl>");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where
                (
                    query.RegistrationNo.In(regno)
                );
            query.Where(query.TransactionCode == TransactionCode.Payment);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdListActive_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
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
                    "<(SELECT ISNULL(SUM(f.[Amount]),0) FROM TransPaymentItem f WHERE a.[PaymentNo] = f.[PaymentNo]) AS 'TotalPaymentAmount'>"
                );

            if (Request.QueryString["pc"] == "yes")
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=yes' as PaymentUrl>");
            else
                query.Select(
                    "<'PaymentReceiveDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo+'&pc=no' as PaymentUrl>");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(usr).On(query.CreatedBy == usr.UserID);

            query.Where
                (
                    query.RegistrationNo.In(regno)
                );
            query.Where(query.TransactionCode == TransactionCode.Payment);

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
