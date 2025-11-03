using System.Data;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitVisitTypeCollection 
	{
        public DataTable GetFullJoinWVisitType(string serviceUnitID)
        {
            esParameters par = new esParameters();
            par.Add("p_ServiceUnitID", serviceUnitID);

            string commandText =
                @"SELECT a.VisitTypeID,a.VisitTypeName,a.Notes,COALESCE(b.VisitDuration,0) VisitDuration,
IsSelect=CONVERT(BIT,CASE WHEN COALESCE(b.VisitTypeID,'-')='-' THEN 0 ELSE 1 END)
FROM   VisitType a
LEFT  JOIN ServiceUnitVisitType b
      ON  a.VisitTypeID = b.VisitTypeID
      AND b.ServiceUnitID = @p_ServiceUnitID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWVisitType(string serviceUnitID)
        {
            esParameters par = new esParameters();
            par.Add("p_ServiceUnitID", serviceUnitID);

            string commandText =
                @"SELECT a.VisitTypeID,a.VisitTypeName,a.Notes,COALESCE(b.VisitDuration,0) VisitDuration,
IsSelect=CONVERT(BIT,1)
FROM   VisitType a
INNER  JOIN ServiceUnitVisitType b
      ON  a.VisitTypeID = b.VisitTypeID
      AND b.ServiceUnitID = @p_ServiceUnitID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
	}
}
