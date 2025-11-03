using Newtonsoft.Json;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Schema;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport37v2025
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
        //public class NutrientPe
        //{
        //    public string Sga { get; set; }
        //}

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, out int pRmRumahSakit, out int pRmBidan, out int pRmPuskesmas, out int pRmFasKesLain,
            out int pRmHidup, out int pRmMati, out int pRnmHidup, out int pRnmMati, out int pNrHidup, out int pNrMati, out int pDiRujuk)
        {
            pRmRumahSakit = 0;
            pRmBidan = 0;
            pRmPuskesmas = 0;
            pRmFasKesLain = 0;
            pRmHidup = 0;
            pRmMati = 0;
            pRnmHidup = 0;
            pRnmMati = 0;
            pNrHidup = 0;
            pNrMati = 0;
            pDiRujuk = 0;

            if (rlMasterReportItemId == 499) //Bayi Lahir Hidup
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID ==
                                                 "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID ==
                                                 "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "05", dischargecondq.Note != "+");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 500)// Lahir Prematur (<37 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID ==
                                                 "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID ==
                                                 "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "02", birthq.BirthPregnancyAge < 37);//dischargecondq.Note != "+"
                regq.Where(birthq.SRBornCondition == "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge < 37);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 501)//1500 - < 2500 gram (BBLR) Prematur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "02", birthq.BirthPregnancyAge < 37, birthq.Weight >= 1500 && birthq.Weight < 2500);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition == "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge < 37);
                regq.Where(birthq.Weight >= 1500 && birthq.Weight < 2500);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 502)//1000 - < 1500 gram (BBLSR) Prematur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "02", birthq.BirthPregnancyAge < 37, birthq.Weight >= 1000 && birthq.Weight < 1500);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition == "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge < 37);
                regq.Where(birthq.Weight >= 1000 && birthq.Weight < 1500);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 503)//< 1000 gram (BBLER) Prematur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "02", birthq.BirthPregnancyAge < 37, birthq.Weight >= 1500 && birthq.Weight < 1000);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition == "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge < 37);
                regq.Where(birthq.Weight < 1000);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 504)// Lahir Non Prematur (>37 - 41 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID ==
                                                 "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID ==
                                                 "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "02", dischargecondq.Note != "+", birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge < 41);
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge <= 41);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 505)//1500 - < 2500 gram (BBLR) Non Prematur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "02", dischargecondq.Note != "+", birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge < 41, birthq.Weight >= 1500 && birthq.Weight < 2500);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge <= 41);
                regq.Where(birthq.Weight >= 1500 && birthq.Weight < 2500);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 506)//2500 - < 4000 gram (BBLN) Non Prematur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false,  birthq.SRBornCondition != "02", dischargecondq.Note != "+", birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge < 41, birthq.Weight >= 2500 && birthq.Weight < 4000);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge <= 41);
                regq.Where(birthq.Weight >= 2500 && birthq.Weight < 4000);


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 507)//>= 4000 gram (BBLL)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                //regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "02", birthq.BirthPregnancyAge < 37, birthq.Weight >= 1500 && birthq.Weight < 1000);//dischargecondq.Note != "+" 
                regq.Where(regq.IsVoid == false);
                regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge >= 37 && birthq.BirthPregnancyAge <= 41);
                regq.Where(birthq.Weight >= 4000);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 508)//(> 41 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID ==
                                                 "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID ==
                                                 "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge > 41);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 509)//1500 - < 2500 gram (BBLR) (> 41 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge > 41);
                regq.Where(birthq.Weight >= 1500 && birthq.Weight < 2500);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 510)//2500 - < 4000 gram (BBLN) (> 41 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge > 41);
                regq.Where(birthq.Weight >= 2500 && birthq.Weight < 4000);


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 511)//>= 4000 gram (BBLL) (> 41 Minggu)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Where(birthq.SRBornCondition != "02");
                regq.Where(dischargecondq.Note != "+");
                regq.Where(birthq.BirthPregnancyAge > 41);
                regq.Where(birthq.Weight >= 4000);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 513)//Lahir Mati Antepartum
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Or(birthq.SRBornCondition == "05", dischargecondq.Note == "+"));
                regq.Where(birthq.SRBornDeath == "01");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 514)//Lahir Mati Intrapartum
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Or(birthq.SRBornCondition == "05", dischargecondq.Note == "+"));
                regq.Where(birthq.SRBornDeath == "02");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 516)//Kematian Neonatal Dini (0 - 7 Hari)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 8, a.RegistrationDate))");


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 517)//Kematian Neonatal Lanjut Perinatal (8 - 28 hari)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDatetime IS NOT NULL AND h.DeceasedDatetime >= DATEADD(DAY, 8, a.RegistrationDate) AND h.DeceasedDatetime < DATEADD(DAY, 29, a.RegistrationDate))");


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 519)//Kematian Neonatal Asfiksia
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDatetime IS NOT NULL AND h.DeceasedDatetime >= a.RegistrationDate AND h.DeceasedDatetime < DATEADD(DAY, 29, a.RegistrationDate))");
                //regq.Where(birthq.SRDeathCondition == 01);
                regq.Where(refq.ReferenceID == "01");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 520)//Kematian Neonatal Trauma Kelahiran
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 29, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "02");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 521)//Kematian Neonatal BBLR
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 29, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "03");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }

            else if (rlMasterReportItemId == 522)//Kematian Neonatal Tetanus Neonatorum
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 29, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "04");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 523)//Kematian Neonatal Kelainan Bawaan
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 8, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "05");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 524)//Kematian Neonatal Covid-19
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 29, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "06");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 525)//Kematian Neonatal Infeksi
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 8, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "07");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 526)//Kematian Neonatal Lain-Lain
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var refq = new AppStandardReferenceItemQuery("z");
                var patq = new PatientQuery("h");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.LeftJoin(refq).On(birthq.SRDeathCondition == refq.ItemID && refq.StandardReferenceID == "DeathCondition");


                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where("(h.DeceasedDateTime IS NOT NULL AND h.DeceasedDateTime >= a.RegistrationDate AND h.DeceasedDateTime < DATEADD(DAY, 8, a.RegistrationDate))");
                regq.Where(refq.ReferenceID == "99");

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmMati += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmMati += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmMati += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmMati += 1;
                                break;
                            case "DUKUN":
                                pRnmMati += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrMati += 1;
                                break;
                        }
                    }
                }
            }
            else if (rlMasterReportItemId == 527)//Bayi BBLR yang dilakukan perawatan metode kanguru 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);//dischargecondq.Note == "+", 
                regq.Where(birthq.IsKangarooMethod == true);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 528)//Bayi baru lahir yang dilakukan IMD 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);//dischargecondq.Note == "+", 
                regq.Where(birthq.IsIMD == true);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 529)//Bayi baru lahir yang dilakukan Skrining Hipertiroid Kongenital 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);//dischargecondq.Note == "+", 
                regq.Where(birthq.IsCongenitalHyperthyroidism == true);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            //else if (rlMasterReportItemId == 530)//Bayi baru lahir yang dilakukan Skrining Hipertiroid Kongenital 
            //{
            //    var regq = new RegistrationQuery("a");
            //    var birthq = new BirthRecordQuery("b");
            //    var stdq = new AppStandardReferenceItemQuery("d");
            //    var regmotherq = new RegistrationQuery("e");
            //    var dischargecondq = new AppStandardReferenceItemQuery("f");
            //    var dischargemethq = new AppStandardReferenceItemQuery("g");

            //    regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
            //                dischargecondq.Note.As("DischargeConditionNote"),
            //                dischargemethq.Note.As("DischargeMethodNote"));

            //    regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
            //    regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

            //    regq.InnerJoin(stdq).On(
            //        regmotherq.SRReferralGroup == stdq.ItemID &&
            //        stdq.StandardReferenceID == "ReferralGroup"
            //        );

            //    regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
            //                                     dischargecondq.StandardReferenceID == "DischargeCondition");

            //    regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
            //                                     dischargemethq.StandardReferenceID == "DischargeMethod");

            //    regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            //    regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            //    regq.Where(regq.IsVoid == false);//dischargecondq.Note == "+", 

            //    DataTable regTable = regq.LoadDataTable();
            //    if (regTable.Rows.Count > 0)
            //    {
            //        foreach (DataRow row in regTable.Rows)
            //        {
            //            switch (row["ReferenceID"].ToString())
            //            {
            //                case "RI":
            //                case "RJ":
            //                case "RS":
            //                case "RSLAIN":
            //                    pRmRumahSakit += 1;
            //                    pRmHidup += 1;
            //                    break;
            //                case "BIDAN":
            //                    pRmBidan += 1;
            //                    pRmHidup += 1;
            //                    break;
            //                case "PUSKESMAS":
            //                    pRmPuskesmas += 1;
            //                    pRmHidup += 1;
            //                    break;
            //                case "FASKES":
            //                case "FASKESLAIN":
            //                    pRmFasKesLain += 1;
            //                    pRmHidup += 1;
            //                    break;
            //                case "DUKUN":
            //                    pRnmHidup += 1;
            //                    break;
            //                case "DATANGSENDIRI":
            //                    pNrHidup += 1;
            //                    break;
            //            }
            //            if (row["DischargeMethodNote"].ToString() == "02")
            //                pDiRujuk += 1;
            //        }
            //    }
            //}
            else if (rlMasterReportItemId == 531) // Bayi dan Anak Balita (Bayi Baru Lahir (0 - 28)) 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);//dischargecondq.Note == "+", 
                regq.Where(regq.AgeInYear == 0 && regq.AgeInMonth == 0 && regq.AgeInDay <= 28);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 532)// Bayi dan Anak Balita (Bayi (29 hari - 11 Bulan)) 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where((regq.AgeInYear == 0 && regq.AgeInMonth >= 1 && regq.AgeInMonth <= 11) || (regq.AgeInYear == 0 && regq.AgeInMonth == 0 && regq.AgeInDay >= 29));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 533)// Bayi dan Anak Balita (Anak Balita (12 bulan - 59 bulan)) 
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regmotherq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where((regq.AgeInYear == 1 && regq.AgeInMonth == 0) || (regq.AgeInYear >= 1 && regq.AgeInYear < 5));


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 535) // Balita Gizi Buruk (0 - 5 Bulan)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var reginfomedicq = new RegistrationInfoMedicQuery("i");
                var assesq = new PatientAssessmentQuery("j");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"),
                            assesq.PhysicalExam);


                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(reginfomedicq).On(regq.RegistrationNo == reginfomedicq.RegistrationNo);
                regq.InnerJoin(assesq).On(reginfomedicq.RegistrationInfoMedicID == assesq.RegistrationInfoMedicID);

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(regq.AgeInYear == 0 && regq.AgeInMonth <= 5);
                regq.Where(string.Format("<JSON_VALUE(j.PhysicalExam,'$.Sga') ='C'>"));


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 536) // Balita Gizi Buruk (6 - 59 Bulan)
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var reginfomedicq = new RegistrationInfoMedicQuery("i");
                var assesq = new PatientAssessmentQuery("j");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"),
                            assesq.PhysicalExam);


                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(reginfomedicq).On(regq.RegistrationNo == reginfomedicq.RegistrationNo);
                regq.InnerJoin(assesq).On(reginfomedicq.RegistrationInfoMedicID == assesq.RegistrationInfoMedicID);

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                //regq.Where(regq.AgeInYear == 0 && regq.AgeInMonth <= 5);
                regq.Where((regq.AgeInYear == 0 && regq.AgeInMonth >= 6) || (regq.AgeInYear >= 1 && regq.AgeInYear < 5));
                regq.Where(string.Format("<JSON_VALUE(j.PhysicalExam,'$.Sga') ='C'>"));


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 537)//Balita Menggunakan Buku KIA (BELUM)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                //var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var servidq = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitKiaId);

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod, dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(regq.ServiceUnitID == servidq);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 539)//Skrining Pertumbuhan sesuai umur
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.01");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 540)//Skrining perkembangan sesuai umur
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.02");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 541)//Skrining keterlambatan bicara dan bahasa
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.03");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 542)//Assessment kelainan motoric
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.04");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 543)//Skrining Kelainan Perilaku
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.05");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 544)//Skrining Gangguan Pendengaran
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.06");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 545)//Skrining Gangguan Penglihatan
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.07");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 547)//Hb 0
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.08");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 548)//BCG
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.09");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 549)//Polio 1,2,3
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.10");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 550)//DPT-HB-HiB 1, 2,3,4
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.11");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 551)//IPV
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.12");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 552)//Campak-Rubella
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.13");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 553)//Vitamin A 100.000 SI (1 kali dalam setahun)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.14");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 554)//Pemberian Komunikasi, Informasi dan Edukasi (KIE)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.15");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 556)//Pemeriksaan Early Infant Diagnosis (EID)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.16");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 557)//Pengobatan ARV bagi balita HIV+
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.17");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 558)//Pengobatan profilaksis kotrimoksazol
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.18");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 560)//Pemeriksaan Titer RPR
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.19");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 561)//Pengobatan dosis tunggal Benzatin Penicilin G
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.20");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 563)//Pemeriksaan serologis HBs Ag
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.21");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 564)//Pemberian Hb 0
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.22");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 565)//Pemberian Hb Ig
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.23");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 567)//Campak-Rubela
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.24");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 568)//Vitamin A 200.000 SI (2kali dalam setahun)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.25");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 569)//Anak balita mendapat obat pencegahan kecacingan 1 kali setahun
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.26");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 570)//Balita (0-59 bulan) terduga TBC/ kontak erat mendapat TPT (Terapi Pencegahan TBC)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.27");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 571)//Balita (0-59 bulan) TBC mendapatkan OAT
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.28");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 572)//Pemberian Komunikasi, Informasi dan Edukasi (KIE)
            {
                var regq = new RegistrationQuery("a");
                //var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var phrl = new PatientHealthRecordLineQuery("k");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"));

                //regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.RegistrationNo);
                //regq.InnerJoin(regmotherq).On(birthq.MotherRegistrationNo == regmotherq.RegistrationNo);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");

                regq.InnerJoin(phrl).On(regq.RegistrationNo == phrl.RegistrationNo && phrl.QuestionID == "RL3.7.29");

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(phrl.QuestionAnswerText == 1);

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 574) // Balita Gizi Buruk usia 0-5 bulan yang mendapat rawat inap
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var reginfomedicq = new RegistrationInfoMedicQuery("i");
                var assesq = new PatientAssessmentQuery("j");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"),
                            assesq.PhysicalExam);


                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(reginfomedicq).On(regq.RegistrationNo == reginfomedicq.RegistrationNo);
                regq.InnerJoin(assesq).On(reginfomedicq.RegistrationInfoMedicID == assesq.RegistrationInfoMedicID);

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(regq.AgeInYear == 0 && regq.AgeInMonth <= 5);
                regq.Where(regq.SRRegistrationType == "IPR");
                regq.Where(string.Format("<JSON_VALUE(j.PhysicalExam,'$.Sga') ='C'>"));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 575) // Balita Gizi Buruk usia 6-59 bulan yang mendapat rawat inap
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var reginfomedicq = new RegistrationInfoMedicQuery("i");
                var assesq = new PatientAssessmentQuery("j");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"),
                            assesq.PhysicalExam);


                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(reginfomedicq).On(regq.RegistrationNo == reginfomedicq.RegistrationNo);
                regq.InnerJoin(assesq).On(reginfomedicq.RegistrationInfoMedicID == assesq.RegistrationInfoMedicID);

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(regq.SRRegistrationType == "IPR");
                regq.Where((regq.AgeInYear == 0 && regq.AgeInMonth >= 6) || (regq.AgeInYear >= 1 && regq.AgeInYear < 5));
                regq.Where(string.Format("<JSON_VALUE(j.PhysicalExam,'$.Sga') ='C'>"));


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 576) // Balita Gizi Buruk usia 6-59 bulan yang mendapat rawat jalan
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var regmotherq = new RegistrationQuery("e");
                var dischargecondq = new AppStandardReferenceItemQuery("f");
                var dischargemethq = new AppStandardReferenceItemQuery("g");
                var reginfomedicq = new RegistrationInfoMedicQuery("i");
                var assesq = new PatientAssessmentQuery("j");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod,
                            dischargecondq.Note.As("DischargeConditionNote"),
                            dischargemethq.Note.As("DischargeMethodNote"),
                            assesq.PhysicalExam);


                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );

                regq.LeftJoin(dischargecondq).On(regq.SRDischargeCondition == dischargecondq.ItemID &&
                                                 dischargecondq.StandardReferenceID == "DischargeCondition");

                regq.LeftJoin(dischargemethq).On(regq.SRDischargeMethod == dischargemethq.ItemID &&
                                                 dischargemethq.StandardReferenceID == "DischargeMethod");
                regq.InnerJoin(reginfomedicq).On(regq.RegistrationNo == reginfomedicq.RegistrationNo);
                regq.InnerJoin(assesq).On(reginfomedicq.RegistrationInfoMedicID == assesq.RegistrationInfoMedicID);

                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                regq.Where(regq.IsVoid == false);
                regq.Where(regq.SRRegistrationType == "OPR");
                regq.Where((regq.AgeInYear == 0 && regq.AgeInMonth >= 6) || (regq.AgeInYear >= 1 && regq.AgeInYear < 5));
                regq.Where(string.Format("<JSON_VALUE(j.PhysicalExam,'$.Sga') ='C'>"));


                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RI":
                            case "RJ":
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                pRmHidup += 1;
                                break;
                            case "DUKUN":
                                pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                pNrHidup += 1;
                                break;
                        }
                        if (row["DischargeMethodNote"].ToString() == "02")
                            pDiRujuk += 1;
                    }
                }
            }
        }
    }
}
