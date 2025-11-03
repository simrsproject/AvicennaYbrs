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
    public partial class ContributoryFactorsClassificationFrameworkItemComponentList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ContributoryFactorsClassificationFramework;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtFactorID.Text = Request.QueryString["fid"];
            var fc = new ContributoryFactorsClassificationFramework();
            fc.LoadByPrimaryKey(txtFactorID.Text);
            lblFactorName.Text = fc.FactorName;

            txtFactorItemID.Text = Request.QueryString["fiid"];
            var fci = new ContributoryFactorsClassificationFrameworkItem();
            fci.LoadByPrimaryKey(txtFactorID.Text, txtFactorItemID.Text);
            lblFactorItemName.Text = fci.FactorItemName;
        }

        private ContributoryFactorsClassificationFrameworkItemComponentCollection ContributoryFactorsClassificationFrameworkItemComponents
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collContributoryFactorsClassificationFrameworkItemComponent"];
                    if (obj != null)
                    {
                        return ((ContributoryFactorsClassificationFrameworkItemComponentCollection)(obj));
                    }
                }

                var coll = new ContributoryFactorsClassificationFrameworkItemComponentCollection();
                var query = new ContributoryFactorsClassificationFrameworkItemComponentQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.FactorID == txtFactorID.Text, query.FactorItemID == txtFactorItemID.Text);
                coll.Load(query);

                Session["collContributoryFactorsClassificationFrameworkItemComponent"] = coll;
                return coll;
            }
            set
            {
                Session["collContributoryFactorsClassificationFrameworkItemComponent"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ContributoryFactorsClassificationFrameworkItemComponents;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String compId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID]);
            ContributoryFactorsClassificationFrameworkItemComponent entity = FindItem(compId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ContributoryFactorsClassificationFrameworkItemComponents.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String compId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        ContributoryFactorsClassificationFrameworkItemComponentMetadata.ColumnNames.ComponentID]);
            ContributoryFactorsClassificationFrameworkItemComponent entity = FindItem(compId);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                //entity.Save();
            }
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ContributoryFactorsClassificationFrameworkItemComponents.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = ContributoryFactorsClassificationFrameworkItemComponents.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ContributoryFactorsClassificationFrameworkItemComponents.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private ContributoryFactorsClassificationFrameworkItemComponent FindItem(String compId)
        {
            ContributoryFactorsClassificationFrameworkItemComponentCollection coll = ContributoryFactorsClassificationFrameworkItemComponents;
            ContributoryFactorsClassificationFrameworkItemComponent retEntity = null;
            foreach (ContributoryFactorsClassificationFrameworkItemComponent rec in coll)
            {
                if (rec.ComponentID.Equals(compId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ContributoryFactorsClassificationFrameworkItemComponent entity, GridCommandEventArgs e)
        {
            var userControl = (ContributoryFactorsClassificationFrameworkItemComponentDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.FactorID = txtFactorID.Text;
                entity.FactorItemID = txtFactorItemID.Text;
                entity.ComponentID = userControl.ComponentID;
                entity.ComponentName = userControl.ComponentName;
                entity.IsAllowEdit = userControl.IsAllowEdit;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }     
    }
}
