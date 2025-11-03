using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ConsolidationCommitLog
    {
        public static bool IsEmptyCommitData(ConsolidationCommitLog log)
        {
            if (log == null) return true;
            return string.IsNullOrEmpty(log.CommitData);
        }
        public void SaveCustom() {
            // testing save
            if (this.Collection == null)
            {
                this.Save();
            }
            else
            {
                this.Collection.Save();
            }
        }
    }
}