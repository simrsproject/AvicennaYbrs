using System;
using System.Data;
using System.Linq;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BudgetingDetailItem
    {
        public string ItemName
        {
            get { return GetColumn("refTo_ItemName").ToString(); }
            set { SetColumn("refTo_ItemName", value); }
        }
    }

    public partial class BudgetingDetailItemCollection
    {
        public bool LoadByBudgetingNoRevisionNo(string BudgetingNo, int Revision)
        {
            this.ClearData();
            this.Query.Where(this.Query.BudgetingNo == BudgetingNo, this.Query.Revision == Revision);
            return this.LoadAll();
        }

        #region Budgeting By Item
        private bool LoadBudgeting(string ServiceUnitID, string ItemID, int year, string BudgetingNoException) {
            var bdi = new BudgetingDetailItemQuery("bdi");
            var bd = new BudgetingQuery("bd");

            bdi.InnerJoin(bd).On(bdi.BudgetingNo == bd.BudgetingNo && bdi.Revision == bd.Revision);
            bdi.Where(bd.IsVoid == false, bd.SRBudgetingVerifyStatus == "03",
                bd.Periode == year, bd.IsApprove == true,
                bd.ServiceUnitID == ServiceUnitID,
                bdi.ItemID == ItemID,
                bdi.BudgetingNo != BudgetingNoException);
            bdi.Select(bdi);

            this.ClearData();
            return (this.Load(bdi));
        }
        public decimal GetCountBudgeting(string ServiceUnitID, string ItemID, int year, string BudgetingNoException)
        {
            LoadBudgeting(ServiceUnitID, ItemID, year, BudgetingNoException);
            return this.Sum(x => (x.Qty * x.ConversionFactor)) ?? 0;
        }

        public decimal GetCountBudgeting(string ServiceUnitID, string ItemID, int year, int month, string BudgetingNoException)
        {
            LoadBudgeting(ServiceUnitID, ItemID, year, BudgetingNoException);
            switch (month) {
                case 1: {
                        return this.Sum(x => (x.QtyMonth01 * x.ConversionFactor)) ?? 0;
                    }
                case 2:
                    {
                        return this.Sum(x => (x.QtyMonth02 * x.ConversionFactor)) ?? 0;
                    }
                case 3:
                    {
                        return this.Sum(x => (x.QtyMonth03 * x.ConversionFactor)) ?? 0;
                    }
                case 4:
                    {
                        return this.Sum(x => (x.QtyMonth04 * x.ConversionFactor)) ?? 0;
                    }
                case 5:
                    {
                        return this.Sum(x => (x.QtyMonth05 * x.ConversionFactor)) ?? 0;
                    }
                case 6:
                    {
                        return this.Sum(x => (x.QtyMonth06 * x.ConversionFactor)) ?? 0;
                    }
                case 7:
                    {
                        return this.Sum(x => (x.QtyMonth07 * x.ConversionFactor)) ?? 0;
                    }
                case 8:
                    {
                        return this.Sum(x => (x.QtyMonth08 * x.ConversionFactor)) ?? 0;
                    }
                case 9:
                    {
                        return this.Sum(x => (x.QtyMonth09 * x.ConversionFactor)) ?? 0;
                    }
                case 10:
                    {
                        return this.Sum(x => (x.QtyMonth10 * x.ConversionFactor)) ?? 0;
                    }
                case 11:
                    {
                        return this.Sum(x => (x.QtyMonth11 * x.ConversionFactor)) ?? 0;
                    }
                case 12:
                    {
                        return this.Sum(x => (x.QtyMonth12 * x.ConversionFactor)) ?? 0;
                    }
                default: {
                        return 0;
                    }
            }
        }
        #endregion
    }
}
