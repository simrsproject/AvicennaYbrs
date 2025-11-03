namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSSA
{
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System;

    public partial class PurchaseOrderSummaryPerPeriodRpt : Telerik.Reporting.Report
    {
        public PurchaseOrderSummaryPerPeriodRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
    
            Helper.InitializeLogo(pageHeader);
            DataTable dt = Helper.ReportDataSource(programID, printJobParameters);

            this.DataSource = dt;
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox9.Value = string.Format("Periode :  {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

            string managingDirector = string.Empty;
            string picPurchasing = string.Empty;

            var app = new AppParameter();
            app.LoadByPrimaryKey("PicManagingDirector");
            managingDirector = app.ParameterValue;
            txtPicManagingDirector.Value = managingDirector;
            app = new AppParameter();
            app.LoadByPrimaryKey("PicPurchasing");
            picPurchasing = app.ParameterValue;
            txtpicPurchasing.Value = picPurchasing;
            if (printJobParameters.FindByParameterName("p_ServiceUnitID").ValueString == "PU04")
            {
                textBox24.Visible = false;
                textBox25.Visible = false;
                textBox26.Visible = false;
                textBox27.Visible = false;
                textBox28.Visible = false;
                txtpicPurchasing.Visible = false;
                txtPicManagingDirector.Visible = false;
            }
            else
            {

                textBox24.Visible = true;
                textBox25.Visible = true;
                textBox26.Visible = true;
                textBox27.Visible = true;
                textBox28.Visible = true;
                txtpicPurchasing.Visible = true;
                txtPicManagingDirector.Visible = true;
            }

        }
    }
}