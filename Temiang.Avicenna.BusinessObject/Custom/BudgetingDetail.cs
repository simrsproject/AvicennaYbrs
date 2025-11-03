using System;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BudgetingDetail
    {

    }

    public partial class BudgetingDetailCollection
    {
        public bool LoadByBudgetingNoRevisionNo(string BudgetingNo, int Revision) {
            this.ClearData();
            this.Query.Where(this.Query.BudgetingNo == BudgetingNo, this.Query.Revision == Revision);
            return this.LoadAll();
        }

        public void SetByBudgetingDetailItem(Budgeting bg, BudgetingDetailItemCollection bdiColl) {
            if (bg.IsByItem ?? false)
            {
                // load items
                var iColl = new ItemCollection();
                iColl.Query.Where(iColl.Query.ItemID.In(bdiColl.Select(b => b.ItemID)));
                iColl.Query.Select(iColl.Query.ItemID, iColl.Query.ProductAccountID);
                iColl.LoadAll();
                // load settingan coa
                var supamColl = new ServiceUnitProductAccountMappingCollection();
                supamColl.Query.Where(supamColl.Query.ServiceUnitId == bg.ServiceUnitID);
                supamColl.Query.Select(
                    supamColl.Query.LocationId, supamColl.Query.ServiceUnitId,
                    supamColl.Query.ProductAccountId, supamColl.Query.ChartOfAccountIdExpense);
                supamColl.LoadAll();

                LoadByBudgetingNoRevisionNo(bg.BudgetingNo, bg.Revision.Value);

                foreach (var bd in this)
                {
                    bd.Limit01 = -1;
                    bd.Limit02 = -1;
                    bd.Limit03 = -1;
                    bd.Limit04 = -1;
                    bd.Limit05 = -1;
                    bd.Limit06 = -1;
                    bd.Limit07 = -1;
                    bd.Limit08 = -1;
                    bd.Limit09 = -1;
                    bd.Limit10 = -1;
                    bd.Limit11 = -1;
                    bd.Limit12 = -1;
                }

                foreach (var bdi in bdiColl)
                {
                    bdi.ChartOfAccountID = 0;
                    var item = iColl.Where(i => i.ItemID == bdi.ItemID).FirstOrDefault();
                    if (item != null)
                    {
                        if (!string.IsNullOrEmpty(item.ProductAccountID))
                        {
                            var coaid = supamColl.Where(s => s.ProductAccountId == item.ProductAccountID && s.ChartOfAccountIdExpense != 0)
                                .Select(s => s.ChartOfAccountIdExpense).FirstOrDefault();
                            if (coaid.HasValue)
                            {
                                bdi.ChartOfAccountID = coaid.Value;
                            }
                        }
                    }

                    if (bdi.ChartOfAccountID != 0)
                    {
                        var bd = this.Where(d => d.ChartOfAccountID == bdi.ChartOfAccountID).FirstOrDefault();
                        if (bd == null)
                        {
                            bd = new BudgetingDetail();
                            bd.BudgetingNo = bg.BudgetingNo;
                            bd.Revision = bg.Revision;
                            bd.ChartOfAccountID = bdi.ChartOfAccountID;
                            this.AttachEntity(bd);
                        }

                        if (bd.Limit01 == -1 || (!bd.Limit01.HasValue)) bd.Limit01 = 0;
                        bd.Limit01 += bdi.QtyMonth01 * bdi.Price;
                        if (bd.Limit02 == -1 || (!bd.Limit02.HasValue)) bd.Limit02 = 0;
                        bd.Limit02 += bdi.QtyMonth02 * bdi.Price;
                        if (bd.Limit03 == -1 || (!bd.Limit03.HasValue)) bd.Limit03 = 0;
                        bd.Limit03 += bdi.QtyMonth03 * bdi.Price;
                        if (bd.Limit04 == -1 || (!bd.Limit04.HasValue)) bd.Limit04 = 0;
                        bd.Limit04 += bdi.QtyMonth04 * bdi.Price;
                        if (bd.Limit05 == -1 || (!bd.Limit05.HasValue)) bd.Limit05 = 0;
                        bd.Limit05 += bdi.QtyMonth05 * bdi.Price;
                        if (bd.Limit06 == -1 || (!bd.Limit06.HasValue)) bd.Limit06 = 0;
                        bd.Limit06 += bdi.QtyMonth06 * bdi.Price;
                        if (bd.Limit07 == -1 || (!bd.Limit07.HasValue)) bd.Limit07 = 0;
                        bd.Limit07 += bdi.QtyMonth07 * bdi.Price;
                        if (bd.Limit08 == -1 || (!bd.Limit08.HasValue)) bd.Limit08 = 0;
                        bd.Limit08 += bdi.QtyMonth08 * bdi.Price;
                        if (bd.Limit09 == -1 || (!bd.Limit09.HasValue)) bd.Limit09 = 0;
                        bd.Limit09 += bdi.QtyMonth09 * bdi.Price;
                        if (bd.Limit10 == -1 || (!bd.Limit10.HasValue)) bd.Limit10 = 0;
                        bd.Limit10 += bdi.QtyMonth10 * bdi.Price;
                        if (bd.Limit11 == -1 || (!bd.Limit11.HasValue)) bd.Limit11 = 0;
                        bd.Limit11 += bdi.QtyMonth11 * bdi.Price;
                        if (bd.Limit12 == -1 || (!bd.Limit12.HasValue)) bd.Limit12 = 0;
                        bd.Limit12 += bdi.QtyMonth12 * bdi.Price;
                    }
                }
            }
            else
            {
                throw new Exception("Method available for budgeting by item only");
            }
        }
        public void SetByBudgetingDetailItemBudgetingNoRevisionNo(string BudgetingNo, int Revision) {
            var bg = new Budgeting();
            if (bg.LoadByPrimaryKey(BudgetingNo)) {
                var bdiColl = new BudgetingDetailItemCollection();
                bdiColl.LoadByBudgetingNoRevisionNo(BudgetingNo, Revision);

                SetByBudgetingDetailItem(bg, bdiColl);
            }
        }
    }
}
