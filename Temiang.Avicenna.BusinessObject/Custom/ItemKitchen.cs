using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemKitchen
    {
        public void SetSupplierPriceInBaseUnit(string supplierID)
        {
            var su = new SupplierItem();
            if (!string.IsNullOrEmpty(supplierID))
                if (su.LoadByPrimaryKey(supplierID, this.ItemID ?? string.Empty))
                {
                    this.PriceInBaseUnit = su.PriceInPurchaseUnit / su.ConversionFactor;
                    this.PurchaseDiscount1 = su.PurchaseDiscount1;
                    this.PurchaseDiscount2 = su.PurchaseDiscount2;
                }
        }
    }
}
