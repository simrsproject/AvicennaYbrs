using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Collections;
using System.Web;
using System.Web.UI.HtmlControls;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.Reference;


namespace Temiang.Avicenna.Module.Charges.ServiceUnitTransaction
{
    public partial class ItemPickerListTemplate : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.AP_PAYMENT;

            //if (!IsPostBack)
            //{
            //    var supp = new Supplier();
            //    supp.LoadByPrimaryKey(Request.QueryString["supp"]);
            //    Title = "Invoice List : " + supp.SupplierName;
            //}
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransChargesTemplateQuery("a");
            query.Select(query.TemplateNo, query.TemplateName);
            query.Where(query.ToServiceUnitID == Request.QueryString["TUnit"],
                query.ParamedicID == AppSession.UserLogin.ParamedicID,
                query.IsDeleted == false);
            DataTable dtb = query.LoadDataTable();

            grdList.DataSource = dtb;
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            using (var trans = new esTransactionScope())
            {
                var templateNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesTemplateMetadata.ColumnNames.TemplateNo]);
                var entity = new TransChargesTemplate();
                if (entity.LoadByPrimaryKey(templateNo))
                {
                    //var tpit = new TransChargesTemplateCollection();
                    //tpit.Query.Where(tpit.Query.TemplateNo == templateNo);
                    //tpit.LoadAll();
                    //tpit.MarkAllAsDeleted();
                    //tpit.Save();

                    //entity.MarkAsDeleted();
                    entity.IsDeleted = true;
                    entity.Save();

                    trans.Complete();
                }
            }
            grdList.DataSource = null;
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grdlistitem": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string transNo = pars[0].Split(':')[1];
                    PopulateItemGrid(transNo);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
            var components = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]];
            var consumptions = (TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]];

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["reg"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var tariffDate = grr.TariffCalculationMethod == 1
                ? reg.RegistrationDate.Value.Date
                : (new DateTime()).NowAtSqlServer().Date;

            var sMsg = string.Empty;
            var sCitoMsg = string.Empty;

            var tcompColl = new TariffComponentCollection();
            tcompColl.LoadAll();

            foreach (GridDataItem dataItem in grdListItem.MasterTableView.Items)
            {
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtChargeQuantity")).Value ?? 0;
                if (qty == 0) continue;

                var parentCharges = charges.Where(x => x.SequenceNo.Length.Equals(3));
                var seqNo = parentCharges.Any() ? parentCharges.ElementAt(parentCharges.Count() - 1).SequenceNo : "000";

                var entity = FindTransChargesItem(dataItem["ItemID"].Text);

                if (entity == null)
                {
                    entity = charges.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;
                }

                entity.TransactionNo = string.Empty;
                entity.ParentNo = string.Empty;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.ChargeClassID = reg.ChargeClassID;
                entity.ParamedicID = string.Empty;
                entity.ParamedicName = string.Empty;
                entity.TariffDate = tariffDate;

                var tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                             (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                if (tariff == null)
                {
                    var cl = new BusinessObject.Class();
                    cl.LoadByPrimaryKey(entity.ChargeClassID);
                    sMsg += (!string.IsNullOrEmpty(sMsg) ? ", " : "") + string.Format("{0} ({1})", entity.ItemName, cl.ClassName);

                    continue;
                }

                entity.IsAdminCalculation = tariff.IsAdminCalculation;
                entity.IsVariable = tariff.IsAllowVariable;
                entity.IsCito = false;

                if (!(entity.IsCito ?? false))
                    entity.CitoAmount = 0;
                else
                {
                    entity.CitoAmount = tariff != null ? (!(tariff.IsCitoInPercent ?? false) ? tariff.CitoValue : (tariff.CitoValue / 100) * tariff.Price) : 0;
                    entity.IsCitoInPercent = tariff.IsCitoInPercent ?? false;
                    entity.BasicCitoAmount = tariff.CitoValue;
                    entity.SRCitoPercentage = string.Empty;
                }
                entity.ChargeQuantity = Convert.ToDecimal(qty);
                entity.StockQuantity = 0;

                if (Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitLaboratoryID ||
                    AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(Request.QueryString["unit"]) ||
                    Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitRadiologyID ||
                    Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitRadiologyID2 ||
                    AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(Request.QueryString["unit"]) ||
                    AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(Request.QueryString["unit"]))
                {
                    entity.SRItemUnit = "X";
                }
                else
                {
                    var its = new ItemService();
                    entity.SRItemUnit = its.LoadByPrimaryKey(entity.ItemID) ? its.SRItemUnit : "X";
                }

                entity.Price = tariff.Price;
                entity.DiscountAmount = 0;
                entity.Total = Helper.Rounding((entity.ChargeQuantity ?? 0) * (((entity.Price ?? 0) - (entity.DiscountAmount ?? 0)) + (entity.CitoAmount ?? 0)), AppEnum.RoundingType.Transaction);
                entity.RoundingAmount = Helper.RoundingDiff;
                entity.SRDiscountReason = string.Empty;
                entity.IsAssetUtilization = false;
                entity.AssetID = string.Empty;
                entity.IsBillProceed = false;
                entity.IsOrderRealization = false;
                entity.IsPackage = false;
                entity.IsApprove = false;
                entity.IsVoid = false;
                entity.Notes = string.Empty;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.IsItemRoom = false;
                entity.SRCitoPercentage = string.Empty;
                entity.IsItemTypeService = true;

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    entity.IsCasemixApproved = Helper.IsCasemixApproved(entity.ItemID, entity.ChargeQuantity ?? 0, reg.RegistrationNo, entity.TransactionNo, reg.GuarantorID, false);
                else
                    entity.IsCasemixApproved = true;

                //if (GuarantorBPJS.Contains(reg.GuarantorID))
                //{
                //    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                //    {
                //        //if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                //        //{
                //        //    var item = new Item();
                //        //    item.LoadByPrimaryKey(entity.ItemID);
                //        //    if (item.SRItemType != BusinessObject.Reference.ItemType.Radiology)
                //        //    {
                //        //        entity.IsCasemixApproved = true;
                //        //        entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //        //        entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //        //    }
                //        //}
                //        //else
                //        //{
                //        entity.IsCasemixApproved = false;
                //        //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //        //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //        //}

                //        var rpc = new RegistrationPathwayCollection();
                //        rpc.Query.Where(rpc.Query.RegistrationNo == reg.RegistrationNo);
                //        if (rpc.Query.Load())
                //        {
                //            foreach (var rp in rpc)
                //            {
                //                if (string.IsNullOrEmpty(rp.PathwayID)) continue;
                //                var rpic = new RegistrationPathwayItemCollection();
                //                rpic.Query.Where(rpic.Query.PathwayID == rp.PathwayID);
                //                rpic.Query.OrderBy(rpic.Query.PathwayItemSeqNo.Ascending);
                //                if (!rpic.Query.Load()) continue;
                //                foreach (var rpi in rpic)
                //                {
                //                    var pi = new PathwayItem();
                //                    if (!pi.LoadByPrimaryKey(rpi.PathwayID, rpi.PathwayItemSeqNo ?? 0)) continue;
                //                    if (entity.ItemID == pi.ItemID)
                //                    {
                //                        entity.IsCasemixApproved = true;
                //                        entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //                        entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        entity.IsCasemixApproved = true;
                //        //entity.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                //        //entity.CasemixApprovedDateTime = (new DateTime()).NowAtSqlServer();
                //    }
                //}
                //else
                //{
                //    entity.IsCasemixApproved = true;
                //}

                if (Request.QueryString["type"] == "ds")
                {
                    var comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, entity.ChargeClassID, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, entity.ChargeClassID, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, entity.ItemID);

                    foreach (var comp in comps)
                    {
                        var component = components.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, comp.TariffComponentID);
                        if (component == null)
                        {
                            component = components.AddNew();
                            component.TransactionNo = entity.TransactionNo;
                            component.SequenceNo = entity.SequenceNo;
                            component.TariffComponentID = comp.TariffComponentID;
                            component.Price = comp.Price;
                            component.DiscountAmount = 0;
                            component.ParamedicID = string.Empty;
                            component.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            component.LastUpdateDateTime = DateTime.Now;
                            component.IsPackage = false;

                            if (entity.BasicCitoAmount > 0)
                            {
                                if (tariff.IsCitoInPercent ?? false)
                                    component.CitoAmount = comp.Price * entity.BasicCitoAmount / 100;
                                else
                                    component.CitoAmount = (comp.Price / entity.Price) * entity.BasicCitoAmount;
                            }
                            else component.CitoAmount = 0;
                            component.FeeDiscountPercentage = 0;

                            var tcomp = tcompColl.Where(t => t.TariffComponentID == component.TariffComponentID).First();
                            if (tcomp != null)
                            {
                                if (tcomp.IsTariffParamedic ?? false)
                                {
                                    if (Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitLaboratoryID ||
                                        AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(Request.QueryString["unit"]))
                                    {
                                        if (!string.IsNullOrEmpty(AppSession.Parameter.ParamedicIdLabDefault))
                                        {
                                            component.ParamedicID = AppSession.Parameter.ParamedicIdLabDefault;
                                        }
                                    }
                                    else if (Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitRadiologyID ||
                                        Request.QueryString["unit"] == AppSession.Parameter.ServiceUnitRadiologyID2 ||
                                        AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(Request.QueryString["unit"]) ||
                                        AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(Request.QueryString["unit"]))
                                    {
                                        if (!string.IsNullOrEmpty(AppSession.Parameter.ParamedicIdRadDefault))
                                        {
                                            component.ParamedicID = AppSession.Parameter.ParamedicIdRadDefault;
                                        }
                                    }
                                    else
                                    {
                                        component.ParamedicID = reg.ParamedicID;
                                    }

                                    if (tcomp.IsPrintParamedicInSlip ?? false)
                                    {
                                        var par = new Paramedic();
                                        if (par.LoadByPrimaryKey(component.ParamedicID))
                                        {
                                            if (par.IsPrintInSlip ?? true)
                                            {
                                                if (string.IsNullOrEmpty(entity.ParamedicCollectionName))
                                                    entity.ParamedicCollectionName = par.ParamedicName;
                                                else if (!entity.ParamedicCollectionName.Contains(par.ParamedicName))
                                                    entity.ParamedicCollectionName = entity.ParamedicCollectionName + "; " + par.ParamedicName;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var consColl = new ItemConsumptionCollection();
                consColl.Query.Where(consColl.Query.ItemID == entity.ItemID);
                consColl.LoadAll();

                foreach (var cons in consColl)
                {
                    var ent = consumptions.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, cons.DetailItemID) ?? consumptions.AddNew();
                    ent.TransactionNo = entity.TransactionNo;
                    ent.SequenceNo = entity.SequenceNo;
                    ent.DetailItemID = cons.DetailItemID;

                    var i = new Item();
                    i.LoadByPrimaryKey(ent.DetailItemID);
                    ent.ItemName = i.ItemName;

                    ent.Qty = entity.ChargeQuantity * cons.Qty;
                    ent.QtyRealization = ent.Qty;
                    ent.SRItemUnit = entity.SRItemUnit;

                    var tariffcons = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                  Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                 (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                  Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                    ent.Price = tariffcons.Price ?? 0;

                    switch (i.SRItemType)
                    {
                        case ItemType.Medical:
                            var im = new ItemProductMedic();
                            im.LoadByPrimaryKey(i.ItemID);
                            ent.AveragePrice = im.CostPrice;
                            ent.FifoPrice = im.PriceInBaseUnit;
                            break;
                        case ItemType.NonMedical:
                            var inm = new ItemProductNonMedic();
                            inm.LoadByPrimaryKey(i.ItemID);
                            ent.AveragePrice = inm.CostPrice;
                            ent.FifoPrice = inm.PriceInBaseUnit;
                            break;
                        case ItemType.Kitchen:
                            var ik = new ItemKitchen();
                            ik.LoadByPrimaryKey(i.ItemID);
                            ent.AveragePrice = ik.CostPrice;
                            ent.FifoPrice = ik.PriceInBaseUnit;
                            break;
                        default:
                            ent.AveragePrice = ent.Price;
                            ent.FifoPrice = ent.Price;
                            break;
                    }

                    ent.IsPackage = false;

                    ent.LastUpdateDateTime = DateTime.Now;
                    ent.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);
                if (item.SRItemType == Temiang.Avicenna.BusinessObject.Reference.ItemType.Laboratory)
                {
                    var labs = new ItemLaboratoryProfileCollection();
                    labs.Query.Where(labs.Query.ParentItemID == entity.ItemID);
                    labs.Query.OrderBy(labs.Query.DisplaySequence.Ascending);
                    if (labs.Query.Load())
                    {
                        foreach (var lab in labs)
                        {
                            var entityLab = FindTransChargesItem(lab.DetailItemID);
                            if (entityLab == null)
                                entityLab = charges.AddNew();

                            //entityLab = charges.AddNew();

                            entityLab.TransactionNo = string.Empty;

                            var sequenceNo = (charges.Where(c => c.ParentNo == entity.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();

                            entityLab.SequenceNo = entity.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                            entityLab.ParentNo = entity.SequenceNo;
                            entityLab.ReferenceNo = string.Empty;
                            entityLab.ReferenceSequenceNo = string.Empty;
                            entityLab.ItemID = lab.DetailItemID;
                            entityLab.ItemName = string.Empty;
                            entity.ChargeClassID = reg.ChargeClassID;
                            entityLab.ParamedicID = string.Empty;
                            entityLab.ParamedicName = string.Empty;
                            entityLab.IsAdminCalculation = false;
                            entityLab.IsVariable = false;
                            entityLab.IsCito = false;
                            entityLab.ChargeQuantity = 0;
                            entityLab.StockQuantity = 0;
                            entityLab.SRItemUnit = "X";
                            entityLab.CostPrice = 0;
                            entityLab.Price = 0;
                            entityLab.CitoAmount = 0;
                            entityLab.IsCitoInPercent = false;
                            entityLab.BasicCitoAmount = 0;
                            entityLab.RoundingAmount = 0;
                            entityLab.SRDiscountReason = string.Empty;
                            entityLab.IsAssetUtilization = false;
                            entityLab.AssetID = string.Empty;
                            entityLab.IsBillProceed = false;
                            entityLab.IsOrderRealization = false;
                            entityLab.IsPackage = false;
                            entityLab.IsVoid = false;
                            entityLab.Notes = string.Empty;
                            entityLab.IsItemTypeService = false;
                            entityLab.SRCenterID = string.Empty;
                            entityLab.IsApprove = false;
                            entityLab.IsItemRoom = false;
                            entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                            entityLab.CreatedDateTime = DateTime.Now;
                            entityLab.SRCitoPercentage = string.Empty;
                            entityLab.IsItemTypeService = true;
                            entityLab.TariffDate = tariffDate;

                            //cek anak level 2
                            var labs2 = new ItemLaboratoryProfileCollection();
                            labs2.Query.Where(labs2.Query.ParentItemID == entityLab.ItemID);
                            labs2.Query.OrderBy(labs2.Query.DisplaySequence.Ascending);
                            if (labs2.Query.Load())
                            {
                                foreach (var lab2 in labs2)
                                {
                                    var entityLab2 = charges.AddNew();
                                    entityLab2.TransactionNo = string.Empty;

                                    sequenceNo = (charges.Where(c => c.ParentNo == entityLab.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                    entityLab2.SequenceNo = entityLab.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                    entityLab2.ParentNo = entityLab.SequenceNo;
                                    entityLab2.ReferenceNo = string.Empty;
                                    entityLab2.ReferenceSequenceNo = string.Empty;
                                    entityLab2.ItemID = lab2.DetailItemID;
                                    entityLab2.ItemName = string.Empty;
                                    entityLab2.ChargeClassID = string.Empty;
                                    entityLab2.ParamedicID = string.Empty;
                                    entityLab2.ParamedicName = string.Empty;
                                    entityLab2.IsAdminCalculation = false;
                                    entityLab2.IsVariable = false;
                                    entityLab2.IsCito = false;
                                    entityLab2.ChargeQuantity = 0;
                                    entityLab2.StockQuantity = 0;
                                    entityLab2.SRItemUnit = "X";
                                    entityLab2.CostPrice = 0;
                                    entityLab2.Price = 0;
                                    entityLab2.CitoAmount = 0;
                                    entityLab2.IsCitoInPercent = false;
                                    entityLab2.BasicCitoAmount = 0;
                                    entityLab2.RoundingAmount = 0;
                                    entityLab2.SRDiscountReason = string.Empty;
                                    entityLab2.IsAssetUtilization = false;
                                    entityLab2.AssetID = string.Empty;
                                    entityLab2.IsBillProceed = false;
                                    entityLab2.IsOrderRealization = false;
                                    entityLab2.IsPaymentConfirmed = false;
                                    entityLab2.IsPackage = false;
                                    entityLab2.IsVoid = false;
                                    entityLab2.Notes = string.Empty;
                                    entityLab2.IsItemTypeService = false;
                                    entityLab2.SRCenterID = string.Empty;
                                    entityLab2.IsApprove = false;
                                    entityLab2.IsItemRoom = false;
                                    entityLab2.SRCitoPercentage = string.Empty;
                                    entityLab2.IsItemTypeService = true;
                                    entityLab2.TariffDate = tariffDate;
                                    entityLab2.CreatedByUserID = AppSession.UserLogin.UserID;
                                    entityLab2.CreatedDateTime = (new DateTime()).NowAtSqlServer();

                                    //cek anak level 3
                                    var labs3 = new ItemLaboratoryProfileCollection();
                                    labs3.Query.Where(labs3.Query.ParentItemID == entityLab2.ItemID);
                                    labs3.Query.OrderBy(labs3.Query.DisplaySequence.Ascending);
                                    if (labs3.Query.Load())
                                    {
                                        foreach (var lab3 in labs3)
                                        {
                                            var entityLab3 = charges.AddNew();
                                            entityLab3.TransactionNo = string.Empty;

                                            sequenceNo = (charges.Where(c => c.ParentNo == entityLab2.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                                            entityLab3.SequenceNo = entityLab2.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                                            entityLab3.ParentNo = entityLab2.SequenceNo;
                                            entityLab3.ReferenceNo = string.Empty;
                                            entityLab3.ReferenceSequenceNo = string.Empty;
                                            entityLab3.ItemID = lab3.DetailItemID;
                                            entityLab3.ItemName = string.Empty;
                                            entityLab3.ChargeClassID = string.Empty;
                                            entityLab3.ParamedicID = string.Empty;
                                            entityLab3.ParamedicName = string.Empty;
                                            entityLab3.IsAdminCalculation = false;
                                            entityLab3.IsVariable = false;
                                            entityLab3.IsCito = false;
                                            entityLab3.ChargeQuantity = 0;
                                            entityLab3.StockQuantity = 0;
                                            entityLab3.SRItemUnit = "X";
                                            entityLab3.CostPrice = 0;
                                            entityLab3.Price = 0;
                                            entityLab3.CitoAmount = 0;
                                            entityLab3.IsCitoInPercent = false;
                                            entityLab3.BasicCitoAmount = 0;
                                            entityLab3.RoundingAmount = 0;
                                            entityLab3.SRDiscountReason = string.Empty;
                                            entityLab3.IsAssetUtilization = false;
                                            entityLab3.AssetID = string.Empty;
                                            entityLab3.IsBillProceed = false;
                                            entityLab3.IsOrderRealization = false;
                                            entityLab3.IsPaymentConfirmed = false;
                                            entityLab3.IsPackage = false;
                                            entityLab3.IsVoid = false;
                                            entityLab3.Notes = string.Empty;
                                            entityLab3.IsItemTypeService = false;
                                            entityLab3.SRCenterID = string.Empty;
                                            entityLab3.IsApprove = false;
                                            entityLab3.IsItemRoom = false;
                                            entityLab3.SRCitoPercentage = string.Empty;
                                            entityLab3.IsItemTypeService = true;
                                            entityLab3.TariffDate = tariffDate;
                                            entityLab3.CreatedByUserID = AppSession.UserLogin.UserID;
                                            entityLab3.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var group = new ItemGroup();
                group.LoadByPrimaryKey(item.ItemGroupID);
                entity.ItemGroupName = group.ItemGroupName;
            }

            if (!string.IsNullOrEmpty(sMsg))
            {
                ShowInformationHeader(
                    string.Format("Item of {0} {1} no tariff, please contact administrator!",
                    sMsg, (sMsg.IndexOf(',') > 0) ? "have" : "has"));

                charges.MarkAllAsDeleted();
                components.MarkAllAsDeleted();
                consumptions.MarkAllAsDeleted();

                return false;
            }

            if (!string.IsNullOrEmpty(sCitoMsg))
            {
                ShowInformationHeader(sCitoMsg);

                charges.MarkAllAsDeleted();
                components.MarkAllAsDeleted();
                consumptions.MarkAllAsDeleted();

                return false;
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }

        private TransChargesItem FindTransChargesItem(string itemID)
        {
            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
            return charges.FirstOrDefault(detail => detail.ItemID == itemID);
        }

        private void PopulateItemGrid(string templateNo)
        {
            var query = new TransChargesItemTemplateQuery("a");
            var itm = new ItemQuery("b");
            query.InnerJoin(itm).On(itm.ItemID == query.ItemID);

            query.Select(
                query.TemplateNo,
                query.SequenceNo,
                query.ItemID,
                itm.ItemName,
                query.ChargeQuantity
                );
            query.Where(query.TemplateNo == templateNo);
            query.OrderBy(query.SequenceNo.Ascending);

            grdListItem.DataSource = query.LoadDataTable();
            grdListItem.DataBind();
        }

        private static string[] GuarantorBPJS
        {
            get
            {
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                else return new string[] { string.Empty };
            }
        }
    }
}