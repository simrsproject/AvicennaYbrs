namespace Temiang.Avicenna.ReportLibrary.Charges.RSUTAMA
{
    using System;
    using System.Data;
    using Telerik.Reporting;
    using Temiang.Avicenna.BusinessObject;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Linq;

    /// <summary>
    /// Summary description for PrescriptionEtiketRpt.
    /// </summary>
    public partial class PrescriptionEtiketRpt : Telerik.Reporting.Report
    {
        public PrescriptionEtiketRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);


            //// for custom etiquette
            var prescriptionNo = printJobParameters.FindByParameterName("p_PrescriptionNo");
            var sequenceNo = printJobParameters.FindByParameterName("p_SequenceNo");
            var label = printJobParameters.FindByParameterName("p_Label");

            var sqVal = string.Empty;
            if(sequenceNo != null){
                sqVal = sequenceNo.ValueString;
                sequenceNo.MarkAsDeleted();
                sequenceNo.Save();
            }

            DataTable ds;

            ds = new ReportDataSource().GetDataTable(programID, printJobParameters);
            if (ds.Rows.Count > 0)
            {
                if (sequenceNo != null)
                {
                    ds = ds.AsEnumerable().Where(x => x.Field<string>("SequenceNo") == sqVal).CopyToDataTable();
                }

                var tab = ds.Clone();

                foreach (DataRow row in ds.Rows)
                {
                    tab.ImportRow(row);
                    if (Convert.ToInt32(row["NumberOfCopies"]) == 1) continue;
                    for (int i = 1; i < Convert.ToInt32(row["NumberOfCopies"]); i++)
                    {
                        tab.ImportRow(row);
                    }
                }

                // REMOVE Non R from etiket
                DataSource = tab.AsEnumerable().Where(x => x.Field<string>("IsRFlag") != string.Empty);
            }
        }
    }
}