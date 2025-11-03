using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Item
    {
        public bool LoadByBarcode(string barcode)
        {
            esItemQuery query = this.GetDynamicQuery();
            query.es.Top = 1;
            query.Where(query.Barcode == barcode);
            return query.Load();
        }

        public override void Save()
        {
            // Check Barcode entry


            if (es.IsModified)
            {
                // Program sebelumnya memakai barcode di detil tablenya shg harus diupdate juga
                if (SRItemType == ItemType.Medical)
                {
                    var ipm = new ItemProductMedic();
                    if (ipm.LoadByPrimaryKey(ItemID))
                    {
                        ipm.Barcode = Barcode;
                        ipm.Save();
                    }
                }
                else if (SRItemType == ItemType.NonMedical)
                {
                    var ipnm = new ItemProductNonMedic();
                    if (ipnm.LoadByPrimaryKey(ItemID))
                    {
                        ipnm.Barcode = Barcode;
                        ipnm.Save();
                    }
                }
                else if (SRItemType == ItemType.Kitchen)
                {
                    var ik = new ItemKitchen();
                    if (ik.LoadByPrimaryKey(ItemID))
                    {
                        ik.Barcode = Barcode;
                        ik.Save();
                    }
                }
            }
            base.Save();
        }
    }
}
