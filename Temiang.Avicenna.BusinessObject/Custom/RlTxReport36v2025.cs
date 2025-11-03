using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport36v2025
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId,
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

            if (rlMasterReportItemId == 446) //Pemberian Buku KIA pada Ibu Hamil
            {
                var regq = new RegistrationQuery("a");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, regq.IsParturition == true);
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
            else if (rlMasterReportItemId == 447)//Pelayanan Antenatal
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo == "242.9");
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
            else if (rlMasterReportItemId == 499)//Persalinan perevaginam tanpa penyulit (normal)
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
            else if (rlMasterReportItemId == 450)//Persalinan pervaginam spontan dengan penyulit
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthMethod == "02");
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
            else if (rlMasterReportItemId == 451)//Persalinan pervaginam dengan bantuan
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthMethod.In("04", "05"));
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
            else if (rlMasterReportItemId == 452)//Persalinan Sectio caesaria
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
            else if (rlMasterReportItemId == 454)//Pendarahan sebelum persalinan
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
            else if (rlMasterReportItemId == 455)//Pendarahan setelah persalinan
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
            else if (rlMasterReportItemId == 456)//Pre eklamsia
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
            else if (rlMasterReportItemId == 457)//Eklamsia
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "03a");
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
            else if (rlMasterReportItemId == 458)//Infeksi
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
            else if (rlMasterReportItemId == 459)//Abortus
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthMethod == "08");
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
            else if (rlMasterReportItemId == 459)//Komplikasi lainnya
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "99");
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
            else if (rlMasterReportItemId == 462)//Aborsi atas indikasi kedaruratan medis
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DiagnoseID == "O04");
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
            else if (rlMasterReportItemId == 463)//Aborsi atas indikasi kehamilan akibat perkosaan
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DiagnoseID == "O02.1");
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
            else if (rlMasterReportItemId == 464)//Skrining Status Imunisasi Tetanus
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DiagnoseID == "Z23.5");
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
            else if (rlMasterReportItemId == 466)//HIV
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo == "039");
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
            else if (rlMasterReportItemId == 467)//Hepatitis B
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo == "037");
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
            else if (rlMasterReportItemId == 468)//Sifilis
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo.In("019", "020", "021"));
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
            else if (rlMasterReportItemId == 469)//Tuberkulosis
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo.In("007.0", "007.1", "007.9", "008.1", "008.2", "008.4", "008.9"));
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
            else if (rlMasterReportItemId == 470)//Penyakit jantung
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo.In("144", "148", "150", "151", "152.9", "270.0"));
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
            else if (rlMasterReportItemId == 471)//Anemia
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo.In("097", "148", "098.0", "098.1", "098.9"));
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
            else if (rlMasterReportItemId == 472)//Diabetes Melitus
            {   //ambil dari ICD X
                var regq = new RegistrationQuery("a");
                var diaq = new DiagnoseQuery("b");
                var epiq = new EpisodeDiagnoseQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(epiq).On(regq.RegistrationNo == epiq.RegistrationNo);
                regq.InnerJoin(diaq).On(epiq.DiagnoseID == diaq.DiagnoseID);
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, epiq.SRDiagnoseType == "DiagnoseType-001", diaq.DtdNo.In("104.0", "104.1", "104.2", "104.3", "104.9", "242.0"));
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
            else if (rlMasterReportItemId == 473)//Terkonfirmasi COVID-19
            {
                var regq = new RegistrationQuery("a");
                var stdq = new AppStandardReferenceItemQuery("d");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                regq.Where(regq.IsVoid == false, regq.SRCovidStatus == "1");
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
            else if (rlMasterReportItemId == 474)//Komplikasi lainnya
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
                regq.Where(regq.IsVoid == false, birthq.SRBirthComplication == "99");
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
            else if (rlMasterReportItemId == 476)//Diberikan antenatal kortikosteroid
            {
                var regq = new RegistrationQuery("a");
                var phq = new PatientHealthRecordQuery("b");
                var phrq = new PatientHealthRecordLineQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var ndq = new NursingDiagnosaTemplateQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(phq).On(regq.RegistrationNo == phq.RegistrationNo);
                regq.InnerJoin(phrq).On(
                    phq.TransactionNo == phrq.TransactionNo &&
                    phq.RegistrationNo == phrq.RegistrationNo &&
                    phq.QuestionFormID == phrq.QuestionFormID &&
                    phrq.QuestionID == "RL36.02" &&
                    phrq.QuestionAnswerText == "1"
                    );
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                //regq.InnerJoin(ndq).On(
                //    ndq.TemplateID.ToString() == phq.QuestionFormID &&
                //    ndq.TemplateName == "Kegiatan Pelayanan Kebidanan"
                //    );
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
            else if (rlMasterReportItemId == 477)//Tidak diberikan antenatal kortkosteroid
            {
                var regq = new RegistrationQuery("a");
                var phq = new PatientHealthRecordQuery("b");
                var phrq = new PatientHealthRecordLineQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var ndq = new NursingDiagnosaTemplateQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(phq).On(regq.RegistrationNo == phq.RegistrationNo);
                regq.InnerJoin(phrq).On(
                    phq.TransactionNo == phrq.TransactionNo &&
                    phq.RegistrationNo == phrq.RegistrationNo &&
                    phq.QuestionFormID == phrq.QuestionFormID &&
                    phrq.QuestionID == "RL36.03" &&
                    phrq.QuestionAnswerText == "1"
                    );
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                //regq.InnerJoin(ndq).On(
                //    ndq.TemplateID.ToString() == phq.QuestionFormID &&
                //    ndq.TemplateName == "Kegiatan Pelayanan Kebidanan"
                //    );
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
            else if (rlMasterReportItemId == 478)//Pelayanan Nifas
            {
                var regq = new RegistrationQuery("a");
                var phq = new PatientHealthRecordQuery("b");
                var phrq = new PatientHealthRecordLineQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var ndq = new NursingDiagnosaTemplateQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(phq).On(regq.RegistrationNo == phq.RegistrationNo);
                regq.InnerJoin(phrq).On(
                    phq.TransactionNo == phrq.TransactionNo &&
                    phq.RegistrationNo == phrq.RegistrationNo &&
                    phq.QuestionFormID == phrq.QuestionFormID &&
                    phrq.QuestionID == "RL36.04" &&
                    phrq.QuestionAnswerText == "1"
                    );
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                //regq.InnerJoin(ndq).On(
                //    ndq.TemplateID.ToString() == phq.QuestionFormID &&
                //    ndq.TemplateName == "Kegiatan Pelayanan Kebidanan"
                //    );
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
            else if (rlMasterReportItemId == 479)//Ibu Nifas mendapat vitamin A
            {
                var regq = new RegistrationQuery("a");
                var phq = new PatientHealthRecordQuery("b");
                var phrq = new PatientHealthRecordLineQuery("c");
                var stdq = new AppStandardReferenceItemQuery("d");
                //var ndq = new NursingDiagnosaTemplateQuery("e");

                regq.Select(regq.RegistrationNo, stdq.ReferenceID, regq.SRDischargeCondition, regq.SRDischargeMethod);

                regq.InnerJoin(phq).On(regq.RegistrationNo == phq.RegistrationNo);
                regq.InnerJoin(phrq).On(
                    phq.TransactionNo == phrq.TransactionNo &&
                    phq.RegistrationNo == phrq.RegistrationNo &&
                    phq.QuestionFormID == phrq.QuestionFormID &&
                    phrq.QuestionID == "RL36.05" &&
                    phrq.QuestionAnswerText == "1"
                    );
                regq.InnerJoin(stdq).On(
                    regq.SRReferralGroup == stdq.ItemID &&
                    stdq.StandardReferenceID == "ReferralGroup"
                    );
                //regq.InnerJoin(ndq).On(
                //    ndq.TemplateID.ToString() == phq.QuestionFormID &&
                //    ndq.TemplateName == "Kegiatan Pelayanan Kebidanan"
                //    );
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
        }
    }
}
