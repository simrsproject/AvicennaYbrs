using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public class MaintenanceOrderDataSource
    {
        private readonly AssetMaintenanceOrder entity;

        public MaintenanceOrderDataSource(AssetMaintenanceOrder entity)
        {
            this.entity = entity;
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

        public string JobOrderNo
        {
            get { return this.entity.JobOrderNo; }
        }

        public string RequestBy
        {
            get { return this.entity.RequestBy; }
        }

        public DateTime? OrderedDate
        {
            get { return this.entity.OrderedDate; }
        }

        public string FromServiceUnit
        {
            get { return string.Format("{0} - {1}", this.entity.FromServiceUnitID, this.entity.FromServiceUnitName); }
        }

        public string Location
        {
            get { return string.Format("{0} - {1}", this.entity.FromLocationID, this.entity.FromLocationName); }
        }

        public string ToServiceUnit
        {
            get { return string.Format("{0} - {1}", this.entity.ToServiceUnitID, this.entity.ToServiceUnitName); }
        }

        public DateTime? RequestDate
        {
            get { return this.entity.RequestDate; }
        }

        public string Description
        {
            get { return this.entity.Notes; }
        }

        public bool IsPosted
        {
            get { return this.entity.IsPosted ?? false; }
        }

        public string LastUpdateByUserID
        {
            get { return this.entity.LastUpdateByUserID; }
        }
    }
}
