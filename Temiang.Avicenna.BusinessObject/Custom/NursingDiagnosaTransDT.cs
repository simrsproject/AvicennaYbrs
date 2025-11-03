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
    public partial class NursingDiagnosaTransDT
    {
        private string _TmpIdEvaluation = string.Empty;
        public string TmpIdEvaluation
        {
            get { return _TmpIdEvaluation; }
            set { _TmpIdEvaluation = value; }
        }
        public string RefToNursingDiagnosaName
        {
            get { return GetColumn("refTo_NursingDiagnosaName").ToString(); }
            set { SetColumn("refTo_NursingDiagnosaName", value); }
        }
        public string RefToUserName
        {
            get { return GetColumn("refTo_UserName").ToString(); }
            set { SetColumn("refTo_UserName", value); }
        }
        public string RefToSRNsEtiologyType
        {
            get { return GetColumn("refTo_SRNsEtiologyType").ToString(); }
            set { SetColumn("refTo_SRNsEtiologyType", value); }
        }
        public string RefToF1
        {
            get { return GetColumn("refTo_F1").ToString(); }
            set { SetColumn("refTo_F1", value); }
        }
        public string RefToF2
        {
            get { return GetColumn("refTo_F2").ToString(); }
            set { SetColumn("refTo_F2", value); }
        }
        public string refToSRNursingNocType
        {
            get { return GetColumn("refTo_SRNursingNocType").ToString(); }
            set { SetColumn("refTo_SRNursingNocType", value); }
        }
        public string refToSRNursingNicType
        {
            get { return GetColumn("refTo_SRNursingNicType").ToString(); }
            set { SetColumn("refTo_SRNursingNicType", value); }
        }
        public string refToEvalSRNursingCarePlanning
        {
            get { return GetColumn("refToEval_SRNursingCarePlanning").ToString(); }
            set { SetColumn("refToEval_SRNursingCarePlanning", value); }
        }
        
        //public static string GetTmpNursingDiagnosaID(NursingDiagnosaTransDTCollection c)
        //{
        //    var TmpNursingDiagnosaID = (from x in c select x.TmpNursingDiagnosaID).Max();
        //    int tid = (string.IsNullOrEmpty(TmpNursingDiagnosaID) ? 0 : System.Convert.ToInt32(TmpNursingDiagnosaID));
        //    tid++;
        //    return TmpNursingDiagnosaID = tid.ToString().PadLeft(3, '0');
        //}

        public override void Save()
        {
            if (this.es.IsAdded)
            {
                str.SRUserType = string.Empty;

                if (string.IsNullOrEmpty(CreateByUserID))
                {
                    var userLogin = new UserLogin();
                    if (HttpContext.Current.Session["_UserLogin"] != null)
                    {
                        userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                        SRUserType = userLogin.SRUserType;
                        CreateByUserID = userLogin.UserID;
                    }
                }
                else
                {
                    var user = new AppUser();
                    user.LoadByPrimaryKey(CreateByUserID);
                    SRUserType = user.SRUserType;

                }
            }
            base.Save();
        }

        public override void MarkAsDeleted()
        {
            switch (this.SRNursingDiagnosaLevel)
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

        public static string GetTmpNursingDiagnosaID(NursingDiagnosaTransDTCollection c, string TransactionNo)
        {
            var q = new NursingDiagnosaTransDTQuery("a");
            q.Where(q.TransactionNo == TransactionNo && q.TmpNursingDiagnosaID != string.Empty)
                .Select(q.TmpNursingDiagnosaID);
            var dt = q.LoadDataTable();
            var tmpID = dt.AsEnumerable().Select(x =>
                System.Convert.ToInt32(x.Field<string>("TmpNursingDiagnosaID"))).ToList(); ;
            var tmp = (from x in c
                       where x.TmpNursingDiagnosaID.Trim().Length > 0
                       select System.Convert.ToInt32(x.TmpNursingDiagnosaID));
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

        public static string GetFullImplementationNameFormatted(string nursingDiagnosaName,
            string S, string B, string A, string R)
        {

            if (Equals(nursingDiagnosaName, "S B A R") || Equals(nursingDiagnosaName, "SBAR"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "B: " + B + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "R: " + R);
            }
            if (Equals(nursingDiagnosaName, "S O A P") || Equals(nursingDiagnosaName, "SOAP"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + B + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + R);
            }
            if (Equals(nursingDiagnosaName, "Handover Patient") || Equals(nursingDiagnosaName, "Handover Patient"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + B + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + R);
            }
            if (Equals(nursingDiagnosaName, "A D I M E") || Equals(nursingDiagnosaName, "ADIME"))
            {
                return (
                    "A: " + S + System.Environment.NewLine +
                    "D: " + B + System.Environment.NewLine +
                    "I: " + A + System.Environment.NewLine +
                    "ME: " + R);
            }
            if (Equals(nursingDiagnosaName, "REF") )
            {
                return (
                    "Konsul ke : " + S + System.Environment.NewLine +
                    "Catatan : " + B + System.Environment.NewLine +
                    "Pemeriksaan : " + A + System.Environment.NewLine +
                    "Jawaban : " + R);
            }
            else
            {
                return nursingDiagnosaName;
            }
        }

        public string GetFullImplementationName
        {
            get
            {
                return GetFullImplementationNameFormatted(NursingDiagnosaName, S, O, A, P);
            }
        }

        //public static string[] GetServiceUnitIDByIdDiagnosaIDL10(long IdL10)
        //{
        //    var nL10 = new NursingDiagnosaTransDT();
        //    nL10.LoadByPrimaryKey(IdL10);
        //    return GetServiceUnitIDByNursingTransNo(nL10.TransactionNo);
        //}
        //public static string[] GetServiceUnitIDByNursingTransNo(string TransactionNo)
        //{
        //    var qhd = new NursingTransHDQuery("qhd");
        //    var qahd = new NursingAssessmentTransHDQuery("qahd");
        //    var qphr = new PatientHealthRecordQuery("qphr");
        //    //var qsu = new QuestionFormInServiceUnitQuery("qsu");

        //    qhd.InnerJoin(qahd).On(qhd.TransactionNo == qahd.TransactionNo && qhd.TransactionNo == TransactionNo)
        //        .InnerJoin(qphr).On(qahd.QuestionFormReference == qphr.TransactionNo)
        //        //.InnerJoin(qsu).On(qphr.QuestionFormID == qsu.QuestionFormID)
        //        .Select(qphr.ServiceUnitID);
        //    qhd.es.Distinct = true;
        //    var dt = qhd.LoadDataTable();

        //    var s = from d in dt.AsEnumerable() select d.Field<string>(0);

        //    List<string> sret = new List<string>();
        //    if (s.Count() > 0)
        //    {
        //        var sun = new NursingDiagnosaServiceUnitQuery("a");
        //        sun.Where(sun.ServiceUnitID.In(s))
        //            .Select(sun.ServiceUnitID);
        //        sun.es.Distinct = true;

        //        var sDt = sun.LoadDataTable();

        //        sret = (from ss in sDt.AsEnumerable() select ss.Field<string>(0)).ToList();

        //        if (sret.Count == 0) sret.Add("UMUM"); /*default ambil dari umum*/

        //        return sret.ToArray();
        //    }
        //    return s.ToArray();
        //}

        #region DIAGNOSA
        #region NURSING PROBLEM (11)
        public static DataTable NursingProblem(string TransactionNo, string DiagnosaLevel, string NursingDiagnosaParentID)
        {
            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery pr = new NursingDiagnosaQuery("b");

            NursingDiagnosaTransDTQuery diag = new NursingDiagnosaTransDTQuery("diag");
            NursingDiagnosaTransDTQuery dt = new NursingDiagnosaTransDTQuery("e");
            AppStandardReferenceItemQuery nictype = new AppStandardReferenceItemQuery("f");

            query.es.Distinct = true;

            query.InnerJoin(pr).On(query.NursingDiagnosaParentID == pr.NursingDiagnosaID);
            query.InnerJoin(diag).On(pr.NursingDiagnosaID == diag.NursingDiagnosaID);
            query.LeftJoin(dt).On(query.NursingDiagnosaID == dt.NursingDiagnosaID
                & dt.TransactionNo == TransactionNo);

            query.LeftJoin(nictype).On(query.SRNursingNicType == nictype.ItemID & nictype.StandardReferenceID == "NursingNicType");

            query.Where(
                query.SRNursingDiagnosaLevel == DiagnosaLevel);
            if (NursingDiagnosaParentID != string.Empty)
            {
                query.Where(query.NursingDiagnosaParentID == NursingDiagnosaParentID);
            }

            if (DiagnosaLevel == "30")
            {
                var ndsu = new NursingDiagnosaNsTypeQuery("ndsu");
                query.InnerJoin(ndsu).On(query.NursingDiagnosaID == ndsu.NursingDiagnosaID &
                    ndsu.SRNsType == diag.SRNsType);
            }

            query.Select(
                query,
                "<ISNULL(e.NursingDiagnosaName, a.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                pr.SequenceNo.As("ParentSequenceNo"),
                pr.NursingDiagnosaName.As("NursingParentName"),
                dt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                dt.Priority,
                dt.EvalPeriod,
                dt.PeriodConversionInHour,
                dt.Skala,
                dt.Target,
                dt.Evaluasi,
                dt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                query.SRNursingNicType,
                nictype.ItemName.As("SRNursingNicTypeName")
                );

            var dttbl = query.LoadDataTable();
            return dttbl;
        }

        /// <summary>
        /// fungsi mengambil data nusing problem yang bisa diangkat menjadi diagnosa baru
        /// </summary>
        /// <param name="RegistrationNo"></param>
        /// <returns></returns>
        public static DataTable NursingProblemAvailable(string TransactionNo, string SRNsType)
        {
            // diagnosa yang bisa diangkat yaitu diagnosa yang tidak sedang aktif

            NursingDiagnosaQuery np = new NursingDiagnosaQuery("np");
            NursingDiagnosaQuery nd = new NursingDiagnosaQuery("nd");
            NursingAssessmentDiagnosaQuery nad = new NursingAssessmentDiagnosaQuery("nad");
            NursingAssessmentTransDTQuery natdt = new NursingAssessmentTransDTQuery("natdt");
            NursingAssessmentTransHDQuery nathd = new NursingAssessmentTransHDQuery("nathd");

            NursingDiagnosaTransDTQuery ndtdt = new NursingDiagnosaTransDTQuery("ndtdt");
            NursingDiagnosaTransDTQuery nd10 = new NursingDiagnosaTransDTQuery("nd10");
            AppStandardReferenceItemQuery nictype = new AppStandardReferenceItemQuery("nictype");
            AppStandardReferenceItemQuery etioType = new AppStandardReferenceItemQuery("etioType");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.NursingDiagnosaParentID == nd.NursingDiagnosaID)
                .InnerJoin(nad).On(nd.NursingDiagnosaID == nad.NursingDiagnosaID)
                .InnerJoin(natdt).On(nad.QuestionID == natdt.QuestionID)
                .InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                .LeftJoin(ndtdt).On(np.NursingDiagnosaID == ndtdt.NursingDiagnosaID
                & ndtdt.TransactionNo == TransactionNo && ndtdt.SRNursingCarePlanning.Coalesce("''") != "01")
                .LeftJoin(nd10).On(nd.NursingDiagnosaID == nd10.NursingDiagnosaID && nd10.TransactionNo == TransactionNo &&
                    nd10.SRNursingCarePlanning.Coalesce("''") != "01")
                .LeftJoin(nictype).On(np.SRNursingNicType == nictype.ItemID & nictype.StandardReferenceID == "NursingNicType")
                .LeftJoin(etioType).On(np.SRNsEtiologyType == etioType.ItemID & etioType.StandardReferenceID == "NsEtiologyType")
                .Where(
                    np.SRNursingDiagnosaLevel == "11",
                    nathd.TransactionNo == TransactionNo,
                    nd10.NursingDiagnosaName.IsNull());

            var ndType = new NursingDiagnosaNsTypeQuery("ndtype");
            np.InnerJoin(ndType).On(np.NursingDiagnosaID == ndType.NursingDiagnosaID &
                ndType.SRNsType == SRNsType);

            np.Select(
                np,
                "<ISNULL(ndtdt.NursingDiagnosaName, np.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.NursingDiagnosaName.As("NursingParentName"),
                ndtdt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                ndtdt.Priority,
                ndtdt.EvalPeriod,
                ndtdt.PeriodConversionInHour,
                ndtdt.Skala,
                ndtdt.Target,
                ndtdt.Evaluasi,
                ndtdt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                ndtdt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                np.SRNursingNicType,
                nictype.ItemName.As("SRNursingNicTypeName"),
                nd.NursingDiagnosaID.As("NursingDiagnosaIDParent"),
                "<0 AssCount>",
                "<'' FTS>",
                etioType.ItemName.As("SREtiologyTypeName")
                );

            var dttbl = np.LoadDataTable();

            // ===============
            // diagnosa yang bisa diangkat yaitu diagnosa yang tidak sedang aktif

            np = new NursingDiagnosaQuery("np");
            var q = new QuestionQuery("q");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.NursingDiagnosaParentID == nd.NursingDiagnosaID)
                .InnerJoin(nad).On(nd.NursingDiagnosaID == nad.NursingDiagnosaID)
                .InnerJoin(q).On(nad.QuestionID == q.EquivalentQuestionID)
                .InnerJoin(natdt).On(q.QuestionID == natdt.QuestionID)
                .InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                .LeftJoin(ndtdt).On(np.NursingDiagnosaID == ndtdt.NursingDiagnosaID
                & ndtdt.TransactionNo == TransactionNo && ndtdt.SRNursingCarePlanning.Coalesce("''") != "01")
                .LeftJoin(nd10).On(nd.NursingDiagnosaID == nd10.NursingDiagnosaID && nd10.TransactionNo == TransactionNo &&
                    nd10.SRNursingCarePlanning.Coalesce("''") != "01")
                .LeftJoin(nictype).On(np.SRNursingNicType == nictype.ItemID & nictype.StandardReferenceID == "NursingNicType")
                .LeftJoin(etioType).On(np.SRNsEtiologyType == etioType.ItemID & etioType.StandardReferenceID == "NsEtiologyType")
                .Where(
                    np.SRNursingDiagnosaLevel == "11",
                    nathd.TransactionNo == TransactionNo,
                    nd10.NursingDiagnosaName.IsNull());

            np.InnerJoin(ndType).On(np.NursingDiagnosaID == ndType.NursingDiagnosaID &
                ndType.SRNsType == SRNsType);

            np.Select(
                np,
                "<ISNULL(ndtdt.NursingDiagnosaName, np.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.NursingDiagnosaName.As("NursingParentName"),
                ndtdt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                ndtdt.Priority,
                ndtdt.EvalPeriod,
                ndtdt.PeriodConversionInHour,
                ndtdt.Skala,
                ndtdt.Target,
                ndtdt.Evaluasi,
                ndtdt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                ndtdt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                np.SRNursingNicType,
                nictype.ItemName.As("SRNursingNicTypeName"),
                nd.NursingDiagnosaID.As("NursingDiagnosaIDParent"),
                "<0 AssCount>",
                "<'' FTS>",
                etioType.ItemName.As("SREtiologyTypeName")
                );

            var dttblE = np.LoadDataTable();

            dttbl.Merge(dttblE);

            if (dttbl.Rows.Count > 0) {
                dttbl = dttbl.AsEnumerable()
                    .GroupBy(x => x.Field<string>("NursingDiagnosaID"))
                    .Select(y => y.First())
                    .CopyToDataTable();
            }
            // ===============

            var result = FilterDiagnosaByAssessmentValue(TransactionNo, dttbl);

            foreach (System.Data.DataRow d in result.Rows)
            {
                d["FTS"] = d["AssCount"].ToString().PadLeft(2, '0') + d["NursingDiagnosaIDParent"].ToString();
            }

            //dttbl.AcceptChanges();

            return result.Rows.Count == 0 ? result : result.AsEnumerable().OrderBy(x => x.Field<string>("SREtiologyTypeName")).CopyToDataTable();
            //return dttbl;
        }
        public static DataTable NursingProblemForImplementation(string TransactionNo, string DS, string DO,
            string[] NursingDiagnosaIDExceptionsL10, string SRNsType, bool ShowAll)
        {
            NursingDiagnosaQuery np = new NursingDiagnosaQuery("np");
            NursingDiagnosaQuery nd = new NursingDiagnosaQuery("nd");
            AppStandardReferenceItemQuery etioType = new AppStandardReferenceItemQuery("etioType");

            np.es.Distinct = true;

            np.InnerJoin(nd).On(np.NursingDiagnosaParentID == nd.NursingDiagnosaID)
                .LeftJoin(etioType).On(np.SRNsEtiologyType == etioType.ItemID & etioType.StandardReferenceID == "NsEtiologyType")
                .Where(np.SRNursingDiagnosaLevel == "11");

            var ndsu = new NursingDiagnosaNsTypeQuery("ndsu");
            np.InnerJoin(ndsu).On(np.NursingDiagnosaID == ndsu.NursingDiagnosaID &
                ndsu.SRNsType == SRNsType);

            if (NursingDiagnosaIDExceptionsL10.Length > 0)
            {
                np.Where(nd.NursingDiagnosaID.NotIn(NursingDiagnosaIDExceptionsL10));
            }

            np.Select(
                np,
                "<np.NursingDiagnosaName NursingDiagnosaNameEdited>",
                nd.SequenceNo.As("ParentSequenceNo"),
                nd.NursingDiagnosaName.As("NursingParentName"),
                nd.NursingDiagnosaID.As("NursingDiagnosaIDParent"),
                nd.NursingDiagnosaCode.As("NursingParentDiagnosaCode"),
                nd.F1.As("NursingParentF1"),
                "<0 AssCount>",
                "<'' FTS>",
                etioType.ItemName.As("SREtiologyTypeName")
                );

            np.OrderBy(nd.NursingDiagnosaCode.Ascending, nd.NursingDiagnosaName.Ascending);

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
                d["FTS"] = d["AssCount"].ToString().PadLeft(2, '0') + d["NursingDiagnosaIDParent"].ToString();
                d["NursingParentName"] = string.Format("<span style='font-size:16px;' title='{1}'>{2}{0}</span>", d["NursingParentName"].ToString(), d["NursingParentF1"].ToString(),
                    AppParameter.IsYes(AppParameter.ParameterItem.NsIsShowDiagnosaCode) ? string.Format("[{0}] ", d["NursingParentDiagnosaCode"].ToString()) : "");
            }

            dttbl.AcceptChanges();

            return result;
            //return dttbl;
        }

        private static DataTable FilterDiagnosaByAssessmentValue(
            string TransactionNo, DataTable dttbl)
        {
            var nad = new NursingAssessmentDiagnosaQuery("nad");
            var q = new QuestionQuery("q");
            var nadt = new NursingAssessmentTransDTQuery("nadt");
            var nahd = new NursingAssessmentTransHDQuery("nahd");
            var nth = new NursingTransHDQuery("nth");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");

            nad.InnerJoin(q).On(nad.QuestionID == q.QuestionID)
                .InnerJoin(nadt).On(q.QuestionID == nadt.QuestionID)
                .InnerJoin(nahd).On(nadt.Hdid == nahd.Id)
                .InnerJoin(nth).On(nahd.TransactionNo == nth.TransactionNo)
                .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .Where(
                    nahd.TransactionNo == TransactionNo, 
                    q.EquivalentQuestionID.Coalesce("''") == string.Empty /*direct mapping*/);

            nad.Select(
                nad.QuestionID,
                nad.QuestionID.As("EquivalentQuestionID"),
                nad.NursingDiagnosaID,
                nad.Operand,
                nad.AcceptedText,
                nad.AcceptedNum,
                nad.AcceptedNum2,
                nad.CheckValue,
                nad.IsUsingRange,
                nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                q.NursingDisplayAs.Coalesce("q.QuestionText").As("QuestionText"),
                q.AnswerDecimalDigit,
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
                nad.AgeInMonthEnd,
                pat.Sex.As("PatientSex"),
                nad.Sex,
                nad.ShowAssessmetAsPrefix,
                nad.ShowAssessmetAsSuffix,
                nad.SRNsDiagnosaPrefix,
                nad.SRNsDiagnosaSuffix,
                nahd.AssessmentDateTime,
                nad.ID
            );
            var dt = nad.LoadDataTable();

            nad = new NursingAssessmentDiagnosaQuery("nad");
            var qEqiv = new QuestionQuery("qEqiv");

            nad.InnerJoin(qEqiv).On(nad.QuestionID == qEqiv.QuestionID) /*equivalent mapping*/
                .InnerJoin(q).On(qEqiv.QuestionID == q.EquivalentQuestionID)
                .InnerJoin(nadt).On(q.QuestionID == nadt.QuestionID)
                .InnerJoin(nahd).On(nadt.Hdid == nahd.Id)
                .InnerJoin(nth).On(nahd.TransactionNo == nth.TransactionNo)
                .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .Where(
                    nahd.TransactionNo == TransactionNo);

            nad.Select(
                q.QuestionID,
                qEqiv.QuestionID.As("EquivalentQuestionID"),
                nad.NursingDiagnosaID,
                nad.Operand,
                nad.AcceptedText,
                nad.AcceptedNum,
                nad.AcceptedNum2,
                nad.CheckValue,
                nad.IsUsingRange,
                qEqiv.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                q.NursingDisplayAs.Coalesce("q.QuestionText").As("QuestionText"),
                q.AnswerDecimalDigit,
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
                nad.AgeInMonthEnd,
                pat.Sex.As("PatientSex"),
                nad.Sex,
                nad.ShowAssessmetAsPrefix,
                nad.ShowAssessmetAsSuffix,
                nad.SRNsDiagnosaPrefix,
                nad.SRNsDiagnosaSuffix,
                nahd.AssessmentDateTime,
                nad.ID
            );
            var dtE = nad.LoadDataTable();

            dt.Merge(dtE);

            if (dt.Rows.Count > 0)
            {
                // ambil nilai terakhir saja dari setiap assessment
                var rows = dt.AsEnumerable()
                    .Where(r => r.Field<DateTime>("AssessmentDateTime") <= DateTime.Now)
                    .GroupBy(r => new
                    {
                        QuestionID = r.Field<string>("QuestionID"),
                        NursingDiagnosaID = r.Field<string>("NursingDiagnosaID"),
                        ID = r.Field<int>("ID")
                    })
                    //.OrderByDescending(o => o.Field<DateTime>("AssessmentDateTime"))
                    //.Select(g => g.First())
                    .Select(g => g.OrderByDescending(r => r.Field<DateTime>("AssessmentDateTime")).First());
                //.CopyToDataTable();
                dt = rows.Any() ? rows.CopyToDataTable() : dt.Clone();
            }

            return DoFilter(TransactionNo, dttbl, dt);
        }
        private static DataTable FilterDiagnosaByDSDOText(
            string TransactionNo, DataTable dttbl, string DS, string DO)
        {
            // ambil id assessment dari app
            List<string> IdAssessment = new List<string>();
            var AppPrmDS = new AppParameter();
            if (AppPrmDS.LoadByPrimaryKey("NursingAssessmentDS"))
            {
                IdAssessment.Add(AppPrmDS.ParameterValue);
            }
            var AppPrmDO = new AppParameter();
            if (AppPrmDO.LoadByPrimaryKey("NursingAssessmentDO"))
            {
                IdAssessment.Add(AppPrmDO.ParameterValue);
            }

            var nad = new NursingAssessmentDiagnosaQuery();
            nad.Where(nad.QuestionID.In(IdAssessment));
            nad.Select(
                nad.QuestionID,
                nad.QuestionID.As("EquivalentQuestionID"),
                nad.NursingDiagnosaID,
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
                nad.AgeInMonthEnd,
                "<'' PatientSex>",
                nad.Sex,
                nad.ShowAssessmetAsPrefix,
                nad.ShowAssessmetAsSuffix,
                nad.SRNsDiagnosaPrefix,
                nad.SRNsDiagnosaSuffix,
                "<getdate() as AssessmentDateTime>"
            );
            var dt = nad.LoadDataTable();

            int PatAgeInMonth = 0;
            string Sex = "";
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

                        var pat = new Patient();
                        if (pat.LoadByPrimaryKey(reg.PatientID)) {
                            Sex = pat.Sex;
                        }
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
                d["PatientSex"] = Sex;
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
                    DateTime DateAssessment = (DateTime)x["AssessmentDateTime"]; 

                    string AnswerText = x["AnswerText"].ToString();
                    decimal ansNum = System.Convert.ToDecimal(x["AnswerNum"]);

                    x["Selected"] = IsAccepted(DateAssessment, SRAnswerTypeFromMatrix, SRAnswerTypeFromQuestion,
                        AcceptedText, Operand, AnswerText, AcceptedNum,
                        ansNum, AcceptedBool, x["QuestionText"].ToString(),
                        System.Convert.ToInt16(x["AnswerDecimalDigit"] is DBNull ? 0: x["AnswerDecimalDigit"]), 
                        x["AnswerSuffix"].ToString(),
                        (bool)(x["IsUsingRange"] ?? false),
                        (decimal)(x["AcceptedNum2"] ?? 0),
                        System.Convert.ToInt32(x["PatientAgeInMonth"]),
                        System.Convert.ToInt32(x["AgeInMonthStart"]),
                        System.Convert.ToInt32(x["AgeInMonthEnd"]),
                        x["PatientSex"].ToString(), x["Sex"].ToString());
                }
            }

            /*
             * perlu kajian mendalam apakah replacement ini masih cocok atau tidak
             * ataukah sudah tercover oleh mapping dengan batasan usia??
             * karena potensi bentrok dengan assessment SRNsMandatoryLevel untuk level 01
             */
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


            //var acceptedDiag = (from d in dt.AsEnumerable()
            //                    where d.Field<bool>("Selected") == true
            //                    select /*d.Field<string>("QuestionID") + */d.Field<string>("NursingDiagnosaID")).ToArray();
            var acceptedDiag = (from d in DSDO.AsEnumerable()
                                where d.Field<bool>("Selected") == true
                                select /*d.Field<string>("QuestionID") + */d.Field<string>("NursingDiagnosaID")).ToArray();

            foreach (System.Data.DataRow r in dttbl.Rows)
            {
                r["AssCount"] = (from d in DSDO.AsEnumerable()
                                 where d.Field<bool>("Selected") == true
                                     && d.Field<string>("NursingDiagnosaID") == r["NursingDiagnosaIDParent"].ToString()
                                 select d.Field<string>("QuestionID")).Count();
            }

            dttbl.AcceptChanges();

            // filter assessment wajib --> level mandatory
            var AMColl = new NursingAssessmentDiagnosaCollection();
            AMColl.Query.Where(AMColl.Query.SRNsMandatoryLevel == "01");
            AMColl.LoadAll();
            foreach (var ad in acceptedDiag) {
                var ams = AMColl.Where(x => x.NursingDiagnosaID == ad);
                if (ams.Count() > 0) {
                    var dsdoAccepted = DSDO.AsEnumerable().Where(x => x.Field<bool>("Selected") == true)
                        .Select(x => new
                        {
                            DiagnosaID = x.Field<string>("NursingDiagnosaID"),
                            EquivalentQuestionID = x.Field<string>("EquivalentQuestionID")
                        });

                    if (ams.Where(x => !dsdoAccepted.Contains(
                         new
                         {
                             DiagnosaID = x.NursingDiagnosaID,
                             EquivalentQuestionID = x.QuestionID
                         })).Any()) {
                        // tidak memenuhi syarat mandatory
                        var rowToReject = dttbl.AsEnumerable()
                            .Where(x => x.Field<string>("NursingDiagnosaIDParent") == ad);
                        foreach (var rtj in rowToReject) {
                            rtj["AssCount"] = 0; // set rejected
                        }
                    }
                }
            }

            dttbl.AcceptChanges();
            // end of filter assessment wajib


            var row = dttbl.AsEnumerable().Where(
                x => acceptedDiag.Contains(x.Field<string>("NursingDiagnosaIDParent")) &&
                x.Field<int>("AssCount") > 0
            ).OrderByDescending(x => x.Field<int>("AssCount"));

            return row.AsDataView().ToTable();
        }
        #endregion
        #region NURSING DIAGNOSA (10)
        public static string DeleteByIdL10(long id)
        {
            // yang didelete adalah L10 dan L11
            // jika sudah ada L20, L30, L31, L40 jangan dihapus
            var d = new NursingDiagnosaTransDT();
            if (d.LoadByPrimaryKey(id))
            {
                // ambil childnya
                var dc = new NursingDiagnosaTransDTCollection();
                dc.Query.Where(dc.Query.ParentID == d.ID);
                dc.LoadAll();
                if (dc.Where(x => x.SRNursingDiagnosaLevel != "11").Count() == 0)
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
        public static NursingDiagnosaTransDTCollection NursingDiagnosaHasParentID(string TransactionNo, string Level)
        {
            var coll = new NursingDiagnosaTransDTCollection();
            coll.Query.Where(coll.Query.TransactionNo == TransactionNo,
                coll.Query.SRNursingDiagnosaLevel == Level);
            coll.Query.Where(coll.Query.ParentID > 0);
            coll.LoadAll();
            return coll;
        }
        //public static NursingDiagnosaTransDTCollection NursingDiagnosa(string TransactionNo, string Level, long ParentID)
        //{
        //    return NursingDiagnosa(TransactionNo, Level, (ParentID == 0) ? (new long[] {}) : (new long[] { ParentID }));
        //}
        //public static NursingDiagnosaTransDTCollection NursingDiagnosa(string TransactionNo, string Level, long[] ParentIDs)
        //{
        //    var coll = new NursingDiagnosaTransDTCollection();
        //    coll.Query.Where(coll.Query.TransactionNo == TransactionNo,
        //        coll.Query.SRNursingDiagnosaLevel == Level);
        //    if (ParentIDs.Length > 0) coll.Query.Where(coll.Query.ParentID.In(ParentIDs));
        //    coll.LoadAll();
        //    return coll;
        //}
        public static NursingDiagnosaTransDTCollection NursingDiagnosa(long ParentID, string Level)
        {
            return NursingDiagnosa((new long[] { ParentID }), Level);
        }
        public static NursingDiagnosaTransDTCollection NursingDiagnosa(long[] ParentIDs, string Level)
        {
            return NursingDiagnosa(ParentIDs, new string[] { Level });
        }
        public static NursingDiagnosaTransDTCollection NursingDiagnosa(long[] ParentIDs, string[] Levels)
        {
            var coll = new NursingDiagnosaTransDTCollection();
            var ndt = new NursingDiagnosaTransDTQuery("ndt");
            var nd = new NursingDiagnosaQuery("nd");
            var noct = new AppStandardReferenceItemQuery("noct");
            var nict = new AppStandardReferenceItemQuery("nict");
            ndt.LeftJoin(nd).On(ndt.NursingDiagnosaID == nd.NursingDiagnosaID)
                .LeftJoin(noct).On(noct.StandardReferenceID == "NursingNocType" && nd.SRNursingNocType == noct.ItemID)
                .LeftJoin(nict).On(nict.StandardReferenceID == "NursingNicType" && nd.SRNursingNicType == nict.ItemID)
                .Where(
                    ndt.ParentID.In(ParentIDs), 
                    ndt.SRNursingDiagnosaLevel.In(Levels))
                .Select(
                    ndt, nd.F1.Coalesce("''").As("refTo_F1"), 
                    nd.F2.Coalesce("''").As("refTo_F2"),
                    noct.ItemName.Coalesce("''").As("refTo_SRNursingNocType"),
                    nict.ItemName.Coalesce("''").As("refTo_SRNursingNicType")
                    );

            coll.Load(ndt);
            return coll;
        }
        public static NursingDiagnosaTransDTCollection NursingDiagnosa(string TransactionNo, string Level)
        {
            return NursingDiagnosa(TransactionNo, new string[] { Level });
        }
        public static NursingDiagnosaTransDTCollection NursingDiagnosa(string TransactionNo, string[] Levels)
        {
            var coll = new NursingDiagnosaTransDTCollection();
            coll.Query.Where(coll.Query.TransactionNo == TransactionNo,
                coll.Query.SRNursingDiagnosaLevel.In(Levels));
            coll.LoadAll();
            return coll;
        }

        public static DataTable NursingDiagnosaFullDefinitionWithNicNoc(string TransactionNo, List<string> SRNsDiagnosaType) {
            var dttbl = NursingDiagnosaFullDefinition(TransactionNo, SRNsDiagnosaType);
            // NOC header dan NIC
            var nocnic = new NursingDiagnosaTransDTCollection();
            if (dttbl.Rows.Count > 0)
            {
                //var nnQ = new NursingDiagnosaTransDTQuery();
                //nnQ.Where(nnQ.ParentID.In(
                //    (from d in dttbl.AsEnumerable() select d.Field<long>("Id"))
                //    ), nnQ.SRNursingDiagnosaLevel.In("20"/*noc header*/, "30"/*nic*/));
                //nocnic.Load(nnQ);

                nocnic = NursingDiagnosa(
                    (from d in dttbl.AsEnumerable() select d.Field<long>("Id")).ToArray(),
                    new string[] { "20"/*noc header*/, "30"/*nic*/});
            }

            // add new column NIC
            //DataColumn dc = new DataColumn("NIC", typeof(string));
            //dttbl.Columns.Add(dc);

            bool IsShowScale = false;

            var appPar = new AppParameter();
            if (appPar.LoadByPrimaryKey("IsNsOutcomeShowScale")) {
                IsShowScale = (appPar.ParameterValue.ToLower() == "yes");
            }
            foreach (DataRow r in dttbl.Rows)
            {
                // if asbid
                if (r["SRNsDiagnosaType"].ToString() == "02" /*Kebidanan*/)
                {
                    // actual
                    string[] etiosA = r["EtiologyActual"].ToString().Split((new string[] { ", " }), StringSplitOptions.RemoveEmptyEntries);
                    string etioActual = "";
                    foreach (var eti in etiosA)
                    {
                        etioActual += "<li>" + eti + "</li>";
                    }
                    if (etioActual != string.Empty) etioActual = "<span style='color: gray; '><strong>Actual</strong></span><br /><ul>" + etioActual + "</ul>";
                    r["EtiologyActual"] = etioActual;

                    // potensial
                    string[] etiosP = r["EtiologyPotensial"].ToString().Split((new string[] { ", " }), StringSplitOptions.RemoveEmptyEntries);
                    string etioPotensial = "";
                    foreach (var eti in etiosP)
                    {
                        etioPotensial += "<li>" + eti + "</li>";
                    }
                    if (etioPotensial != string.Empty) etioPotensial = "<span style='color: gray; '><strong>Potensial</strong></span><br /><ul>" + etioPotensial + "</ul>";
                    r["EtiologyActual"] = r["EtiologyActual"].ToString() + etioPotensial;
                }
                else
                {
                    r["EtiologyActual"] = string.Empty;
                }

                // NIC
                var nics = "";
                var xnic = nocnic.Where(x => x.SRNursingDiagnosaLevel == "30" && x.ParentID == (long)r["Id"]);
                string[] nicts = xnic.Select(x => x.refToSRNursingNicType).ToArray().Distinct().ToArray();
                string[] f2s = xnic.Select(x => x.RefToF2).ToArray().Distinct().ToArray();
                string[] f1s = xnic.Select(x => x.RefToF1).ToArray().Distinct().ToArray();

                foreach (var nict in nicts) {
                    string sf3 = "";
                    foreach (var f2 in f2s) {
                        string sf2 = "";
                        foreach (var f1 in f1s) {
                            string sf1 = "";
                            foreach (var xn in xnic.Where(x => x.refToSRNursingNicType == nict && x.RefToF2 == f2 && x.RefToF1 == f1))
                            {
                                sf1 += string.Format("<li class='linicnoc'>{0}{1}</li>", xn.NursingDiagnosaName,
                                    (xn.SRNursingCarePlanning == "03" ?
                                        string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/new_tag_16.png") :
                                        (xn.SRNursingCarePlanning == "01" ? string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/stop_tag_16.png") : "")));
                            }
                            if (sf1.Length > 0)
                            {
                                sf1 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", f1, sf1);
                                sf2 += sf1;
                            }
                        }
                        if (sf2.Length > 0) {
                            sf2 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", f2, sf2);
                            sf3 += sf2;
                        }
                    }
                    if (sf3.Length > 0)
                    {
                        sf3 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", nict, sf3);
                        nics += sf3;
                    }
                }
                if (nics.Length > 0)
                {
                    nics = string.Format("<ul>{0}</ul>", nics);
                }

                if (nics != string.Empty) nics = "<span style='color: gray; '><strong>" + 
                        (r["SRNsDiagnosaType"].ToString() == "01" ? AppParameter.GetParameterValue(AppParameter.ParameterItem.NsIntervention) : "Interventions") + 
                        "</strong></span><br />" + nics + "";
                r["NIC"] = nics;

                ////var nics = "";
                //var xnic = nocnic.Where(x => x.SRNursingDiagnosaLevel == "30" && x.ParentID == (long)r["Id"]);
                //foreach (var xn in xnic)
                //{
                //    nics += string.Format("<li>{0}{1}</li>", xn.NursingDiagnosaName,
                //        (xn.SRNursingCarePlanning == "03" ?
                //            string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/new_tag_16.png") :
                //            (xn.SRNursingCarePlanning == "01" ? string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/stop_tag_16.png") : "")));
                //}
                //if (nics != string.Empty) nics = "<span style='color: gray; '><strong>Interventions</strong></span><br /><ul>" + nics + "</ul>";
                //r["NIC"] = nics;

                // NOC Header
                var noccc = "";
                var xnocH = nocnic.Where(x => x.SRNursingDiagnosaLevel == "20" && x.ParentID == (long)r["Id"]);
                string[] nocts = xnocH.Select(x => x.refToSRNursingNocType).ToArray().Distinct().ToArray();
                foreach (var noct in nocts)
                {
                    //string f3s = "";
                    var nocHs = "";
                    foreach (var xn in xnocH.Where(x => x.refToSRNursingNocType == noct))
                    {
                        // NOC
                        string snoc = string.Empty;
                        var nocs = NursingDiagnosa(xn.ID.Value, "21");
                        foreach (var noc in nocs)
                        {
                            snoc += string.Format("<li class='linicnoc'>{0}{1}</li>", noc.NursingDiagnosaName,
                                (IsShowScale && (r["SRNsDiagnosaType"].ToString() == "01" /*keperawatan saja yang ditampilkan skalanya*/)) ? (" [" + noc.Skala.ToString() + "]"):"");
                        }
                        if (xn.NursingDiagnosaName.Contains("NOC"))
                        {
                            nocHs += snoc;
                        }
                        else
                        {
                            if (snoc != string.Empty) snoc = "<ul>" + snoc + "</ul>";

                            nocHs += "<li class='linicnoc'>"
                                + string.Format("<span title='{1}'>{2}{0}</span>", xn.NursingDiagnosaName, xn.RefToF1, string.IsNullOrEmpty(xn.RefToF1) ? "":"*") 
                                + snoc + "</li>";
                        }
                    }
                    nocHs = "<li class='linicnoc'>" + noct + "<ul>" + nocHs + "</ul>" + "</li>";
                    noccc += nocHs;
                }
                //    foreach (var xn in xnocH)
                //{
                //    // NOC
                //    string snoc = string.Empty;
                //    var nocs = NursingDiagnosa(xn.ID.Value, "21");
                //    foreach (var noc in nocs)
                //    {
                //        snoc += "<li class='linicnoc'>" + noc.NursingDiagnosaName + "</li>";
                //    }
                //    if (xn.NursingDiagnosaName.Contains("NOC"))
                //    {
                //        nocHs += snoc;
                //    }
                //    else
                //    {
                //        if (snoc != string.Empty) snoc = "<ul>" + snoc + "</ul>";
                        
                //        nocHs += "<li class='linicnoc'>" + xn.NursingDiagnosaName + snoc + "</li>" ;
                //    }
                //}
                if (noccc != string.Empty) noccc = string.Format("<span style='color: gray; '><strong>{0}</strong></span><br /><ul>{1}</ul>",
                    (r["SRNsDiagnosaType"].ToString() == "02") ? AppParameter.GetParameterValue(AppParameter.ParameterItem.NsOutcome02) : AppParameter.GetParameterValue(AppParameter.ParameterItem.NsOutcome), noccc);
                r["NOC"] = noccc;
            }
            dttbl.AcceptChanges();

            // belum selesai
            return dttbl;
        }

        public static DataTable NursingDiagnosaFullDefinitionWithNicNoc(string TransactionNo)
        {
            return NursingDiagnosaFullDefinitionWithNicNoc(TransactionNo, null);
        }

        public static DataTable NursingDiagnosaFullDefinition(string TransactionNo) {
            return NursingDiagnosaFullDefinition(TransactionNo, null);
        }
        public static DataTable NursingDiagnosaFullDefinition(string TransactionNo, List<string> SRNsDiagnosaType)
        {
            bool NsShowDiagnosaCode = AppParameter.IsYes(AppParameter.ParameterItem.NsIsShowDiagnosaCode);

            //NursingDiagnosaCollection coll = new NursingDiagnosaCollection();

            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            NursingDiagnosaTransDTQuery ddt = new NursingDiagnosaTransDTQuery("b");
            var sr1 = new AppStandardReferenceItemQuery("sr1");
            var usr = new AppUserQuery("usr");

            query.InnerJoin(ddt).On(query.NursingDiagnosaID == ddt.NursingDiagnosaID)
                .LeftJoin(sr1).On(ddt.SRNursingCarePlanning == sr1.ItemID && sr1.StandardReferenceID == "NursingCarePlanning")
                .LeftJoin(usr).On(ddt.CreateByUserID == usr.UserID);
            query.Where(ddt.TransactionNo == TransactionNo,
                query.SRNursingDiagnosaLevel == "10");
            query.Select(
                query,
                ddt.ID,
                ddt.NursingDiagnosaName.As("NursingDiagnosaNameDisplay"),
                ddt.NursingDiagnosaName.As("NursingDiagnosaNameTransDT"),
                ddt.Priority,
                ddt.EvalPeriod,
                ddt.PeriodConversionInHour,
                ddt.Skala,
                ddt.Target,
                ddt.Evaluasi,
                ddt.SRNursingCarePlanning,
                sr1.ItemName.As("SRNursingCarePlanningName"),
                ddt.ExecuteDateTime,
                ddt.CreateDateTime.As("NursingDiagnosaCreateDateTime"),
                "<'' Noc>",
                "<'' Nic>",
                "<0 Non11ChildCount>",
                "<'' EtiologyActual>",
                "<'' EtiologyPotensial>",
                "<'' AssAsPrefix>",
                "<'' AssAsSuffix>",
                "<'' AdditionalPrefix>",
                "<'' AdditionalSuffix>",
                usr.UserName,
                ddt.ExecuteDateTime.As("LastEvaluationDateTime")
            ).OrderBy(ddt.Priority.Ascending);
            if (SRNsDiagnosaType != null/* &&  > 0*/) {
                if (SRNsDiagnosaType.Count() == 0) SRNsDiagnosaType.Add("xxxx"); // add sembarang biar gak nemu
                query.Where(query.SRNsDiagnosaType.In(SRNsDiagnosaType));
            }
            query.es.Distinct = true;
            //coll.Load(query);
            var dttbl = query.LoadDataTable();

            foreach (System.Data.DataRow c in dttbl.Rows)
            {
                // ambil jumlah child yang selain level 11
                var dc = new NursingDiagnosaTransDTCollection();
                dc.Query.Where(dc.Query.ParentID == c["ID"] && dc.Query.SRNursingDiagnosaLevel != "11");
                if (dc.LoadAll())
                {
                    c["Non11ChildCount"] = dc.Count;
                }

                // problem
                var collProb = new NursingDiagnosaTransDTCollection();
                var qsprob = new NursingDiagnosaTransDTQuery("b");
                var et = new NursingDiagnosaQuery("et");
                qsprob.InnerJoin(et).On(qsprob.NursingDiagnosaID == et.NursingDiagnosaID)
                    .Where(qsprob.ParentID == c["ID"] && qsprob.SRNursingDiagnosaLevel == "11")
                    .Select(
                        qsprob, 
                        et.SRNsEtiologyType.As("refTo_SRNsEtiologyType"));
                collProb.Load(qsprob);

                //string sBrhbngDng = string.Empty;
                foreach (var iProb in collProb)
                {
                    if (iProb.RefToSRNsEtiologyType.ToString() == "02")
                    {
                        c["EtiologyPotensial"] += string.IsNullOrEmpty(c["EtiologyPotensial"].ToString()) ? "" : ", ";
                        c["EtiologyPotensial"] += iProb.NursingDiagnosaName;
                    }
                    else {
                        c["EtiologyActual"] += string.IsNullOrEmpty(c["EtiologyActual"].ToString()) ? "" : ", ";
                        c["EtiologyActual"] += iProb.NursingDiagnosaName;
                    }
                }

                if (c["SRNsDiagnosaType"].ToString() == "02" /*Kebidanan*/)
                {
                    // kebidanan tidak ditampilkan berhubungan dengan
                    // tampilin dulu deh, tapi dibawah ya
                    //c["NursingDiagnosaNameDisplay"] = c["NursingDiagnosaNameDisplay"].ToString() + " berhubungan dengan " + sBrhbngDng;

                }
                else
                {
                    //c.NursingDiagnosaName += " berhubungan dengan " + sBrhbngDng;
                    c["NursingDiagnosaNameDisplay"] =  (!NsShowDiagnosaCode || string.IsNullOrEmpty(c["NursingDiagnosaCode"].ToString()) ? "" : string.Format("[{0}] ", c["NursingDiagnosaCode"].ToString())) + 
                        c["NursingDiagnosaNameDisplay"].ToString() + 
                        (c["SRNsDiagnosaType"].ToString() == "03"?" berkaitan dengan ": " berhubungan dengan ") + 
                        (c["EtiologyActual"] + " " + c["EtiologyPotensial"]).Trim();
                }

                //string sDitndiDngAsPrefix = string.Empty;
                //string sDitndiDngAsSuffix = string.Empty;
                //string sDiagPrefix = string.Empty;
                //string sDiagSuffix = string.Empty;
                // cek dulu di DS DO etiology ada isi atau tidak
                // kalau ada isi berarti DS DO dari implementasi yang diangkat baru, pakai saja DS DO-nya bulat2
                foreach (var iProb in collProb)
                {
                    if (!string.IsNullOrEmpty(iProb.S) || !string.IsNullOrEmpty(iProb.O))
                    {
                        if (c["SRNsDiagnosaType"].ToString() == "02" /*Kebidanan*/)
                        {
                            c["AssAsPrefix"] += FormatSymptomText(iProb.S);
                            c["AssAsSuffix"] += FormatSymptomText(iProb.O);
                        }
                        else {
                            c["AssAsSuffix"] += FormatSymptomText(iProb.S, iProb.O);
                        }
                        break; /*break aja karena 1 diagnosa bisa lebih dari satu etiology.
                                kalau tidak break nanti "ditandai dengan"-nya jadi double-double*/
                    }
                }

                if (string.IsNullOrEmpty(c["AssAsPrefix"].ToString()) && string.IsNullOrEmpty(c["AssAsSuffix"].ToString()))
                {
                    var dtb = NursingAssessmentLast(TransactionNo, c["NursingDiagnosaID"].ToString(), 
                        (DateTime)c["ExecuteDateTime"]);

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
                        DateTime AssDate = (DateTime)iProb["AssessmentDateTime"];

                        string AnswerText = iProb["AnswerText"].ToString();

                        var sAss = GetAssessmentParsedString(AssDate, 
                            SRAnswerTypeFromQuestion, SRAnswerTypeFromMatrix, AcceptedText, Operand,
                            AnswerText, AcceptedNum, AnsNum, AcceptedBool, iProb["QuestionText"].ToString(),
                            iProb["AnswerDecimalDigit"] is DBNull ? 0: System.Convert.ToInt16(iProb["AnswerDecimalDigit"]), 
                            iProb["AnswerSuffix"].ToString(),
                            (bool)(iProb["IsUsingRange"] ?? false),
                            (decimal)(iProb["AcceptedNum2"] ?? 0),
                            System.Convert.ToInt32(iProb["PatientAgeInMonth"]),
                            System.Convert.ToInt32(iProb["AgeInMonthStart"]),
                            System.Convert.ToInt32(iProb["AgeInMonthEnd"]),
                            iProb["PatientSex"].ToString(), iProb["Sex"].ToString(), iProb["QuestionID"].ToString());

                        if (string.IsNullOrEmpty(sAss)) continue;

                        c["AdditionalPrefix"] += string.IsNullOrEmpty(c["AdditionalPrefix"].ToString()) ? "" : ", ";
                        c["AdditionalPrefix"] += iProb["SRNsDiagnosaPrefix"].ToString();
                        c["AdditionalSuffix"] += string.IsNullOrEmpty(c["AdditionalSuffix"].ToString()) ? "" : ", ";
                        c["AdditionalSuffix"] += iProb["SRNsDiagnosaSuffix"].ToString();

                        if (c["SRNsDiagnosaType"].ToString() != "02") iProb["ShowAssessmetAsSuffix"] = true;
                        if (iProb["ShowAssessmetAsSuffix"] is DBNull) iProb["ShowAssessmetAsSuffix"] = true;
                        if (iProb["ShowAssessmetAsPrefix"] is DBNull) iProb["ShowAssessmetAsPrefix"] = false;

                        if ((bool)(iProb["ShowAssessmetAsSuffix"] ?? true)) {
                            // untuk mencegah double assessment, contoh: mapping text ada nyeri dada,
                            // tapi di checkbox juga ada nyeri dada, maka ditandainya jadi 
                            // nyeri dada, nyeri dada
                            if (c["AssAsSuffix"].ToString().ToLower().IndexOf(sAss.ToLower().Trim()) >= 0) continue;
                            //if (lAss.IndexOf(sAss.ToLower().Trim()) >= 0) continue;

                            c["AssAsSuffix"] += string.IsNullOrEmpty(c["AssAsSuffix"].ToString()) ? "" : ", ";

                            c["AssAsSuffix"] += sAss;
                        } else if ((bool)(iProb["ShowAssessmetAsPrefix"] ?? false))
                        {
                            // untuk mencegah double assessment, contoh: mapping text ada nyeri dada,
                            // tapi di checkbox juga ada nyeri dada, maka ditandainya jadi 
                            // nyeri dada, nyeri dada
                            if (c["AssAsPrefix"].ToString().ToLower().IndexOf(sAss.ToLower().Trim()) >= 0) continue;
                            //if (lAss.IndexOf(sAss.ToLower().Trim()) >= 0) continue;

                            c["AssAsPrefix"] += string.IsNullOrEmpty(c["AssAsPrefix"].ToString()) ? "" : ", ";

                            c["AssAsPrefix"] += sAss;
                        }
                    }
                    //c.NursingDiagnosaName += " ditandai dengan " + sDitndiDng;
                }

                if (c["SRNsDiagnosaType"].ToString() == "02" /*Kebidanan*/)
                {
                    string sDiagPrefix = c["AdditionalPrefix"].ToString();
                    string sDiagSuffix = c["AdditionalSuffix"].ToString();

                    // kebidanan tidak ditampilkan ditandai dengan, tetapi ditampilkan prefix dan suffix
                    if (!string.IsNullOrEmpty(sDiagPrefix)) {
                        string[] sPrefIDs = sDiagPrefix.Split((new string[] { ", " }), StringSplitOptions.RemoveEmptyEntries);
                        var stdPrefColl = new AppStandardReferenceItemCollection();
                        stdPrefColl.Query.Where(stdPrefColl.Query.StandardReferenceID == "NsDiagnosaPrefix",
                            stdPrefColl.Query.ItemID.In(sPrefIDs));
                        stdPrefColl.Query.OrderBy(stdPrefColl.Query.LineNumber.Ascending);
                        if (stdPrefColl.LoadAll()) {
                            sDiagPrefix = string.Empty;
                            foreach (var stdPref in stdPrefColl) {
                                sDiagPrefix = sDiagPrefix + (sDiagPrefix.Length == 0 ? "" : ", ") + stdPref.ItemName; 
                            }
                        }
                    }
                    c["AdditionalPrefix"] = sDiagPrefix;

                    if (!string.IsNullOrEmpty(sDiagSuffix))
                    {
                        string[] sSuffIDs = sDiagSuffix.Split((new string[] { ", " }), StringSplitOptions.RemoveEmptyEntries);
                        var stdSuffColl = new AppStandardReferenceItemCollection();
                        stdSuffColl.Query.Where(stdSuffColl.Query.StandardReferenceID == "NsDiagnosaSuffix",
                            stdSuffColl.Query.ItemID.In(sSuffIDs));
                        stdSuffColl.Query.OrderBy(stdSuffColl.Query.LineNumber.Ascending);
                        if (stdSuffColl.LoadAll())
                        {
                            sDiagSuffix = string.Empty;
                            foreach (var stdSuff in stdSuffColl)
                            {
                                sDiagSuffix = sDiagSuffix + (sDiagSuffix.Length == 0 ? "" : ", ") + stdSuff.ItemName;
                            }
                        }
                    }
                    c["AdditionalSuffix"] = sDiagSuffix;

                    c["NursingDiagnosaNameDisplay"] =
                        ((c["AssAsPrefix"] + " " + (sDiagPrefix + " " + c["NursingDiagnosaNameDisplay"].ToString() + " " + sDiagSuffix).Trim()).Trim() + " " + c["AssAsSuffix"].ToString()).Trim();

                    //c["NursingDiagnosaNameDisplay"] = c["NursingDiagnosaNameDisplay"].ToString() + " berhubungan dengan " + sBrhbngDng;
                }
                else
                {
                    c["NursingDiagnosaNameDisplay"] = c["NursingDiagnosaNameDisplay"].ToString() + 
                        " " + AppParameter.GetParameterValue(AppParameter.ParameterItem.NsSymptom) + " " +
                        //" ditandai dengan " + 
                        (c["AssAsPrefix"] + " " + c["AssAsSuffix"]).Trim();
                }

                c["NursingDiagnosaNameDisplay"] = c["NursingDiagnosaNameDisplay"].ToString();//HelperMirror.StripHTML(c["NursingDiagnosaNameDisplay"].ToString());

                // evaluation
                var ne = dc.Where(d => d.SRNursingDiagnosaLevel == "40").OrderByDescending(d => d.ExecuteDateTime).FirstOrDefault();
                if (ne != null) {
                    c["LastEvaluationDateTime"] = ne.ExecuteDateTime;
                }
            }

            //return coll;
            return dttbl;
        }

        public static List<AssessmentRet> NursingAssessmentForPrint(string TransactionNo, long DiagnosaID)
        {
            List<AssessmentRet> lList = new List<AssessmentRet>();

            var Diag = new NursingDiagnosaTransDT();
            if (Diag.LoadByPrimaryKey(DiagnosaID))
            {
                // problem
                var collProb = new NursingDiagnosaTransDTCollection();
                var qsprob = new NursingDiagnosaTransDTQuery("b");
                qsprob.Where(qsprob.ParentID == DiagnosaID && qsprob.SRNursingDiagnosaLevel == "11");
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
                    //var collAss = new NursingAssessmentTransDTCollection();
                    var natdt = new NursingAssessmentTransDTQuery("a");
                    var q = new QuestionQuery("q");
                    var nathd = new NursingAssessmentTransHDQuery("c");
                    var nad = new NursingAssessmentDiagnosaQuery("d");
                    var ndtdt = new NursingDiagnosaTransDTQuery("e");
                    var nth = new NursingTransHDQuery("nth");
                    var reg = new RegistrationQuery("reg");
                    var pat = new PatientQuery("pat");

                    natdt.InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                        .LeftJoin(nad).On(natdt.QuestionID == nad.QuestionID)
                        /* log: (rssa 2016 09 06)
                         * dari InnerJoin diganti LeftJoin karena Assessment yang ada di NsAssAlwaysPrint
                         * tidak muncul jika tidak ada mapping ke diagnosa yang diangkat 
                         * padahal NsAssAlwaysPrint itu wajib muncul 
                         * disetiap cetakan diagnosa keperawatan. 
                         */
                        //.InnerJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)
                        .LeftJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)
                        .InnerJoin(q).On(natdt.QuestionID == q.QuestionID && q.EquivalentQuestionID.Coalesce("''") == string.Empty)
                        .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                        .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                        .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                        .Where(nathd.TransactionNo == TransactionNo)
                        .Where(natdt.Or(ndtdt.NursingDiagnosaID == Diag.NursingDiagnosaID,
                            natdt.QuestionID.In(sTTV)
                        ))
                        .Select(
                            natdt.QuestionID,
                            natdt.QuestionText,
                            natdt.AnswerText,
                            natdt.AnswerNum,
                            q.AnswerPrefix,
                            q.AnswerSuffix,
                            q.AnswerDecimalDigit,
                            q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                            nad.AcceptedText,
                            nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.CheckValue,
                            nad.IsUsingRange,
                            nad.Operand,
                            nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                            "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd,
                            pat.Sex.As("PatientSex"),
                            nad.Sex,
                            nad.SRNsDiagnosaPrefix,
                            nad.SRNsDiagnosaSuffix,
                            nathd.AssessmentDateTime
                        );
                    natdt.es.Distinct = true;

                    //collAss.Load(qsass);
                    var dtb = natdt.LoadDataTable();

                    // ===== equivalent assessment
                    natdt = new NursingAssessmentTransDTQuery("a");
                    var qE = new QuestionQuery("qE");

                    natdt.InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                        /* log: (rssa 2016 09 06)
                         * dari InnerJoin diganti LeftJoin karena Assessment yang ada di NsAssAlwaysPrint
                         * tidak muncul jika tidak ada mapping ke diagnosa yang diangkat 
                         * padahal NsAssAlwaysPrint itu wajib muncul 
                         * disetiap cetakan diagnosa keperawatan. 
                         */
                        //.InnerJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)
                        .InnerJoin(q).On(natdt.QuestionID == q.QuestionID)
                        .InnerJoin(qE).On(qE.QuestionID == q.EquivalentQuestionID)
                        .LeftJoin(nad).On(qE.QuestionID == nad.QuestionID)
                        .LeftJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)
                        .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                        .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                        .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                        .Where(nathd.TransactionNo == TransactionNo)
                        .Where(natdt.Or(ndtdt.NursingDiagnosaID == Diag.NursingDiagnosaID,
                            natdt.QuestionID.In(sTTV)
                        ))
                        .Select(
                            natdt.QuestionID,
                            natdt.QuestionText,
                            natdt.AnswerText,
                            natdt.AnswerNum,
                            q.AnswerPrefix,
                            q.AnswerSuffix,
                            q.AnswerDecimalDigit,
                            q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                            nad.AcceptedText,
                            nad.AcceptedNum,
                            nad.AcceptedNum2,
                            nad.CheckValue,
                            nad.IsUsingRange,
                            nad.Operand,
                            nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                            "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                            nad.AgeInMonthStart,
                            nad.AgeInMonthEnd,
                            pat.Sex.As("PatientSex"),
                            nad.Sex,
                            nad.SRNsDiagnosaPrefix,
                            nad.SRNsDiagnosaSuffix,
                            nathd.AssessmentDateTime
                        );
                    natdt.es.Distinct = true;

                    //collAss.Load(qsass);
                    var dtb2 = natdt.LoadDataTable();

                    // ===== end of equivalent assessment

                    dtb.Merge(dtb2);

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
                        DateTime DateAssessment = (DateTime)iProb["AssessmentDateTime"];

                        string AnswerText = iProb["AnswerText"].ToString();

                        var sAss = GetAssessmentParsedList(DateAssessment, SRAnswerTypeFromQuestion, SRAnswerTypeFromMatrix, AcceptedText, Operand,
                            AnswerText, AcceptedNum, AnsNum, AcceptedBool, iProb["QuestionText"].ToString(),
                            System.Convert.ToInt16((iProb["AnswerDecimalDigit"] ?? 0)), iProb["AnswerSuffix"].ToString(),
                            System.Convert.ToBoolean((iProb["IsUsingRange"]) is DBNull ? false : iProb["IsUsingRange"]),
                            System.Convert.ToDecimal((iProb["AcceptedNum2"]) is DBNull ? 0.00 : iProb["AcceptedNum2"]),
                            System.Convert.ToInt16(iProb["PatientAgeInMonth"]),
                            System.Convert.ToInt16(iProb["AgeInMonthStart"] is DBNull ? 0 : iProb["AgeInMonthStart"]),
                            System.Convert.ToInt16(iProb["AgeInMonthEnd"] is DBNull ? 0 : iProb["AgeInMonthEnd"]),
                            iProb["PatientSex"].ToString(), iProb["Sex"].ToString(),
                            sTTV.Contains(iProb["QuestionID"]));

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

        private static List<AssessmentRet> FormatSymptomTextList(string S, string O)
        {
            return FormatSymptomTextList(S, 0).Union(FormatSymptomTextList(O, 1)).ToList();
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

        private static string FormatSymptomText(string S, string O)
        {
            S = FormatSymptomText(S);
            O = FormatSymptomText(O);
            return (FormatSymptomText(S) +
                (((S.Length > 0) && (O.Length > 0)) ? ", " : " ") +
                FormatSymptomText(O)).Trim();
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
        #region NURSING TARGET (NOC) (21)
        public static DataTable NursingTarget(string TransactionNo, long IdDiag)
        {
            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery prNoc = new NursingDiagnosaQuery("b");
            NursingDiagnosaQuery prDiag = new NursingDiagnosaQuery("c");
            NursingDiagnosaTransDTQuery dtDiag = new NursingDiagnosaTransDTQuery("d");
            NursingDiagnosaTransDTQuery dtNoc = new NursingDiagnosaTransDTQuery("e");
            NursingDiagnosaTransDTQuery dt = new NursingDiagnosaTransDTQuery("f");
            AppUserQuery uq = new AppUserQuery("uq");
            var noct = new AppStandardReferenceItemQuery("noct");

            //query.es.Distinct = true;

            query.InnerJoin(prNoc).On(query.NursingDiagnosaParentID == prNoc.NursingDiagnosaID
                & query.SRNursingDiagnosaLevel == "21")

                .InnerJoin(prDiag).On(prNoc.NursingDiagnosaParentID == prDiag.NursingDiagnosaID
                & prNoc.SRNursingDiagnosaLevel == "20" & prDiag.SRNursingDiagnosaLevel == "10")

                .InnerJoin(dtDiag).On(prDiag.NursingDiagnosaID == dtDiag.NursingDiagnosaID
                & dtDiag.TransactionNo == TransactionNo & dtDiag.ID == IdDiag)

                .LeftJoin(dtNoc).On(prNoc.NursingDiagnosaID == dtNoc.NursingDiagnosaID
                & dtDiag.ID == dtNoc.ParentID)

                .LeftJoin(dt).On(query.NursingDiagnosaID == dt.NursingDiagnosaID
                & dtNoc.ID == dt.ParentID)

                .LeftJoin(uq).On(dt.LastUpdateByUserID == uq.UserID)

                .LeftJoin(noct).On(noct.StandardReferenceID == "NursingNocType" && prNoc.SRNursingNocType == noct.ItemID);

            var ndsu = new NursingDiagnosaNsTypeQuery("ndsu");
            query.InnerJoin(ndsu).On(query.NursingDiagnosaID == ndsu.NursingDiagnosaID &
                ndsu.SRNsType == dtDiag.SRNsType);

            query.Select(
                query,
                dtDiag.ID.As("IdDiag"),
                prNoc.NursingDiagnosaID.As("NocID"),
                prNoc.SequenceNo.As("NocSequenceNo"),
                prNoc.NursingDiagnosaCode.As("NocCode"),
                prNoc.NursingDiagnosaName.As("NocName"),
                prNoc.SRNursingNocType.As("NocType"),
                noct.ItemName.Coalesce("''").As("NocTypeName"),
                dt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                "<ISNULL(f.NursingDiagnosaName, a.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                dtDiag.Priority,
                dtDiag.EvalPeriod,
                dtDiag.PeriodConversionInHour,
                dt.Skala,
                dt.Target,
                dt.Evaluasi,
                dt.ExecuteDateTime,
                "<CAST(CASE WHEN (DATEDIFF(hh, d.ExecuteDateTime, GETDATE()) >= d.EvalPeriod * d.PeriodConversionInHour) and d.Reexamine = 1 and f.Evaluasi < f.Target THEN 1 ELSE 0 END AS BIT) AlertEvaluate>",
                dt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dt.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                dt.ID,
                uq.UserName
                )
                .Where(query.IsActive == true);

            var dttbl = query.LoadDataTable();
            return dttbl;
        }
        #endregion
        #region NURSING PLANNING (NIC) (30)
        public static DataTable NursingPlanning(/*string TransactionNo, */long IdDiagL10)
        {
            NursingDiagnosaQuery nic = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery diag = new NursingDiagnosaQuery("b");

            NursingDiagnosaTransDTQuery dtDiag = new NursingDiagnosaTransDTQuery("d");
            NursingDiagnosaTransDTQuery dtNic = new NursingDiagnosaTransDTQuery("e");
            AppStandardReferenceItemQuery nictype = new AppStandardReferenceItemQuery("f");
            AppUserQuery uq = new AppUserQuery("uq");

            //nic.es.Distinct = true;

            nic.InnerJoin(diag).On(nic.NursingDiagnosaParentID == diag.NursingDiagnosaID &
                nic.SRNursingDiagnosaLevel == "30");
            nic.InnerJoin(dtDiag).On(diag.NursingDiagnosaID == dtDiag.NursingDiagnosaID & dtDiag.ID == IdDiagL10);
            nic.LeftJoin(dtNic).On(dtDiag.ID == dtNic.ParentID & nic.NursingDiagnosaID == dtNic.NursingDiagnosaID);
            nic.LeftJoin(nictype).On(nic.SRNursingNicType == nictype.ItemID & nictype.StandardReferenceID == "NursingNicType");
            nic.LeftJoin(uq).On(dtNic.LastUpdateByUserID == uq.UserID);

            var ndsu = new NursingDiagnosaNsTypeQuery("ndsu");
            nic.InnerJoin(ndsu).On(nic.NursingDiagnosaID == ndsu.NursingDiagnosaID &
                ndsu.SRNsType == dtDiag.SRNsType);

            nic.Select(
                nic,
                "<ISNULL(e.NursingDiagnosaName, a.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                diag.SequenceNo.As("ParentSequenceNo"),
                diag.NursingDiagnosaName.As("NursingParentName"),
                dtNic.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                dtNic.Priority,
                dtNic.EvalPeriod,
                dtNic.PeriodConversionInHour,
                dtNic.Skala,
                dtNic.Target,
                dtNic.Evaluasi,
                dtNic.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                dtNic.LastUpdateDateTime.As("LastUpdateTransDTDateTime"),
                //nic.SRNursingNicType,
                nictype.ItemName.As("SRNursingNicTypeName"),
                "<CAST((CASE(ISNULL(e.SRNursingCarePlanning, CASE ISNULL(e.[NursingDiagnosaID],'') WHEN '' THEN '01' ELSE '02' END )) WHEN '01' THEN 0 ELSE 1 END) AS BIT) Status>",
                uq.UserName
                )
                .OrderBy(nic.NursingDiagnosaID.Ascending)
                .Where(nic.IsActive == true);
            //.OrderBy("<ISNULL(e.NursingDiagnosaName, a.NursingDiagnosaName), CAST((CASE(e.SRNursingCarePlanning) WHEN '01' THEN 1 ELSE 0 END) AS BIT)>");

            var dttbl = nic.LoadDataTable();
            return dttbl;
        }
        #endregion
        #region NURSING IMPLEMENTASI (31)
        public static NursingDiagnosaTransDTCollection Implementation(string TransactionNo, int Top)
        {
            NursingDiagnosaTransDTCollection coll = new NursingDiagnosaTransDTCollection();
            NursingDiagnosaTransDTQuery query = new NursingDiagnosaTransDTQuery("a");
            AppUserQuery uq = new AppUserQuery("uq");

            if (Top > 0) query.es.Top = Top;

            query.LeftJoin(uq).On(query.LastUpdateByUserID == uq.UserID);
            query.Where(query.TransactionNo == TransactionNo);
            query.Where(query.Or(
                    query.SRNursingDiagnosaLevel == "31",
                    query.SRNursingDiagnosaLevel == "32"
                ));
            query.Select(
                query,
                uq.UserName.As("refTo_UserName")
                );
            query.OrderBy(query.ExecuteDateTime.Descending);
            coll.Load(query);

            return coll;
        }

        #region NURSING EVALUATION (40)
        public static NursingDiagnosaTransDTCollection Evaluation(string TransactionNo)
        {
            NursingDiagnosaTransDTCollection coll = new NursingDiagnosaTransDTCollection();
            NursingDiagnosaTransDTQuery query = new NursingDiagnosaTransDTQuery("a");

            query.Where(query.TransactionNo == TransactionNo);
            query.Where(query.Or(
                    query.SRNursingDiagnosaLevel == "40"
                ));
            query.Select(
                query
                );
            coll.Load(query);

            return coll;
        }

        public static NursingDiagnosaTransDTCollection Evaluation(long idDiagnoseL10)
        {
            NursingDiagnosaTransDTCollection coll = new NursingDiagnosaTransDTCollection();
            NursingDiagnosaTransDTQuery query = new NursingDiagnosaTransDTQuery("a");

            query.Where(query.ParentID == idDiagnoseL10, query.SRNursingDiagnosaLevel == "40");
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
            var t = new NursingDiagnosaTemplateDetailQuery("t");
            //var vs = new VitalSignQuery("vs");

            q.InnerJoin(t).On(q.QuestionID == t.QuestionID && t.TemplateID == TemplateID)
                .OrderBy(t.RowIndex.Ascending);
            //q.InnerJoin(vs).On(q.VitalSignID == vs.VitalSignID);
            //q.OrderBy(vs.SRVitalSignGroup.Ascending, vs.RowIndexInGroup.Ascending);

            q.Select(q);
            qColl.Load(q);
            return qColl;
        }
        public static long GetIDNursingAssessmentByRefNo(string QuestionFormReference)
        {
            var ns = new NursingAssessmentTransHDCollection();
            ns.Query.Where(ns.Query.QuestionFormReference == QuestionFormReference);
            ns.LoadAll();
            return (long)(ns.Count > 0 ? ns[0].Id : 0);
        }

        public static DataTable NursingAssessmentByRefNo(string QuestionFormReference)
        {
            var ns = new NursingAssessmentTransHDCollection();
            ns.Query.Where(ns.Query.QuestionFormReference == QuestionFormReference);
            ns.LoadAll();
            if (ns.Count == 0)
            {
                throw new Exception("Related assessment can not be found or has been deleted.");
            }

            return NursingAssessment(ns[0].Id.ToString(), true);
        }

        public static DataTable NursingAssessment(string AssessmentID, bool AllAssessment)
        {
            QuestionQuery q = new QuestionQuery("q");

            NursingAssessmentTransHDQuery qHd = new NursingAssessmentTransHDQuery("f");
            NursingAssessmentTransDTQuery qDt = new NursingAssessmentTransDTQuery("e");

            Int64 hdID = string.IsNullOrEmpty(AssessmentID) ? 0 : Int64.Parse(AssessmentID);

            q.LeftJoin(qDt).On(q.QuestionID == qDt.QuestionID
                & qDt.Hdid == hdID
            );
            q.Select
                (
                    q,
                    qDt.QuestionID.As("TransQuestionID"),
                    "<ISNULL(e.IsSubjective, 0) IsSub>",
                    "<ISNULL(e.IsObjective, 0) IsObj>",
                    "<ISNULL(e.QuestionText, ISNULL(q.NursingDisplayAs ,q.QuestionText)) QuestionTextEdited>",
                    "<ISNULL(e.AnswerText, '') AnswerText>",
                    qDt.LastUpdateByUserID.As("LastUpdateTransDTBy"),
                    qDt.LastUpdateDateTime.As("LastUpdateTransDTDateTime")
                );
            if (!AllAssessment) q.Where(qDt.QuestionID.IsNotNull());
            q.es.Distinct = true;
            var dttbl = q.LoadDataTable();

            return dttbl;
        }

        public static List<AssessmentRet> NursingAssessmentLastUpdate_DISABLED(string TransactionNo, string NursingDiagnosaID)
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
                            nad.AgeInMonthEnd,
                            pat.Sex as PatientSex,
                            nad.Sex,
                            nad.ShowAssessmetAsPrefix,
                            nad.ShowAssessmetAsSuffix
                            nad.SRNsDiagnosaPrefix,
                            nad.SRNsDiagnosaSuffix,
                            nth.AssessmentDateTime
		                FROM NursingAssessmentTransHD AS nath 
			                INNER JOIN NursingAssessmentTransDT AS natd ON nath.ID = natd.HDID
			                INNER JOIN NursingAssessmentQuestion AS naq ON naq.QuestionID = natd.QuestionID
			                INNER JOIN NursingAssessmentDiagnosa AS nad ON nad.QuestionID = naq.QuestionID
                            INNER JOIN NursingTransHD nth ON nath.TransactionNo = nth.TransactionNo
                            INNER JOIN Registration reg ON nth.RegistrationNo = reg.RegistrationNo
                            INNER JOIN Patient pat ON reg.PatientID = pat.PatientID
		                WHERE nath.TransactionNo = @TransactionNo AND nad.NursingDiagnosaID = @NursingDiagnosaID
                   ) s
                )
                SELECT *
                FROM cte
                WHERE rn = 1
                ";
            esParameters par = new esParameters();
            par.Add("TransactionNo", TransactionNo);
            par.Add("NursingDiagnosaID", NursingDiagnosaID);

            var dt = (new Temiang.Dal.Core.esUtility()).FillDataTable(esQueryType.Text, cmd, par);

            List<AssessmentRet> lAssessment = new List<AssessmentRet>();
            foreach (DataRow d in dt.Rows)
            {
                var ls = GetAssessmentParsedList((DateTime)(d["AssessmentDateTime"]),
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
                    d["PatientSex"].ToString(), d["Sex"].ToString(),
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

        public static DataTable NursingAssessmentLast(string TransactionNo, string NursingDiagnosaID, DateTime DiagnosaDate) {
            // assessment direct mapping
            var natdt = new NursingAssessmentTransDTQuery("a");
            var q = new QuestionQuery("q");
            var nathd = new NursingAssessmentTransHDQuery("c");
            var nad = new NursingAssessmentDiagnosaQuery("d");
            var ndtdt = new NursingDiagnosaTransDTQuery("e");
            var nth = new NursingTransHDQuery("nth");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");

            natdt.InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                .InnerJoin(nad).On(natdt.QuestionID == nad.QuestionID)
                .InnerJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)
                .InnerJoin(q).On(natdt.QuestionID == q.QuestionID && q.EquivalentQuestionID.Coalesce("''") == string.Empty)
                .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .Where(nathd.TransactionNo == TransactionNo,
                    ndtdt.NursingDiagnosaID == NursingDiagnosaID)
                .Select(
                    natdt.QuestionID,
                    natdt.QuestionText,
                    natdt.AnswerText,
                    natdt.AnswerNum,
                    q.AnswerPrefix,
                    q.AnswerSuffix,
                    q.AnswerDecimalDigit.Coalesce("0"),
                    q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                    nad.AcceptedText,
                    nad.AcceptedNum,
                    nad.AcceptedNum2,
                    nad.CheckValue,
                    nad.IsUsingRange,
                    nad.Operand,
                    nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                    "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                    nad.AgeInMonthStart,
                    nad.AgeInMonthEnd,
                    pat.Sex.As("PatientSex"),
                    nad.Sex,
                    nad.ShowAssessmetAsPrefix,
                    nad.ShowAssessmetAsSuffix,
                    nad.SRNsDiagnosaPrefix,
                    nad.SRNsDiagnosaSuffix,
                    nathd.AssessmentDateTime,
                    natdt.Id
                );
            natdt.es.Distinct = true;

            //collAss.Load(qsass);
            var dtb = natdt.LoadDataTable();

            //=============
            // equivalent mapping
            // assessment equivalent mapping
            natdt = new NursingAssessmentTransDTQuery("a");
            var qE = new QuestionQuery("qE");

            natdt.InnerJoin(nathd).On(natdt.Hdid == nathd.Id)
                .InnerJoin(q).On(natdt.QuestionID == q.QuestionID)
                .InnerJoin(qE).On(qE.QuestionID == q.EquivalentQuestionID)
                .InnerJoin(nad).On(qE.QuestionID == nad.QuestionID)
                .InnerJoin(ndtdt).On(nad.NursingDiagnosaID == ndtdt.NursingDiagnosaID)

                .InnerJoin(nth).On(nathd.TransactionNo == nth.TransactionNo)
                .InnerJoin(reg).On(nth.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .Where(nathd.TransactionNo == TransactionNo,
                    ndtdt.NursingDiagnosaID == NursingDiagnosaID)
                .Select(
                    natdt.QuestionID,
                    natdt.QuestionText,
                    natdt.AnswerText,
                    natdt.AnswerNum,
                    q.AnswerPrefix,
                    q.AnswerSuffix,
                    q.AnswerDecimalDigit.Coalesce("0"),
                    q.SRAnswerType.As("SRAnswerTypeFromQuestion"),
                    nad.AcceptedText,
                    nad.AcceptedNum,
                    nad.AcceptedNum2,
                    nad.CheckValue,
                    nad.IsUsingRange,
                    nad.Operand,
                    nad.SRAnswerType.As("SRAnswerTypeFromMatrix"),
                    "<reg.AgeInMonth + (reg.AgeInYear * 12) as PatientAgeInMonth>",
                    nad.AgeInMonthStart,
                    nad.AgeInMonthEnd,
                    pat.Sex.As("PatientSex"),
                    nad.Sex,
                    nad.ShowAssessmetAsPrefix,
                    nad.ShowAssessmetAsSuffix,
                    nad.SRNsDiagnosaPrefix,
                    nad.SRNsDiagnosaSuffix,
                    nathd.AssessmentDateTime,
                    natdt.Id
                );
            natdt.es.Distinct = true;

            //collAss.Load(qsass);
            var dtbE = natdt.LoadDataTable();
            //=============

            dtb.Merge(dtbE);

            if (dtb.Rows.Count > 0)
            {
                // ambil nilai terakhir saja dari setiap assessment
                var newObjR = dtb.AsEnumerable()
                    .Where(r => r.Field<DateTime>("AssessmentDateTime") <= DiagnosaDate)
                    .GroupBy(r => r.Field<string>("QuestionID"))
                    //.OrderByDescending(o => o.Field<DateTime>("AssessmentDateTime"))
                    //.Select(g => g.First())
                    .Select(g => g.OrderByDescending(r => r.Field<DateTime>("AssessmentDateTime")).First())
                    .Select(h => new
                    {
                        QuestionID = h.Field<string>("QuestionID"),
                        AssessmentDateTime = h.Field<DateTime>("AssessmentDateTime")
                    });
                //.CopyToDataTable();

                //dtb = rows.Any() ? rows.CopyToDataTable() : dtb.Clone();
                var xRows = dtb.AsEnumerable().Where(x => newObjR.Contains(new
                {
                    QuestionID = x.Field<string>("QuestionID"),
                    AssessmentDateTime = x.Field<DateTime>("AssessmentDateTime")
                }));

                dtb = xRows.Any() ? xRows.CopyToDataTable() : dtb.Clone();
            }
            return dtb;
        }

        public static List<AssessmentRet> GetAssessmentValueList(string SRAnswerType, string QuestionAnswerText,
            decimal QuestionAnswerNum, string QuestionText, int AnswerDecimalDigit, string AnswerSuffix) {
            return GetAssessmentParsedList(DateTime.Now, //phr.RecordDate.Value,
                            SRAnswerType, SRAnswerType, "a|i|u|e|o", "like", QuestionAnswerText,
                            0, QuestionAnswerNum, true, QuestionText, AnswerDecimalDigit,
                            AnswerSuffix, false, 0, 0, 0, 0, "", "", true);
        }

        private static bool IsAccepted(DateTime DateAssessment, string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd,
            string PatientSex, string Sex)
        {
            var Ret = GetAssessmentParsed(DateAssessment, AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd, 
            PatientSex, Sex, false);

            return (Ret.Count > 0);
        }

        private static string GetAssessmentParsedString(DateTime DateAssessment,
            string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd,
            string PatientSex, string Sex, string QuestionID)
        {
            var Ret = GetAssessmentParsed(DateAssessment, AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd,
            PatientSex, Sex, false);

            string Ass = string.Empty;
            string diagPrefixs = string.Empty;
            string diagSuffixs = string.Empty;

            // var Ass = new AssessmentAccepted

            foreach (var r in Ret)
            {
                var assWithLink = string.Format("<a href=\"../../../Module/RADT/Master/CarePlan/NursingAssessmentQuestion/NursingAssessmentQuestionDetail.aspx?md=view&id={0}\" target=\"_blank\">{1}</a>",
                    QuestionID, r.AssessmentName);
                Ass += ((Ass.Length == 0) ? "" : ", ") + assWithLink; // r.AssessmentName;
            }

            return Ass;
        }

        private static List<AssessmentRet> GetAssessmentParsedList(DateTime DateAssessment, 
            string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd,
            string PatientSex, string Sex, bool AlwaysReturn)
        {
            var Ret = GetAssessmentParsed(DateAssessment, AnswerTypeFromQuestion, AnswerTypeFromMatrix,
            AcceptedText, Operand, AnswerText,
            AcceptedNum, ansNum, AcceptedBool,
            QuestionText, RoundingDigit, AnswerSuffix,
            IsUsingRange, AcceptedNum2,
            PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd,
            PatientSex, Sex, AlwaysReturn);

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

        private static List<AssessmentRet> GetAssessmentParsed(DateTime DateAssessment, string AnswerTypeFromQuestion, string AnswerTypeFromMatrix,
            string AcceptedText, string Operand, string AnswerText,
            decimal AcceptedNum, decimal ansNum, bool AcceptedBool,
            string QuestionText, int RoundingDigit, string AnswerSuffix,
            bool IsUsingRange, decimal AcceptedNum2,
            int PatientAgeInMonth, int AgeInMonthStart, int AgeInMonthEnd,
            string PatientSex, string Sex, bool AlwaysReturn)
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
                case "RBT":
                case "CBL":
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
                    if (AnswerTypeFromQuestion == "CBT") {
                        // kalau pilihannya lain-lain maka buang lain2nya
                        if ((ansTexts[0]).ToLower() == "lain-lain") {
                            ansTexts = ansTexts.Where((source, index) => index != 0).ToArray();
                        }
                        else
                        {
                            sRet = QuestionText + " " + ansTexts[0];
                        }
                    }

                    sRet = ((AnswerTypeFromQuestion == "MEM") ? "" : QuestionText) + " " + ansTexts[0];
                    switch (Operand.ToLower())
                    {
                        case "=":
                            foreach (var at in AcceptedTexts)
                            {
                                if ((Equals(at.ToLower(), ansTexts[0].ToLower())
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd)
                                    && IsSexInRange(PatientSex, Sex))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret | true;

                                    var pAss = ParseAssessmentV2(sRet, at, AnswerTypeFromQuestion);
                                    if (pAss != null) l.AddRange(pAss);
                                }
                            }
                            break;
                        case "like":
                            foreach (var at in AcceptedTexts)
                            {
                                //if (ansTexts[0].IndexOf(at) >= 0 || AlwaysReturn)
                                if ((ContainsKeyWord(ansTexts[0], at)
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd)
                                    && IsSexInRange(PatientSex, Sex))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret | true;

                                    var pAss = ParseAssessmentV2(sRet, at, AnswerTypeFromQuestion);
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
                                    && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd)
                                    && IsSexInRange(PatientSex, Sex))
                                    || AlwaysReturn)
                                {
                                    // match
                                    ret = ret & true;

                                    var pAss = ParseAssessmentV2(sRet, at, AnswerTypeFromQuestion);
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
                case "DAT":
                    sRet = QuestionText + " " + ansTexts[0];
                    // 
                    if (IsUsingRange)
                    {
                        ret = CekDoubleDate(ansTexts[0], DateAssessment, AcceptedNum, AcceptedNum2, Operand);
                    }
                    else
                    {
                        ret = CekSigleDate(ansTexts[0], DateAssessment, AcceptedNum, Operand);
                    }
                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd) && IsSexInRange(PatientSex, Sex))
                        || AlwaysReturn)
                    {
                        var pAss = ParseAssessmentV2(sRet, sRet);
                        if (pAss != null) l.AddRange(pAss);
                    }
                    break;
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
                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd) && IsSexInRange(PatientSex, Sex))
                        || AlwaysReturn)
                    {
                        var pAss = ParseAssessmentV2(sRet, sRet);
                        if (pAss != null) l.AddRange(pAss);
                    }
                    break;
                case "CHK":
                case "CTX":
                case "CDT":
                    sRet = QuestionText + ((ansTexts[0].Trim() == "1") ? string.Empty : ":tidak");
                    if (ansTexts.Length > 1)
                    {
                        sRet = sRet + (string.Equals(ansTexts[1], string.Empty) ? string.Empty : " " + ansTexts[1] + "");
                    }
                    ret = ((ansTexts[0] == "1") == AcceptedBool);

                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd) && IsSexInRange(PatientSex, Sex))
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

                    if ((ret && IsAgeInRange(PatientAgeInMonth, AgeInMonthStart, AgeInMonthEnd) && IsSexInRange(PatientSex, Sex)) || AlwaysReturn)
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

        private static bool IsSexInRange(string PatientSex, string Sex)
        {
            return (Sex.Equals(string.Empty) || PatientSex.Equals(Sex));
        }

        private static bool ContainsKeyWord(string ansText, string keyWord)
        {
            ansText = ansText.ToLower(); keyWord = keyWord.ToLower();

            keyWord = keyWord.Replace("  ", " ");
            char[] splitter = { ' ' };
            var keyWords = keyWord.Split(splitter);
            var newKey = "";
            foreach (var key in keyWords)
            {
                ansText = ansText.Replace("f", "p").Replace("v","p");
                newKey = key.Replace("f", "p").Replace("v", "p");

                if (ansText.IndexOf(newKey) < 0) return false;
            }
            return true;
        }

        private static bool CekDoubleDate(string sVal, DateTime dateAssessment, decimal AcceptedNum, decimal AcceptedNum2, string Operand)
        {
            DateTime dVal;
            if (!DateTime.TryParse(sVal, out dVal)) return false;

            var time = dateAssessment - dVal;

            bool ret = false;
            switch (Operand.ToLower())
            {
                case ">&<":
                    ret = (time.Days > AcceptedNum) && (time.Days < AcceptedNum2);
                    break;
                case ">=&<=":
                    ret = (time.Days >= AcceptedNum) && (time.Days <= AcceptedNum2);
                    break;
                case "<|>":
                    ret = (time.Days < AcceptedNum) || (time.Days > AcceptedNum2);
                    break;
                case "<=|>=":
                    ret = (time.Days <= AcceptedNum) || (time.Days >= AcceptedNum2);
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        private static bool CekSigleDate(string sVal, DateTime dateAssessment, decimal AcceptedNum, string Operand)
        {
            DateTime dVal;
            if (!DateTime.TryParse(sVal, out dVal)) return false;

            var time = dateAssessment - dVal;

            bool ret = false;
            switch (Operand.ToLower())
            {
                case "=":
                    ret = (time.Days == AcceptedNum);
                    break;
                case ">":
                    ret = (time.Days > AcceptedNum);
                    break;
                case "<":
                    ret = (time.Days < AcceptedNum);
                    break;
                case ">=":
                    ret = (time.Days >= AcceptedNum);
                    break;
                case "<=":
                    ret = (time.Days <= AcceptedNum);
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
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
            public string QuestionID;
            //public string MainDiagnosaPrefix;
            //public string MainDiagnosaSuffix;
            //public string AdditionalDiagnosaPrefix;
            //public string AdditionalDiagnosaSuffix;

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

        private static AssessmentRet[] ParseAssessmentV2(string answer/*answertext*/, string key/*acceptedtext*/, 
            string AnswerType)
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

                // hilangkan jawaban "ya" untuk cbo
                if (AnswerType.Contains("CB")) {
                    foreach (var l in ls) l.AssessmentName = l.AssessmentName.ToLower().Replace(" ya", "");
                }

                foreach (var l in ls) l.AssessmentName = l.AssessmentName.Trim();

                lsRet.AddRange(ls);
            }
            return (lsRet.Count == 0) ? null : lsRet.ToArray();
        }

        private static AssessmentRet[] ParseAssessmentV2(string answer/*answertext*/, string key/*acceptedtext*/)
        {
            return ParseAssessmentV2(answer, key, string.Empty);
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
                pre += (pre.Length == 0 ? "": " ") + w.Trim();
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
        public static NursingDiagnosaEvaluationCollection DetailEvaluationByTransactionNo(string TransactionNo)
        {
            var deColl = new NursingDiagnosaEvaluationCollection();
            var de = new NursingDiagnosaEvaluationQuery("de");
            var e = new NursingDiagnosaTransDTQuery("e");
            de.InnerJoin(e).On(de.EvaluationID == e.ID)
                .Where(e.TransactionNo == TransactionNo)
                .Select(de);
            deColl.Load(de);
            return deColl;
        }
        public static NursingDiagnosaTransDTCollection DetailEvaluationByEvaluationID(Int64 EvaluationID)
        {
            var deColl = new NursingDiagnosaTransDTCollection();
            var e = new NursingDiagnosaTransDTQuery("e");
            var d = new NursingDiagnosaQuery("d");
            var de = new NursingDiagnosaEvaluationQuery("de");
            var nType = new AppStandardReferenceItemQuery("nType");

            e.InnerJoin(d).On(e.NursingDiagnosaID == d.NursingDiagnosaID)
                .InnerJoin(de).On(de.InterventionID == e.ID)
                .LeftJoin(nType).On(nType.StandardReferenceID == "SRNursingNicType" && d.SRNursingNicType == nType.ItemID)
                .Where(de.EvaluationID == EvaluationID)
                .Select(e,
                    d.F1.As("refTo_F1"),
                    d.F2.As("refTo_F2"),
                    nType.ItemName.As("refTo_SRNursingNicType"),
                    de.SRNursingCarePlanning.As("refToEval_SRNursingCarePlanning")
                );
            deColl.Load(e);
            return deColl;
        }
        public static string DetailEvaluationByEvaluationIdHtml(Int64 EvaluationID) {
            var nics = "";
            var xnic = DetailEvaluationByEvaluationID(EvaluationID);
            string[] nicts = xnic.Select(x => x.refToSRNursingNicType).ToArray().Distinct().ToArray();
            string[] f2s = xnic.Select(x => x.RefToF2).ToArray().Distinct().ToArray();
            string[] f1s = xnic.Select(x => x.RefToF1).ToArray().Distinct().ToArray();

            foreach (var nict in nicts)
            {
                string sf3 = "";
                foreach (var f2 in f2s)
                {
                    string sf2 = "";
                    foreach (var f1 in f1s)
                    {
                        string sf1 = "";
                        foreach (var xn in xnic.Where(x => x.refToSRNursingNicType == nict && x.RefToF2 == f2 && x.RefToF1 == f1))
                        {
                            sf1 += string.Format("<li class='linicnoc {2}'>{0}{1}</li>", xn.NursingDiagnosaName,
                                (xn.refToEvalSRNursingCarePlanning == "03" ?
                                    string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/new_tag_16.png") :
                                    (xn.refToEvalSRNursingCarePlanning == "01" ? string.Format("<img src=\"{0}\" />", "[BASEURL]/Images/Toolbar/stop_tag_16.png") : "")),
                                xn.refToEvalSRNursingCarePlanning == "01" ? ("linichide linichide" + EvaluationID.ToString()) :"");
                        }
                        if (sf1.Length > 0)
                        {
                            sf1 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", f1, sf1);
                            sf2 += sf1;
                        }
                    }
                    if (sf2.Length > 0)
                    {
                        sf2 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", f2, sf2);
                        sf3 += sf2;
                    }
                }
                if (sf3.Length > 0)
                {
                    sf3 = string.Format("<li class='linicnoc'>{0}<ul>{1}</ul></li>", nict, sf3);
                    nics += sf3;
                }
            }
            if (nics.Length > 0)
            {
                nics = string.Format("<ul>{0}{1}</ul>", nics, nics.Contains("nichide") ? 
                    string.Format("<li class='linicnoc linicshow linicshow{0}'><a href=\"javascript:void(0);\" onclick=\"EvalShowMoreClick('{0}');\">Show More...</li>", EvaluationID) :"");
            }
            return nics;
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

        private static string QuestionReportDisplayV2(string QuestionText, string SRAnswerType,
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
                case "TIM":
                case "MEM":
                case "TXT":
                case "CBO":
                case "RBT":
                case "ABY":
                    {
                        sQuestion = QuestionText;
                        sAnswer = QuestionAnswerText;
                        break;
                    }
                case "DTM":
                case "DAT":
                case "ADT":
                    {
                        sQuestion = QuestionText;
                        if (!string.IsNullOrEmpty(QuestionAnswerText)) { 
                            if (SRAnswerType == "DAT")
                                sAnswer = Convert.ToDateTime(QuestionAnswerText).ToString("dd-MMM-yyyy");
                            else
                                sAnswer = Convert.ToDateTime(QuestionAnswerText).ToString("dd-MMM-yyyy HH:mm");
                        }
                        break;
                    }
                case "NUM":
                    {
                        if (!QuestionAnswerNum.Equals("&nbsp;"))
                        {
                            sQuestion = QuestionText;
                            if (string.IsNullOrEmpty(QuestionAnswerNum))
                                QuestionAnswerNum = "0";
                            sAnswer = Convert.ToDouble(QuestionAnswerNum).ToString() + AnswerSuffix;
                        }
                        break;
                    }
                case "TTX":
                case "CB2":
                case "CBT":
                    {
                        sAnswer = QuestionText + " : " + QuestionAnswerText;
                        break;
                    }
                case "CBM":
                case "CBN":
                    {
                        string[] txtNat = QuestionAnswerText.Split(new char[] { '|' });
                        sQuestion = QuestionText;
                        sAnswer = ((txtNat.Length > 0 ? txtNat[0] : "") + " " +
                            ((txtNat.Length > 1 && !(new List<string>() { "tidak", "no" }).Contains(txtNat[0].ToLower().Trim())) ? txtNat[1] + AnswerSuffix : "")).Trim();
                        break;
                    }
                case "CBL":
                    {  
                        sAnswer = QuestionText + " : " +  QuestionAnswerText;
                        break;
                    }
                case "CHK":
                case "CTX":
                case "CTM":
                case "CNM":
                case "CDT":
                    {
                        string[] txtNat = QuestionAnswerText.Split(new char[] { '|' });
                        sAnswer = QuestionText + " " + ((txtNat.Length > 1) ? ((txtNat[1]).Trim() + AnswerSuffix) : string.Empty);
                        break;
                    }
                case "TBL":
                    {
                        sAnswer = QuestionAnswerText;
                        break;
                    }
                case "IMG": 
                    {
                        sAnswer = QuestionAnswerText;
                        break;
                    }
                case "SIG":
                    {
                        sAnswer = QuestionText;
                        break;
                    }
                default:
                    {
                        sAnswer = QuestionText + " : " +  QuestionAnswerText;
                        //sAnswer = "Not Yet Implemented";
                        break;
                    }
            }

            row["SAnswer"] = sAnswer;
            row["QuestionText"] = sQuestion;

            return FormatQuestionTextForReportDisplay(sQuestion, sAnswer, IsAlwaysPrint);
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
                case "RBT":
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
                    {
                        sAnswer = QuestionAnswerText;
                        break;
                    }
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


        public static DataTable ReportDataSourceGeneralV2(string transactionNo, string registrationNo,
           string questionFormId)
        {
            var qig = new QuestionInGroupQuery("qig");
            var q = new QuestionQuery("q");
            var qg = new QuestionGroupQuery("qg");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var phr = new PatientHealthRecordQuery("phr");
            var qgif = new QuestionGroupInFormQuery("qgif");

            qig.LeftJoin(phrl).On(qig.QuestionID == phrl.QuestionID && phrl.TransactionNo == transactionNo &&
                    phrl.RegistrationNo == registrationNo)
                .InnerJoin(q).On(qig.QuestionID == q.QuestionID)
                .LeftJoin(phr).On(phrl.TransactionNo == phr.TransactionNo &&
                    phrl.RegistrationNo == phr.RegistrationNo && phrl.QuestionFormID == phr.QuestionFormID)
                .InnerJoin(qgif).On(qgif.QuestionGroupID == qig.QuestionGroupID)
                .InnerJoin(qg).On(qig.QuestionGroupID == qg.QuestionGroupID)
                .Where(
                    qgif.QuestionFormID == questionFormId
                ).Select(q.QuestionID,
                    "<ISNULL(qig.ParentQuestionID,q.ParentQuestionID) ParentQuestionID>",
                    q.SRAnswerType, q.AnswerWidth, q.QuestionAnswerSelectionID,
                    phr.RecordDate, "<ISNULL(qig.QuestionLevel,q.QuestionLevel) QuestionLevel>",
                    qg.QuestionGroupName, q.QuestionText, phrl,
                    qgif.RowIndex, qig.RowIndex,
                    "<'' TextToDisplay>", "<ISNULL(q.IsAlwaysPrint,0) IsAlwaysPrint>",
                    "<'' TblPrintAsGroup>", "<'' SAnswer>",
                    "< CASE WHEN q.SRAnswerType = 'TXT' AND phrl.QuestionAnswerText = '' THEN 'HAPUS' WHEN q.SRAnswerType = 'NUM' AND phrl.QuestionAnswerNum = NULL THEN 'HAPUS' ELSE 'ISI' END  KOSONG >",
                    "<'' Deleted>"
                ).OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending);

            var dt = qig.LoadDataTable();

            string[] str = { "CHK", "CTX", "CTM" };
            foreach (System.Data.DataRow r in dt.Rows)
            {
                r["TextToDisplay"] = QuestionReportDisplayV2(
                    r["QuestionText"].ToString(), r["SRAnswerType"].ToString(),
                    r["QuestionAnswerText"].ToString(), r["QuestionAnswerNum"].ToString(),
                    r["QuestionAnswerSuffix"].ToString(), (bool)r["IsAlwaysPrint"], r);

                // buang chk yang tidak dicentang dan alwayprint 0 atau null
                if (string.IsNullOrEmpty(r["TextToDisplay"].ToString()))
                {
                    r["Deleted"] = "DELETE";

                    r.Delete();
                }
            }

            dt.AcceptChanges();

            FormatTable1(dt);
            FormatTable2(dt);

            return dt;
        }


        public static DataTable ReportDataSourceGeneral(string transactionNo, string registrationNo,
            string questionFormId)
        {

            //var phrl = new PatientHealthRecordLineQuery("phrl");
            //var q = new QuestionQuery("q");
            //var phr = new PatientHealthRecordQuery("phr");
            //var qig = new QuestionInGroupQuery("qig");
            //var qgif = new QuestionGroupInFormQuery("qgif");
            //var qg = new QuestionGroupQuery("qg");

            //phrl.InnerJoin(q).On(phrl.QuestionID == q.QuestionID)
            //        .InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo &&
            //        phrl.RegistrationNo == phr.RegistrationNo && phrl.QuestionFormID == phr.QuestionFormID)
            //        .InnerJoin(qig).On(q.QuestionID == qig.QuestionID)
            //        .InnerJoin(qgif).On(qgif.QuestionGroupID == qig.QuestionGroupID)
            //        .InnerJoin(qg).On(qig.QuestionGroupID == qg.QuestionGroupID)
            //        .Where(
            //            phrl.TransactionNo == transactionNo,
            //            phrl.RegistrationNo == registrationNo,
            //            qgif.QuestionFormID == questionFormId
            //        ).Select(q.QuestionID,
            //            "<ISNULL(qig.ParentQuestionID,q.ParentQuestionID) ParentQuestionID>",
            //            q.SRAnswerType, q.AnswerWidth, q.QuestionAnswerSelectionID,
            //            phr.RecordDate, "<ISNULL(qig.QuestionLevel,q.QuestionLevel) QuestionLevel>",
            //            qg.QuestionGroupName, q.QuestionText, phrl,
            //            qgif.RowIndex, qig.RowIndex,
            //            "<'' TextToDisplay>", "<ISNULL(q.IsAlwaysPrint,0) IsAlwaysPrint>",
            //            "<'' TblPrintAsGroup>", "<'' SAnswer>"
            //        ).OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending);

            //var dt = phrl.LoadDataTable();

            //var qgif = new QuestionGroupInFormQuery("qgif");
            //var qig = new QuestionInGroupQuery("qig");
            //var q = new QuestionQuery("q");
            //var qg = new QuestionGroupQuery("qg");
            //var phrl = new PatientHealthRecordLineQuery("phrl");
            //var phr = new PatientHealthRecordQuery("phr");


            //qgif.InnerJoin(qig).On(qgif.QuestionGroupID == qig.QuestionGroupID && qgif.QuestionFormID == questionFormId)
            //    .InnerJoin(q).On(q.QuestionID == qig.QuestionID)
            //    .InnerJoin(qg).On(qg.QuestionGroupID == qgif.QuestionGroupID)
            //    .InnerJoin(phr).On(phr.TransactionNo == transactionNo && phr.RegistrationNo == registrationNo && phr.QuestionFormID == questionFormId)
            //    .LeftJoin(phrl).On(phrl.TransactionNo == phr.TransactionNo && 
            //        phrl.RegistrationNo == phr.RegistrationNo && phrl.QuestionFormID == phr.QuestionFormID &&
            //        phrl.QuestionID == qig.QuestionID)
            //    .Select(q.QuestionID,
            //            "<ISNULL(qig.ParentQuestionID,q.ParentQuestionID) ParentQuestionID>",
            //            q.SRAnswerType, q.AnswerWidth, q.QuestionAnswerSelectionID,
            //            phr.RecordDate, "<ISNULL(qig.QuestionLevel,q.QuestionLevel) QuestionLevel>",
            //            qg.QuestionGroupName, q.QuestionText, phrl,
            //            qgif.RowIndex, qig.RowIndex,
            //            "<'' TextToDisplay>", "<ISNULL(q.IsAlwaysPrint,0) IsAlwaysPrint>",
            //            "<'' TblPrintAsGroup>", "<'' SAnswer>"
            //        ).OrderBy(qgif.RowIndex.Ascending, qig.RowIndex.Ascending);

            //var dt = qgif.LoadDataTable();

            var dt = (new PatientHealthRecordCollection()).GetPHRLforReport(transactionNo, registrationNo, questionFormId);

            string[] str = { "CHK", "CTX", "CTM","CBL" };
            foreach (System.Data.DataRow r in dt.Rows)
            {
                r["TextToDisplay"] = QuestionReportDisplay(
                    r["QuestionText"].ToString(), r["SRAnswerType"].ToString(),
                    r["QuestionAnswerText"].ToString(), r["QuestionAnswerNum"].ToString(),
                    r["QuestionAnswerSuffix"].ToString(), (bool)r["IsAlwaysPrint"], r);


                // buang chk yang tidak dicentang dan alwayprint 0 atau null
                if (string.IsNullOrEmpty(r["TextToDisplay"].ToString()))
                {
                    if(r["SRAnswerType"].ToString() != "TBL")
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

        //private static void FormatImage(Image img)
        //{

        //}

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

    public class NursingDiagnosaTransDTNocTarget : NursingDiagnosaTransDT
    {
        public long IdDiagTmp;
    }

    public partial class NursingDiagnosaTransDTCollection
    {
        public override void Save()
        {
            foreach (var entity in this)
            {
                if (entity.es.IsAdded)
                {
                    entity.str.SRUserType = string.Empty;

                    if (string.IsNullOrEmpty(entity.CreateByUserID))
                    {
                        var userLogin = new UserLogin();
                        if (HttpContext.Current.Session["_UserLogin"] != null)
                        {
                            userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                            entity.SRUserType = userLogin.SRUserType;
                            entity.CreateByUserID = userLogin.UserID;
                        }
                    }
                    else
                    {
                        var user = new AppUser();
                        user.LoadByPrimaryKey(entity.CreateByUserID);
                        entity.SRUserType = user.SRUserType;
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
            var q = new NursingDiagnosaTransDTQuery("a");
            var au = new AppUserQuery("b");
            q.InnerJoin(au).On(q.LastUpdateByUserID.Equal(au.UserID))
                .Where(q.TransactionNo.Equal(TransactionNo),
                q.SRNursingDiagnosaLevel.In(new string[] { "31", "32" }))
                .Select(q, au.UserName.As("refTo_UserName"));
            q.es.PageNumber = iPageNumber;
            q.es.PageSize = iPageSize;
            q.OrderBy(q.ExecuteDateTime.Descending);

            return this.Load(q);
        }

        public DataTable ImplementationCountPerNIC(string TransactionNo)
        {
            var str = @"SELECT a.ID, COUNT(b.ID) ImpCount
                        FROM NursingDiagnosaTransDT a
	                        INNER JOIN NursingDiagnosaTransDT b ON b.ParentID = a.ID
                        WHERE a.TransactionNo = '" + TransactionNo + "' AND a.SRNursingDiagnosaLevel = '30' GROUP BY a.ID";
            return FillDataTable(esQueryType.Text, str);
        }

        public DataTable ImplementationByPage(string[] RegistrationNos, bool HasRespondOnly, int iRowStart, int iRowFinish)
        {
            var sRegNos = "";
            foreach (var r in RegistrationNos) {
                sRegNos += (string.IsNullOrEmpty(sRegNos) ? "" : ",") + string.Format("'{0}'", r);
            }

            var str = @"SELECT x.*, '' Respond2, au.UserName RefToUserName FROM (
	                        SELECT ROW_NUMBER() OVER (ORDER BY ExecuteDateTime DESC) rn, ndtd.*, hd.RegistrationNo
	                        FROM NursingDiagnosaTransDT AS ndtd 
                                INNER JOIN NursingTransHD as hd on ndtd.TransactionNo = hd.TransactionNo
	                        WHERE hd.RegistrationNo IN ("+ sRegNos + @") 
                                AND ISNULL(ndtd.IsDeleted, 0) = 0 AND ndtd.SRNursingDiagnosaLevel IN ('31','32') " +
                                (HasRespondOnly ? " AND (ndtd.Respond <> '' OR ISNULL(ndtd.ReferenceToPhrNo, '') <> '') " : "") + @"
                        ) x LEFT JOIN AppUser AS au ON x.CreateByUserID = au.UserID
                        WHERE x.rn BETWEEN " + iRowStart + " AND " + iRowFinish;
            return FillDataTable(esQueryType.Text, str);
        }

        public int ImplementationCount(string[] RegistrationNos, bool HasRespondOnly)
        {
            var sRegNos = "";
            foreach (var r in RegistrationNos)
            {
                sRegNos += (string.IsNullOrEmpty(sRegNos) ? "" : ",") + string.Format("'{0}'", r);
            }

            var str = @"SELECT COUNT(ID) 
	                        FROM NursingDiagnosaTransDT AS ndtd 
                                INNER JOIN NursingTransHD as hd on ndtd.TransactionNo = hd.TransactionNo
	                        WHERE hd.RegistrationNo IN (" + sRegNos + @") 
                                AND ISNULL(ndtd.IsDeleted, 0) = 0 and SRNursingDiagnosaLevel IN ('31','32')" +
                                (HasRespondOnly ? " AND (ndtd.Respond <> '' OR ISNULL(ndtd.ReferenceToPhrNo, '') <> '') " : "");
            DataTable dt = FillDataTable(esQueryType.Text, str);
            return (int)dt.Rows[0][0];
        }

        public DataTable ImplementationCountPerIntervention(string TransactionNo)
        {
            var str = @"SELECT l30.NursingDiagnosaID, COUNT(l31.ID) cID31
                        FROM (
	                        select ID, NursingDiagnosaID from NursingDiagnosaTransDT
	                        WHERE TransactionNo = '" + TransactionNo + @"'
		                        AND SRNursingDiagnosaLevel = '30'
	                        UNION ALL
	                        SELECT 0, '' NursingDiagnosaID
                        ) AS l30
                        LEFT JOIN (
	                        SELECT l31_1.ID, ISNULL(l31_1.ParentID, 0) ParentID
	                        FROM NursingDiagnosaTransDT AS l31_1
	                        WHERE l31_1.TransactionNo = '" + TransactionNo + @"'
		                        AND l31_1.SRNursingDiagnosaLevel = '31'
                        ) AS l31 ON l30.ID = l31.ParentID 

                        GROUP BY l30.NursingDiagnosaID";
            return FillDataTable(esQueryType.Text, str);
        }

        public DataTable PhrGetDataByIdTemplateImplementation(string RegistrationNo, string TemplateID)
        {
            var str = @"SELECT
	                        phr.RecordDate, phr.RecordTime, phrl.QuestionID, q.QuestionText, phrl.QuestionAnswerText, 
	                        phrl.QuestionAnswerNum, phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix, q.SRAnswerType, ndtd2.RowIndex,
                            q.AnswerDecimalDigit
                        FROM NursingTransHD AS nth 
	                        INNER JOIN NursingDiagnosaTransDT AS ndtd ON nth.TransactionNo = ndtd.TransactionNo
	                        INNER JOIN PatientHealthRecord AS phr ON ndtd.ReferenceToPhrNo = phr.TransactionNo
	                        INNER JOIN PatientHealthRecordLine AS phrl ON phrl.TransactionNo = phr.TransactionNo AND phrl.RegistrationNo = phr.RegistrationNo
	                        INNER JOIN Question AS q ON q.QuestionID = phrl.QuestionID
	                        LEFT JOIN NursingDiagnosaTemplateDetail AS ndtd2 ON ndtd2.QuestionID = q.QuestionID AND ndtd.TemplateID = ndtd2.TemplateID
                        WHERE nth.RegistrationNo = '" + RegistrationNo + @"'
	                        AND ndtd.TemplateID = " + TemplateID + @" 
                            AND ISNULL(ndtd.IsDeleted, 0) = 0
                            AND ndtd2.RowIndex IS NOT NULL 
                        ORDER BY phr.RecordDate, phr.RecordTime, ndtd2.RowIndex";
            return FillDataTable(esQueryType.Text, str);
        }
    }
}
