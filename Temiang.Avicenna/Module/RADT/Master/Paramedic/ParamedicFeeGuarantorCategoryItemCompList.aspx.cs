using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeGuarantorCategoryItemCompList : BasePageDialog
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

            txtItemID.Text = Request.QueryString["itemID"];
            var item = new Item();
            item.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = item.ItemName;
        }

        private ParamedicFeeGuarantorCategoryItemCompCollection ParamedicFeeGuarantorCategoryItemComps
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeGuarantorCategoryItemComp"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeGuarantorCategoryItemCompCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeGuarantorCategoryItemCompCollection();
                var query = new ParamedicFeeGuarantorCategoryItemCompQuery("a");
                var tcQ = new TariffComponentQuery("b");
                query.InnerJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);
                query.Select
                    (
                        query,
                        tcQ.TariffComponentName.As("refToTariffComponent_TariffComponentName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text,
                            query.SRPhysicianFeeType == cboSRPhysicianFeeType.SelectedValue,
                            query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collParamedicFeeGuarantorCategoryItemComp"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeGuarantorCategoryItemComp"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ParamedicFeeGuarantorCategoryItemComps;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String tcId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeGuarantorCategoryItemComp entity = FindParamedicFeeGuarantorCategoryItemComp(tcId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeGuarantorCategoryItemComps.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String tcId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeGuarantorCategoryItemComp entity = FindParamedicFeeGuarantorCategoryItemComp(tcId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicFeeGuarantorCategoryItemComps.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeGuarantorCategoryItemComps.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeGuarantorCategoryItemComps.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ParamedicFeeGuarantorCategoryItemComp FindParamedicFeeGuarantorCategoryItemComp(String tcId)
        {
            ParamedicFeeGuarantorCategoryItemCompCollection coll = ParamedicFeeGuarantorCategoryItemComps;
            ParamedicFeeGuarantorCategoryItemComp retEntity = null;
            foreach (ParamedicFeeGuarantorCategoryItemComp rec in coll)
            {
                if (rec.TariffComponentID.Equals(tcId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicFeeGuarantorCategoryItemComp entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeGuarantorCategoryItemCompListDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.SRPhysicianFeeType = cboSRPhysicianFeeType.SelectedValue;
                entity.ItemID = txtItemID.Text;
                entity.TariffComponentID = userControl.TariffComponentID;
                entity.TariffComponentName = userControl.TariffComponentName;
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
