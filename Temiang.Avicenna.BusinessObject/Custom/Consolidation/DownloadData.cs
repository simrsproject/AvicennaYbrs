using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.Consolidation
{

    public class DownloadData
    {
        public Int64? UpdateID { get; set; }
        public string UpdateData { get; set; }
        public string UpdateSummary { get; set; }
        public Int64? StartConsolidationLogID { get; set; }
        public Int64? EndConsolidationLogID { get; set; }
    }
}
