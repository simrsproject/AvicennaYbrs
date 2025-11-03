namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for BillingSummaryShort.
    /// </summary>
    public partial class BillingStatementSummary : Report
    {
        public BillingStatementSummary(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');

            decimal? downPayment = printJobParameters.FindByParameterName("DownPayment").ValueNumeric;
            decimal? paymentAmount = printJobParameters.FindByParameterName("PaymentAmount").ValueNumeric;
            if (paymentAmount > 0)
                paymentAmount -= downPayment;

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
            var asri = new AppStandardReferenceItemQuery("p");
            var pay = new TransPaymentItemOrderQuery("q");
            var payib = new TransPaymentItemIntermBillQuery("r");

            cost.Select
                (
                    // header
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.DateOfBirth,
                    patient.StreetName,
                    patient.City,
                    medic1.ParamedicName.As("ParamedicNameHeader"),
                    reg.RegistrationDateTime.As("DateRegistration"),
                    room.RoomName.Coalesce("''"),
                    cls.ClassName,
                    reg.BedID.Coalesce("''"),
                    grr.GuarantorName,
                    reg.DischargeDate,
                    reg.DischargeTime,
                    @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()
                        END
                      END AS 'DischargeDates'>",

                    // footer
                    reg.AdministrationAmount,
                    reg.PlavonAmount,
                    "<" + downPayment + " AS DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",

                    // detail
                    asri.ItemName.As("BillingGroup"),
                    group.ItemGroupName,
                    item.ItemName,
                    charges.ExecutionDate.As("TransactionDate"), 
                    unit2.ServiceUnitName,
                    chargesItem.Price,
                    chargesItem.ChargeQuantity,
                    chargesItem.DiscountAmount,
                    cost.PatientAmount.As("TotalPA"),
                    cost.GuarantorAmount.As("TotalGA"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    chargesItem.TransactionNo,
                    chargesItem.SequenceNo,
                    chargesItem.ReferenceNo,
                    chargesItem.ReferenceSequenceNo,
                    item.ItemGroupID,
                    svc.IsPrintWithDoctorName.Coalesce("0")
                );

            // header
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            cost.InnerJoin(cls).On(reg.ChargeClassID == cls.ClassID);
            cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            // detail
            cost.InnerJoin(charges).On(cost.TransactionNo == charges.TransactionNo);
            cost.InnerJoin(chargesItem).On
                (
                    charges.TransactionNo == chargesItem.TransactionNo &
                    cost.SequenceNo == chargesItem.SequenceNo
                );
            cost.InnerJoin(unit2).On(charges.ToServiceUnitID == unit2.ServiceUnitID);
            cost.InnerJoin(item).On(chargesItem.ItemID == item.ItemID);
            cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
            cost.LeftJoin(svc).On(chargesItem.ItemID == svc.ItemID);
            cost.LeftJoin(asri).On
                (
                    item.SRBillingGroup == asri.ItemID &
                    asri.StandardReferenceID == "BillingGroup"
                );
            cost.LeftJoin(pay).On(
                    cost.TransactionNo == pay.TransactionNo && cost.SequenceNo == pay.SequenceNo &&
                    pay.IsPaymentProceed == true && pay.IsPaymentReturned == false);
            cost.LeftJoin(payib).On(
                               cost.IntermBillNo == payib.IntermBillNo &&
                               payib.IsPaymentProceed == true && payib.IsPaymentReturned == false); 
            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    ),
                pay.PaymentNo.IsNull(),
                payib.PaymentNo.IsNull()
                );

            cost.OrderBy
                (
                    cost.RegistrationNo.Ascending,
                    item.ItemGroupID.Ascending,
                    charges.TransactionNo.Ascending,
                    chargesItem.SequenceNo.Ascending
                );

            DataTable table = cost.LoadDataTable(), temp = table.Copy();

            DataView view = temp.DefaultView;
            view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
            temp = view.ToTable();

            foreach (DataRow row in table.Rows)
            {
                if (row["ReferenceNo"].ToString() == string.Empty && row["ReferenceSequenceNo"].ToString() == string.Empty)
                {
                    foreach (DataRow tmp in temp.Rows)
                    {
                        if (row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                            row["SequenceNo"].ToString() == tmp["ReferenceSequenceNo"].ToString())
                        {
                            row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
                            row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
                        }
                    }
                }
            }

            table.AcceptChanges();

            foreach (DataRow row in table.Rows)
            {
                if (((decimal)row["Total"] <= 0) && ((decimal)row["ChargeQuantity"] <= 0))
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
                        printJobParameters.FindByParameterName("ParamedicTariffComponentID").ValueString);

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
            var presc = new TransPrescriptionQuery("k");
            var prescItem = new TransPrescriptionItemQuery("l");
            unit2 = new ServiceUnitQuery("n");
            pay = new TransPaymentItemOrderQuery("o");
            payib = new TransPaymentItemIntermBillQuery("p");

            var prescReff = new TransPrescriptionQuery("w");
            var costReff = new CostCalculationQuery("x");
            var payReff = new TransPaymentItemOrderQuery("y");
            var payibReff = new TransPaymentItemIntermBillQuery("z");

            cost.Select
                (
                // header
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.DateOfBirth,
                    patient.StreetName,
                    patient.City,
                    medic1.ParamedicName.As("ParamedicNameHeader"),
                    reg.RegistrationDateTime.As("DateRegistration"),
                    room.RoomName.Coalesce("''"),
                    cls.ClassName,
                    reg.BedID.Coalesce("''"),
                    grr.GuarantorName,
                    reg.DischargeDate,
                    reg.DischargeTime,
                    @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()
                        END
                      END AS 'DischargeDates'>",

                    // footer
                    reg.AdministrationAmount,
                    reg.PlavonAmount,
                    "<" + downPayment + " AS DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",

                    // detail
                    group.ItemGroupName,
                    item.ItemName,
                    presc.PrescriptionDate.As("TransactionDate"),
                    unit2.ServiceUnitName,
                    prescItem.Price,
                    prescItem.ResultQty.As("ChargeQuantity"),
                    prescItem.DiscountAmount,
                    cost.PatientAmount.As("TotalPA"),
                    cost.GuarantorAmount.As("TotalGA"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    item.ItemGroupID
                );

            // header
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            cost.InnerJoin(cls).On(reg.ChargeClassID == cls.ClassID);
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
            cost.LeftJoin(pay).On(
                                cost.TransactionNo == pay.TransactionNo && cost.SequenceNo == pay.SequenceNo &&
                                pay.IsPaymentProceed == true && pay.IsPaymentReturned == false);
            cost.LeftJoin(payib).On(
                                cost.IntermBillNo == payib.IntermBillNo &&
                                payib.IsPaymentProceed == true && payib.IsPaymentReturned == false);

            cost.LeftJoin(prescReff).On(presc.ReferenceNo == prescReff.PrescriptionNo);
            cost.LeftJoin(costReff).On(cost.RegistrationNo == costReff.RegistrationNo &&
                                       prescReff.PrescriptionNo == costReff.TransactionNo &&
                                       cost.SequenceNo == costReff.SequenceNo);
            cost.LeftJoin(payReff).On(
                 costReff.TransactionNo == payReff.TransactionNo && costReff.SequenceNo == payReff.SequenceNo &&
                 payReff.IsPaymentProceed == true && payReff.IsPaymentReturned == false);
            cost.LeftJoin(payibReff).On(
                 costReff.IntermBillNo == payibReff.IntermBillNo &&
                 payibReff.IsPaymentProceed == true && payibReff.IsPaymentReturned == false);

            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    ),
                pay.PaymentNo.IsNull(),
                payib.PaymentNo.IsNull(),
                    payReff.PaymentNo.IsNull(),
                    payibReff.PaymentNo.IsNull()
                );

            cost.OrderBy
                (
                    cost.RegistrationNo.Ascending,
                    presc.PrescriptionNo.Ascending,
                    prescItem.SequenceNo.Ascending
                );

            table.Merge(cost.LoadDataTable());

            #endregion

            this.DataSource = table;
        }
    }
}