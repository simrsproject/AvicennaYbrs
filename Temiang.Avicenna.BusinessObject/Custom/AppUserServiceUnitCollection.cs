using System.Data;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppUserServiceUnitCollection 
	{
        public DataTable GetFullJoinWUser(string userID)
        {
            esParameters par = new esParameters();
            par.Add("p_UserID", userID);

            string commandText =
                @"SELECT a.ServiceUnitID,a.ServiceUnitName,
IsSelect=CONVERT(BIT,CASE WHEN COALESCE(b.ServiceUnitID,'-')='-' THEN 0 ELSE 1 END)
FROM   ServiceUnit a
LEFT  JOIN AppUserServiceUnit b
      ON  a.ServiceUnitID = b.ServiceUnitID
      AND b.UserID = @p_UserID 
WHERE a.IsActive = 1";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWUser(string userID)
        {
            esParameters par = new esParameters();
            par.Add("p_UserID", userID);

            string commandText =
                @"SELECT a.ServiceUnitID,a.ServiceUnitName,
IsSelect=CONVERT(BIT,1)
FROM   ServiceUnit a
INNER  JOIN AppUserServiceUnit b
      ON  a.ServiceUnitID = b.ServiceUnitID
      AND b.UserID = @p_UserID
WHERE a.IsActive = 1";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public bool LoadByUserID(string userID) {
            this.Query.Where(this.Query.UserID == userID);
            return this.LoadAll();
        }
	}
}
