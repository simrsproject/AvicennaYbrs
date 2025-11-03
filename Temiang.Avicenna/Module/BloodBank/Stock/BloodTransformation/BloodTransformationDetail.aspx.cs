using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class BloodTransformationDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BloodTransformationSearch.aspx";
            UrlPageList = "BloodTransformationList.aspx";

            ProgramID = AppConstant.Program.BloodStockTransformation;

            this.WindowSearch.Height = 400;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BloodTransformation());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BloodTransformation();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BloodTransformation();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "BloodTransformation";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BloodTransformation();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var trans = (BloodTransformation)entity;

            txtTransactionNo.Text = trans.TransactionNo;
            txtTransactionDate.SelectedDate = trans.TransactionDate;
            txtNotes.Text = trans.Notes;

            PopulateItemGrid();

            ViewState["IsApproved"] = trans.IsApproved ?? false;
            ViewState["IsVoid"] = trans.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new BloodTransformation();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        private void SetApproval(BloodTransformation entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                var bagnos = (BloodTransformationItems.Select(i => i.BagNo)).Distinct();

                var bloodBagnos = new BloodBagNoCollection();
                bloodBagnos.Query.Where(bloodBagnos.Query.BagNo.In(bagnos));
                bloodBagnos.LoadAll();

                foreach (var item in BloodTransformationItems)
                {
                    var bagno = bloodBagnos.SingleOrDefault(ib => ib.BagNo == item.BagNo);
                    if (bagno != null)
                    {
                        bagno.SRBloodGroup = item.SRBloodGroupTo;
                        bagno.VolumeBag = item.VolumeBag;
                        bagno.ExpiredDateTime = item.ExpiredDateTime;
                        bagno.IsCrossMatching = false;
                        bagno.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bagno.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                bloodBagnos.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new BloodTransformation();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
        }

        private void SetVoid(BloodTransformation entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
            {
                entity.VoidByUserID = null;
                entity.VoidDateTime = null;
            }
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BloodTransformation entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.Notes = txtNotes.Text;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in BloodTransformationItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(BloodTransformation entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                BloodTransformationItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BloodTransformationQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new BloodTransformation();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Blood Bank Transaction Item

        private BloodTransformationItemCollection BloodTransformationItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBloodTransformationItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((BloodTransformationItemCollection)(obj));
                    }
                }

                var coll = new BloodTransformationItemCollection();
                var query = new BloodTransformationItemQuery("a");
                var bn = new BloodBagNoQuery("b");
                var bt = new AppStandardReferenceItemQuery("c");
                var bgf = new AppStandardReferenceItemQuery("d");
                var bgt = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                        query,
                        bt.ItemName.As("refToAppStandardReferenceItem_BloodType"),
                        bn.BloodRhesus.As("refToBloodBagNo_BloodRhesus"),
                        bgf.ItemName.As("refToAppStandardReferenceItem_BloodGroupFrom"),
                        bgt.ItemName.As("refToAppStandardReferenceItem_BloodGroupTo")

                    );
                query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
                query.InnerJoin(bt).On(bt.StandardReferenceID == AppEnum.StandardReference.BloodType && bt.ItemID == bn.SRBloodType);
                query.InnerJoin(bgf).On(bgf.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgf.ItemID == query.SRBloodGroupFrom);
                query.InnerJoin(bgt).On(bgt.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgt.ItemID == query.SRBloodGroupTo);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                
                coll.Load(query);

                Session["collBloodTransformationItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collBloodTransformationItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            BloodTransformationItems = null; //Reset Record Detail
            grdItem.DataSource = BloodTransformationItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private BloodTransformationItem FindItem(String bagNo)
        {
            BloodTransformationItemCollection coll = BloodTransformationItems;
            BloodTransformationItem retEntity = null;
            foreach (BloodTransformationItem rec in coll)
            {
                if (rec.BagNo.Equals(bagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = BloodTransformationItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bagNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BloodTransformationItemMetadata.ColumnNames.BagNo]);
            BloodTransformationItem entity = FindItem(bagNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String bagNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BloodTransformationItemMetadata.ColumnNames.BagNo]);
            BloodTransformationItem entity = FindItem(bagNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BloodTransformationItem entity = BloodTransformationItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(BloodTransformationItem entity, GridCommandEventArgs e)
        {
            var userControl = (BloodTransformationItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BagNo = userControl.BagNo;
                entity.BloodType = userControl.BloodType;
                entity.BloodRhesus = userControl.BloodRhesus == "0" ? "+" : "-";
                entity.SRBloodGroupFrom = userControl.SrBloodGroupFrom;
                entity.BloodGroupFrom = userControl.BloodGroupFrom;
                entity.SRBloodGroupTo = userControl.SrBloodGroupTo;
                entity.BloodGroupTo = userControl.BloodGroupTo;
                entity.VolumeBag = userControl.VolumeBag;
                entity.ExpiredDateTime = userControl.ExpiredDateTime;
            }
        }

        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.BloodTransformNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}
