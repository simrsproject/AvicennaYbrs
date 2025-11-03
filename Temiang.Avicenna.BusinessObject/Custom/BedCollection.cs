using System;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BedCollection : esBedCollection
    {
        public DataTable GetBedInformationSummary(string IpAddress)
        {
            string cmd = string.Empty;
            cmd = "sp_bedInformation";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
        public DataTable GetBedInformationSummaryMF(string IpAddress)
        {
            string cmd = string.Empty;
            cmd = "sp_bedInformationMF";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
        public DataTable GetBedInformationSummaryMF_Pandemi(string IpAddress)
        {
            string cmd = string.Empty;
            cmd = "sp_bedInformationV2";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
    }
}