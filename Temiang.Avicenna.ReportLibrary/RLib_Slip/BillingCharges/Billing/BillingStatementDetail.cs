using System.Linq;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;

    public partial class BillingStatementDetail : Report
    {
        public BillingStatementDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                InitializeComponent();
                string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
                string[] registrationNoList = new string[1];
                if (registrationNo.Contains(","))
                    registrationNoList = registrationNo.Split(',');

                decimal? downPayment = printJobParameters.FindByParameterName("DownPayment").ValueNumeric;
                decimal? paymentAmount = printJobParameters.FindByParameterName("PaymentAmount").ValueNumeric;
                if (paymentAmount > 0)
                    paymentAmount -= downPayment;

                DateTime? startDate = printJobParameters.FindByParameterName("StartDate").ValueDateTime;
                DateTime? endDate = printJobParameters.FindByParameterName("EndDate").ValueDateTime;
                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

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
                var refItem = new AppStandardReferenceItemQuery("p");
                var clsdt = new ClassQuery("q");
                var pay = new TransPaymentItemOrderQuery("r");
                var payib = new TransPaymentItemIntermBillQuery("s");

                cost.Select
                    (
                    // header
                    reg.ClassID,
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
                    item.ItemID.As("ItemIDDetail"),
                    item.ItemName,
                    refItem.ItemID.As("IDGroup"),
                    @"<CASE WHEN l.ParamedicCollectionName <> '' and l.ParamedicCollectionName is Not Null
                    THEN 'DR' 
                    ELSE 'RS' 
                    END AS 'ItemDR'>",
                    refItem.ItemName.As("NameBilling"),
                    charges.ClassID.As("ClassDt"),
                    clsdt.ClassName.As("ClassNameDt"),
                    charges.ExecutionDate.As("TransactionDate"),
                    unit2.ServiceUnitName.As("SUName"),
                    unit2.ServiceUnitID.As("SU"),
                    chargesItem.Price,
                    chargesItem.ChargeQuantity,
                    (chargesItem.DiscountAmount / chargesItem.ChargeQuantity).As("DiscountAmount"),
                    @"<CASE WHEN l.ParamedicCollectionName <> '' and l.ParamedicCollectionName is Not Null
                    THEN ' (' + l.ParamedicCollectionName + ')' 
                    ELSE '' 
                    END AS 'ParamedicCollectionName'>",
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
                cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
                cost.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

                // detail
                cost.InnerJoin(charges).On(cost.TransactionNo == charges.TransactionNo);
                cost.InnerJoin(chargesItem).On
                    (
                        cost.TransactionNo == chargesItem.TransactionNo &
                        cost.SequenceNo == chargesItem.SequenceNo
                    );
                cost.InnerJoin(clsdt).On(charges.ClassID == clsdt.ClassID);
                cost.InnerJoin(unit2).On(charges.ToServiceUnitID == unit2.ServiceUnitID);
                cost.InnerJoin(item).On(chargesItem.ItemID == item.ItemID);
                cost.LeftJoin(refItem).On
                    (
                    item.SRBillingGroup == refItem.ItemID &
                    refItem.StandardReferenceID == "BillingGroup"
                    );
                cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
                cost.LeftJoin(svc).On(chargesItem.ItemID == svc.ItemID);
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

                if (startDate != null && endDate != null)
                    cost.Where(charges.TransactionDate.Date().Between(startDate.Value.Date, endDate.Value.Date));

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
                            row["TotalPA"] = (decimal)row["TotalPA"] + (decimal)tmp["TotalPA"];
                            row["TotalGA"] = (decimal)row["TotalGA"] + (decimal)tmp["TotalGA"];
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
                    @"<'99' AS 'ClassID'>",
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.DateOfBirth,
                    patient.StreetName,
                    patient.City,
                    medic1.ParamedicName.As("ParamedicNameHeader"),
                    reg.RegistrationDateTime.As("DateRegistration"),
                    room.RoomName.Coalesce("''"),
                    @"<'-' AS 'ClassName'>",
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
                    "<" + downPayment + " as DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",

                    // detail
                    group.ItemGroupName,
                    @"<'RS' AS 'ItemDR'>",
                    item.ItemName,
                    item.ItemID.As("ItemIDDetail"),
                    @"<'010' AS 'IDGroup'>",
                    @"<'Farmasi' AS 'NameBilling'>",
                    @"<'-' AS 'ClassDt'>",
                    @"<'-' AS 'ClassNameDt'>",
                    presc.PrescriptionDate.As("TransactionDate"),
                    unit2.ServiceUnitName,
                    prescItem.Price,
                    prescItem.ResultQty.As("ChargeQuantity"),
                    (prescItem.DiscountAmount / prescItem.ResultQty).As("DiscountAmount"),
                    @"<'' AS 'ParamedicCollectionName'>",
                    cost.PatientAmount.As("TotalPA"),
                    cost.GuarantorAmount.As("TotalGA"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    item.ItemGroupID
                );

                // header
                cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
                cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
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

                if (startDate != null && endDate != null)
                    cost.Where(presc.PrescriptionDate.Date().Between(startDate.Value.Date, endDate.Value.Date));

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

                var healthcare = Healthcare.GetHealthcare();
                
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUser.Value = user.UserName;

                var oreg = new Registration();
                oreg.LoadByPrimaryKey(regNo);
                textBox21.Value = oreg.RegistrationNo;

                decimal pat = 0;
                decimal guar = 0;
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        pat += Convert.ToDecimal(row["TotalPA"]);
                        guar += Convert.ToDecimal(row["TotalGA"]);
                    }
                }

                if (oreg.PlavonAmount > 0)
                {
                    textBox35.Value = string.Format("{0:n0}", (oreg.PlavonAmount));
                    textBox39.Value = string.Format("{0:n0}", (guar + pat - oreg.PlavonAmount));
                }
                else
                {
                    textBox35.Value = string.Format("{0:n0}", (guar));
                    textBox39.Value = string.Format("{0:n0}", (pat));
                }

                var clsp = new Class();
                clsp.LoadByPrimaryKey(oreg.ClassID);
                var serv = new ServiceUnit();
                serv.LoadByPrimaryKey(oreg.ServiceUnitID);

                textBox26.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);
                if (oreg.SRRegistrationType != "IPR")
                {
                    txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.RegistrationDate);
                    textBox43.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                }
                else
                {
                    if (oreg.DischargeDate != null)
                    {
                        txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.DischargeDate);
                        textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " + oreg.DischargeTime;
                    }
                    else
                    {
                        textBox43.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                        if (startDate != null && endDate != null)
                            txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", startDate, endDate);

                        else
                            txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, DateTime.Now);
                    }
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }

                this.DataSource = table;
                table1.DataSource = table;
            }
        }
    }
}