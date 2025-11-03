using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientPrescriptionDetail : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = TransPrescriptions;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string prescriptionNo = dataItem.GetDataKeyValue("PrescriptionNo").ToString();

            //Load record
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var cc = new CostCalculationQuery("c");
            var qItemInt = new ItemQuery("d");

            query.Select
                    (
                        query,
                        qItem.ItemName,
                        qItemInt.ItemName.As("ItemInterventionName"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        query.DiscountAmount,
                        "<c.PatientAmount + c.GuarantorAmount as Total>"
                    );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemInt).On(query.ItemInterventionID == qItemInt.ItemID);
            query.InnerJoin(cc).On(query.PrescriptionNo == cc.TransactionNo && query.SequenceNo == cc.SequenceNo);
            query.Where(query.PrescriptionNo == prescriptionNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable TransPrescriptions
        {
            get
            {
                string patientID = Request.QueryString["patientID"].ToString();

                DataTable dtdTransPrescription = (new TransPrescriptionCollection()).TransPrescriptionHistory(patientID, txtRegistrationNo.Text);

                return dtdTransPrescription;
            }
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                var jobParameters = new PrintJobParameterCollection();

                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                {
                    case "RSSA":
                    case "RSGPI":
                        var jobParameter11 = jobParameters.AddNew();
                        jobParameter11.Name = "p_PrescriptionNo";
                        jobParameter11.ValueString = e.CommandArgument.ToString();

                        var jobParameter12 = jobParameters.AddNew();
                        jobParameter12.Name = "p_Label";
                        jobParameter12.ValueString = "";

                        var jobParameter13 = jobParameters.AddNew();
                        jobParameter13.Name = "temp_TITLE";
                        jobParameter13.ValueString = "COPY RESEP";

                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionSlip;
                        break;
                    case "RSCH":
                        var jobParameterRsch = jobParameters.AddNew();
                        jobParameterRsch.Name = "p_HealthcareID";
                        jobParameterRsch.ValueString = AppSession.Parameter.HealthcareID;

                        var jobParameterRsch2 = jobParameters.AddNew();
                        jobParameterRsch2.Name = "p_PrescriptionNo";
                        jobParameterRsch2.ValueString = e.CommandArgument.ToString();
                        
                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionSlip;
                        break;
                    default:
                        var jobParameter01 = jobParameters.AddNew();
                        jobParameter01.Name = "p_PrescriptionNo";
                        jobParameter01.ValueString = e.CommandArgument.ToString();

                        var jobParameter02 = jobParameters.AddNew();
                        jobParameter02.Name = "p_Label";
                        jobParameter02.ValueString = "";

                        var jobParameter03 = jobParameters.AddNew();
                        jobParameter03.Name = "temp_TITLE";
                        jobParameter03.ValueString = "COPY RESEP";

                        AppSession.PrintJobReportID = AppConstant.Report.PrescriptionSlip;
                        break;
                }

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Print2")
            {
                var jobParameters = new PrintJobParameterCollection();

                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                {
                    case "RSSA":
                    case "RSGPI":
                        var jobParameter11 = jobParameters.AddNew();
                        jobParameter11.Name = "p_PrescriptionNo";
                        jobParameter11.ValueString = e.CommandArgument.ToString();

                        var jobParameter12 = jobParameters.AddNew();
                        jobParameter12.Name = "p_Label";
                        jobParameter12.ValueString = "";

                        var jobParameter13 = jobParameters.AddNew();
                        jobParameter13.Name = "temp_TITLE";
                        jobParameter13.ValueString = "NOTA RESEP";

                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionReceiptSlip;
                        break;
                    case "RSCH":
                        var jobParameterRsch = jobParameters.AddNew();
                        jobParameterRsch.Name = "p_PrescriptionNo";
                        jobParameterRsch.ValueString = e.CommandArgument.ToString();

                        var jobParameterRsch2 = jobParameters.AddNew();
                        jobParameterRsch2.Name = "p_Label";
                        jobParameterRsch2.ValueString = "";

                        var jobParameterRsch3 = jobParameters.AddNew();
                        jobParameterRsch3.Name = "p_UserID";
                        jobParameterRsch3.ValueString = AppSession.UserLogin.UserID;

                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionReceiptSlip;
                        break;
                    case "RSYS":
                        var jobParameterRSYS1 = jobParameters.AddNew();
                        jobParameterRSYS1.Name = "p_PrescriptionNo";
                        jobParameterRSYS1.ValueString = e.CommandArgument.ToString();


                        var jobParameterRSYS2 = jobParameters.AddNew();
                        jobParameterRSYS2.Name = "p_SequenceNo";
                        jobParameterRSYS2.ValueString = "";

                        var jobParameterRSYS3 = jobParameters.AddNew();
                        jobParameterRSYS3.Name = "p_Label";
                        jobParameterRSYS3.ValueString = "";

                        var jobParameterRSYS4 = jobParameters.AddNew();
                        jobParameterRSYS4.Name = "p_UserID";
                        jobParameterRSYS4.ValueString = AppSession.UserLogin.UserID;

                        var jobParameterRSYS5 = jobParameters.AddNew();
                        jobParameterRSYS5.Name = "temp_TITLE";
                        jobParameterRSYS5.ValueString = "NOTA RESEP";

                        AppSession.PrintJobReportID = AppConstant.Report.RSSA_PrescriptionReceiptSlip;
                        break;
                    default:
                        var jobParameter01 = jobParameters.AddNew();
                        jobParameter01.Name = "p_PrescriptionNo";
                        jobParameter01.ValueString = e.CommandArgument.ToString();

                        var jobParameter02 = jobParameters.AddNew();
                        jobParameter02.Name = "p_Label";
                        jobParameter02.ValueString = "";

                        var jobParameter03 = jobParameters.AddNew();
                        jobParameter03.Name = "temp_TITLE";
                        jobParameter03.ValueString = "NOTA RESEP";
                        AppSession.PrintJobReportID = AppConstant.Report.PrescriptionReceiptSlip;
                        break;
                }

                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}