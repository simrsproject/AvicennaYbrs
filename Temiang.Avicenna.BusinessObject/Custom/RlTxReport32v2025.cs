using System;
using System.Data;
using System.Linq;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport32v2025
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

        public static void Process(DateTime startDate, DateTime endDate, string paramedicRl1, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48,
            out int pAwal, out int pMasuk, out int pHidup, out int pLMatiK48, out int pPMatiK48, out int pLMatiL48, out int pPMatiL48, out int pAkhir, out int lamaRawat, out int hariRawat, out int pPindahan,
            out int pDipindahkan, out int vvip, out int vip, out int i, out int ii, out int iii, out int khusus, out int AlokasiTT)
        {
            pAwal = 0;
            pMasuk = 0;
            pHidup = 0;
            pLMatiK48 = 0;
            pPMatiK48 = 0;
            pLMatiL48 = 0;
            pPMatiL48 = 0;
            pAkhir = 0;
            lamaRawat = 0;
            hariRawat = 0;
            pPindahan = 0;
            pDipindahkan = 0;
            vvip = 0;
            vip = 0;
            i = 0;
            ii = 0;
            iii = 0;
            khusus = 0;
            AlokasiTT = 0;

            var census = new CensusBalanceQuery("a");
            census.Where(census.CensusDate == startDate.Date.AddDays(-1), census.SmfID == paramedicRl1,
                         census.ClassID == string.Empty);
            census.Select(census.Balance.Sum().As("Balance"));
            DataTable dtawal = census.LoadDataTable();
            if (dtawal.Rows.Count > 0)
                pAwal = dtawal.Rows[0]["Balance"].ToInt();

            //---------------------------------
            var pth = new PatientTransferHistoryCollection();
            pth.Query.Where(pth.Query.TransferNo == string.Empty,
                            pth.Query.DateOfEntry.Date().Between(startDate, endDate),
                            pth.Query.SmfID == paramedicRl1);
            pth.LoadAll();
            pMasuk = pth.Count;

            var ptransfer = new PatientTransferCollection();
            ptransfer.Query.Where(ptransfer.Query.TransferDate.Date().Between(startDate, endDate),
                                  ptransfer.Query.IsApprove == true,
                                  ptransfer.Query.ToSpecialtyID == paramedicRl1,
                                  ptransfer.Query.ToSpecialtyID != ptransfer.Query.FromSpecialtyID);
            ptransfer.LoadAll();
            pPindahan = ptransfer.Count;

            //---------------------------------
            var regs = new RegistrationCollection();
            var regq = new RegistrationQuery("a");
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                regq.SRDischargeCondition.NotIn(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                regq.SmfID == paramedicRl1
                );

            regs.Load(regq);
            pHidup = regs.Count;

            ptransfer = new PatientTransferCollection();
            ptransfer.Query.Where(ptransfer.Query.TransferDate.Date().Between(startDate, endDate),
                                  ptransfer.Query.IsApprove == true,
                                  ptransfer.Query.FromSpecialtyID == paramedicRl1,
                                  ptransfer.Query.FromSpecialtyID != ptransfer.Query.ToSpecialtyID);
            ptransfer.LoadAll();
            pDipindahkan = ptransfer.Count;

            //---------------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            var patq = new PatientQuery("b");
            regq.InnerJoin(patq).On(patq.PatientID == regq.PatientID);
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                patq.Sex == "M",
                regq.SRDischargeCondition == dischargeConditionDieLessThen48,
                regq.SmfID == paramedicRl1
                );
            regq.Select(regq.RegistrationNo);

            regs.Load(regq);
            pLMatiK48 = regs.Count;
            //---------------------------------

            //---------------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("b");
            regq.InnerJoin(patq).On(patq.PatientID == regq.PatientID);
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                patq.Sex == "F",
                regq.SRDischargeCondition == dischargeConditionDieLessThen48,
                regq.SmfID == paramedicRl1
                );
            regq.Select(regq.RegistrationNo);

            regs.Load(regq);
            pPMatiK48 = regs.Count;


            //---------------------------------

            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("b");
            regq.InnerJoin(patq).On(patq.PatientID == regq.PatientID);
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                patq.Sex == "M",
                regq.SRDischargeCondition == dischargeConditionDieMoreThen48,
                regq.SmfID == paramedicRl1
                );

            regs.Load(regq);
            pLMatiL48 = regs.Count;
            //---------------------------------
            //---------------------------------

            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("b");
            regq.InnerJoin(patq).On(patq.PatientID == regq.PatientID);
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                patq.Sex == "F",
                regq.SRDischargeCondition == dischargeConditionDieMoreThen48,
                regq.SmfID == paramedicRl1
                );

            regs.Load(regq);
            pPMatiL48 = regs.Count;
            //---------------------------------
            pAkhir = pAwal + pMasuk - pHidup - pLMatiK48 - pPMatiK48 - pLMatiL48 - pPMatiL48 + pPindahan - pDipindahkan;
            //---------------------------------

            regq = new RegistrationQuery("a");
            regq.Select(@"<DATEDIFF(DAY, a.RegistrationDate, a.DischargeDate) AS 'lr'>");
            regq.Where(
                regq.DischargeDate.Date().Between(startDate, endDate),
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                regq.SmfID == paramedicRl1
                );

            DataTable dtlr = regq.LoadDataTable();
            if (dtlr.Rows.Count > 0)
            {
                lamaRawat += dtlr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["lr"]) == 0 ? 1 : Convert.ToInt16(row["lr"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1, census.ClassID == string.Empty);
            DataTable dthr = census.LoadDataTable();
            if (dthr.Rows.Count > 0)
            {
                hariRawat += dthr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            var cq = new ClassQuery("c");
            var stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "VVIP"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtvviphr = census.LoadDataTable();
            if (dtvviphr.Rows.Count > 0)
            {
                vvip += dtvviphr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            cq = new ClassQuery("c");
            stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "VIP"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtviphr = census.LoadDataTable();
            if (dtviphr.Rows.Count > 0)
            {
                vip += dtviphr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            cq = new ClassQuery("c");
            stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "I"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtihr = census.LoadDataTable();
            if (dtihr.Rows.Count > 0)
            {
                i += dtihr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            cq = new ClassQuery("c");
            stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "II"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtiihr = census.LoadDataTable();
            if (dtiihr.Rows.Count > 0)
            {
                ii += dtiihr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            cq = new ClassQuery("c");
            stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "III"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtiiihr = census.LoadDataTable();
            if (dtiiihr.Rows.Count > 0)
            {
                iii += dtiiihr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //---------------------------------
            census = new CensusBalanceQuery("a");
            cq = new ClassQuery("c");
            stdq = new AppStandardReferenceItemQuery("d");
            census.InnerJoin(cq).On(census.ClassID == cq.ClassID);
            census.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL" &&
                stdq.ReferenceID == "X"
                );
            census.Select(census.Balance);
            census.Where(census.CensusDate.Between(startDate, endDate), census.SmfID == paramedicRl1);
            DataTable dtkhusushr = census.LoadDataTable();
            if (dtkhusushr.Rows.Count > 0)
            {
                khusus += dtkhusushr.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Balance"]));
            }
            //--------------------------------------
            var nobq = new NumberOfBedSmfQuery("a");
            nobq.Where(
                nobq.SmfID == paramedicRl1);

            var nobc = new NumberOfBedSmfCollection();

            nobc.Load(nobq);

            AlokasiTT = nobc.Sum(n => n.NumberOfBed ?? 0);

            



            




        }
    }
}
