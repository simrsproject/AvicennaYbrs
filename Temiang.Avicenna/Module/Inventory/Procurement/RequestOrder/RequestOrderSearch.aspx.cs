using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["itype"]) ? string.Empty : Request.QueryString["itype"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "a" ? AppConstant.Program.RequestOrderAsset : AppConstant.Program.RequestOrder;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseRequest, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, false);

                var costUnit = new ServiceUnitCollection();
                costUnit.Query.Where(costUnit.Query.IsActive == true);
                costUnit.LoadAll();
                cboServiceUnitCostID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var u in costUnit)
                {
                    cboServiceUnitCostID.Items.Add(new RadComboBoxItem(u.ServiceUnitName, u.ServiceUnitID));
                }

                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                cboCloseStatus.Items.Add(new RadComboBoxItem("", ""));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Still Open", "2"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Closed", "3"));
                cboCloseStatus.Items.Add(new RadComboBoxItem("Void", "4"));

                trApprovedStatus.Visible = AppSession.Parameter.IsUsingApprovalPurchaseRequest;
                cboApprovedStatus.Items.Add(new RadComboBoxItem("", ""));
                cboApprovedStatus.Items.Add(new RadComboBoxItem("Unprocessed", "0"));
                cboApprovedStatus.Items.Add(new RadComboBoxItem("Has been processed", "1"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTransactionQuery("a");
            var qryserviceunit = new ServiceUnitQuery("b");
            var qryserviceunitto = new ServiceUnitQuery("c");
            var itemtype = new AppStandardReferenceItemQuery("d");
            var user = new AppUserServiceUnitQuery("e");
            var costunit = new ServiceUnitQuery("cu");

            query.Select(
                    query.TransactionNo,
                            query.TransactionDate,
                            qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                            qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                            costunit.ServiceUnitName.As("CostUnit"),
                            itemtype.ItemName,
                            query.IsInventoryItem,
                            query.IsApproved,
                            query.IsClosed,
                            query.Notes,
                            query.IsVoid,
                            query.FromLocationID,
                            query.SRItemType
                );

            if (getPageID == "a")
            {
                query.Select("<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=a' AS PrUrl>");
                query.Where(query.IsAssets == true);
            }
            else
                query.Select("<'RequestOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&cons=0&itype=' AS PrUrl>");

            query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(costunit).On(costunit.ServiceUnitID == query.ServiceUnitCostID);
            query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
            query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
            query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest);

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitCostID.SelectedValue))
                query.Where(query.ServiceUnitCostID == cboServiceUnitCostID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);

            if (!string.IsNullOrEmpty(cboCloseStatus.SelectedValue))
            {
                switch (cboCloseStatus.SelectedValue)
                {
                    case "0":
                        query.Where(query.IsApproved == false);
                        break;
                    case "1":
                        query.Where(query.IsApproved == true);
                        break;
                    case "2":
                        query.Where(query.IsClosed == false);
                        break;
                    case "3":
                        query.Where(query.IsClosed == true);
                        break;
                    case "4":
                        query.Where(query.IsVoid == true);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(cboApprovedStatus.SelectedValue))
            {
                var itiq = new ItemTransactionItemQuery("iti");
                query.InnerJoin(itiq).On(itiq.TransactionNo == query.TransactionNo);
                if (cboApprovedStatus.SelectedValue == "0")
                {
                    query.Where(query.Or(itiq.RequestQty.IsNull(), itiq.Quantity == 0));
                }
                else if (cboApprovedStatus.SelectedValue == "1")
                {
                    query.Where(itiq.RequestQty.IsNotNull(), itiq.Quantity > 0);
                }

                query.es.Distinct = true;
            }

            query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
