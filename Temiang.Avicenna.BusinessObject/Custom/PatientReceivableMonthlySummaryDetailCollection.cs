using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientReceivableMonthlySummaryDetailCollection
    {
        public void DeleteByPeriod(System.DateTime Period)
        {
            esParameters par = new esParameters();
            this.es.Connection.CommandTimeout = 600;
            par.Add("p_Period", Period);
            string cmd = @"delete a from PatientReceivableMonthlySummaryDetail a inner join PatientReceivableMonthlySummary b on a.ID = b.ID where b.Period = @p_Period";
            ExecuteNonQuery(esQueryType.Text, cmd, par);
        }
    }
}
