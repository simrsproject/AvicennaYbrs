using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport35
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

            if (rlMasterReportItemId == 299) //Bayi Lahir Hidup
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
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "05"); //dischargecondq.Note != "+"

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
            else if (rlMasterReportItemId == 300)//>= 2500 gram
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
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "05", birthq.Weight >= 2500);//dischargecondq.Note != "+"

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
            else if (rlMasterReportItemId == 301)//< 2500 gram
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
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition != "05", birthq.Weight < 2500);//dischargecondq.Note != "+" 

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
            else if (rlMasterReportItemId == 302)//Kematian Perinatal
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
                regq.Where(regq.IsVoid == false,
                    regq.Or(birthq.SRBornCondition == "05", dischargecondq.Note == "+"));

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
            else if (rlMasterReportItemId == 303)//Kelahiran Mati
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
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "05");

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
            else if (rlMasterReportItemId == 304)//Mati Neonatal < 7 Hari
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition != "05");

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
            else if (rlMasterReportItemId == 305)//Sebab Kematian 
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
                regq.Where(regq.IsVoid == false, birthq.SRBornCondition == "05");//dischargecondq.Note == "+", 

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
            else if (rlMasterReportItemId == 306)//Asphyxia
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "01");

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
            else if (rlMasterReportItemId == 307)//Trauma Kelahiran
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "02");

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
            else if (rlMasterReportItemId == 308)//BBLR
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "03");

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
            else if (rlMasterReportItemId == 309)//Tetanus Neonatorum
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "04");

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
            else if (rlMasterReportItemId == 310)//Kelainan Congenital
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "05");

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
            else if (rlMasterReportItemId == 311)//ISPA
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "08");

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
            else if (rlMasterReportItemId == 312)//Diare
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition == "06");

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
            else if (rlMasterReportItemId == 313)//Lain - Lain
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
                regq.Where(regq.IsVoid == false, dischargecondq.Note == "+", birthq.SRBornCondition == "05", birthq.SRDeathCondition.NotIn("01", "02", "03", "04", "05", "06", "08"));

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
        }
    }
}
