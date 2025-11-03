using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport51V2025
    {
        public static int ProcessRlTxReport51V2025(string reportNo, int fromMonth, int toMonth, int year, string userId)
        {
            esParameters prms = new esParameters();

            prms.Add("p_RlTxReportNo", reportNo, esParameterDirection.Input, DbType.String, 20);
            prms.Add("p_FromMonth", fromMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_ToMonth", toMonth, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_Year", year, esParameterDirection.Input, DbType.Int32, 0);
            prms.Add("p_UserID", userId, esParameterDirection.Input, DbType.String, 20);

            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            RlTxReport51V2025 entity = new RlTxReport51V2025();
            entity.es.Connection.CommandTimeout = 60 * 10; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_RlTxReport51V2025", prms);
            return (int)prms["Return_Value"].Value;
        }
        public string RlMasterReportItemCode
        {
            get { return GetColumn("refToRlMasterReportItemV2025_RlMasterReportItemCode").ToString(); }
            set { SetColumn("refToRlMasterReportItemV2025_RlMasterReportItemCode", value); }
        }

        public string RlMasterReportItemName
        {
            get { return GetColumn("refToRlMasterReportItemV2025_RlMasterReportItemName").ToString(); }
            set { SetColumn("refToRlMasterReportItemV2025_RlMasterReportItemName", value); }
        }

        public string DtdLabel
        {
            get { return GetColumn("refToDtd_DtdLabel").ToString(); }
            set { SetColumn("refToDtd_DtdLabel", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string userId, RlTxReport51V2025Collection coll, out RlTxReport51V2025Collection newcoll)
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
                epidiagq.IsOldCase,
                diagq.DtdNo
                );
            epidiagq.InnerJoin(diagq).On(
                epidiagq.DiagnoseID == diagq.DiagnoseID &&
                diagq.DtdNo.In(coll.Select(r => r.RlMasterReportItemCode).ToArray())
                );
            epidiagq.InnerJoin(regq).On(epidiagq.RegistrationNo == regq.RegistrationNo);
            epidiagq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            epidiagq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType != "IPR",
                epidiagq.IsVoid == false
                );
            epidiagq.Where(string.Format("<MONTH(c.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            epidiagq.Where(string.Format("<YEAR(c.RegistrationDate) = {0}>", year.ToString()));

            var dtb = epidiagq.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var item = coll.Where(r => r.RlMasterReportItemCode == row["DtdNo"].ToString()).Single();

                    if (Convert.ToInt16(row["AgeInDay"]) <= 0 &&
                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                    Convert.ToInt16(row["AgeInYear"]) == 0)
                    {
                        if (row["Sex"].ToString() == "M") item.L0001j++;
                        else item.P0001j++;
                    }
                    else
                    {
                        if (Convert.ToInt16(row["AgeInDay"]) <= 1 &&
                        Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                        Convert.ToInt16(row["AgeInYear"]) == 0)
                        {
                            if (row["Sex"].ToString() == "M") item.L0001h++;
                            else item.P0001h++;
                        }
                        else
                        {
                            if (Convert.ToInt16(row["AgeInDay"]) <= 7 &&
                            Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                            Convert.ToInt16(row["AgeInYear"]) == 0)
                            {
                                if (row["Sex"].ToString() == "M") item.L0007h++;
                                else item.P0007h++;
                            }
                            else
                            {
                                if (Convert.ToInt16(row["AgeInDay"]) > 7 &&
                                    Convert.ToInt16(row["AgeInDay"]) <= 28 &&
                                    Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                    Convert.ToInt16(row["AgeInYear"]) == 0)
                                {
                                    if (row["Sex"].ToString() == "M") item.L0828h++;
                                    else item.P0828h++;
                                }
                                else
                                {
                                    if ((Convert.ToInt16(row["AgeInDay"]) > 28 &&
                                         Convert.ToInt16(row["AgeInMonth"]) == 0 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                        (Convert.ToInt16(row["AgeInMonth"]) > 1 &&
                                         Convert.ToInt16(row["AgeInMonth"]) < 3 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                         (Convert.ToInt16(row["AgeInMonth"]) > 1 &&
                                         Convert.ToInt16(row["AgeInMonth"]) <= 3 &&
                                         Convert.ToInt16(row["AgeInDay"]) == 0 &&
                                         Convert.ToInt16(row["AgeInYear"]) == 0))
                                    {
                                        if (row["Sex"].ToString() == "M") item.L29h03b++;
                                        else item.P29h03b++;
                                    }
                                    else
                                    {
                                        if ((Convert.ToInt16(row["AgeInMonth"]) >= 3 &&
                                             Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                            (Convert.ToInt16(row["AgeInMonth"]) < 6 &&
                                             Convert.ToInt16(row["AgeInYear"]) == 0))
                                        {
                                            if (row["Sex"].ToString() == "M") item.L3b6b++;
                                            else item.P3b6b++;
                                        }
                                        else
                                        {
                                            if ((Convert.ToInt16(row["AgeInMonth"]) >= 6 &&
                                                 Convert.ToInt16(row["AgeInYear"]) == 0) ||
                                                (Convert.ToInt16(row["AgeInMonth"]) <= 11 &&
                                                 Convert.ToInt16(row["AgeInYear"]) == 0))
                                            {
                                                if (row["Sex"].ToString() == "M") item.L6b11b++;
                                                else item.P6b11b++;
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
                                                        Convert.ToInt16(row["AgeInYear"]) <= 9)
                                                    {
                                                        if (row["Sex"].ToString() == "M") item.L0509t++;
                                                        else item.P0509t++;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToInt16(row["AgeInYear"]) > 9 &&
                                                            Convert.ToInt16(row["AgeInYear"]) <= 14)
                                                        {
                                                            if (row["Sex"].ToString() == "M") item.L1014t++;
                                                            else item.P1014t++;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToInt16(row["AgeInYear"]) > 14 &&
                                                                Convert.ToInt16(row["AgeInYear"]) <= 19)
                                                            {
                                                                if (row["Sex"].ToString() == "M") item.L1519t++;
                                                                else item.P1519t++;
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToInt16(row["AgeInYear"]) > 19 &&
                                                                    Convert.ToInt16(row["AgeInYear"]) <= 24)
                                                                {
                                                                    if (row["Sex"].ToString() == "M") item.L2024t++;
                                                                    else item.P2024t++;
                                                                }
                                                                else
                                                                {
                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 24 &&
                                                                        Convert.ToInt16(row["AgeInYear"]) <= 29)
                                                                    {
                                                                        if (row["Sex"].ToString() == "M") item.L2529t++;
                                                                        else item.P2529t++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 29 &&
                                                                            Convert.ToInt16(row["AgeInYear"]) <= 34)
                                                                        {
                                                                            if (row["Sex"].ToString() == "M") item.L3034t++;
                                                                            else item.P3034t++;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 34 &&
                                                                                Convert.ToInt16(row["AgeInYear"]) <= 39)
                                                                            {
                                                                                if (row["Sex"].ToString() == "M") item.L3539t++;
                                                                                else item.P3539t++;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (Convert.ToInt16(row["AgeInYear"]) > 39 &&
                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 44)
                                                                                {
                                                                                    if (row["Sex"].ToString() == "M") item.L4044t++;
                                                                                    else item.P4044t++;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 44 &&
                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 49)
                                                                                    {
                                                                                        if (row["Sex"].ToString() == "M") item.L4549t++;
                                                                                        else item.P4549t++;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 49 &&
                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 54)
                                                                                        {
                                                                                            if (row["Sex"].ToString() == "M") item.L5054t++;
                                                                                            else item.P5054t++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 54 &&
                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 59)
                                                                                            {
                                                                                                if (row["Sex"].ToString() == "M") item.L5559t++;
                                                                                                else item.P5559t++;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (Convert.ToInt16(row["AgeInYear"]) > 59 &&
                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 64)
                                                                                                {
                                                                                                    if (row["Sex"].ToString() == "M") item.L6064t++;
                                                                                                    else item.P6064t++;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (Convert.ToInt16(row["AgeInYear"]) > 64 &&
                                                                                                        Convert.ToInt16(row["AgeInYear"]) <= 69)
                                                                                                    {
                                                                                                        if (row["Sex"].ToString() == "M") item.L6569t++;
                                                                                                        else item.P6569t++;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (Convert.ToInt16(row["AgeInYear"]) > 69 &&
                                                                                                            Convert.ToInt16(row["AgeInYear"]) <= 74)
                                                                                                        {
                                                                                                            if (row["Sex"].ToString() == "M") item.L7074t++;
                                                                                                            else item.P7074t++;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (Convert.ToInt16(row["AgeInYear"]) > 74 &&
                                                                                                                Convert.ToInt16(row["AgeInYear"]) <= 79)
                                                                                                            {
                                                                                                                if (row["Sex"].ToString() == "M") item.L7579t++;
                                                                                                                else item.P7579t++;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (Convert.ToInt16(row["AgeInYear"]) > 79 &&
                                                                                                                    Convert.ToInt16(row["AgeInYear"]) <= 84)
                                                                                                                {
                                                                                                                    if (row["Sex"].ToString() == "M") item.L8084t++;
                                                                                                                    else item.P8084t++;
                                                                                                                }
                                                                                                                else
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
                    if (Convert.ToBoolean(row["IsOldCase"]) == false)
                    {
                        if (row["Sex"].ToString() == "M") item.KasusBaruL++;
                        else item.KasusBaruP++;
                    }

                    item.TotalKasusBaru = item.KasusBaruL + item.KasusBaruP;
                    item.TotalKunjungan++;
                    item.LastUpdateByUserID = userId;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
            newcoll = coll;
        }
    }
}
