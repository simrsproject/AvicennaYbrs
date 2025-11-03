namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.GPI
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    using Temiang.Avicenna.Common;

    public partial class PaymentReceiveDetail : Report
    {
        public PaymentReceiveDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            var hd = new TransPayment();
            if (hd.LoadByPrimaryKey(printJobParameters[0].ValueString))
            {
                if (hd.IsApproved == true)
                {
                    string registrationNo = hd.RegistrationNo;
                    var merge2 = Common.Helper.MergeBilling.GetMergeRegistration(registrationNo);

                    string sRegistrationNo = merge2.Aggregate(string.Empty, (current, str) => current + (str + ","));

                    registrationNo = sRegistrationNo;
                    //string registrationNo = printJobParameters.FindByParameterName("RegistrationNo").ValueString;
                    var registrationNoList = new string[1];
                    if (registrationNo.Contains(","))
                        registrationNoList = registrationNo.Split(',');

                    var order = new TransPaymentItemOrderQuery("a");
                    var payment = new TransPaymentQuery("b");
                    order.es.Distinct = true;
                    order.Select(order.TransactionNo);
                    order.InnerJoin(payment).On(order.PaymentNo == payment.PaymentNo);
                    if (registrationNo.Contains(","))
                        order.Where(payment.RegistrationNo.In(registrationNoList));
                    else
                        order.Where(payment.RegistrationNo == registrationNo);
                    order.Where(
                        order.IsPaymentProceed == true &&
                        order.IsPaymentReturned == false
                        );
                    var orders = new TransPaymentItemOrderCollection();
                    orders.Load(order);

                    #region charges
                    var cost = new CostCalculationQuery("a");
                    var reg = new RegistrationQuery("b");
                    var patient = new PatientQuery("c");
                    var medic1 = new ParamedicQuery("d");
                    var room = new ServiceRoomQuery("f");
                    var cls = new ClassQuery("g");
                    var grr = new GuarantorQuery("h");
                    var item = new ItemQuery("i");
                    var group = new ItemGroupQuery("j");

                    var charges = new TransChargesQuery("k");
                    var chargesItem = new TransChargesItemQuery("l");
                    var unit2 = new ServiceUnitQuery("n");
                    var svc = new ItemServiceQuery("o");

                    cost.Select
                        (
                            // header
                            reg.RegistrationNo,
                            patient.MedicalNo,
                            patient.PatientName,
                            patient.DateOfBirth,
                            medic1.ParamedicName.As("ParamedicNameHeader"),
                            reg.RegistrationDate,
                            reg.RegistrationTime,
                            room.RoomName.Coalesce("''"),
                            cls.ClassName,
                            reg.BedID.Coalesce("''"),
                            grr.GuarantorName,
                            reg.DischargeDate,

                            // footer
                            reg.PlavonAmount,
                            @"<HealthcareName = (SELECT HealthcareName FROM Healthcare) >",

                            // detail
                            group.ItemGroupName,
                            item.ItemName,
                            charges.ExecutionDate.As("TransactionDate"),
                            unit2.ServiceUnitName,
                            chargesItem.Price,
                            cost.PatientAmount,
                            cost.GuarantorAmount,
                            chargesItem.ChargeQuantity,
                            chargesItem.DiscountAmount,
                            (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                            chargesItem.TransactionNo,
                            chargesItem.SequenceNo,
                            chargesItem.ReferenceNo,
                            chargesItem.ReferenceSequenceNo,
                            item.ItemGroupID,
                             "<'02' AS TariffComponentID>",
                            svc.IsPrintWithDoctorName.Coalesce("0"),
                            chargesItem.ParamedicCollectionName
                        );

                    // header
                    cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
                    cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
                    cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    cost.LeftJoin(cls).On(reg.ChargeClassID == cls.ClassID);
                    cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

                    // detail
                    cost.InnerJoin(charges).On(cost.TransactionNo == charges.TransactionNo);
                    cost.InnerJoin(chargesItem).On
                        (
                            cost.TransactionNo == chargesItem.TransactionNo &
                            cost.SequenceNo == chargesItem.SequenceNo
                        );
                    cost.InnerJoin(unit2).On(charges.ToServiceUnitID == unit2.ServiceUnitID);
                    cost.InnerJoin(item).On(chargesItem.ItemID == item.ItemID);
                    cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
                    cost.LeftJoin(svc).On(chargesItem.ItemID == svc.ItemID);

                    if (registrationNo.Contains(","))
                        cost.Where(cost.RegistrationNo.In(registrationNoList));
                    else
                        cost.Where(cost.RegistrationNo == registrationNo);

                    cost.Where(
                        cost.Or(
                            cost.ParentNo == string.Empty,
                            cost.ParentNo.IsNull()
                            )
                        );

                    if (orders.Any())
                        cost.Where(cost.TransactionNo.NotIn(orders.Select(o => o.TransactionNo)));

                    cost.OrderBy
                        (
                            cost.RegistrationNo.Ascending,
                            charges.TransactionNo.Ascending,
                            chargesItem.SequenceNo.Ascending
                        );

                    DataTable table = cost.LoadDataTable(), temp = table.Copy();

                    DataView view = temp.DefaultView;
                    view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
                    temp = view.ToTable();
                    view.Dispose();

                    foreach (DataRow row in table.Rows)
                    {
                        if (row["ReferenceNo"].ToString() == string.Empty && row["ReferenceSequenceNo"].ToString() == string.Empty)
                        {
                            foreach (DataRow tmp in temp.Rows.Cast<DataRow>().Where(tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                                                                                           row["SequenceNo"].ToString() == tmp["ReferenceSequenceNo"].ToString()))
                            {
                                row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
                                row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
                            }
                        }
                    }

                    table.AcceptChanges();

                    foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => ((decimal)row["Total"] <= 0) && ((decimal)row["ChargeQuantity"] <= 0)))
                    {
                        row.Delete();
                    }

                    table.AcceptChanges();

                    foreach (DataRow row in table.Rows)
                    {
                        if (row["IsPrintWithDoctorName"].ToString() == "1")
                        {
                            if (!string.IsNullOrEmpty(row["ParamedicCollectionName"].ToString()))
                            {
                                row["ItemName"] = row["ItemName"] + " (" + row["ParamedicCollectionName"] + ")";
                            }
                        }
                    }
                    table.AcceptChanges();

                    #endregion

                    #region prescription

                    cost = new CostCalculationQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    cls = new ClassQuery("g");
                    grr = new GuarantorQuery("h");
                    item = new ItemQuery("i");
                    group = new ItemGroupQuery("j");

                    var presc = new TransPrescriptionQuery("k");
                    var prescItem = new TransPrescriptionItemQuery("l");
                    unit2 = new ServiceUnitQuery("n");

                    cost.Select
                        (
                            // header
                            reg.RegistrationNo,
                            patient.MedicalNo,
                            patient.PatientName,
                            patient.DateOfBirth,
                            medic1.ParamedicName.As("ParamedicNameHeader"),
                            reg.RegistrationDate,
                            reg.RegistrationTime,
                            room.RoomName.Coalesce("''"),
                            cls.ClassName,
                            reg.BedID.Coalesce("''"),
                            grr.GuarantorName,
                            reg.DischargeDate,

                            // footer
                            reg.PlavonAmount,
                            @"<HealthcareName = (SELECT HealthcareName FROM Healthcare) >",

                            // detail
                            group.ItemGroupName,
                            item.ItemName,
                            presc.PrescriptionDate.As("TransactionDate"),
                            unit2.ServiceUnitName,
                            prescItem.Price,
                            cost.PatientAmount,
                            cost.GuarantorAmount,
                            prescItem.ResultQty.As("ChargeQuantity"),
                            prescItem.DiscountAmount,
                            (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                            item.ItemGroupID
                        );

                    // header
                    cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
                    cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
                    cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    cost.LeftJoin(cls).On(reg.ChargeClassID == cls.ClassID);
                    cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

                    // detail
                    cost.InnerJoin(presc).On(cost.TransactionNo == presc.PrescriptionNo);
                    cost.InnerJoin(prescItem).On
                        (
                            presc.PrescriptionNo == prescItem.PrescriptionNo &
                            cost.SequenceNo == prescItem.SequenceNo
                        );

                    cost.InnerJoin(unit2).On(presc.ServiceUnitID == unit2.ServiceUnitID);
                    cost.InnerJoin(item).On(prescItem.ItemID == item.ItemID);
                    cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);

                    if (registrationNo.Contains(","))
                        cost.Where(cost.RegistrationNo.In(registrationNoList));
                    else
                        cost.Where(cost.RegistrationNo == registrationNo);

                    cost.Where(
                        cost.Or(
                            cost.ParentNo == string.Empty,
                            cost.ParentNo.IsNull()
                            )
                        );

                    if (orders.Any())
                        cost.Where(cost.TransactionNo.NotIn(orders.Select(o => o.TransactionNo)));

                    cost.OrderBy
                        (
                            cost.RegistrationNo.Ascending,
                            presc.PrescriptionNo.Ascending,
                            prescItem.SequenceNo.Ascending
                        );

                    table.Merge(cost.LoadDataTable());

                    #endregion

                    this.DataSource = table;

                    var regis = new Registration();
                    regis.LoadByPrimaryKey(hd.RegistrationNo);
                    txtAdministrationAmount.Value = string.Format("{0:n2}", regis.AdministrationAmount);
                    txtPaymentNo.Value = printJobParameters[0].ValueString;

                    var details = new TransPaymentItemCollection();
                    details.Query.Where(
                        details.Query.PaymentNo == hd.PaymentNo &&
                        details.Query.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR
                        );
                    txtTerimaOleh.Value = details.LoadAll() ? (table.AsEnumerable().Select(t => t.Field<string>("GuarantorName"))).Take(1).SingleOrDefault() : hd.PrintReceiptAsName;

                    DataTable tbl = new TransChargesCollection().fGetPaymentMethod(registrationNo);
                    txtPaymentMethod.Value = Convert.ToString(tbl.Rows[0]["PaymentMethod"]);
                    //txtTotal.Value = string.Format("{0:n2}", table.AsEnumerable().Sum(t => t.Field<decimal>("Total")) + Convert.ToDecimal(txtAdministrationAmount.Value));

                    var xx = table.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount"));

                    txtTotalG.Value = string.Format("{0:n2}", table.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount")) + Convert.ToDecimal(txtAdministrationAmount.Value));
                    txtTotalP.Value = string.Format("{0:n2}", table.AsEnumerable().Sum(t => t.Field<decimal>("PatientAmount")));

                    var healthcare = Healthcare.GetHealthcare();
                    
                    TxtCityRS.Value = healthcare.City;

                    var user = new AppUser();
                    user.LoadByPrimaryKey(hd.LastUpdateByUserID);
                    txtUserName.Value = user.UserName;

                    var dt = new TransPaymentItemCollection();
                    dt.Query.Where(dt.Query.PaymentNo == hd.PaymentNo);
                    dt.LoadAll();
                    txtKembalian.Value = string.Format("{0:n2}", dt.Sum(b => b.Balance));

                    txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(dt.Sum(a => a.Amount) ?? 0);
                }
            }
        }
    }
}