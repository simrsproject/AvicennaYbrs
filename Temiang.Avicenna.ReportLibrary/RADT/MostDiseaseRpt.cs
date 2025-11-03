using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RADT
{

    public partial class MostDiseaseRpt : Report
    {
        public MostDiseaseRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //Test Parameter
            //printJobParameters.AddNew("p_FromDate", new System.DateTime(2010, 05, 19));
            //printJobParameters.AddNew("p_ToDate", new System.DateTime(2010, 05, 24));
            //----------------


            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            txtPeriod.Value = string.Format("Tanggal {0:dd/MM/yyyy} s/d {1:dd/MM/yyyy}", fromDate, toDate);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.RegistrationType.ToString(), 
                printJobParameters.FindByParameterName("p_RegistrationType").ValueString);
            textBox33.Value = std.str.ItemName;
        }


    }
}