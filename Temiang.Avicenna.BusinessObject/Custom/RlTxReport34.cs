using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport34
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, string[] arrServiceUnitImunisasiTTId, string[] arrItemIdImunisasiTT1, string[] arrItemIdImunisasiTT2,
            out int pRmRumahSakit, out int pRmBidan, out int pRmPuskesmas, out int pRmFasKesLain,
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

            if (rlMasterReportItemId == 287) //Persalinan Normal
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthMethod == "01");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 288)//Sectio caesaria
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthMethod == "07");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 289)//Pers dg komplikasi
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false);
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 290)//Perd sbl Persalinan
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "01");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 291)//Perd sdh Persalinan
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "02");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 292)//Pre Eclampsi
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "03");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 293)//Eclampsi
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication.In("03a", "03b"));
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 294)//Infeksi
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "04");
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 295)//Lain-lain
            {
                var regq = new RegistrationQuery("a");
                var birthq = new BirthRecordQuery("b");
                var stdq = new AppStandardReferenceItemQuery("d");
                var std2q = new AppStandardReferenceItemQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(birthq).On(regq.RegistrationNo == birthq.MotherRegistrationNo);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(std2q).On(
                    birthq.SRBirthComplication == std2q.ItemID &&
                    std2q.StandardReferenceID == "BirthComplication"
                    );
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication.NotIn("01", "02", "03", "03a", "03b", "04"));
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 296)//Abortus
            {
                //ambil data dari ICDX 
                var regq = new RegistrationQuery("a");
                var stdq = new AppStandardReferenceItemQuery("d");
                var edq = new EpisodeDiagnoseQuery("e");
                var dq = new DiagnoseQuery("f");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(edq).On(regq.RegistrationNo == edq.RegistrationNo && edq.IsVoid == false);
                regq.InnerJoin(dq).On(edq.DiagnoseID == dq.DiagnoseID && dq.DtdNo.In("234", "235", "236.0", "236.1", "236.2", "236.9"));
                regq.Where(regq.IsVoid == false);
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 297)//Imunisasi - TT1
            {
                var regq = new RegistrationQuery("a");
                var stdq = new AppStandardReferenceItemQuery("d");
                var tcq = new TransChargesQuery("e");
                var tciq = new TransChargesItemQuery("f");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(tcq).On(regq.RegistrationNo == tcq.RegistrationNo);
                regq.InnerJoin(tciq).On(tcq.TransactionNo == tciq.TransactionNo);
                regq.Where(regq.IsVoid == false,
                        regq.ServiceUnitID.In(arrServiceUnitImunisasiTTId),
                        tciq.ItemID.In(arrItemIdImunisasiTT1));
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
            else if (rlMasterReportItemId == 298)//Imunisasi - TT2
            {
                var regq = new RegistrationQuery("a");
                var stdq = new AppStandardReferenceItemQuery("d");
                var tcq = new TransChargesQuery("e");
                var tciq = new TransChargesItemQuery("f");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.InnerJoin(tcq).On(regq.RegistrationNo == tcq.RegistrationNo);
                regq.InnerJoin(tciq).On(tcq.TransactionNo == tciq.TransactionNo);
                regq.Where(regq.IsVoid == false,
                        regq.ServiceUnitID.In(arrServiceUnitImunisasiTTId),
                        tciq.ItemID.In(arrItemIdImunisasiTT2));
                regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                DataTable regTable = regq.LoadDataTable();
                if (regTable.Rows.Count > 0)
                {
                    foreach (DataRow row in regTable.Rows)
                    {
                        switch (row["ReferenceID"].ToString())
                        {
                            case "RS":
                            case "RSLAIN":
                                pRmRumahSakit += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "BIDAN":
                                pRmBidan += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "PUSKESMAS":
                                pRmPuskesmas += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "FASKES":
                            case "FASKESLAIN":
                                pRmFasKesLain += 1;
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRmMati += 1;
                                else
                                    pRmHidup += 1;
                                break;
                            case "DUKUN":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pRnmMati += 1;
                                else
                                    pRnmHidup += 1;
                                break;
                            case "DATANGSENDIRI":
                                if (row["SRDischargeCondition"].ToString() == "I04" || row["SRDischargeCondition"].ToString() == "I05" || row["SRDischargeCondition"].ToString() == "DischrgCond-004" || row["SRDischargeCondition"].ToString() == "DischrgCond-005")
                                    pNrMati += 1;
                                else
                                    pNrHidup += 1;
                                break;
                        }
                        if (row["SRDischargeMethod"].ToString() == "I02" || row["SRDischargeMethod"].ToString() == "DischrgMeth-002")
                            pDiRujuk += 1;
                    }
                }
            }
        }
    }
}
