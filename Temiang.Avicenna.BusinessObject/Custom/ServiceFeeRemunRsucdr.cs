
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceFeeRemunRsucdr
    {
        public bool LoadByRemunNo(string RemunNo) {
            this.Query.Where(this.Query.RemunNo == RemunNo);
            return this.Load(this.Query);
        }
    }

    public partial class ServiceFeeRemunRsucdrCollection : esServiceFeeRemunRsucdrCollection
    {
        public DataTable GetForExportExcelBPJS(int RemunID) {
            var par = new esParameters();
            par.Add("RemunID", RemunID);

            string commandText = "sp_RemunBPJS_RSUDCDR";
            return FillDataTable(esQueryType.StoredProcedure, commandText, par);
        }
        public DataTable GetForExportExcelNonBPJS(int RemunID)
        {
            var par = new esParameters();
            par.Add("RemunID", RemunID);

            string commandText = "sp_RemunNonBPJS_RSUDCDR";
            return FillDataTable(esQueryType.StoredProcedure, commandText, par);
        }
    }
}
