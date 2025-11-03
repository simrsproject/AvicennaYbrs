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
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class BillingGuarantorStatementDetail : Report
    {
        public BillingGuarantorStatementDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();

                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;
                string paymentNo = printJobParameters.FindByParameterName("PaymentNo").ValueString;
                
                DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

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
                var clsp1 = new Class();
                clsp1.LoadByPrimaryKey(oreg.CoverageClassID);
                textBox51.Value = clsp1.ClassName;

                var clsp = new Class();
                clsp.LoadByPrimaryKey(oreg.ClassID);
                var serv = new ServiceUnit();
                serv.LoadByPrimaryKey(oreg.ServiceUnitID);

                textBox26.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate) + " " + oreg.RegistrationTime;
                if (oreg.SRRegistrationType != "IPR")
                {
                    textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate) + " " + oreg.RegistrationTime;
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                }
                else
                {
                    if (oreg.DischargeDate != null)
                        textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " +
                                          oreg.DischargeTime;
                    else
                        textBox43.Value = "";
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }
                if (printJobParameters.FindByParameterName("InitialRpt").ValueString == "1")
                    textBox40.Value = "No. Bukti : " + paymentNo;
                else
                {
                    textBox2.Value = "PERINCIAN TAGIHAN PASIEN (SESUAI KELAS YANG DIPILIH)";
                    textBox18.Value = "Unit/Kelas";
                    clsp = new Class();
                    clsp.LoadByPrimaryKey(printJobParameters.FindByParameterName("ClassID").ValueString);
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                    textBox40.Value = "";
                }

                var py = new TransPayment();
                if (py.LoadByPrimaryKey(paymentNo))
                {
                    if (py.IsToGuarantor == false && oreg.PlavonAmount > 0)
                    {
                        var payQuery = new TransPaymentItemIntermBillQuery("a");
                        var ibQuery = new IntermBillQuery("b");
                        payQuery.InnerJoin(ibQuery).On(payQuery.IntermBillNo == ibQuery.IntermBillNo);
                        payQuery.Where(payQuery.PaymentNo == paymentNo, payQuery.IsPaymentProceed == true,
                                      payQuery.IsPaymentReturned == false);
                        payQuery.Select(ibQuery.PatientAmount.Sum().As("PatientAmount"), ibQuery.GuarantorAmount.Sum().As("GuarantorAmount"));
                        DataTable dtb = payQuery.LoadDataTable();

                        decimal total = Convert.ToDecimal(dtb.Rows[0]["PatientAmount"]) +
                                        Convert.ToDecimal(dtb.Rows[0]["GuarantorAmount"]);
                        
                        textBox35.Value = string.Format("{0:n0}", (oreg.PlavonAmount));
                        textBox39.Value = string.Format("{0:n0}", (total - oreg.PlavonAmount));
                    }
                }

                table1.DataSource = DataSource;

                #region "Room History"

                var pthQ = new PatientTransferHistoryQuery("a");
                var suQ = new ServiceUnitQuery("b");
                pthQ.InnerJoin(suQ).On(pthQ.ServiceUnitID == suQ.ServiceUnitID);
                pthQ.Where(pthQ.RegistrationNo == regNo);
                pthQ.Select(pthQ.TransferNo, pthQ.ServiceUnitID, suQ.ServiceUnitName, pthQ.DateOfEntry, pthQ.TimeOfEntry,
                            @"<DateOfExit = ISNULL(a.DateOfExit, GETDATE())>",
                            @"<TimeOfExit = ISNULL(a.TimeOfExit, CONVERT (VARCHAR (5),GETDATE(),108))>",
                            @"<'' AS TransferNoRef>");
                pthQ.OrderBy(pthQ.TransferNo.Ascending);

                DataTable dtPth = pthQ.LoadDataTable();

                string unitIdBefore = string.Empty;
                string transNoBefore = string.Empty;

                foreach (DataRow row in dtPth.Rows)
                {
                    if (row["TransferNo"].ToString() == string.Empty)
                        row["TransferNo"] = "REG";

                    if (unitIdBefore == row["ServiceUnitID"].ToString())
                        row["TransferNoRef"] = transNoBefore;
                    else
                    {
                        unitIdBefore = row["ServiceUnitID"].ToString();
                        transNoBefore = row["TransferNo"].ToString();
                    }
                }
                dtPth.AcceptChanges();

                DataTable temp = dtPth.Copy();
                DataView view = temp.DefaultView;
                view.RowFilter = "TransferNoRef <> ''";
                temp = view.ToTable();
                view.Dispose();

                foreach (DataRow row in dtPth.Rows)
                {
                    if (row["TransferNoRef"].ToString() == string.Empty)
                    {
                        foreach (DataRow tmp in temp.Rows)
                        {
                            if (row["TransferNo"] == tmp["TransferNoRef"])
                            {
                                row["DateOfExit"] = tmp["DateOfExit"];
                                row["TimeOfExit"] = tmp["TimeOfExit"];
                            }
                        }
                    }
                    else
                        row.Delete();
                }

                dtPth.AcceptChanges();

                table3.DataSource = dtPth;

                #endregion

            }
        }
    }
}
