using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitItemServiceComp : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnit;

            if (!IsPostBack)
            {
                txtServiceUnitID.Text = Request.QueryString["UnitID"];
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtItemID.Text = Request.QueryString["itemID"];
                var item = new Item();
                item.LoadByPrimaryKey(txtItemID.Text);
                lblItemName.Text = item.ItemName;
            }
        }

        private ServiceUnitItemServiceCompMappingCollection ServiceUnitItemServiceCompMappings
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["ServiceUnitItemServiceCompMappingCollection"];
                    if (obj != null)
                        return ((ServiceUnitItemServiceCompMappingCollection)(obj));
                }
                var coll = new ServiceUnitItemServiceCompMappingCollection();

                var query = new ServiceUnitItemServiceCompMappingQuery("a");
                var c = new TariffComponentQuery("b");

                var rev = new ChartOfAccountsQuery("c");
                var srev = new SubLedgersQuery("d");

                var disc = new ChartOfAccountsQuery("e");
                var sdisc = new SubLedgersQuery("f");

                var cost = new ChartOfAccountsQuery("g");
                var scost = new SubLedgersQuery("h");

                var rtype = new AppStandardReferenceItemQuery("i");

                var acctGroup = new AppStandardReferenceItemQuery("j");

                query.Select
                    (
                        query,

                        c.TariffComponentName.As("refToTariffComponent_TariffComponentName"),

                        rev.ChartOfAccountName.As("refToChartOfAccounts_COARevenueName"),
                        //srev.SubLedgerName.As("refToSubledgers_SubledgerRevenueName"),
                        @"<d.SubLedgerName + ' - ' + d.Description AS 'refToSubledgers_SubledgerRevenueName'>",

                        disc.ChartOfAccountName.As("refToChartOfAccounts_COADiscountName"),
                        //sdisc.SubLedgerName.As("refToSubledgers_SubledgerDiscountName"),
                        @"<f.SubLedgerName + ' - ' + f.Description AS 'refToSubledgers_SubledgerDiscountName'>",

                        cost.ChartOfAccountName.As("refToChartOfAccounts_COACostName"),
                        //scost.SubLedgerName.As("refToSubledgers_SubledgerCostName"),
                        @"<h.SubLedgerName + ' - ' + h.Description AS 'refToSubledgers_SubledgerCostName'>",

                        rtype.ItemName.As("refToAppStandardReferenceItem_RegistrationType"),

                        acctGroup.ItemName.As("refToAppStandardReferenceItem_GuarantorIncomeGroup")
                    );
                query.InnerJoin(c).On(query.TariffComponentID == c.TariffComponentID);

                query.LeftJoin(rev).On(query.ChartOfAccountIdIncome == rev.ChartOfAccountId);
                query.LeftJoin(srev).On(query.SubledgerIdIncome == srev.SubLedgerId);

                query.LeftJoin(disc).On(query.ChartOfAccountIdDiscount == disc.ChartOfAccountId);
                query.LeftJoin(sdisc).On(query.SubledgerIdDiscount == sdisc.SubLedgerId);

                query.LeftJoin(cost).On(query.ChartOfAccountIdCost == cost.ChartOfAccountId);
                query.LeftJoin(scost).On(query.SubledgerIdCost == scost.SubLedgerId);

                query.InnerJoin(rtype).On(query.SRRegistrationType == rtype.ItemID &&
                                          rtype.StandardReferenceID == AppEnum.StandardReference.RegistrationType);

                query.LeftJoin(acctGroup).On(query.SRGuarantorIncomeGroup == acctGroup.ItemID && acctGroup.StandardReferenceID == AppEnum.StandardReference.GuarantorIncomeGroup);

                query.Where(query.ServiceUnitID == txtServiceUnitID.Text, query.ItemID == txtItemID.Text);

                coll.Load(query);

                Session["ServiceUnitItemServiceCompMappingCollection"] = coll;
                return coll;
            }
            set
            {
                Session["ServiceUnitItemServiceCompMappingCollection"] = value;
            }
        }

        protected void grdServiceUnitItemServiceComp_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitItemServiceComp.DataSource = ServiceUnitItemServiceCompMappings;


            //var coll = Helper.Tariff.GetItemTariffComponentCollection(txtItemID.Text);

            //// dikomen dulu karena tidak masalah bila settingan lama masih ada, toh tidak terlihat di aplikasi, tidak perlu didelete karena nanti mungkin diperlukan saat membenarkan transaksi lama yang tidak benar
            ////foreach (var rec in ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).Where(rec => coll.All(c => c.TariffComponentID != rec.TariffComponentID)))
            ////{
            ////    rec.MarkAsDeleted();
            ////}

            //foreach (var rec in coll)
            //{
            //    var entity = ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).FindByPrimaryKey(txtServiceUnitID.Text, txtItemID.Text, rec.TariffComponentID, cboRegType.SelectedValue);
            //    if (entity == null)
            //    {
            //        entity = ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).AddNew();
            //        entity.ServiceUnitID = txtServiceUnitID.Text;
            //        entity.ItemID = txtItemID.Text;
            //        entity.TariffComponentID = rec.TariffComponentID;
            //        entity.SRRegistrationType = cboRegType.SelectedValue;

            //        var comp = new TariffComponent();
            //        comp.LoadByPrimaryKey(entity.TariffComponentID);
            //        entity.TariffComponentName = comp.TariffComponentName;
            //    }
            //}

            //grdServiceUnitItemServiceComp.DataSource = ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).Where(c => c.ItemID == txtItemID.Text);
        }

        protected void grdServiceUnitItemServiceComp_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String componentId = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitItemServiceCompMappingMetadata.ColumnNames.TariffComponentID]);
            String srRegistrationType = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRRegistrationType]);
            String srGIncomeGroup = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ServiceUnitItemServiceCompMappingMetadata.ColumnNames.SRGuarantorIncomeGroup]);

            ServiceUnitItemServiceCompMapping entity = FindServiceUnitItemServiceComponent(componentId, srRegistrationType, srGIncomeGroup);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ServiceUnitItemServiceCompMappings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdServiceUnitItemServiceComp_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ServiceUnitItemServiceCompMappings.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ServiceUnitItemServiceCompMappings.Save();
                
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdServiceUnitItemServiceComp.Rebind();
        }

        private ServiceUnitItemServiceCompMapping FindServiceUnitItemServiceComponent(String componentId, String srRegistrationType, string SRGuarantorIncomeGroup)
        {
            ServiceUnitItemServiceCompMappingCollection coll = ServiceUnitItemServiceCompMappings;
            ServiceUnitItemServiceCompMapping retEntity = null;
            foreach (ServiceUnitItemServiceCompMapping rec in coll)
            {
                if (rec.TariffComponentID.Equals(componentId) && rec.SRRegistrationType.Equals(srRegistrationType) && rec.SRGuarantorIncomeGroup.Equals(SRGuarantorIncomeGroup))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;

            //return ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).FindByPrimaryKey(txtServiceUnitID.Text, txtItemID.Text, componentID, cboRegType.SelectedValue);
        }

        private void SetEntityValue(ServiceUnitItemServiceCompMapping entity, GridCommandEventArgs e)
        {
            var userControl = (ServiceUnitItemServiceCompDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ServiceUnitID = txtServiceUnitID.Text;
                entity.ItemID = txtItemID.Text;
                entity.TariffComponentID = userControl.TariffComponentID;
                entity.SRRegistrationType = userControl.SRRegistrationType;
                entity.ChartOfAccountIdIncome = userControl.COARevenueID != 0 ? userControl.COARevenueID : null;
                entity.COARevenueName = userControl.COARevenueName;
                entity.SubledgerIdIncome = userControl.SubledgerRevenueID != 0 ? userControl.SubledgerRevenueID : null;
                entity.SubledgerRevenueName = userControl.SubledgerRevenueName;
                entity.ChartOfAccountIdDiscount = userControl.COADiscountID != 0 ? userControl.COADiscountID : null;
                entity.COADiscountName = userControl.COADiscountName;
                entity.SubledgerIdDiscount = userControl.SubledgerDiscountlID != 0 ? userControl.SubledgerDiscountlID : null;
                entity.SubledgerDiscountName = userControl.SubledgerDiscountName;
                entity.ChartOfAccountIdCost = userControl.COACostID != 0 ? userControl.COACostID : null;
                entity.COACostName = userControl.COACostName;
                entity.SubledgerIdCost = userControl.SubledgerCostlID != 0 ? userControl.SubledgerCostlID : null;
                entity.SubledgerCostName = userControl.SubledgerCostName;
                entity.SRGuarantorIncomeGroup = userControl.GuarantorIncomeGroupID;
                entity.GuarantorIncomeGroupName = userControl.GuarantorIncomeGroupName;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }
    }
}

