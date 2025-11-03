using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public class MovementHistoryDataSource
    {
        private readonly AssetMovement entity;

        public MovementHistoryDataSource(AssetMovement entity)
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

        public string AssetMovementNo
        {
            get { return this.entity.AssetMovementNo; }
        }

        public DateTime MovementDate
        {
            get { return this.entity.MovementDate.Value; }
        }

        public string FromServiceUnitId
        {
            get { return this.entity.FromServiceUnitID; }
        }

        public string FromServiceUnitName
        {
            get { return this.entity.FromServiceUnitName; }
        }

        public string FromLocationId
        {
            get { return this.entity.FromAssetLocationID; }
        }

        public string FromLocationName
        {
            get { return this.entity.FromLocationName; }
        }

        public string ToServiceUnitId
        {
            get { return this.entity.ToServiceUnitID; }
        }

        public string ToServiceUnitName
        {
            get { return this.entity.ToServiceUnitName; }
        }

        public string ToLocationId
        {
            get { return this.entity.ToServiceUnitID; }
        }

        public string ToLocationName
        {
            get { return this.entity.ToLocationName; }
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
