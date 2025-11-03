using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequest2Search : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "" ? AppConstant.Program.ItemServiceTariffRequest2 :
                 (getPageID == "import" ? AppConstant.Program.ItemServiceTariffRequestImport : AppConstant.Program.ItemServiceTariffRequestImportNew);

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRTariffType, AppEnum.StandardReference.TariffType);

                cboSRItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSRItemType.Items.Add(new RadComboBoxItem("Service", BusinessObject.Reference.ItemType.Service));
                cboSRItemType.Items.Add(new RadComboBoxItem("Laboratory", BusinessObject.Reference.ItemType.Laboratory));
                cboSRItemType.Items.Add(new RadComboBoxItem("Radiology", BusinessObject.Reference.ItemType.Radiology));
                cboSRItemType.Items.Add(new RadComboBoxItem("Package", BusinessObject.Reference.ItemType.Package));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemTariffRequest2Query("a");
            var tariffType = new AppStandardReferenceItemQuery("b");
            var itemType = new AppStandardReferenceItemQuery("c");
            var igroup = new ItemGroupQuery("d");
            query.InnerJoin(tariffType).On(query.SRTariffType == tariffType.ItemID &
                                           tariffType.StandardReferenceID ==
                                           AppEnum.StandardReference.TariffType.ToString());
            query.InnerJoin(itemType).On(query.SRItemType == itemType.ItemID &
                                         itemType.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString());
            query.LeftJoin(igroup).On(igroup.ItemGroupID == query.ItemGroupID);
            query.Select(query.SelectAllExcept(), itemType.ItemName.As("ItemTypeName"),
                         tariffType.ItemName.As("TariffTypeName"), igroup.ItemGroupName);
            query.Where(query.IsApproved == chkIsApproved.Checked);
            query.Where(query.SRItemType.NotIn(BusinessObject.Reference.ItemType.Medical,
                                               BusinessObject.Reference.ItemType.NonMedical, BusinessObject.Reference.ItemType.Kitchen));
            if (getPageID == "")
                query.Where(query.IsImport == false);
            else if (getPageID == "import")
                query.Where(query.IsImport == true, query.IsNew == false);
            else query.Where(query.IsImport == true, query.IsNew == true);

            if (!string.IsNullOrEmpty(txtTariffRequestNo.Text))
            {
                if (cboFilterTariffRequestNo.SelectedIndex == 1)
                    query.Where(query.TariffRequestNo == txtTariffRequestNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTariffRequestNo.Text);
                    query.Where(query.TariffRequestNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRTariffType.SelectedValue))
                query.Where(query.SRTariffType == cboSRTariffType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                query.Where(query.SRItemType == cboSRItemType.SelectedValue);
            if (!txtStartingDate.IsEmpty)
                query.Where(query.StartingDate == txtStartingDate.SelectedDate);


            query.OrderBy(query.TariffRequestNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
