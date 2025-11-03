using System;
using System.Configuration;
using System.Runtime.Remoting.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicationReceiveUsed
    {
        public override void Save()
        {
            using (var tran = new esTransactionScope())
            {
                long medicationReceiveNo;
                // Simpan ke variable krn kalau save dari delete akan hilang valuenya
                if (this.es.IsDeleted)
                    medicationReceiveNo = this.GetOriginalColumnValue("MedicationReceiveNo").ToInt();
                else
                    medicationReceiveNo = MedicationReceiveNo ?? 0;

                base.Save();

                // Update Balance
                var hd = new MedicationReceive();
                hd.LoadByPrimaryKey(medicationReceiveNo);

                //Hitung Balance berdasarkan yg suda disetup
                var used = new MedicationReceiveUsedQuery();
                used.Where(used.MedicationReceiveNo == medicationReceiveNo, used.SetupDateTime.IsNotNull(),
                    used.Or(used.IsReSchedule.IsNull(), used.IsReSchedule == false), used.Or(used.IsVoidSchedule.IsNull(), used.IsVoidSchedule == false));
                used.Select(used.Qty.Sum().As("TotalQty"));
                var dtbTot = used.LoadDataTable();
                hd.BalanceQty = hd.ReceiveQty - dtbTot.Rows[0][0].ToDecimal();

                //Hitung Balance berdasarkan yg sudah direalisasi
                used = new MedicationReceiveUsedQuery();
                used.Where(used.MedicationReceiveNo == medicationReceiveNo, used.RealizedDateTime.IsNotNull(),
                    used.Or(used.IsReSchedule.IsNull(), used.IsReSchedule == false), used.Or(used.IsVoidSchedule.IsNull(), used.IsVoidSchedule == false));
                used.Select(used.Qty.Sum().As("TotalQty"));
                dtbTot = used.LoadDataTable();
                hd.BalanceRealQty = hd.ReceiveQty - dtbTot.Rows[0][0].ToDecimal();

                if (hd.BalanceQty < 0 || hd.BalanceRealQty < 0)
                    if (hd.BalanceQty < 0 && hd.BalanceRealQty == 0)
                        throw new Exception("Please realization all medication setup first");
                    else
                        throw new Exception("Insufficient Balance. Save can not continue");

                hd.Save();
                tran.Complete();
            }
        }

        public static int ConsumedDay(string registrationNo, string itemID, string srConsumeMethod, string consumeQty, string srConsumeUnit)
        {
            var dayNo = 0;
            var cm = new ConsumeMethod();
            if (cm.LoadByPrimaryKey(srConsumeMethod))
            {
                // Jumlah Realisasi medication yg dikonsumsi pasien
                var med = new MedicationReceiveQuery("m");
                var medUsed = new MedicationReceiveUsedQuery("mu");
                med.InnerJoin(medUsed).On(med.MedicationReceiveNo == medUsed.MedicationReceiveNo);

                med.Where(med.RegistrationNo == registrationNo, med.ItemID == itemID,
                    med.SRConsumeMethod == srConsumeMethod,
                    med.ConsumeQtyInString == consumeQty,
                    med.SRConsumeUnit == srConsumeUnit,
                    medUsed.IsNotConsume == false,
                    medUsed.RealizedDateTime.IsNotNull());

                med.Select("<COUNT(1) as RealizationCount>");
                var dtb = med.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    if (cm.IterationQty.ToInt() > 0)
                        dayNo = Math.Ceiling(((dtb.Rows[0][0]).ToDecimal() / cm.IterationQty.ToDecimal())).ToInt();
                }
            }
            return dayNo;
        }
    }
}