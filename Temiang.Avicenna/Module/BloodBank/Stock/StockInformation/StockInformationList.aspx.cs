using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class StockInformationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.BloodStockInformation;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
                StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom);
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
                StandardReference.InitializeIncludeSpace(cboSRBloodGroup, AppEnum.StandardReference.BloodGroup);

                cboOrderBy.Items.Add(new RadComboBoxItem("Expired Date", "ED"));
                cboOrderBy.Items.Add(new RadComboBoxItem("Blood Type", "BloodType"));
                cboOrderBy.Items.Add(new RadComboBoxItem("Blood Group", "BloodGroup"));
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdItem.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = BloodBalances;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable BloodBalances
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboSRBloodSource.SelectedValue) && string.IsNullOrEmpty(cboSRBloodSourceFrom.SelectedValue) 
                    && string.IsNullOrEmpty(cboSRBloodType.SelectedValue) && string.IsNullOrEmpty(cboSRBloodGroup.SelectedValue) && txtExpiredDateTime.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Stock Information")) return null;

                var balanceQ = new BloodBalanceQuery("a");
                var bagnoQ = new BloodBagNoQuery("b");
                var sourceQ = new AppStandardReferenceItemQuery("c");
                var typeQ = new AppStandardReferenceItemQuery("d");
                var groupQ = new AppStandardReferenceItemQuery("e");
                var sourcefQ = new AppStandardReferenceItemQuery("f");

                balanceQ.InnerJoin(bagnoQ).On(bagnoQ.BagNo == balanceQ.BagNo);
                balanceQ.InnerJoin(sourceQ).On(sourceQ.StandardReferenceID == AppEnum.StandardReference.BloodSource && sourceQ.ItemID == balanceQ.SRBloodSource);
                balanceQ.InnerJoin(typeQ).On(typeQ.StandardReferenceID == AppEnum.StandardReference.BloodType && typeQ.ItemID == bagnoQ.SRBloodType);
                balanceQ.InnerJoin(groupQ).On(groupQ.StandardReferenceID == AppEnum.StandardReference.BloodGroup && groupQ.ItemID == bagnoQ.SRBloodGroup);
                balanceQ.InnerJoin(sourcefQ).On(sourcefQ.StandardReferenceID == AppEnum.StandardReference.BloodSourceFrom && sourcefQ.ItemID == balanceQ.SRBloodSourceFrom);
                balanceQ.Where(balanceQ.Balance > 0, bagnoQ.IsExtermination == false);
                
                if (!string.IsNullOrEmpty(cboSRBloodSource.SelectedValue))
                    balanceQ.Where(balanceQ.SRBloodSource == cboSRBloodSource.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRBloodSourceFrom.SelectedValue))
                    balanceQ.Where(balanceQ.SRBloodSourceFrom == cboSRBloodSourceFrom.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRBloodType.SelectedValue))
                    balanceQ.Where(bagnoQ.SRBloodType == cboSRBloodType.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRBloodGroup.SelectedValue))
                    balanceQ.Where(bagnoQ.SRBloodGroup == cboSRBloodGroup.SelectedValue);
                if (!txtExpiredDateTime.IsEmpty)
                    balanceQ.Where(bagnoQ.ExpiredDateTime.Date() < txtExpiredDateTime.SelectedDate.Value.Date);

                balanceQ.Select
                (
                    balanceQ.SRBloodSource,
                    sourceQ.ItemName.As("BloodSource"),
                    sourcefQ.ItemName.As("BloodSourceFrom"),
                    balanceQ.BagNo,
                    typeQ.ItemName.As("BloodType"),
                    bagnoQ.BloodRhesus,
                    groupQ.ItemName.As("BloodGroup"),
                    bagnoQ.VolumeBag.Coalesce("0"),
                    bagnoQ.ExpiredDateTime,
                    balanceQ.Balance, 
                    bagnoQ.IsCrossMatching
                );

                if (cboOrderBy.SelectedValue == "ED")
                {
                    if (rblOrderBy.SelectedIndex == 0)
                    {
                        balanceQ.OrderBy(bagnoQ.ExpiredDateTime.Ascending, groupQ.ItemName.Ascending, typeQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                    else
                    {
                        balanceQ.OrderBy(bagnoQ.ExpiredDateTime.Descending, groupQ.ItemName.Ascending, typeQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                }
                else if (cboOrderBy.SelectedValue == "BloodType")
                {
                    if (rblOrderBy.SelectedIndex == 0)
                    {
                        balanceQ.OrderBy(typeQ.ItemName.Ascending, bagnoQ.ExpiredDateTime.Ascending, groupQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                    else
                    {
                        balanceQ.OrderBy(typeQ.ItemName.Descending, bagnoQ.ExpiredDateTime.Ascending, groupQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                }
                else if (cboOrderBy.SelectedValue == "BloodGroup")
                {
                    if (rblOrderBy.SelectedIndex == 0)
                    {
                        balanceQ.OrderBy(groupQ.ItemName.Ascending, bagnoQ.ExpiredDateTime.Ascending, typeQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                    else
                    {
                        balanceQ.OrderBy(groupQ.ItemName.Descending, bagnoQ.ExpiredDateTime.Ascending, typeQ.ItemName.Ascending, balanceQ.BagNo.Ascending);
                    }
                }
                
                return balanceQ.LoadDataTable();
            }
        }
    }
}
