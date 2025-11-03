
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeRemun
    {
        public bool LoadByRemunNo(string RemunNo) {
            this.Query.Where(this.Query.RemunNo == RemunNo);
            return this.Load(this.Query);
        }
    }

    public partial class EmployeeRemunCollection : esEmployeeRemunCollection
    {
        public DataTable GetNewRemun(DateTime RemunDate)
        {
            esParameters par = new esParameters();
            par.Add("p_date", RemunDate);

            return FillDataTable(esQueryType.StoredProcedure, "sp_EmployeeGetForNewRemun", par);
        }

        public DataTable GetRemunByID(int RemunID)
        {
            esParameters par = new esParameters();
            par.Add("p_remunid", RemunID);

            return FillDataTable(esQueryType.StoredProcedure, "sp_EmployeeGetForRemun", par);
        }
    }
}
