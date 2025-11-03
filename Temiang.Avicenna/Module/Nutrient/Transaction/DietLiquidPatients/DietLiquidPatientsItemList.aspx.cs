using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietLiquidPatientsItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DietLiquidPatients;

            if (!IsPostBack)
            {
                txtTransactionNo.Text = Request.QueryString["transNo"];
                txtDietTime.Text = Request.QueryString["time"];
                var dlpt = new DietLiquidPatientTime();
                if (dlpt.LoadByPrimaryKey(txtTransactionNo.Text, txtDietTime.Text))
                {
                    txtFoodID.Text = dlpt.FoodID;
                    var food = new Food();
                    if (food.LoadByPrimaryKey(txtFoodID.Text))
                        txtFoodName.Text = food.FoodName;
                }

                var reg = new Registration();
                if (reg.LoadByPrimaryKey(Request.QueryString["regno"]))
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {
                        this.Title = "Diet Liquid Item for : " + (pat.FirstName + " " + pat.MiddleName + " " + pat.LastName).Trim() + " (MRN: " +
                                     pat.MedicalNo + ")";
                    }
                }
            }
        }

        private DietLiquidPatientItemCollection DietLiquidPatientItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collDietLiquidPatientItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((DietLiquidPatientItemCollection)(obj));
                    }
                }

                var coll = new DietLiquidPatientItemCollection();
                var query = new DietLiquidPatientItemQuery("a");
                var itemq = new ItemQuery("b");
                query.InnerJoin(itemq).On(query.ItemID == itemq.ItemID);
                query.Select
                    (
                        query,
                        itemq.ItemName.As("refToItem_ItemName")
                    );
                query.Where(query.TransactionNo == txtTransactionNo.Text, query.DietTime == txtDietTime.Text);
                coll.Load(query);

                Session["collDietLiquidPatientItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collDietLiquidPatientItem" + Request.UserHostName] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = DietLiquidPatientItems;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        DietLiquidPatientItemMetadata.ColumnNames.ItemID]);
            DietLiquidPatientItem entity = FindItem(itemId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                DietLiquidPatientItems.Save();

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
                        DietLiquidPatientItemMetadata.ColumnNames.ItemID]);

            DietLiquidPatientItem entity = FindItem(itemId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                DietLiquidPatientItems.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = DietLiquidPatientItems.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                DietLiquidPatientItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private DietLiquidPatientItem FindItem(String itemId)
        {
            DietLiquidPatientItemCollection coll = DietLiquidPatientItems;
            DietLiquidPatientItem retEntity = null;
            foreach (DietLiquidPatientItem rec in coll)
            {
                if (rec.ItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(DietLiquidPatientItem entity, GridCommandEventArgs e)
        {
            var userControl = (DietLiquidPatientsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.DietTime = txtDietTime.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }     
    }
}
