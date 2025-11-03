using System.Data;
using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppUserGroupProgramCollection 
    {
        public DataTable GetAppUserGroupProgram(string userGroupID)
        {
            esParameters par = new esParameters();
            par.Add("p_UserGroupID", userGroupID);

            string commandText =
                @"SELECT ap.ProgramID,
ap.ParentProgramID,
ap.IsProgram,
ap.ProgramName,
ap.IsProgramAddAble,
ap.IsProgramEditAble,
ap.IsProgramDeleteAble,
ap.Note,
COALESCE(augp.IsUserGroupAddAble,CONVERT(BIT,0)) IsUserGroupAddAble,
COALESCE(augp.IsUserGroupEditAble,CONVERT(BIT,0)) IsUserGroupEditAble,
COALESCE(augp.IsUserGroupDeleteAble,CONVERT(BIT,0)) IsUserGroupDeleteAble,
IsSelect=CONVERT(BIT,CASE WHEN COALESCE(augp.ProgramID,'-')='-' THEN 0 ELSE 1 END)
FROM   AppProgram ap
       LEFT  JOIN AppUserGroupProgram augp
            ON  ap.ProgramID = augp.ProgramID
            AND augp.UserGroupID = @p_UserGroupID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}