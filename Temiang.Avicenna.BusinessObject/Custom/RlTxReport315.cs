using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport315
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, string serviceUnitLaboratoryId, string serviceUnitRadiologyId,
            out int riJpk, out int riJld, out int rj, out int rjLab, out int rjRad, out int rjLl)
        {
            riJpk = 0;
            riJld = 0;
            rj = 0;
            rjLab = 0;
            rjRad = 0;
            rjLl = 0;

            switch (rlMasterReportItemId)
            {
                case 642: //membayar sendiri
                case 644: //Asuransi Pemerintah (BPJS / AKSES)
                case 645: //Asuransi Swasta
                case 646: //Keringanan (Cost Sharing)
                case 648: //Kartu Sehat
                case 649: //Keterangan Tidak Mampu
                case 650: //Lain-Lain
                    var regq = new RegistrationQuery("a");
                    var guarq = new GuarantorQuery("b");
                    regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID);
                    regq.Select(@"<COUNT(a.RegistrationNo) AS 'qty'>", @"<ISNULL(SUM(DATEDIFF(dd, a.RegistrationDate, ISNULL(a.DischargeDate, a.RegistrationDate)) + 1), 0) AS 'los'>");
                    regq.Where(
                        regq.IsVoid == false,
                        regq.SRRegistrationType == "IPR",
                        guarq.RlMasterReportItemID == rlMasterReportItemId
                        );
                    regq.Where(string.Format("<MONTH(a.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq.Where(string.Format("<YEAR(a.DischargeDate) = {0}>", year.ToString()));

                    DataTable dtri = regq.LoadDataTable();

                    if (dtri.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtri.Rows)
                        {
                            riJpk += Convert.ToInt32(row["qty"]);
                            riJld += Convert.ToInt32(row["los"]);
                        }
                    }

                    regq = new RegistrationQuery("a");
                    guarq = new GuarantorQuery("b");
                    regq.InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID);
                    regq.Select(regq.ServiceUnitID, @"<COUNT(a.RegistrationNo) AS 'qty'>");
                    regq.Where(
                        regq.IsVoid == false,
                        regq.SRRegistrationType != "OPR",
                        guarq.RlMasterReportItemID == rlMasterReportItemId
                        );
                    regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                    regq.GroupBy(regq.ServiceUnitID);

                    DataTable dtrj = regq.LoadDataTable();

                    if (dtrj.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtrj.Rows)
                        {
                            if (row["ServiceUnitID"].ToString() == serviceUnitLaboratoryId)
                                rjLab += Convert.ToInt32(row["qty"]);
                            else if (row["ServiceUnitID"].ToString() == serviceUnitRadiologyId)
                                rjRad += Convert.ToInt32(row["qty"]);
                            else
                                rjLl += Convert.ToInt32(row["qty"]);
                            rj += Convert.ToInt32(row["qty"]);
                        }
                    }
                    break;

                case 643: //Asuransi:
                    var regq2 = new RegistrationQuery("a");
                    var guarq2 = new GuarantorQuery("b");
                    regq2.InnerJoin(guarq2).On(regq2.GuarantorID == guarq2.GuarantorID);
                    regq2.Select(@"<COUNT(a.RegistrationNo) AS 'qty'>", @"<ISNULL(SUM(DATEDIFF(dd, a.RegistrationDate, ISNULL(a.DischargeDate, a.RegistrationDate)) + 1), 0) AS 'los'>");
                    regq2.Where(
                        regq2.IsVoid == false,
                        regq2.SRRegistrationType == "IPR",
                        guarq2.RlMasterReportItemID.In(644, 645)
                        );
                    regq2.Where(string.Format("<MONTH(a.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq2.Where(string.Format("<YEAR(a.DischargeDate) = {0}>", year.ToString()));

                    DataTable dtri2 = regq2.LoadDataTable();

                    if (dtri2.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtri2.Rows)
                        {
                            riJpk += Convert.ToInt32(row["qty"]);
                            riJld += Convert.ToInt32(row["los"]);
                        }
                    }

                    regq2 = new RegistrationQuery("a");
                    guarq2 = new GuarantorQuery("b");
                    regq2.InnerJoin(guarq2).On(regq2.GuarantorID == guarq2.GuarantorID);
                    regq2.Select(regq2.ServiceUnitID, @"<COUNT(a.RegistrationNo) AS 'qty'>");
                    regq2.Where(
                        regq2.IsVoid == false,
                        regq2.SRRegistrationType != "IPR",
                        guarq2.RlMasterReportItemID.In(644, 645)
                        );
                    regq2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                    regq2.GroupBy(regq2.ServiceUnitID);

                    DataTable dtrj2 = regq2.LoadDataTable();

                    if (dtrj2.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtrj2.Rows)
                        {
                            if (row["ServiceUnitID"].ToString() == serviceUnitLaboratoryId)
                                rjLab += Convert.ToInt32(row["qty"]);
                            else if (row["ServiceUnitID"].ToString() == serviceUnitRadiologyId)
                                rjRad += Convert.ToInt32(row["qty"]);
                            else
                                rjLl += Convert.ToInt32(row["qty"]);
                            rj += Convert.ToInt32(row["qty"]);
                        }
                    }
                    break;

                case 647: //Gratis:
                    var regq3 = new RegistrationQuery("a");
                    var guarq3 = new GuarantorQuery("b");
                    regq3.InnerJoin(guarq3).On(regq3.GuarantorID == guarq3.GuarantorID);
                    regq3.Select(@"<COUNT(a.RegistrationNo) AS 'qty'>", @"<ISNULL(SUM(DATEDIFF(dd, a.RegistrationDate, ISNULL(a.DischargeDate, a.RegistrationDate)) + 1), 0) AS 'los'>");
                    regq3.Where(
                        regq3.IsVoid == false,
                        regq3.SRRegistrationType == "IPR",
                        guarq3.RlMasterReportItemID.In(648, 649, 650)
                        );
                    regq3.Where(string.Format("<MONTH(a.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq3.Where(string.Format("<YEAR(a.DischargeDate) = {0}>", year.ToString()));

                    DataTable dtri3 = regq3.LoadDataTable();

                    if (dtri3.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtri3.Rows)
                        {
                            riJpk += Convert.ToInt32(row["qty"]);
                            riJld += Convert.ToInt32(row["los"]);
                        }
                    }

                    regq3 = new RegistrationQuery("a");
                    guarq3 = new GuarantorQuery("b");
                    regq3.InnerJoin(guarq3).On(regq3.GuarantorID == guarq3.GuarantorID);
                    regq3.Select(regq3.ServiceUnitID, @"<COUNT(a.RegistrationNo) AS 'qty'>");
                    regq3.Where(
                        regq3.IsVoid == false,
                        regq3.SRRegistrationType != "IPR",
                        guarq3.RlMasterReportItemID.In(648, 649, 650)
                        );
                    regq3.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                    regq3.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                    regq3.GroupBy(regq3.ServiceUnitID);

                    DataTable dtrj3 = regq3.LoadDataTable();

                    if (dtrj3.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtrj3.Rows)
                        {
                            if (row["ServiceUnitID"].ToString() == serviceUnitLaboratoryId)
                                rjLab += Convert.ToInt32(row["qty"]);
                            else if (row["ServiceUnitID"].ToString() == serviceUnitRadiologyId)
                                rjRad += Convert.ToInt32(row["qty"]);
                            else
                                rjLl += Convert.ToInt32(row["qty"]);
                            rj += Convert.ToInt32(row["qty"]);
                        }
                    }
                    break;
            }
        }
    }
}
