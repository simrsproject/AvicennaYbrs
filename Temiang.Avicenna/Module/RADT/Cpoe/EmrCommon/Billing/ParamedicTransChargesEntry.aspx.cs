using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ParamedicTransChargesEntry : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                var paramedicTransChargesItem = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicFirstTransChargesItemIds);
                var itemIds = paramedicTransChargesItem.Split(';');
                
                foreach (var itemId in itemIds)
                {
                    var item = new Item();
                    if (item.LoadByPrimaryKey(itemId))
                    {
                        optItemID.Items.Add(new ButtonListItem(item.ItemName, item.ItemID));
                    }
                }

            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (string.IsNullOrWhiteSpace(optItemID.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Please select transaction charges item first";
                return;
            }


            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var chargesHD = new TransCharges();
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var transChargesItemsDT = new TransChargesItemCollection();
            var transChargesItemsDTComp = new TransChargesItemCompCollection();
            var transChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
            var costCalculations = new CostCalculationCollection();
            var registrationItemRules = RegistrationItemRules(RegistrationNo);

            // ServiceUnitAutoBill dummy
            var billColl = new ServiceUnitAutoBillItemCollection();
            var item = new Item();
            var itemID = optItemID.SelectedValue;
            if (item.LoadByPrimaryKey(itemID))
            {
                var abItem = new ServiceUnitAutoBillItem
                {
                    ItemID = itemID,
                    ServiceUnitID = "",
                    Quantity = 1,
                    SRItemUnit = "-",
                    IsAutoPayment = false,
                    IsActive = true,
                    IsGenerateOnNewRegistration = true,
                    IsGenerateOnReferral = true,
                    IsGenerateOnRegistration = true
                };
                billColl.Add(abItem);
            }

            Temiang.Avicenna.Module.RADT.RegistrationDetail.SetTransCharges(reg, chargesHD, grr,
    billColl,
    transChargesItemsDT,
    transChargesItemsDTComp,
    transChargesItemsDTConsumption,
    registrationItemRules,
    costCalculations,
    AppSession.UserLogin.ParamedicID);


            using (var trans = new esTransactionScope())
            {
                // Save TransCharges dkk
                chargesHD.Save();
                transChargesItemsDT.Save();
                transChargesItemsDTComp.Save();
                transChargesItemsDTConsumption.Save();

                //Save hist
                var ptc = new ParamedicTransCharges();
                ptc.RegistrationNo = RegistrationNo;
                ptc.ParamedicID = AppSession.UserLogin.ParamedicID;
                ptc.TransactionNo = chargesHD.TransactionNo;
                ptc.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private RegistrationItemRuleCollection RegistrationItemRules(string registrationNo)
        {
            var coll = new RegistrationItemRuleCollection();
            var query = new RegistrationItemRuleQuery("a");
            var iq = new ItemQuery("b");
            var qSr = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                    query,
                    iq.ItemName.As("refToItem_ItemName"),
                    qSr.ItemName.As("refToSRItem_ItemName")
                );

            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(qSr).On
                (
                    query.SRGuarantorRuleType == qSr.ItemID &
                    qSr.StandardReferenceID == "GuarantorRuleType"
                );

            query.Where(query.RegistrationNo == registrationNo);

            query.OrderBy(query.ItemID, esOrderByDirection.Ascending);

            coll.Load(query);

            return coll;
        }
    }
}