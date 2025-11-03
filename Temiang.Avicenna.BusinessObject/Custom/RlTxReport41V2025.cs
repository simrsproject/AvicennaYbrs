using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport41V2025
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

        public string DtdLabel
        {
            get { return GetColumn("refToDtd_DtdLabel").ToString(); }
            set { SetColumn("refToDtd_DtdLabel", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48,
            string userId, RlTxReport41V2025Collection coll, out RlTxReport41V2025Collection newcoll)
        {
            var epidiagq = new EpisodeDiagnoseQuery("a");
            var diagq = new DiagnoseQuery("b");
            var regq = new RegistrationQuery("c");
            var patq = new PatientQuery("d");
            var brq = new BirthRecordQuery("e");

            epidiagq.Select(
                regq.AgeInDay,
                regq.AgeInMonth,
                regq.AgeInYear,
                patq.Sex,
                regq.SRDischargeCondition,
                diagq.DtdNo,
                @"<DATEDIFF(Minute,e.TimeOfBirth, GETDATE()) AS Selisih>"
                );
            epidiagq.InnerJoin(diagq).On(
                epidiagq.DiagnoseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(coll.Select(r => r.RlMasterReportItemCode).ToArray())
                );
            epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            epidiagq.LeftJoin(brq).On(regq.RegistrationNo == brq.RegistrationNo);
            epidiagq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                epidiagq.IsVoid == false
                );
            epidiagq.Where(string.Format("<MONTH(c.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            epidiagq.Where(string.Format("<YEAR(c.DischargeDate) = {0}>", year.ToString()));

            DataTable dtb = epidiagq.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var item = coll.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();

                    if (!Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) < 60)
                    {
                        if (row["Sex"].ToString() == "M") item.L1j++;
                        else item.P1j++;
                    }
                    else
                    {
                        if ((!Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) >= 60 &&
                             !Convert.IsDBNull(row["Selisih"]) && Convert.ToInt32(row["Selisih"]) <= 1380))
                        {
                            if (row["Sex"].ToString() == "M") item.L123j++;
                            else item.P123j++;
                        }
                        else
                        {
                            if ((Convert.ToInt16(row["AgeInDay"]) >= 1 &&
                                 Convert.ToInt16(row["AgeInDay"]) <= 7 &&
                                 Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                 Convert.ToInt16(row["AgeInYear"]) == 0))
                            {
                                if (row["Sex"].ToString() == "M") item.L0107h++;
                                else item.P0107h++;
                            }
                            else
                            {
                                if (Convert.ToInt16(row["AgeInDay"]) >= 8 &&
                                    Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                    Convert.ToInt16(row["AgeInYear"]) == 0)
                                {
                                    if (row["Sex"].ToString() == "M") item.L0828h++;
                                    else item.P0828h++;
                                }
                                else
                                {
                                    if ((Convert.ToInt16(row["AgeInDay"]) >= 29 &&
                                         Convert.ToInt16(row["AgeInMonth"]) < 3 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0))
                                    {
                                        if (row["Sex"].ToString() == "M") item.L29h03b++;
                                        else item.P29h03b++;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(row["AgeInMonth"]) >= 3 &&
                                            Convert.ToInt16(row["AgeInMonth"]) < 6 &&
                                            Convert.ToInt16(row["AgeInYear"]) == 0)
                                        {
                                            if (row["Sex"].ToString() == "M") item.L0306b++;
                                            else item.L0306b++;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(row["AgeInMonth"]) >= 6 &&
                                                Convert.ToInt16(row["AgeInMonth"]) <= 11 &&
                                                Convert.ToInt16(row["AgeInYear"]) == 0)
                                            {
                                                if (row["Sex"].ToString() == "M") item.L0611b++;
                                                else item.P0611b++;
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(row["AgeInYear"]) >= 1 &&
                                                    Convert.ToInt16(row["AgeInYear"]) <= 4)
                                                {
                                                    if (row["Sex"].ToString() == "M") item.L0104t++;
                                                    else item.P0104t++;
                                                }
                                                else
                                                {
                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 5 &&
                                                        Convert.ToInt16(row["AgeInYear"]) <= 9)
                                                    {
                                                        if (row["Sex"].ToString() == "M") item.L0509t++;
                                                        else item.P0509t++;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 10 &&
                                                            Convert.ToInt16(row["AgeInYear"]) <= 14)
                                                        {
                                                            if (row["Sex"].ToString() == "M") item.L1014t++;
                                                            else item.P1014t++;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 15 &&
                                                                Convert.ToInt16(row["AgeInYear"]) <= 19)
                                                            {
                                                                if (row["Sex"].ToString() == "M") item.L1519t++;
                                                                else item.P1519t++;
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 20 &&
                                                                    Convert.ToInt16(row["AgeInYear"]) <= 24)
                                                                {
                                                                    if (row["Sex"].ToString() == "M") item.L2024t++;
                                                                    else item.P2024t++;
                                                                }
                                                                else
                                                                {
                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 25 &&
                                                                        Convert.ToInt16(row["AgeInYear"]) <= 29)
                                                                    {
                                                                        if (row["Sex"].ToString() == "M") item.L2529t++;
                                                                        else item.P2529t++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 30 &&
                                                                            Convert.ToInt16(row["AgeInYear"]) <= 34)
                                                                        {
                                                                            if (row["Sex"].ToString() == "M") item.L3034t++;
                                                                            else item.P3034t++;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 35 &&
                                                                                Convert.ToInt16(row["AgeInYear"]) <= 39)
                                                                            {
                                                                                if (row["Sex"].ToString() == "M") item.L3539t++;
                                                                                else item.P3539t++;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 40 &&
                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 44)
                                                                                {
                                                                                    if (row["Sex"].ToString() == "M") item.L4044t++;
                                                                                    else item.P4044t++;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 45 &&
                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 49)
                                                                                    {
                                                                                        if (row["Sex"].ToString() == "M") item.L4549t++;
                                                                                        else item.P4549t++;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 50 &&
                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 54)
                                                                                        {
                                                                                            if (row["Sex"].ToString() == "M") item.L5054t++;
                                                                                            else item.P5054t++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 55 &&
                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 59)
                                                                                            {
                                                                                                if (row["Sex"].ToString() == "M") item.L5559t++;
                                                                                                else item.P5559t++;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 60 &&
                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 64)
                                                                                                {
                                                                                                    if (row["Sex"].ToString() == "M") item.L6064t++;
                                                                                                    else item.P6064t++;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 65 &&
                                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 69)
                                                                                                    {
                                                                                                        if (row["Sex"].ToString() == "M") item.L6569t++;
                                                                                                        else item.P6569t++;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (Convert.ToInt16(row["AgeInYear"]) >= 70 &&
                                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 74)
                                                                                                        {
                                                                                                            if (row["Sex"].ToString() == "M") item.L7074t++;
                                                                                                            else item.P7074t++;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Convert.ToInt16(row["AgeInYear"]) >= 75 &&
                                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 79)
                                                                                                            {
                                                                                                                if (row["Sex"].ToString() == "M") item.L7579t++;
                                                                                                                else item.P7579t++;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (Convert.ToInt16(row["AgeInYear"]) >= 80 &&
                                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 84)
                                                                                                                {
                                                                                                                    if (row["Sex"].ToString() == "M") item.L8084t++;
                                                                                                                    else item.P8084t++;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (Convert.ToInt16(row["AgeInYear"]) >= 85)
                                                                                                                    {
                                                                                                                        if (row["Sex"].ToString() == "M") item.L85t++;
                                                                                                                        else item.P85t++;
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //if (row["SRDischargeCondition"].ToString() == dischargeConditionDieLessThen48 ||
                    //    row["SRDischargeCondition"].ToString() == dischargeConditionDieMoreThen48)
                    //    item.TotalPasienMati++;

                    if ((row["SRDischargeCondition"].ToString() == dischargeConditionDieLessThen48 ||
                         row["SRDischargeCondition"].ToString() == dischargeConditionDieMoreThen48) &&
                         row["Sex"].ToString() == "M")
                    {
                        item.TotalPasienMatiL++; // Tambah total laki-laki yang meninggal
                    }
                    else if ((row["SRDischargeCondition"].ToString() == dischargeConditionDieLessThen48 ||
                              row["SRDischargeCondition"].ToString() == dischargeConditionDieMoreThen48) &&
                              row["Sex"].ToString() == "F")
                    {
                        item.TotalPasienMatiP++; // Tambah total perempuan yang meninggal
                    }
                    item.TotalPasienHidupL = item.L0107h + item.L0828h + item.L29h03b + item.L0306b + item.L0611b + item.L0104t + item.L0509t + item.L1014t + item.L1519t + item.L2024t + item.L2529t + item.L3034t + item.L3539t + item.L4044t + item.L4549t + item.L5054t + item.L5559t + item.L6064t + item.L6569t + item.L7074t + item.L7579t + item.L8084t + item.L85t + item.L1j + item.L123j;
                    item.TotalPasienHidupP = item.P0107h + item.P0828h + item.P29h03b + item.P0306b + item.P0611b + item.P0104t + item.P0509t + item.P1014t + item.P1519t + item.P2024t + item.P2529t + item.P3034t + item.P3539t + item.P4044t + item.P4549t + item.P5054t + item.P5559t + item.P6064t + item.P6569t + item.P7074t + item.P7579t + item.P8084t + item.P85t + item.P1j + item.P123j;
                    item.TotalPasienHidup = item.TotalPasienHidupL + item.TotalPasienHidupP;
                    item.TotalPasienMati = item.TotalPasienMatiL + item.TotalPasienMatiP;
                    item.LastUpdateByUserID = userId;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
            newcoll = coll;
        }
    }
}
