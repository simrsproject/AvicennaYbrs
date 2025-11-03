using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    public partial class ReceiptSlipSummaryRpt : Report
    {          
        public ReceiptSlipSummaryRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            var hd = new TransPayment();
            if (hd.LoadByPrimaryKey(printJobParameters[0].ValueString))
            {
                if (hd.IsApproved == true)
                {
                    string registrationNo = hd.RegistrationNo;
                    var merge2 = Temiang.Avicenna.Common.Helper.MergeBilling.GetMergeRegistration(registrationNo);

                    string sRegistrationNo = string.Empty;
                    foreach (var str in merge2)
                    {
                        sRegistrationNo += str + ",";
                    }

                    registrationNo = sRegistrationNo;

                    string[] registrationNoList = new string[1];
                    if (registrationNo.Contains(","))
                        registrationNoList = registrationNo.Split(',');

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
                    TransChargesItemCompQuery itemComp = new TransChargesItemCompQuery("x");
                    TariffComponentQuery tarifComp = new TariffComponentQuery("z");

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
                            reg.AdministrationAmount,
                            reg.PlavonAmount,
                            @"<HealthcareName = (SELECT HealthcareName FROM Healthcare) >",

                            // detail
                            item.ItemName.As("ItemGroupName"),
                            @"<ItemName = ISNULL(z.TariffComponentName,i.[ItemName]) >",
                            charges.ExecutionDate.As("TransactionDate"),
                            unit2.ServiceUnitName,
                            @"<Price = ISNULL(X.Price,l.[Price])>",
                            chargesItem.ChargeQuantity,
                            @"<DiscountAmount = l.ChargeQuantity * ISNULL(x.[DiscountAmount],l.[DiscountAmount])>",
                            @"<Total = ((ISNULL(X.Price,l.[Price]) -  ISNULL(x.[DiscountAmount],l.[DiscountAmount]))) * l.ChargeQuantity>",
                            chargesItem.TransactionNo,
                            chargesItem.SequenceNo,
                            chargesItem.ReferenceNo,
                            chargesItem.ReferenceSequenceNo,
                            item.ItemGroupID,
                            svc.IsPrintWithDoctorName.Coalesce("0"),
                            itemComp.TariffComponentID.Coalesce("''"),
                            cost.DiscountAmount.As("DiscountAmountFinal"),
                            chargesItem.ChargeQuantity.Cast(esCastType.Int32).As("PrescriptionQty"),
                            chargesItem.SRItemUnit
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
                            cost.TransactionNo == chargesItem.TransactionNo &
                            cost.SequenceNo == chargesItem.SequenceNo
                        );
                    cost.InnerJoin(unit2).On(charges.ToServiceUnitID == unit2.ServiceUnitID);
                    cost.InnerJoin(item).On(cost.ItemID == item.ItemID);
                    cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
                    cost.LeftJoin(svc).On(cost.ItemID == svc.ItemID);

                    cost.LeftJoin(itemComp).On(chargesItem.TransactionNo == itemComp.TransactionNo && chargesItem.SequenceNo == itemComp.SequenceNo);
                    cost.LeftJoin(tarifComp).On(itemComp.TariffComponentID == tarifComp.TariffComponentID);

                    if (registrationNo.Contains(","))
                        cost.Where(cost.RegistrationNo.In(registrationNoList));
                    else
                        cost.Where(cost.RegistrationNo == registrationNo);
                    cost.Where(cost.Or(chargesItem.ParentNo == string.Empty, chargesItem.ParentNo.IsNull()));
                    cost.OrderBy
                        (
                            cost.RegistrationNo.Ascending,
                            charges.TransactionNo.Ascending,
                            chargesItem.SequenceNo.Ascending
                        );

                    DataTable table = cost.LoadDataTable();

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
                            reg.AdministrationAmount,
                            reg.PlavonAmount,
                            @"<HealthcareName = (SELECT HealthcareName FROM Healthcare) >",

                            // detail
                            group.ItemGroupName,
                            item.ItemName,
                            presc.PrescriptionDate.As("TransactionDate"),
                            unit2.ServiceUnitName,
                            prescItem.Price,
                            prescItem.ResultQty.As("ChargeQuantity"),
                            (prescItem.ResultQty * prescItem.DiscountAmount).As("DiscountAmount"),
                            (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                            prescItem.PrescriptionNo.As("TransactionNo"),
                            prescItem.SequenceNo,
                            presc.ReferenceNo,
                            "<CASE WHEN ISNULL(k.ReferenceNo, '') = '' THEN '' ELSE l.SequenceNo END AS ReferenceSequenceNo>",
                            item.ItemGroupID,
                            "<0 AS IsPrintWithDoctorName>",
                            "<'' AS TariffComponentID>",
                            cost.DiscountAmount.As("DiscountAmountFinal"),
                            prescItem.PrescriptionQty.Cast(esCastType.Int32).As("PrescriptionQty"),
                            prescItem.SRItemUnit
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
                            cost.TransactionNo == prescItem.PrescriptionNo &&
                            cost.SequenceNo == prescItem.SequenceNo
                        );

                    cost.InnerJoin(unit2).On(presc.ServiceUnitID == unit2.ServiceUnitID);
                    cost.InnerJoin(item).On(prescItem.ItemID == item.ItemID);
                    cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);

                    if (registrationNo.Contains(","))
                        cost.Where(cost.RegistrationNo.In(registrationNoList));
                    else
                        cost.Where(cost.RegistrationNo == registrationNo);

                    cost.OrderBy
                        (
                            cost.RegistrationNo.Ascending,
                            presc.PrescriptionNo.Ascending,
                            prescItem.SequenceNo.Ascending
                        );

                    table.Merge(cost.LoadDataTable().AsEnumerable().Where(c => c.Field<decimal>("ChargeQuantity") != 0).CopyToDataTable());

                    #endregion

                    var temp = table.AsEnumerable().Where(c => !string.IsNullOrEmpty(c.Field<string>("ReferenceNo")) &&
                                                               !string.IsNullOrEmpty(c.Field<string>("ReferenceSequenceNo")))
                                                   .GroupBy(c => new
                                                   {
                                                       ReferenceNo = c.Field<string>("ReferenceNo"),
                                                       ReferenceSequenceNo = c.Field<string>("ReferenceSequenceNo"),
                                                       TariffComponentID = c.Field<string>("TariffComponentID")
                                                   })
                                                   .Select(g => new
                                                   {
                                                       g.Key.ReferenceNo,
                                                       g.Key.ReferenceSequenceNo,
                                                       g.Key.TariffComponentID,
                                                       ChargeQuantity = g.Sum(c => c.Field<decimal>("ChargeQuantity")),
                                                       Total = g.Sum(c => c.Field<decimal>("Total"))
                                                   });

                    foreach (DataRow row in table.Rows)
                    {
                        if (row["ReferenceNo"].ToString() != string.Empty && row["ReferenceSequenceNo"].ToString() != string.Empty)
                            continue;

                        foreach (var tmp in temp.Where(tmp => row["TransactionNo"].ToString() == tmp.ReferenceNo &&
                                                              row["SequenceNo"].ToString() == tmp.ReferenceSequenceNo &&
                                                              row["TariffComponentID"].ToString() == tmp.TariffComponentID))
                        {
                            row["Total"] = (decimal)row["Total"] + tmp.Total;
                            row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + tmp.ChargeQuantity;
                        }
                    }

                    table.AcceptChanges();

                    foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => ((decimal)row["Total"] <= 0) &&
                                                                                    ((decimal)row["ChargeQuantity"] <= 0)))
                    {
                        row.Delete();
                    }

                    table.AcceptChanges();

                    foreach (DataRow row in table.AsEnumerable().Where(t => (table.AsEnumerable().GroupBy(c => new
                    {
                        ItemGroupName = c.Field<string>("ItemGroupName")
                    })
                                                                                                 .Select(g => new
                                                                                                 {
                                                                                                     g.Key.ItemGroupName,
                                                                                                     ItemGroupNameCount = g.Count()
                                                                                                 })).Where(c => c.ItemGroupNameCount == 1)
                                                                .Select(c => c.ItemGroupName).Contains(t.Field<string>("ItemGroupName")) &&
                                                                                                       t.Field<string>("TariffComponentID") == "01"))
                    {
                        row["ItemName"] = "Biaya " + row["ItemGroupName"];
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
                                    row["ItemName"] = doc.ParamedicName;
                            }
                        }
                    }

                    this.DataSource = table;

                    txtPaymentNo.Value = printJobParameters[0].ValueString;
                    txtTerimaOleh.Value = hd.PrintReceiptAsName;

                    DataTable tbl = new TransChargesCollection().fGetPaymentMethod(registrationNo);
                    txtPaymentMethod.Value = Convert.ToString(tbl.Rows[0]["PaymentMethod"]);
                    txtDownpayment.Value = string.Format("{0:n0}", printJobParameters.FindByParameterName("DownPayment").ValueNumeric);
                    txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(tbl.Rows[0]["SumTotal"]));
                    txtUserName.Value = printJobParameters.FindByParameterName("UserName").ValueString;

                    var healthcare = Healthcare.GetHealthcare();
                    
                    TxtCityRS.Value = healthcare.AddressLine2;

                    var dt = new TransPaymentItemCollection();
                    dt.Query.Select(dt.Query.Balance.Sum());
                    dt.Query.Where("<PaymentNo = ('" + txtPaymentNo.Value + "')>");
                    dt.LoadAll();
                    txtKembalian.Value = string.Format("{0:n0}", dt[0].Balance);

                    hd.PrintNumber++;
                    if (!hd.IsPrinted ?? false)
                        hd.IsPrinted = true;

                    hd.Save();
                }
            }
        }
    }
}