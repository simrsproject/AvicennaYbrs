using Temiang.Dal.Interfaces; 
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppStandardReferenceItemCollection
    {
        public bool LoadByStandardReferenceID(string standardReferenceID, int resultRow)
        {
            AppStandardReferenceItemQuery itemQuery = new AppStandardReferenceItemQuery();
            itemQuery.Where
                (
                    itemQuery.StandardReferenceID == standardReferenceID,
                    itemQuery.IsActive == true
                );
            itemQuery.OrderBy(AppStandardReferenceItemMetadata.ColumnNames.ItemName, esOrderByDirection.Ascending);
            if (resultRow > 0)
            {
                itemQuery.es.Top = resultRow;
            }
            return Load(itemQuery);
        }

        public bool LoadByStandardReferenceID(string standardReferenceID)
        {
            return LoadByStandardReferenceID(standardReferenceID, 50);
        }

        public bool LoadByStdRefGroup(string StandardReferenceGroup)
        {
            return LoadByStdRefGroup(StandardReferenceGroup, string.Empty);
        }
        public bool LoadByStdRefGroup(string StandardReferenceGroup, string ReferenceID) {
            if (!string.IsNullOrEmpty(ReferenceID))
            {
                return LoadByStdRefGroups(StandardReferenceGroup, new string[] { ReferenceID });
            }
            else {
                return LoadByStdRefGroups(StandardReferenceGroup, new string[] {});
            }
            
            //var stdiChld = new AppStandardReferenceItemQuery("b");
            //var stdChld = new AppStandardReferenceQuery("a");
            //stdiChld.InnerJoin(stdChld).On(stdChld.StandardReferenceID == stdiChld.StandardReferenceID)
            //    .Where(stdChld.StandardReferenceGroup == StandardReferenceGroup,
            //        stdChld.IsActive == true, stdiChld.IsActive == true
            //    ).Select(
            //        stdiChld
            //    );
            //if (!string.IsNullOrEmpty(ReferenceID)) {
            //    stdiChld.Where(stdiChld.ReferenceID == ReferenceID);
            //}
            //return Load(stdiChld);
        }

        public bool LoadByStdRefGroups(string StandardReferenceGroup, string[] ReferenceIDs)
        {
            var stdiChld = new AppStandardReferenceItemQuery("b");
            var stdChld = new AppStandardReferenceQuery("a");
            stdiChld.InnerJoin(stdChld).On(stdChld.StandardReferenceID == stdiChld.StandardReferenceID)
                .Where(stdChld.StandardReferenceGroup == StandardReferenceGroup,
                    stdChld.IsActive == true, stdiChld.IsActive == true
                ).Select(
                    stdiChld
                );
            if (ReferenceIDs.Length > 0)
            {
                stdiChld.Where(stdiChld.ReferenceID.In(ReferenceIDs));
            }
            return Load(stdiChld);
        }

        public string GetRegistrationNo(string strQuery, string val)
        {
            esParameters par = new esParameters();

            string commandText = string.Format(strQuery, string.Format("'{0}'", val));

            if (commandText.ToLower().Contains("delete")) return string.Empty;
            if (commandText.ToLower().Contains("drop")) return string.Empty;
            if (commandText.ToLower().Contains("alter")) return string.Empty;
            if (commandText.ToLower().Contains("rename")) return string.Empty;
            if (commandText.ToLower().Contains("modify")) return string.Empty;

            var dtb = FillDataTable(esQueryType.Text, commandText, par);

            if (dtb.Rows.Count == 0) return string.Empty;
            return dtb.Rows[0][0].ToString();
        }

        public DataTable PopulateItemTypeProductPerUser(string userId)
        {
            esParameters par = new esParameters();

            string commandText =
                @"SELECT x.SRItemType AS ItemID, y.ItemName " +
                "FROM " +
                "(" +
                "SELECT DISTINCT CASE " +
                    "WHEN augp.ProgramID = '03.09.02' THEN '11' " +
                    "WHEN augp.ProgramID = '03.09.03' THEN '21' " +
                    "WHEN augp.ProgramID = '03.09.10' THEN '11' " +
                    "WHEN augp.ProgramID = '03.09.11' THEN '81' END AS SRItemType " +
                "FROM AppUser AS au WITH (NOLOCK) " +
                "INNER JOIN AppUserUserGroup AS auug WITH (NOLOCK) ON auug.UserID = au.UserID " +
                "INNER JOIN AppUserGroupProgram AS augp WITH (NOLOCK) ON augp.UserGroupID = auug.UserGroupID " +
                "WHERE au.UserID = '" + userId + "' " + 
                "AND augp.ProgramID IN ('03.09.02', '03.09.03', '03.09.10', '03.09.11') " +
                ") x " +
                "INNER JOIN AppStandardReferenceItem AS y WITH (NOLOCK) ON y.StandardReferenceID = 'ItemType' AND y.ItemID = x.SRItemType ";
            
            commandText += "ORDER BY x.SRItemType ";
            
            this.es.Connection.CommandTimeout = 300;
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}