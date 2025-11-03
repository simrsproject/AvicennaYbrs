using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public class MaintenanceHistoryDataSource
    {
        private readonly AssetWorkOrder entity;

        public MaintenanceHistoryDataSource(AssetWorkOrder entity)
        {
            this.entity = entity;
        }

        public string OrderNo
        {
            get { return this.entity.OrderNo; }
        }

        public string AssetId
        {
            get { return this.entity.AssetID; }
        }

        public string AssetName
        {
            get { return this.entity.AssetName; }
        }

        public string Asset
        {
            get { return string.Format("{0} - {1}", this.AssetId.Trim(), this.AssetName.Trim()); }
        }

        public string AssetMaintenanceNo
        {
            get { return this.entity.OrderNo; }
        }

        public DateTime MaintenanceDate
        {
            get { return this.entity.LastRealizationDateTime.Value; }
        }

        public string MaintenanceType
        {
            get { return this.entity.SRWorkType; }
        }

        public string Description
        {
            get { return this.entity.ProblemDescription; }
        }

        public string Condition
        {
            get { return string.Format("{0} %", this.entity.ActionTaken); }
        }

        public string MaintenanceBy
        {
            get { return this.entity.ImplementedBy; }
        }

        public bool IsPosted
        {
            get { return this.entity.IsProceed ?? false; }
        }

        public string LastUpdateByUserID
        {
            get { return this.entity.LastUpdateByUserID; }
        }
    }
}
