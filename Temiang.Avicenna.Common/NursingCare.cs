using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class NursingCare
    {
        #region General Private
        private static void SaveDiagnosa(NursingDiagnosaTransDTCollection dtDiag)
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
            var newEti = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "11" && !x.ParentID.HasValue);
            foreach (var eti in newEti)
            {
                eti.ParentID = (
                    from d in dtDiag
                    where d.NursingDiagnosaID == eti.NursingDiagnosaParentID &&
                        d.SRNursingCarePlanning != "01"
                    select d.ID).First();
            }
            // update noc ParentID
            // update noc yang belum ada ParentID
            var newNoc = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "20" && !x.ParentID.HasValue);
            foreach (var noc in newNoc)
            {
                noc.ParentID = (
                    from d in dtDiag
                    where d.NursingDiagnosaID == noc.NursingDiagnosaParentID &&
                        d.SRNursingCarePlanning != "01"
                    select d.ID).First();
            }
            // update nic ParentID
            // update nic yang belum ada ParentID
            var newNic = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "30" && !x.ParentID.HasValue);
            foreach (var nic in newNic)
            {
                nic.ParentID = (
                    from d in dtDiag
                    where d.NursingDiagnosaID == nic.NursingDiagnosaParentID &&
                        d.SRNursingCarePlanning != "01"
                    select d.ID).First();
            }
            using (esTransactionScope trans = new esTransactionScope())
            {
                dtDiag.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            // == STEP 3
            // update noc detail
            // update noc detail yang belum ada ParentID
            var newNocD = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "21" && !x.ParentID.HasValue);
            foreach (var nocD in newNocD)
            {
                nocD.ParentID = (
                    from d in dtDiag
                    where d.NursingDiagnosaID == nocD.NursingDiagnosaParentID &&
                        (
                            from e in dtDiag
                            where e.NursingDiagnosaID == d.NursingDiagnosaParentID &&
                                e.SRNursingCarePlanning != "01"
                            select e.ID
                        ).Contains(d.ParentID)
                    select d.ID).First();
            }

            // hapus NOC Header yang kosong gak ada childnya (pengganti delete di SetNOC)
            var nocH = from d in dtDiag
                       where d.SRNursingDiagnosaLevel == "20" &&
                           !(from e in dtDiag where e.SRNursingDiagnosaLevel == "21" select e.ParentID)
                           .Contains(d.ID)
                       select d;
            foreach (var nh in nocH)
            {
                nh.MarkAsDeleted();
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
            _autoNumberND = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NursingCareNo);
            var GeneratedNo = _autoNumberND.LastCompleteNumber;
            if (TobeSave)
            {
                // save autonumber immediately to decrease time gap between create and save
                _autoNumberND.Save();
            }
            return GeneratedNo;
        }
        public static NursingTransHD SetTransHD(string RegistrationNo, string userID)
        {
            NursingTransHD hd;
            var hdColl = GetTransHD(RegistrationNo);

            if (hdColl.Count == 0)
            {
                //hd = new NursingTransHD();
                hd = hdColl.AddNew();
                //hd.AddNew();
                var GeneratedNo = PopulateNewTransNo(true);


                hd.TransactionNo = GeneratedNo;
                hd.NursingTransDateTime = DateTime.Now;
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
        public static NursingTransHDCollection GetTransHD(string RegistrationNo)
        {
            var hdColl = new NursingTransHDCollection();
            hdColl.Query.Where(hdColl.Query.RegistrationNo == RegistrationNo);
            hdColl.LoadAll();
            return hdColl;
        }
        public static List<long?> FindAllRelatedChild(NursingDiagnosaTransDTCollection dtDiag, List<long?> parents)
        {
            var c1 = parents.Count;
            var res = (dtDiag.Where(x => parents.Contains(x.ParentID) && !parents.Contains(x.ID) && x.SRNursingDiagnosaLevel != "10")
                .Select(x => x.ID)).ToList();
            if (res.Count == 0) return parents;
            parents.AddRange(res);
            return FindAllRelatedChild(dtDiag, parents);
        }
        public static NursingDiagnosaTransDTCollection RemoveClosedDiagnosaAndChildRelated(NursingDiagnosaTransDTCollection dtDiag)
        {
            // buang yang Diagnosanya sudah CLoseD
            List<long?> idToDetach;
            idToDetach = (dtDiag.Where(x => x.SRNursingDiagnosaLevel == "10" && x.SRNursingCarePlanning == "01")
                .Select(x => x.ID)).ToList();
            idToDetach = Common.NursingCare.FindAllRelatedChild(dtDiag, idToDetach);

            foreach (var d in dtDiag)
            {
                if (idToDetach.Contains(d.ID)) dtDiag.DetachEntity(d);
            }
            return dtDiag;
        }
        #endregion
        #region Nursing Care Problem
        public static void SaveDiagL11(string RegistrationNo, NursingDiagnosaTransDT[] selectedProb, NursingDiagnosaTransDT[] selectedDiagEdited, string SRNsType)
        {
            var hd = Common.NursingCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);

            var dtDiag = new NursingDiagnosaTransDTCollection();
            dtDiag.Query.Where(dtDiag.Query.TransactionNo == hd.TransactionNo,
                dtDiag.Query.SRNursingDiagnosaLevel.In(new string[] { "10", "11" }));
            dtDiag.LoadAll();

            dtDiag = RemoveClosedDiagnosaAndChildRelated(dtDiag);

            SetProblem(dtDiag, selectedProb, hd, SRNsType); // common called as etiology
            if (selectedDiagEdited != null) {
                if (selectedDiagEdited.Count() > 0) {
                    foreach (var diagEdited in selectedDiagEdited) {
                        var dx = dtDiag.Where(d => d.NursingDiagnosaID == diagEdited.NursingDiagnosaID).FirstOrDefault();
                        if (dx != null) {
                            dx.NursingDiagnosaName = diagEdited.NursingDiagnosaName;
                        }
                    }
                }
            }
            SaveDiagnosa(/*hd, */dtDiag);
        }
        private static void SetProblem(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT[] selectedProb,
            NursingTransHD hd, string SRNsType)
        {
            #region Problem
            // Populate Selected Problem
            foreach (NursingDiagnosaTransDT na in selectedProb)
            {
                // cari etiology yang diagnosanya belum close, ada atau tidak.
                // kalau ada pake yang itu saja, tapi kalau tidak ada maka bikin baru
                var lNa = (from d in dtDiag
                           join e in dtDiag on d.ParentID equals e.ID
                           where d.NursingDiagnosaID == na.NursingDiagnosaID &&
                           e.SRNursingCarePlanning != "01" /*Closed/Stopped*/
                           select d);
                if (lNa.Count() == 1)
                {
                    // sudah ada, cek ada perubahan atau tidak
                    var n = lNa.First();
                    if (n.NursingDiagnosaName != na.NursingDiagnosaName)
                    {
                        n.NursingDiagnosaName = na.NursingDiagnosaName;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.NursingDiagnosaID = na.NursingDiagnosaID;
                    n.NursingDiagnosaName = na.NursingDiagnosaName;
                    n.SRNursingDiagnosaLevel = "11";
                    n.NursingDiagnosaParentID = na.NursingDiagnosaParentID;
                    n.Priority = 0;
                    n.EvalPeriod = 0;
                    n.PeriodConversionInHour = 24;
                    n.Skala = 1;
                    n.Target = 0;
                    n.Evaluasi = 1;
                    n.Respond = string.Empty;
                    n.Reexamine = false;
                    n.ExecuteDateTime = DateTime.Now;

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.S = na.S ?? string.Empty;
                    n.O = na.O ?? string.Empty;

                    n.TmpNursingDiagnosaID = na.TmpNursingDiagnosaID ?? string.Empty;
                    n.SRNsType = SRNsType;
                }
            }
            #endregion
            #region Diagnosa
            // Diagnosa (diambil dari problem header
            List<string> NewSelectedDiag = new List<string>();

            var qdiag = new NursingDiagnosaQuery();
            if (selectedProb.Length == 0)
            {
                qdiag.Where(qdiag.NursingDiagnosaID == string.Empty);
            }
            else
            {
                qdiag.Where(qdiag.NursingDiagnosaID.In((from t in selectedProb select t.NursingDiagnosaID)));
            }
            qdiag.Select(qdiag.NursingDiagnosaParentID);
            qdiag.es.Distinct = true;
            var ldt = qdiag.LoadDataTable();
            foreach (System.Data.DataRow row in ldt.Rows)
            {
                NewSelectedDiag.Add(row[0].ToString());
            }

            NursingDiagnosaCollection parentColl = new NursingDiagnosaCollection();
            if (NewSelectedDiag.Count == 0)
            {
                parentColl.Query.Where(parentColl.Query.NursingDiagnosaID == string.Empty);
            }
            else
            {
                parentColl.Query.Where(parentColl.Query.NursingDiagnosaID.In(NewSelectedDiag));
            }

            parentColl.LoadAll();

            foreach (var na in parentColl)
            {
                if ((from d in dtDiag
                     where d.NursingDiagnosaID == na.NursingDiagnosaID &&
                         d.SRNursingCarePlanning != "01"
                     select d).Count() == 0)
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.NursingDiagnosaID = na.NursingDiagnosaID;
                    n.NursingDiagnosaName = na.NursingDiagnosaName;
                    n.SRNursingDiagnosaLevel = "10";
                    n.NursingDiagnosaParentID = na.NursingDiagnosaParentID;
                    n.Priority = 0;
                    n.EvalPeriod = 0;
                    n.PeriodConversionInHour = 24;
                    n.Skala = 1;
                    n.Target = 0;
                    n.Evaluasi = 1;
                    n.Respond = string.Empty;
                    n.Reexamine = true;
                    n.ExecuteDateTime = DateTime.Now;/*ExecuteDateTime dipakai untuk tanggal terakhir evaluasi*/

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.TmpNursingDiagnosaID = (from dx in dtDiag where dx.NursingDiagnosaParentID == n.NursingDiagnosaID select dx.TmpNursingDiagnosaID).First();

                    n.SRNsType = SRNsType;
                }
            }
            #endregion
        }
        #endregion
        #region Nursing Care Diagnosa
        public static void SaveDiagL10(string RegistrationNo, NursingDiagnosaTransDT[] selectedDiag,
            NursingDiagnosaTransDTCollection dtDiag, NursingTransHD hd)
        {
            SetDiagnosa(dtDiag, selectedDiag); // common called as etiology
            SaveDiagnosa(/*hd, */dtDiag);
        }
        private static void SetDiagnosa(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT[] selectedDiag)
        {
            // only update
            foreach (var na in selectedDiag)
            {
                //if ((from d in dtDiag where d.NursingDiagnosaID == na.NursingDiagnosaID select d).Count() == 1)
                if ((from d in dtDiag where d.ID == na.ID select d).Count() == 1)
                {
                    //var n = dtDiag.Where(x => x.NursingDiagnosaID == na.NursingDiagnosaID).First();
                    var n = dtDiag.Where(x => x.ID == na.ID).First();
                    if (n.SRNursingCarePlanning != "01"/*stop*/)
                    {
                        var itm = selectedDiag.Where(x => x.ID == na.ID).FirstOrDefault();

                        n.NursingDiagnosaName = itm.NursingDiagnosaName;
                        n.Priority = itm.Priority ?? 0;
                        n.EvalPeriod = itm.EvalPeriod ?? 0;
                        n.PeriodConversionInHour = itm.PeriodConversionInHour ?? 24;
                        n.ExecuteDateTime = itm.ExecuteDateTime ?? DateTime.Now;

                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = DateTime.Now;
                    }
                }
            }
        }
        #endregion
        #region Nursing Care Target (NOC)
        public static void SaveDiagL20_21(string RegistrationNo, NursingDiagnosaTransDT[] selectedNOCTarget,
            NursingDiagnosaTransDTCollection dtDiag, NursingTransHD hd, long idL10)
        {
            SetNOC(dtDiag, selectedNOCTarget, hd, idL10);
            SaveDiagnosa(/*hd, */dtDiag);
        }

        private static void SetNOC(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT[] selectedNOCTarget,
            NursingTransHD hd, long idL10)
        {
            #region NOC
            // NOC (diambil dari NOC TArget header
            var SelectedNoc = (from t in selectedNOCTarget select t.NursingDiagnosaParentID).Distinct();

            NursingDiagnosaCollection NOCColl = new NursingDiagnosaCollection();
            if (SelectedNoc.Count() == 0)
            {
                NOCColl.Query.Where(NOCColl.Query.NursingDiagnosaID == string.Empty);
            }
            else
            {
                NOCColl.Query.Where(NOCColl.Query.NursingDiagnosaID.In(SelectedNoc));
            }
            NOCColl.LoadAll();

            foreach (var na in NOCColl)
            {
                if ((from d in dtDiag
                     where d.NursingDiagnosaID == na.NursingDiagnosaID
                     select d).Count() == 0)
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.NursingDiagnosaID = na.NursingDiagnosaID;
                    string[] aMeningkatMenurun = { "meningkat", "menurun", "membaik" };
                    n.NursingDiagnosaName = na.NursingDiagnosaName + (aMeningkatMenurun.Any(na.NursingDiagnosaName.ToLower().Contains) ? " dengan kriteria hasil:":"");
                    n.SRNursingDiagnosaLevel = "20";
                    n.NursingDiagnosaParentID = na.NursingDiagnosaParentID;
                    n.Priority = 0;
                    n.EvalPeriod = 0;
                    n.PeriodConversionInHour = 24;
                    n.Skala = 1;
                    n.Target = 0;
                    n.Evaluasi = 1;
                    n.Respond = string.Empty;
                    n.Reexamine = true;// (from ip in selectedDiag where ip.NursingDiagnosaID == na.NursingDiagnosaID select ip.Reexamine).FirstOrDefault() ?? false;
                    n.ExecuteDateTime = DateTime.Now;/*ExecuteDateTime dipakai untuk tanggal terakhir evaluasi*/


                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;
                }
            }
            #endregion

            #region NOC Target
            foreach (var na in selectedNOCTarget)
            {
                if ((from d in dtDiag where d.NursingDiagnosaID == na.NursingDiagnosaID select d).Count() == 1)
                {
                    // sudah ada, tidak perlu ditambahkan
                    var n = dtDiag.Where(x => x.NursingDiagnosaID == na.NursingDiagnosaID).First();
                    n.NursingDiagnosaName = na.NursingDiagnosaName;
                    n.Skala = na.Skala;
                    n.Target = na.Target;
                    n.Evaluasi = na.Evaluasi ?? 1;
                    //n.ExecuteDateTime = na.ExecuteDateTime; /*ExecuteDateTime dipakai untuk tanggal terakhir evaluasi*/

                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;
                }
                else
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.NursingDiagnosaID = na.NursingDiagnosaID;
                    n.NursingDiagnosaName = na.NursingDiagnosaName;
                    n.SRNursingDiagnosaLevel = "21";
                    n.NursingDiagnosaParentID = na.NursingDiagnosaParentID;
                    n.Priority = 0;
                    n.EvalPeriod = 0;// default 0
                    n.PeriodConversionInHour = 24;
                    n.Skala = na.Skala;//(from ip in selectedNOCTarget where ip.NursingDiagnosaID == na.NursingDiagnosaID select ip.Skala).FirstOrDefault() ?? 1;
                    n.Target = na.Target;// (from ip in selectedNOCTarget where ip.NursingDiagnosaID == na.NursingDiagnosaID select ip.Target).FirstOrDefault() ?? 0;
                    n.Evaluasi = na.Evaluasi ?? 1;// (from ip in selectedNOCTarget where ip.NursingDiagnosaID == na.NursingDiagnosaID select ip.Evaluasi).FirstOrDefault() ?? 1;
                    n.Respond = string.Empty;
                    n.Reexamine = false;
                    n.ExecuteDateTime = DateTime.Now;  /*ExecuteDateTime dipakai untuk tanggal terakhir evaluasi*/

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;
                }
            }
            #endregion

            #region eliminate unselected
            // hapus noc detail dulu deh
            var nocDDiag = dtDiag.Where(x => !selectedNOCTarget.Select(f => f.NursingDiagnosaID)
                .Contains(x.NursingDiagnosaID) &&
                x.SRNursingDiagnosaLevel == "21" &&
                (from d in
                     (dtDiag.Where(y => y.SRNursingDiagnosaLevel == "20" &&
                         (from d in dtDiag
                          where d.SRNursingDiagnosaLevel == "10" &&
                              d.SRNursingCarePlanning != "01" && d.ID == idL10
                          select d.NursingDiagnosaID).Contains(y.NursingDiagnosaParentID)
                         ))
                 select d.NursingDiagnosaID).Contains(x.NursingDiagnosaParentID)
            );

            foreach (var s in nocDDiag)
            {
                s.MarkAsDeleted();
            }

            var nocToDel = dtDiag.Where(x => !SelectedNoc.Contains(x.NursingDiagnosaID) &&
                x.SRNursingDiagnosaLevel == "20" &&
                (from d in dtDiag
                 where d.SRNursingDiagnosaLevel == "10" &&
                     d.SRNursingCarePlanning != "01" && d.ID == idL10
                 select d.NursingDiagnosaID).Contains(x.NursingDiagnosaParentID)
            );

            foreach (var s in nocToDel)
            {
                //s.MarkAsDeleted();
                /*gak bisa delete disini, konflik karena trigger, delete pas save saja*/
            }
            #endregion
        }
        #endregion
        #region Nursing Care Planning (NIC)
        public static void SaveDiagL30(string RegistrationNo, NursingDiagnosaTransDT[] selectedNIC,
            NursingDiagnosaTransDTCollection dtDiag, NursingTransHD hd, long idL10)
        {
            SetNIC(dtDiag, selectedNIC, hd, idL10, false);
            SaveDiagnosa(/*hd, */dtDiag);
        }
        private static void SetNIC(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT[] selectedNIC,
            NursingTransHD hd, long idL10, bool SkipDelete)
        {
            // TODO: harusnya disini intervensi yang sudah close jangan diganggu lagi,
            // kalau ada intervensi yang sama harusnya add lagi.
            #region NIC
            foreach (NursingDiagnosaTransDT na in selectedNIC)
            {
                var lNa = (from d in dtDiag
                           where d.NursingDiagnosaID == na.NursingDiagnosaID
                           select d);
                if (lNa.Count() == 1)
                {
                    // sudah ada, cek ada perubahan atau tidak
                    var n = lNa.First();
                    if (n.NursingDiagnosaName != na.NursingDiagnosaName ||
                        n.SRNursingCarePlanning != na.SRNursingCarePlanning)
                    {
                        n.NursingDiagnosaName = na.NursingDiagnosaName;
                        n.SRNursingCarePlanning = na.SRNursingCarePlanning;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    var n = dtDiag.AddNew();
                    n.TransactionNo = hd.TransactionNo;
                    n.NursingDiagnosaID = na.NursingDiagnosaID;
                    n.NursingDiagnosaName = na.NursingDiagnosaName;
                    n.SRNursingDiagnosaLevel = "30";
                    n.NursingDiagnosaParentID = na.NursingDiagnosaParentID;
                    n.Priority = 0;
                    n.EvalPeriod = 0;
                    n.PeriodConversionInHour = 24;
                    n.Skala = 1;
                    n.Target = 0;
                    n.Evaluasi = 1;
                    n.Respond = string.Empty;
                    n.Reexamine = false;
                    n.ExecuteDateTime = DateTime.Now;

                    n.CreateByUserID = AppSession.UserLogin.UserID;
                    n.CreateDateTime = DateTime.Now;
                    n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    n.LastUpdateDateTime = DateTime.Now;

                    n.SRNursingCarePlanning = na.SRNursingCarePlanning;

                    n.ParentID = na.ParentID;
                }
            }
            #endregion

            #region eliminate unselected
            if (!SkipDelete)
            {
                var tdlDiag = dtDiag.Where(x => !selectedNIC.Select(f => f.NursingDiagnosaID)
                    .Contains(x.NursingDiagnosaID)
                    && ((new string[] { "30" }).Contains(x.SRNursingDiagnosaLevel))
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
        #region Nursing Care Implementation
        #endregion
        #region Nursing Care Evaluation
        public static void SaveDiagL40(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT Evaluation,
            NursingDiagnosaTransDTCollection NursingEvaluationInterventionSelected, NursingTransHD hd, long idL10)
        {
            SetEvaluation(dtDiag, Evaluation, NursingEvaluationInterventionSelected, hd, idL10);
            //SaveEvaluation();
            using (esTransactionScope trans = new esTransactionScope())
            {
                dtDiag.Save();
                Evaluation.Save();

                // insert detail evaluation
                var dEvaColl = NursingDiagnosaTransDT.DetailEvaluationByTransactionNo(hd.TransactionNo);
                // dikali intervensi yang ada
                var intrvs = dtDiag.Where(x => x.ParentID == Evaluation.ParentID && x.SRNursingDiagnosaLevel == "30");
                foreach (var intrv in intrvs)
                {
                    var EvsI = dEvaColl.AddNew();
                    EvsI.EvaluationID = Evaluation.ID;
                    EvsI.InterventionID = intrv.ID;
                    EvsI.NursingInterventionID = intrv.NursingDiagnosaID;
                    EvsI.SRNursingCarePlanning = intrv.SRNursingCarePlanning;
                    EvsI.CreateByUserID = AppSession.UserLogin.UserID;
                    EvsI.CreateDateTime = DateTime.Now;
                }

                //using (esTransactionScope trans = new esTransactionScope())
                //{
                dEvaColl.Save();

                trans.Complete();
            }
        }

        public static void SetEvaluation(NursingDiagnosaTransDTCollection dtDiag, NursingDiagnosaTransDT Evaluation,
            NursingDiagnosaTransDTCollection NursingEvaluationInterventionSelected, NursingTransHD hd, long idL10)
        {
            // jika ada tambahan intervensi set disini
            List<NursingDiagnosaTransDT> newIntvs = new List<NursingDiagnosaTransDT>();
            foreach (var i in NursingEvaluationInterventionSelected)
            {
                var oldIntv = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "30" &&
                    x.ParentID == i.ParentID && x.NursingDiagnosaID == i.NursingDiagnosaID);
                if (oldIntv.Count() > 0)
                {
                    // jika sudah ada maka update status terakhirnya
                    foreach (var oi in oldIntv)
                    {
                        // yang sudah pernah ada berarti diteruskan
                        oi.SRNursingCarePlanning = "02"/*diteruskan*/;
                        //oi.SRNursingCarePlanning = i.SRNursingCarePlanning;
                    }
                }
                else
                {
                    i.SRNursingCarePlanning = "03";/*revisi*/
                    newIntvs.Add(i);
                }
            }
            // tambahan intervensi
            if (newIntvs.Count > 0)
                SetNIC(dtDiag, newIntvs.ToArray(), hd, idL10, true);

            // stop yang tidak ada di selected nic
            var stopNic = dtDiag.Where(x => x.ParentID == idL10 && x.SRNursingDiagnosaLevel == "30" &&
                !NursingEvaluationInterventionSelected.Select(y => y.NursingDiagnosaID).Contains(x.NursingDiagnosaID));
            foreach (var nn in stopNic)
            {
                nn.SRNursingCarePlanning = "01";
                nn.LastUpdateByUserID = AppSession.UserLogin.UserID;
                nn.LastUpdateDateTime = DateTime.Now;
            }

            // evaluasi harus update headernya
            if (Evaluation.es.IsAdded)
            {
                //var s = (dtDiag.Select(x => x.ID)).ToArray();

                var d = dtDiag.Where(x => x.ID == Evaluation.ParentID).First();
                // update status diagnosa
                d.SRNursingCarePlanning = Evaluation.SRNursingCarePlanning;
                d.P = Evaluation.P;
                // update juga tanggalnya
                //d.ExecuteDateTime = DateTime.Now;/*nanti tambah field sendiri aja*/
                // update reexamine, kalau stop berarti 0
                d.Reexamine = (Evaluation.SRNursingCarePlanning != "01");

                // update status etiologynya
                // cari etiology yang belum stop.
                var eth = dtDiag.Where(x => x.NursingDiagnosaParentID == d.NursingDiagnosaID &&
                    x.SRNursingDiagnosaLevel == "11" /*Etiology*/ &&
                    x.SRNursingCarePlanning != "01" /*STOP*/);
                foreach (var et in eth)
                {
                    et.SRNursingCarePlanning = Evaluation.SRNursingCarePlanning;
                    et.P = Evaluation.P;
                }
            }
        }

        //private void SaveEvaluation(NursingTransHD hd, NursingDiagnosaTransDTCollection dtDiag,
        //    NursingDiagnosaTransDT Evaluation)
        //{
        //    using (esTransactionScope trans = new esTransactionScope())
        //    {
        //        //hd.Save();
        //        dtDiag.Save();
        //        Evaluation.Save();
        //        //Commit if success, Rollback if failed
        //        trans.Complete();
        //    }

        //    // insert detail evaluation
        //    // kumpulkan id evaluasi yang baru tersimpan
        //    var dEvaColl = NursingDiagnosaTransDT.DetailEvaluation(hd.TransactionNo);
        //    var newEva = NursingEvaluations.Where(x => !(dEvaColl.Select(y => y.EvaluationID)).Contains(x.ID));
        //    foreach (var eva in newEva)
        //    {
        //        // dikali intervensi yang ada
        //        var intrvs = dtDiag.Where(x => x.ParentID == eva.ParentID && x.SRNursingDiagnosaLevel == "30");
        //        foreach (var intrv in intrvs)
        //        {
        //            var EvsI = dEvaColl.AddNew();
        //            EvsI.EvaluationID = eva.ID;
        //            EvsI.InterventionID = intrv.ID;
        //            EvsI.NursingInterventionID = intrv.NursingDiagnosaID;
        //            EvsI.SRNursingCarePlanning = intrv.SRNursingCarePlanning;
        //            EvsI.CreateByUserID = AppSession.UserLogin.UserID;
        //            EvsI.CreateDateTime = DateTime.Now;
        //        }
        //    }

        //    using (esTransactionScope trans = new esTransactionScope())
        //    {
        //        dEvaColl.Save();
        //        trans.Complete();
        //    }

        //    NursingEvaluationIntervention = null;
        //}
        #endregion
        #region Nursing Assessment
        public static string NursingAssessmentGetLastImportedType(string RegistrationNo) {
            var qfColl = new QuestionFormCollection();
            var qf = new QuestionFormQuery("qf");
            var phr = new PatientHealthRecordQuery("phr");
            var nath = new NursingAssessmentTransHDQuery("nath");
            var nh = new NursingTransHDQuery("nh");

            qf.InnerJoin(phr).On(qf.QuestionFormID == phr.QuestionFormID)
                .InnerJoin(nath).On(phr.TransactionNo == nath.QuestionFormReference)
                .InnerJoin(nh).On(nath.TransactionNo == nh.TransactionNo && nh.RegistrationNo == phr.RegistrationNo)
                .OrderBy(nath.AssessmentDateTime.Descending)
                .Select(qf)
                .Where(nh.RegistrationNo == RegistrationNo);
            qf.es.Top = 1;
            if (qfColl.Load(qf)) {
                var item = qfColl.First();
                if (string.IsNullOrEmpty(item.SRNsType))
                    throw new Exception(string.Format("Invalid nursing type of form {0}", item.QuestionFormName));
                return qfColl.First().SRNsType;
            }
            return string.Empty;
        }
        #endregion

        public static string[] GetRelatedRegistrationsByNsTransNo(string NsTransactionNo) {
            var hd = new NursingTransHD();
            if (hd.LoadByPrimaryKey(NsTransactionNo)) {
                return GetRelatedRegistrations(hd.RegistrationNo);
            }
            return new string[] { };
        }
        public static string[] GetRelatedRegistrations(string RegistrationNo) {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            // get form merge billing
            var mbColl = new MergeBillingCollection();
            var mb = new MergeBillingQuery("mb");
            mb.Where(mb.Or(mb.RegistrationNo == RegistrationNo, mb.FromRegistrationNo == RegistrationNo));
            mbColl.Load(mb);
            var regs = mbColl.Where(x => x.RegistrationNo != string.Empty).Select(x => x.RegistrationNo).Union(
                    mbColl.Where(x => x.FromRegistrationNo != string.Empty).Select(x => x.FromRegistrationNo)
                ).Distinct();

            var regColl = new RegistrationCollection();
            regColl.Query.Where(regColl.Query.RegistrationNo.In(regs), regColl.Query.FromRegistrationNo.IsNotNull());
            regColl.LoadAll();

            regs = regs.Union(regColl.Select(x => x.FromRegistrationNo)).Distinct();

            regColl.QueryReset();
            regColl.Query.Where(regColl.Query.RegistrationNo.In(regs), regColl.Query.PatientID == reg.PatientID);
            regColl.LoadAll();

            return regColl.Select(x => x.RegistrationNo).Distinct().ToArray();
        }
    }
}