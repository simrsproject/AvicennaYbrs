using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class JobOrderConfirmedList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JobOrderConfirmed;

            if (!IsPostBack)
                ComboBox.PopulateWithServiceUnitForTransactionJO(cboServiceUnitID, TransactionCode.JobOrder, false);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            grdList.DataSource = TransCharges;
        }

        private DataTable TransCharges
        {
            get
            {
                var query = new TransChargesItemQuery("a");
                var tcq = new TransChargesQuery("aa");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var unit2 = new ServiceUnitQuery("e");
                var room = new ServiceRoomQuery("f");
                var item = new ItemQuery("g");
                var guar = new GuarantorQuery("h");
                var sumInfo = new RegistrationInfoSumaryQuery("sum");
                var asr = new AppStandardReferenceItemQuery("i");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.ReferenceNo,
                        tcq.TransactionDate,
                        unit.ServiceUnitName,
                        tcq.RegistrationNo,
                        patient.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        patient.PatientName,
                        unit2.ServiceUnitName.As("ClusterName"),
                        room.RoomName,
                        tcq.BedID,
                        tcq.TransactionDate.Date().As("Group"),
                        //@"<CAST(CONVERT(VARCHAR(10), aa.TransactionDate, 101) AS DATETIME) AS [Group]>",
                        @"<'' AS [PaymentNo]>",
                        @"<CONVERT(VARCHAR(5), aa.TransactionDate, 114) AS [TransactionTime]>",
                        tcq.ToServiceUnitID.As("ServiceUnitID"),
                        reg.PatientID,
                        guar.GuarantorName,
                        sumInfo.NoteCount
                    );

                query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
                query.InnerJoin(reg).On(tcq.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(tcq.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(unit2).On(tcq.FromServiceUnitID == unit2.ServiceUnitID);
                query.LeftJoin(room).On(tcq.RoomID == room.RoomID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
                query.LeftJoin(sumInfo).On(tcq.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & asr.ItemID == patient.SRSalutation);

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    query.Where(tcq.TransactionDate >= txtOrderDate1.SelectedDate, tcq.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            tcq.RegistrationNo == txtRegistrationNo.Text,
                            patient.MedicalNo == txtRegistrationNo.Text,
                            patient.OldMedicalNo == txtRegistrationNo.Text,
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                if (txtTransactionNo.Text != string.Empty)
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where
                    (
                        query.IsOrderRealization == false,
                        query.IsVoid == false,
                        tcq.IsOrder == true,
                        tcq.IsApproved == true,
                        //reg.IsHoldTransactionEntry == false,
                        query.Or(query.IsSelectedExtraItem == true, query.IsSelectedExtraItem.IsNull())
                    );
                query.Where(query.ParentNo == string.Empty);
                query.Where(query.Or(query.IsPaymentConfirmed.IsNull(), query.IsPaymentConfirmed == false));
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA")
                    query.Where(reg.SRRegistrationType != AppConstant.RegistrationType.MedicalCheckUp);
                
                query.es.Distinct = true;
                //query.es2.Connection.CommandTimeout = 0;
                query.OrderBy(query.TransactionNo.Ascending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var pay = new TransPaymentItemOrderQuery();
                    pay.Where(pay.TransactionNo == row["TransactionNo"], pay.IsPaymentProceed == true, pay.IsPaymentReturned == false);
                    pay.Select(pay.PaymentNo);
                    pay.es.Top = 1;
                    DataTable pTable = pay.LoadDataTable();
                    if (pTable.Rows.Count > 0)
                    {
                        row["PaymentNo"] = pTable.Rows[0]["PaymentNo"].ToString();
                    }
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }
    }
}
