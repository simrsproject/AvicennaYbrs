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
    public partial class IncidentTypeSubComponentList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.IncidentType;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtSRIncidentType.Text = Request.QueryString["type"];
            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.IncidentType.ToString(), txtSRIncidentType.Text);
            lblSRIncidentTypeName.Text = std.ItemName;

            txtComponentID.Text = Request.QueryString["compId"];
            var comp = new IncidentType();
            comp.LoadByPrimaryKey(txtSRIncidentType.Text, txtComponentID.Text);
            lblSRIncidentTypeName.Text = comp.ComponentName;
        }

        private IncidentTypeItemCollection IncidentTypeItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collIncidentTypeItem"];
                    if (obj != null)
                    {
                        return ((IncidentTypeItemCollection)(obj));
                    }
                }

                var coll = new IncidentTypeItemCollection();
                var query = new IncidentTypeItemQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.SRIncidentType == txtSRIncidentType.Text, query.ComponentID== txtComponentID.Text);
                coll.Load(query);

                Session["collIncidentTypeItem"] = coll;
                return coll;
            }
            set
            {
                Session["collIncidentTypeItem"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = IncidentTypeItems;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String subCompId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        IncidentTypeItemMetadata.ColumnNames.SubComponentID]);
            IncidentTypeItem entity = FindIncidentTypeItem(subCompId);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                IncidentTypeItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String subCompId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        IncidentTypeItemMetadata.ColumnNames.SubComponentID]);

            using (var trans = new esTransactionScope())
            {
                IncidentTypeItem entity = FindIncidentTypeItem(subCompId);
                if (entity != null)
                {
                    entity.MarkAsDeleted();
                }

                IncidentTypeItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = IncidentTypeItems.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                IncidentTypeItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private IncidentTypeItem FindIncidentTypeItem(String subCompId)
        {
            IncidentTypeItemCollection coll = IncidentTypeItems;
            IncidentTypeItem retEntity = null;
            foreach (IncidentTypeItem rec in coll)
            {
                if (rec.SubComponentID.Equals(subCompId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(IncidentTypeItem entity, GridCommandEventArgs e)
        {
            var userControl = (IncidentTypeSubComponentDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRIncidentType = txtSRIncidentType.Text;
                entity.ComponentID = txtComponentID.Text;
                entity.SubComponentID = userControl.SubComponentID;
                entity.SubComponentName = userControl.SubComponentName;
                entity.IsAllowEdit = userControl.IsAllowEdit;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }     
    }
}
