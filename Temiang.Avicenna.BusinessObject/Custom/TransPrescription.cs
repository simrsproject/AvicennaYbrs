using System;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPrescription
    {
        public static string PrescriptionHist(string paramedicID, string registrationNo, DateTime prescriptionDate)
        {
            var query = new TransPrescriptionItemQuery("a");
            var presc = new TransPrescriptionQuery("b");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");
            var oriconsume = new ConsumeMethodQuery("h");

            presc.Select(
                presc.PrescriptionNo,
                query.SequenceNo,
                presc.PrescriptionDate,
                presc.ParamedicID,
                presc.CreatedByUserID,
                item.ItemName,
                @"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
                @"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName) AS SRConsumeMethodName>",
                presc.IsUnitDosePrescription,
                query.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                query.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel,
                @"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
                @"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
                query.EmbalaceQty,
                presc.Note,
                @"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
                @"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                query.LineAmount,
                query.Notes
                );

            presc.LeftJoin(query).On(query.PrescriptionNo == presc.PrescriptionNo);
            presc.LeftJoin(item).On(query.ItemID == item.ItemID);
            presc.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);
            presc.LeftJoin(oriconsume).On(query.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            presc.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);

            presc.Where(presc.ParamedicID == paramedicID);
            presc.Where(presc.RegistrationNo == registrationNo, presc.PrescriptionDate >= prescriptionDate, presc.PrescriptionDate < prescriptionDate.AddDays(1));
            presc.Where(presc.IsVoid == false);

            presc.OrderBy(presc.PrescriptionDate.Descending, presc.PrescriptionNo.Descending);
            presc.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var table = presc.LoadDataTable();


            // Ambil list PrescriptionNo
            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             PrescriptionNo = t.Field<string>("PrescriptionNo")
                         }
                             into g
                         select new
                         {
                             g.Key.PrescriptionNo
                         };


            var prescriptionHeader = "";
            var sbPresciption = new StringBuilder();
            foreach (var p in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();
                foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == p.PrescriptionNo))
                {

                    if (i == 0)
                    {
                        prescriptionHeader = string.Format("[ Presc No: {0} ]", r["PrescriptionNo"]);
                    }
                    i++;

                    if (r["SequenceNo"] == DBNull.Value) continue;

                    if (!Convert.ToBoolean(r["IsCompound"]))
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7}){8}",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("{0}", @"R/") : "    ",
                            r["ItemName"],
                            r["ResultQty"],
                            StdRefItemName("ItemUnit", r["SRItemUnit"].ToString()),
                            r["SRConsumeMethodName"],
                            r["ConsumeQty"],
                            StdRefItemName("DosageUnit", r["SRConsumeUnit"].ToString()),
                            r["Notes"],
                            Environment.NewLine);
                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9}){10}",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("{0}", @"R/") : "    ",
                            r["ItemName"],
                            r["EmbalaceQty"],
                            r["EmbalaceLabel"],
                            r["DosageQty"],
                            StdRefItemName("DosageUnit", r["SRDosageUnit"].ToString()),
                            r["SRConsumeMethodName"],
                            r["ConsumeQty"],
                            StdRefItemName("DosageUnit", r["SRConsumeUnit"].ToString()),
                            r["Notes"],
                            Environment.NewLine);
                    }
                }

                sbPresciption.AppendFormat("{0}{1}{2}{1}", prescriptionHeader, Environment.NewLine, sbItem);

            }
            return sbPresciption.ToString();
        }

        private static string StdRefItemName(string standardReferenceID, string itemID)
        {
            if (itemID == null) return string.Empty;
            var stdi = new AppStandardReferenceItem();
            if (stdi.LoadByPrimaryKey(standardReferenceID, itemID))
                return stdi.ItemName;
            else
                return string.Empty;
        }
        public static void SoapeUpdatePrescriptionHist(string paramedicID, string registrationNo, DateTime updateDate)
        {
            // Hanya update SAOP regitration bersangkutan dari resep registrasi bersangkutan
            updateDate = updateDate.Date;
            string prescHist = PrescriptionHist(paramedicID, registrationNo, updateDate);

            var rimQr = new RegistrationInfoMedicQuery("a");
            rimQr.Where(rimQr.RegistrationNo == registrationNo);

            rimQr.Where(
                rimQr.ParamedicID == paramedicID,
                rimQr.SRMedicalNotesInputType == "SOAP",
                rimQr.DateTimeInfo >= updateDate, rimQr.DateTimeInfo < updateDate.AddDays(1)
            );

            var rimColl = new RegistrationInfoMedicCollection();
            rimColl.Load(rimQr);

            foreach (var row in rimColl)
            {
                row.PrescriptionCurrentDay = prescHist;
            }

            rimColl.Save();

            // PPA Notes tidak diupdate karena itu adalah catatan perawat, dokter entrinya di RegistrationInfoMedic (Progress Notes)

        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string RegistrationTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
        }

        public double RecipeAmount(string entryMode, string guarantor, string itemId, decimal resultQty, string parentNo, string prescriptionQty, bool isCompound)
        {
            double recipeAmount = 0;
            var tpi = new TransPrescriptionItem();
            if (isCompound == false)
            {
                var gr = new Guarantor();
                gr.LoadByPrimaryKey(guarantor);
                if (!(gr.RecipeMarginValueNonCompound == null) && gr.IsUsingDefaultRecipeAmount == false)
                {
                    recipeAmount = Convert.ToDouble(gr.RecipeMarginValueNonCompound);
                }
                else
                {
                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsOtcFreeRecipeMargin))
                    {
                        if (!string.IsNullOrEmpty(entryMode) && entryMode == "otc")
                            recipeAmount = 0;
                        else
                            if (resultQty > 0)
                            recipeAmount = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));
                        else
                            recipeAmount = 0;
                    }
                    else
                    {
                        if (resultQty > 0)
                        {
                            recipeAmount = Convert.ToDouble(AppParameter.GetParameterValue(AppParameter.ParameterItem.RecipeMarginValueNonCompound));

                            var i = new ItemProductMedic();
                            if (i.LoadByPrimaryKey(itemId))
                            {
                                if (i.IsNoPrescriptionFee ?? false)
                                    recipeAmount = 0;
                            }
                        }
                        else
                            recipeAmount = 0;

                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(parentNo) || AppParameter.IsYes(AppParameter.ParameterItem.IsRecipeMarginValueForEachItemCompound))
                {
                    var gr = new Guarantor();
                    gr.LoadByPrimaryKey(guarantor);
                    if (!(guarantor == null) && gr.IsUsingDefaultRecipeAmount == false)
                    {
                        var margin = new GuarantorRecipeMarginValue();
                        margin.Query.Where(margin.Query.GuarantorID == guarantor,string.Format("<{0} BETWEEN StartingValue AND EndingValue>",
                            new Fraction(string.IsNullOrEmpty(prescriptionQty) ? "0" : prescriptionQty)));
                        if (margin.Query.Load()) recipeAmount = Convert.ToDouble(margin.RecipeAmount);
                        else recipeAmount = 0;
                    }
                    else
                    {
                        var margin = new RecipeMarginValue();                        
                        margin.Query.Where(string.Format("<{0} BETWEEN StartingValue AND EndingValue>",
                            new Fraction(string.IsNullOrEmpty(prescriptionQty) ? "0" : prescriptionQty)));
                        if (margin.Query.Load()) recipeAmount = Convert.ToDouble(margin.RecipeAmount);
                        else recipeAmount = 0;
                    }
                }
                else recipeAmount = 0;
            }
            return recipeAmount;
        }
    }
}
