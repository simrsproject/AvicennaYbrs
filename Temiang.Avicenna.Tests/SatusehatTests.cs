using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temiang.Avicenna.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.WebService.Tests
{
    [TestClass()]
    public class SatusehatTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Dal.Interfaces.esProviderFactory.Factory = new Dal.Loader.esDataProviderFactory();
        }

        //[TestMethod()]
        //public void SatusehatServiceRequestPostAndLogToLisTest()
        //{
        //    var txNo = "JO230109-00251";
        //    var charges = new TransCharges();
        //    charges.LoadByPrimaryKey(txNo);

        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(charges.RegistrationNo);

        //    SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo);
        //}

        //private void SatusehatServiceRequestPostAndLogToLis(Registration reg, string transactionNo)
        //{
        //    var tc = new TransCharges();
        //    if (!tc.LoadByPrimaryKey(transactionNo)) return;

        //    // Stusehat post encounter & ServiceRequest
        //    var util = new Temiang.Avicenna.WebService.Satusehat();
        //    util.OrderLab(transactionNo);

        //    // Update Wynakom
        //    var satuSehatLog = new SatuSehatKunjungan();
        //    if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo)) return;

        //    // Load history Service Request
        //    var ssServiceReqs = new SatuSehatResultCollection();
        //    ssServiceReqs.Query.Where(ssServiceReqs.Query.EncounterID == satuSehatLog.EncounterID, ssServiceReqs.Query.ResourceType == "ServiceRequest", ssServiceReqs.Query.Category == transactionNo);
        //    ssServiceReqs.Query.Load();

        //    var ssItems = new Temiang.Avicenna.BusinessObject.Interop.Wynakom.SatusehatOrderedItemsCollection();

        //    var satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);
        //    var patSs = new PatientBridging();
        //    patSs.LoadByPrimaryKey(reg.PatientID, satuSehatBridgingType);

        //    var parSs = new ParamedicBridging();
        //    parSs.Query.Where(parSs.Query.ParamedicID == reg.ParamedicID, parSs.Query.SRBridgingType == satuSehatBridgingType);
        //    parSs.Query.es.Top = 1;
        //    parSs.Query.Load();


        //    var isExist = false;
        //    foreach (var ssResult in ssServiceReqs)
        //    {
        //        var checkSsOrder = new Temiang.Avicenna.BusinessObject.Interop.Wynakom.SatusehatOrderedItems();
        //        checkSsOrder.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
        //        if (checkSsOrder.LoadByPrimaryKey(transactionNo, ssResult.Code)) continue;

        //        var tci = new TransChargesItem();
        //        if (!tci.LoadByPrimaryKey(transactionNo, ssResult.Code)) continue;

        //        if (tci.IsOrderRealization == null || tci.IsOrderRealization == false) continue;

        //        var ssItem = ssItems.AddNew();
        //        ssItem.OrderNumber = transactionNo;
        //        ssItem.OrderSequenceNo = ssResult.Code;
        //        ssItem.SSEncounterID = satuSehatLog.EncounterID.ToString();

        //        ssItem.SSPatientID = patSs.BridgingID;
        //        ssItem.SSPatientName = patSs.BridgingName;

        //        ssItem.SSRequesterPractionerID = parSs.BridgingID;
        //        ssItem.SSRequesterPractionerName = parSs.BridgingName;



        //        var item = new Item();
        //        item.LoadByPrimaryKey(tci.ItemID);
        //        ssItem.OrderItemID = tci.ItemID;
        //        ssItem.OrderItemName = item.ItemName;

        //        var itemBg = new ItemBridging();
        //        itemBg.Query.Where(itemBg.Query.ItemID == tci.ItemID, itemBg.Query.SRBridgingType == satuSehatBridgingType);
        //        itemBg.Query.es.Top = 1;
        //        itemBg.Query.Load();

        //        ssItem.SSLoincID = itemBg.BridgingID;
        //        ssItem.SSLoincName = itemBg.BridgingName;

        //        ssItem.SSServiceRequestID = ssResult.ResultID.ToString();

        //        //Specimen
        //        // Load history Service Request
        //        var ssSpecimen = new SatuSehatResult();
        //        ssSpecimen.Query.Where(ssSpecimen.Query.EncounterID == satuSehatLog.EncounterID, ssSpecimen.Query.ResourceType == "Specimen", ssSpecimen.Query.Category == transactionNo, ssSpecimen.Query.Code == tci.SequenceNo);
        //        ssSpecimen.Query.es.Top = 1;
        //        if (ssSpecimen.Query.Load())
        //        {
        //            ssItem.SSServiceRequestID = ssSpecimen.ResultID.ToString();
        //        }

        //        isExist = true;
        //    }

        //    if (isExist)
        //    {
        //        ssItems.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
        //        ssItems.Save();
        //    }
        //}


        [TestMethod()]
        public void SatusehatServiceRequestPostAndLogToLisTest()
        {
            var txNo = "JO250507-00359";
            var charges = new TransCharges();
            charges.LoadByPrimaryKey(txNo);

            var reg = new Registration();
            reg.LoadByPrimaryKey(charges.RegistrationNo);

            SatusehatServiceRequestPostAndLogToLis_RSI(reg, charges.TransactionNo, AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME);
        }

        #region RSI

        [TestMethod()]
        public void SatusehatServiceRequestPostAndLogToLisReprocessTest_RSI()
        {
            var orderSs = new BusinessObject.Interop.Wynakom.SatusehatOrderedItemsQuery("a");
            orderSs.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            orderSs.es.Distinct = true;
            orderSs.Select(orderSs.OrderNumber);
            orderSs.Where(orderSs.OrderNumber > "JO250420-00000", orderSs.OrderNumber < "JO250507-00000"); // Filter reproses karena lama
            var dtb = orderSs.LoadDataTable();
            var prevtxNo = "";
            foreach (DataRow row in dtb.Rows)
            {
                var txNo = row[0].ToString();
                if (txNo.Contains("^"))
                {
                    txNo = txNo.Split('^')[0];
                }

                if (txNo.Equals(prevtxNo)) continue;
                prevtxNo = txNo;

                var charges = new TransCharges();
                charges.LoadByPrimaryKey(txNo);

                var reg = new Registration();
                reg.LoadByPrimaryKey(charges.RegistrationNo);

                SatusehatServiceRequestPostAndLogToLis_RSI(reg, charges.TransactionNo, AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME);
                Console.WriteLine(txNo);

            }

        }
        private void SatusehatServiceRequestPostAndLogToLis_RSI(Registration reg, string transactionNo, string lisConnectionName)
        {
            var tc = new TransCharges();
            if (!tc.LoadByPrimaryKey(transactionNo)) return;

            // Stusehat post encounter & ServiceRequest
            var util = new Temiang.Avicenna.WebService.Satusehat();
            util.OrderLab(transactionNo);

            // Update Wynakom
            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo)) return;

            if (!satuSehatLog.EncounterID.HasValue) return;

            // Load history Service Request
            var ssServiceReqs = new SatuSehatResultCollection();
            ssServiceReqs.Query.Where(ssServiceReqs.Query.EncounterID == satuSehatLog.EncounterID, ssServiceReqs.Query.ResourceType == "ServiceRequest", ssServiceReqs.Query.Category == transactionNo);
            ssServiceReqs.Query.Load();

            //var ssItems = new Temiang.Avicenna.BusinessObject.Interop.Wynakom.SatusehatOrderedItemsCollection();

            var satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);
            var patSs = new PatientBridging();
            patSs.LoadByPrimaryKey(reg.PatientID, satuSehatBridgingType);

            var parSs = new ParamedicBridging();
            parSs.Query.Where(parSs.Query.ParamedicID == reg.ParamedicID, parSs.Query.SRBridgingType == satuSehatBridgingType);
            parSs.Query.es.Top = 1;
            parSs.Query.Load();

            var itemIds = string.Empty;
            var itemNames = string.Empty;
            var loinscIds = string.Empty;
            var loinscNames = string.Empty;
            var servReqs = string.Empty;
            var specimens = string.Empty;
            var itemCount = 0;
            foreach (var ssResult in ssServiceReqs)
            {
                var tci = new TransChargesItem();
                if (!tci.LoadByPrimaryKey(transactionNo, ssResult.Code)) continue;

                if (tci.IsOrderRealization == null || tci.IsOrderRealization == false) continue;

                itemCount++;
                var item = new Item();
                item.LoadByPrimaryKey(tci.ItemID);
                itemIds = string.Concat(itemIds, tci.ItemID, "~");
                itemNames = string.Concat(itemNames, item.ItemName, "~");

                var itemBg = new ItemBridging();
                itemBg.Query.Where(itemBg.Query.ItemID == tci.ItemID, itemBg.Query.SRBridgingType == satuSehatBridgingType);
                itemBg.Query.es.Top = 1;
                itemBg.Query.Load();

                loinscIds = string.Concat(loinscIds, itemBg.BridgingID, "~");
                loinscNames = string.Concat(loinscNames, itemBg.BridgingName, "~");
                servReqs = string.Concat(servReqs, ssResult.ResultID.ToString(), "~");

                // Specimen
                var ssSpecimen = new SatuSehatResult();
                ssSpecimen.Query.Where(ssSpecimen.Query.EncounterID == satuSehatLog.EncounterID, ssSpecimen.Query.ResourceType == "Specimen", ssSpecimen.Query.Category == transactionNo, ssSpecimen.Query.Code == tci.SequenceNo);
                ssSpecimen.Query.es.Top = 1;
                if (ssSpecimen.Query.Load())
                {
                    specimens = string.Concat(specimens, ssSpecimen.ResultID.ToString(), "~");
                }
            }

            if (!string.IsNullOrWhiteSpace(itemIds))
            {
                var ssItem = new BusinessObject.Interop.Wynakom.SatusehatOrderedItems();
                ssItem.es.Connection.Name = lisConnectionName;

                var orderNumber = string.Format("{0}^{1:000}", transactionNo, itemCount);
                if (!ssItem.LoadByPrimaryKey(orderNumber, ""))
                {
                    ssItem = new BusinessObject.Interop.Wynakom.SatusehatOrderedItems();
                    ssItem.es.Connection.Name = lisConnectionName;
                }

                ssItem.OrderNumber = orderNumber;
                ssItem.OrderSequenceNo = string.Empty;
                ssItem.SSEncounterID = satuSehatLog.EncounterID.ToString();

                ssItem.SSPatientID = patSs.BridgingID;
                ssItem.SSPatientName = patSs.BridgingName;

                ssItem.SSRequesterPractionerID = parSs.BridgingID;
                ssItem.SSRequesterPractionerName = parSs.BridgingName;

                ssItem.OrderItemID = itemIds.Remove(itemIds.Length - 1);
                ssItem.OrderItemName = itemNames.Remove(itemNames.Length - 1);

                if (!string.IsNullOrEmpty(loinscIds))
                    ssItem.SSLoincID = loinscIds.Remove(loinscIds.Length - 1);

                if (!string.IsNullOrEmpty(loinscNames))
                    ssItem.SSLoincName = loinscNames.Remove(loinscNames.Length - 1);

                if (!string.IsNullOrEmpty(servReqs))
                    ssItem.SSServiceRequestID = servReqs.Remove(servReqs.Length - 1);

                if (!string.IsNullOrEmpty(specimens))
                    ssItem.SSSpecimenID = specimens.Remove(specimens.Length - 1);

                ssItem.Save();
            }
        }
        #endregion

    }
}