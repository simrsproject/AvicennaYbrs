using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BpjsSEP
    {
        public int ImportSEP(string nosep, DateTime tglsep, string nokartu, string nama, string jns)
        {
            var pars = new esParameters();
            pars.Add(new esParameter("nosep", nosep));
            pars.Add(new esParameter("tglsep", tglsep));
            pars.Add(new esParameter("nokartu", nokartu));
            pars.Add(new esParameter("nama", nama));
            pars.Add(new esParameter("jns", jns));

            return ExecuteNonQuery("dbo", "sp_importsep", pars);
        }

        public virtual bool LoadByPrimaryKey(System.String sepNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sepNo);
            else
                return LoadByPrimaryKeyStoredProcedure(sepNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sepNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sepNo);
            else
                return LoadByPrimaryKeyStoredProcedure(sepNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String sepNo)
        {
            esBpjsSEPQuery query = this.GetDynamicQuery();
            query.Where(query.NoSEP == sepNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String sepNo)
        {
            esParameters parms = new esParameters();
            parms.Add("NoSEP", sepNo);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
    }
}
