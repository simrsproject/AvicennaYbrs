using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport313b
    {
        public string RlMasterReportItemCode
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemCode").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemCode", value); }
        }

        public string RlMasterReportItemName
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemName").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemName", value); }
        }

        public static void Process(string rlMasterReportItemCode, out int jmlItemObat, out int jmlItemObatRs, out int jmlItemObatFormulariumRs)
        {
            var its = new ItemCollection();
            var itq = new ItemQuery("a");
            var ipmq = new ItemProductMedicQuery("b");

            itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
            itq.Where(itq.IsActive == true);
            itq.Select(itq.ItemID);
            itq.es.Distinct = true;

            switch (rlMasterReportItemCode)
            {
                case "1": //obat generik
                    itq.Where(ipmq.IsGeneric == true);
                    break;
                case "2": //obat non generik formularium
                    itq.Where(
                        ipmq.IsNonGeneric == true,
                        ipmq.IsFormularium == true
                        );
                    break;
                case "3": //obat non generik non formulariom
                    itq.Where(
                        ipmq.IsNonGeneric == true,
                        ipmq.IsFormularium == false
                        );
                    break;
            }

            its.Load(itq);
            jmlItemObat = its.Count;
            jmlItemObatRs = jmlItemObat;

            its = new ItemCollection();
            itq = new ItemQuery("a");
            ipmq = new ItemProductMedicQuery("b");

            itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
            itq.Where(itq.IsActive == true);
            itq.Select(itq.ItemID);
            itq.es.Distinct = true;

            switch (rlMasterReportItemCode)
            {
                case "1": //obat generik
                    itq.Where(
                        ipmq.IsGeneric == true,
                        ipmq.IsFormularium == true
                        );
                    break;
                case "2": //obat non generik formularium
                    itq.Where(
                        ipmq.IsNonGeneric == true,
                        ipmq.IsFormularium == true
                        );
                    break;
                case "3": //obat non generik non formulariom
                    itq.Where(itq.ItemID == "&");
                    break;
            }

            its.Load(itq);
            jmlItemObatFormulariumRs = its.Count;
        }
    }
}
