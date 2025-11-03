using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalRecordFileStatusCollection
	{
        public DataTable MedicalRecordFileStatusGetList(string IpAddress, string Code)
        {
            string cmd = string.Empty;
            cmd = "sp_MedicalRecordFileStatus";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pCode = new esParameter("Code", Code, esParameterDirection.Input, DbType.String, 2);
            pars.Add(pCode);



            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
    }
}

