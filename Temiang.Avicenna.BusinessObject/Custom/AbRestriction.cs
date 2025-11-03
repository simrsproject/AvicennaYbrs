using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AbRestriction
    {
        public class AntibioticLevel
        {
            // Samakan dengan yg di AppConstant
            public const int AllAntibiotic = 9999;
            public const int StepUp = 1000;
            public const int StepDown = 1001;
            public const int SwitchIvToOral = 1003;
            public const int AddAntibiotic = 1002;
            public const int NoNeedAntibiotic = 0;
            public const int UseAbNonProphylaxis = 1004;
        }
        public class RasproType
        {
            public const string Rasal = "RASAL";
            public const string Raslan = "RASLAN";
            public const string Raspatur = "RASPATUR";
            public const string Prophylaxis = "PPLAXIS";
            public const string Raspraja = "RASPRAJA";

        }
        /// <summary>
        /// Validasi antibiotik yg digunakan
        /// berdasarkan antibiotik pada form raspro terakhir atau pada AB level jika form raspronya baru atau belum ada antibiotiknya
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="abLevel"></param>
        /// <param name="abRestrictionID"></param>
        /// <param name="itemID"></param>
        /// <param name="isAllowed"></param>
        /// <returns></returns>
        public static bool IsAllowed(string regNo, string itemID, RegistrationRaspro baseOnRasproForm, bool isModeNewRaspro, bool isAntibioticRestriction, string abrForLine, ref string messageResult)
        {
            //var ipm = new ItemProductMedic();
            //if (!ipm.LoadByPrimaryKey(itemID)) return true;

            //if (!(ipm.IsAntibiotic ?? false))
            //    return true;

            if (baseOnRasproForm.SeqNo == null)
            {
                // Load last Raspro Form have AB Item
                // Prophylaxis with AntibioticLevel == AntibioticLevel.UseAbNonProphylaxis has no AB Item (must exclude)
                baseOnRasproForm = new RegistrationRaspro();
                baseOnRasproForm.Query.Where(
                    baseOnRasproForm.Query.AndNot(baseOnRasproForm.Query.RegistrationNo == regNo,
                        baseOnRasproForm.Query.And(baseOnRasproForm.Query.SRRaspro == RasproType.Prophylaxis, baseOnRasproForm.Query.AntibioticLevel == AntibioticLevel.UseAbNonProphylaxis)));
                baseOnRasproForm.Query.es.Top = 1;
                baseOnRasproForm.Query.OrderBy(baseOnRasproForm.Query.SeqNo.Descending);
                if (!baseOnRasproForm.Query.Load())
                {
                    if (!string.IsNullOrWhiteSpace(abrForLine))
                    {
                        var itemName = string.Empty;
                        var qr = new AppStandardReferenceItemQuery("a");
                        qr.Select(qr.ItemName);
                        qr.Where(qr.StandardReferenceID == "AntibioticLine", qr.ItemID == abrForLine);

                        var ent = new AppStandardReferenceItem();
                        if (ent.Load(qr))
                            itemName = ent.ItemName;

                        messageResult = string.Format("This item is antibiotic line \"{0}\" and for antibiotic line \"{0}\" must create appropriate RASPRO Form first", itemName);
                    }
                    else
                        messageResult = "This item is antibiotic and for antibiotic must create appropriate RASPRO Form first";

                    return false; // Untuk status tidak dibolehkan lanjut
                }
            }

            // Cek UseAbNonProphylaxis
            if (baseOnRasproForm.AntibioticLevel == AntibioticLevel.UseAbNonProphylaxis)
            {
                // Load NonProphylaxis 
                var nonProphylaxis = new RegistrationRaspro();
                nonProphylaxis.Query.Where(nonProphylaxis.Query.RegistrationNo == regNo, nonProphylaxis.Query.SRRaspro != RasproType.Prophylaxis);
                nonProphylaxis.Query.es.Top = 1;
                nonProphylaxis.Query.OrderBy(nonProphylaxis.Query.SeqNo.Descending);
                if (!nonProphylaxis.Query.Load())
                {
                    messageResult = "Antibiotic not allowed, please create appropriate RASPRO Form first";
                    return false;
                }
            }

            // Cek apakah sudah ada AB di RASPRO FORM nya,
            // jika belum berarti dianggap form baru dan antibiotik diambil dari suggestion raspronya
            // Jika sudah ada maka antibiotik yg boleh hanya yg ada di form raspro yg telah dibuat

            // Check di history resep
            if (!isModeNewRaspro)
            {
                var prevItemAb = new RegistrationRasproItem();
                if (prevItemAb.LoadByPrimaryKey(regNo, baseOnRasproForm.SeqNo ?? 0, itemID))
                    return true; // Exist

                // Cek jika sebenarnya sudah didefinisikan AB nya apa saja di form raspronya
                prevItemAb = new RegistrationRasproItem();
                prevItemAb.Query.Where(prevItemAb.Query.RegistrationNo == regNo);

                if (baseOnRasproForm.SRRaspro == RasproType.Raspraja)
                    prevItemAb.Query.Where(prevItemAb.Query.RasprajaSeqNo == baseOnRasproForm.SeqNo);
                else
                    prevItemAb.Query.Where(prevItemAb.Query.RasproSeqNo == baseOnRasproForm.SeqNo);

                prevItemAb.Query.es.Top = 1;
                if (prevItemAb.Query.Load())
                {
                    if (isAntibioticRestriction)
                        messageResult = "This antibiotic was not selected on the previous RASPRO form, please create a RASLAN form for antibiotic replacement";
                    else
                        messageResult = "This antibiotic was not selected on the previous RASPRO form, but system not restrict";

                    return (isAntibioticRestriction ? false : true);
                }
            }

            // jika belum berarti dianggap form baru dan antibiotik diambil dari suggestion raspronya
            if (baseOnRasproForm.AntibioticLevel.ToInt() == AntibioticLevel.AllAntibiotic) // All AB Allowed
                return true;

            if (baseOnRasproForm.AntibioticLevel > 0 && baseOnRasproForm.AntibioticLevel < AntibioticLevel.AllAntibiotic)
            {
                // Check di AB Restriction
                var qr = new AbRestrictionItemQuery("qr");
                var zam = new ItemProductMedicZatActiveQuery("zam");
                qr.InnerJoin(zam).On(qr.ZatActiveID == zam.ZatActiveID);
                qr.Select(zam.ItemID);
                qr.es.Top = 1;
                qr.Where(qr.AbRestrictionID == baseOnRasproForm.AbRestrictionID, qr.AbLevel == baseOnRasproForm.AntibioticLevel, zam.ItemID == itemID);

                var abri = new AbRestrictionItem();
                if (abri.Load(qr)) // Exist
                    return true;
                else
                {
                    if (isAntibioticRestriction)
                        messageResult = "This Antibiotic not allowed, because not in suggestion result RASPRO Form";
                    else
                        messageResult = "This antibiotic not in suggestion, but system not restrict";

                    return (isAntibioticRestriction ? false : true);
                }
            }


            if (isAntibioticRestriction)
                messageResult = "No Antibiotic sugestion, please create appropriate RASPRO Form first";
            else
                messageResult = "This antibiotic not in suggestion, but system not restrict";

            return (isAntibioticRestriction ? false : true);
        }

        #region AntibioticSuggestion
        //internal static string AntibioticSuggestion(bool isShowEditMenu, string regNo, int rasproSeqNo, ref int abLevel, ref string abRestrictionID)
        public static string AntibioticSuggestion(RegistrationRaspro rr, ref int useRasproSeqNo)
        {
            if (rr == null) return string.Empty;

            // Kasus Antibiotik Profilaksis
            if (rr.AntibioticLevel == AntibioticLevel.UseAbNonProphylaxis)
            {
                //Raspro terakhir dan yg digunakan untuk filter bisa berbeda misal kasus AB Profilaksis opr tercemar atau Kotor
                var rrPrev = new RegistrationRaspro();
                rrPrev.Query.es.Top = 1;
                rrPrev.Query.OrderBy(rrPrev.Query.SeqNo.Descending);
                rrPrev.Query.Where(rrPrev.Query.RegistrationNo == rr.RegistrationNo, rrPrev.Query.SRRaspro != RasproType.Prophylaxis);
                if (rrPrev.Query.Load())
                {
                    useRasproSeqNo = rrPrev.SeqNo ?? 0;
                }
                else
                {
                    // User harus buat form raspro RASAL/RASPATUR dulu
                    useRasproSeqNo = 0;
                }
            }
            else
                // Pakai raspro yg sama
                useRasproSeqNo = rr.SeqNo ?? 0;

            var lastAbSelectedInfo = AntibioticSelectedInfo(rr, useRasproSeqNo);

            var sb = new StringBuilder();
            // Di akhir form tidak ada isian pilihan
            // Atau Tidak dipilih focus infeksinya (No Infection)
            if (string.IsNullOrWhiteSpace(lastAbSelectedInfo) && (rr.ActionNo.ToInt() == 0 || rr.AbRestrictionID == null))
            {
                if (rr.AntibioticLevel == AntibioticLevel.AllAntibiotic)
                {
                    // AllAntibiotic saat memilih AB untuk form RAspro nya
                    // Jika sudah pernah diberi AB maka pakai AB sebelumnya
                    sb.AppendLine("<fieldset>");
                    sb.AppendLine("<p><strong>System not restricting antibiotic</strong></p> ");
                    sb.AppendLine("</fieldset>");
                    return sb.ToString();
                }
                if (rr.AntibioticLevel == AntibioticLevel.NoNeedAntibiotic)
                {
                    sb.AppendLine("<fieldset>");
                    sb.AppendLine("<p><strong>No need antibiotics</strong></p> ");
                    sb.AppendLine("</fieldset>");
                    return sb.ToString();
                }

                if (rr.AntibioticLevel == AntibioticLevel.UseAbNonProphylaxis)
                {
                    sb.AppendLine("<fieldset>");
                    sb.AppendLine("<p style=\"color: red; \"><strong>Using antibiotics from RASAL</strong></p> ");
                    sb.AppendLine("</fieldset>");
                    return sb.ToString();
                }
            }


            // Raspro Item
            sb.AppendLine(lastAbSelectedInfo);

            // Tampilkan suggestion dari RASPRO
            sb.AppendLine(AntibioticSuggestionSymbolInfo());

            // RASAL dan RASLAN ada detilnya tetapi yg lain tidak (RASPRAJA, RASPATUR, PROFILAKSIS)
            if (rr.SRRaspro.Equals(RasproType.Rasal) || rr.SRRaspro.Equals(RasproType.Raslan))
            {
                var rrl = new RegistrationRasproLine();
                rrl.Query.Where(rrl.Query.RegistrationNo == rr.RegistrationNo, rrl.Query.SeqNo == rr.SeqNo);
                rrl.Query.es.Top = 1;
                rrl.Query.OrderBy(rrl.Query.RasproLineID.Descending);
                if (rrl.Query.Load())
                {
                    if (rr.ActionNo == null)
                    {
                        var ras = new Raspro();
                        ras.LoadByPrimaryKey(rrl.RasproLineID);

                        sb.AppendFormat("<fieldset><legend><strong>{0}</strong></legend>", rrl.Condition == "1" ? ras.YesActionDescription : ras.NoActionDescription);
                    }
                    else
                    {
                        var raa = new RasproAction();
                        raa.LoadByPrimaryKey(rrl.RasproLineID, rrl.Condition, rr.ActionNo ?? 0);
                        sb.AppendFormat("<fieldset><legend><strong>{0}</strong></legend>", raa.ActionDescription);
                    }

                }
            }
            else
            {
                sb.AppendLine("<fieldset>");
            }

            if (rr.AbRestrictionID != null)
            {
                var abrs = new AbRestrictionSuggestion();
                abrs.LoadByPrimaryKey(rr.AbRestrictionID, rr.AntibioticLevel ?? 0);
                sb.AppendLine("<br/>");
                sb.AppendLine(abrs.SuggestionNote);
                sb.AppendLine("</fieldset>");
                return sb.ToString();
            }

            return string.Empty;
        }

        private static string AntibioticSelectedInfo(RegistrationRaspro rr, int useRasproSeqNo)
        {
            // Raspro Item / ZatActive yg sudah dipilih
            var sbRasproItem = new StringBuilder();
            if (useRasproSeqNo > 0)
            {
                var rri = new RegistrationRasproItemQuery("rg");
                var za = new ZatActiveQuery("za");
                rri.InnerJoin(za).On(rri.ZatActiveID == za.ZatActiveID);

                var item = new ItemQuery("i");
                rri.InnerJoin(item).On(rri.ItemID == item.ItemID);

                var consume = new ConsumeMethodQuery("cm");
                rri.InnerJoin(consume).On(rri.SRConsumeMethod == consume.SRConsumeMethod);


                rri.Where(rri.RegistrationNo == rr.RegistrationNo);
                if (rr.SRRaspro == RasproType.Raspraja)
                    rri.Where(rri.RasprajaSeqNo == useRasproSeqNo);
                else
                    rri.Where(rri.RasproSeqNo == useRasproSeqNo);

                rri.Select(za.ZatActiveName, item.ItemName, rri.SRConsumeMethod, rri.SRItemUnit, rri.ConsumeQty, rri.SRConsumeUnit, consume.SRConsumeMethodName);
                var dtbZa = rri.LoadDataTable();


                if (dtbZa.Rows.Count > 0)
                {
                    var dosageUnit = string.Empty;
                    sbRasproItem.AppendLine(@"<fieldset><legend><b> Antibiotic Selected </b></legend> <ul>");
                    foreach (DataRow row in dtbZa.Rows)
                    {
                        dosageUnit = string.Empty;
                        var stdi = new AppStandardReferenceItem();
                        if (stdi.LoadByPrimaryKey("DosageUnit", row["SRConsumeUnit"].ToString()))
                            dosageUnit = stdi.ItemName;

                        sbRasproItem.AppendFormat("<li>{0} ({1}) {2} @ {3} {4}</li>",
    row["ItemName"], row["ZatActiveName"], row["SRConsumeMethodName"], row["ConsumeQty"], dosageUnit);
                    }
                    sbRasproItem.AppendLine("</ul></fieldset>");

                }
            }
            return sbRasproItem.ToString();
        }

        public static string AntibioticSuggestionSymbolInfo()
        {
            return @"<fieldset>
                <legend><b>Symbol Information</b></legend>
                Tanda <b style='color: red;'>+</b> : berarti antibiotik harus dikombinasi<br />
                Tanda <b style='color: red;'>+/-</b> : berarti antibiotik boleh dikombinasi atau diberikan tunggal<br />
                Tanda <b style='color: red;'>-</b> : berarti atau (pilihan antara a atau b)<br />
                Tanda <b style='color: red;'>*</b> : berarti bahwa peresepan kategori XDR/PDF & MRSA/E harus berdasarkan pada ketentuan pada poin E halaman 43 panduan antibiotik ini<br />
            </fieldset>";
        }
        public static string AntibioticSuggestionRasproInfo(RegistrationRaspro rr)
        {
            var abr = new AbRestriction();
            abr.LoadByPrimaryKey(rr.AbRestrictionID);


            if (rr.SRRaspro == RasproType.Prophylaxis)
            {
                var woundClassification = string.Empty;
                var stdi = new AppStandardReferenceItem();
                if (stdi.LoadByPrimaryKey("WoundClassification", rr.SRWoundClassification))
                    woundClassification = stdi.ItemName;

                return string.Format(@"<fieldset>
                <legend><b>RASPRO FORM</b></legend>
<strong>Type:</strong>&nbsp;{0}&nbsp;&nbsp;<strong>Create Date:</strong>&nbsp;{1}&nbsp;<strong>No:</strong>&nbsp;{2}&nbsp;<strong>Surgery Name:</strong>&nbsp;{3}&nbsp;<strong>Wound Type:</strong>&nbsp;{4}<br />
            </fieldset>", rr.SRRaspro, rr.RasproDateTime.Value.ToString("dd-MMM-yyyy HH:mm"), rr.SeqNo, abr.AbRestrictionName, woundClassification);

            }

            return string.Format(@"<fieldset>
                <legend><b>RASPRO FORM</b></legend>
<strong>Type:</strong>&nbsp;{0}&nbsp;&nbsp;<strong>Create Date:</strong>&nbsp;{1}&nbsp;<strong>No:</strong>&nbsp;{2}&nbsp;<strong>Focus Infection:</strong>&nbsp;{3}<br />
            </fieldset>", rr.SRRaspro, rr.RasproDateTime.Value.ToString("dd-MMM-yyyy HH:mm"), rr.SeqNo, abr.AbRestrictionName);
        }
        #endregion

    }
}
