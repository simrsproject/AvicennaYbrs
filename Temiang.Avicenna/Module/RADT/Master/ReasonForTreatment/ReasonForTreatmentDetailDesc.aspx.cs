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
    public partial class ReasonForTreatmentDetailDesc : BasePageDialog
    {
        private ReasonsForTreatmentDescCollection ReasonsForTreatmentDesc
        {
            get
            {
                return (ReasonsForTreatmentDescCollection)Session["collReasonsForTreatmentDesc"];
            }
            set { Session["collReasonsForTreatmentDesc"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnit;

            if (!IsPostBack)
            {
                txtSRReasonVisit.Text = Request.QueryString["SRReasonVisit"];
                var sr = new AppStandardReferenceItem();
                sr.LoadByPrimaryKey("VisitReason", txtSRReasonVisit.Text);
                lblSRReasonVisit.Text = sr.ItemName;

                txtReasonsForTreatmentID.Text = Request.QueryString["ReasonsForTreatmentID"];
                var rt = new Temiang.Avicenna.BusinessObject.ReasonsForTreatment();
                rt.LoadByPrimaryKey(sr.ItemID, txtReasonsForTreatmentID.Text);
                lblReasonsForTreatmentName.Text = rt.ReasonsForTreatmentName;
            }
        }

        protected void grdReasonsForTreatmentDesc_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdReasonsForTreatmentDesc.DataSource = ReasonsForTreatmentDesc.Where(c => c.ReasonsForTreatmentID == txtReasonsForTreatmentID.Text);
        }

        protected void grdReasonsForTreatmentDesc_InsertCommand(object source, GridCommandEventArgs e)
        {
            ReasonsForTreatmentDesc entity = ReasonsForTreatmentDesc.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdReasonsForTreatmentDesc.Rebind();
        }

        protected void grdReasonsForTreatmentDesc_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            string ReasonsForTreatmentID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID].ToString();
            string ReasonsForTreatmentDescID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID].ToString();
            ReasonsForTreatmentDesc entity = FindItem(ReasonsForTreatmentID, ReasonsForTreatmentDescID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private ReasonsForTreatmentDesc FindItem(string ReasonsForTreatmentID, string ReasonsForTreatmentDescID)
        {
            var coll = ReasonsForTreatmentDesc;
            ReasonsForTreatmentDesc retEntity = null;
            foreach (ReasonsForTreatmentDesc rec in coll)
            {
                if (rec.ReasonsForTreatmentID.Equals(ReasonsForTreatmentID) && rec.ReasonsForTreatmentDescID.Equals(ReasonsForTreatmentDescID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdReasonsForTreatmentDesc_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string ReasonsForTreatmentID = item.OwnerTableView.DataKeyValues[item.ItemIndex][ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID].ToString();
            string ReasonsForTreatmentDescID = item.OwnerTableView.DataKeyValues[item.ItemIndex][ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID].ToString();
            ReasonsForTreatmentDesc entity = FindItem(ReasonsForTreatmentID, ReasonsForTreatmentDescID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void SetEntityValue(ReasonsForTreatmentDesc entity, GridCommandEventArgs e)
        {
            var userControl = (ReasonForTreatmentDetailDescDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ReasonsForTreatmentDescID = userControl.ReasonsForTreatmentDescID;
                entity.ReasonsForTreatmentDescName = userControl.ReasonsForTreatmentDescName;
                entity.SRReasonVisit = txtSRReasonVisit.Text;
                entity.ReasonsForTreatmentID = txtReasonsForTreatmentID.Text;
            }
        }
    }
}

