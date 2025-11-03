// TODO: Modif rubah item dari picklist ServiceUnit Transaction berdasarkan PaymentNo yg dipilih

using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitVisiteEntryDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "ServiceUnitVisiteEntryList.aspx";

            ProgramID = AppConstant.Program.ServiceUnitVisiteEntry;
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransPayment());

            txtPaymentNo.Text = GetNewPaymentNo();

            txtPaymentDate.SelectedDate = DateTime.Now;
            txtPaymentTime.Text = DateTime.Now.ToString("HH:mm");
            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            txtServiceUnitID.Text = reg.ServiceUnitID;
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            txtPrintReceiptAsName.Text = patient.PatientName;

            txtGuarantorID.Text = reg.GuarantorID;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guarantor.GuarantorName;

            ViewState["IsVoid"] = false;
            ViewState["IsApproved"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("PaymentNo='{0}'", txtPaymentNo.Text.Trim());
            auditLogFilter.TableName = "TransPayment";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransPaymentItemVisite(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPayment();
            if (parameters.Length > 0)
            {
                String paymentNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paymentNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtPaymentNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            using (new esTransactionScope())
            {
                var transPayment = (TransPayment)entity;
                txtPaymentNo.Text = transPayment.PaymentNo;
                txtRegistrationNo.Text = transPayment.RegistrationNo;

                var registration = new Registration();
                registration.LoadByPrimaryKey(txtRegistrationNo.Text);
                txtServiceUnitID.Text = registration.ServiceUnitID;
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.str.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                }

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;

                txtPrintReceiptAsName.Text = transPayment.PrintReceiptAsName;
                txtGuarantorID.Text = registration.GuarantorID;
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
                lblGuarantorName.Text = guarantor.GuarantorName;

                txtPaymentDate.SelectedDate = transPayment.PaymentDate;
                txtPaymentTime.Text = transPayment.PaymentTime;

                ViewState["IsVoid"] = transPayment.IsVoid ?? false;
                ViewState["IsApproved"] = transPayment.IsApproved ?? false;

                txtNotes.Text = transPayment.Notes;

                rblPaymentType.SelectedValue = (transPayment.IsPackagePaymentPerVisit ?? false) ? "1" : "0";
                PopulateTransPaymentItemVisiteGrid();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(TransPayment entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New) txtPaymentNo.Text = GetNewPaymentNo();
            entity.PaymentNo = txtPaymentNo.Text;
            entity.TransactionCode = TransactionCode.DownPayment;
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.PaymentDate = txtPaymentDate.SelectedDate;
            entity.PaymentTime = txtPaymentTime.Text;
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.TotalPaymentAmount = 0;
            entity.RemainingAmount = 0;
            entity.PrintNumber = 0;
            entity.CreatedBy = AppSession.UserLogin.UserID;
            entity.IsVoid = false;
            entity.IsApproved = rblPaymentType.SelectedValue == "1";
            entity.Notes = txtNotes.Text;
            entity.IsVisiteDownPayment = rblPaymentType.SelectedValue == "0";
            entity.GuarantorID = txtGuarantorID.Text;
            entity.IsPackagePaymentPerVisit = rblPaymentType.SelectedValue == "1";

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            foreach (var item in TransPaymentItemVisites)
            {
                item.PaymentNo = entity.PaymentNo;
                item.PatientID = reg.PatientID;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(TransPayment entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New) _autoNumber.Save();

                entity.Save();
                TransPaymentItemVisites.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPaymentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                    que.PaymentNo > txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.DownPayment
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.PaymentNo < txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.DownPayment
                    );
                que.OrderBy(que.PaymentNo.Descending);
            }

            var entity = new TransPayment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        private string GetNewPaymentNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.DownPaymentNo);
            return _autoNumber.LastCompleteNumber;
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        #region Record Detail Methode Fucntion TransPaymentItemVisites
        private TransPaymentItemVisiteCollection TransPaymentItemVisites
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTransPaymentItemVisite" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((TransPaymentItemVisiteCollection)(obj));
                    }
                }

                var coll = new TransPaymentItemVisiteCollection();

                var query = new TransPaymentItemVisiteQuery("a");
                var item = new ItemQuery("b");
                var unit = new ServiceUnitQuery("c");

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName"),
                    unit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.Where(query.PaymentNo == txtPaymentNo.Text);

                coll.Load(query);

                Session["collTransPaymentItemVisite" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransPaymentItemVisite" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemTransPaymentItemVisite(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdVisiteItem.Columns[0].Visible = isVisible;
            grdVisiteItem.Columns[grdVisiteItem.Columns.Count - 1].Visible = isVisible;

            grdVisiteItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            if (oldVal != AppEnum.DataMode.Read)
                TransPaymentItemVisites = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdVisiteItem.Rebind();
        }

        private void PopulateTransPaymentItemVisiteGrid()
        {
            //Display Data Detail
            TransPaymentItemVisites = null; //Reset Record Detail
            grdVisiteItem.DataSource = TransPaymentItemVisites; //Requery
            grdVisiteItem.MasterTableView.IsItemInserted = false;
            grdVisiteItem.MasterTableView.ClearEditItems();
            grdVisiteItem.DataBind();
        }

        protected void grdVisiteItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVisiteItem.DataSource = TransPaymentItemVisites;
        }

        protected void grdVisiteItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var entity = FindTransPaymentItemVisite((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [TransPaymentItemVisiteMetadata.ColumnNames.ItemID]);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdVisiteItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String itemID = item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPaymentItemVisiteMetadata.ColumnNames.ItemID].ToString();
            var entity = FindTransPaymentItemVisite(itemID);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdVisiteItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransPaymentItemVisites.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdVisiteItem.Rebind();
        }

        private TransPaymentItemVisite FindTransPaymentItemVisite(String itemID)
        {
            TransPaymentItemVisiteCollection coll = TransPaymentItemVisites;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        private void SetEntityValue(TransPaymentItemVisite entity, GridCommandEventArgs e)
        {
            var userControl = (ItemVisiteDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.VisiteQty = userControl.Qty;
                entity.Price = userControl.Price;
                entity.Discount = userControl.Discount;
                entity.ServiceUnitID = userControl.ServiceUnitID;
                entity.ServiceUnitName = userControl.ServiceUnitName;
                entity.ExpiredDate = userControl.ExpiredDate;
            }
        }
        #endregion
    }
}