using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeGuarantorCategoryItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Paramedic;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtParamedicID.Text = Request.QueryString["paramedicID"];
            var par = new Paramedic();
            par.LoadByPrimaryKey(txtParamedicID.Text);
            lblParamedicName.Text = par.ParamedicName;

            StandardReference.InitializeIncludeSpace(cboSRPhysicianFeeType, AppEnum.StandardReference.PhysicianFeeType);
            cboSRPhysicianFeeType.SelectedValue = Request.QueryString["feeType"];
        }

        private ParamedicFeeGuarantorCategoryItemCollection ParamedicFeeGuarantorCategoryItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeGuarantorCategoryItem"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeGuarantorCategoryItemCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeGuarantorCategoryItemCollection();
                var query = new ParamedicFeeGuarantorCategoryItemQuery("a");
                var itemQ = new ItemQuery("b");
                query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
                query.Select
                    (
                        query,
                        itemQ.ItemName.As("refToItem_ItemName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text, query.SRPhysicianFeeType == cboSRPhysicianFeeType.SelectedValue);
                coll.Load(query);

                Session["collParamedicFeeGuarantorCategoryItem"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeGuarantorCategoryItem"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ParamedicFeeGuarantorCategoryItems;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID]);
            ParamedicFeeGuarantorCategoryItem entity = FindParamedicFeeGuarantorCategoryItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeGuarantorCategoryItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID]);
            ParamedicFeeGuarantorCategoryItem entity = FindParamedicFeeGuarantorCategoryItem(itemId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicFeeGuarantorCategoryItems.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeGuarantorCategoryItems.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeGuarantorCategoryItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ParamedicFeeGuarantorCategoryItem FindParamedicFeeGuarantorCategoryItem(String itemId)
        {
            ParamedicFeeGuarantorCategoryItemCollection coll = ParamedicFeeGuarantorCategoryItems;
            ParamedicFeeGuarantorCategoryItem retEntity = null;
            foreach (ParamedicFeeGuarantorCategoryItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicFeeGuarantorCategoryItem entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeGuarantorCategoryItemListDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.SRPhysicianFeeType = cboSRPhysicianFeeType.SelectedValue;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.IsParamedicFeeUsePercentage = userControl.IsFeeUsingPercentage;
                entity.ParamedicFeeAmount = userControl.FeeAmount;
                entity.ParamedicFeeAmountReferral = userControl.FeeAmountReferral;
                entity.IsDeductionFeeUsePercentage = userControl.IsDeductionFeeUsePercentage;
                entity.DeductionFeeAmount = userControl.DeductionFeeAmount;
                entity.DeductionFeeAmountReferral = userControl.DeductionFeeAmountReferral;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }     
    }
}
