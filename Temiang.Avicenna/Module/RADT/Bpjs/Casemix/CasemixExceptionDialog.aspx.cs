using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixExceptionDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CasemixCoverage;

            if (!IsCallback)
            {
                //For Grid Detail
                //PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemServiceExcludeProduct, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.ItemProductMedical, Page);
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Guarantor, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["md"] == "edit")
                {
                    var c = new CasemixCovered();
                    c.LoadByPrimaryKey(Request.QueryString["id"].ToInt());
                    txtName.Text = c.CasemixCoveredName;
                    txtNotes.Text = c.Notes;
                    chkActive.Checked = c.IsActive ?? false;
                }
                else
                {
                    chkActive.Checked = true;
                }

                RefreshCommandItemCoveredItem();
                PopulateCoveredItemGrid();

                RefreshCommandItemCoveredItemProduct();
                PopulateCoveredItemProductGrid();

                RefreshCommandItemCasemixCoveredGuarantor();
                PopulateCoveredGuarantorGrid();
            }
        }

        #region Guarantor
        private CasemixCoveredGuarantorCollection CoveredGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCasemixCoveredGuarantor"];
                    if (obj != null) return ((CasemixCoveredGuarantorCollection)(obj));
                }

                CasemixCoveredGuarantorCollection coll = new CasemixCoveredGuarantorCollection();

                CasemixCoveredGuarantorQuery query = new CasemixCoveredGuarantorQuery("a");
                GuarantorQuery asri = new GuarantorQuery("b");

                query.Select(query, asri.GuarantorName.As("refToGuarantor_GuarantorName"));
                query.InnerJoin(asri).On(query.GuarantorID == asri.GuarantorID);
                query.Where(query.CasemixCoveredID == (Request.QueryString["md"] == "new" ? -1 : Request.QueryString["id"].ToInt()));
                query.OrderBy(asri.GuarantorName.Ascending);
                coll.Load(query);

                Session["collCasemixCoveredGuarantor"] = coll;
                return coll;
            }
            set
            {
                Session["collCasemixCoveredGuarantor"] = value;
            }
        }

        private void RefreshCommandItemCasemixCoveredGuarantor()
        {
            //Toogle grid command
            grdGuarantor.Columns[grdGuarantor.Columns.Count - 1].Visible = true;

            //Perbaharui tampilan dan data
            grdGuarantor.Rebind();
        }

        private void PopulateCoveredGuarantorGrid()
        {
            //Display Data Detail
            CoveredGuarantors = null; //Reset Record Detail
            grdGuarantor.DataSource = CoveredGuarantors; //Requery
            grdGuarantor.MasterTableView.IsItemInserted = false;
            grdGuarantor.MasterTableView.ClearEditItems();
            grdGuarantor.DataBind();
        }

        protected void grdGuarantor_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdGuarantor.DataSource = CoveredGuarantors;
        }

        protected void grdGuarantor_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var entity = CoveredGuarantors.AddNew();
            SetEntityValueCoveredGuarantor(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdGuarantor.Rebind();
        }

        protected void grdGuarantor_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredGuarantorMetadata.ColumnNames.CasemixCoveredID]);
            string guarantorID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID]);

            var entity = FindCasemixCoveredGuarantor(guarantorID);
            if (entity != null) entity.MarkAsDeleted();
        }

        private CasemixCoveredGuarantor FindCasemixCoveredGuarantor(string guarantorID)
        {
            var coll = CoveredGuarantors;
            return coll.FirstOrDefault(rec => rec.GuarantorID.Equals(guarantorID));
        }

        private void SetEntityValueCoveredGuarantor(CasemixCoveredGuarantor entity, GridCommandEventArgs e)
        {
            CasemixExceptionGuarantorDetail userControl = (CasemixExceptionGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
            }
        }
        #endregion

        #region ItemService
        private CasemixCoveredDetailCollection CoveredItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCasemixCoveredDetail"];
                    if (obj != null) return ((CasemixCoveredDetailCollection)(obj));
                }

                CasemixCoveredDetailCollection coll = new CasemixCoveredDetailCollection();

                CasemixCoveredDetailQuery query = new CasemixCoveredDetailQuery("a");
                ItemQuery itm = new ItemQuery("b");
                var asri = new AppStandardReferenceItemQuery("c");

                query.Select(query, itm.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(itm).On(query.ItemID == itm.ItemID);
                query.InnerJoin(asri).On(asri.StandardReferenceID == AppEnum.StandardReference.ItemType && asri.ItemID == itm.SRItemType);

                query.Where(query.CasemixCoveredID == (Request.QueryString["md"] == "new" ? -1 : Request.QueryString["id"].ToInt()), 
                    asri.ReferenceID == "Service");
                query.OrderBy(itm.ItemName.Ascending);
                coll.Load(query);

                Session["collCasemixCoveredDetail"] = coll;
                return coll;
            }
            set
            {
                Session["collCasemixCoveredDetail"] = value;
            }
        }

        private void RefreshCommandItemCoveredItem()
        {
            //Toogle grid command
            grdItem.Columns[0].Visible = true;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = true;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateCoveredItemGrid()
        {
            //Display Data Detail
            CoveredItems = null; //Reset Record Detail
            grdItem.DataSource = CoveredItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemService.Text.Trim() != string.Empty)
            {
                var ds = from d in CoveredItems
                         where d.ItemName.ToLower().Contains(txtFilterItemService.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemService.Text.ToLower())
                         select d;
                grdItem.DataSource = ds;
            }
            else
            {
                grdItem.DataSource = CoveredItems;
            }
        }

        protected void grdItem_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var entity = CoveredItems.AddNew();
            SetEntityValueCoveredItem(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        protected void grdItem_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID]);
            string itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetail(itemID);
            if (entity != null) SetEntityValueCoveredItem(entity, e);
        }

        protected void grdItem_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID]);
            string itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetail(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        private CasemixCoveredDetail FindCasemixCoveredDetail(string itemID)
        {
            var coll = CoveredItems;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        private void SetEntityValueCoveredItem(CasemixCoveredDetail entity, GridCommandEventArgs e)
        {
            CasemixExceptionItemDetail userControl = (CasemixExceptionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IsInclude = false; //db:20231204 - include dipake sbg penanda apakah item product atau item service (0: item service, 1: item product)
                entity.IsUsingGlobalSetting = userControl.IsUsingGlobalSetting;
                entity.Qty = Convert.ToDecimal(userControl.Qty);
                entity.QtyIpr = Convert.ToDecimal(userControl.QtyIpr);
                entity.QtyOpr = Convert.ToDecimal(userControl.QtyOpr);
                entity.QtyEmr = Convert.ToDecimal(userControl.QtyEmr);
                entity.IsNeedCasemixValidate = userControl.IsNeedCasemixValidate;
                entity.IsNeedCasemixValidateIpr = userControl.IsNeedCasemixValidateIpr;
                entity.IsNeedCasemixValidateOpr = userControl.IsNeedCasemixValidateOpr;
                entity.IsNeedCasemixValidateEmr = userControl.IsNeedCasemixValidateEmr;
                entity.IsAllowedToOrder = true;
                entity.IsAllowedToOrderIpr = true;
                entity.IsAllowedToOrderOpr = true;
                entity.IsAllowedToOrderEmr = true;
            }
        }
        #endregion

        #region ItemProduct
        private CasemixCoveredDetailCollection CoveredItemProducts
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCasemixCoveredDetailItemProduct"];
                    if (obj != null) return ((CasemixCoveredDetailCollection)(obj));
                }

                CasemixCoveredDetailCollection coll = new CasemixCoveredDetailCollection();

                CasemixCoveredDetailQuery query = new CasemixCoveredDetailQuery("a");
                ItemQuery itm = new ItemQuery("b");
                var asri = new AppStandardReferenceItemQuery("c");

                query.Select(query, itm.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(itm).On(query.ItemID == itm.ItemID);
                query.InnerJoin(asri).On(asri.StandardReferenceID == AppEnum.StandardReference.ItemType && asri.ItemID == itm.SRItemType);

                query.Where(query.CasemixCoveredID == (Request.QueryString["md"] == "new" ? -1 : Request.QueryString["id"].ToInt()),
                    asri.ReferenceID == "Product");
                
                query.OrderBy(itm.ItemName.Ascending);
                coll.Load(query);

                Session["collCasemixCoveredDetailItemProduct"] = coll;
                return coll;
            }
            set
            {
                Session["collCasemixCoveredDetailItemProduct"] = value;
            }
        }

        private void RefreshCommandItemCoveredItemProduct()
        {
            //Toogle grid command
            grdItemProduct.Columns[0].Visible = true;
            grdItemProduct.Columns[grdItemProduct.Columns.Count - 1].Visible = true;

            //Perbaharui tampilan dan data
            grdItemProduct.Rebind();
        }

        private void PopulateCoveredItemProductGrid()
        {
            //Display Data Detail
            CoveredItemProducts = null; //Reset Record Detail
            grdItemProduct.DataSource = CoveredItemProducts; //Requery
            grdItemProduct.MasterTableView.IsItemInserted = false;
            grdItemProduct.MasterTableView.ClearEditItems();
            grdItemProduct.DataBind();
        }

        protected void grdItemProduct_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemProduct.Text.Trim() != string.Empty)
            {
                var ds = from d in CoveredItemProducts
                         where d.ItemName.ToLower().Contains(txtFilterItemProduct.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemProduct.Text.ToLower())
                         select d;
                grdItemProduct.DataSource = ds;
            }
            else
            {
                grdItemProduct.DataSource = CoveredItemProducts;
            }
        }

        protected void grdItemProduct_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var entity = CoveredItemProducts.AddNew();
            SetEntityValueCoveredItemProduct(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItemProduct.Rebind();
        }

        protected void grdItemProduct_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID]);
            string itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetailProduct(itemID);
            if (entity != null) SetEntityValueCoveredItemProduct(entity, e);
        }

        protected void grdItemProduct_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID]);
            string itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredDetailMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetailProduct(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        private CasemixCoveredDetail FindCasemixCoveredDetailProduct(string itemID)
        {
            var coll = CoveredItemProducts;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        private void SetEntityValueCoveredItemProduct(CasemixCoveredDetail entity, GridCommandEventArgs e)
        {
            CasemixExceptionItemProductDetail userControl = (CasemixExceptionItemProductDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IsInclude = true; //db:20231204 - include dipake sbg penanda apakah item product atau item service (0: item service, 1: item product)
                entity.IsUsingGlobalSetting = userControl.IsUsingGlobalSetting;
                entity.Qty = 0;
                entity.QtyIpr = 0;
                entity.QtyOpr = 0;
                entity.QtyEmr = 0;
                entity.IsNeedCasemixValidate = userControl.IsNeedCasemixValidate;
                entity.IsNeedCasemixValidateIpr = userControl.IsNeedCasemixValidateIpr;
                entity.IsNeedCasemixValidateOpr = userControl.IsNeedCasemixValidateOpr;
                entity.IsNeedCasemixValidateEmr = userControl.IsNeedCasemixValidateEmr;
                entity.IsAllowedToOrder = userControl.IsAllowedToOrder;
                entity.IsAllowedToOrderIpr = userControl.IsAllowedToOrderIpr;
                entity.IsAllowedToOrderOpr = userControl.IsAllowedToOrderOpr;
                entity.IsAllowedToOrderEmr = userControl.IsAllowedToOrderEmr;
            }
        }
        #endregion

        public override bool OnButtonOkClicked()
        {
            using (var trans = new esTransactionScope())
            {
                var c = new CasemixCovered();
                if (Request.QueryString["md"] == "new") c = new CasemixCovered();
                else c.LoadByPrimaryKey(Request.QueryString["id"].ToInt());
                c.CasemixCoveredName = txtName.Text;
                c.Notes = txtNotes.Text;
                c.IsActive = chkActive.Checked;
                c.Save();

                foreach (var item in CoveredItems)
                {
                    item.CasemixCoveredID = c.CasemixCoveredID;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
                CoveredItems.Save();

                foreach (var item in CoveredItemProducts)
                {
                    item.CasemixCoveredID = c.CasemixCoveredID;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
                CoveredItemProducts.Save();

                foreach (var item in CoveredGuarantors)
                {
                    item.CasemixCoveredID = c.CasemixCoveredID;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
                CoveredGuarantors.Save();

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'reload'";
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            switch (RadMultiPage1.SelectedIndex)
            {
                case 1:
                    {
                        grdItem.CurrentPageIndex = 0;
                        grdItem.Rebind();
                        break;
                    }
                case 2:
                    {
                        grdItemProduct.CurrentPageIndex = 0;
                        grdItemProduct.Rebind();
                        break;
                    }
            }

        }
    }
}