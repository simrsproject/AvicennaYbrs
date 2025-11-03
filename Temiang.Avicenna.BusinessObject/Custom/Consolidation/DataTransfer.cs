using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.Consolidation
{

    public class DataTransfer
    {
        public string HealthcareID { get; set; }
        public DateTime TransferDateTime { get; set; }
        public List<DataTransferHeader> DataTransferHeaders { get; set; }
    }

    public class DataTransferHeader
    {
        public string LogDateTime { get; set; }
        public string ConsolidationLogID { get; set; }
        public string ConsolidationType { get; set; }
        public string IsSend { get; set; }
        public string PrimaryKeyData { get; set; }
        public string TableName { get; set; }

        public List<DataTransferDetail> DataTransferDetails { get; set; }
    }

    public class DataTransferDetail
    {
        public string ConsolidationLogID { get; set; }
        public string ColumnName { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public bool? IsInPrimaryKey { get; set; }
    }
}
