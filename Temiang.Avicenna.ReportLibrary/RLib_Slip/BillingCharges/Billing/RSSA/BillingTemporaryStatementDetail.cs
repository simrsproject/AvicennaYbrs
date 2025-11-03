using System.Linq;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;

    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class BillingTemporaryStatementDetail : Report
    {
        public BillingTemporaryStatementDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                //Helper.InitializeNoLogoBigFont(this.pageHeader);
                string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
                string[] registrationNoList = new string[1];
                if (registrationNo.Contains(","))
                    registrationNoList = registrationNo.Split(',');

               
                DateTime? endDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value.AddDays(1);
                DateTime? TranDate = printJobParameters.FindByParameterName("TransDate").ValueDateTime.Value;
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
                    unit2.ServiceUnitName.As( "SUName"),
                    unit2.ServiceUnitID.As("SU"),
                    chargesItem.Price,
                    chargesItem.ChargeQuantity,
                    (chargesItem.DiscountAmount / chargesItem.ChargeQuantity).As("DiscountAmount"),
                    @"<CASE WHEN l.ParamedicCollectionName <> '' and l.ParamedicCollectionName is Not Null
                    THEN ' (' + l.ParamedicCollectionName + ')' 
                    ELSE '' 
                    END AS 'ParamedicCollectionName'>",
                    cost.PatientAmount.As( "TotalPA"),
                    cost.GuarantorAmount.As( "TotalGA"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    chargesItem.TransactionNo,
                    chargesItem.SequenceNo,
                    chargesItem.ReferenceNo,
                    chargesItem.ReferenceSequenceNo,
                    item.ItemGroupID,
                    svc.IsPrintWithDoctorName.Coalesce("0"),
                    @"<CAST(0 AS BIT) AS 'IsDeleted'>"
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

                cost.Where(charges.TransactionDate.Date() < endDate.Value.Date);

                cost.Where(
                    cost.Or(
                        cost.ParentNo == string.Empty,
                        cost.ParentNo.IsNull()
                        ),
                    pay.PaymentNo.IsNull(),
                    payib.PaymentNo.IsNull(),
                    cost.IntermBillNo.IsNull()
                    );

                cost.OrderBy
                    (
                        cost.RegistrationNo.Ascending,
                        charges.TransactionNo.Ascending,
                        chargesItem.SequenceNo.Ascending
                    );

                DataTable table = cost.LoadDataTable(), temp = table.Copy(),
                temp2 = table.Copy();

                DataView view = temp.DefaultView;
                view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
                temp = view.ToTable();
                view.Dispose();

                DataView view2 = temp2.DefaultView;
                view2.RowFilter = "ReferenceNo = '' AND ReferenceSequenceNo = ''";
                temp2 = view2.ToTable();
                view2.Dispose();

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
                    else
                    {
                        foreach (
                            DataRow tmp in
                                temp2.Rows.Cast<DataRow>().Where(
                                    tmp => row["ReferenceNo"].ToString() == tmp["TransactionNo"].ToString() &&
                                           row["ReferenceSequenceNo"].ToString() == tmp["SequenceNo"].ToString()))
                        {
                            row["IsDeleted"] = true;
                        }
                    }
                }

                table.AcceptChanges();

                foreach (
                    DataRow row in
                        table.Rows.Cast<DataRow>().Where(
                            row => (((decimal)row["Total"] == 0) && ((decimal)row["ChargeQuantity"] == 0)) || ((bool)row["IsDeleted"])))
                {
                    row.Delete();
                }

                table.AcceptChanges();

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
                    prescItem.PrescriptionNo.As("TransactionNo"),
                    prescItem.SequenceNo,
                    presc.ReferenceNo,
                    item.ItemGroupID,
                    @"<CAST(0 AS BIT) AS 'IsDeleted'>"
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

                    cost.Where(presc.PrescriptionDate.Date() < endDate.Value.Date);

                cost.Where(
                    cost.Or(
                        cost.ParentNo == string.Empty,
                        cost.ParentNo.IsNull()
                        ),
                    pay.PaymentNo.IsNull(),
                    payib.PaymentNo.IsNull(),
                    cost.IntermBillNo.IsNull(),
                    payReff.PaymentNo.IsNull(),
                    payibReff.PaymentNo.IsNull(),
                    costReff.IntermBillNo.IsNull()
                    );

                cost.OrderBy
                    (
                        cost.RegistrationNo.Ascending,
                        presc.PrescriptionNo.Ascending,
                        prescItem.SequenceNo.Ascending
                    );

                DataTable table2 = cost.LoadDataTable(), temp3 = table2.Copy(), temp4 = table2.Copy();

                DataView view3 = temp3.DefaultView;
                view3.RowFilter = "ReferenceNo <> ''";
                temp3 = view3.ToTable();
                view3.Dispose();

                DataView view4 = temp4.DefaultView;
                view4.RowFilter = "ReferenceNo = ''";
                temp4 = view4.ToTable();
                view4.Dispose();

                foreach (DataRow row in table2.Rows)
                {
                    if (row["ReferenceNo"].ToString() == string.Empty)
                    {
                        foreach (
                            DataRow tmp in
                                temp3.Rows.Cast<DataRow>().Where(
                                    tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                                           row["SequenceNo"].ToString() == tmp["SequenceNo"].ToString()))
                        {
                            row["TotalPA"] = (decimal)row["TotalPA"] + (decimal)tmp["TotalPA"];
                            row["TotalGA"] = (decimal)row["TotalGA"] + (decimal)tmp["TotalGA"];
                            row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
                            row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
                        }
                    }
                    else
                    {
                        foreach (
                            DataRow tmp in
                                temp4.Rows.Cast<DataRow>().Where(
                                    tmp => row["ReferenceNo"].ToString() == tmp["TransactionNo"].ToString() &&
                                           row["SequenceNo"].ToString() == tmp["SequenceNo"].ToString()))
                        {
                            row["IsDeleted"] = true;
                        }
                    }
                }

                table2.AcceptChanges();

                foreach (
                    DataRow row in
                        table2.Rows.Cast<DataRow>().Where(
                            row => (((decimal)row["Total"] == 0) && ((decimal)row["ChargeQuantity"] == 0)) || ((bool)row["IsDeleted"])))
                {
                    row.Delete();
                }

                table2.AcceptChanges();

                table.Merge(table2);

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
                
                var cc = new CostCalculationQuery("a");
                var py = new TransPaymentItemOrderQuery("b");
                var pyib = new TransPaymentItemIntermBillQuery("f");
                var prsc = new TransPrescriptionQuery("g");
                var qReff = new CostCalculationQuery("h");
                var pyReff = new TransPaymentItemOrderQuery("j");
                var pyibReff = new TransPaymentItemIntermBillQuery("k");

                cc.LeftJoin(py).On(cc.TransactionNo == py.TransactionNo && cc.SequenceNo == py.SequenceNo &&
                                   py.IsPaymentProceed == true && py.IsPaymentReturned == false);
                cc.LeftJoin(pyib).On(
                    cc.IntermBillNo == pyib.IntermBillNo &&
                    pyib.IsPaymentProceed == true &&
                    pyib.IsPaymentReturned == false
                    );

                cc.LeftJoin(prsc).On(cc.TransactionNo == prsc.PrescriptionNo);
                cc.LeftJoin(qReff).On(
                    prsc.ReferenceNo == qReff.TransactionNo && cc.SequenceNo == qReff.SequenceNo);
                cc.LeftJoin(pyReff).On(
                                    qReff.TransactionNo == pyReff.TransactionNo &&
                                    qReff.SequenceNo == pyReff.SequenceNo &&
                                    pyReff.IsPaymentProceed == true &&
                                    pyReff.IsPaymentReturned == false
                                    );
                cc.LeftJoin(pyibReff).On(
                    qReff.IntermBillNo == pyibReff.IntermBillNo &&
                    pyibReff.IsPaymentProceed == true &&
                    pyibReff.IsPaymentReturned == false);



                cc.Select(cc.PatientAmount.Sum().As("PatientAmount"), cc.GuarantorAmount.Sum().As("GuarantorAmount"));
                cc.Where(py.PaymentNo.IsNull(), pyib.PaymentNo.IsNull(),
                    pyReff.PaymentNo.IsNull(),
                    pyibReff.PaymentNo.IsNull());
                cc.Where(cc.LastUpdateDateTime < endDate);
                if (registrationNo.Contains(","))
                    cc.Where(cc.RegistrationNo.In(registrationNoList));
                else
                    cc.Where(cc.RegistrationNo == registrationNo);
                
                DataTable dtcc = cc.LoadDataTable();
                decimal pat = Convert.ToDecimal(dtcc.Rows[0]["PatientAmount"]);
                decimal guar = Convert.ToDecimal(dtcc.Rows[0]["GuarantorAmount"]);

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

                textBox26.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate) + " " + oreg.RegistrationTime;
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
                        //textBox43.Value = '';
                        txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, TranDate);
                    }
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }

                this.DataSource = table;
                table1.DataSource = table;
            }
        }
    }
}