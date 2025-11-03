using System;
using System.Data;
using System.Web;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    public class esEntityWAuditLog : esEntity
    {
        private int _logID;
        public bool _isColsDefLoaded = false;

        public esEntityWAuditLog()
        {
        }

        public esEntityWAuditLog(DataRow row)
            : base(row)
        {
        }

        public void LoadColsDef()
        {
            var dtb = Common.Utils.LoadDataTable("sp_getFieldLength '" + this.es.Source + "'");
            foreach (esColumnMetadata column in this.es.Meta.Columns)
            {
                var dr = dtb.AsEnumerable().Where(r => r["column_name"].ToString() == column.Name).FirstOrDefault();
                if (dr != null)
                {
                    column.CharacterMaxLength = System.Convert.ToInt64(dr["character_maximum_length"]);
                }
            }
            _isColsDefLoaded = true;
        }

        public void LoadColsDefFromAnotherEntity(esEntityWAuditLog anotherEntity)
        {
            foreach (esColumnMetadata column in this.es.Meta.Columns)
            {
                column.CharacterMaxLength = anotherEntity.es.Meta.Columns.FindByColumnName(column.Name).CharacterMaxLength;
            }
            _isColsDefLoaded = true;
        }

        public void DoValidateByColsDef()
        {
            // kalau ada value yang lebih panjang dari fieldnya maka potong saja textnya
            foreach (esColumnMetadata column in this.es.Meta.Columns)
            {
                if (column.CharacterMaxLength > 0)
                {
                    var colVal = this.GetColumn(column.Name);
                    if (colVal != DBNull.Value && colVal != null) // Bug fix value selain berisi DBNull.Value ada juga yg berisi null (Handono 240123)
                    {
                        if (colVal.ToString().Length > column.CharacterMaxLength)
                        {
                            this.SetColumn(column.Name, HelperMirror.CutText(
                                colVal.ToString(),
                                System.Convert.ToInt32(column.CharacterMaxLength)));
                        }
                    }
                }
            }
        }

        private void ValidateByColsDef()
        {
            if (es.IsDeleted) return;
            if (es.IsModified || es.IsAdded)
            {
                if (_isColsDefLoaded)
                {
                    DoValidateByColsDef();
                }
                else
                {
                    LoadColsDef();
                    DoValidateByColsDef();
                }
            }
        }

        public override void Save()
        {
            Save(true);
        }

        public void BaseSave()
        {
            base.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordToConsolidationLog">simpan ke ConsolidationLog  atau tidak. khusus untuk save dari web service konsolidasi 
        /// dan program konsolidasi set recordToConsolidationLog ke false supaya tidak recursive</param>
        public void Save(bool recordToConsolidationLog)
        {
            var logType = (es.IsAdded ? "C" : es.IsDeleted ? "D" : "U");
            bool? isAuditLog = null;
            string[] excAuditCols = null;
            bool? isConsolidationHOToBranch = null;

            // Save AuditLog for Modified & Delete
            if ((logType == "U" || logType == "D") && !es.Source.Equals("AuditLog") && !es.Source.Equals("AuditLogData")
                    && !es.Source.Equals("ConsolidationLog") && !es.Source.Equals("ConsolidationLogData"))
            {
                SaveLog(recordToConsolidationLog, logType, isAuditLog, excAuditCols, isConsolidationHOToBranch);
            }

            this.PopulateCommonField();

            // validate by field length
            ValidateByColsDef();

            // Save
            base.Save();

            // Save AuditLog for Added ..identity baru didapat setelah save
            if (logType == "C" && !es.Source.Equals("AuditLog") && !es.Source.Equals("AuditLogData")
                    && !es.Source.Equals("ConsolidationLog") && !es.Source.Equals("ConsolidationLogData"))
            {
                SaveLog(recordToConsolidationLog, logType, isAuditLog, excAuditCols, isConsolidationHOToBranch);
            }
        }


        private class AuditLogSettingItem
        {
            public bool IsAuditLog { get; set; }
            public bool IsConsolidationHOToBranch { get; set; }
            public string ExcludeAuditColumn { get; set; }

        }

        private static Dictionary<string, AuditLogSettingItem> _auditLogSettingDict = new Dictionary<string, AuditLogSettingItem>();
        private static bool? _isAuditLogSetExist = null;

        private void SaveLog(bool recordToConsolidationLog, string logType, bool? isAuditLog, string[] excAuditCols, bool? isConsolidationHOToBranch)
        {
            if (_auditLogSettingDict.ContainsKey(es.Source))
            {
                var auSet = _auditLogSettingDict[es.Source];
                if (auSet != null)
                {
                    isAuditLog = auSet.IsAuditLog;
                    isConsolidationHOToBranch = auSet.IsConsolidationHOToBranch;
                    if (!string.IsNullOrEmpty(auSet.ExcludeAuditColumn))
                    {
                        if (!auSet.ExcludeAuditColumn.Contains(","))
                            auSet.ExcludeAuditColumn = string.Concat(auSet.ExcludeAuditColumn, ",");

                        excAuditCols = auSet.ExcludeAuditColumn.Split(',');
                    }
                    else
                        excAuditCols = null;
                }
                else
                {
                    isAuditLog = false;
                    isConsolidationHOToBranch = false;
                    excAuditCols = null;
                }
            }

            if (_isAuditLogSetExist == null)
            {
                // Check AuditLogSetting recotd exist true
                var alsCheck = new AuditLogSetting();
                alsCheck.Query.es.Top = 1;
                alsCheck.Query.Where(alsCheck.Query.Or(alsCheck.Query.IsAuditLog == true, alsCheck.Query.IsConsolidationBranchToHO == true));
                _isAuditLogSetExist = alsCheck.Query.Load();
            }

            // Check default setting
            if ((_isAuditLogSetExist ?? false) && (isAuditLog == null || isConsolidationHOToBranch == null))
            {
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

                    // Add dict
                    var item = new AuditLogSettingItem();
                    item.IsAuditLog = als.IsAuditLog ?? false;
                    item.IsConsolidationHOToBranch = als.IsConsolidationHOToBranch ?? false;
                    item.ExcludeAuditColumn = als.ExcludeAuditColumn;
                    _auditLogSettingDict.Add(es.Source, item);

                }
                else
                    _auditLogSettingDict.Add(es.Source, null);
            }

            if (isAuditLog ?? false)
                SaveAuditLog(logType, excAuditCols);

            // Save ConsolidationLog if recordToConsolidationLog = true and isConsolidationHOToBranch = true
            if (recordToConsolidationLog && (isConsolidationHOToBranch ?? false))
                SaveConsolidationLog(logType, false, string.Empty); // healthcareID empty = for all klinik (department)
        }

        #region AuditLog
        private void SaveAuditLog(string logType, string[] excAuditCols)
        {
            var excCommonFields = new string[] { "CreatedByUserID", "CreateByUserID", "CreatedDateTime", "CreateDateTime", "LastUpdateByUserID", "LastUpdateDateTime", "LastUpdatedByUserID", "LastUpdatedDateTime" };
            if (excAuditCols == null)
                excAuditCols = excCommonFields;
            else
                excAuditCols = excAuditCols.Concat(excCommonFields).ToArray();

            if (logType == "D") // 1) Audit Deletes
                SaveAuditLog(logType, excAuditCols, this);
            else if (logType == "U") // 2) Audit Update
                SaveAuditLog(logType, excAuditCols, this);
            else if (logType == "C") // 3) Audit Insert
                SaveAuditLog(logType, excAuditCols, this);
        }

        private void SaveAuditLog(string logType, string[] excAuditCols, esEntity item)
        {
            //Kode harus sama dgn yg di EntityCollectionBase
            var metadata = Meta;
            var primaryKeyData = "";
            foreach (esColumnMetadata col in metadata.Columns)
            {
                if (col.IsInPrimaryKey)
                {
                    primaryKeyData += string.Format("{0}='{1}' AND ", col.Name,
                                                    logType == "D"
                                                        ? item.GetOriginalColumnValue(col.Name)
                                                        : item.GetColumn(col.Name));
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
                    logData.ColumnName = column.Name;
                    logData.IsInPrimaryKey = column.IsInPrimaryKey;
                    if (logType == "D" || logType == "U")
                    {
                        if (item.GetOriginalColumnValue(column.Name) == DBNull.Value)
                            logData.str.OldValue = string.Empty;
                        else
                            logData.OldValue = Convert.ToString(item.GetOriginalColumnValue(column.Name));
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
                            logData.NewValue = Convert.ToString(item.GetColumn(column.Name));
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
                log.TableName = es.Source;
                log.AuditActionType = logType;
                log.PrimaryKeyData = primaryKeyData;
                try
                {
                    log.ActionByUserID = ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID;
                }
                catch
                {
                    log.ActionByUserID = "WEBSERVICE";
                }
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

        #endregion

        #region ConsolidationLog
        public int? SaveConsolidationLog(string logType, string healthcareID)
        {
            return SaveConsolidationLog(logType, true, healthcareID);
        }
        private int? SaveConsolidationLog(string logType, bool isManualLog, string healthcareID)
        {
            var log = new ConsolidationLog();
            var logDatas = new ConsolidationLogDataCollection();

            log.Query.Select(log.Query.ConsolidationLogID.Max());
            if (log.Query.Load())
                _logID = log.ConsolidationLogID ?? 0;

            log = new ConsolidationLog();
            switch (logType)
            {
                case "D":
                    log = PrepareConsolidationLog(logDatas, logType, this, isManualLog, healthcareID);
                    break;
                case "U":
                    log = PrepareConsolidationLog(logDatas, logType, this, isManualLog, healthcareID);
                    break;
                case "C":
                    log = PrepareConsolidationLog(logDatas, logType, this, isManualLog, healthcareID);
                    break;
            }

            //Save Log
            log.Save();
            logDatas.Save();
            return log.ConsolidationLogID;
        }

        private ConsolidationLog PrepareConsolidationLog(ConsolidationLogDataCollection logDatas, string logType,
                                     esEntity item, bool isManualLog, string healthcareID)
        {
            var metadata = Meta;
            var primaryKeyData = "";
            foreach (esColumnMetadata col in metadata.Columns)
            {
                if (col.IsInPrimaryKey)
                {
                    primaryKeyData += string.Format("{0}='{1}' AND ", col.Name,
                                                    logType == "D"
                                                        ? item.GetOriginalColumnValue(col.Name)
                                                        : item.GetColumn(col.Name));
                }
            }
            primaryKeyData = primaryKeyData.Substring(0, primaryKeyData.Length - 5);

            //ConsolidationLog
            _logID++;
            var log = new ConsolidationLog
            {
                ConsolidationLogID = _logID,
                TableName = es.Source,
                ConsolidationType = logType,
                PrimaryKeyData = primaryKeyData,
                LogDateTime = DateTime.Now,
                IsManualLog = isManualLog
            };
            log.str.HealthcareID = healthcareID;

            // ConsolidationLogData
            foreach (esColumnMetadata column in metadata.Columns)
            {
                // Semua field diambil untuk mengatasi jika update tetapi record belum ada di DB tujuan konsolidasi

                var logData = logDatas.AddNew();
                logData.ConsolidationLogID = log.ConsolidationLogID;
                logData.ColumnName = column.Name;
                logData.IsInPrimaryKey = column.IsInPrimaryKey;
                if (logType == "D" || logType == "U")
                {
                    //logData.OldValue = Convert.ToString(item.GetOriginalColumnValue(column.Name));
                    if (item.GetOriginalColumnValue(column.Name) is System.DBNull || item.GetOriginalColumnValue(column.Name) == null)
                    {
                        // set null
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
                    //logData.NewValue = Convert.ToString(item.GetColumn(column.Name));
                    if (item.GetColumn(column.Name) is System.DBNull || item.GetColumn(column.Name) == null)
                    {
                        // gak usah diset aja biar jadi null
                        logData.str.NewValue = string.Empty;
                    }
                    else
                    {
                        logData.NewValue = Convert.ToString(item.GetColumn(column.Name));
                        //consolidationdCols = string.Concat(consolidationdCols, "|", column.Name);
                    }
                }
                else
                {
                    logData.NewValue = "";
                }
            }
            return log;
        }

        #endregion
        protected void UpdateLastUpdateInformation()
        {
            if (this.es.IsAdded)
            {

                return;
            }

            if (this.es.IsModified)
            {
                var metadata = Meta;
                foreach (esColumnMetadata column in metadata.Columns)
                {
                    GetOriginalColumnValue(column.Name).Equals(GetColumn(column.Name));
                }
            }
        }
        public bool Load(string whereClause)
        {
            var whereClauseLower = whereClause.ToLower();
            if (whereClauseLower.Contains("delete"))
                throw new Exception("Load parameter whereClause can't contain DELETE keyword");

            if (whereClauseLower.Contains("drop"))
                throw new Exception("Load parameter whereClause can't contain DROP keyword");

            var sqlText = "SELECT * FROM ";
            sqlText += "[" + this.es.Source + "]";
            sqlText += " WHERE ";
            sqlText += whereClause;

            return this.Load(esQueryType.Text, sqlText);
        }

        public string PrimaryKeyData()
        {
            var primaryKeyData = string.Empty;
            foreach (esColumnMetadata column in this.es.Meta.Columns)
            {
                if (!column.IsInPrimaryKey) continue;
                if (column.esType == esSystemType.String || column.esType == esSystemType.DateTime)
                    primaryKeyData += string.Format("{0}='{1}' AND ", column.Name, this.GetColumn(column.Name));
                else
                    primaryKeyData += string.Format("{0}={1} AND ", column.Name, this.GetColumn(column.Name));
            }
            primaryKeyData = primaryKeyData.Substring(0, primaryKeyData.Length - 5);
            return primaryKeyData;
        }


    }
}