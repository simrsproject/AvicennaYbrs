using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSSA
{
    
   
    public partial class EvaluationPORpt : Report
    {
        public EvaluationPORpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            Helper.InitializeDataSource(this, programID, printJobParameters);
                       

            DateTime? fromDate = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            DateTime? toDate = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;

            var user = new AppUser();
            user.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_UserID").ValueString);
            TxtUser.Value = user.UserName;

            var type = new AppStandardReferenceItem();
            type.LoadByPrimaryKey("ItemType", printJobParameters.FindByParameterName("p_ItemType").ValueString);
            textBox15.Value = type.ItemName;
            //textBox19.Value =
            textBox2.Value = string.Format("Tanggal {0:dd-MMM-yyyy} s/d {1:dd-MMM-yyyy}", fromDate, toDate);

            
        }
    }
}