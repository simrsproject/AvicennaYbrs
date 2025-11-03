using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Inventory
{

    public partial class ItemMasterRpt : Report
    {
        public ItemMasterRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
     
            InitializeComponent();

            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);

        }


    }
}