using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ProductionOfGoodsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ProductionOfGoods;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.ProductionOfGoods, true);
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "4"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ProductionOfGoodsQuery("a");
            var suQ = new ServiceUnitQuery("b");
            var pfQ = new ProductionFormulaQuery("c");
            var itQ = new ItemQuery("d");
            var usrQ = new AppUserServiceUnitQuery("e");
            
            if (!string.IsNullOrEmpty(txtProductionNo.Text))
            {
                if (cboFilterProductionNo.SelectedIndex == 1)
                    query.Where(query.ProductionNo == txtProductionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProductionNo.Text);
                    query.Where(query.ProductionNo.Like(searchTextContain));
                }
            }
            if (!txtProductionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.ProductionDate == txtProductionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboFormulaID.SelectedValue))
            {
                query.Where(query.FormulaID == cboFormulaID.SelectedValue);
            }
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);

            query.InnerJoin(suQ).On(query.ServiceUnitID == suQ.ServiceUnitID);
            query.InnerJoin(pfQ).On(query.FormulaID == pfQ.FormulaID);
            query.InnerJoin(itQ).On(pfQ.ItemID == itQ.ItemID);
            query.InnerJoin(usrQ).On(query.ServiceUnitID == usrQ.ServiceUnitID &&
                                     usrQ.UserID == AppSession.UserLogin.UserID);
            query.OrderBy(query.ProductionDate.Descending, query.ProductionNo.Descending);

            query.Select
                        (
                           query.ProductionNo,
                           query.ProductionDate,
                           suQ.ServiceUnitName,
                           query.IsApproved,
                           query.IsVoid,
                           query.Notes,
                           pfQ.FormulaName,
                           itQ.ItemName,
                           (query.Qty * pfQ.Qty).As("Qty")
                       );

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        protected void cboFormulaID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ProductionFormulaQuery("a");
            var itemQ = new ItemQuery("b");

            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);

            query.es.Top = 10;
            query.Select
                (
                    query.FormulaID,
                    query.FormulaName,
                    itemQ.ItemName,
                    itemQ.SRItemType
                );
            query.Where
                (
                    query.Or
                        (
                            query.FormulaID.Like(searchTextContain),
                            query.FormulaName.Like(searchTextContain)
                        ),
                        itemQ.IsActive == true

                );

            var dtb = query.LoadDataTable();

            cboFormulaID.DataSource = dtb;
            cboFormulaID.DataBind();
        }

        protected void cboFormulaID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FormulaName"] + " (" + ((DataRowView)e.Item.DataItem)["FormulaID"] + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FormulaID"].ToString();
        }
    }
}
