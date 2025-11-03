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
    public partial class ParamedicFeeItemCompList : BasePageDialog
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

            txtItemID.Text = Request.QueryString["itemID"];
            var item = new Item();
            item.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = item.ItemName;
        }

        private ParamedicFeeItemCompCollection ParamedicFeeItemComps
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeItemComp"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeItemCompCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeItemCompCollection();
                var query = new ParamedicFeeItemCompQuery("a");
                var tcQ = new TariffComponentQuery("b");
                query.InnerJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);
                query.Select
                    (
                        query,
                        tcQ.TariffComponentName.As("refToTariffComponent_TariffComponentName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text, query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collParamedicFeeItemComp"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeItemComp"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ParamedicFeeItemComps;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String tcId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeItemComp entity = FindParamedicFeeItemComp(tcId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemComps.Save();

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
                        ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeItemComp entity = FindParamedicFeeItemComp(tcId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicFeeItemComps.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeItemComps.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemComps.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ParamedicFeeItemComp FindParamedicFeeItemComp(String tcId)
        {
            ParamedicFeeItemCompCollection coll = ParamedicFeeItemComps;
            ParamedicFeeItemComp retEntity = null;
            foreach (ParamedicFeeItemComp rec in coll)
            {
                if (rec.TariffComponentID.Equals(tcId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicFeeItemComp entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeItemCompDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
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
