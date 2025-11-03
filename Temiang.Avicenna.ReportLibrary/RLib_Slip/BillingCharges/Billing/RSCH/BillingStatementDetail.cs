using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for BillingStatementDetail.
    /// </summary>
    public partial class BillingStatementDetail : Telerik.Reporting.Report
    {
        public BillingStatementDetail(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            else
                registrationNoList.SetValue(registrationNo, 0);

            //var cost = new CostCalculationQuery("a");
            //var reg = new RegistrationQuery("b");
            //var patient = new PatientQuery("c");
            //var grr = new GuarantorQuery("d");
            //var cHeader = new TransChargesQuery("e");
            //var cDetail = new TransChargesItemQuery("f");
            //var item = new ItemQuery("g");
            //var svc = new ItemServiceQuery("h");
            //var igrp = new ItemGroupQuery("i");
            //var appstd = new AppStandardReferenceItemQuery("j");

            //cost.Select(
            //    @"<CASE WHEN b.SRRegistrationType = 'OPR' THEN '01. Biaya Rawat Jalan' " +
            //           "WHEN b.SRRegistrationType = 'IPR' THEN '02. Biaya Rawat Inap' " +
            //           "ELSE '' END AS RegistrationTypeName>",
            //    patient.MedicalNo,
            //    patient.PatientName,
            //    reg.RegistrationNo,
            //    reg.RegistrationDate,
            //    reg.DischargeDate,
            //    grr.GuarantorName,
            //    igrp.ItemGroupName,
            //    appstd.ItemID.As("GroupBillingID"),
            //    appstd.ItemName.As("GroupBilling"),
            //    cHeader.TransactionNo,
            //    cDetail.ReferenceNo,
            //    "<CAST(CONVERT(VARCHAR(10), e.ExecutionDate, 101) AS DATETIME) AS TransactionDate>",
            //    cDetail.SequenceNo,
            //    cDetail.ReferenceSequenceNo,
            //    "<'' AS NoResep>",
            //    item.ItemID,
            //    item.ItemName,
            //    cDetail.ChargeQuantity,
            //    cDetail.SRItemUnit,
            //    cost.PatientAmount,
            //    cost.GuarantorAmount,
            //    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
            //    "<'' AS ParamedicName>",
            //    svc.IsPrintWithDoctorName.Coalesce("0")
            //    );
            //cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            //cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            //cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            //cost.InnerJoin(cHeader).On(cost.TransactionNo == cHeader.TransactionNo);
            //cost.InnerJoin(cDetail).On(
            //    cost.TransactionNo == cDetail.TransactionNo &&
            //    cost.SequenceNo == cDetail.SequenceNo
            //    );
            //cost.InnerJoin(item).On(cDetail.ItemID == item.ItemID);
            //cost.LeftJoin(svc).On(item.ItemID == svc.ItemID);
            //cost.LeftJoin(igrp).On(item.ItemGroupID == igrp.ItemGroupID);
            //cost.LeftJoin(appstd).On(item.SRBillingGroup == appstd.ItemID &&
            //    appstd.StandardReferenceID == "BillingGroup");
            //cost.Where(cost.RegistrationNo.In(registrationNoList));

            //DataTable table = cost.LoadDataTable(), temp = table.Copy();

            //DataView view = temp.DefaultView;
            //view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
            //temp = view.ToTable();
            //view.Dispose();


            //foreach (DataRow row in table.Rows)
            //{
            //    if (string.IsNullOrEmpty(row["ReferenceNo"].ToString()) && string.IsNullOrEmpty(row["ReferenceSequenceNo"].ToString()))
            //    {
            //        foreach (DataRow tmp in temp.Rows.Cast<DataRow>().Where(tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
            //                                                                       row["SequenceNo"].ToString() == tmp["ReferenceSequenceNo"].ToString()))
            //        {
            //            row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
            //            row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
            //        }
            //    }
            //}

            //foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => ((decimal)row["Total"] <= 0) && ((decimal)row["ChargeQuantity"] <= 0)))
            //{
            //    row.Delete();
            //}

            //table.AcceptChanges();

            //foreach (DataRow transaction in table.Rows.Cast<DataRow>().Where(row => row.Field<int>("IsPrintWithDoctorName") == 1))
            //{
            //    var component = new TransChargesItemComp();
            //    component.Query.Where(
            //        component.Query.TransactionNo == transaction["TransactionNo"].ToString(),
            //        component.Query.SequenceNo == transaction["SequenceNo"].ToString(),
            //        component.Query.TariffComponentID == AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTariffComponentID)
            //        );
            //    if (component.Query.Load())
            //    {
            //        var medic = new Paramedic();
            //        medic.LoadByPrimaryKey(component.ParamedicID);

            //        transaction["ParamedicName"] = medic.ParamedicName;
            //        transaction["ItemName"] = transaction["ItemName"] + ", " + medic.ParamedicName;
            //    }
            //}

            //var chargeBedItems = new ServiceRoomCollection();
            //chargeBedItems.Query.Where(chargeBedItems.Query.ItemID != string.Empty);
            //chargeBedItems.LoadAll();

            //var chargeBeds = table.AsEnumerable().Where(t => chargeBedItems.Select(c => c.ItemID).Contains(t.Field<string>("ItemID")));
            //if (chargeBedItems != null)
            //{
            //    foreach (DataRow chargeBed in chargeBeds)
            //    {
            //        chargeBed["ItemName"] = chargeBed["ItemName"].ToString() + " (" + chargeBed["PatientName"].ToString() + ")";
            //    }
            //}

            //// resep
            //cost = new CostCalculationQuery("a");
            //reg = new RegistrationQuery("b");
            //patient = new PatientQuery("c");
            //grr = new GuarantorQuery("d");
            //var pHeader = new TransPrescriptionQuery("e");
            //var pDetail = new TransPrescriptionItemQuery("f");
            //item = new ItemQuery("g");
            //igrp = new ItemGroupQuery("h");

            //cost.Select(
            //    @"<'03. Resep Farmasi' AS RegistrationTypeName>",
            //    patient.MedicalNo,
            //    patient.PatientName,
            //    reg.RegistrationNo,
            //    reg.RegistrationDate,
            //    reg.DischargeDate,
            //    grr.GuarantorName,
            //    igrp.ItemGroupName,
            //    "<'010' AS GroupBillingID>",
            //    "<'Farmasi' AS GroupBilling>",
            //    pHeader.PrescriptionNo.As("TransactionNo"),
            //    "<'' AS ReferenceNo>",
            //    pHeader.PrescriptionDate.As("TransactionDate"),
            //    pDetail.SequenceNo,
            //    "<'' AS ReferenceSequenceNo>",
            //    pHeader.PrescriptionNo.As("NoResep"),
            //    item.ItemID,
            //    item.ItemName,
            //    pDetail.ResultQty.As("ChargeQuantity"),
            //    pDetail.SRItemUnit,
            //    cost.PatientAmount,
            //    cost.GuarantorAmount,
            //    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
            //    "<'' AS ParamedicName>",
            //    "<0 AS IsPrintWithDoctorName>"
            //    );
            //cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            //cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            //cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            //cost.InnerJoin(pHeader).On(cost.TransactionNo == pHeader.PrescriptionNo);
            //cost.InnerJoin(pDetail).On(
            //    cost.TransactionNo == pDetail.PrescriptionNo &&
            //    cost.SequenceNo == pDetail.SequenceNo
            //    );
            //cost.InnerJoin(item).On(pDetail.ItemID == item.ItemID);
            //cost.LeftJoin(igrp).On(item.ItemGroupID == igrp.ItemGroupID);
            //cost.Where(
            //    cost.RegistrationNo.In(registrationNoList),
            //    pHeader.IsPrescriptionReturn == false
            //    );

            //table.Merge(cost.LoadDataTable());

            //// retur resep
            //cost = new CostCalculationQuery("a");
            //reg = new RegistrationQuery("b");
            //patient = new PatientQuery("c");
            //grr = new GuarantorQuery("d");
            //pHeader = new TransPrescriptionQuery("e");
            //pDetail = new TransPrescriptionItemQuery("f");
            //item = new ItemQuery("g");
            //igrp = new ItemGroupQuery("h");

            //cost.Select(
            //    @"<'04. Retur Resep Farmasi' AS RegistrationTypeName>",
            //    patient.MedicalNo,
            //    patient.PatientName,
            //    reg.RegistrationNo,
            //    reg.RegistrationDate,
            //    reg.DischargeDate,
            //    grr.GuarantorName,
            //    igrp.ItemGroupName,
            //    "<'015' AS GroupBillingID>",
            //    "<'Retur Obat' AS GroupBilling>",
            //    pHeader.PrescriptionNo.As("TransactionNo"),
            //    "<'' AS ReferenceNo>",
            //    pHeader.PrescriptionDate.As("TransactionDate"),
            //    pDetail.SequenceNo,
            //    "<'' AS ReferenceSequenceNo>",
            //    pHeader.PrescriptionNo.As("NoResep"),
            //    item.ItemID,
            //    item.ItemName,
            //    pDetail.ResultQty.As("ChargeQuantity"),
            //    pDetail.SRItemUnit,
            //    cost.PatientAmount,
            //    cost.GuarantorAmount,
            //    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
            //    "<'' AS ParamedicName>",
            //    "<0 AS IsPrintWithDoctorName>"
            //    );
            //cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            //cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            //cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            //cost.InnerJoin(pHeader).On(cost.TransactionNo == pHeader.PrescriptionNo);
            //cost.InnerJoin(pDetail).On(
            //    cost.TransactionNo == pDetail.PrescriptionNo &&
            //    cost.SequenceNo == pDetail.SequenceNo
            //    );
            //cost.InnerJoin(item).On(pDetail.ItemID == item.ItemID);
            //cost.LeftJoin(igrp).On(item.ItemGroupID == igrp.ItemGroupID);
            //cost.Where(
            //    cost.RegistrationNo.In(registrationNoList),
            //    pHeader.IsPrescriptionReturn == true
            //    );

            //table.Merge(cost.LoadDataTable());

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            decimal adm = regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal total = table.AsEnumerable().Sum(t => t.Field<decimal>("Total")) +
            //                regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal tpatient = table.AsEnumerable().Sum(t => t.Field<decimal>("PatientAmount"));
            //decimal tguarantor = table.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount"));

            //textBox31.Value = string.Format("{0:N2}", adm);
            //textBox33.Value = string.Format("{0:N2}", total);
            textBox41.Value = string.Format("{0:N2}", Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList));
            //textBox37.Value = string.Format("{0:N2}", total - (Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList)));
            textBox39.Value = printJobParameters.FindByParameterName("UserName").ValueString;



            var regis = new Registration();
            regis.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString);
            if (regis.GuarantorID == printJobParameters.FindByParameterName("SelfGuarantor").ValueString)
            {

            }
            else
            {

            }

            if (regis.PlavonAmount > 0)
            {
                //textBox41.Value = string.Format("{0:N2}", regis.PlavonAmount);
                //textBox44.Value = string.Format("{0:N2}", total - regis.PlavonAmount);
            }
            else
            {
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(regis.GuarantorID);

                if (guar.IsIncludeAdminValue ?? false)
                {
                    //textBox41.Value = string.Format("{0:N2}", tguarantor + adm);
                    //textBox44.Value = string.Format("{0:N2}", tpatient);
                }
                else
                {
                    //textBox41.Value = string.Format("{0:N2}", tguarantor);
                    //textBox44.Value = string.Format("{0:N2}", tpatient + adm);
                }
            }
            textBox48.Value = string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate);
            textBox54.Value = string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate);
            textBox10.Value = printJobParameters.FindByParameterName("RegNo").ValueString;

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;
        }
    }
}