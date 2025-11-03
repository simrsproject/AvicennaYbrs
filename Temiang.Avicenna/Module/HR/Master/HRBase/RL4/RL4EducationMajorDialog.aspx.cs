using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.Master.HRBase.RL4
{
    public partial class RL4EducationMajorDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RL4;

            if (!IsPostBack)
            {
                LoadData();

                bool isVisible;
                if (Request.QueryString["md"] == "view")
                {
                    isVisible = false;
                }
                else
                {
                    isVisible = true;
                }

                grdDetail.Columns[0].Visible = isVisible;
                grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;

                grdDetail.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            }
        }

        private void LoadData()
        {
            txtRL4TypeID.Text = Request.QueryString["typeId"];
            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.RL4Type.ToString(), txtRL4TypeID.Text);
            lblRL4TypeName.Text = std.ItemName;

            txtRL4ProfessionTypeID.Text = Request.QueryString["profId"];
            std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.RL4ProfessionType.ToString(), txtRL4ProfessionTypeID.Text);
            lblRL4ProfessionTypeName.Text = std.ItemName;

            txtRL4EducationLevelID.Text = Request.QueryString["eduId"];
            std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.RL4EducationLevel.ToString(), txtRL4EducationLevelID.Text);
            lblRL4EducationLevelName.Text = std.ItemName;
        }

        private AppStandardReferenceItemCollection RL4EducationMajors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRL4EducationMajor"];
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
                query.Where(query.StandardReferenceID == AppEnum.StandardReference.RL4EducationMajor, query.ReferenceID == txtRL4EducationLevelID.Text);
                coll.Load(query);

                Session["collRL4EducationMajor"] = coll;
                return coll;
            }
            set
            {
                Session["collRL4EducationMajor"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = RL4EducationMajors;
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
                RL4EducationMajors.Save();

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
            var entity = RL4EducationMajors.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                RL4EducationMajors.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private AppStandardReferenceItem FindItem(String id)
        {
            AppStandardReferenceItemCollection coll = RL4EducationMajors;
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
            var userControl = (RL4EducationMajorItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = AppEnum.StandardReference.RL4EducationMajor.ToString();
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.ReferenceID = txtRL4EducationLevelID.Text;
                entity.CustomField = txtRL4TypeID.Text;
                entity.CustomField2 = txtRL4ProfessionTypeID.Text;
                entity.IsActive = true;
                entity.IsUsedBySystem = true;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }
    }
}