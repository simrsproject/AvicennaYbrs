using System;
using System.Configuration;
using System.Data;
using System.Runtime.Remoting.Services;
using System.Text;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject;
using System.Linq;
using System.Collections.Generic;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicationReceive
    {
        #region Import TransPrescription to Medication Receive
        //[Obsolete("Ganti dengan ImportFromPrescriptionBaseOnTherapy karena tidak cocok untuk penerapan konsep UDD", true)]
        //public static void ImportFromPrescription(string prescriptionNo, string registrationNo, DateTime receiveDateTime)
        //{
        //    using (var trans = new esTransactionScope())
        //    {
        //        // Cek apakah sudah di import
        //        var receive = new MedicationReceive();
        //        receive.Query.Where(receive.Query.RegistrationNo == registrationNo, receive.Query.RefTransactionNo == prescriptionNo);
        //        receive.Query.es.Top = 1;
        //        var isImported = (receive.Query.Load() && receive.MedicationReceiveNo != null);

        //        // Import
        //        var presc = new TransPrescriptionItemQuery("p");
        //        var qItemMedic = new ItemProductMedicQuery("im");
        //        presc.InnerJoin(qItemMedic).On(presc.ItemID == qItemMedic.ItemID);
        //        presc.Select
        //        (
        //            presc,
        //            presc.TakenQty.As("QtyInput"),
        //            presc.ResultQty.As("RefQty"),
        //            presc.Acpcdc.As("SRMedicationConsume"),
        //            "<GETDATE() as StartDateTime>"
        //        );
        //        presc.Where(presc.PrescriptionNo == prescriptionNo, presc.Or(presc.ParentNo.IsNull(), presc.ParentNo == string.Empty), qItemMedic.IsMedication == true);
        //        presc.OrderBy(presc.SequenceNo.Ascending);
        //        var dtb = presc.LoadDataTable();
        //        foreach (DataRow row in dtb.Rows)
        //        {
        //            // Hanya tipe header saja yg diimport
        //            if (!string.IsNullOrEmpty(row["ParentNo"].ToString())) continue;

        //            // Jika sudah diimport maka hanya yg belum di setup yg diimport
        //            var isUsed = false;
        //            if (isImported)
        //            {
        //                receive = new MedicationReceive();
        //                receive.Query.Where(receive.Query.RegistrationNo == registrationNo, receive.Query.RefTransactionNo == prescriptionNo, receive.Query.RefSequenceNo == row["SequenceNo"].ToString());
        //                receive.Query.es.Top = 1;
        //                if (receive.Query.Load())
        //                {
        //                    var used = new MedicationReceiveUsed();
        //                    used.Query.Where(used.Query.MedicationReceiveNo == receive.MedicationReceiveNo);
        //                    used.Query.es.Top = 1;
        //                    isUsed = (used.Load(used.Query) && used.MedicationReceiveNo != null);
        //                }
        //            }
        //            if (!isUsed)
        //            {
        //                if (isImported)
        //                    UpdateMedicationReceive(row, receiveDateTime);
        //                else if (Convert.ToDecimal(row["QtyInput"]) > 0)
        //                    InsertMedicationReceive(row, registrationNo, receiveDateTime);
        //            }
        //        }

        //        trans.Complete();
        //    }
        //}

        /// <summary>
        /// Import Prescription to Medication Receive per ItemID+ConsumeMethod+ConsumeQty+ConsumeUnit
        /// </summary>
        /// <param name="prescriptionNo"></param>
        /// <param name="registrationNo"></param>
        /// <param name="receiveDateTime"></param>
        /// <param name="isMedicationReceiveCancel">isMedicationReceiveCancel = true digunakan untuk mengurangi ReceiveQty pada Medication</param>
        public static void ImportFromPrescriptionBaseOnTherapy(string prescriptionNo, string registrationNo, DateTime? receiveDateTime, bool isMedicationReceiveCancel = false)
        {
            using (var trans = new esTransactionScope())
            {
                // History
                var recHistColl = new MedicationReceiveCollection();
                recHistColl.Query.Where(recHistColl.Query.RegistrationNo == registrationNo);
                recHistColl.LoadAll();

                // Import
                var prescItem = new TransPrescriptionItemQuery("p");
                var qItemMedic = new ItemProductMedicQuery("im");
                prescItem.InnerJoin(qItemMedic).On(prescItem.ItemID == qItemMedic.ItemID);
                prescItem.Select
                (
                    prescItem,
                    prescItem.ResultQty.As("RefQty"),
                    prescItem.Acpcdc.As("SRMedicationConsume"),
                    "<GETDATE() as StartDateTime>"
                );
                prescItem.Where(prescItem.PrescriptionNo == prescriptionNo, prescItem.Or(prescItem.ParentNo.IsNull(), prescItem.ParentNo == string.Empty), qItemMedic.IsMedication == true);
                prescItem.OrderBy(prescItem.SequenceNo.Ascending);
                var dtb = prescItem.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    // Hanya tipe header saja yg diimport
                    if (!string.IsNullOrEmpty(row["ParentNo"].ToString())) continue;

                    var isCompound = true.Equals(row["IsCompound"]);
                    var curCompoundFormula = string.Empty;
                    decimal qtyReceiveQtyInConsumeUnit = 0;

                    MedicationReceive receiveImported = null;
                    var itemID = row["ItemID"].ToString();
                    if (row["ItemInterventionID"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["ItemInterventionID"].ToString()))
                        itemID = row["ItemInterventionID"].ToString();


                    // Cek Therapy compound & not compound (Handono 230906)
                    if (isCompound) // Racikan
                    {
                        // Compound
                        curCompoundFormula = CompoundFormulaFrom(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());
                        foreach (var item in recHistColl)
                        {
                            // Update CompoundFormula untuk data lama yg belum terekam CompoundFormula nya
                            if (item.ItemID == itemID
                                && (item.IsVoid == null || item.IsVoid == false)
                                && item.SRConsumeMethod.Equals(row["SRConsumeMethod"])
                                && item.ConsumeQtyInString.Equals(row["ConsumeQty"])
                                && item.SRConsumeUnit.Equals(row["SRConsumeUnit"])
                            )
                            {
                                if (string.IsNullOrWhiteSpace(item.CompoundFormula))
                                {
                                    // Update recHistColl variable
                                    var compoundFormula = CompoundFormulaFrom(item.RefTransactionNo, item.RefSequenceNo);
                                    if (!string.IsNullOrEmpty(compoundFormula))
                                    {
                                        item.CompoundFormula = compoundFormula;

                                        // Save
                                        var rec = new MedicationReceive();
                                        if (rec.LoadByPrimaryKey(item.MedicationReceiveNo ?? 0))
                                        {
                                            rec.CompoundFormula = item.CompoundFormula;
                                            rec.Save();
                                        }
                                    }
                                }
                            }

                            // Cek menggunakan CompoundFormula
                            if ((item.IsVoid == null || item.IsVoid == false)
                                && curCompoundFormula.Equals(item.CompoundFormula)
                            )
                            {
                                receiveImported = new MedicationReceive();
                                receiveImported.LoadByPrimaryKey(item.MedicationReceiveNo ?? 0);
                                break;
                            }
                        }

                        // Racikan dianggap 1 konversinya, jika di entrian tidak sama antara kemasan dan consume unit nya
                        // misal untuk kasus kemasan botol dan consume unit nya sendok maka pakai fitur edit Receive Qty di setup medication
                        qtyReceiveQtyInConsumeUnit = Convert.ToDecimal(new Fraction(row["ItemQtyInString"].ToString()));

                    }
                    else
                    {
                        // Non Compound
                        qtyReceiveQtyInConsumeUnit = CalculateReceiveQtyInConsumeUnit(itemID, row["SRItemUnit"].ToString(), row["SRConsumeUnit"].ToString(), row["TakenQty"].ToDecimal());

                        foreach (var item in recHistColl)
                        {
                            if (item.ItemID == itemID
                                && (item.IsVoid == null || item.IsVoid == false)
                                && item.SRConsumeMethod.Equals(row["SRConsumeMethod"])
                                && item.ConsumeQtyInString.Equals(row["ConsumeQty"])
                                && item.SRConsumeUnit.Equals(row["SRConsumeUnit"])

                            )
                            {
                                receiveImported = new MedicationReceive();
                                receiveImported.LoadByPrimaryKey(item.MedicationReceiveNo ?? 0);
                                break;
                            }
                        }
                    }


                    // Jika sudah diimport maka tambahkan balance nya
                    if (receiveImported != null)
                    {
                        if (isMedicationReceiveCancel)
                        {
                            // Kurangi jika untuk proses UnApprove
                            receiveImported.ReceiveQty = receiveImported.ReceiveQty - qtyReceiveQtyInConsumeUnit;
                            receiveImported.BalanceQty = receiveImported.BalanceQty - qtyReceiveQtyInConsumeUnit;
                            receiveImported.BalanceRealQty = receiveImported.BalanceRealQty - qtyReceiveQtyInConsumeUnit;
                            receiveImported.Save();

                            // MedicationReceivePrescLog
                            MedRecPrescLogCreate(receiveImported.MedicationReceiveNo, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), true);

                        }
                        else
                        {
                            // Tambah 
                            receiveImported.ReceiveQty = receiveImported.ReceiveQty + qtyReceiveQtyInConsumeUnit;
                            receiveImported.BalanceQty = receiveImported.BalanceQty + qtyReceiveQtyInConsumeUnit;
                            receiveImported.BalanceRealQty = receiveImported.BalanceRealQty + qtyReceiveQtyInConsumeUnit;
                            receiveImported.Save();

                            // MedicationReceivePrescLog
                            MedRecPrescLogCreate(receiveImported.MedicationReceiveNo, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());
                        }

                    }
                    else if (qtyReceiveQtyInConsumeUnit > 0 && !isMedicationReceiveCancel)
                    {
                        // Add new MedicationReceive
                        var ent = new MedicationReceive();
                        ent.RegistrationNo = registrationNo;
                        ent.MedicationReceiveNo = NewMedicationReceiveNo();

                        ent.RefTransactionNo = row["PrescriptionNo"].ToString();  // Untuk ref pengambilan Item Description
                        ent.RefSequenceNo = row["SequenceNo"].ToString();  // Untuk ref pengambilan Item Description
                        ent.ItemID = itemID;
                        ent.ItemDescription = PrescriptionItemDescription(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());

                        ent.ReceiveQty = qtyReceiveQtyInConsumeUnit;
                        ent.BalanceQty = qtyReceiveQtyInConsumeUnit;
                        ent.BalanceRealQty = qtyReceiveQtyInConsumeUnit;

                        ent.StartDateTime = row["StartDateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["StartDateTime"]);
                        ent.ReceiveDateTime = receiveDateTime;

                        var consumeQty = row["ConsumeQty"].ToString(); // Consume Qty berupa string yg bisa berisi contoh 1/2
                        ent.ConsumeQtyInString = consumeQty; // Simpan untuk pengecekan terapi, nama field beda karena baru dipakai belakangan ketika dirubah ke per terapi

                        ent.ConsumeQty = Convert.ToDecimal(new Fraction(consumeQty)); // Convert ke numeric untuk proses perhitungan balance
                        ent.SRConsumeUnit = row["SRConsumeUnit"].ToString();
                        ent.SRConsumeMethod = row["SRConsumeMethod"].ToString();
                        ent.SRMedicationConsume = row["SRMedicationConsume"].ToString();
                        ent.IsVoid = false;
                        ent.IsContinue = true;
                        ent.IsBroughtHome = false;
                        ent.Note = row["Notes"].ToString();

                        // CompoundFormula untuk pencarian tipe racikan
                        if (string.IsNullOrEmpty(curCompoundFormula))
                            ent.str.CompoundFormula = string.Empty;
                        else
                            ent.CompoundFormula = curCompoundFormula;

                        ent.Save();

                        // MedicationReceivePrescLog
                        MedRecPrescLogCreate(ent.MedicationReceiveNo, row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString());
                    }
                }

                trans.Complete();
            }
        }

        private static void MedRecPrescLogCreate(long? medicationReceiveNo, string prescriptionNo, string sequenceNo, bool isMedicationReceiveCancel = false)
        {
            // MedicationReceivePrescLog
            var medrecPrescLog = new MedicationReceivePrescLog();
            medrecPrescLog.MedicationReceiveNo = medicationReceiveNo;
            medrecPrescLog.PrescriptionNo = prescriptionNo;
            medrecPrescLog.SequenceNo = sequenceNo;
            medrecPrescLog.IsPrescriptionVoid = isMedicationReceiveCancel; 
            medrecPrescLog.Save();
        }

        private static string CompoundFormulaFrom(string prescNo, string seqNo)
        {
            var itemColl = new TransPrescriptionItemCollection();
            itemColl.Query.Where(itemColl.Query.PrescriptionNo == prescNo, itemColl.Query.Or(itemColl.Query.SequenceNo == seqNo, itemColl.Query.ParentNo == seqNo));
            itemColl.Query.OrderBy(itemColl.Query.SequenceNo.Ascending);
            itemColl.Query.Select(itemColl.Query.ItemID, itemColl.Query.ItemInterventionID, itemColl.Query.DosageQty, itemColl.Query.SRDosageUnit, itemColl.Query.IsCompound);
            if (itemColl.LoadAll())
            {
                if (itemColl.Count == 0) return String.Empty;
                if (!true.Equals(itemColl[0].IsCompound)) return String.Empty; // pakai !true karena isinya bisa null dan null != false

                var currItemList = new List<string>();
                foreach (var item in itemColl)
                {
                    currItemList.Add(string.Format("{0}_{1}_{2}", string.IsNullOrWhiteSpace(item.ItemInterventionID) ? item.ItemID : item.ItemInterventionID, item.DosageQty, item.SRDosageUnit));
                }
                currItemList.Sort();

                var compoundFormula = string.Join(";", currItemList);
                return compoundFormula.Trim();
            }
            return String.Empty;
        }

        public static int NewMedicationReceiveNo()
        {
            var qr = new MedicationReceiveQuery("a");
            var fb = new MedicationReceive();
            qr.es.Top = 1;
            qr.OrderBy(qr.MedicationReceiveNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MedicationReceiveNo.ToInt() + 1;
            }
            return 1;
        }

        public static void InsertMedicationReceive(DataRow row, string registrationNo, DateTime receiveDateTime)
        {
            var prescNo = row["PrescriptionNo"].ToString();
            var seqNo = row["SequenceNo"].ToString();

            // Check has register
            var itemID = row["ItemID"].ToString();
            if (row["ItemInterventionID"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["ItemInterventionID"].ToString()))
                itemID = row["ItemInterventionID"].ToString();

            // Check therapy exist
            var qr = new MedicationReceiveQuery("q");
            qr.Where(qr.RegistrationNo == registrationNo,
                qr.ItemID == itemID,
                qr.SRConsumeMethod == row["SRConsumeMethod"].ToString(),
                qr.ConsumeQtyInString == row["ConsumeQty"].ToString(),
                qr.SRConsumeUnit == row["SRConsumeUnit"].ToString(),
                qr.Or(qr.IsVoid.IsNull(), qr.IsVoid == false)
                );
            qr.es.Top = 1;
            var ent = new MedicationReceive();
            if (ent.Load(qr))
                return;

            ent = new MedicationReceive();
            ent.RegistrationNo = registrationNo;
            ent.MedicationReceiveNo = NewMedicationReceiveNo();
            ent.RefTransactionNo = prescNo;
            ent.RefSequenceNo = seqNo;

            PopulateMedicationReceiveUpdate(row, receiveDateTime, ent);
            ent.Save();

        }

        [Obsolete("Ganti dengan ImportFromPrescriptionBaseOnTherapy dgn parameter isUnImport diset true", true)]
        public static void PrescriptionUnApprovalApply(string prescriptionNo, string registrationNo)
        {
            using (var trans = new esTransactionScope())
            {
                // Import
                var presc = new TransPrescriptionItemQuery("p");
                var qItemMedic = new ItemProductMedicQuery("im");
                presc.InnerJoin(qItemMedic).On(presc.ItemID == qItemMedic.ItemID);
                presc.Select
                (
                    presc
                );
                presc.Where(presc.PrescriptionNo == prescriptionNo, presc.Or(presc.ParentNo.IsNull(), presc.ParentNo == string.Empty), qItemMedic.IsMedication == true);
                presc.OrderBy(presc.SequenceNo.Ascending);
                var dtb = presc.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    // Hanya tipe header saja yg diimport
                    if (!string.IsNullOrEmpty(row["ParentNo"].ToString())) continue;

                    // Cek Therapy
                    var receive = new MedicationReceive();
                    receive.Query.Where(receive.Query.RegistrationNo == registrationNo,
                        receive.Query.ItemID == row["ItemID"],
                        receive.Query.SRConsumeMethod == row["SRConsumeMethod"],
                        receive.Query.ConsumeQtyInString == row["ConsumeQty"],
                        receive.Query.SRConsumeUnit == row["SRConsumeUnit"],
                        receive.Query.Or(receive.Query.IsVoid.IsNull(), receive.Query.IsVoid == false)
                        );
                    receive.Query.es.Top = 1;
                    var isImported = (receive.Query.Load() && receive.MedicationReceiveNo != null);

                    // Jika sudah diimport maka kurangi balance nya
                    if (isImported)
                    {
                        decimal qtyInConsumeUnit = 0;

                        if (false.Equals(row["IsCompound"]))
                            qtyInConsumeUnit = CalculateReceiveQtyInConsumeUnit(row["ItemID"].ToString(), row["SRItemUnit"].ToString(), row["SRConsumeUnit"].ToString(), row["TakenQty"].ToDecimal());
                        else
                        {
                            // Racikan dianggap 1 konversinya, jika di entrian tidak sama antara kemasan dan consume unit nya
                            // misal untuk kasus kemasan botol dan consume unit nya sendok maka pakai fitur edit Receive Qty di setup medication
                            qtyInConsumeUnit = Convert.ToDecimal(new Fraction(row["ItemQtyInString"].ToString()));
                        }

                        receive.ReceiveQty = receive.ReceiveQty - qtyInConsumeUnit;
                        if (receive.ReceiveQty < 0)
                            receive.ReceiveQty = 0;

                        receive.BalanceQty = receive.BalanceQty - qtyInConsumeUnit;
                        if (receive.BalanceQty < 0)
                            receive.BalanceQty = 0;

                        receive.BalanceRealQty = receive.BalanceRealQty - qtyInConsumeUnit;
                        if (receive.BalanceRealQty < 0)
                            receive.BalanceRealQty = 0;

                        receive.Save();
                    }
                }

                trans.Complete();
            }
        }


        //public static void InsertMedicationReceive(DataRow row, string registrationNo, DateTime receiveDateTime)
        //{
        //    var prescNo = row["PrescriptionNo"].ToString();
        //    var seqNo = row["SequenceNo"].ToString();

        //    // Check has register
        //    var qr = new MedicationReceiveQuery("q");
        //    qr.Where(qr.RefTransactionNo == prescNo, qr.RefSequenceNo == seqNo);
        //    qr.es.Top = 1;
        //    var ent = new MedicationReceive();
        //    if (ent.Load(qr))
        //        return;

        //    ent = new MedicationReceive();
        //    ent.RegistrationNo = registrationNo;
        //    ent.MedicationReceiveNo = NewMedicationReceiveNo();
        //    ent.RefTransactionNo = prescNo;
        //    ent.RefSequenceNo = seqNo;

        //    PopulateMedicationReceiveUpdate(row, receiveDateTime, ent);
        //    ent.Save();

        //}

        //public static void UpdateMedicationReceive(DataRow row, DateTime receiveDateTime)
        //{
        //    var prescNo = row["PrescriptionNo"].ToString();
        //    var seqNo = row["SequenceNo"].ToString();

        //    // Check has register
        //    var qr = new MedicationReceiveQuery("q");
        //    qr.Where(qr.RefTransactionNo == prescNo, qr.RefSequenceNo == seqNo);
        //    qr.es.Top = 1;
        //    var ent = new MedicationReceive();
        //    if (!ent.Load(qr))
        //        return;

        //    PopulateMedicationReceiveUpdate(row, receiveDateTime, ent);
        //    ent.Save();

        //}

        private static void PopulateMedicationReceiveUpdate(DataRow row, DateTime receiveDateTime, MedicationReceive ent)
        {
            ent.RefQty = Convert.ToDecimal(row["RefQty"]);
            ent.BalanceQty = Convert.ToDecimal(row["QtyInput"]);
            ent.StartDateTime = row["StartDateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["StartDateTime"]);
            ent.ReceiveDateTime = receiveDateTime;

            ent.ItemID = row["ItemID"].ToString();
            if (row["ItemInterventionID"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["ItemInterventionID"].ToString()))
                ent.ItemID = row["ItemInterventionID"].ToString();

            ent.ItemDescription = PrescriptionItemDescription(ent.RefTransactionNo, ent.RefSequenceNo);

            ent.ReceiveQty = CalculateReceiveQtyInConsumeUnit(row["ItemID"].ToString(), row["SRItemUnit"].ToString(), row["SRConsumeUnit"].ToString(), row["QtyInput"].ToDecimal());
            ent.BalanceQty = ent.ReceiveQty;
            ent.BalanceRealQty = ent.ReceiveQty;

            var consumeQtyInString = row["ConsumeQty"].ToString(); // Consume Qty berupa string yg bisa berisi contoh 1/2
            ent.ConsumeQty = Convert.ToDecimal(new Fraction(consumeQtyInString)); // 1/2
            ent.ConsumeQtyInString = consumeQtyInString;
            ent.SRConsumeUnit = row["SRConsumeUnit"].ToString();
            ent.SRConsumeMethod = row["SRConsumeMethod"].ToString();
            ent.SRMedicationConsume = row["SRMedicationConsume"].ToString();
            ent.IsVoid = false;
            ent.IsContinue = true;
            ent.IsBroughtHome = false;
            ent.Note = row["Notes"].ToString();
        }

        private static decimal CalculateReceiveQtyInConsumeUnit(string itemID, string srItemUnit, string srConsumeUnit, decimal qtyInput)
        {
            // Obat paten
            // Konversi dari Item Unit ke ConsumeUnit jika unitnya berbeda
            if (!srItemUnit.Equals(srConsumeUnit))
            {
                // Cek dosage unit di master
                var med = new ItemProductMedic();
                if (med.LoadByPrimaryKey(itemID) && med.Dosage > 0 && srConsumeUnit.Equals(med.SRDosageUnit))
                {
                    return (qtyInput * med.Dosage).ToDecimal();
                }
                else
                {
                    // cek conversion di matrix
                    var conversion = new ItemProductConsumeUnitMatrix();
                    if (conversion.LoadByPrimaryKey(itemID, srItemUnit, srConsumeUnit))
                    {
                        return (qtyInput * conversion.ConversionFactor).ToDecimal();
                    }
                }
            }
            return qtyInput;
        }

        //private static DataTable InitilaizePrescriptionItemForMedication(string prescriptionNo)
        //{

        //    var query = new TransPrescriptionItemQuery("a");
        //    var qItem = new ItemQuery("b");
        //    var qItemMedic = new ItemProductMedicQuery("im");
        //    var qItemIntervention = new ItemQuery("c");
        //    var cons = new ConsumeMethodQuery("d");
        //    var consOri = new ConsumeMethodQuery("cono");
        //    var emb = new EmbalaceQuery("e");


        //    query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
        //    query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID && qItemMedic.IsMedication == true);
        //    query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);
        //    query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
        //    query.LeftJoin(consOri).On(query.OriSRConsumeMethod == consOri.SRConsumeMethod);
        //    query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);


        //    query.Select
        //    (
        //        query,
        //        query.TakenQty.As("QtyInput"),
        //        query.ResultQty.As("RefQty"),
        //        query.Acpcdc.As("SRMedicationConsume"),
        //        "<GETDATE() as StartDateTime>",
        //        qItem.ItemName,
        //        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
        //        cons.SRConsumeMethodName,
        //        emb.EmbalaceLabel,
        //        qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention"),
        //        consOri.SRConsumeMethodName.Coalesce("''").As("SRConsumeMethodNameOri"),
        //        qItem.ItemName.As("ItemNameOri")

        //    );

        //    query.Where(query.PrescriptionNo == prescriptionNo);
        //    query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

        //    var dtb = query.LoadDataTable();

        //    // Racikan dibuat 1 row
        //    var presc = dtb.Clone();
        //    var sbItem = new StringBuilder();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        var isItemIntervention = (row["ItemInterventionID"] != DBNull.Value &&
        //                                  !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()) && !row["ItemID"].Equals(row["ItemInterventionID"]));
        //        if (isItemIntervention)
        //        {
        //            if (row["ItemInterventionID"] != DBNull.Value &&
        //                 !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
        //            {
        //                row["ItemID"] = row["ItemInterventionID"].ToString();
        //                row["ItemName"] = row["ItemNameIntervention"];
        //            }
        //        }

        //        var itemName = isItemIntervention
        //            ? string.Format("<del style='color:gray;'>{0}</del> {1} ", row["ItemNameOri"], row["ItemName"])
        //            : row["ItemName"].ToString();

        //        var resultQty = FieldNumericText(row, "OriResultQty", "ResultQty");
        //        var itemUnit = FieldStringText(row, "OriSRItemUnit", "SRItemUnit");
        //        var dosageQty = FieldNumericText(row, "OriDosageQty", "DosageQty");
        //        var dosageUnit = FieldStringText(row, "OriSRDosageUnit", "SRDosageUnit");

        //        if (Convert.ToBoolean(row["IsCompound"]))
        //        {

        //            // Racikan
        //            if (row["ParentNo"] == DBNull.Value || string.IsNullOrEmpty(row["ParentNo"].ToString()))
        //            {
        //                //Header Racikan
        //                sbItem = new StringBuilder();

        //                var destRow = presc.NewRow();
        //                destRow.ItemArray = row.ItemArray.Clone() as object[];

        //                sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} {6}<br />",
        //                    Convert.ToBoolean(row["IsRFlag"])
        //                        ? "R/"
        //                        : "&nbsp;&nbsp;",
        //                    itemName, row["EmbalaceQty"], row["EmbalaceLabel"], dosageQty,
        //                    dosageUnit, row["Notes"]);

        //                destRow["ItemName"] = sbItem.ToString();
        //                destRow["QtyInput"] = row["EmbalaceQty"];
        //                destRow["RefQty"] = row["EmbalaceQty"];
        //                presc.Rows.Add(destRow);
        //            }
        //            else
        //            {
        //                sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5}<br />",
        //                    Convert.ToBoolean(row["IsRFlag"])
        //                        ? "R/&nbsp;&nbsp;&nbsp;"
        //                        : "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;",
        //                    itemName, row["EmbalaceQty"], row["EmbalaceLabel"], dosageQty,
        //                    dosageUnit);

        //                if (row["SRConsumeMethodNameOri"] != DBNull.Value && !string.IsNullOrEmpty(row["SRConsumeMethodNameOri"].ToString())
        //                      && !row["SRConsumeMethodName"].Equals(row["SRConsumeMethodNameOri"]))
        //                {
        //                    sbItem.AppendFormat("<br /><del style='color:gray;'>{0}</del> {1} ", row["SRConsumeMethodNameOri"],
        //                        row["SRConsumeMethodName"]);
        //                }

        //                presc.Rows[presc.Rows.Count - 1]["ItemName"] = sbItem.ToString();
        //            }
        //        }
        //        else
        //        {
        //            // Obat paten
        //            // Conversi dari Item Unit ke ConsumeUnit (sama dg DOsage Unit) jika unitnya berbeda
        //            if (!row["SRItemUnit"].Equals(row["SRConsumeUnit"]))
        //            {
        //                // Cek dosage di master
        //                var med = new ItemProductMedic();
        //                if (med.LoadByPrimaryKey(row["ItemID"].ToString()) && med.Dosage > 0 && row["SRConsumeUnit"].Equals(med.SRDosageUnit))
        //                {
        //                    row["QtyInput"] = row["QtyInput"].ToDecimal() * med.Dosage;

        //                }
        //                else
        //                {
        //                    // cek conversion di matrix
        //                    var conversion = new ItemProductConsumeUnitMatrix();
        //                    if (conversion.LoadByPrimaryKey(row["ItemID"].ToString(), row["SRItemUnit"].ToString(),
        //                        row["SRConsumeUnit"].ToString()))
        //                    {
        //                        row["QtyInput"] = row["QtyInput"].ToDecimal() * conversion.ConversionFactor;
        //                    }
        //                }
        //            }

        //            sbItem = new StringBuilder();
        //            sbItem.AppendFormat("{0} {1} {2} {3} {4}<br />",
        //                Convert.ToBoolean(row["IsRFlag"])
        //                    ? "R/"
        //                    : "     ",
        //                itemName, resultQty, itemUnit, row["Notes"]);

        //            if (row["SRConsumeMethodNameOri"] != DBNull.Value && !string.IsNullOrEmpty(row["SRConsumeMethodNameOri"].ToString())
        //                && !row["SRConsumeMethodName"].Equals(row["SRConsumeMethodNameOri"]))
        //            {
        //                sbItem.AppendFormat("<br /><del style='color:gray;'>{0}</del> {1} ", row["SRConsumeMethodNameOri"],
        //                    row["SRConsumeMethodName"]);
        //            }
        //            var destRow = presc.NewRow();
        //            destRow.ItemArray = row.ItemArray.Clone() as object[];
        //            destRow["ItemName"] = sbItem.ToString();
        //            presc.Rows.Add(destRow);
        //        }
        //    }

        //    return presc;
        //}
        #endregion

        #region Import Service Unit Transaction

        [Obsolete("Obat dari Service Unit Transaction adalah bukan obat harian jadi tidak diimport ke UDD ",true)]
        public static void ImportFromTransCharges(string transactionNo, string registrationNo, DateTime receiveDateTime)
        {
            // TODO: ImportFromTransCharges belum bisa dipakai krn ConsumeQty, SRConsumeUnit, SRConsumeMethod, SRMedicationConsume belum bisa diset defaultnya
            var query = new TransChargesItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");

            query.Select
            (
                query,
                query.ChargeQuantity.As("QtyInput"),
                qItem.ItemName
            );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID && qItemMedic.IsMedication == true);
            query.Where(query.TransactionNo == transactionNo);

            var dtbSource = query.LoadDataTable();
            foreach (DataRow row in dtbSource.Rows)
            {
                if (string.IsNullOrEmpty(row["ParentNo"].ToString()) && Convert.ToDecimal(row["QtyInput"]) > 0)
                    InsertMedicationReceiveFromChargeRow(row, registrationNo, receiveDateTime);
            }

        }
        
        private static void InsertMedicationReceiveFromChargeRow(DataRow row, string registrationNo, DateTime receiveDateTime)
        {
            var transNo = row["TransactionNo"].ToString();
            var seqNo = row["SequenceNo"].ToString();

            // Check has register
            var qr = new MedicationReceiveQuery("q");
            qr.Where(qr.RefTransactionNo == transNo, qr.RefSequenceNo == seqNo);
            qr.es.Top = 1;
            var ent = new MedicationReceive();
            if (ent.Load(qr))
                return;

            ent = new MedicationReceive
            {
                RegistrationNo = registrationNo,
                MedicationReceiveNo = NewMedicationReceiveNo(),
                RefTransactionNo = transNo,
                BalanceQty = Convert.ToDecimal(row["QtyInput"]),
                StartDateTime = receiveDateTime,
                ReceiveDateTime = receiveDateTime,
                ItemID = row["ItemID"].ToString(),
                ItemDescription = row["ItemName"].ToString(),
                ReceiveQty = Convert.ToDecimal(row["QtyInput"]),
                ConsumeQty = Convert.ToDecimal(row["QtyInput"]),
                SRConsumeUnit = row["SRConsumeUnit"].ToString(),
                SRConsumeMethod = row["SRConsumeMethod"].ToString(),
                SRMedicationConsume = row["SRMedicationConsume"].ToString(),
                IsVoid = false,
                IsContinue = true
            };

            ent.Save();
        }

        #endregion

        public static string BalanceInZeroQty(string registrationNo, string fromRegistrationNo)
        {
            // Cari item yg akan habis dalam waktu (MedicationWillOutOfBalanceInDay) hari
            var medrec = new MedicationReceiveQuery("mr");
            var cm = new ConsumeMethodQuery("cm");
            medrec.InnerJoin(cm).On(medrec.SRConsumeMethod == cm.SRConsumeMethod);
            medrec.Where(cm.IterationQty > 0, medrec.IsClosed != true, medrec.IsVoid != true);

            if (string.IsNullOrEmpty(fromRegistrationNo))
                medrec.Where(medrec.RegistrationNo == registrationNo);
            else
                medrec.Where(medrec.Or(medrec.RegistrationNo == registrationNo, medrec.RegistrationNo == fromRegistrationNo));

            medrec.Where("<(mr.BalanceRealQty - (mr.ConsumeQty * cm.IterationQty * " + AppParameter.GetParameterValue(AppParameter.ParameterItem.MedicationWillOutOfBalanceInDay) + ")) < 1>");
            medrec.Select(medrec.ItemDescription, medrec.BalanceRealQty, medrec.ConsumeQty, cm.IterationQty, medrec.SRConsumeUnit);

            // For Not UDD Item
            var uddItem = new UddItemQuery("udi");
            uddItem.Select(uddItem.ItemID);
            uddItem.Where(uddItem.RegistrationNo == registrationNo, uddItem.ItemID == medrec.ItemID);

            medrec.Where(medrec.ItemID.NotIn(uddItem));

            var dtb = medrec.LoadDataTable();
            var retval = string.Empty;
            foreach (DataRow row in dtb.Rows)
            {
                var itemDescription = row["ItemDescription"].ToString();
                if (itemDescription.Length > 5 && itemDescription.Substring(itemDescription.Length - 6).Trim() == "<br />")
                {
                    itemDescription = itemDescription.Substring(0, itemDescription.Length - 6);
                }
                retval = string.Concat(retval, "<br />", string.Format(" • {0} (Bal: {1}) {2}x{3} {4}", itemDescription, row["BalanceRealQty"], row["IterationQty"], row["ConsumeQty"], row["SRConsumeUnit"]));
            }

            if (!string.IsNullOrEmpty(retval))
                return retval.Substring(6).Replace(Environment.NewLine, "");
            return string.Empty;
        }

        #region For display
        public static string ConsumeMethod(int medicationReceiveNo)
        {
            var mr = new MedicationReceive();
            if (!mr.LoadByPrimaryKey(medicationReceiveNo)) return string.Empty;

            var cons = new ConsumeMethod();
            cons.LoadByPrimaryKey(mr.SRConsumeMethod);
            return string.Format("{0} {1} {2}", cons.SRConsumeMethodName, mr.ConsumeQty, mr.SRConsumeUnit);
        }

        public static string PrescriptionItemDescription(object opresNo, object oseqNo, object odefaultItemDesc, bool isUseConsumeMethod = true)

        {
            string presNo = opresNo.ToString();
            string seqNo = oseqNo.ToString();
            string defaultItemDesc = odefaultItemDesc.ToString();

            return PrescriptionItemDescription(presNo, seqNo, defaultItemDesc, isUseConsumeMethod, true);
        }
        public static string PrescriptionItemDescription(object opresNo, object oseqNo, object odefaultItemDesc, bool isUseConsumeMethod = true, object osrRoute = null)
        {
            string srRoute = osrRoute == null ? String.Empty : osrRoute.ToString();
            string presNo = opresNo.ToString();
            string seqNo = oseqNo.ToString();
            string defaultItemDesc = odefaultItemDesc.ToString();

            return PrescriptionItemDescription(presNo, seqNo, defaultItemDesc, isUseConsumeMethod, true, srRoute);
        }
        public static string PrescriptionItemDescription(string presNo, string seqNo, string defaultItemDesc = null, bool isUseConsumeMethod = true, bool isUseNote = true, string srRoute = "")
        {
            if (string.IsNullOrWhiteSpace(presNo) || string.IsNullOrWhiteSpace(seqNo))
                return defaultItemDesc == null ? string.Empty : defaultItemDesc;


            if (!string.IsNullOrEmpty(presNo))
            {
                var prescItem = new TransPrescriptionItem();
                if (prescItem.LoadByPrimaryKey(presNo, seqNo))
                {
                    if (prescItem.IsCompound ?? false)
                    {
                        return PrescriptionItemDescriptionCompound(prescItem, isUseConsumeMethod, isUseNote, srRoute);
                    }
                    else
                    {
                        return PrescriptionItemDescription(prescItem, isUseConsumeMethod, isUseNote, srRoute);
                    }
                }
            }

            return defaultItemDesc == null ? string.Empty : defaultItemDesc;
        }

        private static string PrescriptionItemDescriptionCompound(TransPrescriptionItem prescItem, bool isUseConsumeMethod = true, bool isUseNote = true, string srRoute = "")
        {
            // Obat Racikan
            var sbItem = new StringBuilder();

            var dosageUnit = FieldStringText(prescItem.OriSRDosageUnit, prescItem.SRDosageUnit);
            var dosageQty = FieldStringText(prescItem.OriDosageQty, prescItem.DosageQty);

            var emb = new Embalace();
            emb.LoadByPrimaryKey(prescItem.EmbalaceID);
            sbItem.AppendFormat("<span>{0} {1} {2} @ {3} {4} {5}</span>",
                ItemName(prescItem), prescItem.EmbalaceQty, emb.EmbalaceLabel, dosageQty,
                dosageUnit, isUseNote ? prescItem.Notes : String.Empty);

            // Detil racikan
            var coll = new TransPrescriptionItemCollection();
            coll.Query.Where(coll.Query.PrescriptionNo == prescItem.PrescriptionNo, coll.Query.ParentNo == prescItem.SequenceNo);
            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
            coll.LoadAll();

            foreach (var entChild in coll)
            {
                dosageUnit = FieldStringText(entChild.OriSRDosageUnit, entChild.SRDosageUnit);
                dosageQty = FieldStringText(entChild.OriDosageQty, entChild.DosageQty);

                sbItem.AppendFormat("<br/><span style=\"padding-left:10px;\">• {0} @ {1} {2}</span>",
                    ItemName(entChild),
                    dosageQty,
                    dosageUnit);
            }

            if (isUseConsumeMethod)
            {
                // Utk kejelasan info order ori dari dokter
                var cons = new ConsumeMethod();
                cons.LoadByPrimaryKey(prescItem.SRConsumeMethod);
                var cmName = cons.SRConsumeMethodName;
                if (prescItem.OriSRConsumeMethod != null && prescItem.SRConsumeMethod.Equals(prescItem.OriSRConsumeMethod))
                {
                    cons = new ConsumeMethod();
                    cons.LoadByPrimaryKey(prescItem.OriSRConsumeMethod);
                    cmName = string.Format("<del style='color:gray;font-size:smaller;'>{0}</del> {1} ", cons.SRConsumeMethodName, cmName);

                }

                var cmQty = FieldStringText(prescItem.OriConsumeQty, prescItem.ConsumeQty);
                var cmUnit = FieldStringText(prescItem.OriSRConsumeUnit, prescItem.SRConsumeUnit);
                sbItem.AppendFormat("<br/><span style=\"font-style:italic;\">{0} {1} {2}</span>", cmName, cmQty, cmUnit);
            }

            // Route
            if (!string.IsNullOrWhiteSpace(srRoute))
            {
                var route = new AppStandardReferenceItem();
                if (route.LoadByPrimaryKey("ROUTE", srRoute))
                    sbItem.AppendFormat("<br/><span style=\"font-style:italic;\">Route: {0}</span>", route.ItemName);
            }
            return sbItem.ToString();
        }

        private static string ItemName(TransPrescriptionItem prescItem)
        {
            string itemName;
            var item = new Item();
            var itemMedic = new ItemProductMedic();

            item.LoadByPrimaryKey(prescItem.ItemID);
            itemName = item.ItemName;

            if (prescItem.ItemInterventionID != null
                && !string.IsNullOrEmpty(prescItem.ItemInterventionID.ToString())
                && !prescItem.ItemID.Equals(prescItem.ItemInterventionID))
            {
                item = new Item(); // Load baru kalau tidak maka LoadByPrimaryKey berikutnya tidak akan berperngaruh
                item.LoadByPrimaryKey(prescItem.ItemInterventionID);
                itemName = string.Format("<del style='color:gray;font-size:smaller;'>{0}</del> {1} ", itemName, item.ItemName);

                itemMedic.LoadByPrimaryKey(prescItem.ItemInterventionID);
            }
            else
                itemMedic.LoadByPrimaryKey(prescItem.ItemID);


            // Add info
            var lasa = (itemMedic.IsLasa ?? false) ? "&nbsp;<span style='color:white;background-color:blue;'>&nbsp;[LASA]&nbsp;</span>" : string.Empty;
            var aso = (itemMedic.IsAso ?? false) ? "&nbsp;<span style='background-color:yellow;'>&nbsp;[ASO]&nbsp;</span>" : string.Empty;

            return itemName + lasa + aso;
        }

        private static string PrescriptionItemDescription(TransPrescriptionItem prescItem, bool isUseConsumeMethod = true, bool isUseNote = true, string srRoute = "")
        {
            // Obat paten

            var sbItem = new StringBuilder();
            // Sudah tidak valid untuk model per terapi 
            //var resultQty = FieldNumericText(prescItem.OriResultQty, prescItem.ResultQty);
            //var itemUnit = FieldStringText(prescItem.OriSRItemUnit, prescItem.SRItemUnit);
            //sbItem.AppendFormat("{0} {1} {2} {3}",
            //    ItemName(prescItem), resultQty, itemUnit, prescItem.Notes);

            if (isUseNote)
                sbItem.AppendFormat("{0} {1}", ItemName(prescItem), prescItem.Notes);
            else
                sbItem.Append(ItemName(prescItem));

            // Utk kejelasan info order ori dari dokter
            if (isUseConsumeMethod)
            {
                var cons = new ConsumeMethod();
                cons.LoadByPrimaryKey(prescItem.SRConsumeMethod);
                var cmName = cons.SRConsumeMethodName;
                if (prescItem.OriSRConsumeMethod != null && !prescItem.SRConsumeMethod.Equals(prescItem.OriSRConsumeMethod))
                {
                    cons = new ConsumeMethod();
                    cons.LoadByPrimaryKey(prescItem.OriSRConsumeMethod);
                    cmName = string.Format("<del style='color:gray;'>{0}</del> {1} ", cons.SRConsumeMethodName, cmName);
                }

                var cmQty = FieldStringText(prescItem.OriConsumeQty, prescItem.ConsumeQty);
                var cmUnit = FieldStringText(prescItem.OriSRConsumeUnit, prescItem.SRConsumeUnit);
                sbItem.AppendFormat("<br/><span style=\"font-style:italic;\">{0} @{1} {2}</span>", cmName, cmQty, cmUnit);
            }
            // Route
            if (!string.IsNullOrWhiteSpace(srRoute))
            {
                var route = new AppStandardReferenceItem();
                if (route.LoadByPrimaryKey("ROUTE", srRoute))
                    sbItem.AppendFormat("<br/><span style=\"font-style:italic;\">Route: {0}</span>", route.ItemName);
            }
            return sbItem.ToString();
        }

        private static string FieldStringText(string oriValue, string currentValue)
        {
            var interventionFormat = "<del style='color:gray;font-size:smaller;'>{0}</del> {1} ";
            return (oriValue != null && !string.IsNullOrEmpty(oriValue.ToString())
             && !currentValue.Equals(oriValue))
                ? string.Format(interventionFormat, oriValue, currentValue)
                : currentValue;
        }
        private static string FieldNumericText(decimal? oriValue, decimal? currentValue)
        {
            var interventionFormat = "<del style='color:gray;font-size:smaller;'>{0}</del> {1} ";

            return (oriValue > 0 && oriValue != currentValue)
                ? string.Format(interventionFormat, oriValue.RemoveZeroDigits(), currentValue.RemoveZeroDigits())
                : currentValue.RemoveZeroDigits();
        }
        #endregion
    }
}