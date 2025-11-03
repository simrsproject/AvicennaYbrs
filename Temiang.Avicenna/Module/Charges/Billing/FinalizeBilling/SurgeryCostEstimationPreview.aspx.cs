using System;
using System.Data;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling
{
    public partial class SurgeryCostEstimationPreview : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                var cls = new ClassCollection();
                cls.Query.Where(cls.Query.IsTariffClass == true, cls.Query.IsActive == true);
                cls.Query.OrderBy(cls.Query.ClassSeq.Ascending);
                cls.LoadAll();

                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var c in cls)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }

                ButtonOk.Text = "Print";
                ButtonCancel.Visible = false;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.print = '" + Page.Request.QueryString["regNo"] + "'";
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();
            if (string.IsNullOrEmpty(cboClassID.SelectedValue))
            {
                ShowInformationHeader("Charge Class required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboItemID.SelectedValue))
            {
                ShowInformationHeader("Item required.");
                return false;
            }

            AppSession.PrintShowToolBarPrint = false;
            var jobParameters = new PrintJobParameterCollection();

            var pRegNo = jobParameters.AddNew();
            pRegNo.Name = "p_RegistrationNo";
            pRegNo.ValueString = Request.QueryString["regNo"];

            var pClassId = jobParameters.AddNew();
            pClassId.Name = "p_ClassID";
            pClassId.ValueString = cboClassID.SelectedValue;

            var pItemId = jobParameters.AddNew();
            pItemId.Name = "p_ItemID";
            pItemId.ValueString = cboItemID.SelectedValue;

            AppSession.PrintJobParameters = jobParameters;

            AppSession.PrintJobReportID =  AppSession.Parameter.ProgramIdPrintSurgeryCostEstimation;

            return true;
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var itemgroup = new ItemGroupQuery("b");
            var itemUnit = new ServiceUnitItemServiceQuery("c");

            query.es.Top = 10;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemGroupID,
                    itemgroup.ItemGroupName
                );
            query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);

            query.InnerJoin(itemUnit).On(
                            query.ItemID == itemUnit.ItemID &&
                            itemUnit.ServiceUnitID == AppSession.Parameter.ServiceUnitOperationRoomID
                        );

            query.Where(query.SRItemType == ItemType.Service,
                    query.Or(
                            itemgroup.ItemGroupName.Like(searchTextContain),
                            query.ItemGroupID.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.OrderBy(itemgroup.ItemGroupName.Ascending);

            DataTable tbl = query.LoadDataTable();

            cboItemGroupID.DataSource = tbl;
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboItemGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            cboItemID.SelectedValue = string.Empty;
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var suis = new ServiceUnitItemServiceQuery("c");

            var ig = new ItemGroupQuery("ig");
            query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);

            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                );

            query.InnerJoin(suis).On(
                            query.ItemID == suis.ItemID &&
                            suis.ServiceUnitID == AppSession.Parameter.ServiceUnitOperationRoomID);

            query.Where(query.SRItemType == ItemType.Service,
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

            DataTable tbl = query.LoadDataTable();

            cboItemID.DataSource = tbl;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}