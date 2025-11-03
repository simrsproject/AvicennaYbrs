using System.Linq;
using System.Data;
using System.Data.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class NutritionCareDiagnoseTransDT
    {
        private string _TmpIdEvaluation = string.Empty;
        public string TmpIdEvaluation
        {
            get { return _TmpIdEvaluation; }
            set { _TmpIdEvaluation = value; }
        }
        public string RefToTerminologyName
        {
            get { return GetColumn("refTo_TerminologyName").ToString(); }
            set { SetColumn("refTo_TerminologyName", value); }
        }
        public string RefToUserName
        {
            get { return GetColumn("refTo_UserName").ToString(); }
            set { SetColumn("refTo_UserName", value); }
        }

        public override void Save()
        {
            if (this.es.IsAdded)
            {
                if (string.IsNullOrEmpty(CreateByUserID))
                {
                    var userLogin = new UserLogin();
                    if (HttpContext.Current.Session["_UserLogin"] != null)
                    {
                        userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                        CreateByUserID = userLogin.UserID;
                    }
                }
            }
            base.Save();
        }

        public override void MarkAsDeleted()
        {
            switch (this.SRNutritionCareTerminologyLevel)
            {
                case "31":
                    {
                        if (!this.es.IsAdded)
                        {
                            this.IsDeleted = true;
                        }
                        else
                        {
                            base.MarkAsDeleted();
                        }
                        break;
                    }
                default:
                    {
                        base.MarkAsDeleted();
                        break;
                    }
            }
        }

        public static string GetTmpTerminologyID(NutritionCareDiagnoseTransDTCollection c, string TransactionNo)
        {
            var q = new NutritionCareDiagnoseTransDTQuery("a");
            q.Where(q.TransactionNo == TransactionNo && q.TmpTerminologyID != string.Empty)
                .Select(q.TmpTerminologyID);
            var dt = q.LoadDataTable();
            var tmpID = dt.AsEnumerable().Select(x =>
                System.Convert.ToInt32(x.Field<string>("TmpTerminologyID"))).ToList(); ;
            var tmp = (from x in c
                       where x.TmpTerminologyID.Trim().Length > 0
                       select System.Convert.ToInt32(x.TmpTerminologyID));
            tmpID = tmpID.Union(tmp).ToList();
            int tid = 0;
            if (tmpID.Count() > 0)
            {
                tid = tmpID.Max();
            }
            tid++;
            var NewTmpNursingDiagnosaID = tid.ToString().PadLeft(4, '0');
            return NewTmpNursingDiagnosaID;
        }

        public static string GetFullImplementationNameFormatted(string terminologyName, string S, string O)
        {

            //if (Equals(terminologyName, "S B A R") || Equals(terminologyName, "SBAR"))
            //{
            //    return (
            //        "S: " + S + System.Environment.NewLine +
            //        "B: " + B + System.Environment.NewLine +
            //        "A: " + A + System.Environment.NewLine +
            //        "R: " + R);
            //}
            //if (Equals(terminologyName, "S O A P") || Equals(terminologyName, "SOAP"))
            //{
            //    return (
            //        "S: " + S + System.Environment.NewLine +
            //        "O: " + B + System.Environment.NewLine +
            //        "A: " + A + System.Environment.NewLine +
            //        "P: " + R);
            //}
            //else
            {
                return terminologyName;
            }
        }

        public string GetFullImplementationName
        {
            get
            {
                return GetFullImplementationNameFormatted(TerminologyName, S, O);
            }
        }


        #region DIAGNOSA
        #region NUTRITION CARE PROBLEM (11)
        public static DataTable NutritionCareProblem(string TransactionNo, string TerminologyLevel, string TerminologyParentID)
        {
            var query = new NutritionCareTerminologyQuery("a");
            var pr = new NutritionCareTerminologyQuery("b");

            var dt = new NutritionCareDiagnoseTransDTQuery("e");
            var nictype = new AppStandardReferenceItemQuery("f");

            query.es.Distinct = true;

            query.InnerJoin(pr).On(query.TerminologyParentID == pr.TerminologyID);
            query.LeftJoin(dt).On(query.TerminologyID == dt.TerminologyID
                & dt.TransactionNo == TransactionNo);

            query.Where(
                query.SRNutritionCareTerminologyLevel == TerminologyLevel);
            if (TerminologyParentID != string.Empty)
            {
                query.Where(query.TerminologyParentID == TerminologyParentID);
            }

            query.Select(
                query,
                "<ISNULL(e.TerminologyName, a.TerminologyName) TerminologyNameEdited>",
                pr.SequenceNo.As("ParentSequenceNo"),
                pr.TerminologyName.As("TerminologyParentName"),
                dt.TerminologyID.As("TransTerminologyID"),
                dt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dt.LastUpdateDateTime.As("LastUpdateTransDTDateTime")
                );

            var dttbl = query.LoadDataTable();
            return dttbl;
        }
        public static DataTable NutritionCareProblem(string TransactionNo)
        {
            var np = new NutritionCareTerminologyQuery("np");
            var nd = new NutritionCareTerminologyQuery("nd");
            var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("nad");
            var natdt = new NutritionCareAssessmentTransDTQuery("natdt");
            var nathd = new NutritionCareAssessmentTransHDQuery("nathd");

            var ndtdt = new NutritionCareDiagnoseTransDTQuery("ndtdt");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.TerminologyParentID == nd.TerminologyID)
                .InnerJoin(nad).On(nd.TerminologyID == nad.TerminologyID)
                .InnerJoin(natdt).On(nad.QuestionID == natdt.QuestionID)
                .InnerJoin(nathd).On(natdt.HDID == nathd.ID)
                .LeftJoin(ndtdt).On(np.TerminologyID == ndtdt.TerminologyID
                & ndtdt.TransactionNo == TransactionNo)
                .Where(
                    np.SRNutritionCareTerminologyLevel == "11",
                    nathd.TransactionNo == TransactionNo);

            np.Select(
                np,
                "<ISNULL(ndtdt.TerminologyName, np.TerminologyName) TerminologyNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.TerminologyName.As("TerminologyParentName"),
                ndtdt.TerminologyID.As("TransTerminologyID"),
                ndtdt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                ndtdt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                nd.TerminologyID.As("TerminologyIDParent"),
                "<0 AssCount>",
                "<'' FTS>",
                "<(CASE WHEN np.TerminologyName LIKE '%...%' THEN 1 ELSE 0 end) SortingX>"
                );

            np.OrderBy(
                np.TerminologyParentID.Ascending,
                "<(CASE WHEN np.TerminologyName LIKE '%...%' THEN 1 ELSE 0 end)>",
                np.TerminologyName.Ascending,
                np.TerminologyID.Ascending
                );

            var dttbl = np.LoadDataTable();
            var result = FilterDiagnosaByAssessmentValue(TransactionNo, dttbl);

            foreach (System.Data.DataRow d in result.Rows)
            {
                d["FTS"] = d["AssCount"].ToString().PadLeft(2, '0') + d["TerminologyIDParent"].ToString();
            }

            dttbl.AcceptChanges();

            return result;
            //return dttbl;
        }

        /// <summary>
        /// fungsi mengambil data nusing problem yang bisa diangkat menjadi diagnosa baru
        /// </summary>
        /// <param name="RegistrationNo"></param>
        /// <returns></returns>
        public static DataTable NutritionCareProblemAvailable(string TransactionNo)
        {
            // diagnosa yang bisa diangkat yaitu diagnosa yang tidak sedang aktif

            var np = new NutritionCareTerminologyQuery("np");
            var nd = new NutritionCareTerminologyQuery("nd");
            var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("nad");
            var natdt = new NutritionCareAssessmentTransDTQuery("natdt");
            var nathd = new NutritionCareAssessmentTransHDQuery("nathd");

            var ndtdt = new NutritionCareDiagnoseTransDTQuery("ndtdt");
            var nd10 = new NutritionCareDiagnoseTransDTQuery("nd10");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.TerminologyParentID == nd.TerminologyID)
                .InnerJoin(nad).On(nd.TerminologyID == nad.TerminologyID)
                .InnerJoin(natdt).On(nad.QuestionID == natdt.QuestionID)
                .InnerJoin(nathd).On(natdt.HDID == nathd.ID)
                .LeftJoin(ndtdt).On(np.TerminologyID == ndtdt.TerminologyID
                & ndtdt.TransactionNo == TransactionNo)
                .LeftJoin(nd10).On(nd.TerminologyID == nd10.TerminologyID && nd10.TransactionNo == TransactionNo)
                .Where(
                    np.SRNutritionCareTerminologyLevel == "11",
                    nathd.TransactionNo == TransactionNo,
                    nd10.TerminologyName.IsNull());

            np.Select(
                np,
                "<ISNULL(ndtdt.TerminologyName, np.TerminologyName) TerminologyNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.TerminologyName.As("TerminologyParentName"),
                ndtdt.TerminologyID.As("TransTerminologyID"),
                ndtdt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                ndtdt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                nd.TerminologyID.As("TerminologyIDParent"),
                "<0 AssCount>",
                "<'' FTS>"
                );

            np.OrderBy(np.SequenceNo.Ascending);
            
            var dttbl = np.LoadDataTable();
            var result = FilterDiagnosaByAssessmentValue(TransactionNo, dttbl);

            foreach (System.Data.DataRow d in result.Rows)
            {
                d["FTS"] = d["AssCount"].ToString().PadLeft(2, '0') + d["TerminologyIDParent"].ToString();
            }

            dttbl.AcceptChanges();

            return result;
            //return dttbl;
        }
        public static DataTable NutritionCareProblemForImplementation(string TransactionNo, string DS, string DO,
            string[] TerminologyIDExceptionsL10, bool ShowAll)
        {
            var np = new NutritionCareTerminologyQuery("np");
            var nd = new NutritionCareTerminologyQuery("nd");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.TerminologyParentID == nd.TerminologyID)
                .Where(np.SRNutritionCareTerminologyLevel == "11");

            if (TerminologyIDExceptionsL10.Length > 0)
            {
                np.Where(nd.TerminologyID.NotIn(TerminologyIDExceptionsL10));
            }

            np.Select(
                np,
                "<np.TerminologyName TerminologyNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.TerminologyName.As("TerminologyParentName"),
                nd.TerminologyID.As("TerminologyIDParent"),
                "<0 AssCount>",
                "<'' FTS>"
                );

            np.OrderBy(np.SequenceNo.Ascending);

            var dttbl = np.LoadDataTable();
            DataTable result;
            if (ShowAll)
            {
                result = dttbl;
            }
            else
            {
                result = FilterDiagnosaByDSDOText(TransactionNo, dttbl, DS, DO);
            }


            foreach (System.Data.DataRow d in result.Rows)
            {
                d["FTS"] = d["AssCount"].ToString().PadLeft(2, '0') + d["TerminologyIDParent"].ToString();
            }

            dttbl.AcceptChanges();

            return result;
            //return dttbl;
        }

        private static DataTable FilterDiagnosaByAssessmentValue(
            string TransactionNo, DataTable dttbl)
        {
            var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("nad");
            var nadt = new NutritionCareAssessmentTransDTQuery("nadt");
            var nahd = new NutritionCareAssessmentTransHDQuery("nahd");
            var na = new NutritionCareAssessmentQuestionQuery("na");
            var nth = new NutritionCareTransHDQuery("nth");
            var reg = new RegistrationQuery("reg");

            nad.InnerJoin(nadt).On(nad.QuestionID == nadt.QuestionID)
                .InnerJoin(nahd).On(nadt.HDID == nahd.ID)
                .InnerJoin(na).On(nadt.QuestionID == na.QuestionID)
                .InnerJoin(nth).On(nahd.TransactionNo == nth.TransactionNo)
                .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                .Where(nahd.TransactionNo == TransactionNo);

            nad.Select(
                nad.QuestionID,
                nad.TerminologyID,
                nad.Operand,
                nad.AcceptedText,
                nad.AcceptedNum,
                nad.AcceptedNum2,
                nad.CheckValue,
                nad.IsUsingRange,
                nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                na.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                na.QuestionText,
                na.AnswerDecimalDigit,
                nadt.AnswerPrefix,
                nadt.AnswerSuffix,
                nadt.AnswerText,
                nadt.QuestionText,
                "<ISNULL(nadt.AnswerNum, 0) as AnswerNum>",
                "<'' as ErrorMessage>",
                "<CAST(0 as bit) as Selected >",
                "<'' as AssessmentText>",
                "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                nad.AgeInMonthStart,
                nad.AgeInMonthEnd
            );
            var dt = nad.LoadDataTable();
            return DoFilter(TransactionNo, dttbl, dt);
        }
        private static DataTable FilterDiagnosaByDSDOText(
            string TransactionNo, DataTable dttbl, string DS, string DO)
        {
            // ambil id assessment dari app
            List<string> IdAssessment = new List<string>();
            var AppPrmDS = new AppParameter();
            if (AppPrmDS.LoadByPrimaryKey("NutritionCareAssessmentDS"))
            {
                IdAssessment.Add(AppPrmDS.ParameterValue);
            }
            var AppPrmDO = new AppParameter();
            if (AppPrmDO.LoadByPrimaryKey("NutritionCareAssessmentDO"))
            {
                IdAssessment.Add(AppPrmDO.ParameterValue);
            }

            var nad = new NutritionCareAssessmentQuestionDiagnoseQuery();
            nad.Where(nad.QuestionID.In(IdAssessment));
            nad.Select(
                nad.QuestionID,
                nad.TerminologyID,
                nad.Operand,
                nad.AcceptedText,
                nad.AcceptedNum,
                nad.AcceptedNum2,
                nad.CheckValue,
                nad.IsUsingRange,
                nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                "<'MEM' SRAnswerTypeFromQuestion>",
                "<'' QuestionText>",
                "<0 AnswerDecimalDigit>",
                "<'' AnswerPrefix>",
                "<'' AnswerSuffix>",
                "<'' AnswerText>",
                "<'' QuestionText>",
                "<0 AnswerNum>",
                "<'' as ErrorMessage>",
                "<CAST(0 as bit) as Selected >",
                "<'' as AssessmentText>",
                "<0 PatientAgeInMonth>",
                nad.AgeInMonthStart,
                nad.AgeInMonthEnd
            );
            var dt = nad.LoadDataTable();

            int PatAgeInMonth = 0;
            if (dt.Rows.Count > 0)
            {
                var nth = new NursingTransHD();
                if (nth.LoadByPrimaryKey(TransactionNo))
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(nth.RegistrationNo))
                    {
                        PatAgeInMonth = System.Convert.ToInt32(reg.AgeInMonth) +
                            (System.Convert.ToInt32(reg.AgeInYear) * 12);
                    }
                }
            }

            foreach (System.Data.DataRow d in dt.Rows)
            {
                if (d["QuestionID"].ToString() == AppPrmDS.ParameterValue)
                {
                    d["AnswerText"] = DS;
                }
                else if (d["QuestionID"].ToString() == AppPrmDO.ParameterValue)
                {
                    d["AnswerText"] = DO;
                }
                
                d["PatientAgeInMonth"] = PatAgeInMonth;
            }
            dt.AcceptChanges();

            return DoFilter(TransactionNo, dttbl, dt);
        }

        private static DataTable DoFilter(string TransactionNo, DataTable dttbl, DataTable DSDO)
        {

            foreach (System.Data.DataRow x in DSDO.Rows)
            {
                if (!Equals(x["SRAnswerTypeFromMatrix"].ToString(), x["SRAnswerTypeFromQuestion"].ToString()))
                {
                    x["ErrorMessage"] = "Error: Answer type of question from Nursing Assessment is different from Question master";
                }
                else
                {
                    string SRAnswerTypeFromMatrix = x["SRAnswerTypeFromMatrix"].ToString();
                    string SRAnswerTypeFromQuestion = x["SRAnswerTypeFromQuestion"].ToString();
                    string Operand = x["Operand"].ToString();
                    char[] splitter = { '|' };
                    string AcceptedText = x["AcceptedText"].ToString();
                    decimal AcceptedNum = (decimal)(x["AcceptedNum"] ?? 0);
                    bool AcceptedBool = (bool)x["CheckValue"];

                    string AnswerText = x["AnswerText"].ToString();
                    decimal ansNum = System.Convert.ToDecimal(x["AnswerNum"]);

                    x["Selected"] = IsAccepted(SRAnswerTypeFromMatrix, SRAnswerTypeFromQuestion,
                        AcceptedText, Operand, AnswerText, AcceptedNum,
                        ansNum, AcceptedBool, x["QuestionText"].ToString(),
                        System.Convert.ToInt16((x["AnswerDecimalDigit"] ?? 0)), x["AnswerSuffix"].ToString(),
                        (bool)(x["IsUsingRange"] ?? false),
                        (decimal)(x["AcceptedNum2"] ?? 0),
                        System.Convert.ToInt32(x["PatientAgeInMonth"]),
                        System.Convert.ToInt32(x["AgeInMonthStart"]),
                        System.Convert.ToInt32(x["AgeInMonthEnd"]));
                }
            }

            // cek pengecualian dari fungsi IsAccepted
            // misal seperti IMT vs LLA, kalau IMT ada isinya maka pake IMT, kalau tidak pakai LLA
            var exColl = new AppStandardReferenceItemCollection();
            exColl.Query.Where(exColl.Query.StandardReferenceID == "NcAssOrNum");
            exColl.LoadAll();
            foreach (var ex in exColl)
            {
                var sQIDs = ex.ItemName.Split('|');
                foreach (var sQID in sQIDs)
                {
                    var iData = DSDO.AsEnumerable().Where(x => x.Field<string>("QuestionID") == sQID);
                    if (iData.Count() == 0) continue;
                    if (iData.First().Field<decimal>("AnswerNum") != 0)
                    {
                        // keep this, remove others
                        var rowToDelete = DSDO.AsEnumerable().Where(x => sQIDs.Contains(x.Field<string>("QuestionID")) &&
                            x.Field<string>("QuestionID") != sQID);
                        foreach (var rtd in rowToDelete) rtd.Delete();
                        DSDO.AcceptChanges();
                        break;
                    }
                }
            }


            var acceptedDiag = (from d in DSDO.AsEnumerable()
                                where d.Field<bool>("Selected") == true
                                select d.Field<string>("TerminologyID")).ToArray();

            foreach (System.Data.DataRow r in dttbl.Rows)
            {
                r["AssCount"] = (from d in DSDO.AsEnumerable()
                                 where d.Field<bool>("Selected") == true
                                     && d.Field<string>("TerminologyID") == r["TerminologyIDParent"].ToString()
                                 select d.Field<string>("QuestionID")).Count();
            }
            dttbl.AcceptChanges();

            var row = dttbl.AsEnumerable().Where(
                x => acceptedDiag.Contains(x.Field<string>("TerminologyIDParent"))
            ).OrderByDescending(x => x.Field<int>("AssCount"));

            return row.AsDataView().ToTable();
        }
        #endregion
        #region NUTRITION CARE DIAGNOSA (10)
        public static string DeleteByIdL10(long id)
        {
            // yang didelete adalah L10 dan L11
            // jika sudah ada L20, L30, L31, L40 jangan dihapus
            var d = new NutritionCareDiagnoseTransDT();
            if (d.LoadByPrimaryKey(id))
            {
                // ambil childnya
                var dc = new NutritionCareDiagnoseTransDTCollection();
                dc.Query.Where(dc.Query.ParentID == d.ID);
                dc.LoadAll();
                if (dc.Where(x => x.SRNutritionCareTerminologyLevel != "11").Count() == 0)
                {
                    // delete L11 first
                    try
                    {
                        using (esTransactionScope trans = new esTransactionScope())
                        {
                            dc.MarkAllAsDeleted();
                            dc.Save();

                            d.MarkAsDeleted();
                            d.Save();
                            //Commit if success, Rollback if failed
                            trans.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                    return string.Empty;
                }
                else
                {
                    // can not delete, delete is forbidden
                    return "Can not delete!";
                }
            }
            else
            {
                return "Can not delete, record does not exist";
            }
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosaHasParentID(string TransactionNo, string Level)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            coll.Query.Where(coll.Query.TransactionNo == TransactionNo,
                coll.Query.SRNutritionCareTerminologyLevel == Level);
            coll.Query.Where(coll.Query.ParentID > 0);
            coll.LoadAll();
            return coll;
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosa(long ParentID, string Level)
        {
            return NutritionCareDiagnosa((new long[] { ParentID }), Level);
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosa(long[] ParentIDs, string Level)
        {
            return NutritionCareDiagnosa(ParentIDs, new string[] { Level });
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosa(long[] ParentIDs, string[] Levels)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            coll.Query.Where(coll.Query.ParentID.In(ParentIDs),
                coll.Query.SRNutritionCareTerminologyLevel.In(Levels));
            //coll.Query.OrderBy(coll.Query.TerminologyID.Ascending);
            coll.LoadAll();
            return coll;
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosa(string TransactionNo, string Level)
        {
            return NutritionCareDiagnosa(TransactionNo, new string[] { Level });
        }
        public static NutritionCareDiagnoseTransDTCollection NutritionCareDiagnosa(string TransactionNo, string[] Levels)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            coll.Query.Where(coll.Query.TransactionNo == TransactionNo,
                coll.Query.SRNutritionCareTerminologyLevel.In(Levels));
            coll.LoadAll();
            return coll;
        }
        public static DataTable NutritionCareDiagnosaFullDefinitionWithNicNoc(string TransactionNo)
        {
            var dttbl = NutritionCareDiagnosaFullDefinition(TransactionNo);
            // NOC header dan NIC
            var nocnic = new NutritionCareDiagnoseTransDTCollection();
            if (dttbl.Rows.Count > 0)
            {
                nocnic = NutritionCareDiagnosa(
                    (from d in dttbl.AsEnumerable() select d.Field<long>("Id")).ToArray(),
                    new string[] { "20"/*noc header*/, "30"/*nic*/});
            }

            // add new column NIC
            //DataColumn dc = new DataColumn("NIC", typeof(string));
            //dttbl.Columns.Add(dc);

            foreach (DataRow r in dttbl.Rows)
            {
                // NIC
                var nics = "";
                var xnic = nocnic.Where(x => x.SRNutritionCareTerminologyLevel == "30" && x.ParentID == (long)r["Id"]);
                foreach (var xn in xnic)
                {
                    var terminologyName = string.Empty;
                    var t = new NutritionCareTerminology();
                    t.LoadByPrimaryKey(xn.TerminologyParentID);

                    terminologyName = t.TerminologyName + " dengan " + xn.TerminologyName;

                    nics += "<li>" + terminologyName + "</li>";
                    //nics += "<li>" + xn.TerminologyName + "</li>";
                }
                if (nics != string.Empty) nics = "<ul>" + nics + "</ul>";
                r["NIC"] = nics;

                // NOC Header
                var nocHs = "";
                var xnocH = nocnic.Where(x => x.SRNutritionCareTerminologyLevel == "20" && x.ParentID == (long)r["Id"]);
                foreach (var xn in xnocH)
                {
                    // NOC
                    string snoc = string.Empty;
                    var nocs = NutritionCareDiagnosa(xn.ID.Value, "21");
                    foreach (var noc in nocs)
                    {
                        snoc += "<li>" + noc.TerminologyName + "</li>";
                    }
                    if (xn.TerminologyName.Contains("NOC"))
                    {
                        nocHs += snoc;
                    }
                    else
                    {
                        if (snoc != string.Empty) snoc = "<ul>" + snoc + "</ul>";
                        nocHs += "<li>" + xn.TerminologyName + "</li>" + snoc;
                    }
                }
                if (nocHs != string.Empty) nocHs = "<ul>" + nocHs + "</ul>";
                r["NOC"] = nocHs;
            }
            dttbl.AcceptChanges();

            // belum selesai
            return dttbl;
        }
        public static DataTable NutritionCareDiagnosaFullDefinition(string TransactionNo)
        {
            var query = new NutritionCareTerminologyQuery("a");
            var ddt = new NutritionCareDiagnoseTransDTQuery("b");
            
            query.InnerJoin(ddt).On(query.TerminologyID == ddt.TerminologyID);
                
            query.Where(ddt.TransactionNo == TransactionNo,
                query.SRNutritionCareTerminologyLevel == "10");
            query.Select(
                query,
                ddt.ID,
                query.TerminologyName.As("TerminologyNameDisplay"),
                ddt.CreateDateTime.As("NutritionCaregDiagnosaCreateDateTime"),
                ddt.S,
                ddt.O,
                ddt.D,
                ddt.I,
                ddt.ME,
                ddt.ExecuteDateTime,
                "<'' Noc>",
                "<'' Nic>",
                "<0 Non11ChildCount>"
            ).OrderBy(ddt.CreateDateTime.Ascending);
            query.es.Distinct = true;
            //coll.Load(query);
            var dttbl = query.LoadDataTable();

            foreach (System.Data.DataRow c in dttbl.Rows)
            {
                // ambil jumlah child yang selain level 11
                var dc = new NutritionCareDiagnoseTransDTCollection();
                dc.Query.Where(dc.Query.ParentID == c["ID"] && dc.Query.SRNutritionCareTerminologyLevel != "11");
                if (dc.LoadAll())
                {
                    c["Non11ChildCount"] = dc.Count;
                }

                // problem
                var collProb = new NutritionCareDiagnoseTransDTCollection();
                var qsprob = new NutritionCareDiagnoseTransDTQuery("b");
                qsprob.Where(qsprob.ParentID == c["ID"] && qsprob.SRNutritionCareTerminologyLevel == "11");
                collProb.Load(qsprob);

                string sBrhbngDng = string.Empty;
                foreach (var iProb in collProb)
                {
                    sBrhbngDng += string.IsNullOrEmpty(sBrhbngDng) ? "" : ", ";
                    sBrhbngDng += iProb.TerminologyName;
                }
                
                c["TerminologyNameDisplay"] = c["TerminologyNameDisplay"].ToString() + " berhubungan dengan " + sBrhbngDng;

                string sDitndiDng = string.Empty;
                // cek dulu di DS DO etiology ada isi atau tidak
                // kalau ada isi berarti DS DO dari implementasi yang diangkat baru, pakai saja DS DO-nya bulat2
                foreach (var iProb in collProb)
                {
                    if (!string.IsNullOrEmpty(iProb.S) || !string.IsNullOrEmpty(iProb.O))
                    {
                        sDitndiDng += FormatSymptomText(iProb.S, iProb.O);
                        break; /*break aja karena 1 diagnosa bisa lebih dari satu etiology.
                                kalau tidak break nanti "ditandai dengan"-nya jadi double-double*/
                    }
                }

                if (string.IsNullOrEmpty(sDitndiDng))
                {
                    // assessment
                    var natdt = new NutritionCareAssessmentTransDTQuery("a");
                    var ass = new NutritionCareAssessmentQuestionQuery("b");
                    var nathd = new NutritionCareAssessmentTransHDQuery("c");
                    var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("d");
                    var ndtdt = new NutritionCareDiagnoseTransDTQuery("e");
                    var nth = new NutritionCareTransHDQuery("nth");
                    var reg = new RegistrationQuery("reg");

                    natdt.InnerJoin(nathd).On(natdt.HDID == nathd.ID)
                        .InnerJoin(nad).On(natdt.QuestionID == nad.QuestionID)
                        .InnerJoin(ndtdt).On(nad.TerminologyID == ndtdt.TerminologyID)
                        .InnerJoin(ass).On(natdt.QuestionID == ass.QuestionID)
                        .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                        .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                        .Where(nathd.TransactionNo == TransactionNo,
                            ndtdt.TerminologyID == c["TerminologyID"].ToString())
                        .Select(
                            natdt.QuestionID,
                            natdt.QuestionText,
                            natdt.AnswerText,
                            natdt.AnswerNum,
                            ass.AnswerPrefix,
                            ass.AnswerSuffix,
                            ass.AnswerDecimalDigit,
                            ass.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                            nad.AcceptedText,
                            nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.CheckValue,
                            nad.IsUsingRange,
                            nad.Operand,
                            nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                            "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd
                        );
                    natdt.es.Distinct = true;

                    //collAss.Load(qsass);
                    var dtb = natdt.LoadDataTable();

                    foreach (System.Data.DataRow iProb in dtb.Rows)
                    {
                        string sAnsNum = iProb["AnswerNum"].ToString();
                        decimal AnsNum = 0;
                        if (!sAnsNum.Equals(string.Empty))
                        {
                            try
                            {
                                AnsNum = decimal.Parse(sAnsNum);
                            }
                            catch (Exception e) { }
                        }

                        string sAnsDigt = iProb["AnswerDecimalDigit"].ToString();
                        int AnsDigt = 0;
                        if (!sAnsDigt.Equals(string.Empty))
                        {
                            try
                            {
                                AnsDigt = int.Parse(sAnsDigt);
                            }
                            catch (Exception e) { }
                        }

                        string SRAnswerTypeFromQuestion = iProb["SRAnswerTypeFromQuestion"].ToString();
                        string SRAnswerTypeFromMatrix = iProb["SRAnswerTypeFromMatrix"].ToString();
                        string Operand = iProb["Operand"].ToString();
                        char[] splitter = { '|' };
                        string AcceptedText = iProb["AcceptedText"].ToString();
                        decimal AcceptedNum = (decimal)(iProb["AcceptedNum"] ?? 0);
                        bool AcceptedBool = (bool)iProb["CheckValue"];

                        string AnswerText = iProb["AnswerText"].ToString();

                        var sAss = GetAssessmentParsedString(SRAnswerTypeFromQuestion, SRAnswerTypeFromMatrix, AcceptedText, Operand,
                            AnswerText, AcceptedNum, AnsNum, AcceptedBool, iProb["QuestionText"].ToString(),
                            System.Convert.ToInt16((iProb["AnswerDecimalDigit"] ?? 0)), iProb["AnswerSuffix"].ToString(),
                            (bool)(iProb["IsUsingRange"] ?? false),
                            (decimal)(iProb["AcceptedNum2"] ?? 0),
                            System.Convert.ToInt32(iProb["PatientAgeInMonth"]),
                            System.Convert.ToInt32(iProb["AgeInMonthStart"]),
                            System.Convert.ToInt32(iProb["AgeInMonthEnd"]));

                        if (string.IsNullOrEmpty(sAss)) continue;

                        // untuk mencegah double assessment, contoh: mapping text ada nyeri dada,
                        // tapi di checkbox juga ada nyeri dada, maka ditandainya jadi 
                        // nyeri dada, nyeri dada
                        if (sDitndiDng.ToLower().IndexOf(sAss.ToLower().Trim()) >= 0) continue;
                    
                        sDitndiDng += string.IsNullOrEmpty(sDitndiDng) ? "" : ", ";

                        sDitndiDng += sAss;
                    }
                    
                }
                c["TerminologyNameDisplay"] = c["TerminologyNameDisplay"].ToString() +
                    //" ditandai dengan " + 
                    " " + AppParameter.GetParameterValue(AppParameter.ParameterItem.NsSymptom) + " " +
                    sDitndiDng;
            }

            //return coll;
            return dttbl;
        }
        public static DataTable NutritionCareDiagnosaFullDefinitionSingleWithNicNoc(string TransactionNo, long id)
        {
            var dttbl = NutritionCareDiagnosaFullDefinitionSingle(TransactionNo, id);
            
            // NOC header dan NIC
            var nocnic = new NutritionCareDiagnoseTransDTCollection();
            if (dttbl.Rows.Count > 0)
            {
                nocnic = NutritionCareDiagnosa(
                    (from d in dttbl.AsEnumerable() select d.Field<long>("Id")).ToArray(),
                    new string[] { "11"/*noc header*/, "30"/*nic*/});
            }

            // add new column NIC
            //DataColumn dc = new DataColumn("NIC", typeof(string));
            //dttbl.Columns.Add(dc);

            foreach (DataRow r in dttbl.Rows)
            {
                // NIC
                var nics = "";
                var xnic = nocnic.Where(x => x.SRNutritionCareTerminologyLevel == "30" && x.ParentID == (long)r["Id"]);
                foreach (var xn in xnic)
                {
                    var terminologyName = string.Empty;
                    var t = new NutritionCareTerminology();
                    t.LoadByPrimaryKey(xn.TerminologyParentID);
                    
                    terminologyName = t.TerminologyName + " dengan " + xn.TerminologyName;

                    nics += "<li>" + terminologyName + "</li>";
                }
                if (nics != string.Empty) nics = "<ul>" + nics + "</ul>";
                r["NIC"] = nics;

                // NOC Header
                var nocHs = "";
                var xnocH = nocnic.Where(x => x.SRNutritionCareTerminologyLevel == "11" && x.ParentID == (long)r["Id"]);
                var s = string.Empty;
                var o = string.Empty;
                foreach (var xn in xnocH)
                {
                    if (s == string.Empty && o == string.Empty)
                    {
                        nocHs += "<li>" + xn.S + "</li>";
                        nocHs += "<li>" + xn.O + "</li>";
                        s = xn.S;
                        o = xn.O;
                    }
                }
                if (nocHs != string.Empty) nocHs = "<ul>" + nocHs + "</ul>";
                r["NOC"] = nocHs;
            }
            dttbl.AcceptChanges();

            // belum selesai
            return dttbl;
        }
        public static DataTable NutritionCareDiagnosaFullDefinitionSingle(string TransactionNo, long id)
        {
            var query = new NutritionCareTerminologyQuery("a");
            var ddt = new NutritionCareDiagnoseTransDTQuery("b");

            query.InnerJoin(ddt).On(query.TerminologyID == ddt.TerminologyID);

            query.Where(ddt.ID == id);
            query.Select(
                query,
                ddt.ID,
                query.TerminologyName.As("TerminologyNameDisplay"),
                ddt.CreateDateTime.As("NutritionCaregDiagnosaCreateDateTime"),
                ddt.S,
                ddt.O,
                ddt.D,
                ddt.I,
                ddt.ME,
                ddt.ExecuteDateTime,
                "<'' Noc>",
                "<'' Nic>",
                "<0 Non11ChildCount>"
            ).OrderBy(ddt.CreateDateTime.Ascending);
            query.es.Distinct = true;
            //coll.Load(query);
            var dttbl = query.LoadDataTable();

            foreach (System.Data.DataRow c in dttbl.Rows)
            {
                // ambil jumlah child yang selain level 11
                var dc = new NutritionCareDiagnoseTransDTCollection();
                dc.Query.Where(dc.Query.ParentID == c["ID"] && dc.Query.SRNutritionCareTerminologyLevel != "11");
                if (dc.LoadAll())
                {
                    c["Non11ChildCount"] = dc.Count;
                }

                // problem
                var collProb = new NutritionCareDiagnoseTransDTCollection();
                var qsprob = new NutritionCareDiagnoseTransDTQuery("b");
                qsprob.Where(qsprob.ParentID == c["ID"] && qsprob.SRNutritionCareTerminologyLevel == "11");
                collProb.Load(qsprob);

                string sBrhbngDng = string.Empty;
                foreach (var iProb in collProb)
                {
                    sBrhbngDng += string.IsNullOrEmpty(sBrhbngDng) ? "" : ", ";
                    sBrhbngDng += iProb.TerminologyName;
                }

                c["TerminologyNameDisplay"] = c["TerminologyNameDisplay"].ToString() + " berhubungan dengan " + sBrhbngDng;

                string sDitndiDng = string.Empty;
                // cek dulu di DS DO etiology ada isi atau tidak
                // kalau ada isi berarti DS DO dari implementasi yang diangkat baru, pakai saja DS DO-nya bulat2
                foreach (var iProb in collProb)
                {
                    if (!string.IsNullOrEmpty(iProb.S) || !string.IsNullOrEmpty(iProb.O))
                    {
                        sDitndiDng += FormatSymptomText(iProb.S, iProb.O);
                        break; /*break aja karena 1 diagnosa bisa lebih dari satu etiology.
                                kalau tidak break nanti "ditandai dengan"-nya jadi double-double*/
                    }
                }

                if (string.IsNullOrEmpty(sDitndiDng))
                {
                    // assessment
                    var natdt = new NutritionCareAssessmentTransDTQuery("a");
                    var ass = new NutritionCareAssessmentQuestionQuery("b");
                    var nathd = new NutritionCareAssessmentTransHDQuery("c");
                    var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("d");
                    var ndtdt = new NutritionCareDiagnoseTransDTQuery("e");
                    var nth = new NutritionCareTransHDQuery("nth");
                    var reg = new RegistrationQuery("reg");

                    natdt.InnerJoin(nathd).On(natdt.HDID == nathd.ID)
                        .InnerJoin(nad).On(natdt.QuestionID == nad.QuestionID)
                        .InnerJoin(ndtdt).On(nad.TerminologyID == ndtdt.TerminologyID)
                        .InnerJoin(ass).On(natdt.QuestionID == ass.QuestionID)
                        .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                        .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                        .Where(nathd.TransactionNo == TransactionNo,
                            ndtdt.TerminologyID == c["TerminologyID"].ToString())
                        .Select(
                            natdt.QuestionID,
                            natdt.QuestionText,
                            natdt.AnswerText,
                            natdt.AnswerNum,
                            ass.AnswerPrefix,
                            ass.AnswerSuffix,
                            ass.AnswerDecimalDigit,
                            ass.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                            nad.AcceptedText,
                            nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.CheckValue,
                            nad.IsUsingRange,
                            nad.Operand,
                            nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                            "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd
                        );
                    natdt.es.Distinct = true;

                    //collAss.Load(qsass);
                    var dtb = natdt.LoadDataTable();

                    foreach (System.Data.DataRow iProb in dtb.Rows)
                    {
                        string sAnsNum = iProb["AnswerNum"].ToString();
                        decimal AnsNum = 0;
                        if (!sAnsNum.Equals(string.Empty))
                        {
                            try
                            {
                                AnsNum = decimal.Parse(sAnsNum);
                            }
                            catch (Exception e) { }
                        }

                        string sAnsDigt = iProb["AnswerDecimalDigit"].ToString();
                        int AnsDigt = 0;
                        if (!sAnsDigt.Equals(string.Empty))
                        {
                            try
                            {
                                AnsDigt = int.Parse(sAnsDigt);
                            }
                            catch (Exception e) { }
                        }

                        string SRAnswerTypeFromQuestion = iProb["SRAnswerTypeFromQuestion"].ToString();
                        string SRAnswerTypeFromMatrix = iProb["SRAnswerTypeFromMatrix"].ToString();
                        string Operand = iProb["Operand"].ToString();
                        char[] splitter = { '|' };
                        string AcceptedText = iProb["AcceptedText"].ToString();
                        decimal AcceptedNum = (decimal)(iProb["AcceptedNum"] ?? 0);
                        bool AcceptedBool = (bool)iProb["CheckValue"];

                        string AnswerText = iProb["AnswerText"].ToString();

                        var sAss = GetAssessmentParsedString(SRAnswerTypeFromQuestion, SRAnswerTypeFromMatrix, AcceptedText, Operand,
                            AnswerText, AcceptedNum, AnsNum, AcceptedBool, iProb["QuestionText"].ToString(),
                            System.Convert.ToInt16((iProb["AnswerDecimalDigit"] ?? 0)), iProb["AnswerSuffix"].ToString(),
                            (bool)(iProb["IsUsingRange"] ?? false),
                            (decimal)(iProb["AcceptedNum2"] ?? 0),
                            System.Convert.ToInt32(iProb["PatientAgeInMonth"]),
                            System.Convert.ToInt32(iProb["AgeInMonthStart"]),
                            System.Convert.ToInt32(iProb["AgeInMonthEnd"]));

                        if (string.IsNullOrEmpty(sAss)) continue;

                        // untuk mencegah double assessment, contoh: mapping text ada nyeri dada,
                        // tapi di checkbox juga ada nyeri dada, maka ditandainya jadi 
                        // nyeri dada, nyeri dada
                        if (sDitndiDng.ToLower().IndexOf(sAss.ToLower().Trim()) >= 0) continue;

                        sDitndiDng += string.IsNullOrEmpty(sDitndiDng) ? "" : ", ";

                        sDitndiDng += sAss;
                    }

                }
                c["TerminologyNameDisplay"] = c["TerminologyNameDisplay"].ToString() +
                    //" ditandai dengan " + 
                    " " + AppParameter.GetParameterValue(AppParameter.ParameterItem.NsSymptom) + " " +
                    sDitndiDng;
            }

            //return coll;
            return dttbl;
        }

        public static List<AssessmentRet> NutritionCareAssessmentForPrint(string TransactionNo, long DiagnosaID)
        {
            List<AssessmentRet> lList = new List<AssessmentRet>();

            var Diag = new NutritionCareDiagnoseTransDT();
            if (Diag.LoadByPrimaryKey(DiagnosaID))
            {
                // problem
                var collProb = new NutritionCareDiagnoseTransDTCollection();
                var qsprob = new NutritionCareDiagnoseTransDTQuery("b");
                qsprob.Where(qsprob.ParentID == DiagnosaID && qsprob.SRNutritionCareTerminologyLevel == "11");
                collProb.Load(qsprob);

                string sDitndiDng = string.Empty;
                // cek dulu di DS DO etiology ada isi atau tidak
                // kalau ada isi berarti DS DO dari implementasi yang diangkat baru, pakai saja DS DO-nya bulat2
                foreach (var iProb in collProb)
                {
                    if (!string.IsNullOrEmpty(iProb.S) || !string.IsNullOrEmpty(iProb.O))
                    {
                        lList.AddRange(FormatSymptomTextList(iProb.S, iProb.O));
                    }
                }

                if (lList.Count == 0)
                {
                    var std = new AppStandardReferenceItemCollection();
                    std.Query.Where(std.Query.StandardReferenceID == "NsAssAlwaysPrint");
                    std.LoadAll();
                    var sTTV = std.Select(x => x.ItemID).ToArray();

                    // assessment
                    var natdt = new NutritionCareAssessmentTransDTQuery("a");
                    var ass = new NutritionCareAssessmentQuestionQuery("b");
                    var nathd = new NutritionCareAssessmentTransHDQuery("c");
                    var nad = new NutritionCareAssessmentQuestionDiagnoseQuery("d");
                    var ndtdt = new NutritionCareDiagnoseTransDTQuery("e");
                    var nth = new NutritionCareTransHDQuery("nth");
                    var reg = new RegistrationQuery("reg");


                    natdt.InnerJoin(nathd).On(natdt.HDID == nathd.ID)
                        .LeftJoin(nad).On(natdt.QuestionID == nad.QuestionID)
                        .LeftJoin(ndtdt).On(nad.TerminologyID == ndtdt.TerminologyID)
                        .InnerJoin(ass).On(natdt.QuestionID == ass.QuestionID)
                        .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                        .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                        .Where(nathd.TransactionNo == TransactionNo)
                        .Where(natdt.Or(ndtdt.TerminologyID == Diag.TerminologyID,
                            natdt.QuestionID.In(sTTV)
                        ))
                        .Select(
                            natdt.QuestionID,
                            natdt.QuestionText,
                            natdt.AnswerText,
                            natdt.AnswerNum,
                            ass.AnswerPrefix,
                            ass.AnswerSuffix,
                            ass.AnswerDecimalDigit,
                            ass.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                            nad.AcceptedText,
                            nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.CheckValue,
                            nad.IsUsingRange,
                            nad.Operand,
                            nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                            "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd
                        );
                    natdt.es.Distinct = true;

                    //collAss.Load(qsass);
                    var dtb = natdt.LoadDataTable();

                    foreach (System.Data.DataRow iProb in dtb.Rows)
                    {
                        string sAnsNum = iProb["AnswerNum"].ToString();
                        decimal AnsNum = 0;
                        if (!sAnsNum.Equals(string.Empty))
                        {
                            try
                            {
                                AnsNum = decimal.Parse(sAnsNum);
                            }
                            catch (Exception e) { }
                        }

                        string sAnsDigt = iProb["AnswerDecimalDigit"].ToString();
                        int AnsDigt = 0;
                        if (!sAnsDigt.Equals(string.Empty))
                        {
                            try
                            {
                                AnsDigt = int.Parse(sAnsDigt);
                            }
                            catch (Exception e) { }
                        }

                        bool AlwaysPrint = sTTV.Contains(iProb["QuestionID"]);

                        string SRAnswerTypeFromQuestion = iProb["SRAnswerTypeFromQuestion"].ToString();
                        string SRAnswerTypeFromMatrix = AlwaysPrint ?
                            (iProb["SRAnswerTypeFromMatrix"].ToString() == string.Empty ? SRAnswerTypeFromQuestion : iProb["SRAnswerTypeFromMatrix"].ToString())
                            : iProb["SRAnswerTypeFromMatrix"].ToString();
                        string Operand = iProb["Operand"].ToString();
                        char[] splitter = { '|' };
                        string AcceptedText = iProb["AcceptedText"].ToString();
                        decimal AcceptedNum = System.Convert.ToDecimal((iProb["AcceptedNum"]) is DBNull ? 0.00 : iProb["AcceptedNum"]);
                        bool AcceptedBool = System.Convert.ToBoolean((iProb["CheckValue"]) is DBNull ? false : iProb["CheckValue"]);

                        string AnswerText = iProb["AnswerText"].ToString();

                        var sAss = GetAssessmentParsedList(SRAnswerTypeFromQuestion, SRAnswerTypeFromMatrix, AcceptedText, Operand,
                            AnswerText, AcceptedNum, AnsNum, AcceptedBool, iProb["QuestionText"].ToString(),
                            System.Convert.ToInt16((iProb["AnswerDecimalDigit"] ?? 0)), iProb["AnswerSuffix"].ToString(),
                            System.Convert.ToBoolean((iProb["IsUsingRange"]) is DBNull ? false : iProb["IsUsingRange"]),
                            System.Convert.ToDecimal((iProb["AcceptedNum2"]) is DBNull ? 0.00 : iProb["AcceptedNum2"]),
                            System.Convert.ToInt16(iProb["PatientAgeInMonth"]),
                            System.Convert.ToInt16(iProb["AgeInMonthStart"] is DBNull ? 0 : iProb["AgeInMonthStart"]),
                            System.Convert.ToInt16(iProb["AgeInMonthEnd"] is DBNull ? 0 : iProb["AgeInMonthEnd"]),
                            AlwaysPrint);

                        // untuk mencegah double assessment, contoh: mapping text ada nyeri dada,
                        // tapi di checkbox juga ada nyeri dada, maka ditandainya jadi 
                        // nyeri dada, nyeri dada
                        foreach (var ass2 in sAss)
                        {
                            if ((from i in lList
                                 where i.AssessmentName.ToLower().Trim() ==
                                     ass2.AssessmentName.ToLower().Trim()
                                 select i).Count() == 0)
                            {
                                lList.Add(ass2);
                                continue;
                            }
                        }

                        //lList.AddRange(sAss);
                    }
                }
            }
            return lList.Distinct().ToList();
        }
        
        private static List<AssessmentRet> FormatSymptomTextList(string DS, string DO)
        {
            return FormatSymptomTextList(DS, 0).Union(FormatSymptomTextList(DO, 1)).ToList();
        }
        private static List<AssessmentRet> FormatSymptomTextList(string t, int SO /*0=S, 1=O*/)
        {
            List<AssessmentRet> ret = new List<AssessmentRet>();
            string[] splitter = { '\n'.ToString(), ", ", ". " };
            if (!string.IsNullOrEmpty(t))
            {
                string[] sPart = t.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in sPart)
                {
                    var r = new AssessmentRet();
                    r.AssessmentName = part;
                    r.IsSubjective = SO == 0;
                    r.IsObjective = SO == 1;
                    ret.Add(r);
                }
            }
            else
            {

            }
            return ret;
        }

        private static string FormatSymptomText(string DS, string DO)
        {
            DS = FormatSymptomText(DS);
            DO = FormatSymptomText(DO);

            return (FormatSymptomText(DS) +
                (((DS.Length > 0) && (DO.Length > 0)) ? ", " : "") +
                FormatSymptomText(DO)).Trim();
        }
        private static string FormatSymptomText(string t)
        {
            string ret = string.Empty;
            string[] splitter = { '\n'.ToString(), ", ", ". " };
            if (!string.IsNullOrEmpty(t))
            {
                string[] sPart = t.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in sPart)
                {
                    ret += ((ret.Length == 0) ? "" : ", ") + part;
                }
            }
            else
            {

            }
            return ret;
        }
        #endregion
        #region NUTRITION CARE PLANNING (NIC) (30)
        public static DataTable NutritionCarePlanning_Depre(string TransactionNo, string TerminologyParentID)
        {
            var query = new NutritionCareTerminologyQuery("a");
            var pr = new NutritionCareTerminologyQuery("b");

            var dt = new NutritionCareDiagnoseTransDTQuery("e");

            query.es.Distinct = true;

            query.InnerJoin(pr).On(query.TerminologyParentID == pr.TerminologyID);
            query.LeftJoin(dt).On(query.TerminologyID == dt.TerminologyID
                & dt.TransactionNo == TransactionNo);

            query.Where(
                query.SRNutritionCareTerminologyLevel == "30");

            if (TerminologyParentID != string.Empty)
            {
                query.Where(query.TerminologyParentID == TerminologyParentID);
            }

            query.Select(
                query,
                "<ISNULL(e.TerminologyName, a.TerminologyName) TerminologyNameEdited>",
                pr.SequenceNo.As("ParentSequenceNo"),
                pr.TerminologyName.As("TerminologyParentName"),
                dt.TerminologyID.As("TransTerminologyID"),
                dt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dt.LastUpdateDateTime.As("LastUpdateTransDTDateTime")
                )
                .OrderBy("<ISNULL(e.TerminologyName, a.TerminologyName)>");

            var dttbl = query.LoadDataTable();
            return dttbl;
        }

        public static DataTable NutritionCarePlanning(/*string TransactionNo, */long IdDiagL10)
        {
            var nic = new NutritionCareTerminologyQuery("a");
            var diag = new NutritionCareTerminologyQuery("b");
            var dom = new NutritionCareTerminologyQuery("c");
            var dtDiag = new NutritionCareDiagnoseTransDTQuery("d");
            var dtNic = new NutritionCareDiagnoseTransDTQuery("e");
            var uq = new AppUserQuery("uq");

            nic.InnerJoin(diag).On(nic.TerminologyParentID == diag.TerminologyID &
                nic.SRNutritionCareTerminologyLevel == "30");
            nic.InnerJoin(dom).On(nic.DomainID == dom.TerminologyID &
                dom.SRNutritionCareTerminologyLevel == "00");
            nic.LeftJoin(dtNic).On(dtNic.ParentID == IdDiagL10 & nic.TerminologyID == dtNic.TerminologyID);
            nic.LeftJoin(uq).On(dtNic.LastUpdateByUserID == uq.UserID);

            nic.Select(
                nic,
                "<ISNULL(e.TerminologyName, a.TerminologyName) TerminologyNameEdited>",
                diag.SequenceNo.As("ParentSequenceNo"),
                diag.TerminologyName.As("TerminologyParentName"),
                dtNic.TerminologyID.As("TransTerminologyID"),
                dtNic.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dtNic.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                uq.UserName,
                nic.DomainID,
                dom.TerminologyName.As("DomainName"),
                dom.SequenceNo.As("DomainSeqNo")
                )
                .OrderBy(nic.SequenceNo.Ascending);
                //.OrderBy(nic.TerminologyID.Ascending);
            
            var dttbl = nic.LoadDataTable();

            return dttbl;
        }
        #endregion
        #region NUTRITION IMPLEMENTASI (31)
        public static NutritionCareDiagnoseTransDTCollection Implementation(string TransactionNo, int Top)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            var query = new NutritionCareDiagnoseTransDTQuery("a");
            var uq = new AppUserQuery("uq");

            if (Top > 0) query.es.Top = Top;

            query.LeftJoin(uq).On(query.LastUpdateByUserID == uq.UserID);
            query.Where(query.TransactionNo == TransactionNo);
            query.Where(query.Or(
                    query.SRNutritionCareTerminologyLevel == "31",
                    query.SRNutritionCareTerminologyLevel == "32"
                ));
            query.Select(
                query,
                uq.UserName.As("refTo_UserName")
                );
            query.OrderBy(query.CreateDateTime.Descending);
            coll.Load(query);

            return coll;
        }

        #region NUTRITION EVALUATION (40)
        public static NutritionCareDiagnoseTransDTCollection Evaluation(string TransactionNo)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            var query = new NutritionCareDiagnoseTransDTQuery("a");

            query.Where(query.TransactionNo == TransactionNo);
            query.Where(query.Or(
                    query.SRNutritionCareTerminologyLevel == "40"
                ));
            query.Select(
                query
                );
            coll.Load(query);

            return coll;
        }

        public static NutritionCareDiagnoseTransDTCollection Evaluation(long idDiagnoseL10)
        {
            var coll = new NutritionCareDiagnoseTransDTCollection();
            var query = new NutritionCareDiagnoseTransDTQuery("a");

            query.Where(query.ParentID == idDiagnoseL10, query.SRNutritionCareTerminologyLevel == "40");
            coll.Load(query);

            return coll;
        }
        #endregion
        #endregion
        #endregion
        #region ASSESSMENT
        public static QuestionCollection GetQuestionsByTemplateID(int TemplateID)
        {
            var qColl = new QuestionCollection();
            var q = new QuestionQuery("q");
            var t = new NutritionCareTerminologyTemplateDetailQuery("t");
            q.InnerJoin(t).On(q.QuestionID == t.QuestionID && t.TemplateID == TemplateID)
                .Select(q);
            qColl.Load(q);
            return qColl;
        }
        public static long GetIDNutritionCareAssessmentByRefNo(string QuestionFormReference)
        {
            var ns = new NutritionCareAssessmentTransHDCollection();
            ns.Query.Where(ns.Query.QuestionFormReference == QuestionFormReference);
            ns.LoadAll();
            return (long)(ns.Count > 0 ? ns[0].ID : 0);
        }

        public static DataTable NutritionCareAssessmentByRefNo(string QuestionFormReference)
        {
            var ns = new NutritionCareAssessmentTransHDCollection();
            ns.Query.Where(ns.Query.QuestionFormReference == QuestionFormReference);
            ns.LoadAll();
            if (ns.Count == 0)
            {
                throw new Exception("Related assessment can not be found or has been deleted.");
            }

            return NutritionCareAssessment(ns[0].ID.ToString(), true);
        }

        public static DataTable NutritionCareAssessment(string AssessmentID, bool AllAssessment)
        {
            var qna = new NutritionCareAssessmentQuestionQuery("a");

            var qHd = new NutritionCareAssessmentTransHDQuery("f");
            var qDt = new NutritionCareAssessmentTransDTQuery("e");

            Int64 hdID = string.IsNullOrEmpty(AssessmentID) ? 0 : Int64.Parse(AssessmentID);

            qna.LeftJoin(qDt).On(qna.QuestionID == qDt.QuestionID
                & qDt.HDID == hdID
            );
            qna.Select
                (
                    qna,
                    qDt.QuestionID.As("TransQuestionID"),
                    "<ISNULL(e.IsSubjective, 0) IsSub>",
                    "<ISNULL(e.IsObjective, 0) IsObj>",
                    "<ISNULL(e.QuestionText, a.QuestionText) QuestionTextEdited>",
                    "<ISNULL(e.AnswerText, '') AnswerText>",
                    qDt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                    qDt.LastUpdateDateTime.As("LastUpdateTransDTDateTime")
                );
            if (!AllAssessment) qna.Where(qDt.QuestionID.IsNotNull());
            qna.es.Distinct = true;
            var dttbl = qna.LoadDataTable();

            return dttbl;
        }

        public static List<AssessmentRet> NutritionCareAssessmentLastUpdate_DISABLED(string TransactionNo, string TerminologyID)
        {

            string cmd = @"
                WITH cte AS
                (
                   SELECT *,
                         ROW_NUMBER() OVER (PARTITION BY QuestionID ORDER BY HDID DESC) AS rn
                   FROM (
		                SELECT 
			                natd.*, 
			                naq.SRAnswerType AnswerTypeFromQuestion,
			                nad.SRAnswerType AnswerTypeFromMatrix,
			                nad.AcceptedText,
			                nad.Operand,
			                nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.IsUsingRange,
			                nad.CheckValue,
			                naq.AnswerDecimalDigit,
                            reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth,
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd
		                FROM NutritionCareAssessmentTransHD AS nath 
			                INNER JOIN NutritionCareAssessmentTransDT AS natd ON nath.ID = natd.HDID
			                INNER JOIN NutritionCareAssessmentQuestion AS naq ON naq.QuestionID = natd.QuestionID
			                INNER JOIN NutritionCareAssessmentQuestionDiagnose AS nad ON nad.QuestionID = naq.QuestionID
                            INNER JOIN NutritionCareTransHD nth ON nath.TransactionNo = nth.TransactionNo
                            INNER JOIN Registration reg ON nth.RegistrationNo = reg.RegistrationNo
		                WHERE nath.TransactionNo = @TransactionNo AND nad.TerminologyID = @TerminologyID
                   ) s
                )
                SELECT *
                FROM cte
                WHERE rn = 1
                ";
            esParameters par = new esParameters();
            par.Add("TransactionNo", TransactionNo);
            par.Add("TerminologyID", TerminologyID);

            var dt = (new Temiang.Dal.Core.esUtility()).FillDataTable(esQueryType.Text, cmd, par);

            List<AssessmentRet> lAssessment = new List<AssessmentRet>();
            foreach (DataRow d in dt.Rows)
            {
                var ls = GetAssessmentParsedList(
                    d["AnswerTypeFromQuestion"].ToString(),
                    d["AnswerTypeFromMatrix"].ToString(),
                    d["AcceptedText"].ToString(),
                    d["Operand"].ToString(),
                    (d["AnswerText"] ?? string.Empty).ToString(),
                    (decimal)(d["AcceptedNum"] ?? 0),
                    System.Convert.ToDecimal((d["AnswerNum"] is System.DBNull) ? 0 : d["AnswerNum"]),
                    (bool)(d["CheckValue"] ?? false),
                    d["QuestionText"].ToString(),
                    System.Convert.ToInt16((d["AnswerDecimalDigit"] ?? 0)),
                    d["AnswerSuffix"].ToString(),
                    (bool)(d["IsUsingRange"] ?? false),
                    (decimal)(d["AcceptedNum2"] ?? 0),
                    System.Convert.ToInt32(d["PatientAgeInMonth"]),
                    System.Convert.ToInt32(d["AgeInMonthStart"]),
                    System.Convert.ToInt32(d["AgeInMonthEnd"]),
                    false
                );

                foreach (var l in ls)
                {
                    l.IsSubjective = (bool)d["IsSubjective"];
                    l.IsObjective = (bool)d["IsObjective"];
                }

                lAssessment.AddRange(ls);
            }

            return lAssessment;
        }
        private static bool IsAccepted(string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd)
        {
            var Ret = GetAssessmentParsed(AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd, false);

            return (Ret.Count > 0);
        }

        private static string GetAssessmentParsedString(string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd)
        {
            var Ret = GetAssessmentParsed(AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd, false);

            string Ass = string.Empty;
            foreach (var r in Ret)
            {
                Ass += ((Ass.Length == 0) ? "" : ", ") + r.AssessmentName;
            }

            return Ass;
        }

        private static List<AssessmentRet> GetAssessmentParsedList(string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd, bool AlwaysReturn)
        {
            var Ret = GetAssessmentParsed(AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd, AlwaysReturn);

            // get pre
            var sPre = string.Empty;
            foreach (var r in Ret)
            {
                //sPre = string.IsNullOrEmpty(r.Pre) ? sPre : r.Pre;
                //r.AssessmentName = string.IsNullOrEmpty(sPre) ? r.AssessmentName : sPre + " " + r.AssessmentName;
                r.AssessmentName = string.IsNullOrEmpty(r.Pre) ? r.AssessmentName : r.Pre + " " + r.AssessmentName;
                //
                r.IsSubjective = !string.IsNullOrEmpty(r.Pre);
                r.IsObjective = string.IsNullOrEmpty(r.Pre);
            }

            return Ret.ToList();
        }

        private static List<AssessmentRet> GetAssessmentParsed(string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd, bool AlwaysReturn)
        {
            bool ret = false;
            string sRet = string.Empty;
            var l = new List<AssessmentRet>();

            if (!Equals(AnswerTypeFromQuestion, AnswerTypeFromMatrix)) return l;

            char[] splitter = { '|' };
            string[] TmpAcceptedTexts = AcceptedText.ToLower().Split(splitter);
            string[] ansTexts = AnswerText.ToLower().Split(splitter);

            var AcceptedTexts = (
                from a in TmpAcceptedTexts
                where !string.IsNullOrEmpty(a.Trim())
                select RemoveDoubleSpace(a.Trim())).ToArray();

            switch (AnswerTypeFromQuestion)
            {
                case "MSK":
                case "TXT":
                case "MEM":
                case "CBO":
                case "CBT": // pake nilai option saja
                case "CBN":
                case "CBM":
                case "CB2":
                case "TTX":
                    /*sRet = ansTexts[0] = ((
                        AnswerTypeFromQuestion == "CBO" ||
                        AnswerTypeFromQuestion == "CBT" ||
                        AnswerTypeFromQuestion == "CB2"
                        ) ? QuestionText : "") + " " + ansTexts[0];*/
                    sRet = ((AnswerTypeFromQuestion == "MEM") ? "" : QuestionText) + " " + ansTexts[0];
                    switch (Operand.ToLower())
                    {
                        case "=":
                            foreach (var at in AcceptedTexts)
                            {
                                if ((Equals(at.ToLower(), ansTexts[0].ToLower())
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret | true;

                                    var pAss = ParseAssessmentV2(sRet, at);
                                    if (pAss != null) l.AddRange(pAss);
                                }
                            }
                            break;
                        case "like":
                            foreach (var at in AcceptedTexts)
                            {
                                //if (ansTexts[0].IndexOf(at) >= 0 || AlwaysReturn)
                                if ((ContainsKeyWord(ansTexts[0], at)
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret | true;

                                    var pAss = ParseAssessmentV2(sRet, at);
                                    if (pAss != null) l.AddRange(pAss);
                                }
                            }
                            break;
                        case "notlike":
                            var templ = new List<AssessmentRet>();
                            foreach (var at in AcceptedTexts)
                            {
                                //if (ansTexts[0].IndexOf(at) < 0 || AlwaysReturn)
                                if (((!ContainsKeyWord(ansTexts[0], at))
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret & true;

                                    var pAss = ParseAssessmentV2(sRet, at);
                                    if (pAss != null) templ.AddRange(pAss);
                                }
                            }
                            if (ret) l.AddRange(templ);
                            break;
                        default:
                            ret = false;
                            break;
                    }
                    //if (ret)
                    //{
                    //    var pAss = ParseAssessment(ansTexts[0], at);
                    //    if (pAss != null) l.AddRange(pAss);
                    //}
                    break;
                //case "DAT":
                case "NUM":
                    sRet = QuestionText + " " + System.Math.Round(ansNum, RoundingDigit).ToString() + AnswerSuffix;
                    if (IsUsingRange)
                    {
                        ret = CekDoubleNum(ansNum, AcceptedNum, AcceptedNum2, Operand);
                    }
                    else
                    {
                        ret = CekSigleNum(ansNum, AcceptedNum, Operand);
                    }
                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd))
                        || AlwaysReturn)
                    {
                        var pAss = ParseAssessmentV2(sRet, sRet);
                        if (pAss != null) l.AddRange(pAss);
                    }
                    break;
                case "CHK":
                case "CTX":
                case "CDT":
                    sRet = QuestionText + ((ansTexts[0] == "1") ? string.Empty : ":tidak");
                    if (ansTexts.Length > 1)
                    {
                        sRet = sRet + (string.Equals(ansTexts[1], string.Empty) ? string.Empty : " " + ansTexts[1] + "");
                    }
                    ret = ((ansTexts[0] == "1") == AcceptedBool);

                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd))
                        || AlwaysReturn)
                    {
                        var pAss = ParseAssessmentV2(sRet, sRet);
                        if (pAss != null) l.AddRange(pAss);
                    }
                    break;
                case "CNM":
                    ret = ((ansTexts[0] == "1") == AcceptedBool);
                    decimal ans = 0;
                    try
                    {
                        ans = System.Convert.ToDecimal(ansTexts[1]);
                    }
                    catch
                    {
                        //
                    }

                    sRet = QuestionText + " " + System.Math.Round(ans, RoundingDigit).ToString() + AnswerSuffix;

                    if (IsUsingRange)
                    {
                        ret = ret & CekDoubleNum(ansNum, AcceptedNum, AcceptedNum2, Operand);
                    }
                    else
                    {
                        ret = ret & CekSigleNum(ansNum, AcceptedNum, Operand);
                    }

                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd)) || AlwaysReturn)
                    {
                        var pAss = ParseAssessmentV2(sRet, sRet);
                        if (pAss != null) l.AddRange(pAss);
                    }
                    break;
                default:
                    ret = false;
                    break;
            }

            foreach (var ll in l)
            {
                ll.IsAlwaysShow = AlwaysReturn;
            }

            // remove duplicate caused by double keywords
            return l.Distinct().ToList();
        }

        private static bool IsAgeInRange(int PatAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd)
        {
            return (AgeInMonthStart == 0 && AgeInMonthStart == AgeInMonthEnd) ||
                (PatAgeInMonth >= AgeInMonthStart && PatAgeInMonth <= AgeInMonthEnd);
        }

        private static bool ContainsKeyWord(string ansText, string keyWord)
        {
            keyWord = keyWord.Replace("  ", " ");
            char[] splitter = { ' ' };
            var keyWords = keyWord.Split(splitter);
            foreach (var key in keyWords)
            {
                if (ansText.IndexOf(key) < 0) return false;
            }
            return true;
        }

        private static bool CekSigleNum(decimal ansNum, decimal AcceptedNum, string Operand)
        {
            bool ret = false;
            switch (Operand.ToLower())
            {
                case "=":
                    ret = (ansNum == AcceptedNum);
                    break;
                case ">":
                    ret = (ansNum > AcceptedNum);
                    break;
                case "<":
                    ret = (ansNum < AcceptedNum);
                    break;
                case ">=":
                    ret = (ansNum >= AcceptedNum);
                    break;
                case "<=":
                    ret = (ansNum <= AcceptedNum);
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        private static bool CekDoubleNum(decimal ansNum, decimal AcceptedNum, decimal AcceptedNum2, string Operand)
        {
            bool ret = false;
            switch (Operand.ToLower())
            {
                case ">&<":
                    ret = (ansNum > AcceptedNum && ansNum < AcceptedNum2);
                    break;
                case ">=&<=":
                    ret = (ansNum >= AcceptedNum && ansNum <= AcceptedNum2);
                    break;
                case "<|>":
                    ret = (ansNum < AcceptedNum || ansNum > AcceptedNum2);
                    break;
                case "<=|>=":
                    ret = (ansNum <= AcceptedNum || ansNum >= AcceptedNum2);
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        #region parse assessment
        private static string RemoveDoubleSpace(string s)
        {
            return ((s.IndexOf("  ") == -1) ? s : RemoveDoubleSpace(s.Replace("  ", " ")));
        }

        private static string RemoveVocal(string s)
        {
            return s.Replace("a", "")
                .Replace("i", "")
                .Replace("u", "")
                .Replace("e", "")
                .Replace("o", "");
        }

        public class AssessmentRet : IEquatable<AssessmentRet>
        {
            public string AssessmentName;
            public string Pre;
            public bool IsSubjective;
            public bool IsObjective;
            public bool IsAlwaysShow;

            public bool Equals(AssessmentRet x)
            {
                if (x == null)
                {
                    return false;
                }

                return Pre + AssessmentName == x.Pre + x.AssessmentName;
            }

            public override int GetHashCode()
            {
                return (AssessmentName + Pre).GetHashCode();
            }
        }

        private static AssessmentRet[] ParseAssessment_(string s/*answertext*/, string k/*acceptedtext*/)
        {
            var i = s.IndexOf(k);
            if (i < 0 && s.Length > 0) return null;

            var r = new AssessmentRet();
            string[] aS = s.Split(new char[] { ','/*,'.','!'*/});

            var ls = new List<AssessmentRet>();
            var pre = string.Empty;
            foreach (var x in aS)
            {
                var spre = AssessmentSubjectiveDictionary(x);
                if (!string.IsNullOrEmpty(spre)) pre = spre;
                if (x.IndexOf(k) >= 0)
                {
                    ls.Add(new AssessmentRet() { AssessmentName = x, Pre = pre });
                }
            }

            //var list = (
            //    from x in aS
            //    where !(x.IndexOf(k) < 0)
            //    select new AssessmentRet()
            //    {
            //        AssessmentName = x,
            //        Pre = AssessmentSubjectiveDictionary(x)
            //    }).ToArray();
            pre = string.Empty;
            foreach (var l in ls)
            {
                if (!string.IsNullOrEmpty(l.Pre))
                {
                    pre = l.Pre;
                    l.AssessmentName = l.AssessmentName.Replace(pre, "").Trim();
                }
                else
                {
                    l.Pre = pre;
                }

            }

            return ls.ToArray();
        }

        private static AssessmentRet[] ParseAssessmentV2(string answer/*answertext*/, string key/*acceptedtext*/)
        {
            var lsRet = new List<AssessmentRet>();

            string[] splitter = { ". " };
            var strs = (answer + " ").Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in strs)
            {
                var i = ContainsKeyWord(str, key);// str.IndexOf(key);
                if ((!i) && str.Length > 0) continue;


                string[] aS = str.Split(new char[] { ','/*,'.','!'*/});

                var ls = new List<AssessmentRet>();
                var pre = string.Empty;
                foreach (var x in aS)
                {
                    var spre = AssessmentSubjectiveDictionary(x);
                    if (!string.IsNullOrEmpty(spre)) pre = spre;
                    if (ContainsKeyWord(x, key))//(x.IndexOf(key) >= 0)
                    {
                        ls.Add(new AssessmentRet() { AssessmentName = x, Pre = pre });
                    }
                }

                pre = string.Empty;
                foreach (var l in ls)
                {
                    if (!string.IsNullOrEmpty(l.Pre))
                    {
                        pre = l.Pre;
                        l.AssessmentName = l.AssessmentName.Replace(pre, "").Trim();
                    }
                    else
                    {
                        l.Pre = pre;
                    }
                }

                if (ls.Count == 0) ls.Add(new AssessmentRet() { AssessmentName = str, Pre = "" });

                lsRet.AddRange(ls);
            }
            return (lsRet.Count == 0) ? null : lsRet.ToArray();
        }

        public static string[] DSKeyWords = { };
        private static string[] GetDSKeyWords()
        {
            if (DSKeyWords.Length > 0) return DSKeyWords;

            var std = new AppStandardReferenceItemCollection();
            std.Query.Where(std.Query.StandardReferenceID == "NsAssDSKeyWord");
            if (std.LoadAll())
            {
                DSKeyWords = std.Select(x => x.ItemName).ToArray();
                return DSKeyWords;
            }
            else
            {
                return DSKeyWords;
            }
        }

        private static string AssessmentSubjectiveDictionary(string s)
        {
            //string[] sDictPrime = { "pasien", "patient", "keluarga" };

            string[] DefaultSDictSec = { "ngtkn", "brthkn", "ngfrm", "mlprkn" };
            List<string> sDictSec = GetDSKeyWords().ToList();
            sDictSec.AddRange(DefaultSDictSec);

            var aS = s.Split(new char[] { ' ' });
            string pre = string.Empty;
            //foreach (var w in aS)
            //{
            //    if (string.IsNullOrEmpty(w.Trim())) continue;
            //    pre += " " + w.Trim();
            //    foreach (var d in sDictPrime)
            //    {
            //        if (w.IndexOf(d) >= 0)
            //        {
            //            return pre;
            //        }
            //    }
            //}

            pre = string.Empty;
            foreach (var w in aS)
            {
                if (string.IsNullOrEmpty(w.Trim())) continue;
                pre += " " + w.Trim();
                foreach (var d in sDictSec)
                {
                    if (RemoveVocal(w).IndexOf(RemoveVocal(d)) > -1)
                    {
                        return pre;
                    }
                }
            }
            return string.Empty;
        }
        #endregion
        #endregion
        #region DETAIL EVALUATION
        public static NutritionCareDiagnoseEvaluationCollection DetailEvaluation(string TransactionNo)
        {
            var deColl = new NutritionCareDiagnoseEvaluationCollection();
            var de = new NutritionCareDiagnoseEvaluationQuery("de");
            var e = new NutritionCareDiagnoseTransDTQuery("e");
            de.InnerJoin(e).On(de.EvaluationID == e.ID)
                .Where(e.TransactionNo == TransactionNo)
                .Select(de);
            deColl.Load(de);
            return deColl;
        }
        #endregion

        #region Report Display Processing

        private static string FormatQuestionTextForReportDisplay(string QuestionText, string AnswerText, bool IsAlwaysPrint)
        {
            QuestionText = QuestionText.Trim();
            AnswerText = AnswerText.Trim();
            if (!QuestionText.Equals(string.Empty))
            {
                if (!QuestionText[QuestionText.Length - 1].Equals(':'))
                {
                    QuestionText += ":";
                }
            }

            if (IsAlwaysPrint)
            {
                return (QuestionText + " " + AnswerText).Trim();
            }
            else
            {
                return (AnswerText.Length > 0) ? (QuestionText + " " + AnswerText).Trim() : string.Empty;
            }
        }

        private static string QuestionReportDisplay(string QuestionText, string SRAnswerType,
            string QuestionAnswerText, string QuestionAnswerNum, string AnswerSuffix, bool IsAlwaysPrint,
            System.Data.DataRow row)
        {
            string sQuestion = string.Empty;
            string sAnswer = string.Empty;
            switch (SRAnswerType)
            {
                case "LBL":
                    {
                        sAnswer = QuestionText;
                        break;
                    }
                case "MSK":
                case "DAT":
                case "TIM":
                case "MEM":
                case "TXT":
                case "CBO":
                case "DTM":
                    {
                        sQuestion = QuestionText;
                        sAnswer = QuestionAnswerText;
                        break;
                    }
                case "NUM":
                    {
                        if (!QuestionAnswerNum.Equals("&nbsp;"))
                        {
                            sQuestion = QuestionText;
                            sAnswer = Convert.ToDouble(QuestionAnswerNum).ToString() + AnswerSuffix;
                        }
                        break;
                    }
                case "TTX":
                case "CB2":
                case "CBT":
                case "CBM":
                case "CBN":
                    {
                        string[] txtNat = QuestionAnswerText.Split(new char[] { '|' });
                        sQuestion = QuestionText;
                        sAnswer = ((txtNat.Length > 0 ? txtNat[0] : "") + " " +
                            ((txtNat.Length > 1 && !(new List<string>() { "tidak", "no" }).Contains(txtNat[0].ToLower().Trim())) ? txtNat[1] + AnswerSuffix : "")).Trim();
                        break;
                    }
                case "CHK":
                case "CTX":
                case "CTM":
                case "CNM":
                case "CDT":
                    {
                        string[] txtNat = QuestionAnswerText.Split(new char[] { '|' });
                        if ((txtNat[0]).Equals("1"))
                        {
                            sAnswer = QuestionText + " " + ((txtNat.Length > 1) ? ((txtNat[1]).Trim() + AnswerSuffix) : string.Empty);
                        }
                        break;
                    }
                case "TBL":
                    {
                        sAnswer = QuestionAnswerText;
                        break;
                    }
                default:
                    {
                        sAnswer = "Not Yet Implemented";
                        break;
                    }
            }

            row["SAnswer"] = sAnswer;
            row["QuestionText"] = sQuestion;

            return FormatQuestionTextForReportDisplay(sQuestion, sAnswer, IsAlwaysPrint);
        }

        #region ReportCustomFunction GENERAL
        class iToTbl
        {
            public string[] Questions;
            public string Name;
            public string Header;
            public string TblPrintAsGroup;
        }

        public static DataTable ReportDataSourceGENERAL(string TransactionNo, string RegistrationNo,
            string QuestionFormID)
        {

            var phrl = new PatientHealthRecordLineQuery("phrl");
            var q = new QuestionQuery("q");
            var phr = new PatientHealthRecordQuery("phr");
            var qig = new QuestionInGroupQuery("qig");
            var qgif = new QuestionGroupInFormQuery("qgif");
            var qg = new QuestionGroupQuery("qg");

            phrl.InnerJoin(q).On(phrl.QuestionID == q.QuestionID)
                    .InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo &&
                    phrl.RegistrationNo == phr.RegistrationNo && phrl.QuestionFormID == phr.QuestionFormID)
                    .InnerJoin(qig).On(q.QuestionID == qig.QuestionID)
                    .InnerJoin(qgif).On(qgif.QuestionGroupID == qig.QuestionGroupID)
                    .InnerJoin(qg).On(qig.QuestionGroupID == qg.QuestionGroupID)
                    .Where(
                        phrl.TransactionNo == TransactionNo,
                        phrl.RegistrationNo == RegistrationNo,
                        qgif.QuestionFormID == QuestionFormID
                    ).Select(q.QuestionID,
                        "<ISNULL(qig.ParentQuestionID,q.ParentQuestionID) ParentQuestionID>",
                        q.SRAnswerType, q.AnswerWidth, q.QuestionAnswerSelectionID,
                        phr.RecordDate, "<ISNULL(qig.QuestionLevel,q.QuestionLevel) QuestionLevel>",
                        qg.QuestionGroupName, q.QuestionText, phrl,
                        qgif.RowIndex, qig.RowIndex,
                        "<'' TextToDisplay>", "<ISNULL(q.IsAlwaysPrint,0) IsAlwaysPrint>",
                        "<'' TblPrintAsGroup>", "<'' SAnswer>"
                    ).OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending);

            var dt = phrl.LoadDataTable();

            string[] str = { "CHK", "CTX", "CTM" };
            foreach (System.Data.DataRow r in dt.Rows)
            {
                r["TextToDisplay"] = QuestionReportDisplay(
                    r["QuestionText"].ToString(), r["SRAnswerType"].ToString(),
                    r["QuestionAnswerText"].ToString(), r["QuestionAnswerNum"].ToString(),
                    r["QuestionAnswerSuffix"].ToString(), (bool)r["IsAlwaysPrint"], r);


                // buang chk yang tidak dicentang dan alwayprint 0 atau null
                if (string.IsNullOrEmpty(r["TextToDisplay"].ToString()))
                {
                    r["TextToDisplay"] = "DELETE";
                }
            }

            // buang parent (label) yang gak ada childnya
            foreach (System.Data.DataRow r in dt.Rows)
            {
                if (r["SRAnswerType"].ToString() == "LBL")
                {
                    // buang parent (label) yang gak ada childnya
                    if (LblToDelete(r, dt)) r["TextToDisplay"] = "DELETE";
                }
                else
                {
                    // buang yang ini jika dia punya parent dan parentnya DELETE
                    if (IsParentDeleted(r, dt)) r["TextToDisplay"] = "DELETE";
                    // buang childnya jika combobox jawabannya tidak
                    if ((new List<string>() { "tidak", "no" }).Contains(
                        r["QuestionAnswerText"].ToString().Split('|')[0].ToLower().Trim()))
                    {
                        var chlds = dt.AsEnumerable().Where(x => x.Field<string>("ParentQuestionID") == r["QuestionID"].ToString());
                        foreach (var chld in chlds) chld["TextToDisplay"] = "DELETE";
                    }
                }
            }

            FormatTable1(dt);
            FormatTable2(dt);

            dt.AcceptChanges();

            return dt;
        }

        /// <summary>
        /// Formatting table GCS
        /// </summary>
        /// <param name="dt"></param>
        private static void FormatTable1(DataTable dt)
        {
            List<iToTbl> liTbl = new List<iToTbl>();
            // try table formatted
            iToTbl iGCSList = new iToTbl();
            iGCSList.Questions = new string[] { "NEU.GCS.BM", "NEU.GCS.RV", "NEU.GCS.RM" };
            iGCSList.Name = "GCS";
            liTbl.Add(iGCSList);
            // add more if any

            foreach (var iTbl in liTbl)
            {
                if (dt.AsEnumerable().Where(x => iTbl.Questions.Contains(x.Field<string>("QuestionID"))).Count() > 0)
                {
                    var gcsRow = dt.NewRow();
                    gcsRow["QuestionID"] = "TABLE" + iTbl.Name;
                    gcsRow["SRAnswerType"] = "TBL";
                    gcsRow["QuestionLevel"] = 1;
                    gcsRow["QuestionText"] = iTbl.Name;
                    gcsRow["TextToDisplay"] = "";
                    gcsRow["QuestionAnswerSelectionID"] = ""; // for header table
                    gcsRow["TblPrintAsGroup"] = "";

                    // table
                    var dttbl = new DataTable();
                    foreach (string gcs in iTbl.Questions)
                    {
                        dttbl.Columns.Add(gcs + "_score", typeof(string));
                        dttbl.Columns.Add(gcs, typeof(string));
                    }
                    // --- end of table

                    int rn = 0; int rnToInsert = 0;
                    foreach (System.Data.DataRow r in dt.Rows)
                    {
                        foreach (string qst in iTbl.Questions)
                        {
                            if (r["QuestionID"].ToString() != qst) continue;

                            var qaslC = new QuestionAnswerSelectionLineCollection();
                            qaslC.Query
                                .Where(qaslC.Query.QuestionAnswerSelectionID == r["QuestionAnswerSelectionID"].ToString())
                                .OrderBy(qaslC.Query.QuestionAnswerSelectionLineID.Ascending);
                            qaslC.LoadAll();
                            if (qaslC.Count > 0)
                            {
                                for (var a = 0; a < qaslC.Count; a++)
                                {
                                    if (a == dttbl.Rows.Count)
                                    {
                                        // bikin row baru
                                        var nr = dttbl.NewRow();
                                        for (var b = 0; b < dttbl.Columns.Count; b++) nr[b] = string.Empty;
                                        dttbl.Rows.Add();
                                    }
                                    dttbl.Rows[a][qst + "_score"] =
                                        (qaslC[a].QuestionAnswerSelectionLineID == r["QuestionAnswerSelectionLineID"].ToString()) ?
                                        ("(" + qaslC[a].QuestionAnswerSelectionLineID + ")") : qaslC[a].QuestionAnswerSelectionLineID;
                                    dttbl.Rows[a][qst] = qaslC[a].QuestionAnswerSelectionLineText;
                                }
                            }

                            gcsRow["QuestionAnswerSelectionID"] += (gcsRow["QuestionAnswerSelectionID"].ToString() == string.Empty ? "" : "|") + "Skor:30";
                            gcsRow["QuestionAnswerSelectionID"] += "|" + r["QuestionText"].ToString() + ":75";
                            gcsRow["QuestionGroupName"] = r["QuestionGroupName"].ToString();

                            r["TextToDisplay"] = "DELETE"; // hide the original one
                            rnToInsert = rn;
                        }
                        rn++;
                    }

                    gcsRow["TextToDisplay"] = ParsingDatatableToStrForReport(dttbl);
                    dt.Rows.InsertAt(gcsRow, rnToInsert);
                }
            }
            // --- end of try table formatted
        }

        /// <summary>
        /// Formatting table to be 
        /// No|Kriteria|Skor|Keterangan
        /// </summary>
        /// <param name="dt"></param>
        private static void FormatTable2(DataTable dt)
        {
            List<iToTbl> liTbl2 = new List<iToTbl>();
            // try table formatted
            iToTbl iNRTList = new iToTbl();
            iNRTList.Questions = new string[] { "DKB.SN.FS", "DKB.SN.IN", "DKB.SN.ME", "DKB.SN.MO", "DKB.SN.AC" };
            iNRTList.Name = "NRT";
            iNRTList.Header = "Kriteria:90|Skor:30|Keterangan:230";
            iNRTList.TblPrintAsGroup = "1";
            liTbl2.Add(iNRTList);
            // resiko jatuh MFS
            iToTbl iMFSList = new iToTbl();
            iMFSList.Questions = new string[] { "FLL.MFS.1", "FLL.MFS.2", "FLL.MFS.3", "FLL.MFS.4", "FLL.MFS.5", "FLL.MFS.6" };
            iMFSList.Name = "MFS";
            iMFSList.Header = "Kriteria:90|Skor:30|Keterangan:230";
            iMFSList.TblPrintAsGroup = "1";
            liTbl2.Add(iMFSList);
            // add more if any
            iToTbl iGPCList = new iToTbl();
            iGPCList.Questions = new string[] {
                "NEU.G.N1","NEU.G.N2","NEU.G.N3","NEU.G.N5","NEU.G.N7a",
                "NEU.G.N7b","NEU.G.N8","NEU.G.N9","NEU.G.N11","NEU.G.N12" };
            iGPCList.Name = "GPC";
            iGPCList.Header = "Kriteria:130|Skor:60|Keterangan:160";
            iGPCList.TblPrintAsGroup = "2";
            liTbl2.Add(iGPCList);

            // ambil dari appstandardreference
            var asrColl = new AppStandardReferenceItemCollection();
            var asrq = new AppStandardReferenceItemQuery();
            asrq.Where(asrq.StandardReferenceID == "QuestionTableFormatted", asrq.IsActive == true,
                asrq.Note != string.Empty);
            if (asrColl.Load(asrq))
            {
                foreach (var asr in asrColl)
                {
                    if (asr.Note.Trim() != string.Empty)
                    {
                        // try parse json
                        try
                        {
                            var jSon = JsonConvert.DeserializeObject<iToTbl>(asr.Note.Trim());
                            liTbl2.Add(jSon);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            foreach (var iTbl in liTbl2)
            {
                if (dt.AsEnumerable().Where(x => iTbl.Questions.Contains(x.Field<string>("QuestionID"))).Count() > 0)
                {
                    var nRow = dt.NewRow();
                    nRow["QuestionID"] = "TABLE" + iTbl.Name;
                    nRow["SRAnswerType"] = "TBL";
                    nRow["QuestionLevel"] = 1;
                    nRow["QuestionText"] = iTbl.Name;
                    nRow["TextToDisplay"] = "";
                    nRow["QuestionAnswerSelectionID"] = iTbl.Header; // for header table
                    nRow["TblPrintAsGroup"] = iTbl.TblPrintAsGroup; //(iTbl.Name == "GPC") ? "2" : "1"; // 2 for check style of table

                    // table
                    var dttbl = new DataTable();
                    dttbl.Columns.Add("kriteria", typeof(string));
                    dttbl.Columns.Add("skor", typeof(string));
                    dttbl.Columns.Add("keterangan", typeof(string));
                    // --- end of table

                    int rn = 0; int rnToInsert = 0;
                    foreach (System.Data.DataRow r in dt.Rows)
                    {
                        foreach (string qst in iTbl.Questions)
                        {
                            if (r["QuestionID"].ToString() != qst) continue;

                            var qaslC = new QuestionAnswerSelectionLineCollection();
                            qaslC.Query
                                .Where(qaslC.Query.QuestionAnswerSelectionID == r["QuestionAnswerSelectionID"].ToString())
                                .OrderBy(qaslC.Query.QuestionAnswerSelectionLineID.Ascending);
                            qaslC.LoadAll();
                            if (qaslC.Count > 0)
                            {
                                for (var a = 0; a < qaslC.Count; a++)
                                {
                                    // bikin row baru
                                    var nr = dttbl.NewRow();

                                    nr["kriteria"] = r["QuestionText"].ToString();
                                    nr["skor"] =
                                        (qaslC[a].QuestionAnswerSelectionLineID == r["QuestionAnswerSelectionLineID"].ToString()) ?
                                        (nRow["TblPrintAsGroup"] == "2" ? "." : ("(" + qaslC[a].QuestionAnswerSelectionLineID + ")")) :
                                        (nRow["TblPrintAsGroup"] == "2" ? "" : qaslC[a].QuestionAnswerSelectionLineID);
                                    nr["keterangan"] = qaslC[a].QuestionAnswerSelectionLineText;

                                    dttbl.Rows.Add(nr);
                                }
                            }

                            nRow["QuestionGroupName"] = r["QuestionGroupName"].ToString();

                            r["TextToDisplay"] = "DELETE"; // hide the original one
                            rnToInsert = rn;
                        }
                        rn++;
                    }

                    nRow["TextToDisplay"] = ParsingDatatableToStrForReport(dttbl);
                    dt.Rows.InsertAt(nRow, rnToInsert);
                }
                // --- end of try table formatted
            }
        }
        private static string ParsingDatatableToStrForReport(DataTable dt)
        {
            var ret = string.Empty;
            for (var a = 0; a < dt.Rows.Count; a++)
            {
                for (var b = 0; b < dt.Columns.Count; b++)
                {
                    ret += (ret == string.Empty ? "" : "|") + dt.Rows[a][b].ToString();
                }
            }
            return ret;
        }
        public static DataTable ParsingStrToDatatableForReport(string sstr, int columnCount)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < columnCount; i++)
            {
                dt.Columns.Add("c" + (i + 1).ToString(), typeof(string));
            }
            // parsing string to datatable content
            var strs = sstr.Split('|');
            var iCount = 0;
            foreach (var str in strs)
            {
                DataRow r;
                if (iCount % columnCount == 0)
                {
                    r = dt.NewRow();
                    dt.Rows.Add(r);
                }
                else
                {
                    r = dt.Rows[dt.Rows.Count - 1];
                }

                r[iCount % columnCount] = str;
                iCount++;
            }
            dt.AcceptChanges();

            return dt;
        }
        private static bool LblToDelete(System.Data.DataRow r, System.Data.DataTable dt)
        {
            // cek punya anak atau tidak
            var rows = dt.AsEnumerable()
                .Where(y => y.Field<string>("ParentQuestionID") == r["QuestionID"].ToString() &&
                y.Field<string>("QuestionID") != r["QuestionID"].ToString() /*&&
                y.Field<string>("TextToDisplay") != "DELETE"*/
                                                              );
            if (rows.Count() == 0) return false;


            var ret = false;
            foreach (var z in rows)
            {
                if (z["SRAnswerType"].ToString() == "LBL")
                {
                    ret = ret || LblToDelete(z, dt);
                }
                else if (z["TextToDisplay"].ToString() != "DELETE")
                {
                    return false;
                }
                else
                {
                    ret = ret || z["TextToDisplay"].ToString() == "DELETE";
                }
            }
            return ret;
        }

        private static bool IsParentDeleted(System.Data.DataRow r, System.Data.DataTable dt)
        {
            // cek punya parent atau tidak
            if (r["ParentQuestionID"].ToString() == string.Empty) return false;

            var rows = dt.AsEnumerable()
                .Where(y => y.Field<string>("QuestionID") == r["ParentQuestionID"].ToString() &&
                y.Field<string>("QuestionID") != r["QuestionID"].ToString() /*&&
                y.Field<string>("TextToDisplay") != "DELETE"*/
                                                              );
            if (rows.Count() == 0) return false;


            var ret = false;
            foreach (var z in rows)
            {
                if (z["SRAnswerType"].ToString() != "LBL")
                {
                    ret = z["TextToDisplay"].ToString() == "DELETE";
                }
            }
            return ret;
        }
        #endregion

        #endregion
    }

    public class NutritionCareDiagnosaTransDTNocTarget : NutritionCareDiagnoseTransDT
    {
        public long IdDiagTmp;
    }

    public partial class NutritionCareDiagnoseTransDTCollection
    {
        public override void Save()
        {
            foreach (var entity in this)
            {
                if (entity.es.IsAdded)
                {
                    if (string.IsNullOrEmpty(entity.CreateByUserID))
                    {
                        var userLogin = new UserLogin();
                        if (HttpContext.Current.Session["_UserLogin"] != null)
                        {
                            userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                            entity.CreateByUserID = userLogin.UserID;
                        }
                    }
                }
            }

            base.Save();
        }

        public override void MarkAllAsDeleted()
        {
            foreach (var i in this)
            {
                i.MarkAsDeleted();
            }
        }

        public bool LoadImplementationByPage(string TransactionNo, int iPageNumber, int iPageSize)
        {
            var q = new NutritionCareDiagnoseTransDTQuery("a");
            var au = new AppUserQuery("b");
            q.InnerJoin(au).On(q.LastUpdateByUserID.Equal(au.UserID))
                .Where(q.TransactionNo.Equal(TransactionNo),
                q.SRNutritionCareTerminologyLevel.In(new string[] { "31", "32" }))
                .Select(q, au.UserName.As("refTo_UserName"));
            q.es.PageNumber = iPageNumber;
            q.es.PageSize = iPageSize;
            q.OrderBy(q.CreateDateTime.Descending);

            return this.Load(q);
        }

        public DataTable ImplementationCountPerNIC(string TransactionNo)
        {
            var str = @"SELECT a.ID, COUNT(b.ID) ImpCount
                        FROM NutritionCareDiagnoseTransDT a
	                        INNER JOIN NutritionCareDiagnoseTransDT b ON b.ParentID = a.ID
                        WHERE a.TransactionNo = '" + TransactionNo + "' AND a.SRNutritionCareTerminologyLevel = '30' GROUP BY a.ID";
            return FillDataTable(esQueryType.Text, str);
        }

        public DataTable ImplementationByPage(string TransactionNo, bool HasRespondOnly, int iRowStart, int iRowFinish)
        {
            var str = @"SELECT x.*, '' Respond2, au.UserName RefToUserName FROM (
	                        SELECT ROW_NUMBER() OVER (ORDER BY ExecuteDateTime DESC) rn, * 
	                        FROM NutritionCareDiagnoseTransDT AS ndtd 
	                        WHERE ndtd.TransactionNo = '" + TransactionNo + @"'
		                        AND ndtd.SRNutritionCareTerminologyLevel IN ('31','32') " +
                                (HasRespondOnly ? " AND (ISNULL(ndtd.ReferenceToPhrNo, '') <> '') " : "") + @"
                        ) x LEFT JOIN AppUser AS au ON x.LastUpdateByUserID = au.UserID
                        WHERE x.rn BETWEEN " + iRowStart + " AND " + iRowFinish;
            return FillDataTable(esQueryType.Text, str);
        }

        public int ImplementationCount(string TransactionNo, bool HasRespondOnly)
        {
            var str = @"SELECT COUNT(ID) 
	                        FROM NutritionCareDiagnoseTransDT AS ndtd 
	                        WHERE ndtd.TransactionNo = '" + TransactionNo + "' and SRNutritionCareTerminologyLevel IN ('31','32')" +
                                (HasRespondOnly ? " AND (ISNULL(ndtd.ReferenceToPhrNo, '') <> '') " : "");
            DataTable dt = FillDataTable(esQueryType.Text, str);
            return (int)dt.Rows[0][0];
        }

        public DataTable ImplementationCountPerIntervention(string TransactionNo)
        {
            var str = @"SELECT l30.TerminologyID, COUNT(l31.ID) cID31
                        FROM (
	                        select ID, TerminologyID from NutritionCareDiagnoseTransDT
	                        WHERE TransactionNo = '" + TransactionNo + @"'
		                        AND SRNutritionCareTerminologyLevel = '30'
	                        UNION ALL
	                        SELECT 0, '' TerminologyID
                        ) AS l30
                        LEFT JOIN (
	                        SELECT l31_1.ID, ISNULL(l31_1.ParentID, 0) ParentID
	                        FROM NutritionCareDiagnoseTransDT AS l31_1
	                        WHERE l31_1.TransactionNo = '" + TransactionNo + @"'
		                        AND l31_1.SRNutritionCareTerminologyLevel = '31'
                        ) AS l31 ON l30.ID = l31.ParentID 

                        GROUP BY l30.TerminologyID";
            return FillDataTable(esQueryType.Text, str);
        }
    }
}
