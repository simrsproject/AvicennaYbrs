using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitVisiteEntryList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitVisiteEntry;

            if (!IsPostBack)
            {
                ServiceUnitCollection coll = new ServiceUnitCollection();
                coll.Query.Where(
                    coll.Query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient
                        ),
                    coll.Query.IsActive == true
                );
                coll.Query.OrderBy(coll.Query.DepartmentID.Ascending);
                coll.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (BusinessObject.ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Registrations;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable Registrations
        {
            get
            {
                RegistrationQuery qr = new RegistrationQuery("r");
                PatientQuery qp = new PatientQuery("p");
                ParamedicQuery qm = new ParamedicQuery("m");
                ServiceUnitQuery unit = new ServiceUnitQuery("s");
                ServiceRoomQuery room = new ServiceRoomQuery("d");
                MergeBillingQuery mrg = new MergeBillingQuery("b");
                GuarantorQuery grr = new GuarantorQuery("c");

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

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate.Between(txtOrderDate1.SelectedDate, txtOrderDate2.SelectedDate));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    qr.Where(
                        qr.Or(
                            qr.RegistrationNo == searchReg,
                            qp.MedicalNo == searchReg,
                            qp.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(qr.IsClosed == false);

                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    if (row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.ClusterPatient && (bool)row["IsConsul"])
                        row.Delete();
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string[] mrg = Helper.MergeBilling.GetMergeRegistration(
                e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());

            TransPaymentQuery query = new TransPaymentQuery("a");
            RegistrationQuery reg = new RegistrationQuery("b");
            PatientQuery patient = new PatientQuery("c");
            ServiceUnitQuery unit = new ServiceUnitQuery("d");
            AppStandardReferenceItemQuery sr = new AppStandardReferenceItemQuery("e");

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
                    @"<(SELECT SUM(f.[Amount]) 
                        FROM TransPaymentItem f 
                        WHERE a.[PaymentNo] = f.[PaymentNo]
                            AND f.SRPaymentType = '" + AppSession.Parameter.PaymentTypeDownPayment + "') AS 'TotalPaymentAmount'>",
                    "<'ServiceUnitVisiteEntryDetail.aspx?md=view&id='+a.PaymentNo+'&regno='+a.RegistrationNo as PaymentUrl>"
                );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            query.Where
                (
                    query.RegistrationNo.In(mrg),
                    query.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
