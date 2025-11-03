using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport314
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

        public static void Process(int fromMonth, int toMonth, int year, string paramedicRl1, out int rujukanPuskesmas, out int rujukanFasKesLain, out int rujukanRsLain,
            out int dirujukKePuskesmasAsal, out int dirujukKeFasKesAsal, out int dirujukKeRsAsal, out int dirujukPasienRujukan, out int dirujukPasienDtgSendiri, out int dirujukDiterimaKembali)
        {
            rujukanPuskesmas = 0;
            rujukanFasKesLain = 0;
            rujukanRsLain = 0;
            dirujukKePuskesmasAsal = 0;
            dirujukKeFasKesAsal = 0;
            dirujukKeRsAsal = 0;
            dirujukPasienRujukan = 0;
            dirujukPasienDtgSendiri = 0;
            dirujukDiterimaKembali = 0;

            var regq = new RegistrationQuery("a");
            var stdq = new AppStandardReferenceItemQuery("c");
            regq.Select(stdq.ReferenceID);
            regq.InnerJoin(stdq).On(regq.SRReferralGroup == stdq.ItemID && stdq.StandardReferenceID == "ReferralGroup");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                regq.SmfID == paramedicRl1
                );
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            DataTable dtbrujukan = regq.LoadDataTable();
            if (dtbrujukan.Rows.Count > 0)
            {
                foreach (DataRow row in dtbrujukan.Rows)
                {
                    switch (row["ReferenceID"].ToString())
                    {
                        case "PUSKESMAS":
                            rujukanPuskesmas++;
                            break;
                        case "RS":
                        case "RSLAIN":
                            rujukanRsLain++;
                            break;
                        case "FASKES":
                        case "FASKESLAIN":
                            rujukanFasKesLain++;
                            break;
                    }
                }
            }

            ////-----------------------------------
            //dirujukKePuskesmasAsal = 0;
            ////-----------------------------------
            //dirujukKeFasKesAsal = 0;
            ////-----------------------------------
            //dirujukKeRsAsal = 0;
            ////-----------------------------------

            regq = new RegistrationQuery("a");
            stdq = new AppStandardReferenceItemQuery("c");
            var dmq = new AppStandardReferenceItemQuery("q");

            regq.Select(@"<ISNULL(c.ReferenceID, '') AS ReferenceID>", dmq.Note);
            regq.LeftJoin(stdq).On(
                regq.SRReferralGroup == stdq.ItemID &&
                stdq.StandardReferenceID == "ReferralGroup"
                );
            regq.InnerJoin(dmq).On(regq.SRDischargeMethod == dmq.ItemID &&
                                   dmq.StandardReferenceID == "DischargeMethod");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "IPR",
                regq.SmfID == paramedicRl1
                );
            regq.Where(string.Format("<MONTH(a.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.DischargeDate) = {0}>", year.ToString()));
            regq.Where(dmq.Note.In("02", "05"));

            DataTable dtbdirujuk = regq.LoadDataTable();
            if (dtbdirujuk.Rows.Count > 0)
            {
                foreach (DataRow row in dtbdirujuk.Rows)
                {
                    if (row["Note"].ToString() == "05")
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "PUSKESMAS":
                                dirujukKePuskesmasAsal++;
                                break;
                            case "RS":
                            case "RSLAIN":
                                dirujukKeRsAsal++;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                dirujukKeFasKesAsal++;
                                break;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(row["ReferenceID"].ToString()) || row["ReferenceID"].ToString() == "DATANGSENDIRI")
                            dirujukPasienDtgSendiri++;
                        else
                            dirujukPasienRujukan++;
                    }
                    
                }
            }
            //-----------------------------------
            dirujukDiterimaKembali = 0;

            //------------- rawat jalan ---------------------
            regq = new RegistrationQuery("a");
            stdq = new AppStandardReferenceItemQuery("c");
            var parq = new ParamedicQuery("p");
            var regfq = new RegistrationQuery("af");
            dmq = new AppStandardReferenceItemQuery("q");

            regq.Select(@"<ISNULL(c.ReferenceID, '') AS ReferenceID>", @"<ISNULL(q.Note, '') AS 'Note'>");
            regq.InnerJoin(stdq).On(regq.SRReferralGroup == stdq.ItemID && stdq.StandardReferenceID == "ReferralGroup");
            regq.InnerJoin(parq).On(regq.ParamedicID == parq.ParamedicID);
            regq.LeftJoin(regfq).On(regq.RegistrationNo == regfq.FromRegistrationNo);
            regq.LeftJoin(dmq).On(regq.SRDischargeMethod == dmq.ItemID && dmq.StandardReferenceID == "DischargeMethod");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType.In("OPR", "EMR"),
                parq.SRParamedicRL1 == paramedicRl1,
                regfq.RegistrationNo.IsNull()
                );
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            DataTable dtbrujukan2 = regq.LoadDataTable();
            if (dtbrujukan2.Rows.Count > 0)
            {
                foreach (DataRow row in dtbrujukan2.Rows)
                {
                    switch (row["ReferenceID"].ToString())
                    {
                        case "PUSKESMAS":
                            rujukanPuskesmas++;
                            if (row["Note"].ToString() == "05")
                                dirujukKePuskesmasAsal++;
                            else if (row["Note"].ToString() == "02")
                            {
                                dirujukPasienRujukan++;
                            }
                            break;
                        case "RS":
                        case "RSLAIN":
                            rujukanRsLain++;
                            if (row["Note"].ToString() == "05")
                                dirujukKePuskesmasAsal++;
                            else if (row["Note"].ToString() == "02")
                            {
                                dirujukPasienRujukan++;
                            }
                            break;
                        case "FASKES":
                        case "FASKESLAIN":
                            rujukanFasKesLain++;
                            if (row["Note"].ToString() == "05")
                                dirujukKePuskesmasAsal++;
                            else if (row["Note"].ToString() == "02")
                            {
                                dirujukPasienRujukan++;
                            }
                            break;

                        case "":
                        case "DATANGSENDIRI":
                            if (row["Note"].ToString() == "02")
                            {
                                dirujukPasienDtgSendiri++;
                            }
                            break;
                    }
                }
            }

            //regq = new RegistrationQuery("a");
            //stdq = new AppStandardReferenceItemQuery("c");
            //parq = new ParamedicQuery("p");
            //dmq = new AppStandardReferenceItemQuery("q");
            //regfq = new RegistrationQuery("af");

            //regq.Select(@"<ISNULL(c.ReferenceID, '') AS ReferenceID>");
            //regq.LeftJoin(stdq).On(
            //    regq.SRReferralGroup == stdq.ItemID &&
            //    stdq.StandardReferenceID == "ReferralGroup"
            //    );
            //regq.InnerJoin(parq).On(regq.ParamedicID == parq.ParamedicID);
            //regq.InnerJoin(dmq).On(regq.SRDischargeMethod == dmq.ItemID &&
            //                       dmq.StandardReferenceID == "DischargeMethod");
            //regq.LeftJoin(regfq).On(regq.RegistrationNo == regfq.FromRegistrationNo);
            //regq.Where(
            //    regq.IsVoid == false,
            //    regq.SRRegistrationType.In("OPR", "EMR"),
            //    parq.SRParamedicRL1 == paramedicRl1,
            //    regfq.RegistrationNo.IsNull()
            //    );
            //regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            //regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            //regq.Where(dmq.Note == "02");

            //DataTable dtbdirujuk2 = regq.LoadDataTable();
            //if (dtbdirujuk2.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dtbdirujuk2.Rows)
            //    {
            //        if (string.IsNullOrEmpty(row["ReferenceID"].ToString()) || row["ReferenceID"].ToString() == "DATANGSENDIRI")
            //            dirujukPasienDtgSendiri++;
            //        else
            //            dirujukPasienRujukan++;
            //    }
            //}
        }
    }
}
