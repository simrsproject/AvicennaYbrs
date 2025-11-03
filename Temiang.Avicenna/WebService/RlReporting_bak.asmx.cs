using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Data.Linq;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for RlReporting
    ///  fj ljsfjasdf jasdfjasdlfj asdfjasdf jsdjf als
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RlReporting : BaseDataService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Rl1_2_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 1;

                var accessCode = ValidateAccessKey(AccessKey);

                RlTxReport12Item.ProcessInsert(month.ToString(), year.ToString(), accessCode);

                double jmHariPerawatan = 0;
                double jmTt = 0;
                double jmHariDlmSatuPeriode = 0;
                double jmJtt = 0;
                double jmLamaDiRawat = 0;
                double jmPasienKeluar = 0;
                double jmPasienMati = 0;
                double jmPasienMati48 = 0;
                double jmKunjunganPoli = 0;

                var rl = new RlTxReport12Item();
                if (rl.LoadByPrimaryKey(month.ToString(), year.ToString()))
                {
                    jmHariPerawatan = Convert.ToDouble(rl.HariPerawatan);
                    jmTt = Convert.ToDouble(rl.Tt);
                    jmHariDlmSatuPeriode = Convert.ToDouble(rl.HariDlmSatuPeriode);
                    jmJtt = Convert.ToDouble(rl.JTt);
                    jmLamaDiRawat = Convert.ToDouble(rl.LamaDirawat);
                    jmPasienKeluar = Convert.ToDouble(rl.Keluar);
                    jmPasienMati = Convert.ToDouble(rl.KeluarMati);
                    jmPasienMati48 = Convert.ToDouble(rl.KeluarMati48);
                    jmKunjunganPoli = Convert.ToDouble(rl.Kunjungan);
                }

                var bor = (jmHariPerawatan / jmJtt) * 100; //(jmHariPerawatan / (jmTt * jmHariDlmSatuPeriode)) * 100;
                var los = jmLamaDiRawat / jmPasienKeluar;
                var bto = jmPasienKeluar / jmTt;
                var toi = (jmJtt - jmHariPerawatan) / jmPasienKeluar; //((jmTt * jmHariDlmSatuPeriode) - jmHariPerawatan) / jmPasienKeluar;
                var ndr = (jmPasienMati48 / jmPasienKeluar) * 1000;
                var gdr = (jmPasienMati / jmPasienKeluar) * 1000;
                var avgVisit = jmKunjunganPoli / jmHariDlmSatuPeriode;
                var avg = 0;


                /// 
                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                RlTxReport12 detail = new RlTxReport12();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());
                if (coll.LoadAll())
                {
                    entity = coll.First();
                    if (!detail.LoadByPrimaryKey(entity.RlTxReportNo))
                    {
                        detail.AddNew();
                    }
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();

                    detail = new RlTxReport12();
                    detail.AddNew();
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                detail.RlTxReportNo = entity.RlTxReportNo;
                detail.Bor = bor.ToDecimal();
                detail.Los = los.ToDecimal();
                detail.Bto = bto.ToDecimal();
                detail.Toi = toi.ToDecimal();
                detail.Ndr = ndr.ToDecimal();
                detail.Gdr = gdr.ToDecimal();
                detail.RataKunjungan = avgVisit.ToDecimal();
                detail.RataRata = avg.ToDecimal();

                //Last Update Status
                if (detail.es.IsAdded || detail.es.IsModified)
                {
                    detail.LastUpdateByUserID = accessCode;
                    detail.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl1_3_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 2;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport13Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                var startDate = new DateTime(year, month, 1);
                var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                var pdate = endDate;
                var nob = new NumberOfBedSmfQuery();
                nob.Select(nob.StartingDate);
                nob.Where(nob.StartingDate.Date() <= endDate.Date);
                nob.OrderBy(nob.StartingDate.Descending);
                nob.es.Top = 1;
                DataTable nobdt = nob.LoadDataTable();
                if (nobdt.Rows.Count > 0)
                {
                    pdate = Convert.ToDateTime(nobdt.Rows[0]["StartingDate"]);
                }

                foreach (var mri in mstDt)
                {
                    int vvip = 0, vip = 0, i = 0, ii = 0, iii = 0, khusus = 0;
                    var smfId = mri.SRParamedicRL1;

                    if (!string.IsNullOrEmpty(smfId))
                    {
                        RlTxReport13.Process(pdate, smfId, out vvip, out vip, out i, out ii, out iii, out khusus);
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.JumlahTt = vvip + vip + i + ii + iii + khusus;
                    item.Vvip = vvip;
                    item.Vip = vip;
                    item.I = i;
                    item.Ii = ii;
                    item.Iii = iii;
                    item.KelasKhusus = khusus;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_1_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 4;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport31Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                var startDate = new DateTime(year, month, 1);
                var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                foreach (var mri in mstDt)
                {
                    int pAwal = 0, pMasuk = 0, pHidup = 0, pMatiK48 = 0, pMatiL48 = 0, pAkhir = 0, lamaRawat = 0, hariRawat = 0;
                    int pPindahan = 0, pDipindahkan = 0;
                    int vvip = 0, vip = 0, i = 0, ii = 0, iii = 0, khusus = 0;

                    string paramedicRl1 = mri.SRParamedicRL1;
                    if (!string.IsNullOrEmpty(paramedicRl1))
                    {
                        RlTxReport31.Process(startDate, endDate, paramedicRl1, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
                            out pAwal, out pMasuk, out pHidup, out pMatiK48, out pMatiL48, out pAkhir, out lamaRawat, out hariRawat, out pPindahan, out pDipindahkan,
                            out vvip, out vip, out i, out ii, out iii, out khusus);
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = item.RlMasterReportItemID;
                    item.RlMasterReportItemCode = item.RlMasterReportItemCode;
                    item.RlMasterReportItemName = item.RlMasterReportItemName;
                    item.PasienAwal = pAwal;
                    item.PasienMasuk = pMasuk;
                    item.PasienKeluarHidup = pHidup;
                    item.PasienKeluarMatiK48 = pMatiK48;
                    item.PasienKeluarMatiL48 = pMatiL48;
                    item.PasienPindahan = pPindahan;
                    item.PasienDipindahkan = pDipindahkan;
                    item.LamaRawat = lamaRawat;
                    item.PasienAkhir = pAkhir;
                    item.HariRawat = hariRawat;
                    item.Vvip = vvip;
                    item.Vip = vip;
                    item.I = i;
                    item.Ii = ii;
                    item.Iii = iii;
                    item.KelasKhusus = khusus;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_2_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 5;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport32Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    int pRujukan = 0, pNonRujukan = 0, pDiRawat = 0, pDiRujuk = 0, pPulang = 0, pMatiDiUgd = 0, pDoa = 0;

                    string parValue = mri.ParameterValue;
                    if (!string.IsNullOrEmpty(parValue))
                    {
                        RlTxReport32.Process(month, month, year, parValue, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, AppSession.Parameter.DischargeConditionDie,
                           out pRujukan, out pNonRujukan, out pDiRawat, out pDiRujuk, out pPulang, out pMatiDiUgd, out pDoa);
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.PasienRujukan = pRujukan;
                    item.PasienNonRujukan = pNonRujukan;
                    item.DiRawat = pDiRawat;
                    item.DiRujuk = pDiRujuk;
                    item.Pulang = pPulang;
                    item.MatiDiUgd = pMatiDiUgd;
                    item.Doa = pDoa;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_3_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 6;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport33Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport33.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_4_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 7;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport34Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport34.Process(month, month, year, mri.RlMasterReportItemID ?? 0, AppSession.Parameter.ServiceUnitImunisasiTTId, AppSession.Parameter.ItemIdImunisasiTT1, AppSession.Parameter.ItemIdImunisasiTT2,
                        out int pRmRumahSakit, out int pRmBidan, out int pRmPuskesmas, out int pRmFasKesLain, out int pRmHidup, out int pRmMati, out int pRnmHidup, out int pRnmMati,
                        out int pNrHidup, out int pNrMati, out int pDiRujuk);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.RmRumahSakit = pRmRumahSakit;
                    item.RmBidan = pRmBidan;
                    item.RmPuskesmas = pRmPuskesmas;
                    item.RmFasKesLain = pRmFasKesLain;
                    item.RmHidup = pRmHidup;
                    item.RmMati = pRmMati;
                    item.RmTotal = pRmHidup + pRmMati;
                    item.RnmHidup = pRnmHidup;
                    item.RnmMati = pRnmMati;
                    item.RnmTotal = pRnmHidup + pRnmMati;
                    item.NrHidup = pNrHidup;
                    item.NrMati = pNrMati;
                    item.NrTotal = pNrHidup + pNrMati;
                    item.DiRujuk = pDiRujuk;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_5_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 8;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport35Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport35.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int pRmRumahSakit, out int pRmBidan, out int pRmPuskesmas, out int pRmFasKesLain,
                        out int pRmHidup, out int pRmMati, out int pRnmHidup, out int pRnmMati, out int pNrHidup, out int pNrMati, out int pDiRujuk);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.RmRumahSakit = pRmRumahSakit;
                    item.RmBidan = pRmBidan;
                    item.RmPuskesmas = pRmPuskesmas;
                    item.RmFasKesLain = pRmFasKesLain;
                    item.RmMati = pRmMati;
                    item.RmTotal = pRmHidup + pRmMati;
                    item.RnmMati = pRnmMati;
                    item.RnmTotal = pRnmHidup + pRnmMati;
                    item.NrMati = pNrMati;
                    item.NrTotal = pNrHidup + pNrMati;
                    item.DiRujuk = pDiRujuk;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_6_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 9;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport36Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    int khusus = 0, besar = 0, sedang = 0, kecil = 0, lain = 0;

                    string surgerySpecialty = mri.ParameterValue;
                    if (!string.IsNullOrEmpty(surgerySpecialty))
                    {
                        RlTxReport36.Process(month, month, year, surgerySpecialty, out khusus, out besar, out sedang, out kecil, out lain);
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Total = khusus + besar + sedang + kecil + lain;
                    item.Khusus = khusus;
                    item.Besar = besar;
                    item.Sedang = sedang;
                    item.Kecil = kecil;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_7_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 10;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport37Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport37.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_8_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 11;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport38Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport38.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_9_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 12;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport39Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport39.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_10_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 13;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport310Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport310.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_11_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 14;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport311Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport311.Process(month, month, year, mri.RlMasterReportItemID ?? 0, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_12_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 15;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport312Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                var startDate = new DateTime(year, month, 1);
                var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                foreach (var mri in mstDt)
                {
                    RlTxReport312.Process(startDate, endDate, mri.RlMasterReportItemID ?? 0, out int konselingAnc, out int konselingPascaPersalinan, out int kbBaruCmBukanRujukan,
                        out int kbBaruCmRujukanRi, out int kbBaruCmRujukanRj, out int kbBaruCmTotal, out int kbBaruDkNifas, out int kbBaruDkAbortus, out int kbBaruDkLain, out int kunjunganUlang,
                        out int keluhanEfekSamping, out int keluhanEfekSampingDiRujuk);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.KonselingAnc = konselingAnc;
                    item.KonselingPascaPersalinan = konselingPascaPersalinan;
                    item.KbBaruCmBukanRujukan = kbBaruCmBukanRujukan;
                    item.KbBaruCmRujukanRi = kbBaruCmRujukanRi;
                    item.KbBaruCmRujukanRj = kbBaruCmRujukanRj;
                    item.KbBaruCmTotal = kbBaruCmTotal;
                    item.KbBaruDkNifas = kbBaruDkNifas;
                    item.KbBaruDkAbortus = kbBaruDkAbortus;
                    item.KbBaruDkLain = kbBaruDkLain;
                    item.KunjunganUlang = kunjunganUlang;
                    item.KeluhanEfekSamping = keluhanEfekSamping;
                    item.KeluhanEfekSampingDiRujuk = keluhanEfekSampingDiRujuk;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_13_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 16;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport313Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport313.Process(month, month, year, mri.RlMasterReportItemCode, out int rj, out int rd, out int ri);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Rj = rj;
                    item.Rd = rd;
                    item.Ri = ri;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_13b_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 17;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport313bCollection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport313b.Process(mri.RlMasterReportItemCode, out int jmlItemObat, out int jmlItemObatRs, out int jmlItemObatFormulariumRs);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.JumlahItemObat = jmlItemObat;
                    item.JumlahItemObatRs = jmlItemObatRs;
                    item.JumlahItemObatFormulariumRs = jmlItemObatFormulariumRs;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_14_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 18;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport314Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    int rujukanPuskesmas = 0, rujukanFasKesLain = 0, rujukanRsLain = 0, dirujukKePuskesmasAsal = 0, dirujukKeFasKesAsal = 0;
                    int dirujukKeRsAsal = 0, dirujukPasienRujukan = 0, dirujukPasienDtgSendiri = 0, dirujukDiterimaKembali = 0;

                    string paramedicRl1 = mri.SRParamedicRL1;
                    if (!string.IsNullOrEmpty(paramedicRl1))
                    {
                        RlTxReport314.Process(month, month, year, paramedicRl1, out rujukanPuskesmas, out rujukanFasKesLain, out rujukanRsLain, out dirujukKePuskesmasAsal,
                            out dirujukKeFasKesAsal, out dirujukKeRsAsal, out dirujukPasienRujukan, out dirujukPasienDtgSendiri, out dirujukDiterimaKembali);
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.RujukanPuskesmas = rujukanPuskesmas;
                    item.RujukanFasKesLain = rujukanFasKesLain;
                    item.RujukanRsLain = rujukanRsLain;
                    item.DirujukKePuskesmasAsal = dirujukKePuskesmasAsal;
                    item.DirujukKeFasKesAsal = dirujukKeFasKesAsal;
                    item.DirujukKeRsAsal = dirujukKeRsAsal;
                    item.DirujukPasienRujukan = dirujukPasienRujukan;
                    item.DirujukPasienDtgSendiri = dirujukPasienDtgSendiri;
                    item.DirujukDiterimaKembali = dirujukDiterimaKembali;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl3_15_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 19;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport315Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport315.Process(month, month, year, mri.RlMasterReportItemID ?? 0, AppSession.Parameter.ServiceUnitLaboratoryID, AppSession.Parameter.ServiceUnitRadiologyID,
                    out int riJpk, out int riJld, out int rj, out int rjLab, out int rjRad, out int rjLl);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.RiJpk = riJpk;
                    item.RiJld = riJld;
                    item.Rj = rj;
                    item.RjLab = rjLab;
                    item.RjRad = rjRad;
                    item.RjLl = rjLl;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl4A_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 20;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport4ACollection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    var dtd = new Dtd();
                    dtd.LoadByPrimaryKey(item.RlMasterReportItemCode);
                    item.DtdLabel = dtd.DtdLabel;
                    item.L0006h = 0;
                    item.P0006h = 0;
                    item.L0628h = 0;
                    item.P0628h = 0;
                    item.L28h01t = 0;
                    item.P28h01t = 0;
                    item.L0104t = 0;
                    item.P0104t = 0;
                    item.L0414t = 0;
                    item.P0414t = 0;
                    item.L1424t = 0;
                    item.P1424t = 0;
                    item.L2444t = 0;
                    item.P2444t = 0;
                    item.L4464t = 0;
                    item.P4464t = 0;
                    item.L64t = 0;
                    item.P64t = 0;
                    item.TotalL = 0;
                    item.TotalP = 0;
                    item.Total = 0;
                    item.TotalMati = 0;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                RlTxReport4A.Process(month, month, year, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl4ASebab_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 21;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport4ASebabCollection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    var dtd = new Dtd();
                    dtd.LoadByPrimaryKey(item.RlMasterReportItemCode);
                    item.DtdLabel = dtd.DtdLabel;
                    item.L0006h = 0;
                    item.P0006h = 0;
                    item.L0628h = 0;
                    item.P0628h = 0;
                    item.L28h01t = 0;
                    item.P28h01t = 0;
                    item.L0104t = 0;
                    item.P0104t = 0;
                    item.L0414t = 0;
                    item.P0414t = 0;
                    item.L1424t = 0;
                    item.P1424t = 0;
                    item.L2444t = 0;
                    item.P2444t = 0;
                    item.L4464t = 0;
                    item.P4464t = 0;
                    item.L64t = 0;
                    item.P64t = 0;
                    item.TotalL = 0;
                    item.TotalP = 0;
                    item.Total = 0;
                    item.TotalMati = 0;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                RlTxReport4ASebab.Process(month, month, year, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl4B_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 22;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport4BCollection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    var dtd = new Dtd();
                    dtd.LoadByPrimaryKey(item.RlMasterReportItemCode);
                    item.DtdLabel = dtd.DtdLabel;
                    item.L0006h = 0;
                    item.P0006h = 0;
                    item.L0628h = 0;
                    item.P0628h = 0;
                    item.L28h01t = 0;
                    item.P28h01t = 0;
                    item.L0104t = 0;
                    item.P0104t = 0;
                    item.L0414t = 0;
                    item.P0414t = 0;
                    item.L1424t = 0;
                    item.P1424t = 0;
                    item.L2444t = 0;
                    item.P2444t = 0;
                    item.L4464t = 0;
                    item.P4464t = 0;
                    item.L64t = 0;
                    item.P64t = 0;
                    item.KasusBaruL = 0;
                    item.KasusBaruP = 0;
                    item.TotalKasusBaru = 0;
                    item.TotalKunjungan = 0;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                RlTxReport4B.Process(month, month, year, accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl4BSebab_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 23;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport4BSebabCollection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    var dtd = new Dtd();
                    dtd.LoadByPrimaryKey(item.RlMasterReportItemCode);
                    item.DtdLabel = dtd.DtdLabel;
                    item.L0006h = 0;
                    item.P0006h = 0;
                    item.L0628h = 0;
                    item.P0628h = 0;
                    item.L28h01t = 0;
                    item.P28h01t = 0;
                    item.L0104t = 0;
                    item.P0104t = 0;
                    item.L0414t = 0;
                    item.P0414t = 0;
                    item.L1424t = 0;
                    item.P1424t = 0;
                    item.L2444t = 0;
                    item.P2444t = 0;
                    item.L4464t = 0;
                    item.P4464t = 0;
                    item.L64t = 0;
                    item.P64t = 0;
                    item.KasusBaruL = 0;
                    item.KasusBaruP = 0;
                    item.TotalKasusBaru = 0;
                    item.TotalKunjungan = 0;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                RlTxReport4BSebab.Process(month, month, year, accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl51_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 24;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport51Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                foreach (var mri in mstDt)
                {
                    RlTxReport51.Process(month, month, year, mri.RlMasterReportItemCode, out int jml);

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl52_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 25;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport52Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                int total = 0;
                foreach (var mri in mstDt)
                {
                    int jml = 0;
                    if (mri.RlMasterReportItemID == 1756) //TOTAL
                        jml = total;
                    else
                    {
                        RlTxReport52.Process(month, month, year, mri.RlMasterReportItemID ?? 0, mri.ParameterValue, AppSession.Parameter.AssasmentObgynPoliKebidanan, AppSession.Parameter.AssasmentObgynPenyKandungan,
                            out jml);
                        total += jml;
                    }

                    var item = detail.AddNew();
                    item.RlTxReportNo = entity.RlTxReportNo;
                    item.RlMasterReportItemID = mri.RlMasterReportItemID;
                    item.RlMasterReportItemCode = mri.RlMasterReportItemCode;
                    item.RlMasterReportItemName = mri.RlMasterReportItemName;
                    item.Jumlah = jml;
                    item.LastUpdateByUserID = accessCode;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl53_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 26;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport53Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                RlTxReport53.Process(month, month, year, entity.RlTxReportNo, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
                    accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }

        public void Rl54_Process(string AccessKey, int month, int year)
        {
            var log = LogAdd();
            try
            {
                int RlMasterReportID = 27;

                var accessCode = ValidateAccessKey(AccessKey);

                var mstDt = new RlMasterReportItemCollection();
                mstDt.Query.Where(mstDt.Query.RlMasterReportID == RlMasterReportID);
                mstDt.Query.OrderBy(mstDt.Query.RlMasterReportItemNo.Ascending);
                mstDt.LoadAll();

                AppAutoNumberLast _autoNumber = new AppAutoNumberLast();
                RlTxReport entity;
                var detail = new RlTxReport54Collection();
                var coll = new RlTxReportCollection();
                coll.Query.Where(coll.Query.RlMasterReportID == RlMasterReportID,
                                 coll.Query.PeriodMonthStart == month.ToString(),
                                 coll.Query.PeriodMonthEnd == month.ToString(),
                                 coll.Query.PeriodYear == year.ToString());

                if (coll.LoadAll())
                {
                    entity = coll.First();

                    detail.Query.Where(detail.Query.RlTxReportNo == entity.RlTxReportNo);
                    detail.LoadAll();
                    detail.MarkAllAsDeleted();
                    detail.Save();
                }
                else
                {
                    entity = new RlTxReport();
                    entity.AddNew();

                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.ReportRlNo);

                    entity.RlTxReportNo = _autoNumber.LastCompleteNumber;
                    entity.RlMasterReportID = RlMasterReportID;
                    entity.PeriodMonthStart = month.ToString();
                    entity.PeriodMonthEnd = month.ToString();
                    entity.PeriodYear = year.ToString();
                }

                RlTxReport54.Process(month, month, year, entity.RlTxReportNo, accessCode, detail, out detail);

                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = accessCode;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                using (esTransactionScope trans = new esTransactionScope())
                {
                    if (entity.es.IsAdded)
                    {
                        _autoNumber.Save();
                    }

                    entity.Save();
                    detail.Save();
                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                WriteResponseAndLog(log, JSonRetFormatted("done"));
            }
            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
    }
}
