/*
===============================================================================
                    Temiang.Dal(TM) 2008 by Temiang.Dal, LLC
             Persistence Layer and Business Objects for Microsoft .NET  
                          http://www.entityspaces.net
===============================================================================
                       Temiang.Dal Version # 2008.1.1110.0
                       CodeSmith Version    # 4.1.4.3592
                       Date Generated       : 01/12/2008 10:15:07
===============================================================================
*/

using System.Data;
using Temiang.Dal.Interfaces; 
using Temiang.Dal.DynamicQuery;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppProgramCollection
    {
        public DataTable GetDataTable(string storeProcedureName, esParameters paramaters)
        {
            return FillDataTable(esQueryType.StoredProcedure, storeProcedureName, paramaters);
        }

        public void LoadPRG(string UserID)
        {
            var ap = new AppProgramQuery("ap");
            var augp = new AppUserGroupProgramQuery("augp");
            var auug = new AppUserUserGroupQuery("auug");
            ap.InnerJoin(augp).On(ap.ProgramID == augp.ProgramID)
                .InnerJoin(auug).On(augp.UserGroupID == auug.UserGroupID)
                .Where(auug.UserID == UserID, ap.ProgramType == "PRG", ap.IsParentProgram == false, ap.IsVisible == true, ap.IsDiscontinue == false)
                .Select(ap);
            ap.es.Distinct = true;
            this.Load(ap);
            LoadParent(this.Where(x => x.IsParentProgram == false && !string.IsNullOrEmpty(x.ParentProgramID))
                .Select(x => x.ParentProgramID).Distinct().ToList());
        }

        public void LoadMvcByUserID(string AssemblyName, string UserID) {
            var ap = new AppProgramQuery("ap");
            var augp = new AppUserGroupProgramQuery("augp");
            var auug = new AppUserUserGroupQuery("auug");
            ap.InnerJoin(augp).On(ap.ProgramID == augp.ProgramID)
                .InnerJoin(auug).On(augp.UserGroupID == auug.UserGroupID)
                .Where(auug.UserID == UserID, ap.AssemblyName == AssemblyName, ap.IsParentProgram == false, ap.IsVisible == true, ap.IsDiscontinue == false)
                .Select(ap);
            ap.es.Distinct = true;
            this.Load(ap);
            LoadParent(this.Where(x => x.IsParentProgram == false && !string.IsNullOrEmpty(x.ParentProgramID))
                .Select(x => x.ParentProgramID).Distinct().ToList());
        }

        public void LoadParent(System.Collections.Generic.List<string> prgids) {
            if (!prgids.Any()) return;

            var coll = new AppProgramCollection();
            coll.Query.Where(coll.Query.ProgramID.In(prgids));
            if (coll.LoadAll()) {
                // attach to main collection
                foreach (var prg in coll) {
                    if(!this.Where(p => p.ProgramID == prg.ProgramID).Any())
                        this.AttachEntity(prg);
                }
                LoadParent(coll.Where(x => !string.IsNullOrEmpty(x.ParentProgramID))
                .Select(x => x.ParentProgramID).ToList());
            }
        }
    }
}