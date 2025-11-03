using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport4ASebab
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
            string userId, RlTxReport4ASebabCollection coll, out RlTxReport4ASebabCollection newcoll)
        {
            var epidiagq = new EpisodeDiagnoseQuery("a");
            var diagq = new DiagnoseQuery("b");
            var regq = new RegistrationQuery("c");
            var patq = new PatientQuery("d");

            epidiagq.Select(
                regq.AgeInDay,
                regq.AgeInMonth,
                regq.AgeInYear,
                patq.Sex,
                regq.SRDischargeCondition,
                diagq.DtdNo
                );
            epidiagq.InnerJoin(diagq).On(
                epidiagq.ExternalCauseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(coll.Select(r => r.RlMasterReportItemCode).ToArray())
                );
            epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
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

                    if (Convert.ToInt16(row["AgeInDay"]) <= 6 &&
                        Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                        Convert.ToInt16(row["AgeInYear"]) == 0)
                    {
                        if (row["Sex"].ToString() == "M") item.L0006h++;
                        else item.P0006h++;
                    }
                    else
                    {
                        if (Convert.ToInt16(row["AgeInDay"]) > 6 &&
                            Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                            Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                            Convert.ToInt16(row["AgeInYear"]) == 0)
                        {
                            if (row["Sex"].ToString() == "M") item.L0628h++;
                            else item.P0628h++;
                        }
                        else
                        {
                            if ((Convert.ToInt16(row["AgeInDay"]) > 28 &&
                                 Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                 Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                (Convert.ToInt16(row["AgeInMonth"]) >= 1 &&
                                 Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                (Convert.ToInt16(row["AgeInYear"]) == 1))
                            {
                                if (row["Sex"].ToString() == "M") item.L28h01t++;
                                else item.P28h01t++;
                            }
                            else
                            {
                                if (Convert.ToInt16(row["AgeInYear"]) > 1 &&
                                    Convert.ToInt16(row["AgeInYear"]) <= 4)
                                {
                                    if (row["Sex"].ToString() == "M") item.L0104t++;
                                    else item.P0104t++;
                                }
                                else
                                {
                                    if (Convert.ToInt16(row["AgeInYear"]) > 4 &&
                                        Convert.ToInt16(row["AgeInYear"]) <= 14)
                                    {
                                        if (row["Sex"].ToString() == "M") item.L0414t++;
                                        else item.P0414t++;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt16(row["AgeInYear"]) > 14 &&
                                            Convert.ToInt16(row["AgeInYear"]) <= 24)
                                        {
                                            if (row["Sex"].ToString() == "M") item.L1424t++;
                                            else item.P1424t++;
                                        }
                                        else
                                        {
                                            if (Convert.ToInt16(row["AgeInYear"]) > 24 &&
                                                Convert.ToInt16(row["AgeInYear"]) <= 44)
                                            {
                                                if (row["Sex"].ToString() == "M") item.L2444t++;
                                                else item.P2444t++;
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(row["AgeInYear"]) > 44 &&
                                                    Convert.ToInt16(row["AgeInYear"]) <= 64)
                                                {
                                                    if (row["Sex"].ToString() == "M") item.L4464t++;
                                                    else item.P4464t++;
                                                }
                                                else
                                                {
                                                    if (row["Sex"].ToString() == "M") item.L64t++;
                                                    else item.P64t++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (row["SRDischargeCondition"].ToString() == dischargeConditionDieLessThen48 ||
                        row["SRDischargeCondition"].ToString() == dischargeConditionDieMoreThen48)
                        item.TotalMati++;
                    item.TotalL = item.L0006h + item.L0628h + item.L28h01t + item.L0104t + item.L0414t + item.L1424t + item.L2444t + item.L4464t + item.L64t;
                    item.TotalP = item.P0006h + item.P0628h + item.P28h01t + item.P0104t + item.P0414t + item.P1424t + item.P2444t + item.P4464t + item.P64t;
                    item.Total = item.TotalL + item.TotalP;
                    item.LastUpdateByUserID = userId;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
            newcoll = coll;
        }
    }
}
