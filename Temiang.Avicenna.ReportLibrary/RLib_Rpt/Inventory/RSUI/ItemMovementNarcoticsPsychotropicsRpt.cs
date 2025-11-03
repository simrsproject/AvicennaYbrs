namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.Inventory.RSUI
{
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;

    public partial class ItemMovementNarcoticsPsychotropicsRpt : Telerik.Reporting.Report
    {
        public ItemMovementNarcoticsPsychotropicsRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            var dFrom = printJobParameters.FindByParameterName("p_FromDate").ValueDateTime;
            var dTo = printJobParameters.FindByParameterName("p_ToDate").ValueDateTime;
            textBox3.Value = dFrom.Value.ToString("dd/MM/yyyy") + " - " +
                dTo.Value.ToString("dd/MM/yyyy");

            var sGroup = printJobParameters.FindByParameterName("p_ItemGroupID").ValueString;
            switch (sGroup) {
                case "0": {
                    textBox1.Value = "LAPORAN OBAT PSIKOTROPIKA DAN NARKOTIKA";
                    break;
                }
                case "1": {
                    textBox1.Value = "LAPORAN OBAT PSIKOTROPIKA";
                    break;
                }
                case "2": {
                    textBox1.Value = "LAPORAN OBAT NARKOTIKA";
                    break;
                }
                default: {
                    break;
                }
            }

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            table1.DataSource = DataSource;
        }
    }
}