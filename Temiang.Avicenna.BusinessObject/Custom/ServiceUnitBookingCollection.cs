using System.Data;
using System;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitBookingCollection 
	{
        public DataTable GetQueueByDate(DateTime TransactionDate)
        {
            string cmd = string.Empty;
            cmd = "sp_ServiceUnitBookingQueueByDate";

            var pars = new esParameters();
            var pTD = new esParameter("TransactionDate", TransactionDate, esParameterDirection.Input, DbType.Date, 20);
            pars.Add(pTD);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueueOutstanding(string IpAddress)
        {
            string cmd = string.Empty;
            cmd = "sp_operatingTheaterInformation";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }


        public DataTable GetQueueOutstandingV2(string IpAddress)
        {
            string cmd = string.Empty;
            cmd = "sp_operatingTheaterInformationV2";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
    }
}
