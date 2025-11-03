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
    public partial class ServiceUnitItemServiceClass : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnit;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtServiceUnitID.Text = Request.QueryString["unitID"];
            ServiceUnit unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);
            lblServiceUnitName.Text = unit.ServiceUnitName;

            txtItemID.Text = Request.QueryString["itemID"];
            Item item = new Item();
            item.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = item.ItemName;

        }
       
        protected void grdServiceUnitItemService_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           // var comps = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).Where(c => c.ItemID == txtItemID.Text);
            //if (!comps.Any())
            //{
                var coll = Helper.Tariff.GetItemTariffComponentCollection(txtItemID.Text);
                var classes = new ClassCollection();
                classes.LoadAll();

                foreach (ItemTariffComponent rec in coll)
                {
                    //if (rec.ClassID == AppSession.Parameter.DefaultTariffClass)
                    //{
                        foreach (Class a in classes)
                        {
                            var comps = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).Where(c => c.ItemID == txtItemID.Text && 
                                                                                                                                             c.TariffComponentID == rec.TariffComponentID && 
                                                                                                                                             c.ClassID == a.ClassID);

                            if (!comps.Any())
                            {
                                var entity = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).AddNew();
                                entity.ServiceUnitID = txtServiceUnitID.Text;
                                entity.ClassID = a.ClassID;

                                var cls = new Class();
                                cls.LoadByPrimaryKey(entity.ClassID);
                                entity.ClassName = a.ClassName;

                                entity.ItemID = txtItemID.Text;
                                entity.TariffComponentID = rec.TariffComponentID;

                                var comp = new TariffComponent();
                                comp.LoadByPrimaryKey(entity.TariffComponentID);
                                entity.TariffComponentName = comp.TariffComponentName;
                            }
                        }
                    //}
                    //else
                    //{
                    //    var comps = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).Where(c => c.ItemID == txtItemID.Text && 
                    //                                                                                                                     c.TariffComponentID == rec.TariffComponentID);
                    //    if (!comps.Any())
                    //    {
                    //        var entity = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).AddNew();
                    //        entity.ServiceUnitID = txtServiceUnitID.Text;
                    //        entity.ClassID = rec.ClassID;

                    //        var cls = new Class();
                    //        cls.LoadByPrimaryKey(entity.ClassID);
                    //        entity.ClassName = cls.ClassName;

                    //        entity.ItemID = txtItemID.Text;
                    //        entity.TariffComponentID = rec.TariffComponentID;

                    //        var comp = new TariffComponent();
                    //        comp.LoadByPrimaryKey(entity.TariffComponentID);
                    //        entity.TariffComponentName = comp.TariffComponentName;
                    //    }
                    //}
                }
            //}
            grdServiceUnitItemService.DataSource = ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).Where(c => c.ItemID == txtItemID.Text);
        }

        protected void grdServiceUnitItemService_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String classID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID]);
            String componentID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID]);
            BusinessObject.ServiceUnitItemServiceClass entity = FindServiceUnitItemServiceClass(classID, componentID);
            if (entity != null)
                SetEntityValue(entity, e);
        }
     
        private BusinessObject.ServiceUnitItemServiceClass FindServiceUnitItemServiceClass(String classID, string componentID)
        {
            return ((ServiceUnitItemServiceClassCollection)Session["ServiceUnitItemServiceClassCollection"]).FindByPrimaryKey(txtServiceUnitID.Text, txtItemID.Text, classID, componentID);
        }

        private void SetEntityValue(BusinessObject.ServiceUnitItemServiceClass entity, GridCommandEventArgs e)
        {
            ServiceUnitItemServiceClassDetail userControl = (ServiceUnitItemServiceClassDetail)e.Item.FindControl(
                GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
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
            }
        }     
    }
}
