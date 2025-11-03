using System;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public class AssetDepreciationPostDataSource
    {
        private readonly AssetDepreciationPost entity;

        public AssetDepreciationPostDataSource(AssetDepreciationPost entity)
        {
            this.entity = entity;
        }

        public int? PeriodeNo
        {
            get { return this.entity.PeriodNo; }
        }

        public string Period
        {
            get { return string.Format("{0} - {1}", this.entity.Year, this.entity.Month); }
        }

        public decimal? CurrentAmount
        {
            get { return this.entity.CurrentAmount; }
        }

        public decimal? DepreciationAmount
        {
            get { return this.entity.DepreciationAmount; }
        }

        public decimal? AccumulationAmount
        {
            get { return this.entity.AccumulationAmount; }
        }

        public decimal? AssetValue
        {
            get { return this.CurrentAmount - this.DepreciationAmount; }
        }

        public bool? IsPosted
        {
            get { return this.entity.IsPosted; }
        }

        public string JournalNumber
        {
            get { return this.entity.JournalNumber; }
        }

        public string LastUpdateByUserID
        {
            get { return this.entity.LastUpdateByUserID; }
        }
    }
}
