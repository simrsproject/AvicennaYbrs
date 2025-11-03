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
    public partial class ParamedicFeeItemGuarantorCompList : BasePageDialog
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

            txtGuarantorID.Text = Request.QueryString["guarId"];
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guar.GuarantorName;
        }

        private ParamedicFeeItemGuarantorCompCollection ParamedicFeeItemGuarantorComps
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeItemGuarantorComp"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeItemGuarantorCompCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeItemGuarantorCompCollection();
                var query = new ParamedicFeeItemGuarantorCompQuery("a");
                var tcQ = new TariffComponentQuery("b");
                query.InnerJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);
                query.Select
                    (
                        query,
                        tcQ.TariffComponentName.As("refToTariffComponent_TariffComponentName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text, query.ItemID == txtItemID.Text,
                            query.GuarantorID == txtGuarantorID.Text);
                coll.Load(query);

                Session["collParamedicFeeItemGuarantorComp"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeItemGuarantorComp"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ParamedicFeeItemGuarantorComps;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String tcId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeItemGuarantorComp entity = FindParamedicFeeItemGuarantorComp(tcId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemGuarantorComps.Save();

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
                        ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID]);
            ParamedicFeeItemGuarantorComp entity = FindParamedicFeeItemGuarantorComp(tcId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicFeeItemGuarantorComps.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeItemGuarantorComps.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemGuarantorComps.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ParamedicFeeItemGuarantorComp FindParamedicFeeItemGuarantorComp(String tcId)
        {
            ParamedicFeeItemGuarantorCompCollection coll = ParamedicFeeItemGuarantorComps;
            ParamedicFeeItemGuarantorComp retEntity = null;
            foreach (ParamedicFeeItemGuarantorComp rec in coll)
            {
                if (rec.TariffComponentID.Equals(tcId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicFeeItemGuarantorComp entity, GridCommandEventArgs e)
        {
            var userControl = (ParamedicFeeItemGuarantorCompDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.ItemID = txtItemID.Text;
                entity.GuarantorID = txtGuarantorID.Text;
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
