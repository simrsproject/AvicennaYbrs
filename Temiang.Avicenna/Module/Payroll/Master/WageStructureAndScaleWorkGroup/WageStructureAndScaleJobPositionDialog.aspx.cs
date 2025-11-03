using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScaleJobPositionDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.WageStructureAndScaleWorkGroup;

            if (!IsPostBack)
            {
                LoadData();

                if (Request.QueryString["md"] == "view")
                {
                    bool isVisible = false;
                    grdDetail.Columns[0].Visible = isVisible;
                    grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;

                    grdDetail.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
                }
            }
        }

        private void LoadData()
        {
            txtGroupID.Text = Request.QueryString["groupId"];
            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeWorkGroup.ToString(), txtGroupID.Text);
            lblGroupName.Text = std.ItemName;

            txtSubGroupID.Text = Request.QueryString["subGroupId"];
            std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeWorkSubGroup.ToString(), txtSubGroupID.Text);
            lblSubGroupName.Text = std.ItemName;
        }

        private AppStandardReferenceItemCollection EmployeeJobPositions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeJobPosition"];
                    if (obj != null)
                    {
                        return ((AppStandardReferenceItemCollection)(obj));
                    }
                }

                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition, query.ReferenceID == txtSubGroupID.Text);
                coll.Load(query);

                Session["collEmployeeJobPosition"] = coll;
                return coll;
            }
            set
            {
                Session["collEmployeeJobPosition"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = EmployeeJobPositions;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                EmployeeJobPositions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        AppStandardReferenceItemMetadata.ColumnNames.ItemID]);
            AppStandardReferenceItem entity = FindItem(id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                entity.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = EmployeeJobPositions.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                EmployeeJobPositions.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private AppStandardReferenceItem FindItem(String id)
        {
            AppStandardReferenceItemCollection coll = EmployeeJobPositions;
            AppStandardReferenceItem retEntity = null;
            foreach (AppStandardReferenceItem rec in coll)
            {
                if (rec.ItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(AppStandardReferenceItem entity, GridCommandEventArgs e)
        {
            var userControl = (WageStructureAndScaleJobPositionDialogItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.EmployeeJobPosition.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = txtSubGroupID.Text;
                entity.CustomField = txtGroupID.Text;
                entity.IsActive = true;
                entity.IsUsedBySystem = true;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }
    }
}