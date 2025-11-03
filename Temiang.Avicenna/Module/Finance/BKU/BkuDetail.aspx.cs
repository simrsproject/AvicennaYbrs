using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class BkuDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "BkuSearch.aspx";
            UrlPageList = "BkuList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BkuMasuk;

            if (!IsPostBack)
            {
                var units = new ServiceUnitCollection();
                units.Query.Where(units.Query.IsActive == true);
                units.Query.Load();

                cboUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach( var unit in units)
                {
                    cboUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }

                var banks = new BankCollection();
                banks.Query.Where(units.Query.IsActive == true);
                banks.Query.Load();

                cboKasBank.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var bank in banks)
                {
                    cboKasBank.Items.Add(new Telerik.Web.UI.RadComboBoxItem(bank.BankName, bank.BankID));
                }
            }
        }

        protected void cboJenis_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var pelanggans = new VwPelangganBkuCollection();
            pelanggans.Query.Where(pelanggans.Query.Type.In(e.Value.ToInt(), 0));
            pelanggans.Query.Load();

            cboPelanggan.Items.Clear();
            cboPelanggan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var pelanggan in pelanggans)
            {
                cboPelanggan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(pelanggan.Name, pelanggan.Id));
            }

            if (e.Value == "1") lblKasBank.Text = "Masuk";
            else lblKasBank.Text = "Keluar";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransaksiBku();
            if (!entity.LoadByPrimaryKey(txtNomor.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = true;
            entity.IsVoid = false;
            entity.Save();
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new TransaksiBku();
            if (!entity.LoadByPrimaryKey(txtNomor.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            entity.IsApproved = false;
            entity.IsVoid = true;
            entity.Save();
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TransaksiBku());

            txtTanggal.SelectedDate = DateTime.Now.Date;
        }

        private void SetEntityValue(TransaksiBku entity)
        {
            entity.Nomor = txtNomor.Text;
            entity.Jenis = byte.Parse(cboJenis.SelectedValue);
            entity.Pelanggan = cboPelanggan.SelectedValue;
            entity.Unit = cboUnit.SelectedValue;
            entity.Tanggal = txtTanggal.SelectedDate.Value.Date;
            entity.Uraian = txtUraian.Text;
            entity.KasBank = cboKasBank.SelectedValue;

            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            foreach (var item in TransaksiBkuDetails)
            {
                item.Nomor = txtNomor.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransaksiBkuQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.Nomor > txtNomor.Text);
                que.OrderBy(que.Nomor.Ascending);
            }
            else
            {
                que.Where(que.Nomor < txtNomor.Text);
                que.OrderBy(que.Nomor.Descending);
            }
            var entity = new TransaksiBku();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransaksiBku();
            if (parameters.Length > 0)
            {
                String classID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(classID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtNomor.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var bku = (TransaksiBku)entity;
            txtNomor.Text = bku.Nomor;
            cboJenis.SelectedValue = bku.Jenis.ToString();
            cboPelanggan.SelectedValue = bku.Pelanggan;
            cboUnit.SelectedValue = bku.Unit;
            txtTanggal.SelectedDate = bku.Tanggal;
            txtUraian.Text = bku.Uraian;
            cboKasBank.SelectedValue = bku.KasBank;

            ViewState["IsApproved"] = bku.IsApproved ?? false;
            ViewState["IsVoid"] = bku.IsVoid ?? false;

            PopulateTransaksiBkuDetailGrid();
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
            auditLogFilter.PrimaryKeyData = string.Format("Nomor='{0}'", txtNomor.Text.Trim());
            auditLogFilter.TableName = "Nomor";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransaksiBkuDetail(newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransaksiBku();
            entity.LoadByPrimaryKey(txtNomor.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new TransaksiBku();
            if (entity.LoadByPrimaryKey(txtNomor.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new TransaksiBku();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(TransaksiBku entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                TransaksiBkuDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new TransaksiBku();
            if (entity.LoadByPrimaryKey(txtNomor.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected void grdDetail_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = TransaksiBkuDetails;
        }

        protected void grdDetail_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][TransaksiBkuDetailMetadata.ColumnNames.Id]);

            var entity = FindTransaksiBkuDetail(id);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdDetail_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransaksiBkuDetailMetadata.ColumnNames.Id]);

            var entity = FindTransaksiBkuDetail(id);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdDetail_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var entity = TransaksiBkuDetails.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private TransaksiBkuDetailCollection TransaksiBkuDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collTransaksiBkuDetail"];
                    if (obj != null) return ((TransaksiBkuDetailCollection)(obj));
                }

                var coll = new TransaksiBkuDetailCollection();

                var query = new TransaksiBkuDetailQuery("a");
                var coa = new ChartOfAccountsQuery("b");
                var item = new ItemQuery("c");

                query.Select(query, (coa.ChartOfAccountCode + " - " + coa.ChartOfAccountName).As("refToChartOfAccounts_ChartOfAccountCode"), item.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(coa).On(query.KodeRekening == coa.ChartOfAccountId);
                query.LeftJoin(item).On(query.KodeItem == item.ItemID);
                query.Where(query.Nomor == txtNomor.Text);
                coll.Load(query);

                Session["collTransaksiBkuDetail"] = coll;
                return coll;
            }
            set
            {
                Session["collTransaksiBkuDetail"] = value;
            }
        }

        private void RefreshCommandItemTransaksiBkuDetail(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDetail.Columns[0].Visible = isVisible;
            grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;

            grdDetail.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdDetail.Rebind();
        }

        private void PopulateTransaksiBkuDetailGrid()
        {
            //Display Data Detail
            TransaksiBkuDetails = null; //Reset Record Detail
            grdDetail.DataSource = TransaksiBkuDetails; //Requery
            grdDetail.MasterTableView.IsItemInserted = false;
            grdDetail.MasterTableView.ClearEditItems();
            grdDetail.DataBind();
        }

        private TransaksiBkuDetail FindTransaksiBkuDetail(int id)
        {
            var coll = TransaksiBkuDetails;
            return coll.FirstOrDefault(rec => rec.Id.Equals(id));
        }

        private void SetEntityValue(TransaksiBkuDetail entity, GridCommandEventArgs e)
        {
            BkuDetailItem userControl = (BkuDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.Id = userControl.TransaksiBkuDetailId;
                entity.KodeRekening = userControl.KodeRekening;
                entity.ChartOfAccountCode = userControl.ChartOfAccountCode;
                entity.KodeItem = userControl.KodeItem;
                entity.ItemName = userControl.ItemName;
                entity.Memo = userControl.Memo;
                entity.Posisi = userControl.Posisi;
                entity.Nominal = userControl.Nominal;
            }
        }

        protected void cboKode_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value)) return;

            txtNomor.Text = $"{e.Value}.{JournalCodes.GenerateAndIncrementAutoNumber(e.Value)}";
        }

        protected void cboKode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["JournalCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["JournalCode"].ToString();
        }

        protected void cboKode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var coa = new JournalCodesQuery();
            coa.es.Top = 20;
            coa.Where(coa.Or(coa.JournalCode.Like(searchText), coa.Description.Like(searchText)), coa.IsEnabled == true, coa.IsVisible == true, coa.IsBku == true);
            coa.OrderBy(coa.JournalCode.Descending);
            cboKode.DataSource = coa.LoadDataTable();
            cboKode.DataBind();
        }
    }
}