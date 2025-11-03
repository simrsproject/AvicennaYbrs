using System;
using System.Data;
using System.Web;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    public class esEntityCollectionWAuditLog : esEntityCollection
    {
        int _auditLogID = 0;
        int _conLogID = 0;

        public override void Save()
        {
            // Save for 
            if (!es.Source.Equals("AuditLog") && !es.Source.Equals("AuditLogData")
                && !es.Source.Equals("ConsolidationLog") && !es.Source.Equals("ConsolidationLogData"))
            {
                SaveLog();

                // Update common field
                PopulateCommonFieldAllEntity();
                //// Log matrix table
                //var currentProgramID = HttpContext.Current.Session["_ProgramID"];
                //if (currentProgramID != null)
                //{
                //    var tbl = new AppProgramTable();
                //    if (!tbl.LoadByPrimaryKey(currentProgramID.ToString(), this.es.Source))
                //    {
                //        tbl.AddNew();
                //        tbl.ProgramID = currentProgramID.ToString();
                //        tbl.TableName = this.es.Source;
                //        tbl.BaseSave();
                //    }
                //}
            }

            // validate by field length
            ValidateByColsDef();

            base.Save();

            // TODO: Save untuk new record yg menggunakan Identity
            //// Save AuditLog for Added ..identity baru didapat setelah save
            //if (auditActionType == "C" && !es.Source.Equals("AuditLog") && !es.Source.Equals("AuditLogData")
            //    && !es.Source.Equals("ConsolidationLog") && !es.Source.Equals("ConsolidationLogData"))
            //    SaveAuditLog(auditActionType);
        }

        private void ValidateByColsDef()
        {
            esEntityWAuditLog entityColsDefLoaded = null;
            foreach (esEntityWAuditLog entity in this) {
                if (entity.es.IsDeleted) continue;
                if (entity.es.IsModified || entity.es.IsAdded) {
                    if (entity._isColsDefLoaded)
                    {
                        entity.DoValidateByColsDef();
                    }
                    else
                    {
                        if (entityColsDefLoaded == null)
                        {
                            entity.LoadColsDef();
                            entityColsDefLoaded = entity;
                        }
                        else
                        {
                            entity.LoadColsDefFromAnotherEntity(entityColsDefLoaded);
                        }

                        entity.DoValidateByColsDef();
                    }
                }
            }
        }

        private void PopulateCommonFieldAllEntity()
        {
            foreach (esEntity entity in this)
            {
                entity.PopulateCommonField();
            }
        }

        #region AuditLog
        private void SaveLog()
        {
            DataViewRowState state = this.RowStateFilter;
            AuditLogDataCollection logDatas = new AuditLogDataCollection();
            AuditLogCollection logs = new AuditLogCollection();

            var isAuditLog = false;
            var isConsolidationHOToBranch = false;
            string[] excAuditCols = null;
            var als = new AuditLogSetting();
            if (als.LoadByPrimaryKey(es.Source))
            {
                isAuditLog = als.IsAuditLog ?? false;
                if (!string.IsNullOrEmpty(als.ExcludeAuditColumn))
                {
                    if (!als.ExcludeAuditColumn.Contains(","))
                        als.ExcludeAuditColumn = string.Concat(als.ExcludeAuditColumn, ",");

                    excAuditCols = als.ExcludeAuditColumn.Split(',');
                }
                isConsolidationHOToBranch = als.IsConsolidationHOToBranch ?? false;
            }

            var excCommonFields = new string[] { "CreatedByUserID", "CreateByUserID", "CreatedDateTime", "CreateDateTime", "LastUpdateByUserID", "LastUpdateDateTime", "LastUpdatedByUserID", "LastUpdatedDateTime" };
            if (excAuditCols == null)
                excAuditCols = excCommonFields;
            else
                excAuditCols = excAuditCols.Concat(excCommonFields).ToArray();

            var conLogDatas = new ConsolidationLogDataCollection();
            var conLogs = new ConsolidationLogCollection();

            // 1) Audit Deletes
            this.RowStateFilter = DataViewRowState.Deleted;
            PrepareLog(conLogs, conLogDatas, "D", isAuditLog, excAuditCols, isConsolidationHOToBranch);

            // 2) Audit Update
            this.RowStateFilter = DataViewRowState.ModifiedOriginal;
            PrepareLog(conLogs, conLogDatas, "U", isAuditLog, excAuditCols, isConsolidationHOToBranch);

            // 3) Audit Insert
            this.RowStateFilter = DataViewRowState.Added;
            PrepareLog(conLogs, conLogDatas, "C", isAuditLog, excAuditCols, isConsolidationHOToBranch);

            this.RowStateFilter = state;

            //Save ConsolidationLog
            conLogs.Save();
            conLogDatas.Save();
        }

        private void PrepareLog(ConsolidationLogCollection conLogs, ConsolidationLogDataCollection conLogDatas, string logType, bool isAuditLog, string[] excAuditCols, bool isConsolidationHOToBranch)
        {
            foreach (esEntity item in this)
            {
                if (isAuditLog)
                {
                    SaveAuditLog(logType, excAuditCols, item);
                }

                if (isConsolidationHOToBranch)
                {
                    PrepareConsolidationLog(conLogs, conLogDatas, logType, item);
                }
            }
        }

        private void SaveAuditLog(string logType, string[] excAuditCols, esEntity item)
        {
            //Kode harus sama dgn yg di EntityBase
            IMetadata metadata = this.Meta;
            string primaryKeyData = "";
            foreach (esColumnMetadata col in metadata.Columns)
            {
                if (col.IsInPrimaryKey)
                {
                    primaryKeyData += string.Format("{0}='{1}' AND ", col.Name, logType == "D" ? item.GetOriginalColumnValue(col.Name) : item.GetColumn(col.Name));
                }
            }

            primaryKeyData = primaryKeyData.Substring(0, primaryKeyData.Length - 5);


            //AuditLogData
            var isDataChanged = false;
            AuditLogDataCollection logDatas = new AuditLogDataCollection();
            if (logType != "C") // Insert tidak direkam log nya untuk menghemat record (Handono 2023-11-26 by req meeting avicenna usulan BT)
            {
                foreach (esColumnMetadata column in metadata.Columns)
                {
                    if (!column.IsInPrimaryKey && excAuditCols != null && excAuditCols.Any() && excAuditCols.FirstOrDefault(c => c == column.Name) != null)
                        continue;

                    //if (!column.IsInPrimaryKey && (logType == "U" &&
                    //    item.GetOriginalColumnValue(column.Name).Equals(item.GetColumn(column.Name))))
                    //    continue;

                    if (logType == "U" &&
                        item.GetOriginalColumnValue(column.Name).Equals(item.GetColumn(column.Name)))
                        continue;

                    var logData = logDatas.AddNew();
                    logData.AuditLogID = _auditLogID;
                    logData.ColumnName = column.Name;
                    logData.IsInPrimaryKey = column.IsInPrimaryKey;
                    if (logType == "D" || logType == "U")
                    {
                        if (item.GetOriginalColumnValue(column.Name) == DBNull.Value)
                            logData.str.OldValue = string.Empty;
                        else
                            logData.OldValue = System.Convert.ToString(item.GetOriginalColumnValue(column.Name));
                    }
                    else
                    {
                        logData.OldValue = "";
                    }
                    if (logType == "U" || logType == "C")
                    {
                        if (item.GetColumn(column.Name) == DBNull.Value)
                            logData.str.NewValue = string.Empty;
                        else
                            logData.NewValue = System.Convert.ToString(item.GetColumn(column.Name));
                        isDataChanged = true;
                    }
                    else
                    {
                        logData.NewValue = "";
                    }
                }
            }

            //AuditLog
            if (logType == "D" || logType == "C" || (logType == "U" && isDataChanged))
            {
                var log = new AuditLog();
                log.TableName = this.es.Source;
                log.AuditActionType = logType;
                log.PrimaryKeyData = primaryKeyData;
                log.ActionByUserID = ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID;
                log.LogDateTime = DateTime.Now;
                log.Save();

                // Update logData.AuditLogID 
                foreach (AuditLogData logData in logDatas)
                {
                    logData.AuditLogID = log.AuditLogID;
                }
                logDatas.Save();
            }
        }

        private void PrepareConsolidationLog(ConsolidationLogCollection logs, ConsolidationLogDataCollection logDatas, string logType, esEntity item)
        {
            //Kode harus sama dgn yg di EntityBase
            ConsolidationLog log;
            ConsolidationLogData logData;
            IMetadata metadata = this.Meta;
            string primaryKeyData = "";
            foreach (esColumnMetadata col in metadata.Columns)
            {
                if (col.IsInPrimaryKey)
                {
                    primaryKeyData += string.Format("{0}='{1}' AND ", col.Name, logType == "D" ? item.GetOriginalColumnValue(col.Name) : item.GetColumn(col.Name));
                }
            }
            primaryKeyData = primaryKeyData.Substring(0, primaryKeyData.Length - 5);

            //ConsolidationLog
            if (_conLogID == 0)
            {
                var conLog = new ConsolidationLog();
                conLog.Query.Select(conLog.Query.ConsolidationLogID.Max());
                if (conLog.Query.Load())
                    _conLogID = conLog.ConsolidationLogID ?? 0;
            }

            _conLogID++;
            log = logs.AddNew();
            log.ConsolidationLogID = _conLogID;
            log.TableName = this.es.Source;
            log.ConsolidationType = logType;
            log.PrimaryKeyData = primaryKeyData;
            log.LogDateTime = DateTime.Now;
            log.IsManualLog = false;

            //ConsolidationLogData
            foreach (esColumnMetadata column in metadata.Columns)
            {
                // Semua field diambil untuk mengatasi jika update tetapi record belum ada di DB tujuan konsolidasi

                logData = logDatas.AddNew();
                logData.ConsolidationLogID = log.ConsolidationLogID;
                logData.ColumnName = column.Name;
                logData.IsInPrimaryKey = column.IsInPrimaryKey;

                if (logType == "D" || logType == "U")
                {
                    //logData.OldValue = Convert.ToString(item.GetOriginalColumnValue(column.Name));
                    if (item.GetOriginalColumnValue(column.Name) is System.DBNull || item.GetOriginalColumnValue(column.Name) == null)
                    {
                        // Isi Null
                        logData.str.NewValue = string.Empty;
                    }
                    else
                        logData.OldValue = Convert.ToString(item.GetOriginalColumnValue(column.Name));
                }
                else
                {
                    logData.OldValue = "";
                }
                if (logType == "U" || logType == "C")
                {
                    if (item.GetColumn(column.Name) is System.DBNull || item.GetColumn(column.Name) == null)
                    {
                        // Isi null
                        logData.str.NewValue = string.Empty;
                    }
                    else
                        logData.NewValue = Convert.ToString(item.GetColumn(column.Name));
                }
                else
                {
                    logData.NewValue = "";
                }
            }
        }

        #endregion
    }
}
