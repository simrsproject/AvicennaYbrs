using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSIAMTP
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    public partial class PaymentReceiveDetail : Report
    {
        public PaymentReceiveDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            
            Helper.InitializeLogo(pageHeader);

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
                    CostCalculationQuery cost = new CostCalculationQuery("a");
                    RegistrationQuery reg = new RegistrationQuery("b");
                    PatientQuery patient = new PatientQuery("c");
                    ParamedicQuery medic1 = new ParamedicQuery("d");
                    ServiceRoomQuery room = new ServiceRoomQuery("f");
                    ClassQuery cls = new ClassQuery("g");
                    GuarantorQuery grr = new GuarantorQuery("h");
                    ItemQuery item = new ItemQuery("i");
                    ItemGroupQuery group = new ItemGroupQuery("j");

                    TransChargesQuery charges = new TransChargesQuery("k");
                    TransChargesItemQuery chargesItem = new TransChargesItemQuery("l");
                    ServiceUnitQuery unit2 = new ServiceUnitQuery("n");
                    ItemServiceQuery svc = new ItemServiceQuery("o");

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
                        //charges.TransactionDate,
                            charges.ExecutionDate.As("TransactionDate"),
                            unit2.ServiceUnitName,
                            chargesItem.Price,
                            chargesItem.ChargeQuantity,
                            chargesItem.DiscountAmount,
                            (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                            chargesItem.TransactionNo,
                            chargesItem.SequenceNo,
                            chargesItem.ReferenceNo,
                            chargesItem.ReferenceSequenceNo,
                            item.ItemGroupID,
                             "<'02' AS TariffComponentID>",
                            svc.IsPrintWithDoctorName.Coalesce("0")
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
                            var comps = new TransChargesItemCompCollection();
                            comps.Query.Where
                                (
                                    comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                                    comps.Query.SequenceNo == row["SequenceNo"].ToString()
                                );
                            comps.LoadAll();

                            var comp = comps.FindByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString(),
                                row["TariffComponentID"].ToString());

                            if (comp != null && !string.IsNullOrEmpty(comp.ParamedicID))
                            {
                                var doc = new Paramedic();
                                if (doc.LoadByPrimaryKey(comp.ParamedicID))
                                    row["ItemName"] = row["ItemName"].ToString() + " (" + doc.ParamedicName + ")";
                            }
                        }
                    }

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

                    TransPrescriptionQuery presc = new TransPrescriptionQuery("k");
                    TransPrescriptionItemQuery prescItem = new TransPrescriptionItemQuery("l");
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
                    txtTerimaOleh.Value = hd.PrintReceiptAsName;

                    DataTable tbl = new TransChargesCollection().fGetPaymentMethod(registrationNo);
                    txtPaymentMethod.Value = Convert.ToString(tbl.Rows[0]["PaymentMethod"]);
                    txtTotal.Value = string.Format("{0:n2}", table.AsEnumerable().Sum(t => t.Field<decimal>("Total")) + Convert.ToDecimal(txtAdministrationAmount.Value));

                    var healthcare = Healthcare.GetHealthcare();
                    
                    TxtCityRS.Value = healthcare.AddressLine2;

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