using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.ServiceUnitTransaction
{
    public partial class VisiteEntry : BasePageDialogEntry
    {
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        private string ItemID
        {
            get { return Request.QueryString["itemid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.AddVisible = false;

            ProgramID = AppConstant.Program.ServiceUnitTransaction;
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            this.Page.Title = "Patient Visite Order";
        }

        TransChargesVisiteItem _visite;
        private TransChargesVisiteItem Visite
        {
            get
            {
                if (_visite == null)
                {
                    var visite = new TransChargesVisiteItem();
                    if (visite.LoadByPrimaryKey(TransactionNo, ItemID))
                    {
                        _visite = visite;
                    }
                    else
                        _visite = new TransChargesVisiteItem();
                }
                return _visite;
            }
            set { _visite = value; }
        }

        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
            if (txtVisitQty.Value > lblChargeQuantity.Text.ToInt())
            {
                args.IsCancel = true;
                args.MessageText = string.Format("Visite Qty max {0}", lblChargeQuantity.Text.ToInt()); ;
            }
            if (txtVisitQty.Value < Visite.RealizationQty)
            {
                args.IsCancel = true;
                args.MessageText = string.Format("Visite qty can't less than visite realization {0}", Visite.RealizationQty); ;
            }
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var item = new Item();
            item.LoadByPrimaryKey(ItemID);
            lblItemName.Text = item.ItemName;

            var tci = new TransChargesItemQuery();
            tci.Select((tci.ChargeQuantity * tci.Price).Sum().As("Amount")
                , (tci.ChargeQuantity * tci.DiscountAmount).Sum().As("DiscountAmount")
                , tci.ChargeQuantity.Sum().As("ChargeQuantity"));
            tci.Where(tci.TransactionNo == TransactionNo && tci.ItemID == ItemID);
            var dtbTci = tci.LoadDataTable();
            if (dtbTci.Rows.Count > 0)
            {
                lblAmount.Text = string.Format("{0:n2}", dtbTci.Rows[0]["Amount"]);
                lblDiscountAmount.Text = string.Format("{0:n2}", dtbTci.Rows[0]["DiscountAmount"]);
                lblChargeQuantity.Text = string.Format("{0:n0}", dtbTci.Rows[0]["ChargeQuantity"]);
            }

            var visite = Visite;
            txtVisitQty.Value = visite.VisiteQty??0;
            txtNotes.Text = visite.Notes;

            lblRealizationQty.Text = string.Format("{0:n0}", visite.RealizationQty??0);
            lblVisitQtyInfo.Text = string.Format("{0:n0}", visite.VisiteQty??0);

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var visite = Visite;
            if (string.IsNullOrEmpty(visite.TransactionNo))
            {
                visite = new TransChargesVisiteItem();
                visite.TransactionNo = TransactionNo;
                visite.ItemID = ItemID;
                visite.RealizationQty = 0;
                visite.IsClosed = false;
            }
            visite.VisiteQty = (Int16)txtVisitQty.Value;
            visite.Notes = txtNotes.Text;
            visite.Save();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            // Anggap sbg new record
            if (txtVisitQty.Value == 0)
            {
                txtVisitQty.Value = lblVisitQtyInfo.Text.ToInt() - 1;
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var visite = Visite;
            if (!string.IsNullOrEmpty(visite.TransactionNo) && visite.RealizationQty > 0)
            {
                args.MessageText = "This visite can't delete cause has realization";
                args.IsCancel = true;
            }
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !string.IsNullOrEmpty(Visite.TransactionNo);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = VisitRealization();
        }
        private DataTable VisitRealization()
        {
            var query = new TransChargesVisiteItemRealizationQuery("vr");
            var reg = new RegistrationQuery("q");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var su = new ServiceUnitQuery("s");
            query.InnerJoin(su).On(reg.ServiceUnitID==su.ServiceUnitID);

            var par = new ParamedicQuery("par");
            query.InnerJoin(par).On(reg.ParamedicID==par.ParamedicID);
            query.Select(query.RegistrationNo, reg.RegistrationDate, su.ServiceUnitName, par.ParamedicName);

            query.Where(query.TransactionNo == TransactionNo, query.ItemID == ItemID);

            var dtb = query.LoadDataTable();
            return dtb;
        }
    }
}
