using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
namespace Temiang.Avicenna.Common
{
    public class NutritionCare
    {
        #region General Private
        private static void SaveDiagnosa(NutritionCareDiagnoseTransDTCollection dtDiag)
        {
            // == STEP 1 
            using (esTransactionScope trans = new esTransactionScope())
            {
                dtDiag.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
            // == STEP 2
            // update etiologi ParentID
            // update etiology yang belum ada ParentID
            var newEti = dtDiag.Where(x => x.SRNutritionCareTerminologyLevel == "11" && !x.ParentID.HasValue);
            foreach (var eti in newEti)
            {
                eti.ParentID = (
                    from d in dtDiag
                    where d.TerminologyID == eti.TerminologyParentID
                    select d.ID).First();
            }
            // update nic ParentID
            // update nic yang belum ada ParentID
            var newNic = dtDiag.Where(x => x.SRNutritionCareTerminologyLevel == "30" && !x.ParentID.HasValue);
            foreach (var nic in newNic)
            {
                nic.ParentID = (
                    from d in dtDiag
                    where d.TerminologyID == nic.TerminologyParentID
                    select d.ID).First();
            }
            using (esTransactionScope trans = new esTransactionScope())
            {
                dtDiag.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        #endregion
        #region General Public
        public static string PopulateNewTransNo(bool TobeSave)
        {
            AppAutoNumberLast _autoNumberND;
            _autoNumberND = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NutritionCareNo);
            var GeneratedNo = _autoNumberND.LastCompleteNumber;
            if (TobeSave)
            {
                // save autonumber immediately to decrease time gap between create and save
                _autoNumberND.Save();
            }
            return GeneratedNo;
        }
        public static NutritionCareTransHD SetTransHD(string RegistrationNo, string userID)
        {
            NutritionCareTransHD hd;
            var hdColl = GetTransHD(RegistrationNo);

            if (hdColl.Count == 0)
            {
                //hd = new NutritionCareTransHD();
                hd = hdColl.AddNew();
                //hd.AddNew();
                var GeneratedNo = PopulateNewTransNo(true);


                hd.TransactionNo = GeneratedNo;
                hd.NutritionCareTransDateTime = DateTime.Now;
                hd.RegistrationNo = RegistrationNo;
                //Last Update Status

                hd.CreateByUserID = userID;
                hd.CreateDateTime = DateTime.Now;
                hd.LastUpdateByUserID = userID;
                hd.LastUpdateDateTime = DateTime.Now;
            }
            else
            {
                hd = hdColl[0];

                hd.LastUpdateByUserID = userID;
                hd.LastUpdateDateTime = DateTime.Now;


            }
            hdColl.Save();
            return hd;
        }
        public static NutritionCareTransHDCollection GetTransHD(string RegistrationNo)
        {
            var hdColl = new NutritionCareTransHDCollection();
            hdColl.Query.Where(hdColl.Query.RegistrationNo == RegistrationNo);
            hdColl.LoadAll();
            return hdColl;
        }
        public static List<long?> FindAllRelatedChild(NutritionCareDiagnoseTransDTCollection dtDiag, List<long?> parents)
        {
            var c1 = parents.Count;
            var res = (dtDiag.Where(x => parents.Contains(x.ParentID) && !parents.Contains(x.ID) && x.SRNutritionCareTerminologyLevel != "10")
                .Select(x => x.ID)).ToList();
            if (res.Count == 0) return parents;
            parents.AddRange(res);
            return FindAllRelatedChild(dtDiag, parents);
        }
        public static NutritionCareDiagnoseTransDTCollection RemoveClosedDiagnosaAndChildRelated(NutritionCareDiagnoseTransDTCollection dtDiag)
        {
            // buang yang Diagnosanya sudah CLoseD
            List<long?> idToDetach;
            idToDetach = (dtDiag.Where(x => x.SRNutritionCareTerminologyLevel == "10")
                .Select(x => x.ID)).ToList();
            idToDetach = Common.NutritionCare.FindAllRelatedChild(dtDiag, idToDetach);

            foreach (var d in dtDiag)
            {
                if (idToDetach.Contains(d.ID)) dtDiag.DetachEntity(d);
            }
            return dtDiag;
        }
        #endregion
        #region Nutrition Care Problem
        public static void SaveDiagL11(string RegistrationNo, NutritionCareDiagnoseTransDT[] selectedProb)
        {
            var hd = Common.NutritionCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);

            var dtDiag = new NutritionCareDiagnoseTransDTCollection();
            dtDiag.Query.Where(dtDiag.Query.TransactionNo == hd.TransactionNo,
                dtDiag.Query.SRNutritionCareTerminologyLevel.In(new string[] { "10", "11" }));
            dtDiag.LoadAll();

            dtDiag = RemoveClosedDiagnosaAndChildRelated(dtDiag);

            SetProblem(dtDiag, selectedProb, hd); // common called as etiology
            SaveDiagnosa(/*hd, */dtDiag);
        }
        private static void SetProblem(NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareDiagnoseTransDT[] selectedProb,
            NutritionCareTransHD hd)
        {
            #region Problem
            // Populate Selected Problem
            foreach (NutritionCareDiagnoseTransDT na in selectedProb)
            {
                // cari etiology yang diagnosanya belum close, ada atau tidak.
                // kalau ada pake yang itu saja, tapi kalau tidak ada maka bikin baru
                var lNa = (from d in dtDiag
                           join e in dtDiag on d.ParentID equals e.ID
                           where d.TerminologyID == na.TerminologyID
                           select d);
                if (lNa.Count() == 1)
                {
                    // sudah ada, cek ada perubahan atau tidak
                    var n = lNa.First();
                    if (n.TerminologyName != na.TerminologyName)
                    {
                        n.TerminologyName = na.TerminologyName;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.TerminologyID = na.TerminologyID;
                    n.TerminologyName = na.TerminologyName;
                    n.SRNutritionCareTerminologyLevel = "11";
                    n.TerminologyParentID = na.TerminologyParentID;
                    n.ExecuteDateTime = DateTime.Now;

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.S = na.S ?? string.Empty;
                    n.O = na.O ?? string.Empty;

                    n.TmpTerminologyID = na.TmpTerminologyID ?? string.Empty;

                }
            }
            #endregion
            #region Diagnosa
            // Diagnosa (diambil dari problem header
            List<string> NewSelectedDiag = new List<string>();

            var qdiag = new NutritionCareTerminologyQuery();
            if (selectedProb.Length == 0)
            {
                qdiag.Where(qdiag.TerminologyID == string.Empty);
            }
            else
            {
                qdiag.Where(qdiag.TerminologyID.In((from t in selectedProb select t.TerminologyID)));
            }
            qdiag.Select(qdiag.TerminologyParentID);
            qdiag.es.Distinct = true;
            var ldt = qdiag.LoadDataTable();
            foreach (System.Data.DataRow row in ldt.Rows)
            {
                NewSelectedDiag.Add(row[0].ToString());
            }

            NutritionCareTerminologyCollection parentColl = new NutritionCareTerminologyCollection();
            if (NewSelectedDiag.Count == 0)
            {
                parentColl.Query.Where(parentColl.Query.TerminologyID == string.Empty);
            }
            else
            {
                parentColl.Query.Where(parentColl.Query.TerminologyID.In(NewSelectedDiag));
            }

            parentColl.LoadAll();

            foreach (var na in parentColl)
            {
                if ((from d in dtDiag
                     where d.TerminologyID == na.TerminologyID
                     select d).Count() == 0)
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.TerminologyID = na.TerminologyID;
                    n.TerminologyName = na.TerminologyName;
                    n.SRNutritionCareTerminologyLevel = "10";
                    n.TerminologyParentID = na.TerminologyParentID;
                    n.ExecuteDateTime = DateTime.Now;/*ExecuteDateTime dipakai untuk tanggal terakhir evaluasi*/

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.TmpTerminologyID = (from dx in dtDiag where dx.TerminologyParentID == n.TerminologyID select dx.TmpTerminologyID).First();
                }
            }
            #endregion
        }
        #endregion
        #region Nutrition Care Diagnosa
        public static void SaveDiagL10(string RegistrationNo, NutritionCareDiagnoseTransDT[] selectedDiag,
            NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareTransHD hd)
        {
            //SetDiagnosa(dtDiag, selectedDiag); // common called as etiology
            SaveDiagnosa(/*hd, */dtDiag);
        }
        //private static void SetDiagnosa(NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareDiagnoseTransDT[] selectedDiag)
        //{
        //    // only update
        //    foreach (var na in selectedDiag)
        //    {
        //        if ((from d in dtDiag where d.ID == na.ID select d).Count() == 1)
        //        {
        //            var n = dtDiag.Where(x => x.ID == na.ID).First();
        //            if (n.SRNutritionCarePlanning != "01"/*stop*/)
        //            {
        //                n.Priority = (from ip in selectedDiag where ip.ID == na.ID select ip.Priority).FirstOrDefault() ?? 0;
        //                n.EvalPeriod = (from ip in selectedDiag where ip.ID == na.ID select ip.EvalPeriod).FirstOrDefault() ?? 0;
        //                n.PeriodConversionInHour = (from ip in selectedDiag where ip.ID == na.ID select ip.PeriodConversionInHour).FirstOrDefault() ?? 24;

        //                n.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                n.LastUpdateDateTime = DateTime.Now;
        //            }
        //        }
        //    }
        //}
        #endregion
        
        #region Nutrition Care Planning (NIC)
        public static void SaveDiagL30(string RegistrationNo, NutritionCareDiagnoseTransDT[] selectedNIC,
            NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareTransHD hd, long idL10)
        {
            SetNIC(dtDiag, selectedNIC, hd, idL10, false);
            SaveDiagnosa(/*hd, */dtDiag);
        }
        private static void SetNIC(NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareDiagnoseTransDT[] selectedNIC,
            NutritionCareTransHD hd, long idL10, bool SkipDelete)
        {
            // TODO: harusnya disini intervensi yang sudah close jangan diganggu lagi,
            // kalau ada intervensi yang sama harusnya add lagi.
            #region NIC
            foreach (NutritionCareDiagnoseTransDT na in selectedNIC)
            {
                var lNa = (from d in dtDiag
                           where d.TerminologyID == na.TerminologyID & d.ParentID == idL10
                           select d);
                if (lNa.Count() == 1)
                {
                    // sudah ada, cek ada perubahan atau tidak
                    var n = lNa.First();
                    if (n.TerminologyName != na.TerminologyName)
                    {
                        n.TerminologyName = na.TerminologyName;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.TerminologyID = na.TerminologyID;
                    n.TerminologyName = na.TerminologyName;
                    n.SRNutritionCareTerminologyLevel = "30";
                    n.TerminologyParentID = na.TerminologyParentID;
                    n.ExecuteDateTime = DateTime.Now;

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.ParentID = na.ParentID;
                }
            }
            #endregion

            #region eliminate unselected
            if (!SkipDelete)
            {
                var tdlDiag = dtDiag.Where(x => !selectedNIC.Select(f => f.TerminologyID)
                    .Contains(x.TerminologyID)
                    && ((new string[] { "30" }).Contains(x.SRNutritionCareTerminologyLevel))
                    && x.ParentID == idL10
                );

                foreach (var s in tdlDiag)
                {
                    s.MarkAsDeleted();
                }
            }
            #endregion
        }
        #endregion
        #region Nutrition Care Implementation
        #endregion
        #region Nutrition Care Evaluation
        public static void SaveDiagL40(NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareDiagnoseTransDT Evaluation,
            NutritionCareDiagnoseTransDTCollection NutritionCareEvaluationIntervention, NutritionCareTransHD hd, long idL10)
        {
            SetEvaluation(dtDiag, Evaluation, NutritionCareEvaluationIntervention, hd, idL10);
            //SaveEvaluation();
            using (esTransactionScope trans = new esTransactionScope())
            {
                dtDiag.Save();
                Evaluation.Save();

                // insert detail evaluation
                var dEvaColl = NutritionCareDiagnoseTransDT.DetailEvaluation(hd.TransactionNo);
                // dikali intervensi yang ada
                var intrvs = dtDiag.Where(x => x.ParentID == Evaluation.ParentID && x.SRNutritionCareTerminologyLevel == "30");
                foreach (var intrv in intrvs)
                {
                    var EvsI = dEvaColl.AddNew();
                    EvsI.EvaluationID = Evaluation.ID;
                    EvsI.InterventionID = intrv.ID;
                    EvsI.NutritionCareInterventionID = intrv.TerminologyID;
                    EvsI.CreateByUserID = AppSession.UserLogin.UserID;
                    EvsI.CreateDateTime = DateTime.Now;
                }

                //using (esTransactionScope trans = new esTransactionScope())
                //{
                dEvaColl.Save();

                trans.Complete();
            }
        }

        public static void SetEvaluation(NutritionCareDiagnoseTransDTCollection dtDiag, NutritionCareDiagnoseTransDT Evaluation,
            NutritionCareDiagnoseTransDTCollection NutritionCareEvaluationIntervention, NutritionCareTransHD hd, long idL10)
        {
            // jika ada tambahan intervensi set disini
            List<NutritionCareDiagnoseTransDT> newIntvs = new List<NutritionCareDiagnoseTransDT>();
            foreach (var i in NutritionCareEvaluationIntervention)
            {
                var oldIntv = dtDiag.Where(x => x.SRNutritionCareTerminologyLevel == "30" &&
                    x.ParentID == i.ParentID && x.TerminologyID == i.TerminologyID);
                if (oldIntv.Count() > 0)
                {
                    //// jika sudah ada maka update status terakhirnya
                    //foreach (var oi in oldIntv)
                    //{
                    //    // yang sudah pernah ada berarti diteruskan / stop
                    //    oi.SRNutritionCarePlanning = (i.SRNutritionCarePlanning == "01") ?
                    //        "01" /*stop*/: "02"/*diteruskan*/;
                    //    //oi.SRNutritionCarePlanning = i.SRNutritionCarePlanning;
                    //}
                }
                else
                {
                    newIntvs.Add(i);
                }
            }
            // tambahan intervensi
            if (newIntvs.Count > 0)
                SetNIC(dtDiag, newIntvs.ToArray(), hd, idL10, true);

            // evaluasi harus update headernya
            if (Evaluation.es.IsAdded)
            {
                var d = dtDiag.Where(x => x.ID == Evaluation.ParentID).First();
                // update juga tanggalnya
                d.ExecuteDateTime = DateTime.Now;/*nanti tambah field sendiri aja*/
            }
        }

        #endregion
    }
}
