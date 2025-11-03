using System;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSSA

{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for PriceListRpt.
    /// </summary>
    public partial class PriceListRpt : Report
    {
        public PriceListRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeaderSection1);
            Helper.InitializeDataSource(this, programID, printJobParameters);
            var type = new AppStandardReferenceItem();
            type.LoadByPrimaryKey("ItemType", printJobParameters.FindByParameterName("p_ItemType").ValueString);
            textBox2.Value = type.ItemName;
        }
    }
}