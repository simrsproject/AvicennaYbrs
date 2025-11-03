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
    public partial class ParamedicFeeItemGuarantorList : BasePageDialog
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

        private ParamedicFeeItemGuarantorCollection ParamedicFeeItemGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicFeeItemGuarantor"];
                    if (obj != null)
                    {
                        return ((ParamedicFeeItemGuarantorCollection)(obj));
                    }
                }

                var coll = new ParamedicFeeItemGuarantorCollection();
                var query = new ParamedicFeeItemGuarantorQuery("a");
                var guarQ = new GuarantorQuery("b");
                query.InnerJoin(guarQ).On(query.GuarantorID == guarQ.GuarantorID);
                query.Select
                    (
                        query,
                        guarQ.GuarantorName.As("refToGuarantor_GuarantorName")
                    );
                query.Where(query.ParamedicID == txtParamedicID.Text, query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collParamedicFeeItemGuarantor"] = coll;
                return coll;
            }
            set
            {
                Session["collParamedicFeeItemGuarantor"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ParamedicFeeItemGuarantors;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String guarID =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID]);
            ParamedicFeeItemGuarantor entity = FindParamedicFeeItemGuarantor(guarID);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemGuarantors.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String guarID =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID]);
            ParamedicFeeItemGuarantor entity = FindParamedicFeeItemGuarantor(guarID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParamedicFeeItemGuarantors.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ParamedicFeeItemGuarantors.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicFeeItemGuarantors.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ParamedicFeeItemGuarantor FindParamedicFeeItemGuarantor(String guarID)
        {
            ParamedicFeeItemGuarantorCollection coll = ParamedicFeeItemGuarantors;
            ParamedicFeeItemGuarantor retEntity = null;
            foreach (ParamedicFeeItemGuarantor rec in coll)
            {
                if (rec.GuarantorID.Equals(guarID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicFeeItemGuarantor entity, GridCommandEventArgs e)
        {
            ParamedicFeeItemGuarantorDetail userControl = (ParamedicFeeItemGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = txtParamedicID.Text;
                entity.ItemID = txtItemID.Text;
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
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
