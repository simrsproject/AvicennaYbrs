using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
//using Temiang.Avicenna.Module.Finance.Payable.Adjustment.Adding;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class AddItem : BasePageDialog
    {
        private int _pageNo 
        {
            get { return Request.QueryString["pageNo"].ToInt(); }
        }

        private string ItemType
        {
            get { return Request.QueryString["it"]; }
        }
        private string ServiceUnitID
        {
            get { return Request.QueryString["suid"]; }
        }
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        private bool IsNewItem
        {
            get { return Request.QueryString["type"] == "new"; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.ButtonOk.ValidationGroup = "ok";
            this.ButtonCancel.Text = "Close";


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string script = string.Format("var parentPage = GetRadWindow().BrowserWindow;parentPage.RebindGridItem('rebind_{0}');return;", _pageNo);
            return script;
        }


        private bool CheckInCurrentStockOpname(ValidateArgs args, string itemID, string itemName)
        {
            var qr = new ItemTransactionItemQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.ItemID == itemID, qr.PageNo == _pageNo);
            qr.es.Top = 1;
            var it = new ItemTransactionItem();
            if (!it.Load(qr))
            {
                args.MessageText = string.Format("Item {0} not listed in this stock taking on the selected page ({1}).", itemName, _pageNo.ToString());
                return false;
            }

            // Cek approve Stat
            var appr = new ItemStockOpnameApproval();
            appr.LoadByPrimaryKey(TransactionNo, _pageNo);

            if (appr.IsApproved ?? false)
            {
                args.MessageText = "Stock taking for this item has been approved.";
                return false;
            }

            return true;
        }



        #region ComboBox ItemID
        protected void csvItemID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(cboItemID.SelectedValue) && !string.IsNullOrEmpty(cboItemID.Text);
        }
        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemID((RadComboBox)sender, e.Text, false);
        }

        private void PopulateCboItemID(RadComboBox comboBox, string textSearch, bool isUseItemID)
        {
            string searchText = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");

            if (ItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedicQuery("c");
                query.InnerJoin(medic).On(query.ItemID == medic.ItemID );
            }
            else if (ItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var nonmedic = new ItemProductNonMedicQuery("d");
                query.InnerJoin(nonmedic).On(query.ItemID == nonmedic.ItemID);
            }

            query.Select(query.ItemID, query.ItemName);
            query.Where(
                query.IsActive == true,
                query.IsNeedToBeSterilized == true
                );

            query.es.Top = 20;
            query.es.Distinct = true;

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            HideInformationHeader();
            txtQuantity.MaxLength = 10;
            txtQuantity.MinValue = 0;
            txtQuantity.MaxValue = 99999999.99;
            txtSRItemUnit.Text = string.Empty;
            txtQuantity.Value = 0;

            var args = new ValidateArgs();
            CheckInCurrentStockOpname(args, e.Value, e.Text);

            if (!string.IsNullOrEmpty(args.MessageText))
                ShowInformationHeader(args.MessageText);

            PopulateItemUnit(e.Value);
        }

        private void PopulateItemUnit(string itemID)
        {
            if (ItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var medic = new ItemProductMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
            else if (ItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var medic = new ItemProductNonMedic();
                medic.LoadByPrimaryKey(itemID);
                txtSRItemUnit.Text = medic.SRItemUnit;
            }
        }


        #endregion

        protected override void OnButtonOkClicked(ValidateArgs args)
        {

            if (!IsValid(args))
            {
                return;
            }

            int lastSeqNo = 0;

            // Cari di detail
            var itemID = cboItemID.SelectedValue;
            var note = txtNote.Text;
            var srItemUnit = string.Empty;

            // Last SequenceNo
            var itiq = new CssdStockOpnameItemQuery();
            var iti = new CssdStockOpnameItem();
            itiq.Where(itiq.TransactionNo == TransactionNo, itiq.SequenceNo.Like("E%"));
            itiq.OrderBy(itiq.SequenceNo.Descending);
            itiq.es.Top = 1;
            if (iti.Load(itiq))
            {
                lastSeqNo = iti.SequenceNo.Replace("E", "").ToInt();
            }

            itiq = new CssdStockOpnameItemQuery();
            iti = new CssdStockOpnameItem();
            itiq.Where(itiq.TransactionNo == TransactionNo, itiq.SequenceNo.NotLike("E%"), itiq.ItemID == itemID, itiq.PageNo == _pageNo);
            itiq.es.Top = 1;
            if (iti.Load(itiq))
            {
                srItemUnit = iti.SRItemUnit;
            }

            var detail = new CssdStockOpnameItem();
            detail.AddNew();
            detail.TransactionNo = TransactionNo;
            detail.SequenceNo = string.Format("E{0:0000}", lastSeqNo + 1);
            detail.PageNo = _pageNo;
            detail.ItemID = itemID;
            detail.Balance = Convert.ToDecimal(txtQuantity.Value);
            detail.BalanceReceived = 0;
            detail.BalanceDeconImmersion = 0;
            detail.BalanceDeconAbstersion = 0;
            detail.BalanceDeconDrying = 0;
            detail.BalanceFeasibilityTest = 0;
            detail.BalancePackaging = 0;
            detail.BalanceUltrasound = 0;
            detail.BalanceSterilization = 0;
            detail.BalanceDistribution = 0;
            detail.BalanceReturned = 0;
            detail.PrevBalance = 0;
            detail.PrevBalanceReceived = 0;
            detail.PrevBalanceDeconImmersion = 0;
            detail.PrevBalanceDeconAbstersion = 0;
            detail.PrevBalanceDeconDrying = 0;
            detail.PrevBalanceFeasibilityTest = 0;
            detail.PrevBalancePackaging = 0;
            detail.PrevBalanceUltrasound = 0;
            detail.PrevBalanceSterilization = 0;
            detail.PrevBalanceDistribution = 0;
            detail.Notes = txtNote.Text;

            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
            detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            detail.Save();

            cboItemID.Focus();


        }


        private bool IsValid(ValidateArgs args)
        {
            //Check Entry ItemID
            var item = new Item();

            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Selected item not valid, please select exist item.";
                return false;
            }

            var qr = new CssdStockOpnameItemQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.ItemID == cboItemID.SelectedValue, qr.PageNo == _pageNo);
            qr.es.Top = 1;
            var it = new CssdStockOpnameItem();
            if (it.Load(qr))
            {
                args.IsCancel = true;
                args.MessageText = string.Format("Item {0} with the selected have been registered in the stock taking.", cboItemID.Text);
                return false;
            }


            // Cek approve Stat
            var appr = new CssdStockOpnameApproval();
            appr.LoadByPrimaryKey(TransactionNo, _pageNo);

            if (appr.IsApproved ?? false)
            {
                args.IsCancel = true;
                args.MessageText = "Stock taking for this item has been approved.";
                return false;
            }

            return true;
        }


    }
}