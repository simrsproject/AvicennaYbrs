using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Consolidation;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject.Util
{
    public class ConsolidationUtil
    {
        public static bool IsConsolidation
        {
            get
            {
                if (HttpContext.Current.Session["IsConsolidation"] == null)
                {
                    var value = ConfigurationManager.AppSettings["IsConsolidation"].ToLower();
                    bool isConsolidation = value == "yes" || value == "true";
                    HttpContext.Current.Session["IsConsolidation"] = isConsolidation;
                }
                return (bool)HttpContext.Current.Session["IsConsolidation"];
            }
        }

        public static void UpdateDataMasterFromHeadOffice()
        {
            // Consolidation from automatic log
            // Get previous pending / error UpdateLog 
            var updateLogColl = GetPreviouseUpdateError(string.Empty);
            if (updateLogColl.Count > 0)
            {
                foreach (var errLog in updateLogColl)
                {
                    var udateLog = new ConsolidationUpdateLog();
                    udateLog.LoadByPrimaryKey(errLog.UpdateID ?? 0);
                    if (!UpdateLogWriteToDB(udateLog)) return;
                    // Kalau ada error harus selesai dulu masalah error yang ini baru bisa lanjut
                }
            }

            // Ambil data dari HeadOffice
            var serv = new DataConsolidationSoap();

            // Check service exists
            if (ServiceExists(serv.Url))
            {
                try
                {
                    var token = "tokenyangbenar";
                    var healthcareID = AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareID);

                    // Ambil data hasil log otomatis di HO
                    var downloadData =
                        JsonConvert.DeserializeObject<DownloadData>(serv.DownloadDataMaster(token, healthcareID));

                    SaveConsolidationDataAndLog(downloadData);
                }
                catch (Exception ex)
                {
                    // Abaikan saja
                }
            }
        }
        public static bool ServiceExists(string url)
        {
            try
            {
                // try accessing the web service directly via it's URL
                HttpWebRequest request =
                    WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 2000;

                using (HttpWebResponse response =
                           request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                }

                // try getting the WSDL?
                // asmx lets you put "?wsdl" to make sure the URL is a web service
                // could parse and validate WSDL here

            }
            catch (WebException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        private static void SaveConsolidationDataAndLog(DownloadData downloadData)
        {
            if (string.IsNullOrEmpty(downloadData.UpdateData)) return;

            // Save Log
            var updateLog = new ConsolidationUpdateLog
            {
                ReferenceID = downloadData.UpdateID,
                ReferenceNote = downloadData.UpdateSummary,
                StartConsolidationLogID = downloadData.StartConsolidationLogID,
                EndConsolidationLogID = downloadData.EndConsolidationLogID,
                UpdateData = downloadData.UpdateData,
                UpdateDateTime = DateTime.Now,
                UpdateSummary = string.Empty,
                HealthcareID = string.Empty,
                IsManualLog = false,
                IsError = true // set as error first, if save sucessful then update as false; 
            };
            updateLog.Save();

            // Save consolidation data
            UpdateLogWriteToDB(updateLog);
        }


        public static string CommitData(string token, string healthcareID, string dataTransfer, bool isManualLog)
        {
            var commitLog = new ConsolidationCommitLog();
            var isCommitLogSaved = false;
            try
            {
                //Kode 
                // 0 -> Sukses
                // -1 -> data tidak disimpan di ConsolidationCommitLog
                // 1 -> data sudah disimpan di ConsolidationCommitLog tetapi error saat update
                // Validation first
                if (string.IsNullOrEmpty(dataTransfer))
                {
                    // the data to be transferred must not be empty
                    return "-1|The data to be transferred can not empty";
                }
                if (string.IsNullOrEmpty(healthcareID))
                {
                    // healthcare id must not be empty as well
                    return "-1|Department ID can not empty";
                }
                // End of Validation

                // Save Log
                commitLog.CommitData = dataTransfer;
                commitLog.CommitDateTime = DateTime.Now;
                commitLog.CommitSummary = string.Empty;
                commitLog.HealthcareID = healthcareID;
                commitLog.IsManualLog = isManualLog;
                commitLog.IsError = true;// set as error first, update as true later when all done
                commitLog.Save();

                isCommitLogSaved = true;

                var errMsg = CommitLogWriteToDB(commitLog);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return string.Format("1|Error save consolidation data to database production. {0}", errMsg);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    // Simpan jika ada error
                    commitLog.IsError = true;
                    commitLog.ErrorMessage = ex.Message;
                    commitLog.Save();
                    return string.Format("{0}|Error at web service. {1}", isCommitLogSaved ? "1" : "-1", commitLog.ErrorMessage);
                }
                catch
                {
                    // fail save log
                }
            }
            return "0|";
        }

        public bool CommitDataClosingJournalToHeadOffice(AccountBalance accountBalance, string editedBy, ref int createdJournalID, ref string returnErrorMessage)
        {
            var postingStatus = new PostingStatus();
            var qr = new PostingStatusQuery();
            qr.Where(qr.Year == accountBalance.ClosingYear && qr.Month == accountBalance.ClosingMonth);
            qr.es.Top = 1;
            postingStatus.Load(qr);
            accountBalance.JournalID = postingStatus.ConsolidationJournalID ?? 0;

            //if (postingStatus.IsConsolidation == true)
            //    return false;

            var commitData = JsonConvert.SerializeObject(accountBalance);
            var log = new ConsolidationCommitLog
            {
                HealthcareID = accountBalance.HealthcareID,
                IsManualLog = true,
                CommitData = commitData,
                CommitDateTime = DateTime.Now,
                CommitSummary = "",
                IsError = false
            };

            try
            {
                // Commit to HeadOffice via webservice
                var serv = new DataConsolidationSoap();
                var consolidationJournalID = serv.CommitDataClosingJournal(commitData, editedBy);
                log.Save();

                createdJournalID = consolidationJournalID;

                if (consolidationJournalID > 0)
                {
                    postingStatus.IsConsolidation = true;
                    postingStatus.IsEnabled = true; //Diisi true untuk mencegah proses closing ulang ..nama / pengisian fieldnya agak membingungkan nih
                    postingStatus.ConsolidationJournalID = consolidationJournalID;
                    postingStatus.Save();
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                try
                {
                    returnErrorMessage = ex.Message;
                    // Simpan jika ada error
                    log.IsError = true;
                    log.ErrorMessage = ex.Message;
                    log.Save();
                    return false;
                }
                catch (Exception e)
                {
                    returnErrorMessage = e.Message;
                    return false;
                }
            }
            return true;
        }

        public static ConsolidationUpdateLogCollection GetPreviouseUpdateError(string healthcareID)
        {
            var consolidationUpdateLogColl = new ConsolidationUpdateLogCollection();
            var qrCheck = new ConsolidationUpdateLogQuery();
            if (!string.IsNullOrEmpty(healthcareID))
            {
                qrCheck.Where(qrCheck.HealthcareID == healthcareID);
            }
            qrCheck.Where(qrCheck.IsError == true);
            qrCheck.OrderBy(qrCheck.UpdateID.Ascending);
            consolidationUpdateLogColl.Load(qrCheck);
            return consolidationUpdateLogColl;
        }

        public static string DownloadDataMaster(string token, string requestByHealthcareID)
        {
            return DownloadData(token, requestByHealthcareID, false);
        }

        public static string DownloadDataTransaction(string token, string requestByHealthcareID)
        {
            return DownloadData(token, requestByHealthcareID, true);
        }

        private static string DownloadData(string token, string requestByHealthcareID, bool isManualLog)
        {
            if (token != "tokenyangbenar") return "Wrong token";

            // Ambil ConsolidationLogID terakhir Klinik download data
            var qr = new ConsolidationUpdateLogQuery();
            qr.es.Top = 1;
            qr.Where(qr.HealthcareID == requestByHealthcareID && qr.IsManualLog == isManualLog);
            qr.OrderBy(qr.UpdateID.Descending);

            long? lastDownloadConsolidationLogID = 0;
            var ent = new ConsolidationUpdateLog();
            if (ent.Load(qr))
            {
                lastDownloadConsolidationLogID = ent.EndConsolidationLogID;
            }

            // Declare untuk start end download data
            string summary; long? startConsolidationLogID; long? endConsolidationLogID;
            var result = ConsolidationLogToConsolidationLogJson(requestByHealthcareID, isManualLog, lastDownloadConsolidationLogID,
                out startConsolidationLogID, out endConsolidationLogID, out summary);

            if (string.IsNullOrEmpty(result.Trim())) return string.Empty;

            // Save history
            ent = new ConsolidationUpdateLog
            {
                EndConsolidationLogID = endConsolidationLogID,
                HealthcareID = requestByHealthcareID,
                IsManualLog = isManualLog,
                StartConsolidationLogID = startConsolidationLogID,
                UpdateData = result,
                UpdateDateTime = DateTime.Now,
                UpdateSummary = summary
            };
            ent.Save();

            var downloadData = new DownloadData
            {
                UpdateID = ent.UpdateID,
                StartConsolidationLogID = ent.StartConsolidationLogID,
                EndConsolidationLogID = ent.EndConsolidationLogID,
                UpdateData = result,
                UpdateSummary = ent.UpdateSummary
            };

            return JsonConvert.SerializeObject(downloadData);
        }


        /// <summary>
        /// Fungsi ini digunakan untuk menterjemahkan data log klinik ke database klinik
        /// </summary>
        /// <param name="log">ConsolidationUpdateLog yang ada di database klinik</param>
        /// <returns></returns>
        public static bool UpdateLogWriteToDB(ConsolidationUpdateLog log)
        {
            try
            {
                // Extract and save data
                var dtf = JsonConvert.DeserializeObject<DataTransfer>(log.UpdateData);
                var updateSummary = string.Empty;
                var dataHds = dtf.DataTransferHeaders;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    updateSummary = SaveLogToDatabaseProduction(dataHds, log.IsManualLog ?? false);
                    trans.Complete();
                }
                // Save Summary
                log.UpdateSummary = updateSummary;
                log.ErrorMessage = string.Empty;
                log.IsError = false;
                log.Save();
            }
            catch (Exception ex)
            {
                try
                {
                    log.IsError = true;
                    log.ErrorMessage = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
                    log.Save();
                }
                catch (Exception ex2)
                {
                    // Nothing 
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fungsi ini digunakan untuk menterjemahkan data log pengelola ke database pengelola
        /// </summary>
        /// <param name="log">ConsolidationUpdateLog yang ada di database klinik</param>
        /// <returns></returns>
        public static string CommitLogWriteToDB(ConsolidationCommitLog log)
        {
            var ret = true;
            try
            {
                // Extract and save data
                var dtf = JsonConvert.DeserializeObject<DataTransfer>(log.CommitData);
                var commitSummary = string.Empty;
                var dataHds = dtf.DataTransferHeaders;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    //save summary
                    log.CommitSummary = SaveLogToDatabaseProduction(dataHds, log.IsManualLog ?? false);
                    log.IsError = false;
                    log.ErrorMessage = string.Empty;
                    log.Save();

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    log.IsError = true;
                    log.ErrorMessage = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
                    log.Save();
                    return ex.Message;
                }
                catch (Exception ex2)
                {
                    return ex2.Message;
                }
            }
            return string.Empty;
        }

        public static void ReCommitLogWriteToDB()
        {
            var coll = new ConsolidationCommitLogCollection();
            coll.Query.Where(coll.Query.IsError == true);
            coll.Query.Select(coll.Query.CommitID);
            coll.LoadAll();

            foreach (var item in coll)
            {
                var log = new ConsolidationCommitLog();
                if (log.LoadByPrimaryKey(item.CommitID ?? 0))
                {
                    CommitLogWriteToDB(log);
                }
            }
        }


        public static string ConsolidationLogToConsolidationLogJson(string healthcareID, bool isManualLog,
            int? fromConsolidationLogID, int? toConsolidationLogID, out string summary)
        {
            // Ambil ConsolidationLog
            var qrHd = new ConsolidationLogQuery();
            qrHd.Where(qrHd.ConsolidationLogID >= fromConsolidationLogID && qrHd.ConsolidationLogID <= toConsolidationLogID && qrHd.HealthcareID == healthcareID && qrHd.IsManualLog == isManualLog);

            var collHd = new ConsolidationLogCollection();
            if (!collHd.Load(qrHd))
            {
                summary = string.Empty;
                return string.Empty;
            }

            var sumList = new List<UpdateSummary>();
            var dataTransfer = new DataTransfer { DataTransferHeaders = new List<DataTransferHeader>() };
            foreach (var consolidationLog in collHd)
            {
                //Summary
                UpdateSummary sum;
                var sums = from s in sumList where s.TableName == consolidationLog.TableName select s;
                if (sums.Count() == 0)
                {
                    sum = new UpdateSummary() { TableName = consolidationLog.TableName };
                    sumList.Add(sum);
                }
                else
                {
                    sum = sums.First();
                }

                switch (consolidationLog.ConsolidationType)
                {
                    case "C": sum.C++; break;
                    case "U": sum.U++; break;
                    case "D": sum.D++; break;
                }


                //Collect header log
                var hd = new DataTransferHeader
                {
                    ConsolidationType = consolidationLog.ConsolidationType,
                    PrimaryKeyData = consolidationLog.PrimaryKeyData,
                    TableName = consolidationLog.TableName,
                    DataTransferDetails = new List<DataTransferDetail>()
                };


                // Collect detail
                // kalau delete D tidak perlu ambil detail datanya karena delete cuma perlu primary key di header
                if (consolidationLog.ConsolidationType != "D")
                {
                    var qrDt = new ConsolidationLogDataQuery();
                    qrDt.Where(qrDt.ConsolidationLogID == consolidationLog.ConsolidationLogID);

                    var collDt = new ConsolidationLogDataCollection();
                    if (collDt.Load(qrDt))
                    {
                        foreach (var consolidationLogData in collDt)
                        {
                            var dt = new DataTransferDetail
                            {
                                ColumnName = consolidationLogData.ColumnName,
                                IsInPrimaryKey = consolidationLogData.IsInPrimaryKey,
                                NewValue = consolidationLogData.NewValue,
                                OldValue = consolidationLogData.OldValue
                            };
                            hd.DataTransferDetails.Add(dt);
                        }
                    }
                }

                dataTransfer.DataTransferHeaders.Add(hd);
            }

            summary = string.Empty;
            foreach (var sum in sumList)
            {
                summary += string.Format("{0} : C[{1}], U[{2}], D[{3}]{4}", sum.TableName, sum.C, sum.U, sum.D,
                    Environment.NewLine);
            }

            return JsonConvert.SerializeObject(dataTransfer);
        }


        /// <summary>
        /// fungsi untuk mengubah ConsolidationLog ke DataTransfer format JSON
        /// </summary>
        /// <param name="healthcareID"></param>
        /// <param name="isManualLog"></param>
        /// <param name="lastConsolidationLogID"></param>
        /// <param name="startConsolidationLogID"></param>
        /// <param name="endConsolidationLogID"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        public static string ConsolidationLogToConsolidationLogJson(string requestByHealthcareID, bool isManualLog, long? lastConsolidationLogID,
            out long? startConsolidationLogID, out long? endConsolidationLogID, out string summary)
        {

            startConsolidationLogID = 0;
            endConsolidationLogID = 0;

            // Ambil ConsolidationLog
            var dataTransfer = new DataTransfer { DataTransferHeaders = new List<DataTransferHeader>() };

            var qrHd = new ConsolidationLogQuery();
            if (isManualLog == false)
            {
                // Ambil data hasil automatic log (kasus untuk data2 master)
                qrHd.Where(qrHd.ConsolidationLogID > lastConsolidationLogID &&
                           qrHd.IsManualLog == false);
            }
            else
            {
                qrHd.Where(qrHd.ConsolidationLogID > lastConsolidationLogID && qrHd.HealthcareID == requestByHealthcareID &&
                           qrHd.IsManualLog == isManualLog);
            }

            var collHd = new ConsolidationLogCollection();
            if (!collHd.Load(qrHd))
            {
                summary = string.Empty;
                return string.Empty;
            }

            var sumList = new List<UpdateSummary>();

            foreach (ConsolidationLog consolidationLog in collHd)
            {
                //Summary
                UpdateSummary sum;
                var sums = from s in sumList where s.TableName == consolidationLog.TableName select s;
                if (sums.Count() == 0)
                {
                    sum = new UpdateSummary() { TableName = consolidationLog.TableName };
                    sumList.Add(sum);
                }
                else
                {
                    sum = sums.First();
                }

                switch (consolidationLog.ConsolidationType)
                {
                    case "C": sum.C++; break;
                    case "U": sum.U++; break;
                    case "D": sum.D++; break;
                }

                // Ambil start & end ConsolidationLogID
                if (startConsolidationLogID == 0)
                    startConsolidationLogID = consolidationLog.ConsolidationLogID;

                endConsolidationLogID = consolidationLog.ConsolidationLogID;


                //Collect header log
                var hd = new DataTransferHeader
                {
                    ConsolidationType = consolidationLog.ConsolidationType,
                    PrimaryKeyData = consolidationLog.PrimaryKeyData,
                    TableName = consolidationLog.TableName,
                    DataTransferDetails = new List<DataTransferDetail>()
                };


                // Collect detail
                // kalau delete D tidak perlu ambil detail datanya karena delete cuma perlu primary key di header
                if (consolidationLog.ConsolidationType != "D")
                {
                    var qrDt = new ConsolidationLogDataQuery();
                    qrDt.Where(qrDt.ConsolidationLogID == consolidationLog.ConsolidationLogID);

                    var collDt = new ConsolidationLogDataCollection();
                    if (collDt.Load(qrDt))
                    {
                        foreach (ConsolidationLogData ConsolidationLogData in collDt)
                        {
                            var dt = new DataTransferDetail
                            {
                                ColumnName = ConsolidationLogData.ColumnName,
                                IsInPrimaryKey = ConsolidationLogData.IsInPrimaryKey,
                                NewValue = ConsolidationLogData.NewValue,
                                OldValue = ConsolidationLogData.OldValue
                            };
                            hd.DataTransferDetails.Add(dt);
                        }
                    }
                }

                // Add ConsolidationLog
                dataTransfer.DataTransferHeaders.Add(hd);
            }

            summary = string.Empty;
            foreach (var sum in sumList)
            {
                summary += string.Format("{0} : C[{1}], U[{2}], D[{3}]{4}", sum.TableName, sum.C, sum.U, sum.D,
                    Environment.NewLine);
            }

            return JsonConvert.SerializeObject(dataTransfer);
        }

        private class UpdateSummary
        {
            public string TableName = string.Empty;
            public int C = 0;
            public int U = 0;
            public int D = 0;
        }

        private static string SaveLogToDatabaseProduction(List<DataTransferHeader> dataHds, bool isManualLog)
        {
            var sumList = new List<UpdateSummary>();
            foreach (DataTransferHeader dataHd in dataHds)
            {
                // Protect untuk table ItemBalance dan ItemBalanceDetail jangan sampai diupdate atau didelete 
                // dari pusat krn transaksi pengeluarannya hanya ada diklinik
                var tableNameLower = dataHd.TableName.ToLower();
                if (tableNameLower == "itembalance" || tableNameLower == "itembalancedetail")
                {
                    if (dataHd.ConsolidationType == "U" || dataHd.ConsolidationType == "D")
                    {
                        continue;
                    }
                }

                // summary
                UpdateSummary sum;
                var sums = from s in sumList where s.TableName == dataHd.TableName select s;
                if (!sums.Any())
                {
                    sum = new UpdateSummary() { TableName = dataHd.TableName };
                    sumList.Add(sum);
                }
                else
                {
                    sum = sums.First();
                }


                var ent = CreateEntity(dataHd);
                var dataDts = dataHd.DataTransferDetails;


                // Jika Manual Log Data Jurnal
                // Hapus semua detil dulu
                if (isManualLog)
                {
                    if (ent.es.Source.ToLower().Equals("journaltransactions"))
                    {
                        var journalHd = new JournalTransactions();
                        if (journalHd.Load(dataHd.PrimaryKeyData))
                        {
                            var journalDtColl = new JournalTransactionDetailsCollection();
                            journalDtColl.Query.Where(journalDtColl.Query.JournalId == journalHd.JournalId);
                            journalDtColl.LoadAll();
                            journalDtColl.MarkAllAsDeleted();
                            journalDtColl.Save();
                        }
                    }
                }

                if (dataHd.ConsolidationType == "C" || dataHd.ConsolidationType == "U")
                {
                    // Untuk menghindari konflik duplicate key data baru maupun update dicoba dgn meload dulu kecuali 
                    // Record baru dgn type key binary yg tandanya pada pk mengandung ''
                    if (!dataHd.PrimaryKeyData.Contains("''"))
                    {
                        if (tableNameLower == "itembalance" || tableNameLower == "itembalancedetail")
                        {
                            if (ent.Load(dataHd.PrimaryKeyData))
                                continue; // Cegah ada update itembalance
                        }
                        else if (!ent.Load(dataHd.PrimaryKeyData))
                        {
                            ent = CreateEntity(dataHd);
                            ent.AddNew();
                            dataHd.ConsolidationType = "C"; //Rubah jadi C krn data tidak ada dan dibuat menjadi insert
                        }
                    }

                    foreach (DataTransferDetail dataDt in dataDts)
                    {
                        if (dataDt.NewValue == null)
                        {
                            // kalau null gak usah di-set aja biar pas insert atau update nilainya null
                            continue;
                        }
                        switch (dataHd.ConsolidationType)
                        {
                            case "C":
                                ent.SetColumn(dataDt.ColumnName, dataDt.NewValue);
                                break;
                            case "U":
                                // Primary key disimpan di OldValue sedangkan perubahan disimpan di NewValue
                                ent.SetColumn(dataDt.ColumnName,
                                    dataDt.IsInPrimaryKey ?? false ? dataDt.OldValue : dataDt.NewValue);
                                break;
                        }
                    }

                    if (dataHd.ConsolidationType == "C")
                        sum.C++;
                    else
                        sum.U++;

                    ent.Save(false);
                }
                else if (dataHd.ConsolidationType == "D")
                {
                    // Delete
                    if (ent.Load(dataHd.PrimaryKeyData))
                    {
                        sum.D++;
                        ent.MarkAsDeleted();
                        ent.Save(false);
                    }
                }
            }
            string summary = string.Empty;
            foreach (var sum in sumList)
            {
                summary += string.Format("{0} : C[{1}], U[{2}], D[{3}]{4}", sum.TableName, sum.C, sum.U, sum.D,
                    Environment.NewLine);
            }
            return summary;
        }

        private static esEntityWAuditLog CreateEntity(DataTransferHeader dataHd)
        {
            return Temiang.Avicenna.BusinessObject.Common.Utils.GetEntity(dataHd.TableName.Replace("_", ""));
        }
    }
}