using System;
using System.Data;
using Temiang.Avicenna.BusinessObject; 

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport317
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

        public static void Process(int fromMonth, int toMonth, int year, string rlMasterReportItemCode, out int jmlItemObat, out int jmlItemObatRs)
        {
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial) == "RSISB")
            {
                var its = new ItemCollection();
                var itq = new ItemQuery("a");
                var ipmq = new ItemProductMedicQuery("b");
                itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
                itq.Where(itq.IsActive == true);
                itq.Select(itq.ItemID);
                itq.es.Distinct = true;
                switch (rlMasterReportItemCode)
                {
                    //case "1": //obat generik
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.isGeneric = 1) OR (YEAR(a.CreatedDateTime) < {3} AND b.isGeneric = 1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.isGeneric = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;
                    //case "2": //obat non generik formularium
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;
                    //case "3": //obat non generik non formulariom
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;

                    case "1": //Obat Generik Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsGeneric = 1 AND b.IsFormularium =1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsGeneric = 1 AND b.IsFormularium =1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "2": //Obat Generik Non Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "3": //Obat Non Generik Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "4": //Obat Non Generik Non Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "99": //Total
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND (b.IsGeneric = 1 OR b.IsNonGeneric = 1))>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                }

                its.Load(itq);
                jmlItemObat = its.Count;
                jmlItemObatRs = jmlItemObat;

                its = new ItemCollection();
                itq = new ItemQuery("a");
                ipmq = new ItemProductMedicQuery("b");

                itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
                itq.Where(itq.IsActive == true);
                itq.Select(itq.ItemID);
                itq.es.Distinct = true;

                switch (rlMasterReportItemCode)
                {
                    //case "1": //obat generik
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.isGeneric = 1 AND b.IsFormularium =1) OR (YEAR(a.CreatedDateTime) < {3} AND b.isGeneric = 1 AND b.IsFormularium =1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.isGeneric = 1 AND b.IsFormularium =1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;
                    //case "2": //obat non generik formularium
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;
                    //case "3": //obat non generik non formulariom
                    //    itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                    //    break;

                    case "1": //Obat Generik Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsGeneric = 1 AND b.IsFormularium =1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsGeneric = 1 AND b.IsFormularium =1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "2": //Obat Generik Non Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "3": //Obat Non Generik Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 1)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "4": //Obat Non Generik Non Formularium Nasional
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0)>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                    case "99": //Total
                        itq.Where(string.Format("<(MONTH(a.CreatedDateTime) <= {0} AND MONTH(a.CreatedDateTime) <= {1} AND YEAR(a.CreatedDateTime) = {2} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (YEAR(a.CreatedDateTime) < {3} AND b.IsNonGeneric = 1 AND b.IsFormularium = 0) OR (MONTH(a.CreatedDateTime) >= {4} AND MONTH(a.CreatedDateTime) <= {5} AND YEAR(a.CreatedDateTime) = {6} AND (b.IsGeneric = 1 OR b.IsNonGeneric = 1))>", fromMonth.ToString(), toMonth.ToString(), year.ToString(), year.ToString(), fromMonth.ToString(), toMonth.ToString(), year.ToString()));
                        break;
                }

                its.Load(itq);
            }
            else
            {
                var its = new ItemCollection();
                var itq = new ItemQuery("a");
                var ipmq = new ItemProductMedicQuery("b");
                itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
                itq.Where(itq.IsActive == true);
                itq.Select(itq.ItemID);
                itq.es.Distinct = true;

                switch (rlMasterReportItemCode)
                {
                    //case "1": //obat generik
                    //    itq.Where(ipmq.isGeneric == true);        
                    //        break;

                    //case "2": //obat non generik formularium
                    //    itq.Where(
                    //        ipmq.IsNonGeneric == true,
                    //        ipmq.IsFormularium == true);
                    //    break;
                    //case "3": //obat non generik non formulariom
                    //    itq.Where(
                    //        ipmq.IsNonGeneric == true,
                    //        ipmq.IsFormularium == false);
                    //    break;

                    case "1": //Obat Generik Formularium Nasional
                        itq.Where(
                            ipmq.IsGeneric == true,
                            ipmq.IsFormularium == true
                            );
                        break;
                    case "2": //Obat Generik Non Formularium Nasional
                        itq.Where(
                            ipmq.IsGeneric == true,
                            ipmq.IsFormularium == false
                            );
                        break;
                    case "3": //Obat Non Generik Formularium Nasional
                        itq.Where(
                            ipmq.IsNonGeneric == true,
                            ipmq.IsFormularium == true
                            );
                        break;
                    case "4": //Obat Non Generik Non Formularium Nasional
                        itq.Where(
                            ipmq.IsNonGeneric == true,
                            ipmq.IsFormularium == false
                            );
                        break;
                    case "99": //Total
                        itq.Where(
                            itq.Or(ipmq.IsGeneric == true, ipmq.IsNonGeneric == true)
                            );
                        break;
                }

                its.Load(itq);
                jmlItemObat = its.Count;
                jmlItemObatRs = jmlItemObat;

                its = new ItemCollection();
                itq = new ItemQuery("a");
                ipmq = new ItemProductMedicQuery("b");

                itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
                itq.Where(itq.IsActive == true);
                itq.Select(itq.ItemID);
                itq.es.Distinct = true;

                switch (rlMasterReportItemCode)
                {
                    //case "1": //obat generik
                    //    itq.Where(
                    //        ipmq.isGeneric == true,
                    //        ipmq.IsFormularium == true
                    //        );
                    //    break;
                    //case "2": //obat non generik formularium
                    //    itq.Where(
                    //        ipmq.IsNonGeneric == true,
                    //        ipmq.IsFormularium == true
                    //        );
                    //    break;
                    //case "3": //obat non generik non formulariom
                    //    itq.Where(itq.ItemID == "&");
                    //    break;

                    case "1": //Obat Generik Formularium Nasional
                        itq.Where(
                            ipmq.IsGeneric == true,
                            ipmq.IsFormularium == true
                            );
                        break;
                    case "2": //Obat Generik Non Formularium Nasional
                        itq.Where(
                            ipmq.IsGeneric == true,
                            ipmq.IsFormularium == false
                            );
                        break;
                    case "3": //Obat Non Generik Formularium Nasional
                        itq.Where(
                            ipmq.IsNonGeneric == true,
                            ipmq.IsFormularium == true
                            );
                        break;
                    case "4": //Obat Non Generik Non Formularium Nasional
                        itq.Where(
                            ipmq.IsNonGeneric == true,
                            ipmq.IsFormularium == false
                            );
                        break;
                    case "99": //Total
                        itq.Where(
                            itq.Or(ipmq.IsGeneric == true, ipmq.IsNonGeneric == true)
                            );
                        break;
                }

                its.Load(itq);

            }
        }
    }
}