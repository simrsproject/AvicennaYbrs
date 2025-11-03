using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class WebServiceAPILogCollection
    {
        public int DeletePrevMonth()
        {
            string cmd = @"DELETE TOP (100) FROM WebServiceAPILog WHERE DateRequest < DATEADD(MONTH,-1, GETDATE())";
            return ExecuteNonQuery(esQueryType.Text, cmd);
        }
    }
}
