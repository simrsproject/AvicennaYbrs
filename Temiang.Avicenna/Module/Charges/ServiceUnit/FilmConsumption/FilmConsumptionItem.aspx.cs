using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FilmConsumptionItem : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.FilmConsumptionEntry;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtItemID.Text = Request.QueryString["itemID"];
            var i = new Item();
            i.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = i.ItemName;
        }

        private TransChargesItemFilmConsumptionCollection TransChargesItemFilmConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTransChargesItemFilmConsumption" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TransChargesItemFilmConsumptionCollection)(obj));
                    }
                }

                var coll = new TransChargesItemFilmConsumptionCollection();
                var query = new TransChargesItemFilmConsumptionQuery("a");
                var srQ = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(srQ).On(query.SRFilmID == srQ.ItemID && srQ.StandardReferenceID == "Film");
                query.Select
                    (
                        query,
                        srQ.ItemName.As("refToAppStandardReferenceItem_Film")
                    );
                query.Where(query.TransactionNo == Request.QueryString["joNo"],
                            query.SequenceNo == Request.QueryString["seqNo"]);
                coll.Load(query);

                Session["collTransChargesItemFilmConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collTransChargesItemFilmConsumption" + Request.UserHostName] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = TransChargesItemFilmConsumptions;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String transNo =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo]);
            String seqNo =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo]);
            String srFilmId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID]);
            TransChargesItemFilmConsumption entity = FindTransChargesItemFilmConsumption(transNo, seqNo, srFilmId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String transNo =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo]);
            String seqNo =
                            Convert.ToString(
                                item.OwnerTableView.DataKeyValues[item.ItemIndex][
                                    TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo]);
            String srFilmId =
                            Convert.ToString(
                                item.OwnerTableView.DataKeyValues[item.ItemIndex][
                                    TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID]);
            TransChargesItemFilmConsumption entity = FindTransChargesItemFilmConsumption(transNo, seqNo, srFilmId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransChargesItemFilmConsumptions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private TransChargesItemFilmConsumption FindTransChargesItemFilmConsumption(String transNo, String seqNo, string srFilmId)
        {
            TransChargesItemFilmConsumptionCollection coll = TransChargesItemFilmConsumptions;
            TransChargesItemFilmConsumption retEntity = null;
            foreach (TransChargesItemFilmConsumption rec in coll)
            {
                if (rec.TransactionNo.Equals(transNo) && rec.SequenceNo.Equals(seqNo) && rec.SRFilmID.Equals(srFilmId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TransChargesItemFilmConsumption entity, GridCommandEventArgs e)
        {
            var userControl = (FilmConsumptionItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = Request.QueryString["joNo"];
                entity.SequenceNo = Request.QueryString["seqNo"];
                entity.SRFilmID = userControl.SRFilmID;
                entity.FilmName = userControl.FilmName;
                entity.Qty = userControl.Qty;
                entity.Kv = userControl.Kv;
                entity.Ma = userControl.Ma;
                entity.S = userControl.S;
                entity.Mas = userControl.Mas;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        public override bool OnButtonOkClicked()
        {
            using (var trans = new esTransactionScope())
            {
                TransChargesItemFilmConsumptions.Save();
                trans.Complete();
            }

            return true;
        }
    }
}
