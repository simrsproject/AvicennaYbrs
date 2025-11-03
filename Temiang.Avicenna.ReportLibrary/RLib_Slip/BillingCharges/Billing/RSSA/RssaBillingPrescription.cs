using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;


namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;


    /// <summary>
    /// Summary description for RssaBillingPrescription.
    /// </summary>
    public partial class RssaBillingPrescription : Report
    {
        public RssaBillingPrescription(string programID, PrintJobParameterCollection printJobParameters)
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

                DateTime? startDate = null;
                DateTime? endDate = null;

                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

                string IntermBillNo = printJobParameters.FindByParameterName("IntermBillNoList").ValueString;
                string[] IntermBillNoList = new string[1];
                if (IntermBillNo.Contains(","))
                    IntermBillNoList = IntermBillNo.Split(',');


//                #region prescription

//                var cost = new CostCalculationQuery("a");
//                var reg = new RegistrationQuery("b");
//                var patient = new PatientQuery("c");
//                var medic1 = new ParamedicQuery("d");
//                var room = new ServiceRoomQuery("f");
//                var grr = new GuarantorQuery("h");
//                var item = new ItemQuery("i");
//                var group = new ItemGroupQuery("j");
//                var presc = new TransPrescriptionQuery("k");
//                var prescItem = new TransPrescriptionItemQuery("l");
//                var unit2 = new ServiceUnitQuery("n");
//                var pay = new TransPaymentItemOrderQuery("o");

//                cost.Select
//                    (
//                         header
//                    @"<'99' AS 'ClassID'>",
//                    reg.RegistrationNo,
//                    patient.MedicalNo,
//                    patient.PatientName,
//                    patient.DateOfBirth,
//                    patient.StreetName,
//                    patient.City,
//                    medic1.ParamedicName.As("ParamedicNameHeader"),
//                    reg.RegistrationDateTime.As("DateRegistration"),
//                    room.RoomName.Coalesce("''"),
//                    @"<'-' AS 'ClassName'>",
//                    reg.BedID.Coalesce("''"),
//                    grr.GuarantorName,
//                    reg.DischargeDate,
//                    reg.DischargeTime,
//                    @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
//            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
//                    ELSE             	 
//            	        CASE 
//                              WHEN b.DischargeDate Is Not NULL THEN 	
//                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
//                              ELSE GETDATE()	
//                        END	
//                      END AS 'DischargeDates'>",


//                     footer
//                    reg.AdministrationAmount,
//                    reg.PlavonAmount,

//                     detail
//                    group.ItemGroupName,
//                    item.ItemName,
//                    item.ItemID.As("ItemIDDetail"),
//                    @"<'010' AS 'IDGroup'>",
//                    @"<'Farmasi' AS 'NameBilling'>",
//                    @"<'-' AS 'ClassDt'>",
//                    @"<'-' AS 'ClassNameDt'>",
//                    presc.PrescriptionNo,
//                    presc.PrescriptionDate.As("TransactionDate"),
//                    unit2.ServiceUnitName,
//                    prescItem.Price,
//                    prescItem.ResultQty.As("ChargeQuantity"), 
//                    @"<CASE WHEN l.ResultQty > 0 THEN
//                        '' Else '(Retur)' End AS 'Jenis'>",
//                    prescItem.DiscountAmount,
//                    @"<'' AS 'ParamedicCollectionName'>",
//                    cost.PatientAmount.As("TotalPA"),
//                    cost.GuarantorAmount.As("TotalGA"),
//                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
//                    item.ItemGroupID
//                    );

//                 header
//                cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
//                cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
//                cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
//                cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
//                cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

//                 detail
//                cost.InnerJoin(presc).On(cost.TransactionNo == presc.PrescriptionNo);
//                cost.InnerJoin(prescItem).On
//                    (
//                        presc.PrescriptionNo == prescItem.PrescriptionNo &
//                        cost.SequenceNo == prescItem.SequenceNo
//                    );

//                cost.InnerJoin(unit2).On(presc.ServiceUnitID == unit2.ServiceUnitID);
//                cost.InnerJoin(item).On(prescItem.ItemID == item.ItemID);
//                cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
//                cost.LeftJoin(pay).On(
//                    cost.TransactionNo == pay.TransactionNo && cost.SequenceNo == pay.SequenceNo &&
//                    pay.IsPaymentProceed == true && pay.IsPaymentReturned == false);

//                if (IntermBillNo.Contains(","))
//                    cost.Where(cost.IntermBillNo.In(IntermBillNoList));
//                else
//                    cost.Where(cost.IntermBillNo == IntermBillNo);
//                cost.Where(
//                    cost.Or(
//                        cost.ParentNo == string.Empty,
//                        cost.ParentNo.IsNull()
//                        ),
//                    pay.PaymentNo.IsNull()
//                    );

//                cost.OrderBy
//                    (
//                        cost.RegistrationNo.Ascending,
//                        presc.PrescriptionNo.Ascending,
//                        prescItem.SequenceNo.Ascending
//                    );

//                DataTable table = cost.LoadDataTable();

                
//                #endregion
                DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
                table1.DataSource = DataSource;

                var healthcare = Healthcare.GetHealthcare();
                
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUser.Value = user.UserName;

                var oreg = new Registration();
                oreg.LoadByPrimaryKey(regNo);
                textBox21.Value = oreg.RegistrationNo;

                //decimal tpatient = 0;
                //decimal tguarantor = 0;
                //foreach (DataRow row in DataSource.Rows)
                //{
                //    tpatient += Convert.ToDecimal(row["TotalPA"]);
                //    tguarantor += Convert.ToDecimal(row["TotalGA"]);
                //}
                //textBox35.Value = string.Format("{0:n0}", tguarantor);
                //textBox39.Value = string.Format("{0:n0}", tpatient);
                //textBox10.Value = string.Format("{0:n0}", tpatient + tguarantor);

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

                //this.DataSource = table;
                //table1.DataSource = table;

                }
            
        }
    }
}