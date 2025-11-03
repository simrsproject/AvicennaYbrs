using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.Registration
{

    /// <summary>
    /// Summary description for IdentitasPasienRpt.
    /// </summary>
    public partial class MapForder1Rpt : Telerik.Reporting.Report
    {
        public MapForder1Rpt(DataTable dtb)
        {
            InitializeComponent();
            DataSource = dtb;

        }
    }
}