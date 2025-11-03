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
    public partial class BillingIntermStatementPatient : Report
    {
        public BillingIntermStatementPatient(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                InitializeComponent();

                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

                string IntermBillNo = printJobParameters.FindByParameterName("IntermBillNoList").ValueString;
                string[] IntermBillNoList = new string[1];
                if (IntermBillNo.Contains(","))
                    IntermBillNoList = IntermBillNo.Split(',');

                var oreg = new Registration();
                oreg.LoadByPrimaryKey(regNo);

                textBox21.Value = oreg.RegistrationNo;

                var healthcare = Healthcare.GetHealthcare();
                
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUser.Value = user.UserName;

                var clsp = new Class();
                clsp.LoadByPrimaryKey(oreg.ClassID);
                var serv = new ServiceUnit();
                serv.LoadByPrimaryKey(oreg.ServiceUnitID);

                var clsp1 = new Class();
                clsp1.LoadByPrimaryKey(oreg.CoverageClassID);
                textBox51.Value = clsp1.ClassName;

                textBox21.Value = oreg.RegistrationNo;
                textBox26.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate) + " " + oreg.RegistrationTime;

                DateTime startdate = oreg.RegistrationDate ?? DateTime.Now;
                DateTime enddate = oreg.DischargeDate ?? DateTime.Now;

                var ib = new IntermBillQuery();
                if (IntermBillNo.Contains(","))
                    ib.Where(ib.IntermBillNo.In(IntermBillNoList));
                else
                    ib.Where(ib.IntermBillNo == IntermBillNo);
                ib.Select(ib.StartDate);
                ib.OrderBy(ib.StartDate.Ascending);
                ib.es.Top = 1;
                DataTable dtib = ib.LoadDataTable();
                if (dtib.Rows.Count > 0)
                    startdate = Convert.ToDateTime(dtib.Rows[0]["StartDate"]);

                var ib2 = new IntermBillQuery();
                if (IntermBillNo.Contains(","))
                    ib2.Where(ib2.IntermBillNo.In(IntermBillNoList));
                else
                    ib2.Where(ib2.IntermBillNo == IntermBillNo);

                ib2.Select(ib2.EndDate);
                ib2.OrderBy(ib.EndDate.Descending);
                ib2.es.Top = 1;
                DataTable dtib2 = ib2.LoadDataTable();
                if (dtib2.Rows.Count > 0)
                    enddate = Convert.ToDateTime(dtib2.Rows[0]["EndDate"]);

                if (oreg.SRRegistrationType != "IPR")
                {
                    textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.RegistrationDate) + " " + oreg.RegistrationTime;
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                    txtPeriode.Value = string.Empty;
                    groupFooterSection2.Visible = false;
                    txtPeriode.Visible = false;
                }
                else
                {
                    groupFooterSection2.Visible = true;
                    txtPeriode.Visible = true;

                    txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}",
                                                 startdate.Date, enddate.Date);

                    if (oreg.DischargeDate != null)
                        textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " +
                                          oreg.DischargeTime;
                    else
                        textBox43.Value = "";

                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }
                textBox40.Value = "No.Interm Bill : " + IntermBillNo;

                DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
                table1.DataSource = DataSource;

                var iba = new IntermBillQuery();
                if (IntermBillNo.Contains(","))
                    iba.Where(iba.IntermBillNo.In(IntermBillNoList));
                else
                    iba.Where(iba.IntermBillNo == IntermBillNo);
                iba.Select(iba.PatientAmount.Sum().As("PatientAmount"), iba.GuarantorAmount.Sum().As("GuarantorAmount"));

                DataTable dtcc = iba.LoadDataTable();
                decimal pat = Convert.ToDecimal(dtcc.Rows[0]["PatientAmount"]);
                decimal guar = Convert.ToDecimal(dtcc.Rows[0]["GuarantorAmount"]);
                decimal total = Convert.ToDecimal(dtcc.Rows[0]["PatientAmount"]) + Convert.ToDecimal(dtcc.Rows[0]["GuarantorAmount"]);

                decimal plafond = Convert.ToDecimal(printJobParameters.FindByParameterName("plafond").ValueString);
                if (plafond > 0)
                {
                    if (oreg.GuarantorID == printJobParameters.FindByParameterName("AskesGuarantor").ValueString)
                    {
                        var ibacColl = new IntermBillCollection();
                        if (IntermBillNo.Contains(","))
                            ibacColl.Query.Where(ibacColl.Query.IntermBillNo.In(IntermBillNoList),
                                                 ibacColl.Query.AskesCoveredSeqNo != string.Empty);
                        else
                            ibacColl.Query.Where(ibacColl.Query.IntermBillNo == IntermBillNo,
                                                 ibacColl.Query.AskesCoveredSeqNo != string.Empty);
                        ibacColl.LoadAll();

                        decimal? plafondDetail = (from ibac in ibacColl let ac = new AskesCovered2() where ac.LoadByPrimaryKey(regNo, ibac.AskesCoveredSeqNo) select ac).Aggregate<AskesCovered2, decimal?>(0, (current, ac) => current + ((ac.RoomAmount * ac.RoomDays) + (ac.IccuAmount * ac.IccuDays) + (ac.HcuAmount * ac.HcuDays) + ac.SurgeryAmount + ac.MedicalSupportAmount + ac.HaemodialiseAmount + ac.CtScanAmount));

                        textBox35.Value = string.Format("{0:n0}", (plafondDetail > 0 ? plafondDetail : plafond));
                        textBox39.Value = string.Format("{0:n0}", (total - (plafondDetail > 0 ? plafondDetail : plafond)));
                    }
                    else
                    {
                        textBox35.Value = string.Format("{0:n0}", (plafond));
                        textBox39.Value = string.Format("{0:n0}", (total - plafond));
                    }
                }
                else
                {
                    textBox35.Value = string.Format("{0:n0}", (guar));
                    textBox39.Value = string.Format("{0:n0}", (pat));
                }

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
