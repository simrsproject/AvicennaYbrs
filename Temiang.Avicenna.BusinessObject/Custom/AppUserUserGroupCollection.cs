using System.Data;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppUserUserGroupCollection 
	{
        public DataTable GetFullJoinWUserGroup(string userID)
        {
            esParameters par = new esParameters();
            par.Add("p_UserID", userID);

            string commandText =
                @"SELECT a.UserGroupID,a.UserGroupName,
IsSelect=CONVERT(BIT,CASE WHEN COALESCE(b.UserGroupID,'-')='-' THEN 0 ELSE 1 END)
FROM   AppUserGroup a
LEFT  JOIN AppUserUserGroup b
      ON  a.UserGroupID = b.UserGroupID
      AND b.UserID = @p_UserID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWUserGroup(string userID)
        {
            esParameters par = new esParameters();
            par.Add("p_UserID", userID);

            string commandText =
                @"SELECT a.UserGroupID,a.UserGroupName,
IsSelect=CONVERT(BIT,1)
FROM   AppUserGroup a
INNER  JOIN AppUserUserGroup b
      ON  a.UserGroupID = b.UserGroupID
      AND b.UserID = @p_UserID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
	}
}
