namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingDiagnosa
    {
        #region Private
        //private string GetPrefix(string dLevel)
        //{
        //    var prefix = string.Empty;
        //    switch (dLevel)
        //    {
        //        case "00": { prefix = "xx"; break; } // Domain / Division/ Group
        //        case "10": { prefix = "##.xx"; break; } // Diagnosa
        //        case "11": { prefix = "##.##.xx"; break; } // Complication / problem
        //        case "20": { prefix = "##.##.nocxx"; break; } // NOC
        //        case "21": { prefix = "##.##.noc##.xx"; break; } // Noc Objective
        //        case "30": { prefix = "##.##.nicxx"; break; } // NIC
        //        case "31": { prefix = "##.##.nic##.xx"; break; } // NIC Implementation
        //        case "32": { prefix = "##.##.nic##.##.xx"; break; } // NIC Implementation Activity
        //    }

        //    return prefix;
        //}
        #endregion

        #region Public
        public static string GetNewID(string dLevel, string SRNsDiagnosaType) {
            if (string.IsNullOrEmpty(SRNsDiagnosaType)) return string.Empty;

            string ident = "0";
            if (SRNsDiagnosaType == "01") ident = "0";
            else if (SRNsDiagnosaType == "02") ident = "1";
            else if (SRNsDiagnosaType == "03") ident = "2";

            var defVal = dLevel + ident + "0".PadLeft(6, '0');

            var query = new NursingDiagnosaQuery();
            query.Where(query.SRNursingDiagnosaLevel == dLevel, query.SRNsDiagnosaType == SRNsDiagnosaType);
            query.Select("<ISNULL(max(NursingDiagnosaID),'" + defVal + "') LastID>");
            var dttbl = query.LoadDataTable();

            var iLastID = 0;
            if (dttbl.Rows.Count > 0)
            {
                iLastID = System.Convert.ToInt32(dttbl.Rows[0][0].ToString().Substring((dLevel + ident).Length));
            }

            var newid = "";

            var ns = new NursingDiagnosa();
            do
            {
                iLastID++;
                newid = dLevel + ident + iLastID.ToString().PadLeft(6, '0');
                ns = new NursingDiagnosa();
            } while (ns.LoadByPrimaryKey(newid));

            return newid;
        }

        public static string GetNewSequenceNo(string dLevel, string SRNsDiagnosaType)
        {
            var defVal = "0".PadLeft(5, '0');

            var query = new NursingDiagnosaQuery();
            query.Where(query.SRNursingDiagnosaLevel == dLevel, query.SRNsDiagnosaType == SRNsDiagnosaType);
            query.Select("<ISNULL(max(SequenceNo),'"+defVal+"') LastID>");
            var dttbl = query.LoadDataTable();

            var iLastID = 1;
            if (dttbl.Rows.Count > 0)
            {
                iLastID = System.Convert.ToInt32(dttbl.Rows[0][0]);
                iLastID++;
            }
            return iLastID.ToString().PadLeft(5, '0');
        }
        public static NursingDiagnosaCollection GetDiagnosaByLevelWithException(string level, string[] NursingDiagnosaExceptionIDs) {
            return GetDiagnosaByParentAndLevel(string.Empty, level, NursingDiagnosaExceptionIDs);
        }
        public static NursingDiagnosaCollection GetDiagnosaByLevel(string level)
        {
            return GetDiagnosaByParentAndLevel(string.Empty, level, new string[]{});
        }
        public static NursingDiagnosaCollection GetDiagnosaByParentAndLevel(string ParentID, string level, string[] NursingDiagnosaExceptionIDs) {
            var c = new NursingDiagnosaCollection();
            c.Query.Where(
                c.Query.SRNursingDiagnosaLevel == level,
                c.Query.IsActive == true
            ).OrderBy(c.Query.NursingDiagnosaName.Ascending);
            if (!string.IsNullOrEmpty(ParentID)) {
                c.Query.Where(c.Query.NursingDiagnosaParentID == ParentID);
            }
            if (NursingDiagnosaExceptionIDs.Length > 0) {
                c.Query.Where(c.Query.NursingDiagnosaID.NotIn(NursingDiagnosaExceptionIDs));
            }

            c.LoadAll();
            return c;
        }

        public static NursingDiagnosaCollection GetDiagnosaByLevelAndServiceUnit(string level, string suid)
        {
            if (string.IsNullOrEmpty(suid)) suid = string.Empty;

            var coll = new NursingDiagnosaCollection();
            var nd = new NursingDiagnosaQuery("nd");
            var ndsu = new NursingDiagnosaServiceUnitQuery("ndsu");
            nd.LeftJoin(ndsu).On(nd.NursingDiagnosaID == ndsu.NursingDiagnosaID)
                .Where(ndsu.Or(ndsu.ServiceUnitID == suid, ndsu.ServiceUnitID.IsNull()))
                .Where(nd.SRNursingDiagnosaLevel == level);
            coll.Load(nd);
            return coll;
        }

        //public static string _GetNewId(string ParentID, string dLevel)
        //{
        //    // define prefix
        //    var nd = new NursingDiagnosa();
        //    var prefix = nd.GetPrefix(dLevel);
        //    // select last top
        //    var ndQuery = new NursingDiagnosaQuery("a");
        //    var ndColl = new NursingDiagnosaCollection();
        //    ndQuery.Where(ndQuery.SRNursingDiagnosaLevel == dLevel,
        //        ndQuery.NursingParentID == ParentID);
        //    //ndQuery.es.Top = 1;
        //    ndQuery.OrderBy(ndQuery.NursingDiagnosaID.Descending);
        //    ndColl.Load(ndQuery);

        //    int iLastID = 0;
        //    if (ndColl.Count > 0)
        //    {
        //        foreach (var i in ndColl) {
        //            // fetch based on prefix
        //            if (i.NursingDiagnosaID.Length < prefix.Length)
        //            {
        //                // not valid, pick another one
        //            }
        //            else { 
        //                var sLast = i.NursingDiagnosaID.Substring(prefix.Replace("x","").Length);
        //                try
        //                {
        //                    iLastID = System.Convert.ToInt32(sLast);
        //                    break;
        //                }   
        //                catch { 

        //                }
        //            }
        //        }
        //    }

        //    iLastID++;
        //    return prefix.Replace("x", "") + iLastID.ToString().PadLeft(2, '0');
        //}
        #endregion
    }
}
